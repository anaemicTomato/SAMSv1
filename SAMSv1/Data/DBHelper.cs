using System;
using System.IO;

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
                throw new FileNotFoundException("Template database not found. Cannot create runtime DB.");

            // No local DB yet — copy template
            if (!File.Exists(LocalDb))
            {
                File.Copy(TemplateDb, LocalDb);
                return LocalDb;
            }

            // Template is newer than local DB — replace it
            var templateModified = File.GetLastWriteTime(TemplateDb);
            var localModified = File.GetLastWriteTime(LocalDb);

            if (templateModified > localModified)
            {
                File.Delete(LocalDb);
                File.Copy(TemplateDb, LocalDb);
            }

            return LocalDb;
        }

        public static string GetConnectionString()
        {
            return $"Data Source={GetProjectDbPath()}";
        }
    }
}