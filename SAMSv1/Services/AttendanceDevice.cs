using SAMSv1.Models;
using System.Collections.Generic;

namespace SAMSv1.Services
{
    public abstract class AttendanceDevice
    {
        protected readonly string Ip;
        protected readonly int Port;
        protected readonly string User;
        protected readonly string Pass;

        protected AttendanceDevice(string ip, int port, string user, string pass)
        {
            Ip = ip; Port = port; User = user; Pass = pass;
        }

        // ── Attendance (existing) ─────────────────────────────────
        public abstract bool Connect();
        public abstract void Disconnect();
        public abstract long GetLatestSerialNo();

        public abstract bool DeleteStudent(string idNumber);
        public abstract List<Attendance> PollNewEvents(
            long afterSerialNo, int windowMinutes = 2);

        // ── Student Management (new) ──────────────────────────────

        /// <summary>
        /// Fetches all users enrolled on the physical device.
        /// ABSTRACTION: callers don't know how — Hikvision uses ISAPI,
        /// another brand might use a different protocol.
        /// </summary>
        public abstract List<DeviceStudent> GetEnrolledStudents();

        /// <summary>
        /// Pushes a new student record to the physical device so they
        /// can scan their face/card at the terminal.
        /// Returns true if the device accepted the registration.
        /// </summary>
        public abstract bool RegisterStudent(string idNumber, string fullName);

    }
}