namespace SAMSv1.Services
{
    /// <summary>
    /// Holds the single shared AttendanceDevice instance for the application.
    /// Both AttendanceControl and RegisterStudentsV2 use this — one connection,
    /// no duplicate SDK logins.
    /// 
    /// ENCAPSULATION:  _device is private; outside code only calls Initialize()
    ///                 and reads the Device property.
    /// POLYMORPHISM:   Device is typed as AttendanceDevice (abstract base),
    ///                 not HikvisionDevice — callers don't know or care which brand.
    /// </summary>
    public static class DeviceManager
    {
        private static AttendanceDevice _device;

        public static AttendanceDevice Device => _device;

        /// <summary>
        /// Call once at app startup (e.g. in your MainForm or Program.cs).
        /// Pass in a fully constructed HikvisionDevice (or any future device).
        /// </summary>
        public static bool Initialize(AttendanceDevice device)
        {
            _device = device;
            return _device.Connect();
        }

        public static void Shutdown()
        {
            _device?.Disconnect();
            _device = null;
        }
    }
}