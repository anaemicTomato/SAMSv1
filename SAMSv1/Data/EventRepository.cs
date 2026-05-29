// Data/EventRepository.cs
using System;
using System.Data.SQLite;

namespace SAMSv1.Data
{
    /// <summary>
    /// Handles all EventsTable operations.
    /// OOP: Inherits BaseRepository (Inheritance),
    ///      private binding (Encapsulation)
    /// </summary>
    public class EventRepository : BaseRepository
    {
        public int Create(string eventName, string eventDate, string startTime)
        {
            const string sql = @"
                INSERT INTO EventsTable (EventName, EventDate, StartTime)
                VALUES (@name, @date, @start);
                SELECT last_insert_rowid();";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@name", eventName);
                cmd.Parameters.AddWithValue("@date", eventDate);
                cmd.Parameters.AddWithValue("@start", startTime);
                return (int)(long)cmd.ExecuteScalar();
            }
        }

        public void Close(int eventId, string endTime)
        {
            const string sql = @"
                UPDATE EventsTable
                SET    EndTime = @end
                WHERE  EventID = @eid";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@end", endTime);
                cmd.Parameters.AddWithValue("@eid", eventId);
                cmd.ExecuteNonQuery();
            }
        }

        public int GetLatestEventId(string eventName, string eventDate)
        {
            const string sql = @"
                SELECT EventID FROM EventsTable
                WHERE  EventName = @name
                  AND  EventDate = @date
                ORDER  BY EventID DESC LIMIT 1";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@name", eventName);
                cmd.Parameters.AddWithValue("@date", eventDate);
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }

        public void Delete(int eventId)
        {
            const string sql = "DELETE FROM EventsTable WHERE EventID = @eid";

            using (var conn = OpenConn())
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@eid", eventId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}