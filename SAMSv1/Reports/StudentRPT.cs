using SAMSv1.Data;
using SAMSv1.Helpers;
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
            string yearLevel,
            string session,
            string semester,
            string attendanceType)
        {
            InitializeComponent();

            var repo = new AttendanceRepository();
            var rows = repo.GetAttendance(
                dates, eventId, course, yearLevel,
                session, semester, attendanceType);

            this.DataSource = rows.ToDataTable();
        }
    }
}