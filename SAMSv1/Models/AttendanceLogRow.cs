using SAMSv1.Models;

public class AttendanceLogRow : PersonBase
{
    public int AttendanceID { get; set; }
    public string Course { get; set; }
    public string YearLevel { get; set; }
    public string EventName { get; set; }
    public string Session { get; set; } 
    public string EventDescription { get; set; } 
    public string Semester { get; set; }
    public string Date { get; set; }
    public string ScanTime { get; set; }
    public string TimeIn { get; set; }
    public string TimeOut { get; set; }
    public string AttendanceType { get; set; }
    public string Status { get; set; }
}