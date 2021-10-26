using System.Text.Json.Serialization;

namespace PrayerTimeBot.DTO.TimingsByLL
{
    public class Weekday
    {
        [JsonPropertyName("en")]
        public string En { get; set; }

        [JsonPropertyName("ar")]
        public string Ar { get; set; }
    }


}