using SAMSv1.Data;
using SAMSv1.Helpers;
using SAMSv1.Models;
using System.Collections.Generic;

namespace SAMSv1.Reports
{
    public partial class StudentRPT : DevExpress.XtraReports.UI.XtraReport
    {
        public StudentRPT()
        {
            InitializeComponent();
        }

        public StudentRPT(
            List<string> dates,
            int? eventId,
            string course,
            string yearLevel)
        {
            InitializeComponent();

            var repo = new AttendanceRepository();
            var rows = repo.GetAttendance(dates, eventId, course, yearLevel);

            this.DataSource = rows.ToDataTable();
        }
    }
}