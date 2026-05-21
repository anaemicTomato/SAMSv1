using SAMSv1.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SAMSv1.Services
{
    public static class FaceService
    {
        private static string _connString;

        // ── Accept a string directly — no connection object needed ──
        public static void Init(string connString)
        {
            _connString = connString;
        }

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

        // ── SAVE ATTENDANCE RECORD ────────────────────────────────
        public static void SaveAttendance(int studentId, string date,
                                          string timeIn, string status)
        {
            const string checkSql = @"
                SELECT COUNT(*) FROM AttendanceTable
                WHERE StudentID = @sid AND Date = @date AND TimeIn = @timeIn";

            const string insertSql = @"
                INSERT INTO AttendanceTable (StudentID, Date, TimeIn, Status)
                VALUES (@sid, @date, @timeIn, @status)";

            using (var conn = OpenConn())
            {
                using (var cmd = new SQLiteCommand(checkSql, conn))
                {
                    cmd.Parameters.AddWithValue("@sid", studentId);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@timeIn", timeIn);
                    if ((long)cmd.ExecuteScalar() > 0)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"[FaceService] Duplicate skipped — StudentID={studentId} Date={date} TimeIn={timeIn}");
                        return;
                    }
                }

                using (var cmd = new SQLiteCommand(insertSql, conn))
                {
                    cmd.Parameters.AddWithValue("@sid", studentId);
                    cmd.Parameters.AddWithValue("@date", date ?? "");
                    cmd.Parameters.AddWithValue("@timeIn", timeIn ?? "");
                    cmd.Parameters.AddWithValue("@status", status ?? "");
                    int rows = cmd.ExecuteNonQuery();
                    System.Diagnostics.Debug.WriteLine(
                        $"[FaceService] SaveAttendance rows={rows} " +
                        $"StudentID={studentId} Date={date} TimeIn={timeIn} Status={status}");
                }
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
        public static List<StudentGridRow> GetAllStudents()
        {
            var list = new List<StudentGridRow>();

            const string sql = @"
                SELECT StudentID, FullName, IdNumber, Course, YearLevel
                FROM StudentsTable ORDER BY FullName";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            using (var r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                    list.Add(new StudentGridRow
                    {
                        StudentID = r.GetInt32(0),
                        FullName  = r.GetString(1),
                        IdNumber  = r.GetString(2),
                        Course    = r.GetString(3),
                        YearLevel = r.GetString(4)
                    });
                }
            }
            return list;
        }

        public class StudentGridRow
        {
            public int StudentID { get; set; }
            public string FullName { get; set; }
            public string IdNumber { get; set; }
            public string Course { get; set; }
            public string YearLevel { get; set; }
        }
    }
}