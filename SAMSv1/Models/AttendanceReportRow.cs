namespace SAMSv1.Models
{
    public class AttendanceReportRow
    {
        // Student info
        public string FullName { get; set; }
        public string Course { get; set; }
        public string YearLevel { get; set; }
        public string IdNumber { get; set; }

        // Attendance info
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public string Status { get; set; }
        public string AttendanceType { get; set; }
        public string Session { get; set; }
        public string Semester { get; set; }

        // Event info
        public string EventName { get; set; }
        public string EventDate { get; set; }
        public string EventDescription { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}