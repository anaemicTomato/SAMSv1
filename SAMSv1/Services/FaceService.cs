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
            return conn;
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

        // ── GRID ROW MODEL ────────────────────────────────────────

        // ── GET ALL EVENT NAMES FOR COMBOBOX ─────────────────────
        public static List<string> GetAllEventNames()
        {
            var list = new List<string>();
            const string sql = @"
        SELECT DISTINCT EventName FROM EventsTable
        ORDER BY EventName";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            using (var r = cmd.ExecuteReader())
                while (r.Read())
                    list.Add(r.GetString(0));

            return list;
        }

        // ── GET ATTENDANCE LOGS WITH FILTERS ─────────────────────
        public static List<AttendanceLogRow> GetAttendanceLogs(
            string eventName, string nameSearch, string course, string yearLevel, string session, string eventDescription)
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
                a.Date,
                a.TimeIn,
                a.TimeOut,
                a.AttendanceType,
                a.Status
            FROM AttendanceTable a
            INNER JOIN StudentsTable s ON s.StudentID = a.StudentID
            INNER JOIN EventsTable   e ON e.EventID   = a.EventID
            WHERE 1=1
              AND (@eventName        IS NULL OR e.EventName        = @eventName)
              AND (@nameSearch       IS NULL OR s.FullName         LIKE '%' || @nameSearch || '%'
                                            OR s.IdNumber          LIKE '%' || @nameSearch || '%')
              AND (@course           IS NULL OR s.Course           = @course)
              AND (@yearLevel        IS NULL OR s.YearLevel        = @yearLevel)
              AND (@session          IS NULL OR a.Session          = @session)          -- ← add
              AND (@eventDescription IS NULL OR a.EventDescription = @eventDescription) -- ← add
            ORDER BY a.Date DESC, a.TimeIn DESC";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@eventName", (object)eventName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nameSearch", (object)nameSearch ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@course", (object)course ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@yearLevel", (object)yearLevel ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@session", (object)session ?? DBNull.Value); // ← add
                cmd.Parameters.AddWithValue("@eventDescription", (object)eventDescription ?? DBNull.Value); // ← add

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new AttendanceLogRow
                        {
                            AttendanceID = r.GetInt32(0),
                            IdNumber = r.IsDBNull(1) ? "" : r.GetString(1),
                            FullName = r.IsDBNull(2) ? "" : r.GetString(2),
                            Course = r.IsDBNull(3) ? "" : r.GetString(3),
                            YearLevel = r.IsDBNull(4) ? "" : r.GetString(4),
                            EventName = r.IsDBNull(5) ? "" : r.GetString(5),
                            Session = r.IsDBNull(6) ? "" : r.GetString(6),
                            EventDescription = r.IsDBNull(7) ? "" : r.GetString(7),
                            Date = r.IsDBNull(8) ? "" : r.GetString(8),
                            TimeIn = r.IsDBNull(9) ? "" : r.GetString(9),
                            TimeOut = r.IsDBNull(10) ? "" : r.GetString(10),
                            AttendanceType = r.IsDBNull(11) ? "" : r.GetString(11),
                            Status = r.IsDBNull(12) ? "" : r.GetString(12),
                        });
                    }
                }
            }
            return list;
        }

        public static AttendanceSummary GetAttendanceSummary(
    string eventName, string course, string yearLevel)
        {
            // TotalAttendance = total scan slots used
            //   Complete = 2 (both TimeIn + TimeOut filled)
            //   Time-In only OR Time-Out only = 1
            //   No record = 0
            const string attendanceSql = @"
        SELECT SUM(CASE WHEN a.AttendanceType = 'Complete' THEN 2 ELSE 1 END)
        FROM AttendanceTable a
        INNER JOIN StudentsTable s ON s.StudentID = a.StudentID
        INNER JOIN EventsTable   e ON e.EventID   = a.EventID
        WHERE 1=1
          AND (@eventName IS NULL OR e.EventName = @eventName)
          AND (@course    IS NULL OR s.Course    = @course)
          AND (@yearLevel IS NULL OR s.YearLevel = @yearLevel)";

            // TotalPresent = present scan slots
            //   Complete = 2 present slots
            //   Incomplete (Time-In or Time-Out only) = 1 present slot
            //   No record = 0 present slots
            const string presentSql = @"
        SELECT SUM(CASE WHEN a.AttendanceType = 'Complete' THEN 2 ELSE 1 END)
        FROM AttendanceTable a
        INNER JOIN StudentsTable s ON s.StudentID = a.StudentID
        INNER JOIN EventsTable   e ON e.EventID   = a.EventID
        WHERE 1=1
          AND (@eventName IS NULL OR e.EventName = @eventName)
          AND (@course    IS NULL OR s.Course    = @course)
          AND (@yearLevel IS NULL OR s.YearLevel = @yearLevel)";

            // TotalAbsent:
            //   Each event has 2 slots per student (Time-In + Time-Out)
            //   Complete = 0 absent slots
            //   Incomplete = 1 absent slot
            //   No record = 2 absent slots
            const string absentSql = @"
        SELECT
            -- Total possible slots for ALL students in this event
            (
                SELECT COUNT(*) * 2
                FROM StudentsTable s2
                WHERE (@course    IS NULL OR s2.Course    = @course)
                  AND (@yearLevel IS NULL OR s2.YearLevel = @yearLevel)
            )
            -
            -- Subtract present slots already counted
            (
                SELECT COALESCE(SUM(CASE WHEN a.AttendanceType = 'Complete' THEN 2 ELSE 1 END), 0)
                FROM AttendanceTable a
                INNER JOIN StudentsTable s ON s.StudentID = a.StudentID
                INNER JOIN EventsTable   e ON e.EventID   = a.EventID
                WHERE 1=1
                  AND (@eventName IS NULL OR e.EventName = @eventName)
                  AND (@course    IS NULL OR s.Course    = @course)
                  AND (@yearLevel IS NULL OR s.YearLevel = @yearLevel)
            )";

            var summary = new AttendanceSummary();

            using (var conn = OpenConn())
            {
                using (var cmd = new SQLiteCommand(attendanceSql, conn))
                {
                    cmd.Parameters.AddWithValue("@eventName", (object)eventName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@course", (object)course ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@yearLevel", (object)yearLevel ?? DBNull.Value);
                    var result = cmd.ExecuteScalar();
                    summary.TotalAttendance = result == DBNull.Value || result == null
                        ? 0 : Convert.ToInt32(result);
                }

                using (var cmd = new SQLiteCommand(presentSql, conn))
                {
                    cmd.Parameters.AddWithValue("@eventName", (object)eventName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@course", (object)course ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@yearLevel", (object)yearLevel ?? DBNull.Value);
                    var result = cmd.ExecuteScalar();
                    summary.TotalPresent = result == DBNull.Value || result == null
                        ? 0 : Convert.ToInt32(result);
                }

                using (var cmd = new SQLiteCommand(absentSql, conn))
                {
                    cmd.Parameters.AddWithValue("@eventName", (object)eventName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@course", (object)course ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@yearLevel", (object)yearLevel ?? DBNull.Value);
                    var result = cmd.ExecuteScalar();
                    summary.TotalAbsent = result == DBNull.Value || result == null
                        ? 0 : Math.Max(0, Convert.ToInt32(result));
                }
            }

            return summary;
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

    }
}