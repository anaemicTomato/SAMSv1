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

        // ── Load events for the ComboBox ──────────────────────────────
        public IEnumerable<Event> GetAllEvents()
        {
            using (var conn = new SQLiteConnection(_cs))
                return conn.Query<Event>("SELECT * FROM EventsTable").ToList();
        }

        // ── Load distinct courses for cbCourse ────────────────────────
        public IEnumerable<string> GetAllCourses()
        {
            using (var conn = new SQLiteConnection(_cs))
                return conn.Query<string>(
                    "SELECT DISTINCT Course FROM StudentsTable ORDER BY Course").ToList();
        }

        // ── Load distinct year levels for cbYearLevel ─────────────────
        public IEnumerable<string> GetAllYearLevels()
        {
            using (var conn = new SQLiteConnection(_cs))
                return conn.Query<string>(
                    "SELECT DISTINCT YearLevel FROM StudentsTable ORDER BY YearLevel").ToList();
        }

        // ── Live grid preview + report data ───────────────────────────
        // dates: pass one date "yyyy-MM-dd" or multiple for range
        // course/yearLevel: pass null or "" to mean "All"
        public IEnumerable<AttendanceReportRow> GetAttendance(
            List<string> dates,
            int? eventId,
            string course,
            string yearLevel)
        {
            // Build WHERE clauses dynamically
            var where = new List<string>();
            var param = new DynamicParameters();

            // Date filter — IN clause for single or range
            if (dates != null && dates.Count > 0)
            {
                var placeholders = string.Join(",",
                    dates.Select((d, i) => { param.Add($"@d{i}", d); return $"@d{i}"; }));
                where.Add($"a.Date IN ({placeholders})");
            }

            if (eventId.HasValue)
            {
                where.Add("e.EventID = @EventID");
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
                : "";

            string sql = $@"
                SELECT s.FullName, s.Course, s.YearLevel, s.IdNumber,
                       a.TimeIn, a.TimeOut, a.Status,
                       COALESCE(e.EventName, 'No Event') AS EventName,
                       a.Date AS EventDate
                FROM AttendanceTable a
                JOIN StudentsTable s ON a.StudentID = s.StudentID
                LEFT JOIN EventsTable e ON a.Date = e.EventDate
                {whereClause}
                ORDER BY s.FullName";

            using (var conn = new SQLiteConnection(_cs))
                return conn.Query<AttendanceReportRow>(sql, param).ToList();
        }
    }
}