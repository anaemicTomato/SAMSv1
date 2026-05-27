using SAMSv1.Models;
using System.Collections.Generic;

namespace SAMSv1.Services.Interfaces
{
    /// <summary>
    /// Contract for any attendance-capable biometric device.
    /// AttendanceControl depends on this, not on a concrete device class.
    /// </summary>
    public interface IAttendanceDevice
    {
        /// <summary>Connects to the device. Returns true on success.</summary>
        bool Connect();

        /// <summary>Disconnects and releases SDK resources.</summary>
        void Disconnect();

        /// <summary>
        /// Returns the highest serial number currently on the device.
        /// Call this once when live polling starts so we only process NEW events.
        /// </summary>
        long GetLatestSerialNo();

        /// <summary>
        /// Fetches scan events newer than afterSerialNo from the last windowMinutes.
        /// Returns an empty list when there is nothing new.
        /// </summary>
        List<Attendance> PollNewEvents(long afterSerialNo, int windowMinutes = 2);
    }
}