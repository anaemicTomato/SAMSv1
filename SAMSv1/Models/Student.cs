// Models/Student.cs
namespace SAMSv1.Models
{
    public class Student : PersonBase
    {
        public int StudentID { get; set; }
        public string Course { get; set; }
        public string YearLevel { get; set; }
    }
}