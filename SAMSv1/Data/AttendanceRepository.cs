// Data/AttendanceRepository.cs
using Dapper;
using SAMSv1.Models;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace SAMSv1.Data
{
    /// <summary>
    /// Dapper-based repository for filtered report queries.
    /// OOP: Inherits BaseRepository (Inheritance)
    /// </summary>
    public class AttendanceRepository : BaseRepository   // ← add this
    {
        // DELETE this line — no longer needed, BaseRepository handles it
        // private readonly string _cs = DBHelper.GetConnectionString();

        public IEnumerable<Event> GetAllEvents()
        {
            using (var conn = new SQLiteConnection(DBHelper.ConnectionString))
                return conn.Query<Event>(
                    "SELECT * FROM EventsTable ORDER BY EventDate DESC").ToList();
        }

        public IEnumerable<string> GetAllCourses()
        {
            using (var conn = new SQLiteConnection(DBHelper.ConnectionString))
                return conn.Query<string>(
                    "SELECT DISTINCT Course FROM StudentsTable ORDER BY Course").ToList();
        }

        public IEnumerable<string> GetAllYearLevels()
        {
            using (var conn = new SQLiteConnection(DBHelper.ConnectionString))
                return conn.Query<string>(
                    "SELECT DISTINCT YearLevel FROM StudentsTable ORDER BY YearLevel").ToList();
        }

        // GetAttendance(...) stays exactly as you had it — no changes needed
        public IEnumerable<AttendanceReportRow> GetAttendance(
    List<string> dates, int? eventId, string course,
    string yearLevel, string session, string semester)
        {
            using (var conn = new SQLiteConnection(DBHelper.ConnectionString))
            {
                conn.Open();

                var param = new DynamicParameters();

                // ── Build date filter ─────────────────────────────────
                string dateFilter = string.Empty;
                if (dates != null && dates.Count > 0)
                {
                    string placeholders = string.Join(",",
                        dates.Select((d, i) => { param.Add($"@d{i}", d); return $"@d{i}"; }));
                    dateFilter = $"AND a.Date IN ({placeholders})";
                }

                // ── Build semester filter on JOIN ─────────────────────
                string semesterJoinFilter = string.Empty;
                if (!string.IsNullOrEmpty(semester) && semester != "All")
                {
                    semesterJoinFilter = "AND a.Semester = @Semester";
                    param.Add("@Semester", semester);
                }

                // ── Session filter for ExistingSessions CTE ───────────
                string sessionCteFilter = string.Empty;
                if (!string.IsNullOrEmpty(session) && session != "All")
                {
                    sessionCteFilter = "AND Session = @Session";
                    param.Add("@Session", session);
                }

                // ── WHERE clause filters ──────────────────────────────
                var where = new List<string>();

                if (eventId.HasValue)
                {
                    where.Add("(a.EventID = @EventID OR a.EventID IS NULL)");
                    param.Add("@EventID", eventId.Value);
                }

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

                string whereClause = where.Count > 0
                    ? "WHERE " + string.Join(" AND ", where)
                    : string.Empty;

                string sql = $@"
            WITH ExistingSessions AS (
                SELECT DISTINCT Session
                FROM   AttendanceTable
                WHERE  Session IS NOT NULL
                {sessionCteFilter}
            )
            SELECT
                s.FullName,
                s.Course,
                s.YearLevel,
                s.IdNumber,
                COALESCE(a.TimeIn,  '-')                        AS TimeIn,
                COALESCE(a.TimeOut, '-')                        AS TimeOut,
                CASE WHEN a.StudentID IS NULL THEN 'Absent'
                     ELSE a.Status END                          AS Status,
                COALESCE(a.AttendanceType, 'Absent')            AS AttendanceType,
                es.Session                                      AS Session,
                COALESCE(a.Semester, '-')                       AS Semester,
                COALESCE(a.EventDescription, '')                AS EventDescription,
                COALESCE(a.Date, '')                            AS EventDate,
                COALESCE(e.EventName, 'No Event')               AS EventName,
                COALESCE(e.StartTime, '')                       AS StartTime,
                COALESCE(e.EndTime,   '')                       AS EndTime
            FROM StudentsTable s
            CROSS JOIN ExistingSessions es
            LEFT JOIN AttendanceTable a
                   ON  a.StudentID = s.StudentID
                  AND  a.Session   = es.Session
                  {dateFilter}
                  {semesterJoinFilter}
            LEFT JOIN EventsTable e ON a.EventID = e.EventID
            {whereClause}
            ORDER BY es.Session, s.Course, s.YearLevel, s.FullName";

                return conn.Query<AttendanceReportRow>(sql, param).ToList();
            }
        }
    }
}