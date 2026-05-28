using DevExpress.Xpo;
using DevExpress.XtraEditors;
using SAMSv1.CtrlForms;
using SAMSv1.Data;
using SAMSv1.Services;
using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace SAMSv1
{
    public partial class AdminForm : DevExpress.XtraEditors.XtraForm
    {
        public AdminForm()
        {
            InitializeComponent();
            FaceService.Init(DBHelper.ConnectionString);
            LoadFormDefaults();
        }

        // ── STARTUP DEFAULTS ─────────────────────────────────────
        private void LoadFormDefaults()
        {
            // Course options
            cbCOURSE.Properties.Items.AddRange(new[]
            { "BSCS", "BSIT", "BSIS", "BSED", "BEED", "BSBA", "BSA", "BSCRIM", "BSN", "BSECE" });

            // Year level options
            cbYEARLEVEL.Properties.Items.AddRange(new[]
            { "1st Year", "2nd Year", "3rd Year", "4th Year" });

            // Attendance type options
            cbATTENDANCETYPE.Properties.Items.AddRange(new[]
            { "Time-In", "Time-Out" });

            // Session options
            cbSESSION.Properties.Items.AddRange(new[]
            { "Morning", "Afternoon", "Evening" });

            LoadStudentCombo();
            LoadEventCombo();
        }

        // ── LOAD COMBOS ──────────────────────────────────────────
        private void LoadStudentCombo()
        {
            cbSTUDENTID.Properties.Items.Clear();
            var students = FaceService.GetAllStudents();
            foreach (var s in students)
                cbSTUDENTID.Properties.Items.Add(new StudentItem(s.StudentID, s.FullName));
        }

        private void LoadEventCombo()
        {
            cbEVENTID.Properties.Items.Clear();
            using (var conn = DBHelper.GetConnection())
            {
                var cmd = new SQLiteCommand(
                    "SELECT EventID, EventName FROM EventsTable ORDER BY EventID DESC", conn);
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        cbEVENTID.Properties.Items.Add(
                            new EventItem(r.GetInt32(0), r.GetString(1)));
            }
        }

        // ── ADD STUDENT ──────────────────────────────────────────
        private void btnADDSTUDENT_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSTUDENTNAME.Text) ||
                string.IsNullOrWhiteSpace(txtIDNUMBER.Text) ||
                cbCOURSE.SelectedItem == null ||
                cbYEARLEVEL.SelectedItem == null)
            {
                XtraMessageBox.Show("Fill in all student fields.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                FaceService.SaveStudentFull(
                    idNumber: txtIDNUMBER.Text.Trim(),
                    fullName: txtSTUDENTNAME.Text.Trim(),
                    course: cbCOURSE.SelectedItem.ToString(),
                    yearLevel: cbYEARLEVEL.SelectedItem.ToString()
                );

                XtraMessageBox.Show("Student added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtSTUDENTNAME.Clear();
                txtIDNUMBER.Clear();
                cbCOURSE.SelectedIndex = -1;
                cbYEARLEVEL.SelectedIndex = -1;

                LoadStudentCombo(); // refresh attendance combo
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error adding student:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── ADD EVENT ────────────────────────────────────────────
        private void btnADDEVENT_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEVENTNAME.Text))
            {
                XtraMessageBox.Show("Enter an event name.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string today = DateTime.Now.ToString("yyyy-MM-dd");
                string startTime = DateTime.Now.ToString("HH:mm:ss");

                int eventId = FaceService.CreateEvent(
                    eventName: txtEVENTNAME.Text.Trim(),
                    eventDate: today,
                    startTime: startTime
                );

                // Auto-close with +5 seconds as end time
                string endTime = DateTime.Now.AddSeconds(5).ToString("HH:mm:ss");
                FaceService.CloseEvent(eventId, endTime);

                XtraMessageBox.Show(
                    $"Event added!\nID: {eventId}\nDate: {today}\nStart: {startTime}\nEnd: {endTime}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtEVENTNAME.Clear();
                LoadEventCombo(); // refresh attendance combo
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error adding event:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── ADD ATTENDANCE ───────────────────────────────────────
        private void btnADDATTENDANCE_Click(object sender, EventArgs e)
        {
            if (cbSTUDENTID.SelectedItem == null ||
                cbEVENTID.SelectedItem == null ||
                cbATTENDANCETYPE.SelectedItem == null)
            {
                XtraMessageBox.Show("Fill in Student, Event, and Attendance Type.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var studentItem = (StudentItem)cbSTUDENTID.SelectedItem;
                var eventItem = (EventItem)cbEVENTID.SelectedItem;
                string type = cbATTENDANCETYPE.SelectedItem.ToString();
                string now = DateTime.Now.ToString("HH:mm:ss");
                string today = DateTime.Now.ToString("yyyy-MM-dd");
                string session = cbSESSION.SelectedItem?.ToString() ?? null;
                string desc = string.IsNullOrWhiteSpace(txtEVENTDESC.Text)
                                    ? null : txtEVENTDESC.Text.Trim();

                FaceService.SaveAttendance(
                    studentId: studentItem.Id,
                    date: today,
                    time: now,
                    attendanceType: type,
                    eventId: eventItem.Id,
                    session: session,
                    eventDescription: desc,
                    semester: "1st"
                );

                XtraMessageBox.Show("Attendance record saved!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                cbSTUDENTID.SelectedIndex = -1;
                cbEVENTID.SelectedIndex = -1;
                cbATTENDANCETYPE.SelectedIndex = -1;
                cbSESSION.SelectedIndex = -1;
                txtEVENTDESC.Clear();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error adding attendance:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── LOAD CONTROLS INTO PANEL ─────────────────────────────
        private void LoadControl(XtraUserControl control)
        {
        }

        // ── NAV BUTTONS ──────────────────────────────────────────
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            LoadControl(new AttendanceControl());
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            LoadControl(new RegisterStudentsV2());
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            LoadControl(new ReportControl());
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
        }

        // ── PAINT HANDLERS ───────────────────────────────────────
        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tablePanel1_Paint(object sender, PaintEventArgs e)
        {
            using (var brush = new SolidBrush(Color.FromArgb(10, 255, 255, 255)))
                e.Graphics.FillRectangle(brush, tablePanel1.ClientRectangle);
        }

        private void ReportControl_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            tablePanel1.BackColor = Color.Transparent;
        }

        // ── INNER COMBO ITEM CLASSES ─────────────────────────────
        private class StudentItem
        {
            public int Id { get; }
            public string Name { get; }
            public StudentItem(int id, string name) { Id = id; Name = name; }
            public override string ToString() => Name;
        }

        private class EventItem
        {
            public int Id { get; }
            public string Name { get; }
            public EventItem(int id, string name) { Id = id; Name = name; }
            public override string ToString() => Name;
        }
    }
}