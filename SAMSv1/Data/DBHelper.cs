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

        public static string GetConnectionString()
        {
            return $"Data Source={DbPath};Version=3;";
        }
    }
}