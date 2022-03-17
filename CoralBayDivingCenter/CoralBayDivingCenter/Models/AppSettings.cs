namespace CoralBayDivingCenter.Models
{
    public class AppSettings
    {
        public DeviceSettings DeviceSettings { get; set; }
        public bool IsLoggedIn { get; set; }
    }
    public class DeviceSettings
    {
        public string DeviceUdid { get; set; }

        public string FcmIdentifire { get; set; }

        public string OneSignalIdentifire { get; set; }
    }
}
