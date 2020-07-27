using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HackATL_Server.NotificationHub
{
    public class DeviceRegistration
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public MobilePlatform Platform { get; set; }
        public string Handle { get; set; }
        public string[] Tags { get; set; }
    }
}
