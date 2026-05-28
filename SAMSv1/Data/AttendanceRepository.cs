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
    string semester)
{
    using (var conn = new SQLiteConnection(_cs))
    {
        conn.Open();

        var param = new DynamicParameters();
        var where = new List<string>();
        // ─────────────────────────────────────
        // CHECK IF SESSION EXISTS
        // ─────────────────────────────────────

        if (!string.IsNullOrEmpty(session) &&
            session != "All")
        {
            int sessionExists = conn.ExecuteScalar<int>(@"
                SELECT COUNT(*)
                FROM AttendanceTable
                WHERE Session = @Session",
                new { Session = session });

            // No attendance rows for this session
            if (sessionExists == 0)
            {
                return new List<AttendanceReportRow>();
            }
        }

        // ─────────────────────────────────────
        // BUILD ATTENDANCE JOIN
        // ─────────────────────────────────────

        string attendanceJoin = @"
            LEFT JOIN AttendanceTable a
                ON s.StudentID = a.StudentID";

                // SESSION
                if (!string.IsNullOrEmpty(session) &&
                session != "All")
                {
                    where.Add("a.Session = @Session");
                    param.Add("@Session", session);
                }
                // EVENT
                if (eventId.HasValue)
                {
                    where.Add("a.EventID = @EventID");
                    param.Add("@EventID", eventId.Value);
                }

                // SEMESTER
                if (!string.IsNullOrEmpty(semester) &&
            semester != "All")
        {
            attendanceJoin += " AND a.Semester = @Semester";
            param.Add("@Semester", semester);
        }



        // DATES
        if (dates != null && dates.Count > 0)
        {
            var placeholders = string.Join(",",
                dates.Select((d, i) =>
                {
                    param.Add($"@d{i}", d);
                    return $"@d{i}";
                }));

            attendanceJoin +=
                $" AND a.Date IN ({placeholders})";
        }

        // ─────────────────────────────────────
        // WHERE CLAUSE
        // ─────────────────────────────────────

        

        // COURSE
        if (!string.IsNullOrEmpty(course) &&
            course != "All")
        {
            where.Add("s.Course = @Course");
            param.Add("@Course", course);
        }

        // YEAR LEVEL
        if (!string.IsNullOrEmpty(yearLevel) &&
            yearLevel != "All")
        {
            where.Add("s.YearLevel = @YearLevel");
            param.Add("@YearLevel", yearLevel);
        }

        string whereClause = where.Count > 0
            ? "WHERE " + string.Join(" AND ", where)
            : "";

                // ─────────────────────────────────────
                // FINAL SQL
                // ─────────────────────────────────────

                string sql = $@"
                WITH ExistingSessions AS
                (
                    SELECT DISTINCT Session
                    FROM AttendanceTable
                    WHERE Session IS NOT NULL
                )

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

                    es.Session AS Session,

                    COALESCE(a.Semester, '-') AS Semester,

                    COALESCE(a.EventDescription, '') AS EventDescription,

                    COALESCE(a.Date, '') AS EventDate,

                    COALESCE(e.EventName, 'No Event') AS EventName,
                    COALESCE(e.StartTime, '') AS StartTime,
                    COALESCE(e.EndTime, '') AS EndTime

                FROM StudentsTable s

                CROSS JOIN ExistingSessions es

                {attendanceJoin}
                AND a.Session = es.Session

                LEFT JOIN EventsTable e
                    ON a.EventID = e.EventID

                {whereClause}

                ORDER BY
                    es.Session,
                    s.Course,
                    s.YearLevel,
                    s.FullName
                ";

                return conn.Query<AttendanceReportRow>(
            sql,
            param).ToList();
    }
}
    }
}