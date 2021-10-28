using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PrayerTime.Services;
using PrayerTimeBot.DTO.TimingsByLL;
using PrayerTimeBot.Model;

namespace PrayerTimeBot.Services
{
    public class TimingsByLLService
    {
        private readonly HttpClientService _httpService;
        private readonly ILogger<TimingsByLLService> _logger;
        // private readonly ICacheService _memCache;
        public TimingsByLLService(ILogger<TimingsByLLService> logger, HttpClientService httpService/*,ICacheService memCache*/)
        {
            _logger = logger;
            _httpService = httpService;
            // _memCache = memCache;
        }
        private async Task<HttpResult<TimingsByLL>> _getResult(string time, float longitude, float latitude)
        {
            var timingsByLLApi = $"https://api.aladhan.com/v1/timings/{time}?latitude={latitude}&longitude={longitude}&method=14&school=1";
            var result = await _httpService.GetObjectAsync<TimingsByLL>(timingsByLLApi);
            if(result.IsSuccess)
            {
                var settings = new JsonSerializerOptions()
                {
                    WriteIndented = true
                };
                _logger.LogInformation("Timing is successfully recieved.");
                return result;
            }
            else
            {
                _logger.LogCritical("We can't connect to API.");
                return null;
            }
        }
        public async Task<HttpResult<TimingsByLL>> getTimings(float longitude, float latitude, int bugunmi = 1)
        {
            HttpResult<TimingsByLL> result;
            if(bugunmi == 1)
            {
                result = await _getResult(DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() , longitude, latitude);
            }
            else
            {
                result = await _getResult(DateTimeOffset.UtcNow.AddDays(1).ToUnixTimeSeconds().ToString(), longitude, latitude);
            }
            return result;
        }
    }
}