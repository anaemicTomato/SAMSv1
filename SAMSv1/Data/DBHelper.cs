using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace SAMSv1.Data
{
    public static class DBHelper
    {
        private static readonly string DataFolder = Path.Combine(
            GetProjectRoot(), "Data"
        );

        private static readonly string TemplateDb = Path.Combine(DataFolder, "sams_template.db");
        private static readonly string LocalDb = Path.Combine(DataFolder, "sams.db");

        private static string GetProjectRoot()
        {
            var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            while (directory != null)
            {
                if (directory.GetFiles("*.csproj").Length > 0)
                    return directory.FullName;

                directory = directory.Parent;
            }

            throw new DirectoryNotFoundException("Could not locate project root.");
        }

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

            // Compare schema versions instead of timestamps
            if (GetSchemaVersion(TemplateDb) > GetSchemaVersion(LocalDb))
            {
                File.Delete(LocalDb);
                File.Copy(TemplateDb, LocalDb);
            }

            return LocalDb;
        }

        private static int GetSchemaVersion(string dbPath)
        {
            using (var conn = new SqliteConnection($"Data Source={dbPath}"))
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
            return $"Data Source={GetProjectDbPath()}";
        }
    }
}