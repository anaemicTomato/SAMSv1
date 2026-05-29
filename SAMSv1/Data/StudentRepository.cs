// Data/StudentRepository.cs
using SAMSv1.Interface;
using SAMSv1.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SAMSv1.Data
{
    /// <summary>
    /// Handles all Student CRUD operations.
    /// OOP: Inherits BaseRepository (Inheritance),
    ///      implements IRepository(Student) (Polymorphism),
    ///      private mapping + binding (Encapsulation)
    /// </summary>
    public class StudentRepository : BaseRepository, IRepository<Student>
    {
        public List<Student> GetAll()
        {
            var list = new List<Student>();
            const string sql = @"
                SELECT StudentID, FullName, IdNumber, Course, YearLevel
                FROM   StudentsTable
                ORDER  BY FullName";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            using (var r = cmd.ExecuteReader())
                while (r.Read())
                    list.Add(MapRow(r));

            return list;
        }

        public Student GetById(int id)
        {
            const string sql = @"
                SELECT StudentID, FullName, IdNumber, Course, YearLevel
                FROM   StudentsTable
                WHERE  StudentID = @id LIMIT 1";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (var r = cmd.ExecuteReader())
                    return r.Read() ? MapRow(r) : null;
            }
        }

        public Student GetByIdNumber(string idNumber)
        {
            const string sql = @"
                SELECT StudentID, FullName, IdNumber, Course, YearLevel
                FROM   StudentsTable
                WHERE  IdNumber = @id LIMIT 1";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", idNumber.Trim());
                using (var r = cmd.ExecuteReader())
                    return r.Read() ? MapRow(r) : null;
            }
        }

        public void Add(Student student)
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
                BindParams(cmd, student);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Student student)
        {
            const string sql = @"
                UPDATE StudentsTable
                SET    FullName  = @name,
                       Course    = @course,
                       YearLevel = @year
                WHERE  IdNumber  = @id";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                BindParams(cmd, student);
                cmd.ExecuteNonQuery();
            }
        }

        // POLY Overload — when ID number itself is being changed
        public void Update(Student student, string oldIdNumber)
        {
            const string sql = @"
                UPDATE StudentsTable
                SET    IdNumber  = @id,
                       FullName  = @name,
                       Course    = @course,
                       YearLevel = @year
                WHERE  IdNumber  = @oldId";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                BindParams(cmd, student);
                cmd.Parameters.AddWithValue("@oldId", oldIdNumber.Trim());
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            const string sql = "DELETE FROM StudentsTable WHERE StudentID = @id";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteByIdNumber(string idNumber)
        {
            const string sql = "DELETE FROM StudentsTable WHERE IdNumber = @id";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", idNumber.Trim());
                cmd.ExecuteNonQuery();
            }
        }

        public int GetTotalCount()
        {
            const string sql = "SELECT COUNT(*) FROM StudentsTable";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
                return Convert.ToInt32(cmd.ExecuteScalar());
        }

        // ── Private helpers (Encapsulation) ───────────────────────
        private Student MapRow(SQLiteDataReader r) => new Student
        {
            StudentID = r.GetInt32(0),
            FullName = SafeString(r, 1),
            IdNumber = SafeString(r, 2),
            Course = SafeString(r, 3),
            YearLevel = SafeString(r, 4)
        };

        private void BindParams(SQLiteCommand cmd, Student s)
        {
            cmd.Parameters.AddWithValue("@id", s.IdNumber?.Trim() ?? "");
            cmd.Parameters.AddWithValue("@name", s.FullName ?? "");
            cmd.Parameters.AddWithValue("@course", s.Course ?? "");
            cmd.Parameters.AddWithValue("@year", s.YearLevel ?? "");
        }
    }
}