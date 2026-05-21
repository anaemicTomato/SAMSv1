using SAMSv1.Data;
using SAMSv1.Services;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAMSv1.CtrlForms
{
    public partial class AttendanceControl : DevExpress.XtraEditors.XtraUserControl
    {
        private const string DEVICE_IP = "192.168.1.65";
        private const int DEVICE_PORT = 8000;
        private const string DEVICE_USER = "admin";
        private const string DEVICE_PASS = "DMC2026#";

        private AttendanceDevice _device;
        private System.Windows.Forms.Timer _liveTimer;
        private long _lastSerialNo = 0;

        public class AttendanceLogRow
        {
            public string Time { get; set; }
            public string IdNumber { get; set; }
            public string FullName { get; set; }
            public string Status { get; set; }
        }

        private BindingList<AttendanceLogRow> _logRows;

        public AttendanceControl()
        {
            InitializeComponent();
        }

        // ── Load: only set up the grid — NO device connection here ──
        private void AttendanceControl_Load(object sender, EventArgs e)
        {
            _logRows = new BindingList<AttendanceLogRow>();
            gcLiveAttendanceLog.DataSource = _logRows;

            btnStopAttendance.Visible = false;
            btnStartAttendance.Visible = true;

            Log("Click '▶ Start Attendance' to begin.");
        }

        // ── Start: connect + baseline on background thread ──
        private async void btnStartAttendance_Click(object sender, EventArgs e)
        {
            btnStartAttendance.Enabled = false;
            Log("⏳ Connecting to device — please wait...");

            try
            {
                // ── Run blocking work off the UI thread ──
                bool connected = await Task.Run(() =>
                {
                    _device = new HikvisionDevice(
                        DEVICE_IP, DEVICE_PORT, DEVICE_USER, DEVICE_PASS);
                    return _device.Connect();
                });

                if (!connected)
                {
                    Log("⚠ Device connection failed — check IP/credentials.");
                    btnStartAttendance.Enabled = true;
                    return;
                }

                Log("✓ Connected. Setting baseline — please wait...");

                // ── Grab baseline serial off UI thread too ──
                _lastSerialNo = await Task.Run(() =>
                {
                    long maxSerial = 0;
                    var baseline = _device.PollNewEvents(0);
                    foreach (var evt in baseline)
                        if (evt.SerialNo > maxSerial)
                            maxSerial = evt.SerialNo;
                    return maxSerial;
                });

                System.Diagnostics.Debug.WriteLine(
                    $"[Attendance] Baseline set — _lastSerialNo={_lastSerialNo}");

                // ── Now start the timer on the UI thread ──
                _liveTimer = new System.Windows.Forms.Timer();
                _liveTimer.Interval = 5000;
                _liveTimer.Tick += (s, ev) =>
                    System.Threading.ThreadPool.QueueUserWorkItem(_ => Poll());
                _liveTimer.Start();

                btnStartAttendance.Visible = false;
                btnStopAttendance.Visible = true;
                Log("✓ Live polling STARTED — ready to scan.");
            }
            catch (Exception ex)
            {
                Log("Start error: " + ex.Message);
                btnStartAttendance.Enabled = true;
            }
        }

        private void btnStopAttendance_Click(object sender, EventArgs e)
        {
            StopLive();
            btnStopAttendance.Visible = false;
            btnStartAttendance.Visible = true;
            btnStartAttendance.Enabled = true;
            Log("⏹ Attendance stopped.");
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            StopLive();
            base.OnHandleDestroyed(e);
        }

        private void StopLive()
        {
            _liveTimer?.Stop();
            _liveTimer?.Dispose();
            _liveTimer = null;
        }

        private void Poll()
        {
            if (IsDisposed || !IsHandleCreated) return;

            try
            {
                var events = _device.PollNewEvents(_lastSerialNo);

                foreach (var evt in events)
                {
                    if (evt.SerialNo > _lastSerialNo)
                        _lastSerialNo = evt.SerialNo;

                    System.Diagnostics.Debug.WriteLine(
                        $"[Poll] Raw event — IdNumber='{evt.IdNumber}' " +
                        $"Date={evt.Date} TimeIn={evt.TimeIn} Status={evt.Status}");

                    var (studentId, fullName) =
                        FaceService.GetStudentByIdNumber(evt.IdNumber);

                    System.Diagnostics.Debug.WriteLine(
                        $"[Poll] Lookup — StudentID={studentId} FullName='{fullName}'");

                    if (studentId < 0)
                    {
                        SafeInvoke(() =>
                            Log($"⚠ Unknown ID: {evt.IdNumber} — not registered."));
                        continue;
                    }

                    try
                    {
                        FaceService.SaveAttendance(
                            studentId, evt.Date, evt.TimeIn, evt.Status);

                        SafeInvoke(() =>
                        {
                            _logRows.Add(new AttendanceLogRow
                            {
                                Time = $"{evt.Date} {evt.TimeIn}",
                                IdNumber = evt.IdNumber,
                                FullName = fullName,
                                Status = evt.Status
                            });
                            Log($"[{evt.Date} {evt.TimeIn}]  ✓ {fullName}" +
                                $"  (ID: {evt.IdNumber})  [{evt.Status}]");
                        });
                    }
                    catch (Exception saveEx)
                    {
                        SafeInvoke(() =>
                            Log($"⚠ Save failed for {fullName}: {saveEx.Message}"));
                    }
                }
            }
            catch (Exception ex)
            {
                SafeInvoke(() => Log($"Poll error: {ex.Message}"));
            }
        }

        private void SafeInvoke(Action action)
        {
            if (IsDisposed || !IsHandleCreated) return;
            try { Invoke(action); }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
        }

        private void Log(string message)
        {
            System.Diagnostics.Debug.WriteLine($"[Attendance] {message}");
            SafeInvoke(() => textEdit1.Text = message);
        }
    }
}