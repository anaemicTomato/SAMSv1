using SAMSv1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace SAMSv1.Services
{
    /// <summary>
    /// Hikvision-specific attendance device.
    /// 
    /// INHERITANCE:   Extends AttendanceDevice, overrides all abstract methods.
    /// ENCAPSULATION: Every Hikvision detail (SDK calls, HTTP, JSON parsing)
    ///                is private — the rest of the app never sees it.
    /// </summary>
    public class HikvisionDevice : AttendanceDevice
    {
        // ENCAPSULATION: SDK user ID is private — no other class
        // should ever touch this directly
        private int _userId = -1;

        // ── SDK Init guard — Init/Cleanup must only happen ONCE per app lifetime ──
        private static bool _sdkInitialized = false;
        private static readonly object _sdkLock = new object();

        // ── DLL IMPORTS (private — hidden from outside) ───────────
        [DllImport(@"..\..\..\HCNetSDK\HCNetSDK.dll")] private static extern bool NET_DVR_Init();
        [DllImport(@"..\..\..\HCNetSDK\HCNetSDK.dll")] private static extern bool NET_DVR_Cleanup();
        [DllImport(@"..\..\..\HCNetSDK\HCNetSDK.dll")] private static extern uint NET_DVR_GetLastError();
        [DllImport(@"..\..\..\HCNetSDK\HCNetSDK.dll")] private static extern bool NET_DVR_Logout(int lUserID);

        [DllImport(@"..\..\..\HCNetSDK\HCNetSDK.dll")]
        private static extern int NET_DVR_Login_V30(
            string sDVRIP, int wDVRPort,
            string sUserName, string sPassword,
            ref NET_DVR_DEVICEINFO_V30 lpDeviceInfo);

        [StructLayout(LayoutKind.Sequential)]
        private struct NET_DVR_DEVICEINFO_V30
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)] public byte[] sSerialNumber;
            public byte byAlarmInPortNum, byAlarmOutPortNum, byDiskNum, byDVRType;
            public byte byChanNum, byStartChan, byAudioChanNum, byIPChanNum;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)] public byte[] byRes1;
        }

        // INHERITANCE: calls base constructor to store config
        public HikvisionDevice(string ip, int port, string user, string pass)
            : base(ip, port, user, pass) { }

        // ═════════════════════════════════════════════════════════
        // ABSTRACT METHOD OVERRIDES
        // ═════════════════════════════════════════════════════════

        public override bool Connect()
        {
            // ── SDK Init — only ever called once per app lifetime ──
            lock (_sdkLock)
            {
                if (!_sdkInitialized)
                {
                    if (!NET_DVR_Init())
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"[SDK] NET_DVR_Init failed — error {NET_DVR_GetLastError()}");
                        return false;
                    }
                    _sdkInitialized = true;
                    System.Diagnostics.Debug.WriteLine("[SDK] NET_DVR_Init called once — OK");
                }
            }

            // Logout first if already logged in (reconnect scenario)
            if (_userId >= 0)
            {
                NET_DVR_Logout(_userId);
                _userId = -1;
            }

            var devInfo = new NET_DVR_DEVICEINFO_V30
            {
                sSerialNumber = new byte[48],
                byRes1 = new byte[24]
            };

            _userId = NET_DVR_Login_V30(Ip, Port, User, Pass, ref devInfo);

            System.Diagnostics.Debug.WriteLine(
                _userId >= 0
                    ? $"[SDK] Login OK — UserID={_userId}"
                    : $"[SDK] Login FAILED — error {NET_DVR_GetLastError()}");

            return _userId >= 0;
        }

        public override void Disconnect()
        {
            if (_userId >= 0)
            {
                NET_DVR_Logout(_userId);
                System.Diagnostics.Debug.WriteLine($"[SDK] Logout — UserID={_userId}");
                _userId = -1;
            }
            // ── DO NOT call NET_DVR_Cleanup() here ──
            // It is called only once on app exit via SDKCleanup()
        }

        // ── Call this ONCE on application exit (e.g. AdminFormV3.OnFormClosing) ──
        public static void SDKCleanup()
        {
            lock (_sdkLock)
            {
                if (_sdkInitialized)
                {
                    NET_DVR_Cleanup();
                    _sdkInitialized = false;
                    System.Diagnostics.Debug.WriteLine("[SDK] NET_DVR_Cleanup called on exit");
                }
            }
        }

        public override long GetLatestSerialNo()
        {
            try
            {
                string start = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd") + "T00:00:00+08:00";
                string end = DateTime.Now.ToString("yyyy-MM-dd") + "T23:59:59+08:00";

                string countResp = PostISAPI(
                    "/ISAPI/AccessControl/AcsEvent?format=json",
                    BuildEventQuery(0, 1, start, end));

                if (string.IsNullOrEmpty(countResp)) return 0;

                int total = ParseIntField(countResp, "totalMatches");
                if (total <= 0) return 0;

                string lastResp = PostISAPI(
                    "/ISAPI/AccessControl/AcsEvent?format=json",
                    BuildEventQuery(total - 1, 1, start, end));

                if (string.IsNullOrEmpty(lastResp)) return 0;

                int infoIdx = lastResp.IndexOf("\"InfoList\"");
                if (infoIdx < 0) return 0;

                return ParseLongField(lastResp.Substring(infoIdx), "serialNo");
            }
            catch (WebException wex)
            {
                throw new Exception("GetLatestSerialNo error: " + ReadWebError(wex));
            }
            catch (Exception ex)
            {
                throw new Exception("GetLatestSerialNo error: " + ex.Message);
            }
        }

        public override List<Attendance> PollNewEvents(
            long afterSerialNo, int windowMinutes = 2)
        {
            var results = new List<Attendance>();

            try
            {
                string start = DateTime.Now.ToString("yyyy-MM-dd") + "T00:00:00+08:00";
                string end = DateTime.Now.ToString("yyyy-MM-dd") + "T23:59:59+08:00";

                int position = 0;
                const int batch = 30;

                while (true)
                {
                    string response = PostISAPI(
                        "/ISAPI/AccessControl/AcsEvent?format=json",
                        BuildEventQuery(position, batch, start, end));

                    if (string.IsNullOrEmpty(response)) break;

                    int numMatches = ParseIntField(response, "numOfMatches");
                    int total = ParseIntField(response, "totalMatches");

                    if (numMatches == 0) break;

                    foreach (var evt in ParseEvents(response))
                    {
                        if (evt.SerialNo > afterSerialNo)
                            results.Add(evt);
                    }

                    if (position + numMatches >= total || numMatches < batch) break;
                    position += numMatches;
                }
            }
            catch (WebException wex)
            {
                throw new Exception("Device HTTP error: " + ReadWebError(wex));
            }

            return results;
        }

        public override List<DeviceStudent> GetEnrolledStudents()
        {
            var result = new List<DeviceStudent>();
            int position = 0;
            const int batch = 50;

            try
            {
                while (true)
                {
                    string body =
                        "{\"UserInfoSearchCond\":{" +
                        "\"searchID\":\"1\"," +
                        "\"searchResultPosition\":" + position + "," +
                        "\"maxResults\":" + batch +
                        "}}";

                    string response = PostISAPI(
                        "/ISAPI/AccessControl/UserInfo/Search?format=json", body);

                    if (string.IsNullOrEmpty(response)) break;

                    // Device returns statusCode with no UserInfo when list is empty
                    if (response.Contains("\"statusCode\"") &&
                        !response.Contains("\"UserInfo\"")) break;

                    int total = ParseIntField(response, "totalMatches");
                    int numMatches = ParseIntField(response, "numOfMatches");

                    var page = ParseDeviceStudents(response);
                    if (page.Count == 0) break;

                    result.AddRange(page);

                    if (result.Count >= total || page.Count < batch) break;
                    position += page.Count;
                }
            }
            catch { /* device offline — return whatever we fetched so far */ }

            return result;
        }

        public override bool RegisterStudent(string idNumber, string fullName)
        {
            try
            {
                string body =
                    "{\"UserInfo\":{" +
                    "\"employeeNo\":\"" + idNumber + "\"," +
                    "\"name\":\"" + fullName + "\"," +
                    "\"userType\":\"normal\"," +
                    "\"closeDelayEnabled\":false," +
                    "\"Valid\":{" +
                    "\"enable\":true," +
                    "\"beginTime\":\"2020-01-01T00:00:00\"," +
                    "\"endTime\":\"2030-12-31T23:59:59\"" +
                    "}," +
                    "\"doorRight\":\"1\"," +
                    "\"RightPlan\":[{\"doorNo\":1,\"planTemplateNo\":\"1\"}]" +
                    "}}";

                string response = PutISAPI(
                    "/ISAPI/AccessControl/UserInfo/SetUp?format=json", body);

                return response.Contains("\"statusString\":\"OK\"") ||
                       response.Contains("\"statusCode\":1");
            }
            catch { return false; }
        }

        // ═════════════════════════════════════════════════════════
        // PRIVATE — HTTP (hidden by Encapsulation)
        // ═════════════════════════════════════════════════════════

        private string PostISAPI(string path, string jsonBody)
        {
            var req = (HttpWebRequest)WebRequest.Create($"http://{Ip}{path}");
            req.Method = "POST";
            req.ContentType = "application/json;charset=UTF-8";
            req.Credentials = new NetworkCredential(User, Pass);
            req.PreAuthenticate = false;
            req.Timeout = 15000;

            byte[] data = Encoding.UTF8.GetBytes(jsonBody);
            req.ContentLength = data.Length;
            using (var s = req.GetRequestStream()) s.Write(data, 0, data.Length);
            using (var resp = (HttpWebResponse)req.GetResponse())
            using (var reader = new StreamReader(resp.GetResponseStream()))
                return reader.ReadToEnd();
        }

        private string PutISAPI(string path, string jsonBody)
        {
            var req = (HttpWebRequest)WebRequest.Create($"http://{Ip}{path}");
            req.Method = "PUT";
            req.ContentType = "application/json;charset=UTF-8";
            req.Credentials = new NetworkCredential(User, Pass);
            req.PreAuthenticate = false;
            req.Timeout = 15000;

            byte[] data = Encoding.UTF8.GetBytes(jsonBody);
            req.ContentLength = data.Length;
            using (var s = req.GetRequestStream()) s.Write(data, 0, data.Length);
            using (var resp = (HttpWebResponse)req.GetResponse())
            using (var reader = new StreamReader(resp.GetResponseStream()))
                return reader.ReadToEnd();
        }

        private string ReadWebError(WebException wex)
        {
            if (wex.Response == null) return wex.Message;
            using (var s = wex.Response.GetResponseStream())
            using (var r = new StreamReader(s))
                return r.ReadToEnd();
        }

        // ═════════════════════════════════════════════════════════
        // PRIVATE — BUILDERS & PARSERS (hidden by Encapsulation)
        // ═════════════════════════════════════════════════════════

        private string BuildEventQuery(int position, int maxResults,
                                       string start, string end)
        {
            return "{\"AcsEventCond\":{" +
                   "\"searchID\":\"1\"," +
                   "\"searchResultPosition\":" + position + "," +
                   "\"maxResults\":" + maxResults + "," +
                   "\"major\":0," +
                   "\"minor\":0," +
                   "\"startTime\":\"" + start + "\"," +
                   "\"endTime\":\"" + end + "\"" +
                   "}}";
        }

        private List<Attendance> ParseEvents(string json)
        {
            var result = new List<Attendance>();
            string[] blocks = json.Split(
                new[] { "\"major\":" }, StringSplitOptions.None);

            for (int i = 1; i < blocks.Length; i++)
            {
                string b = blocks[i];

                long serialNo = 0;
                int snIdx = b.IndexOf("\"serialNo\"");
                if (snIdx >= 0)
                    long.TryParse(ExtractValue(b, b.IndexOf(':', snIdx) + 1), out serialNo);

                string idNumber = string.Empty;
                int eIdx = b.IndexOf("\"employeeNoString\"");
                if (eIdx >= 0)
                    idNumber = ExtractValue(b, b.IndexOf(':', eIdx) + 1) ?? string.Empty;

                if (string.IsNullOrWhiteSpace(idNumber) || idNumber == "0") continue;

                string date = string.Empty, timeIn = string.Empty;
                int tIdx = b.IndexOf("\"time\"");
                if (tIdx >= 0)
                {
                    int tq1 = b.IndexOf('"', b.IndexOf(':', tIdx) + 1);
                    int tq2 = b.IndexOf('"', tq1 + 1);
                    if (tq1 >= 0 && tq2 > tq1)
                    {
                        string raw = b.Substring(tq1 + 1, tq2 - tq1 - 1);
                        int plusIdx = raw.IndexOf('+', 10);
                        if (plusIdx > 0) raw = raw.Substring(0, plusIdx);
                        raw = raw.Replace("T", " ").Trim();
                        date = raw.Length >= 10 ? raw.Substring(0, 10) : raw;
                        timeIn = raw.Length > 11 ? raw.Substring(11) : raw;
                    }
                }

                string rawStatus = string.Empty;
                int asIdx = b.IndexOf("\"attendanceStatus\"");
                if (asIdx >= 0)
                    rawStatus = ExtractValue(b, b.IndexOf(':', asIdx) + 1) ?? string.Empty;

                // INHERITANCE: MapStatus() defined once in base class
                string status = MapStatus(rawStatus);

                result.Add(new Attendance(
                    0,               // AttendanceID — not known yet
                    0,               // StudentID    — not known yet
                    date,
                    timeIn,
                    null,            // TimeOut      — not scanned out yet
                    status,
                    serialNo,
                    string.Empty,    // FullName     — filled in by FaceService
                    idNumber.Trim()
                ));
            }
            return result;
        }

        private List<DeviceStudent> ParseDeviceStudents(string json)
        {
            var result = new List<DeviceStudent>();
            string[] blocks = json.Split(
                new[] { "\"employeeNo\"" }, StringSplitOptions.None);

            for (int i = 1; i < blocks.Length; i++)
            {
                string b = blocks[i];

                string idNumber = ExtractValue(b, b.IndexOf(':') + 1);
                if (string.IsNullOrWhiteSpace(idNumber)) continue;

                string name = string.Empty;
                int nIdx = b.IndexOf("\"name\"");
                if (nIdx >= 0)
                    name = ExtractValue(b, b.IndexOf(':', nIdx) + 1) ?? string.Empty;

                string cardNo = string.Empty;
                int cIdx = b.IndexOf("\"cardNo\"");
                if (cIdx >= 0)
                    cardNo = ExtractValue(b, b.IndexOf(':', cIdx) + 1) ?? string.Empty;

                result.Add(new DeviceStudent(
                    idNumber.Trim(), name.Trim(), cardNo.Trim()));
            }
            return result;
        }

        private int ParseIntField(string json, string field)
        {
            string key = $"\"{field}\":";
            int idx = json.IndexOf(key);
            if (idx < 0) return 0;
            int start = idx + key.Length;
            while (start < json.Length && (json[start] == ' ' || json[start] == '\t' ||
                   json[start] == '\r' || json[start] == '\n')) start++;
            if (start < json.Length && json[start] == '"') start++;
            int end = start;
            while (end < json.Length && (char.IsDigit(json[end]) || json[end] == '-')) end++;
            int.TryParse(json.Substring(start, end - start), out int val);
            return val;
        }

        private long ParseLongField(string json, string field)
        {
            string key = $"\"{field}\":";
            int idx = json.IndexOf(key);
            if (idx < 0) return 0;
            int start = idx + key.Length;
            while (start < json.Length && (json[start] == ' ' || json[start] == '\t' ||
                   json[start] == '\r' || json[start] == '\n')) start++;
            if (start < json.Length && json[start] == '"') start++;
            int end = start;
            while (end < json.Length && (char.IsDigit(json[end]) || json[end] == '-')) end++;
            long.TryParse(json.Substring(start, end - start), out long val);
            return val;
        }

        private string ExtractValue(string src, int startPos)
        {
            while (startPos < src.Length && (src[startPos] == ' ' || src[startPos] == '\t' ||
                   src[startPos] == '\r' || src[startPos] == '\n')) startPos++;
            if (startPos >= src.Length) return null;
            if (src[startPos] == '"')
            {
                int start = startPos + 1;
                int end = src.IndexOf('"', start);
                return end < 0 ? null : src.Substring(start, end - start);
            }
            int endPos = startPos;
            while (endPos < src.Length && (char.IsDigit(src[endPos]) || src[endPos] == '-')) endPos++;
            return endPos == startPos ? null : src.Substring(startPos, endPos - startPos);
        }
    }
}