using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Script.Serialization;
using SAMSv1.Data;
using SAMSv1.Models;

namespace SAMSv1.Services
{
    // ===================================================================
    //  HikvisionSDKService.cs
    //
    //  Uses the Hikvision HCNetSDK.dll to:
    //    1. CONNECT  — Log into the device using the SDK
    //    2. LISTEN   — Receive face scan events in real-time via callback
    //    3. REGISTER — Push a student's profile + face photo to the device
    //
    //  HOW THE REAL-TIME CALLBACK WORKS:
    //    - We call NET_DVR_SetupAlarmChan_V41() which tells the device
    //      "start sending me events".
    //    - The device calls our AlarmCallback() function the moment a
    //      face is scanned — no polling, no delay.
    //    - Inside AlarmCallback() we parse the event data, look up the
    //      student, and save the attendance record.
    // ===================================================================
    public static class HikvisionSDKService
    {
        // ---------------------------------------------------------------
        //  CONFIG
        // ---------------------------------------------------------------
        private const string DeviceIP   = "192.168.1.65";
        private const string DeviceUser = "admin";
        private const string DevicePass = "DMC2026#";
        private const ushort DevicePort = 8000;   // SDK port (NOT the HTTP port 80)

        // Path to the SDK folder inside your project.
        // At runtime the .exe runs from bin/x64/Debug — so we go up to find SDK/.
        // Adjust this if your folder structure is different.
        private static readonly string SdkPath = Path.GetFullPath(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\SDK"));

        // ---------------------------------------------------------------
        //  STATE
        // ---------------------------------------------------------------
        private static int _userID      = -1;   // SDK login handle (-1 = not logged in)
        private static int _alarmHandle = -1;   // alarm channel handle

        // We must keep a reference to the callback delegate alive for the
        // entire lifetime of the app. If it gets garbage collected, the
        // SDK will crash when it tries to call it.
        private static HCNetSDK.MSGCallBack _callbackRef;

        // ---------------------------------------------------------------
        //  EVENT — fires on the UI thread when a face scan is recorded
        // ---------------------------------------------------------------
        public static event Action<string> OnAttendanceRecorded;

        // ---------------------------------------------------------------
        //  STEP 1: Initialize the SDK
        //
        //  Call this ONCE when the app starts (e.g. in Program.cs or
        //  when AdminFormv2 loads).
        //
        //  What it does:
        //    - Tells the SDK where its support files (DLLs) are.
        //    - Initializes internal SDK state.
        // ---------------------------------------------------------------
        public static bool InitializeSDK()
        {
            try
            {
                // Tell the SDK where to find its own support DLLs.
                // Without this, it may fail to load on some machines.
                var sdkPathStruct = new HCNetSDK.NET_DVR_LOCAL_SDK_PATH
                {
                    sPath = SdkPath,
                    byRes = new byte[128]
                };

                IntPtr pSdkPath = Marshal.AllocHGlobal(Marshal.SizeOf(sdkPathStruct));
                Marshal.StructureToPtr(sdkPathStruct, pSdkPath, false);
                HCNetSDK.NET_DVR_SetSDKInitCfg(HCNetSDK.NET_SDK_INIT_CFG_SDK_PATH, pSdkPath);
                Marshal.FreeHGlobal(pSdkPath);

                // Initialize the SDK.
                bool initOk = HCNetSDK.NET_DVR_Init();
                if (!initOk)
                {
                    uint err = HCNetSDK.NET_DVR_GetLastError();
                    throw new Exception($"NET_DVR_Init failed. Error code: {err}");
                }

                Console.WriteLine("[HikvisionSDK] SDK initialized.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HikvisionSDK] InitializeSDK error: {ex.Message}");
                return false;
            }
        }

        // ---------------------------------------------------------------
        //  STEP 2: Connect and start listening for face scan events
        //
        //  Call this when AttendanceControl loads.
        //
        //  What it does:
        //    a. Logs into the device (gets a userID).
        //    b. Registers our AlarmCallback function with the SDK.
        //    c. Opens an alarm channel — device starts sending events.
        // ---------------------------------------------------------------
        public static bool StartListening()
        {
            try
            {
                // ----- a. Log into the device -----
                var loginInfo = new HCNetSDK.NET_DVR_USER_LOGIN_INFO
                {
                    sDeviceAddress = DeviceIP,
                    wPort          = DevicePort,
                    sUserName      = DeviceUser,
                    sPassword      = DevicePass,
                    bUseAsynLogin  = false,         // synchronous — wait for result
                    iLoginMode     = 0,             // 0 = ISAPI protocol
                    byRes3         = new byte[120]
                };

                var deviceInfo = new HCNetSDK.NET_DVR_DEVICEINFO_V40();

                _userID = HCNetSDK.NET_DVR_Login_V40(ref loginInfo, ref deviceInfo);

                if (_userID < 0)
                {
                    uint err = HCNetSDK.NET_DVR_GetLastError();
                    throw new Exception($"Login failed. Error code: {err}. " +
                        "Check IP, port (8000), username and password.");
                }

                Console.WriteLine($"[HikvisionSDK] Logged in. UserID: {_userID}");

                // ----- b. Register the alarm callback -----
                // We create the delegate and store it in _callbackRef so it
                // won't be garbage collected while the SDK is using it.
                _callbackRef = new HCNetSDK.MSGCallBack(AlarmCallback);
                HCNetSDK.NET_DVR_SetDVRMessageCallBack_V50(0, _callbackRef, IntPtr.Zero);

                // ----- c. Open the alarm channel -----
                // This tells the device "start sending me events now".
                _alarmHandle = HCNetSDK.NET_DVR_SetupAlarmChan_V41(_userID);

                if (_alarmHandle < 0)
                {
                    uint err = HCNetSDK.NET_DVR_GetLastError();
                    throw new Exception($"SetupAlarmChan failed. Error code: {err}");
                }

                Console.WriteLine("[HikvisionSDK] Listening for face scan events...");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HikvisionSDK] StartListening error: {ex.Message}");
                return false;
            }
        }

