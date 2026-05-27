using SAMSv1.Data;
using SAMSv1.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAMSv1.CtrlForms
{
    public partial class StudentAttendanceControl : DevExpress.XtraEditors.XtraUserControl
    {
        private const string DEVICE_IP = "192.168.1.65";
        private const int DEVICE_PORT = 8000;
        private const string DEVICE_USER = "admin";
        private const string DEVICE_PASS = "DMC2026#";

        private AttendanceDevice _device;

        private CancellationTokenSource _cts;
        private Task _worker;

        private long _lastSerialNo;
        private string _attendanceType;
        private int _currentEventId;

        // ✅ FIX 1: ConcurrentDictionary instead of HashSet (thread-safe)
        private readonly ConcurrentDictionary<int, byte> _scanned =
            new ConcurrentDictionary<int, byte>();

        private readonly ConcurrentQueue<AttendanceLogRow> _queue =
            new ConcurrentQueue<AttendanceLogRow>();

        private BindingList<AttendanceLogRow> _bindingList;
        private BindingSource _bindingSource;
        private System.Windows.Forms.Timer _uiTimer;

        public class AttendanceLogRow
        {
            public string Time { get; set; }
            public string IdNumber { get; set; }
            public string FullName { get; set; }
            public string Status { get; set; }
            public string AttendanceType { get; set; }
        }

        // ================= CONSTRUCTOR =================
        public StudentAttendanceControl()
        {
            InitializeComponent();
            InitializeSafeState();
        }

        // ================= SAFE INIT =================
        private void InitializeSafeState()
        {
            _bindingList = new BindingList<AttendanceLogRow>();
            _bindingSource = new BindingSource { DataSource = _bindingList };

            if (gcLiveAttendanceLog != null)
                gcLiveAttendanceLog.DataSource = _bindingSource;

            // ✅ FIX 4: UI timer drains queue on UI thread — no blocking Invoke
            _uiTimer = new System.Windows.Forms.Timer();
            _uiTimer.Interval = 500;
            _uiTimer.Tick += (s, e) => DrainQueueToUI();
        }

        // ================= LOAD =================
        private void StudentAttendanceControl_Load(object sender, EventArgs e)
        {
           
        }

        // ================= START =================
        private async void btnStartAttendance_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEventName?.Text))
            {
                Log("Enter event name");
                return;
            }

            if (cbAttendanceType?.SelectedItem == null)
            {
                Log("Select attendance type first");
                return;
            }

            _attendanceType = cbAttendanceType.SelectedItem.ToString();

            string eventName = txtEventName.Text.Trim();
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm:ss");

            try
            {
                _currentEventId =
                    _attendanceType == "Time-In"
                    ? FaceService.CreateEvent(eventName, date, time)
                    : FaceService.GetOpenEventId(eventName, date);
            }
            catch (Exception ex)
            {
                Log("EVENT ERROR: " + ex.Message);
                return;
            }

            _scanned.Clear();
            _bindingList.Clear();

            btnStartAttendance.Enabled = false;
            Log("Connecting to device...");

            // ── CONNECT ───────────────────────────────────────────
            bool connected = await Task.Run(() =>
            {
                try
                {
                    _device?.Disconnect();
                    _device = new HikvisionDevice(DEVICE_IP, DEVICE_PORT, DEVICE_USER, DEVICE_PASS);
                    return _device.Connect();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[Connect] {ex}");
                    return false;
                }
            });

            if (!connected)
            {
                Log("Device connection failed");
                btnStartAttendance.Enabled = true;
                return;
            }

            // ── BASELINE ──────────────────────────────────────────
            Log("Setting baseline — please wait...");

            var deviceRef = _device;
            if (deviceRef == null)
            {
                Log("Device lost after connect.");
                btnStartAttendance.Enabled = true;
                return;
            }

            // ✅ FIX 2: Interlocked write for thread-safe long
            long baseline = await Task.Run(() =>
            {
                try
                {
                    long max = 0;
                    var events = deviceRef.PollNewEvents(0);
                    if (events == null) return 0L;

                    foreach (var evt in events)
                    {
                        if (evt == null) continue;
                        if (evt.SerialNo > max) max = evt.SerialNo;
                    }
                    return max;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[Baseline] {ex}");
                    return 0L;
                }
            });

            Interlocked.Exchange(ref _lastSerialNo, baseline);
            System.Diagnostics.Debug.WriteLine($"[Baseline] _lastSerialNo = {_lastSerialNo}");
            // ─────────────────────────────────────────────────────

            StartWorker();

            btnStartAttendance.Visible = false;
            btnStopAttendance.Visible = true;

            Log("STARTED [" + _attendanceType + "]");
        }

        // ================= WORKER LOOP =================
        private void StartWorker()
        {
            _cts = new CancellationTokenSource();
            _worker = Task.Run(async () =>
            {
                while (!_cts.IsCancellationRequested)
                {
                    try { PollDevice(); }
                    catch (Exception ex)
                    { System.Diagnostics.Debug.WriteLine($"[Worker] {ex}"); }

                    await Task.Delay(500);
                }
            });

            _uiTimer.Start();
        }

        // ================= POLL =================
        private void PollDevice()
        {
            var device = _device;
            if (device == null) return;

            // ✅ FIX 2: Interlocked read for thread-safe long
            long serial = Interlocked.Read(ref _lastSerialNo);
            var events = device.PollNewEvents(serial);
            if (events == null) return;

            foreach (var evt in events)
            {
                if (evt == null || string.IsNullOrWhiteSpace(evt.IdNumber))
                    continue;

                if (evt.SerialNo > serial)
                {
                    serial = evt.SerialNo;
                    Interlocked.Exchange(ref _lastSerialNo, serial);
                }

                var student = FaceService.GetStudentByIdNumber(evt.IdNumber);
                if (student.Equals(default((int, string)))) continue;

                var (id, name) = student;

                // ✅ FIX 1: TryAdd is atomic — no race condition
                if (!_scanned.TryAdd(id, 0)) continue;

                try
                {
                    FaceService.SaveAttendance(
                        id,
                        DateTime.Now.ToString("yyyy-MM-dd"),
                        DateTime.Now.ToString("HH:mm:ss"),
                        _attendanceType,
                        _currentEventId);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[SaveAttendance] {ex}");
                    _scanned.TryRemove(id, out _); // rollback so it can retry
                    continue;
                }

                _queue.Enqueue(new AttendanceLogRow
                {
                    Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    IdNumber = evt.IdNumber,
                    FullName = name,
                    Status = _attendanceType == "Time-Out" ? "Complete" : "Present",
                    AttendanceType = _attendanceType
                });

                System.Diagnostics.Debug.WriteLine($"[Poll] Scanned: {name}");
            }
        }

        // ================= UI DRAIN =================
        // ✅ FIX 4: Runs on UI thread via timer — no blocking Invoke
        private void DrainQueueToUI()
        {
            bool any = false;

            while (_queue.TryDequeue(out var row))
            {
                _bindingList.Add(row);
                any = true;
            }

            if (!any) return;

            _bindingSource.ResetBindings(false);
            gcLiveAttendanceLog.RefreshDataSource();
            UpdateCounters();
        }

        // ================= STOP =================
        // ✅ FIX 3: Waits for worker to finish before allowing restart
        private async void btnStopAttendance_Click(object sender, EventArgs e)
        {
            btnStopAttendance.Enabled = false;
            _uiTimer.Stop();
            _cts?.Cancel();

            if (_worker != null)
                await _worker.ContinueWith(_ => { });

            _device?.Disconnect();
            _device = null;

            Interlocked.Exchange(ref _lastSerialNo, 0);
            _scanned.Clear();

            btnStopAttendance.Visible = false;
            btnStartAttendance.Visible = true;
            btnStartAttendance.Enabled = true;

            Log("STOPPED");
        }

        // ================= DESTROY =================
        protected override void OnHandleDestroyed(EventArgs e)
        {
            _uiTimer?.Stop();
            _uiTimer?.Dispose();
            _cts?.Cancel();
            _device?.Disconnect();
            _device = null;
            base.OnHandleDestroyed(e);
        }

        // ================= COUNTERS =================
        private void UpdateCounters()
        {
            try
            {
                int total = FaceService.GetTotalStudents();
                int present = _currentEventId > 0 ? FaceService.GetPresentCount(_currentEventId) : 0;
                int incomplete = _currentEventId > 0 ? FaceService.GetIncompleteCount(_currentEventId) : 0;
                int absent = Math.Max(0, total - present - incomplete);

                labelTotal.Text = total.ToString("D2");
                labelPresent.Text = present.ToString("D2");
                labelAbsent.Text = absent.ToString("D2");
                labelIncomplete.Text = incomplete.ToString("D2");
            }
            catch (Exception ex)
            {
                // See what's actually failing
                System.Diagnostics.Debug.WriteLine($"[Counters] {ex}");
                Log("COUNTER ERROR: " + ex.Message);
            }
        }

        // ================= LOG =================
        private void Log(string msg)
        {
            string full = $"[{DateTime.Now:HH:mm:ss}] {msg}";
            System.Diagnostics.Debug.WriteLine(full);

            SafeInvoke(() =>
            {
                if (txtForDebug != null) txtForDebug.Text = full + "\r\n" + txtForDebug.Text;
                if (labelForDebug != null) labelForDebug.Text = full;
                if (textTemporaryDisplay != null) textTemporaryDisplay.Text = full;
            });
        }


        // ================= SAFE INVOKE =================
        private void SafeInvoke(Action action)
        {
            if (IsDisposed || !IsHandleCreated) return;
            try { Invoke(action); }
            catch { }
        }

        private void StudentAttendanceControl_Load_1(object sender, EventArgs e)
        {
            btnStopAttendance.Visible = false;
            btnStartAttendance.Visible = true;

            UpdateCounters();
            Log("System Ready");
        }
    }
}