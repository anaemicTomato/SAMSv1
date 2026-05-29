// Services/FaceService.cs
using SAMSv1.Data;
using SAMSv1.Models;
using System.Collections.Generic;

namespace SAMSv1.Services
{
    /// <summary>
    /// Orchestration layer for face-scan attendance flow.
    /// OOP: Composition (holds repo instances), 
    ///      Single Responsibility (coordinates, doesn't query directly),
    ///      Dependency Inversion (depends on repo abstractions via ctor)
    /// </summary>
    public class FaceService
    {
        private readonly StudentRepository _students;
        private readonly EventRepository _events;
        private readonly LiveAttendanceRepository _attendance;

        public FaceService()
        {
            _students = new StudentRepository();
            _events = new EventRepository();
            _attendance = new LiveAttendanceRepository();
        }

        // ── STUDENT ───────────────────────────────────────────────
        public (int StudentID, string FullName) GetStudentByIdNumber(string idNumber)
        {
            var s = _students.GetByIdNumber(idNumber);
            return s != null ? (s.StudentID, s.FullName) : (-1, string.Empty);
        }

        public int GetTotalStudents() => _students.GetTotalCount();

        public List<Student> GetAllStudents() => _students.GetAll();

        public void SaveStudentFromDevice(string idNumber, string fullName)
        {
            _students.Add(new Student
            {
                IdNumber = idNumber,
                FullName = fullName,
                Course = "Unknown",
                YearLevel = "Unknown"
            });
        }

        public void SaveStudentFull(string idNumber, string fullName,
                                    string course, string yearLevel)
        {
            _students.Add(new Student
            {
                IdNumber = idNumber,
                FullName = fullName,
                Course = course,
                YearLevel = yearLevel
            });
        }

        public void UpdateStudent(string newIdNumber, string fullName,
                                   string course, string yearLevel,
                                   string oldIdNumber)
        {
            _students.Update(new Student
            {
                IdNumber = newIdNumber,
                FullName = fullName,
                Course = course,
                YearLevel = yearLevel
            }, oldIdNumber);
        }

        public void DeleteStudent(string idNumber) =>
            _students.DeleteByIdNumber(idNumber);

        public List<AttendanceLogRow> GetAttendanceByStudent(int studentId) =>
            _attendance.GetByStudent(studentId);

        public (int TotalAttendance, int TotalPresent, int TotalAbsent)
            GetStudentSummary(int studentId)
        {
            var s = _attendance.GetStudentSummary(studentId);
            return (s.TotalAttendance, s.TotalPresent, s.TotalAbsent);
        }

        // ── EVENT ─────────────────────────────────────────────────
        public int CreateEvent(string eventName, string eventDate, string startTime) =>
            _events.Create(eventName, eventDate, startTime);

        public void CloseEvent(int eventId, string endTime) =>
            _events.Close(eventId, endTime);

        public int GetOpenEventId(string eventName, string eventDate) =>
            _events.GetLatestEventId(eventName, eventDate);

        public void DeleteEvent(int eventId) =>
            _events.Delete(eventId);

        // ── ATTENDANCE ────────────────────────────────────────────
        /// <summary>
        /// Orchestrates the full Time-In / Time-Out scan logic.
        /// All DB work is delegated to LiveAttendanceRepository.
        /// </summary>
        public void SaveAttendance(
            int studentId, string date, string time,
            string attendanceType, int eventId,
            string session, string eventDescription, string semester)
        {
            if (attendanceType == "Time-In")
            {
                _attendance.InsertTimeIn(
                    studentId, date, time,
                    eventId, session, eventDescription, semester);
            }
            else if (attendanceType == "Time-Out")
            {
                int openId = _attendance.FindOpenTimeIn(studentId, eventId, date);

                if (openId >= 0)
                {
                    _attendance.CompleteAttendance(openId, time);

                    // Auto-close event when all Time-Ins are resolved
                    int stillOpen = _attendance.GetOpenTimeInCount(eventId);
                    if (stillOpen == 0)
                        _events.Close(eventId, time);
                }
                else
                {
                    _attendance.InsertStandaloneTimeOut(
                        studentId, date, time,
                        eventId, session, eventDescription, semester);
                }
            }
        }

        public int GetAttendanceCountForEvent(int eventId) =>
            _attendance.GetCountForEvent(eventId);

        public int GetPresentCount(int eventId) =>
            _attendance.GetPresentCount(eventId);

        public int GetIncompleteCount(int eventId) =>
            _attendance.GetIncompleteCount(eventId);
    }
}