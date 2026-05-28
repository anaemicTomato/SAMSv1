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
    // Services/DeviceManager.cs
    public static class DeviceManager
    {
        // ENCAPSULATION: credentials are private — no UI control ever sees them
        private const string IP = "192.168.1.65";
        private const int Port = 8000;
        private const string User = "admin";
        private const string Pass = "DMC2026#";

        private static AttendanceDevice _device;

        // POLYMORPHISM: typed as abstract — callers don't know it's Hikvision
        public static AttendanceDevice Device => _device;

        public static bool Initialize()
        {
            _device = new HikvisionDevice(IP, Port, User, Pass);
            return _device.Connect();
        }

        public static void Disconnect()
        {
            _device?.Disconnect();
            _device = null;
        }

        public static void Shutdown()
        {
            _device?.Disconnect();
            HikvisionDevice.SDKCleanup(); // only call once on exit
            _device = null;
        }
    }
}