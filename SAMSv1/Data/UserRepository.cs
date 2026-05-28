using System.Collections.Generic;
using System.Data.SQLite;
using Dapper;
using SAMSv1.Models;

namespace SAMSv1.Data
{
    public class UserRepository
    {
        private readonly string _connectionString =
            DBHelper.GetConnectionString();

        // CREATE
        public void AddUser(User user)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Execute(@"
                    INSERT INTO UserTable
                    (Username, Password, Role)
                    VALUES
                    (@Username, @Password, @Role)", user);
            }
        }

        // READ 
        public IEnumerable<User> GetAllUsers()
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                return conn.Query<User>(
                    "SELECT * FROM UserTable");
            }
        }

        // UPDATE
        public void UpdateUser(User user)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Execute(@"
                    UPDATE UserTable
                    SET
                        Username = @Username,
                        Password = @Password,
                        Role = @Role
                    WHERE UserID = @UserID", user);
            }
        }

        // DELETE
        public void DeleteUser(int userId)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Execute(@"
                    DELETE FROM UserTable
                    WHERE UserID = @UserID",
                    new { UserID = userId });
            }
        }
    }
}