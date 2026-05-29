// Data/LiveAttendanceRepository.cs
using SAMSv1.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SAMSv1.Data
{
    /// <summary>
    /// Handles live attendance scanning — inserts, updates, counters,
    /// and per-student log queries.
    /// OOP: Inherits BaseRepository (Inheritance),
    ///      private mapping + binding (Encapsulation)
    /// </summary>
    public class LiveAttendanceRepository : BaseRepository
    {
        // ── Time-In ───────────────────────────────────────────────
        public void InsertTimeIn(int studentId, string date, string time,
                                 int eventId, string session,
                                 string description, string semester)
        {
            const string sql = @"
                INSERT INTO AttendanceTable
                    (StudentID, Date, TimeIn, TimeOut, Status, AttendanceType,
                     EventID, Session, EventDescription, Semester)
                VALUES
                    (@sid, @date, @time, NULL, 'Present', 'Time-In',
                     @eid, @session, @desc, @semester)";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                BindParams(cmd, studentId, date, time,
                           eventId, session, description, semester);
                cmd.ExecuteNonQuery();
            }
        }

        // ── Time-Out ──────────────────────────────────────────────
        public int FindOpenTimeIn(int studentId, int eventId, string date)
        {
            const string sql = @"
                SELECT AttendanceID FROM AttendanceTable
                WHERE  StudentID      = @sid
                  AND  EventID        = @eid
                  AND  Date           = @date
                  AND  AttendanceType = 'Time-In'
                  AND  TimeOut        IS NULL
                ORDER  BY AttendanceID DESC LIMIT 1";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@sid", studentId);
                cmd.Parameters.AddWithValue("@eid", eventId);
                cmd.Parameters.AddWithValue("@date", date);
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }

        public void CompleteAttendance(int attendanceId, string timeOut)
        {
            const string sql = @"
                UPDATE AttendanceTable
                SET    TimeOut        = @time,
                       AttendanceType = 'Complete'
                WHERE  AttendanceID   = @id";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@time", timeOut);
                cmd.Parameters.AddWithValue("@id", attendanceId);
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertStandaloneTimeOut(int studentId, string date, string time,
                                            int eventId, string session,
                                            string description, string semester)
        {
            const string sql = @"
                INSERT INTO AttendanceTable
                    (StudentID, Date, TimeIn, TimeOut, Status, AttendanceType,
                     EventID, Session, EventDescription, Semester)
                VALUES
                    (@sid, @date, NULL, @time, 'Present', 'Time-Out',
                     @eid, @session, @desc, @semester)";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                BindParams(cmd, studentId, date, time,
                           eventId, session, description, semester);
                cmd.ExecuteNonQuery();
            }
        }

        // ── Counters ──────────────────────────────────────────────
        public int GetCountForEvent(int eventId)
        {
            const string sql =
                "SELECT COUNT(*) FROM AttendanceTable WHERE EventID = @eid";
            return ScalarInt(sql, ("@eid", eventId));
        }

        public int GetPresentCount(int eventId)
        {
            const string sql = @"
                SELECT COUNT(DISTINCT StudentID) FROM AttendanceTable
                WHERE  EventID        = @eid
                  AND  AttendanceType = 'Complete'";
            return ScalarInt(sql, ("@eid", eventId));
        }

        public int GetIncompleteCount(int eventId)
        {
            const string sql = @"
                SELECT COUNT(DISTINCT StudentID) FROM AttendanceTable
                WHERE  EventID = @eid
                  AND  (   (AttendanceType = 'Time-In'  AND TimeOut IS NULL)
                        OR (AttendanceType = 'Time-Out' AND TimeIn  IS NULL))";
            return ScalarInt(sql, ("@eid", eventId));
        }

        public int GetOpenTimeInCount(int eventId)
        {
            const string sql = @"
                SELECT COUNT(*) FROM AttendanceTable
                WHERE  EventID        = @eid
                  AND  AttendanceType = 'Time-In'
                  AND  TimeOut        IS NULL";
            return ScalarInt(sql, ("@eid", eventId));
        }

        // ── Student summary ───────────────────────────────────────
        public AttendanceSummary GetStudentSummary(int studentId)
        {
            const string totalSql = "SELECT COUNT(*) * 2 FROM EventsTable";
            const string presentSql = @"
                SELECT COALESCE(SUM(
                    CASE WHEN AttendanceType = 'Complete' THEN 2 ELSE 1 END
                ), 0)
                FROM AttendanceTable
                WHERE StudentID = @sid";

            using (var conn = OpenConn())
            {
                int totalSlots;
                using (var cmd = new SQLiteCommand(totalSql, conn))
                    totalSlots = Convert.ToInt32(cmd.ExecuteScalar());

                int totalPresent;
                using (var cmd = new SQLiteCommand(presentSql, conn))
                {
                    cmd.Parameters.AddWithValue("@sid", studentId);
                    var r = cmd.ExecuteScalar();
                    totalPresent = (r == null || r == DBNull.Value)
                                   ? 0 : Convert.ToInt32(r);
                }

                return new AttendanceSummary
                {
                    TotalAttendance = totalSlots,
                    TotalPresent = totalPresent,
                    TotalAbsent = Math.Max(0, totalSlots - totalPresent)
                };
            }
        }

        // ── Per-student attendance log ────────────────────────────
        public List<AttendanceLogRow> GetByStudent(int studentId)
        {
            var list = new List<AttendanceLogRow>();
            const string sql = @"
                SELECT
                    a.AttendanceID,
                    s.IdNumber,
                    s.FullName,
                    s.Course,
                    s.YearLevel,
                    e.EventName,
                    a.Session,
                    a.EventDescription,
                    e.EventDate,
                    a.TimeIn,
                    a.TimeOut,
                    a.AttendanceType,
                    a.Status
                FROM EventsTable e
                CROSS JOIN StudentsTable s
                LEFT  JOIN AttendanceTable a
                       ON  a.EventID   = e.EventID
                      AND  a.StudentID = s.StudentID
                WHERE s.StudentID = @sid
                ORDER BY e.EventDate DESC, e.EventID DESC";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@sid", studentId);
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(MapLogRow(r));
            }
            return list;
        }

        // ── Private helpers (Encapsulation) ───────────────────────
        private AttendanceLogRow MapLogRow(SQLiteDataReader r)
        {
            bool isAbsent = r.IsDBNull(0);
            return new AttendanceLogRow
            {
                AttendanceID = isAbsent ? 0 : r.GetInt32(0),
                IdNumber = SafeString(r, 1),
                FullName = SafeString(r, 2),
                Course = SafeString(r, 3),
                YearLevel = SafeString(r, 4),
                EventName = SafeString(r, 5),
                Session = SafeString(r, 6),
                EventDescription = r.IsDBNull(7) ? "No description" : r.GetString(7),
                Date = SafeString(r, 8),
                TimeIn = isAbsent ? "—" : (r.IsDBNull(9) ? "—" : r.GetString(9)),
                TimeOut = isAbsent ? "—" : (r.IsDBNull(10) ? "—" : r.GetString(10)),
                AttendanceType = isAbsent ? "Absent" : SafeString(r, 11),
                Status = isAbsent ? "Absent" : SafeString(r, 12)
            };
        }

        private void BindParams(SQLiteCommand cmd,
            int studentId, string date, string time, int eventId,
            string session, string description, string semester)
        {
            cmd.Parameters.AddWithValue("@sid", studentId);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@time", time);
            cmd.Parameters.AddWithValue("@eid", eventId);
            cmd.Parameters.AddWithValue("@session", (object)session ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@desc", (object)description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@semester", (object)semester ?? DBNull.Value);
        }

        private int ScalarInt(string sql, params (string, object)[] parameters)
        {
            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                foreach (var (name, value) in parameters)
                    cmd.Parameters.AddWithValue(name, value);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}