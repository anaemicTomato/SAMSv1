namespace SAMSv1.Models
{
    // This class represents one attendance record in the database.
    // Each property maps to a column in AttendanceTable.
    public class Attendance
    {
        public int AttendanceID { get; set; }
        public int StudentID    { get; set; }
        public string Date      { get; set; }   // stored as "yyyy-MM-dd"
        public string TimeIn    { get; set; }   // stored as "HH:mm:ss"
        public string TimeOut   { get; set; }   // null until student scans out
        public string Status    { get; set; }   // "Present", "Late", etc.
        public long SerialNo    { get; set; }

        // These two are NOT in the database — we fill them in after a JOIN
        // so the attendance grid can show the student's name and ID number.
        public string FullName  { get; set; }
        public string IdNumber  { get; set; }

        public string Course { get; set; } //didungag nako para sa JOIN wala ni sila sa contstructor
        public string YearLevel { get; set; }


        public Attendance(
            int attendanceID = 0,
            int studentID = 0,
            string date = "",
            string timeIn = "",
            string timeOut = null,
            string status = "",
            long serialNo = 0,
            string fullName = "",
            string idNumber = "")
        {
            AttendanceID = attendanceID;
            StudentID = studentID;
            Date = date;
            TimeIn = timeIn;
            TimeOut = timeOut;
            Status = status;
            SerialNo = serialNo;
            FullName = fullName;
            IdNumber = idNumber;
        }
    }
}
