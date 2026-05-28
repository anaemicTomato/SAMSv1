// Models/DeviceStudent.cs
namespace SAMSv1.Models
{
    /// <summary>
    /// Represents a user fetched from the Hikvision device's
    /// UserInfo/Search ISAPI endpoint.
    /// INHERITANCE: Extends PersonBase — it's still a person, just from the device.
    /// </summary>
    public class DeviceStudent : PersonBase
    {
        public string CardNo { get; private set; }

        public DeviceStudent(string idNumber, string fullName, string cardNo)
        {
            IdNumber = idNumber;
            FullName = fullName;
            CardNo = cardNo;
        }
    }
}