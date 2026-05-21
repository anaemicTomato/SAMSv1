using System;
using System.Data.SQLite;
using System.IO;

namespace SAMSv1.Data
{
    public static class DBHelper
    {
        private static readonly string DbPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "Data", "sams.db"
        );

        // ── Add this ──
        public static readonly string ConnectionString =
            $"Data Source={DbPath};Version=3;";

        public static SQLiteConnection GetConnection()
        {
            var conn = new SQLiteConnection(ConnectionString);
            conn.Open();
            return conn;
        }

        public static string GetConnectionString() => ConnectionString;
    }
}