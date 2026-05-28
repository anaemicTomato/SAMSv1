using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using SAMSv1.Data;
using SAMSv1.Models;
using SAMSv1.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SAMSv1.CtrlForms
{
    public partial class AttendanceLogAvian : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly AttendanceRepository _repo = new AttendanceRepository();
        private List<Event> _events = new List<Event>();
        private DateTime? _rangeStart = null;
        private DateTime? _rangeEnd = null;

        public AttendanceLogAvian()
        {
            InitializeComponent();
            this.Load += AttendanceLogAvian_Load;
        }

        private void AttendanceLogAvian_Load(object sender, EventArgs e)
        {
            LoadEvents();
            LoadCourses();
            LoadYearLevels();
            EnableCalendarMultiSelect();

            cbEvent.SelectedIndexChanged += (s, _) => RefreshGrid();
            cbCourse.SelectedIndexChanged += (s, _) => RefreshGrid();
            cbYearLevel.SelectedIndexChanged += (s, _) => RefreshGrid();

            scSearchStudent.Properties.Client = gcStudentTable;

            btnSetStart.Click += btnSetStart_Click;
            btnSetEnd.Click += btnSetEnd_Click;
        }

        private void LoadEvents()
        {
            _events = _repo.GetAllEvents().ToList();
            cbEvent.Properties.Items.Clear();
            cbEvent.Properties.Items.Add("All Events");
            foreach (var ev in _events)
                cbEvent.Properties.Items.Add(ev.EventName);
            cbEvent.SelectedIndex = 0;
        }

        private void LoadCourses()
        {
            cbCourse.Properties.Items.Clear();
            cbCourse.Properties.Items.Add("All");
            foreach (var c in _repo.GetAllCourses())
                cbCourse.Properties.Items.Add(c);
            cbCourse.SelectedIndex = 0;
        }

        private void LoadYearLevels()
        {
            cbYearLevel.Properties.Items.Clear();
            cbYearLevel.Properties.Items.Add("All");
            foreach (var y in _repo.GetAllYearLevels())
                cbYearLevel.Properties.Items.Add(y);
            cbYearLevel.SelectedIndex = 0;
        }

        private void EnableCalendarMultiSelect()
        {
            ccCalendar.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Classic;
        }

        private void btnSetStart_Click(object sender, EventArgs e)
        {
            _rangeStart = ccCalendar.DateTime.Date;
            _rangeEnd = null; // reset end whenever start changes

            lblDateRange.Text = $"From: {_rangeStart:MMM dd, yyyy} — now pick end date";
            lblDateRange.ForeColor = System.Drawing.Color.DarkOrange;

            RefreshGrid();
        }

        private void btnSetEnd_Click(object sender, EventArgs e)
        {
            if (_rangeStart == null)
            {
                XtraMessageBox.Show(
                    "Please set a start date first.",
                    "No Start Date",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            _rangeEnd = ccCalendar.DateTime.Date;

            // Swap if end is before start
            if (_rangeEnd < _rangeStart)
                (_rangeStart, _rangeEnd) = (_rangeEnd, _rangeStart);

            // Same date = single day
            if (_rangeEnd == _rangeStart)
                lblDateRange.Text = $"Single day: {_rangeStart:MMM dd, yyyy}";
            else
                lblDateRange.Text = $"{_rangeStart:MMM dd, yyyy}  →  {_rangeEnd:MMM dd, yyyy}";

            lblDateRange.ForeColor = System.Drawing.Color.Green;

            RefreshGrid();
        }

        private List<string> GetSelectedDates()
        {
            if (_rangeStart == null || _rangeEnd == null)
                return new List<string>();

            var dates = new List<string>();
            for (var d = _rangeStart.Value; d <= _rangeEnd.Value; d = d.AddDays(1))
                dates.Add(d.ToString("yyyy-MM-dd"));

            return dates;
        }

        private int? GetSelectedEventId()
        {
            var selected = cbEvent.SelectedItem?.ToString();
            if (selected == null || selected == "All Events") return null;
            var match = _events.FirstOrDefault(e => e.EventName == selected);
            return match?.EventID;
        }

        private string GetSelectedCourse()
            => cbCourse.SelectedItem?.ToString();

        private string GetSelectedYearLevel()
            => cbYearLevel.SelectedItem?.ToString();

        private void RefreshGrid()
        {
            gcStudentTable.DataSource = _repo.GetAttendance(
                GetSelectedDates(),
                GetSelectedEventId(),
                GetSelectedCourse(),
                GetSelectedYearLevel()).ToList();
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            if (_rangeStart == null || _rangeEnd == null)
            {
                XtraMessageBox.Show(
                    "Please set a start and end date first.",
                    "No Date Range Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var report = new StudentRPT(
                GetSelectedDates(),
                GetSelectedEventId(),
                GetSelectedCourse(),
                GetSelectedYearLevel());

            report.ShowPreviewDialog();
        }
    }
}