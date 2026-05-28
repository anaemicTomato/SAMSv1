using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using SAMSv1.Models;
using SAMSv1.Services;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SAMSv1.CtrlForms
{
    public partial class AttendanceLogControl : DevExpress.XtraEditors.XtraUserControl
    {
        private BindingList<Student> _bindingList;
        private BindingSource _bindingSource;

        public AttendanceLogControl()
        {
            InitializeComponent();
        }

        private void AttendanceLogControl_Load_1(object sender, EventArgs e)
        {
            InitGrid();
            LoadStudents();
            ResetCounters();
        }

        // ── GRID INIT ────────────────────────────────────────────
        private void InitGrid()
        {
            _bindingList = new BindingList<Student>();
            _bindingSource = new BindingSource { DataSource = _bindingList };
            gcAttendanceLogs.DataSource = _bindingSource;

            // Map your existing grid columns to Student fields
            gridColumnID.FieldName = nameof(Student.IdNumber);
            gridColumnName.FieldName = nameof(Student.FullName);
            gridColumnCourse.FieldName = nameof(Student.Course);
            gridColumnYearLevel.FieldName = nameof(Student.YearLevel);

            // Hide attendance-specific columns that no longer apply
            gridColumnDate.Visible = false;
            gridColumnTimeIn.Visible = false;
            gridColumnTimeOut.Visible = false;
            gridColumnStatus.Visible = false;

            // Wire row click
            gridView2.FocusedRowChanged += GvAttendanceLogs_FocusedRowChanged;
        }

        // ── LOAD ALL STUDENTS ────────────────────────────────────
        private void LoadStudents()
        {
            try
            {
                _bindingList.Clear();
                var students = FaceService.GetAllStudents();
                foreach (var s in students)
                    _bindingList.Add(s);

                _bindingSource.ResetBindings(false);
                gcAttendanceLogs.RefreshDataSource();
                ResetCounters();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[LoadStudents] {ex}");
            }
        }

        // ── ROW CLICK → UPDATE LABELS ────────────────────────────
        private void GvAttendanceLogs_FocusedRowChanged(
            object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                var student = gridView2.GetFocusedRow() as Student;
                if (student == null) { ResetCounters(); return; }

                var (totalAttendance, totalPresent, totalAbsent) =
                    FaceService.GetStudentSummary(student.StudentID);

                labelTotalAttendance.Text = totalAttendance.ToString("D2");
                labelTotalPresent.Text = totalPresent.ToString("D2");
                labelTotalAbsent.Text = totalAbsent.ToString("D2");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[RowClick] {ex}");
            }
        }

        // ── RESET COUNTERS ───────────────────────────────────────
        private void ResetCounters()
        {
            labelTotalAttendance.Text = "00";
            labelTotalPresent.Text = "00";
            labelTotalAbsent.Text = "00";
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            var report = new Reports.StudentRPT();
            report.ShowPreviewDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxYears_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxCourse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void panelControl4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureEdit3_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}