        // ---------------------------------------------------------------
        //  STEP 3: Stop listening and log out
        //
        //  Call this when AttendanceControl is closed/unloaded.
        // ---------------------------------------------------------------
        public static void StopListening()
        {
            if (_alarmHandle >= 0)
            {
                HCNetSDK.NET_DVR_CloseAlarmChan_V30(_alarmHandle);
                _alarmHandle = -1;
            }

            if (_userID >= 0)
            {
                HCNetSDK.NET_DVR_Logout(_userID);
                _userID = -1;
            }

            Console.WriteLine("[HikvisionSDK] Stopped listening and logged out.");
        }

        // ---------------------------------------------------------------
        //  Clean up SDK on app exit — call in Program.cs on shutdown
        // ---------------------------------------------------------------
        public static void CleanupSDK()
        {
            StopListening();
            HCNetSDK.NET_DVR_Cleanup();
            Console.WriteLine("[HikvisionSDK] SDK cleaned up.");
        }

        // ---------------------------------------------------------------
        //  THE CALLBACK — fires the instant a face is scanned
        //
        //  The SDK calls this function on its own internal thread.
        //  We must use Invoke() to get back onto the UI thread for
        //  any UI updates (same as before with the HTTP listener).
        //
        //  lCommand tells us what type of event this is.
        //  We only process COMM_ALARM_ACS (= 0x4000) which covers
        //  all Access Control System events including face scans.
        // ---------------------------------------------------------------
        private static void AlarmCallback(
            int    lCommand,
            ref HCNetSDK.NET_DVR_ALARMER pAlarmer,
            IntPtr pAlarmInfo,
            uint   dwBufLen,
            IntPtr pUser)
        {
            try
            {
                // Only process Access Control alarms.
                if (lCommand != (int)HCNetSDK.COMM_ALARM_ACS) return;

                // Read the alarm info struct from the unmanaged memory pointer.
                var alarmInfo = (HCNetSDK.NET_DVR_ALARM_ISAPI_INFO)
                    Marshal.PtrToStructure(pAlarmInfo, typeof(HCNetSDK.NET_DVR_ALARM_ISAPI_INFO));

                if (alarmInfo.pAlarmData == IntPtr.Zero || alarmInfo.dwAlarmDataLen == 0)
                    return;

                // Read the raw alarm data bytes and convert to a string.
                byte[] dataBytes = new byte[alarmInfo.dwAlarmDataLen];
                Marshal.Copy(alarmInfo.pAlarmData, dataBytes, 0, (int)alarmInfo.dwAlarmDataLen);
                string alarmJson = Encoding.UTF8.GetString(dataBytes);

                Console.WriteLine($"[HikvisionSDK] Alarm received: {alarmJson}");

                // Parse and save the attendance record.
                ProcessAlarm(alarmJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HikvisionSDK] AlarmCallback error: {ex.Message}");
            }
        }

        // ---------------------------------------------------------------
        //  Parse the alarm JSON and save attendance to the database.
        //
        //  The device sends something like:
        //  {
        //    "AccessControllerEvent": {
        //      "majorEventType":    5,
        //      "subEventType":      75,
        //      "employeeNoString":  "2024-0001",
        //      "dateTime":          "2026-05-08T14:32:00+08:00",
        //      "name":              "Juan dela Cruz"
        //    }
        //  }
        // ---------------------------------------------------------------
        private static void ProcessAlarm(string alarmJson)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                dynamic parsed = serializer.DeserializeObject(alarmJson);

