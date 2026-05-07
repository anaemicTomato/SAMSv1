using System;
using System.Data.SQLite;
using System.IO;

namespace SAMSv1.Data
{
    public static class DBHelper
    {
        private static readonly string DataFolder = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "Data"
        );
        private static readonly string TemplateDb = Path.Combine(DataFolder, "samsDB_Template.db");
        private static readonly string LocalDb = Path.Combine(DataFolder, "sams.db");

        public static string GetProjectDbPath()
        {
            Directory.CreateDirectory(DataFolder);

            if (!File.Exists(TemplateDb))
                throw new FileNotFoundException("Template database not found.");

            if (!File.Exists(LocalDb))
            {
                File.Copy(TemplateDb, LocalDb);
                return LocalDb;
            }

            if (GetSchemaVersion(TemplateDb) > GetSchemaVersion(LocalDb))
            {
                File.Delete(LocalDb);
                File.Copy(TemplateDb, LocalDb);
            }

            return LocalDb;
        }

        private static int GetSchemaVersion(string dbPath)
        {
            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "PRAGMA user_version";
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static string GetConnectionString()
        {
            return $"Data Source={GetProjectDbPath()};Version=3;";
        }
    }
}