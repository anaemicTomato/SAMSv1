using SAMSv1.Data;
using SAMSv1.Services;
using System;
using SAMSv1.Models;
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
        
        private CancellationTokenSource _cts;
        private Task _worker;
        private long _lastSerialNo;
        private string _attendanceType;
        private int _currentEventId;
        private string _session;
        private string _eventDescription;
        private string _semester;

        // Static flag — survives control disposal, visible across the whole app
        public static bool IsLiveRunning { get; private set; } = false;

        // ✅ FIX 1: ConcurrentDictionary instead of HashSet (thread-safe)
        private readonly ConcurrentDictionary<int, byte> _scanned =
            new ConcurrentDictionary<int, byte>();

        private readonly ConcurrentQueue<AttendanceLogRow> _queue =
            new ConcurrentQueue<AttendanceLogRow>();

        private BindingList<AttendanceLogRow> _bindingList;
        private BindingSource _bindingSource;
        private System.Windows.Forms.Timer _uiTimer;

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

        // ================= START =================
        private async void btnStartAttendance_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEventName?.Text))
            {
                Log("Enter event name");
                return;
            }

            if (cbSemester.SelectedItem == null)
            {
                Log("Select a semester first");
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
            _session = cbSession.SelectedItem?.ToString();
            _semester = cbSemester.SelectedItem?.ToString(); // ← add this
            _eventDescription = string.IsNullOrWhiteSpace(txtEventDescription?.Text)
                                ? null : txtEventDescription.Text.Trim();

            _scanned.Clear();
            _bindingList.Clear();

            btnStartAttendance.Enabled = false;
            Log("Connecting to device...");

            // ── CONNECT FIRST — event only created after successful connection ──
            bool connected = await Task.Run(() =>
            {
                try { return DeviceManager.Initialize(); }
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
                return; // ← event NOT created, DB stays clean
            }

            // ── CONNECTION SUCCESS — NOW create or reuse event ──
            try
            {
                if (_attendanceType == "Time-In")
                {
                    _currentEventId = FaceService.CreateEvent(eventName, date, time);
                }
                else
                {
                    _currentEventId = FaceService.GetOpenEventId(eventName, date);
                    if (_currentEventId <= 0)
                        _currentEventId = FaceService.CreateEvent(eventName, date, time);
                }
                Log($"Event ready — ID={_currentEventId} '{eventName}'");
            }
            catch (Exception ex)
            {
                Log("EVENT ERROR: " + ex.Message);
                DeviceManager.Disconnect();
                btnStartAttendance.Enabled = true;
                return;
            }

            // ── BASELINE ──────────────────────────────────────────
            Log("Setting baseline — please wait...");

            var deviceRef = DeviceManager.Device;
            if (deviceRef == null)
            {
                Log("Device lost after connect.");
                btnStartAttendance.Enabled = true;
                return;
            }

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

            StartWorker();

            btnStartAttendance.Visible = false;
            btnStopAttendance.Visible = true;
            IsLiveRunning = true;

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
            var device = DeviceManager.Device;
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
                        _currentEventId,
                        _session,
                        _eventDescription,
                        _semester          // ← add this
                    );
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[SaveAttendance] {ex}");
                    _scanned.TryRemove(id, out _); // rollback so it can retry
                    continue;
                }

                _queue.Enqueue(new AttendanceLogRow
                {
                    ScanTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
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

            DeviceManager.Disconnect();

            // ── Delete event if it has no attendance records ──
            try
            {
                if (_currentEventId > 0)
                {
                    int count = FaceService.GetAttendanceCountForEvent(_currentEventId);
                    if (count == 0)
                    {
                        FaceService.DeleteEvent(_currentEventId);
                        Log($"Event ID={_currentEventId} deleted — no attendance recorded.");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[DeleteOrphanEvent] {ex}");
            }

            Interlocked.Exchange(ref _lastSerialNo, 0);
            _scanned.Clear();
            _currentEventId = -1;
            IsLiveRunning = false;

            btnStopAttendance.Visible = false;
            btnStartAttendance.Visible = true;
            btnStartAttendance.Enabled = true;

            Log("STOPPED");
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

// ================= DESTROY =================
        protected override void OnHandleDestroyed(EventArgs e)
        {
            IsLiveRunning = false;
            _uiTimer?.Stop();
            _uiTimer?.Dispose();
            _cts?.Cancel();
            base.OnHandleDestroyed(e);
        }

        private void StudentAttendanceControl_Load_1(object sender, EventArgs e)
        {
            btnStopAttendance.Visible = false;
            btnStartAttendance.Visible = true;

            UpdateCounters();
            Log("System Ready");
        }

       
        public async Task StopAsync()
        {
            if (!IsLiveRunning) return;

            _uiTimer?.Stop();
            _cts?.Cancel();

            if (_worker != null)
                await _worker.ContinueWith(_ => { });

            IsLiveRunning = false;
        }

    }
}