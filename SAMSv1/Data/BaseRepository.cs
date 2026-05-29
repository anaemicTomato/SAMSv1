// Data/BaseRepository.cs
using System.Data.SQLite;

namespace SAMSv1.Data
{
    /// <summary>
    /// OOP: Abstraction (abstract class), Inheritance (all repos extend this)
    /// Uses DBHelper — no duplicate connection string logic.
    /// </summary>
    public abstract class BaseRepository
    {
        // Just delegates to your existing DBHelper
        protected SQLiteConnection OpenConn() => DBHelper.GetConnection();

        protected string SafeString(SQLiteDataReader r, int i)
            => r.IsDBNull(i) ? string.Empty : r.GetString(i);

        protected int SafeInt(SQLiteDataReader r, int i)
            => r.IsDBNull(i) ? 0 : r.GetInt32(i);
    }
}