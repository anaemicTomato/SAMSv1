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
        private BindingList<Student> _studentList;
        private BindingList<AttendanceLogRow> _attendanceList;
        private BindingSource _bindingSource;

        // Tracks which mode the grid is in
        private bool _showingAttendance = false;
        private Student _selectedStudent = null;
        private readonly FaceService _faceService = new FaceService();

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

        // ═════════════════════════════════════════════════════════
        // GRID INIT — starts in Student mode
        // ═════════════════════════════════════════════════════════
        private void InitGrid()
        {
            _studentList = new BindingList<Student>();
            _attendanceList = new BindingList<AttendanceLogRow>();
            _bindingSource = new BindingSource { DataSource = _studentList };

            gcAttendanceLogs.DataSource = _bindingSource;

            ShowStudentColumns();

            gridView2.FocusedRowChanged += GvAttendanceLogs_FocusedRowChanged;
        }

        // ═════════════════════════════════════════════════════════
        // COLUMN MODES
        // ═════════════════════════════════════════════════════════
        private void ShowStudentColumns()
        {
            gridColumnID.FieldName = "IdNumber";
            gridColumnName.FieldName = "FullName";
            gridColumnCourse.FieldName = "Course";
            gridColumnYearLevel.FieldName = "YearLevel";

            gridColumnID.Caption = "ID Number";
            gridColumnName.Caption = "Full Name";
            gridColumnCourse.Caption = "Course";
            gridColumnYearLevel.Caption = "Year Level";

            gridColumnID.Visible = true;
            gridColumnName.Visible = true;
            gridColumnCourse.Visible = true;
            gridColumnYearLevel.Visible = true;
            gridColumnDate.Visible = false;
            gridColumnTimeIn.Visible = false;
            gridColumnTimeOut.Visible = false;
            gridColumnStatus.Visible = false;
        }

        private void ShowAttendanceColumns()
        {
            gridColumnID.FieldName = "IdNumber";
            gridColumnName.FieldName = "FullName";
            gridColumnCourse.FieldName = "EventName";      // ← key fix
            gridColumnYearLevel.FieldName = "AttendanceType"; // ← key fix
            gridColumnDate.FieldName = "Date";
            gridColumnTimeIn.FieldName = "TimeIn";
            gridColumnTimeOut.FieldName = "TimeOut";
            gridColumnStatus.FieldName = "Status";

            gridColumnID.Caption = "ID Number";
            gridColumnName.Caption = "Full Name";
            gridColumnCourse.Caption = "Event";
            gridColumnYearLevel.Caption = "Type";
            gridColumnDate.Caption = "Date";
            gridColumnTimeIn.Caption = "Time In";
            gridColumnTimeOut.Caption = "Time Out";
            gridColumnStatus.Caption = "Status";

            gridColumnID.Visible = true;
            gridColumnName.Visible = true;
            gridColumnCourse.Visible = true;
            gridColumnYearLevel.Visible = true;
            gridColumnDate.Visible = true;
            gridColumnTimeIn.Visible = true;
            gridColumnTimeOut.Visible = true;
            gridColumnStatus.Visible = true;
        }

        // ═════════════════════════════════════════════════════════
        // LOAD ALL STUDENTS
        // ═════════════════════════════════════════════════════════
        private void LoadStudents()
        {
            try
            {
                _showingAttendance = false;
                _selectedStudent = null;

                _studentList.Clear();
                var students = _faceService.GetAllStudents();
                foreach (var s in students)
                    _studentList.Add(s);

                ShowStudentColumns();
                _bindingSource.DataSource = _studentList;
                _bindingSource.ResetBindings(false);
                gcAttendanceLogs.RefreshDataSource();
                ResetCounters();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[LoadStudents] {ex}");
            }
        }

        // ═════════════════════════════════════════════════════════
        // DOUBLE CLICK — show attendance of selected student
        // ═════════════════════════════════════════════════════════
        private void gcAttendanceLogs_DoubleClick(object sender, EventArgs e)
        {
            btnBack.Visible = true;

            if (_showingAttendance)
            {
                // Already showing attendance — double click goes back to student list
                LoadStudents();
                return;
            }

            var student = gridView2.GetFocusedRow() as Student;
            if (student == null) return;

            _selectedStudent = student;
            LoadAttendanceForStudent(student);
        }

        private void LoadAttendanceForStudent(Student student)
        {
            try
            {
                _showingAttendance = true;

                var records = _faceService.GetAttendanceByStudent(student.StudentID);

                _attendanceList.Clear();
                foreach (var r in records)
                    _attendanceList.Add(r);

                ShowAttendanceColumns();
                _bindingSource.DataSource = _attendanceList;
                _bindingSource.ResetBindings(false);
                gcAttendanceLogs.RefreshDataSource();

                var (totalAttendance, totalPresent, totalAbsent) =
                    _faceService.GetStudentSummary(student.StudentID);

                labelTotalAttendance.Text = totalAttendance.ToString("D2"); // total events
                labelTotalPresent.Text = totalPresent.ToString("D2");    // present slots filled
                labelTotalAbsent.Text = totalAbsent.ToString("D2");     // events with no record
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[LoadAttendance] {ex}");
            }
        }

        // ═════════════════════════════════════════════════════════
        // ROW CLICK — update counters when browsing student list
        // ═════════════════════════════════════════════════════════
        private void GvAttendanceLogs_FocusedRowChanged(
            object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // Only update counters when in student mode
            if (_showingAttendance) return;

            try
            {
                var student = gridView2.GetFocusedRow() as Student;
                if (student == null) { ResetCounters(); return; }

                var (totalAttendance, totalPresent, totalAbsent) =
                    _faceService.GetStudentSummary(student.StudentID);

                labelTotalAttendance.Text = totalAttendance.ToString("D2");
                labelTotalPresent.Text = totalPresent.ToString("D2");
                labelTotalAbsent.Text = totalAbsent.ToString("D2");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[RowClick] {ex}");
            }
        }

        // ═════════════════════════════════════════════════════════
        // RESET COUNTERS
        // ═════════════════════════════════════════════════════════
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            btnBack.Visible = false;
            InitGrid();
            LoadStudents();
            ResetCounters();

        }

    }
}