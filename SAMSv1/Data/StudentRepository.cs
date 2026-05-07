using System.Collections.Generic;
using System.Data.SQLite;
using Dapper;
using SAMSv1.Models;

namespace SAMSv1.Data
{
    public class StudentRepository
    {
        private readonly string _connectionString = DBHelper.GetConnectionString();

        public void RegisterStudent(Student student)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Execute(@"INSERT INTO StudentsTable (FullName, Course, IdNumber) 
                               VALUES (@FullName, @Course, @IdNumber)", student);
            }
        }

        public IEnumerable<Student> GetAllStudents()
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                return conn.Query<Student>("SELECT * FROM StudentsTable");
            }
        }

        public Student GetStudentByIdNumber(string idNumber)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                return conn.QueryFirstOrDefault<Student>(
                    "SELECT * FROM StudentsTable WHERE IdNumber = @IdNumber",
                    new { IdNumber = idNumber });
            }
        }
    }
}