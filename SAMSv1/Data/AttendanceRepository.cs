using System.Collections.Generic;
using System.Data.SQLite;
using Dapper;
using SAMSv1.Models;

namespace SAMSv1.Data
{
    // This class handles all database read/write operations for attendance.
    // It follows the same pattern as your existing StudentRepository.
    public class AttendanceRepository
    {
        private readonly string _connectionString = DBHelper.GetConnectionString();

        // ---------------------------------------------------------------
        // Called when a face is scanned on the Hikvision device.
        // It records a TimeIn for the student on today's date.
        //
        // Logic:
        //   - If no record exists yet today → insert a new "Time In" row.
        //   - If a record exists but TimeOut is empty → fill in TimeOut.
        //   - If both TimeIn and TimeOut are already filled → do nothing
        //     (prevents duplicate scans from spamming the database).
        // ---------------------------------------------------------------
        public void RecordScan(int studentId, string dateTime)
        {
            // dateTime comes from the device in format: "2026-05-08T14:32:00"
            // We split it into a date part and a time part.
            string date = dateTime.Substring(0, 10);      // "2026-05-08"
            string time = dateTime.Substring(11, 8);      // "14:32:00"

            using (var conn = new SQLiteConnection(_connectionString))
            {
                // Check if there is already a record for this student today.
                var existing = conn.QueryFirstOrDefault<Attendance>(
                    @"SELECT * FROM AttendanceTable 
                      WHERE StudentID = @StudentID AND Date = @Date",
                    new { StudentID = studentId, Date = date });

                if (existing == null)
                {
                    // No record yet today — this is a Time In scan.
                    conn.Execute(
                        @"INSERT INTO AttendanceTable (StudentID, Date, TimeIn, Status)
                          VALUES (@StudentID, @Date, @TimeIn, @Status)",
                        new
                        {
                            StudentID = studentId,
                            Date      = date,
                            TimeIn    = time,
                            Status    = "Present"
                        });
                }
                else if (string.IsNullOrEmpty(existing.TimeOut))
                {
                    // Record exists but no TimeOut yet — this is a Time Out scan.
                    conn.Execute(
                        @"UPDATE AttendanceTable 
                          SET TimeOut = @TimeOut 
                          WHERE AttendanceID = @AttendanceID",
                        new
                        {
                            TimeOut      = time,
                            AttendanceID = existing.AttendanceID
                        });
                }
                // If both TimeIn and TimeOut are filled, we ignore the scan.
            }
        }

        // ---------------------------------------------------------------
        // Returns all attendance records joined with student info,
        // so we can display FullName and IdNumber in the grid.
        // ---------------------------------------------------------------
        public IEnumerable<Attendance> GetAllAttendance()
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                return conn.Query<Attendance>(
                    @"SELECT 
                        a.AttendanceID,
                        a.StudentID,
                        a.Date,
                        a.TimeIn,
                        a.TimeOut,
                        a.Status,
                        s.FullName,
                        s.IdNumber
                      FROM AttendanceTable a
                      JOIN StudentsTable s ON s.StudentID = a.StudentID
                      ORDER BY a.Date DESC, a.TimeIn DESC");
            }
        }

        // ---------------------------------------------------------------
        // Returns attendance for a specific date only (used for filtering).
        // ---------------------------------------------------------------
        public IEnumerable<Attendance> GetAttendanceByDate(string date)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                return conn.Query<Attendance>(
                    @"SELECT 
                        a.AttendanceID,
                        a.StudentID,
                        a.Date,
                        a.TimeIn,
                        a.TimeOut,
                        a.Status,
                        s.FullName,
                        s.IdNumber
                      FROM AttendanceTable a
                      JOIN StudentsTable s ON s.StudentID = a.StudentID
                      WHERE a.Date = @Date
                      ORDER BY a.TimeIn DESC",
                    new { Date = date });
            }
        }
    }
}
