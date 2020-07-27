using System;
namespace HackATL_Server.NotificationHub
{
    public class Notification : DeviceRegistration
    {
        public string Tag { get; set; }

        public string Content { get; set; }
    }
}
