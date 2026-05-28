using SAMSv1.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SAMSv1.Services
{
    public static class FaceService
    {
        private static string _connString;

        public static void Init(string connString) => _connString = connString;

        private static SQLiteConnection OpenConn()
        {
            var conn = new SQLiteConnection(_connString);
            conn.Open();
            return           conn;
        }

        // ── GET STUDENT BY ID NUMBER ──────────────────────────────
        public static (int StudentID, string FullName) GetStudentByIdNumber(string idNumber)
        {
            const string sql = @"
                SELECT StudentID, FullName FROM StudentsTable
                WHERE IdNumber = @id LIMIT 1";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", idNumber.Trim());
                using (var r = cmd.ExecuteReader())
                    if (r.Read()) return (r.GetInt32(0), r.GetString(1));
            }
            return (-1, string.Empty);
        }

        // ── CREATE EVENT ──────────────────────────────────────────
        // Saves a new event and returns its auto-generated EventID
        public static int CreateEvent(string eventName, string eventDate, string startTime)
        {
            const string sql = @"
                INSERT INTO EventsTable (EventName, EventDate, StartTime)
                VALUES (@name, @date, @start);
                SELECT last_insert_rowid();";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@name", eventName);
                cmd.Parameters.AddWithValue("@date", eventDate);
                cmd.Parameters.AddWithValue("@start", startTime);
                return (int)(long)cmd.ExecuteScalar();
            }
        }

        // ── CLOSE EVENT ───────────────────────────────────────────
        // Called automatically when all Time-In rows for an event are timed out
        // Never called on Stop button — only triggered by last Time-Out scan
        public static void CloseEvent(int eventId, string endTime)
        {
            const string sql = @"
                UPDATE EventsTable SET EndTime = @end
                WHERE EventID = @eid";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@end", endTime);
                cmd.Parameters.AddWithValue("@eid", eventId);
                cmd.ExecuteNonQuery();
            }
        }

        // ── GET LATEST EVENT BY NAME + DATE ──────────────────────
        // Used by Time-Out to reuse the same event as Time-In
        // Finds the latest event regardless of EndTime status
        public static int GetOpenEventId(string eventName, string eventDate)
        {
            const string sql = @"
                SELECT EventID FROM EventsTable
                WHERE  EventName = @name
                  AND  EventDate = @date
                ORDER  BY EventID DESC
                LIMIT  1";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@name", eventName);
                cmd.Parameters.AddWithValue("@date", eventDate);
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }

        // ── GET ATTENDANCE COUNT FOR EVENT ───────────────────────
        // Used to check if an event has any records before deleting it
        public static int GetAttendanceCountForEvent(int eventId)
        {
            const string sql = @"
        SELECT COUNT(*) FROM AttendanceTable
        WHERE EventID = @eid";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@eid", eventId);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // ── DELETE EVENT ──────────────────────────────────────────
        // Only called when event has zero attendance records
        public static void DeleteEvent(int eventId)
        {
            const string sql = @"
        DELETE FROM EventsTable WHERE EventID = @eid";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@eid", eventId);
                cmd.ExecuteNonQuery();
            }
        }

        // ── SAVE ATTENDANCE RECORD ────────────────────────────────
        // Time-In  → INSERT new row (TimeIn filled, TimeOut null, Status = Present)
        // Time-Out → find open Time-In row for same student + event
        //            → fill TimeOut, set Status = Complete, AttendanceType = Complete
        //            → if no open row found, insert standalone Time-Out row
        //            → auto-close event when all Time-In rows have TimeOut filled
        public static void SaveAttendance(
        int studentId, string date, string time,
        string attendanceType, int eventId,
        string session, string eventDescription, string semester)
        {
            if (attendanceType == "Time-In")
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
                    cmd.Parameters.AddWithValue("@sid", studentId);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Parameters.AddWithValue("@eid", eventId);
                    cmd.Parameters.AddWithValue("@session", (object)session ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@desc", (object)eventDescription ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@semester", (object)semester ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            else if (attendanceType == "Time-Out")
            {
                const string findSql = @"
            SELECT AttendanceID FROM AttendanceTable
            WHERE  StudentID      = @sid
              AND  EventID        = @eid
              AND  Date           = @date
              AND  AttendanceType = 'Time-In'
              AND  TimeOut        IS NULL
            ORDER  BY AttendanceID DESC
            LIMIT  1";

                int existingId = -1;
                using (var conn = OpenConn())
                using (var cmd = new SQLiteCommand(findSql, conn))
                {
                    cmd.Parameters.AddWithValue("@sid", studentId);
                    cmd.Parameters.AddWithValue("@eid", eventId);
                    cmd.Parameters.AddWithValue("@date", date);
                    var result = cmd.ExecuteScalar();
                    if (result != null) existingId = Convert.ToInt32(result);
                }

                if (existingId >= 0)
                {
                    const string updateSql = @"
                UPDATE AttendanceTable
                SET    TimeOut        = @time,
                       AttendanceType = 'Complete'
                WHERE  AttendanceID   = @id";

                    using (var conn = OpenConn())
                    using (var cmd = new SQLiteCommand(updateSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@time", time);
                        cmd.Parameters.AddWithValue("@id", existingId);
                        cmd.ExecuteNonQuery();
                    }

                    const string checkSql = @"
                SELECT COUNT(*) FROM AttendanceTable
                WHERE  EventID        = @eid
                  AND  AttendanceType = 'Time-In'
                  AND  TimeOut        IS NULL";

                    int openCount = 0;
                    using (var conn = OpenConn())
                    using (var cmd = new SQLiteCommand(checkSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@eid", eventId);
                        openCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    if (openCount == 0)
                        CloseEvent(eventId, time);
                }
                else
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
                        cmd.Parameters.AddWithValue("@sid", studentId);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@time", time);
                        cmd.Parameters.AddWithValue("@eid", eventId);
                        cmd.Parameters.AddWithValue("@session", (object)session ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@desc", (object)eventDescription ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@semester", (object)semester ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // ── GET TOTAL REGISTERED STUDENTS ────────────────────────
        public static int GetTotalStudents()
        {
            const string sql = "SELECT COUNT(*) FROM StudentsTable";
            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
                return Convert.ToInt32(cmd.ExecuteScalar());
        }

        // ── GET PRESENT COUNT FOR EVENT ───────────────────────────
        // Present = has a Time-In or Complete row for this event
        // ── GET PRESENT COUNT FOR EVENT ───────────────────────────
        // Present = has a Complete row (both Time-In AND Time-Out done)
        public static int GetPresentCount(int eventId)
        {
            const string sql = @"
        SELECT COUNT(DISTINCT StudentID) FROM AttendanceTable
        WHERE  EventID        = @eid
          AND  AttendanceType = 'Complete'";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@eid", eventId);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // ── GET INCOMPLETE COUNT FOR EVENT ────────────────────────
        // Incomplete = Time-In with no TimeOut (scanned in, never out)
        //              OR standalone Time-Out with no TimeIn (scanned out, never in)
        public static int GetIncompleteCount(int eventId)
        {
            const string sql = @"
        SELECT COUNT(DISTINCT StudentID) FROM AttendanceTable
        WHERE  EventID = @eid
          AND  (
                  (AttendanceType = 'Time-In'  AND TimeOut IS NULL)
               OR (AttendanceType = 'Time-Out' AND TimeIn  IS NULL)
              )";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@eid", eventId);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // ── SAVE STUDENT SYNCED FROM DEVICE ──────────────────────
        public static void SaveStudentFromDevice(string idNumber, string fullName)
        {
            const string sql = @"
                INSERT OR IGNORE INTO StudentsTable (FullName, Course, IdNumber, YearLevel)
                VALUES (@name, 'Unknown', @id, 'Unknown')";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@name", fullName ?? "");
                cmd.Parameters.AddWithValue("@id", idNumber.Trim());
                cmd.ExecuteNonQuery();
            }
        }

        // ── SAVE STUDENT WITH FULL DETAILS (Register form) ───────
        public static void SaveStudentFull(string idNumber, string fullName,
                                           string course, string yearLevel)
        {
            const string sql = @"
                INSERT INTO StudentsTable (FullName, Course, IdNumber, YearLevel)
                VALUES (@name, @course, @id, @year)
                ON CONFLICT(IdNumber) DO UPDATE SET
                    FullName  = excluded.FullName,
                    Course    = excluded.Course,
                    YearLevel = excluded.YearLevel";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@name", fullName ?? "");
                cmd.Parameters.AddWithValue("@course", course ?? "");
                cmd.Parameters.AddWithValue("@id", idNumber.Trim());
                cmd.Parameters.AddWithValue("@year", yearLevel ?? "");
                cmd.ExecuteNonQuery();
            }
        }

        // ── GET ALL STUDENTS FOR GRID ─────────────────────────────
        public static List<Student> GetAllStudents()
        {
            var list = new List<Student>();

            const string sql = @"
                SELECT StudentID, FullName, IdNumber, Course, YearLevel
                FROM   StudentsTable
                ORDER  BY FullName";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            using (var r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                    list.Add(new Student
                    {
                        StudentID = r.GetInt32(0),
                        FullName = r.GetString(1),
                        IdNumber = r.GetString(2),
                        Course = r.GetString(3),
                        YearLevel = r.GetString(4)
                    });
                }
            }
            return list;
        }



        // ── GET STUDENT ATTENDANCE SUMMARY ───────────────────────
        // TotalAttendance = sum across all events this student attended
        //   Complete row (has TimeIn + TimeOut) = 2
        //   Time-In only OR Time-Out only       = 1
        // TotalPresent   = distinct events where student has a Complete row
        // TotalAbsent    = total events in DB minus events student attended at all
        public static (int TotalAttendance, int TotalPresent, int TotalAbsent)
            GetStudentSummary(int studentId)
        {
            // Total attendance slots = number of events × 2 (each event has Time-In + Time-Out)
            const string totalSlotsSql = @"
        SELECT COUNT(*) * 2 FROM EventsTable";

            // Present slots:
            //   Complete row = 2 present (both Time-In and Time-Out done)
            //   Time-In only or Time-Out only = 1 present
            const string presentSql = @"
        SELECT COALESCE(SUM(
            CASE WHEN AttendanceType = 'Complete' THEN 2 ELSE 1 END
        ), 0)
        FROM AttendanceTable
        WHERE StudentID = @sid";

            using (var conn = OpenConn())
            {
                int totalSlots, totalPresent;

                using (var cmd = new SQLiteCommand(totalSlotsSql, conn))
                    totalSlots = Convert.ToInt32(cmd.ExecuteScalar());

                using (var cmd = new SQLiteCommand(presentSql, conn))
                {
                    cmd.Parameters.AddWithValue("@sid", studentId);
                    var r = cmd.ExecuteScalar();
                    totalPresent = r == null || r == DBNull.Value ? 0 : Convert.ToInt32(r);
                }

                int totalAbsent = Math.Max(0, totalSlots - totalPresent);

                return (totalSlots, totalPresent, totalAbsent);
            }
        }

        public static List<string> GetAllSessions()
        {
            var list = new List<string>();
            const string sql = @"
        SELECT DISTINCT Session FROM AttendanceTable
        WHERE Session IS NOT NULL
        ORDER BY Session";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            using (var r = cmd.ExecuteReader())
                while (r.Read())
                    list.Add(r.GetString(0));

            return list;
        }

        public static List<string> GetAllEventDescriptions()
        {
            var list = new List<string>();
            const string sql = @"
        SELECT DISTINCT EventDescription FROM AttendanceTable
        WHERE EventDescription IS NOT NULL
        ORDER BY EventDescription";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            using (var r = cmd.ExecuteReader())
                while (r.Read())
                    list.Add(r.GetString(0));

            return list;
        }

        // ── UPDATE STUDENT ────────────────────────────────────────
        public static void UpdateStudent(string newIdNumber, string fullName,
                                          string course, string yearLevel,
                                          string oldIdNumber)
        {
            const string sql = @"
        UPDATE StudentsTable
        SET    IdNumber  = @newId,
               FullName  = @name,
               Course    = @course,
               YearLevel = @year
        WHERE  IdNumber  = @oldId";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@newId", newIdNumber.Trim());
                cmd.Parameters.AddWithValue("@name", fullName ?? "");
                cmd.Parameters.AddWithValue("@course", course ?? "");
                cmd.Parameters.AddWithValue("@year", yearLevel ?? "");
                cmd.Parameters.AddWithValue("@oldId", oldIdNumber.Trim());
                cmd.ExecuteNonQuery();
            }
        }

        // ── GET ATTENDANCE RECORDS FOR A SPECIFIC STUDENT ────────
        public static List<AttendanceLogRow> GetAttendanceByStudent(int studentId)
        {
            var list = new List<AttendanceLogRow>();

            // LEFT JOIN — returns ALL events, with NULL attendance columns
            // for events the student never attended
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
        LEFT JOIN AttendanceTable a
               ON a.EventID   = e.EventID
              AND a.StudentID = s.StudentID
        WHERE s.StudentID = @sid
        ORDER BY e.EventDate DESC, e.EventID DESC";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@sid", studentId);
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        bool isAbsent = r.IsDBNull(0); // AttendanceID is NULL = no record

                        list.Add(new AttendanceLogRow
                        {
                            AttendanceID = isAbsent ? 0 : r.GetInt32(0),
                            IdNumber = r.IsDBNull(1) ? "" : r.GetString(1),
                            FullName = r.IsDBNull(2) ? "" : r.GetString(2),
                            Course = r.IsDBNull(3) ? "" : r.GetString(3),
                            YearLevel = r.IsDBNull(4) ? "" : r.GetString(4),
                            EventName = r.IsDBNull(5) ? "" : r.GetString(5),
                            Session = r.IsDBNull(6) ? "" : r.GetString(6),
                            EventDescription = r.IsDBNull(7) ? "No description" : r.GetString(7),
                            Date = r.IsDBNull(8) ? "" : r.GetString(8),
                            TimeIn = isAbsent ? "—" : (r.IsDBNull(9) ? "—" : r.GetString(9)),
                            TimeOut = isAbsent ? "—" : (r.IsDBNull(10) ? "—" : r.GetString(10)),
                            AttendanceType = isAbsent ? "Absent" : (r.IsDBNull(11) ? "" : r.GetString(11)),
                            Status = isAbsent ? "Absent" : (r.IsDBNull(12) ? "" : r.GetString(12)),
                        });
                    }
                }
            }
            return list;
        }

        // ── DELETE STUDENT ────────────────────────────────────────
        public static void DeleteStudent(string idNumber)
        {
            const string sql = @"
        DELETE FROM StudentsTable WHERE IdNumber = @id";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", idNumber.Trim());
                cmd.ExecuteNonQuery();
            }
        }

    }
}