namespace SAMSv1.Models
{
    /// <summary>
    /// Represents a user record returned by the Hikvision device's
    /// UserInfo/Search ISAPI endpoint.
    /// IdNumber = device's employeeNo field.
    /// </summary>
    public class DeviceStudent
    {
        public string IdNumber { get; private set; }
        public string FullName { get; private set; }
        public string CardNo { get; private set; }

        public DeviceStudent(string idNumber, string fullName, string cardNo)
        {
            IdNumber = idNumber;
            FullName = fullName;
            CardNo = cardNo;
        }
    }
}