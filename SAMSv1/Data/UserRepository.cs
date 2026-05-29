// Data/UserRepository.cs
using Dapper;
using SAMSv1.Models;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SAMSv1.Data
{
    /// <summary>
    /// Handles all User CRUD operations.
    /// OOP: Inherits BaseRepository (Inheritance),
    ///      private Dapper calls hidden from outside (Encapsulation)
    /// </summary>
    public class UserRepository : BaseRepository
    {
        // CREATE
        public void AddUser(User user)
        {
            using (var conn = new SQLiteConnection(DBHelper.ConnectionString))
            {
                conn.Execute(@"
                    INSERT INTO UserTable (Username, Password, Role)
                    VALUES (@Username, @Password, @Role)", user);
            }
        }

        // READ
        public List<User> GetAllUsers()
        {
            using (var conn = new SQLiteConnection(DBHelper.ConnectionString))
            {
                return conn.Query<User>(
                    "SELECT * FROM UserTable").AsList();
            }
        }

        // UPDATE
        public void UpdateUser(User user)
        {
            using (var conn = new SQLiteConnection(DBHelper.ConnectionString))
            {
                conn.Execute(@"
                    UPDATE UserTable
                    SET    Username = @Username,
                           Password = @Password,
                           Role     = @Role
                    WHERE  UserID   = @UserID", user);
            }
        }

        // DELETE
        public void DeleteUser(int userId)
        {
            using (var conn = new SQLiteConnection(DBHelper.ConnectionString))
            {
                conn.Execute(@"
                    DELETE FROM UserTable
                    WHERE UserID = @UserID",
                    new { UserID = userId });
            }
        }
    }
}