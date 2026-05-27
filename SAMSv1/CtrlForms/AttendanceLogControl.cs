using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using SAMSv1.Reports;
using SAMSv1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAMSv1.CtrlForms
{
    public partial class AttendanceLogControl : DevExpress.XtraEditors.XtraUserControl
    {
        private BindingList<AttendanceLogRow> _bindingList;
        private BindingSource _bindingSource;

        public class AttendanceLogRow
        {
            public int AttendanceID { get; set; }
            public string IdNumber { get; set; }
            public string FullName { get; set; }
            public string Course { get; set; }
            public string YearLevel { get; set; }
            public string EventName { get; set; }
            public string Date { get; set; }
            public string TimeIn { get; set; }
            public string TimeOut { get; set; }
            public string AttendanceType { get; set; }
            public string Status { get; set; }
        }

        public AttendanceLogControl()
        {
            InitializeComponent();
        }

        private void AttendanceLogControl_Load_1(object sender, EventArgs e)
        {
            InitGrid();
            LoadEventComboBox();
            LoadAttendanceLogs();
            UpdateSummaryCounters();

            // Wire up filters
            cbSearchEvent.SelectedIndexChanged += (s, ev) => LoadAttendanceLogs();
            searchStudent.EditValueChanged += (s, ev) => LoadAttendanceLogs();
            comboBoxCourse.SelectedIndexChanged += (s, ev) => LoadAttendanceLogs();
            comboBoxYears.SelectedIndexChanged += (s, ev) => LoadAttendanceLogs();
        }


        // ================= GRID INIT =================
        private void InitGrid()
        {
            _bindingList = new BindingList<AttendanceLogRow>();
            _bindingSource = new BindingSource { DataSource = _bindingList };

            gcAttendanceLogs.DataSource = _bindingSource;

            // Map columns to properties
            gridColumnID.FieldName = nameof(AttendanceLogRow.IdNumber);
            gridColumnName.FieldName = nameof(AttendanceLogRow.FullName);
            gridColumnCourse.FieldName = nameof(AttendanceLogRow.Course);
            gridColumnYearLevel.FieldName = nameof(AttendanceLogRow.YearLevel);
            gridColumnTime.FieldName = nameof(AttendanceLogRow.EventName);
        }

        // ================= LOAD EVENTS INTO COMBOBOX =================
        private void LoadEventComboBox()
        {
            try
            {
                var events = FaceService.GetAllEventNames();

                cbSearchEvent.Properties.Items.Clear();
                cbSearchEvent.Properties.Items.Add("All Events");

                foreach (var ev in events)
                    cbSearchEvent.Properties.Items.Add(ev);

                cbSearchEvent.EditValue = "All Events";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[LoadEvents] {ex}");
            }
        }

        // ================= LOAD / FILTER GRID =================
        private void LoadAttendanceLogs()
        {
            try
            {
                string eventFilter = cbSearchEvent.EditValue?.ToString();
                string nameFilter = searchStudent.Text?.Trim();
                string courseFilter = comboBoxCourse.EditValue?.ToString();
                string yearFilter = comboBoxYears.EditValue?.ToString();

                if (eventFilter == "All Events") eventFilter = null;
                if (courseFilter == "All Course") courseFilter = null;
                if (yearFilter == "All Years") yearFilter = null;

                var rows = FaceService.GetAttendanceLogs(
                    eventFilter, nameFilter, courseFilter, yearFilter);

                _bindingList.Clear();
                foreach (var r in rows)
                    _bindingList.Add(r);

                _bindingSource.ResetBindings(false);
                gcAttendanceLogs.RefreshDataSource();

                UpdateSummaryCounters();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[LoadLogs] {ex}");
            }
        }

        // ================= SUMMARY COUNTERS =================
        // labelTotalAttendance = total attendance records
        //   (Complete row = 2, Time-In only or Time-Out only = 1)
        // labelTotalPresent    = total present slots filled
        //   (Complete = 2 present, single Time-In or Time-Out = 1 present)
        // labelTotalAbsent     = total registered students
        //   minus distinct students who appear in current filtered logs
        private void UpdateSummaryCounters()
        {
            try
            {
                string eventFilter = cbSearchEvent.EditValue?.ToString();
                string courseFilter = comboBoxCourse.EditValue?.ToString();
                string yearFilter = comboBoxYears.EditValue?.ToString();

                if (eventFilter == "All Events") eventFilter = null;
                if (courseFilter == "All Course") courseFilter = null;
                if (yearFilter == "All Years") yearFilter = null;

                var counts = FaceService.GetAttendanceSummary(
                    eventFilter, courseFilter, yearFilter);

                labelTotalAttendance.Text = counts.TotalAttendance.ToString("D2");
                labelTotalPresent.Text = counts.TotalPresent.ToString("D2");
                labelTotalAbsent.Text = counts.TotalAbsent.ToString("D2");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Counters] {ex}");
            }
        }
        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            var report = new StudentRPT();
            report.ShowPreviewDialog();
        }

        
    }
}
