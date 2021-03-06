using System;
using System.Threading.Tasks;
using PrayerTimeBot.DTO.TimingsByLL;
using PrayerTimeBot.Services;
using Microsoft.Extensions.Caching.Memory;
using PrayerTimeBot.Model;

namespace PrayerTime.Services
{
    public class TimingsByLLCache : ICacheService
    {
        private readonly TimingsByLLService _client;
        private readonly IMemoryCache _memCache;

        public TimingsByLLCache(IMemoryCache memCache, TimingsByLLService client)
        {
            _client = client;
            _memCache = memCache;
        }
        public async Task<HttpResult<TimingsByLL>> GetOrUpdateTimingAsync(float longitude, float latitude, int date, int bugunmi=1)
        {
            var key = string.Format($"{longitude}:{latitude}:{date}");
            return await _memCache.GetOrCreateAsync(key, async entry => 
            {
                var result = await _client.getTimings(longitude, latitude, bugunmi);
                var zone = result.Data.Data.Meta.Timezone;
                var zoneId = TimeZoneInfo.FindSystemTimeZoneById(zone);
                var expirationTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.Parse("23:59:59"), zoneId);
                entry.AbsoluteExpiration = expirationTime;
                return result;
            });
        }
        public async Task<string> getTodayTimings(float longitude, float latitude, string lan)
        {
            var result = await GetOrUpdateTimingAsync(longitude, latitude, DateTime.UtcNow.Day);
            if(result.IsSuccess && result != null)
            {
                return ($"{Language.today(lan)}: {(result.Data.Data.Date.Gregorian.Date).Replace("-", ".")}\n\n",
                        $"{Language.fajr(lan)}: *{result.Data.Data.Timings.Fajr}*\n",
                        $"{Language.sunrise(lan)}: *{result.Data.Data.Timings.Sunrise}*\n",
                        $"{Language.dhuhr(lan)}: *{result.Data.Data.Timings.Dhuhr}*\n",
                        $"{Language.asr(lan)}: *{result.Data.Data.Timings.Asr}*\n",
                        $"{Language.maghrib(lan)}: *{result.Data.Data.Timings.Maghrib}*\n",
                        $"{Language.isha(lan)}: *{result.Data.Data.Timings.Isha}*\n\n",
                        "@muslimprayertimebot"
                        ).ToString().Replace(",", "").Replace("(", "").Replace(")", "");
            }
            else
            {
                return "We can't connect to API.";
            }
        }
        public async Task<string> getTomorrowTimings(float longitude, float latitude, string lan)
        {
            var result = await GetOrUpdateTimingAsync(longitude, latitude, DateTime.UtcNow.AddDays(1).Day, 0);
            if(result.IsSuccess && result != null)
            {
                return ($"{Language.tomorrow(lan)}: {(result.Data.Data.Date.Gregorian.Date).Replace("-", ".")}\n\n",
                        $"{Language.fajr(lan)}: *{result.Data.Data.Timings.Fajr}*\n",
                        $"{Language.sunrise(lan)}: *{result.Data.Data.Timings.Sunrise}*\n",
                        $"{Language.dhuhr(lan)}: *{result.Data.Data.Timings.Dhuhr}*\n",
                        $"{Language.asr(lan)}: *{result.Data.Data.Timings.Asr}*\n",
                        $"{Language.maghrib(lan)}: *{result.Data.Data.Timings.Maghrib}*\n",
                        $"{Language.isha(lan)}: *{result.Data.Data.Timings.Isha}*\n\n",
                        "@muslimprayertimebot"
                        ).ToString().Replace(",", "").Replace("(", "").Replace(")", "");
            }
            else
            {
                return "We can't connect to API.";
            }
        }
        public async Task<string> getTimeZone(float longitude, float latitude)
        {
            var result = await GetOrUpdateTimingAsync(longitude, latitude, DateTime.UtcNow.Day);
            return result.Data.Data.Meta.Timezone;
        }
        public async Task<string> getWeekday(float longitude, float latitude)
        {
            var result = await GetOrUpdateTimingAsync(longitude, latitude, DateTime.UtcNow.Day);
            return result.Data.Data.Date.Gregorian.Weekday.En;
        }
    }
}