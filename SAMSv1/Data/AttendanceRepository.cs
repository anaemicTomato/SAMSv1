using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using SAMSv1.Models;

namespace SAMSv1.Data
{
    public class AttendanceRepository
    {
        private readonly string _cs = DBHelper.GetConnectionString();

        public IEnumerable<Event> GetAllEvents()
        {
            using (var conn = new SQLiteConnection(_cs))
                return conn.Query<Event>("SELECT * FROM EventsTable").ToList();
        }

        public IEnumerable<string> GetAllCourses()
        {
            using (var conn = new SQLiteConnection(_cs))
                return conn.Query<string>(
                    "SELECT DISTINCT Course FROM StudentsTable ORDER BY Course").ToList();
        }

        public IEnumerable<string> GetAllYearLevels()
        {
            using (var conn = new SQLiteConnection(_cs))
                return conn.Query<string>(
                    "SELECT DISTINCT YearLevel FROM StudentsTable ORDER BY YearLevel").ToList();
        }

        public IEnumerable<AttendanceReportRow> GetAttendance(
            List<string> dates,
            int? eventId,
            string course,
            string yearLevel,
            string session,
            string semester,
            string attendanceType)
        {
            var where = new List<string>();
            var param = new DynamicParameters();

            // ─────────────────────────────────────
            // STUDENT FILTERS
            // ─────────────────────────────────────

            if (!string.IsNullOrEmpty(course) && course != "All")
            {
                where.Add("s.Course = @Course");
                param.Add("@Course", course);
            }

            if (!string.IsNullOrEmpty(yearLevel) && yearLevel != "All")
            {
                where.Add("s.YearLevel = @YearLevel");
                param.Add("@YearLevel", yearLevel);
            }

            // ─────────────────────────────────────
            // ATTENDANCE FILTERS
            // ─────────────────────────────────────

            if (dates != null && dates.Count > 0)
            {
                var placeholders = string.Join(",",
                    dates.Select((d, i) =>
                    {
                        param.Add($"@d{i}", d);
                        return $"@d{i}";
                    }));

                where.Add($@"
            (
                a.Date IN ({placeholders})
                OR a.Date IS NULL
                )");
            }

            if (eventId.HasValue)
            {
                where.Add(@"
            (
                a.EventID = @EventID
                OR a.EventID IS NULL
                )");

                param.Add("@EventID", eventId.Value);
            }

            if (!string.IsNullOrEmpty(session) && session != "All")
            {
                where.Add(@"
            (
                a.Session = @Session
                OR a.Session IS NULL
                )");

                param.Add("@Session", session);
            }

            if (!string.IsNullOrEmpty(semester) && semester != "All")
            {
                where.Add(@"
            (
                a.Semester = @Semester
                OR a.Semester IS NULL
                )");

                param.Add("@Semester", semester);
            }

            if (!string.IsNullOrEmpty(attendanceType) &&
                attendanceType != "Both")
            {
                where.Add(@"
            (
                a.AttendanceType = @AttendanceType
                OR a.AttendanceType IS NULL
                )");

                param.Add("@AttendanceType", attendanceType);
            }

            string whereClause = where.Count > 0
                ? "WHERE " + string.Join(" AND ", where)
                : "";

            string sql = $@"
            SELECT
            s.FullName,
            s.Course,
            s.YearLevel,
            s.IdNumber,

            COALESCE(a.TimeIn, '-') AS TimeIn,
            COALESCE(a.TimeOut, '-') AS TimeOut,

            CASE
                WHEN a.StudentID IS NULL THEN 'Absent'
                ELSE a.Status
            END AS Status,

            COALESCE(a.AttendanceType, '-') AS AttendanceType,
            COALESCE(a.Session, '-') AS Session,
            COALESCE(a.Semester, '-') AS Semester,

            COALESCE(a.EventDescription, '') AS EventDescription,

            COALESCE(a.Date, '') AS EventDate,

            COALESCE(e.EventName, 'No Event') AS EventName,
            COALESCE(e.StartTime, '') AS StartTime,
            COALESCE(e.EndTime, '') AS EndTime

            FROM StudentsTable s

            LEFT JOIN AttendanceTable a
                ON s.StudentID = a.StudentID

            LEFT JOIN EventsTable e
                ON a.EventID = e.EventID

            {whereClause}

            ORDER BY
                s.Course,
                s.YearLevel,
                s.FullName
            ";

            using (var conn = new SQLiteConnection(_cs))
            {
                return conn.Query<AttendanceReportRow>(sql, param).ToList();
            }
        }
    }
}