                dynamic evt = parsed["AccessControllerEvent"];
                if (evt == null) return;

                // majorEventType 5 = Access Control
                // subEventType  75 = Face recognition passed ✅
                // subEventType  76 = Face recognition failed ❌ (we skip these)
                int major = (int)(evt["majorEventType"] ?? 0);
                int minor = (int)(evt["subEventType"]   ?? 0);

                if (major != 5 || minor != 75)
                {
                    Console.WriteLine($"[HikvisionSDK] Skipping event major={major} minor={minor}");
                    return;
                }

                string employeeNo = evt["employeeNoString"] as string;
                string dateTime   = evt["dateTime"]         as string;

                if (string.IsNullOrEmpty(employeeNo) || employeeNo == "0") return;
                if (string.IsNullOrEmpty(dateTime)) return;

                // Strip timezone offset — keep only "2026-05-08T14:32:00"
                if (dateTime.Length > 19)
                    dateTime = dateTime.Substring(0, 19);

                // Look up the student by IdNumber.
                var studentRepo = new StudentRepository();
                Student student = studentRepo.GetStudentByIdNumber(employeeNo);

                if (student == null)
                {
                    Console.WriteLine($"[HikvisionSDK] Unknown employeeNo: {employeeNo}");
                    return;
                }

                // Save the attendance record.
                var attendanceRepo = new AttendanceRepository();
                attendanceRepo.RecordScan(student.Id, dateTime);

