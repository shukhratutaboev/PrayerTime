using System.Text.Json.Serialization;

namespace PrayerTimeBot.DTO.CurrentTime
{
    public class CurrentTime
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }
    }


}