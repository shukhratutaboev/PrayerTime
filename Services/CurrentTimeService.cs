using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PrayerTimeBot.DTO.CurrentTime;
using PrayerTimeBot.Model;

namespace PrayerTimeBot.Services
{
    public class CurrentTimeService
    {
        private readonly HttpClientService _httpService = new HttpClientService();
        private readonly ILogger<CurrentTimeService> _logger;
        public CurrentTimeService(ILogger<CurrentTimeService> logger)
        {
            _logger = logger;
        }
        public async Task<DateTime> getCurrentTime(string timezone)
        {
            var currentTimeApi = $"https://api.aladhan.com/v1/currentTime?zone={timezone}";
            var result = await _httpService.GetObjectAsync<CurrentTime>(currentTimeApi);
            if(result.IsSuccess)
            {
                var settings = new JsonSerializerOptions()
                {
                    WriteIndented = true
                };
                _logger.LogInformation("Current time is successfully recieved.");
                return DateTime.Parse(result.Data.Data);
            }
            else
            {
                _logger.LogCritical("We can't connect to API.");
                return DateTime.Now;
            }
        }
    }
}