                // Notify the UI.
                string message = $"{student.FullName} scanned at {dateTime.Substring(11, 8)}";
                Console.WriteLine($"[HikvisionSDK] Recorded: {message}");
                OnAttendanceRecorded?.Invoke(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HikvisionSDK] ProcessAlarm error: {ex.Message}");
                Console.WriteLine($"  Raw JSON: {alarmJson}");
            }
        }

        // ---------------------------------------------------------------
        //  REGISTER — Push a student's profile + face to the device
        //
        //  Uses NET_DVR_STDXMLConfig to send ISAPI requests through the
        //  SDK instead of raw HTTP — more reliable than HttpWebRequest.
        //
        //  Called from RegisterStudentsControl after saving to DB.
        // ---------------------------------------------------------------
        public static bool RegisterFaceOnDevice(Student student, Image faceImage)
        {
            // We need to be logged in to register faces.
            // Log in temporarily if not already connected.
            bool tempLogin = false;
            if (_userID < 0)
            {
                if (!StartListening()) return false;
                tempLogin = true;
            }

            try
            {
                // ----- Step A: Create user profile -----
                string createUrl  = $"PUT /ISAPI/AccessControl/UserInfo/SetUp?format=json";
                string createBody = $@"{{
                    ""UserInfo"": {{
                        ""employeeNo"": ""{student.IdNumber}"",
                        ""name"":       ""{student.FullName}"",
                        ""userType"":   ""normal"",
                        ""Valid"": {{
                            ""enable"":    true,
                            ""beginTime"": ""2000-01-01T00:00:00"",
                            ""endTime"":   ""2030-12-31T23:59:59""
                        }},
                        ""doorRight"": ""1"",
                        ""RightPlan"": [{{ ""doorNo"": 1, ""planTemplateNo"": ""1"" }}]
                    }}
                }}";

                string createResponse = SendISAPIRequest(createUrl, createBody);
                Console.WriteLine($"[HikvisionSDK] Create user response: {createResponse}");

                if (!createResponse.Contains("\"statusCode\":0") &&
                    !createResponse.Contains("\"statusCode\": 0"))
                {
                    throw new Exception($"Device rejected user creation: {createResponse}");
                }

                // ----- Step B: Upload face photo -----
                // Convert Image to JPEG bytes.
                byte[] photoBytes;
                using (var ms = new MemoryStream())
                {
                    faceImage.Save(ms, ImageFormat.Jpeg);
                    photoBytes = ms.ToArray();
                }

                // Build multipart body.
                string boundary  = "----SAMSBoundary";
                string faceJson  = $@"{{""FaceDataRecord"":{{""employeeNo"":""{student.IdNumber}"",""faceData"":""""}}}}";
                byte[] multipart = BuildMultipartBody(boundary, faceJson, photoBytes);

                string faceUrl      = $"POST /ISAPI/Intelligent/FDLib/FDSetUp?format=json";
                string faceResponse = SendISAPIRequestRaw(faceUrl, multipart,
                    $"multipart/form-data; boundary={boundary}");

                Console.WriteLine($"[HikvisionSDK] Face upload response: {faceResponse}");

                if (!faceResponse.Contains("\"statusCode\":0") &&
                    !faceResponse.Contains("\"statusCode\": 0"))
                {
                    throw new Exception($"Device rejected face photo: {faceResponse}");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HikvisionSDK] RegisterFaceOnDevice error: {ex.Message}");
                return false;
            }
            finally
            {
                // If we logged in temporarily just for registration, log out now.
                if (tempLogin) StopListening();
            }
        }

        // ---------------------------------------------------------------
        //  ISAPI HELPERS — send requests through the SDK
        // ---------------------------------------------------------------

        private static string SendISAPIRequest(string url, string jsonBody)
        {
            byte[] urlBytes  = Encoding.UTF8.GetBytes(url + "\0");
            byte[] bodyBytes = Encoding.UTF8.GetBytes(jsonBody);
            return SendISAPIRequestRaw(url, bodyBytes, "application/json");
        }

        private static string SendISAPIRequestRaw(string url, byte[] bodyBytes, string contentType)
        {
            byte[] urlBytes    = Encoding.UTF8.GetBytes(url + "\0");
            byte[] outputBuf   = new byte[1024 * 64];   // 64KB response buffer
            byte[] statusBuf   = new byte[1024];

            IntPtr pUrl    = Marshal.AllocHGlobal(urlBytes.Length);
            IntPtr pBody   = Marshal.AllocHGlobal(bodyBytes.Length);
            IntPtr pOutput = Marshal.AllocHGlobal(outputBuf.Length);
            IntPtr pStatus = Marshal.AllocHGlobal(statusBuf.Length);

            try
            {
                Marshal.Copy(urlBytes,  0, pUrl,    urlBytes.Length);
                Marshal.Copy(bodyBytes, 0, pBody,   bodyBytes.Length);

                var input = new HCNetSDK.NET_DVR_XML_CONFIG_INPUT
                {
                    dwSize          = (uint)Marshal.SizeOf(typeof(HCNetSDK.NET_DVR_XML_CONFIG_INPUT)),
                    lpRequestUrl    = pUrl,
                    dwRequestUrlLen = (uint)urlBytes.Length,
                    lpInBuffer      = pBody,
                    dwInBufferSize  = (uint)bodyBytes.Length,
                    dwRecvTimeOut   = 10000,   // 10 second timeout
                    byRes           = new byte[32]
                };

                var output = new HCNetSDK.NET_DVR_XML_CONFIG_OUTPUT
                {
                    dwSize          = (uint)Marshal.SizeOf(typeof(HCNetSDK.NET_DVR_XML_CONFIG_OUTPUT)),
                    lpOutBuffer     = pOutput,
                    dwOutBufferSize = (uint)outputBuf.Length,
                    lpStatusBuffer  = pStatus,
                    dwStatusSize    = (uint)statusBuf.Length,
                    byRes           = new byte[32]
                };

                bool ok = HCNetSDK.NET_DVR_STDXMLConfig(_userID, ref input, ref output);

                if (!ok)
                {
                    uint err = HCNetSDK.NET_DVR_GetLastError();
                    return $"{{\"error\": \"NET_DVR_STDXMLConfig failed, code {err}\"}}";
                }

                Marshal.Copy(pOutput, outputBuf, 0, (int)output.dwReturnedXMLSize);
                return Encoding.UTF8.GetString(outputBuf, 0, (int)output.dwReturnedXMLSize);
            }
            finally
            {
                Marshal.FreeHGlobal(pUrl);
                Marshal.FreeHGlobal(pBody);
                Marshal.FreeHGlobal(pOutput);
                Marshal.FreeHGlobal(pStatus);
            }
        }

        private static byte[] BuildMultipartBody(string boundary, string jsonPart, byte[] photoBytes)
        {
            using (var ms = new MemoryStream())
            {
                void Write(string s) { byte[] b = Encoding.UTF8.GetBytes(s); ms.Write(b, 0, b.Length); }

                Write($"--{boundary}\r\n");
                Write("Content-Disposition: form-data; name=\"FaceDataRecord\"\r\n");
                Write("Content-Type: application/json\r\n\r\n");
                Write(jsonPart);
                Write("\r\n");

                Write($"--{boundary}\r\n");
                Write("Content-Disposition: form-data; name=\"FaceImage\"; filename=\"face.jpg\"\r\n");
                Write("Content-Type: image/jpeg\r\n\r\n");
                ms.Write(photoBytes, 0, photoBytes.Length);
                Write("\r\n");

                Write($"--{boundary}--\r\n");
                return ms.ToArray();
            }
        }
    }
}
