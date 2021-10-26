using System.Text.Json.Serialization;

namespace PrayerTimeBot.DTO.TimingsByLL
{
    public class Designation
    {
        [JsonPropertyName("abbreviated")]
        public string Abbreviated { get; set; }

        [JsonPropertyName("expanded")]
        public string Expanded { get; set; }
    }


}