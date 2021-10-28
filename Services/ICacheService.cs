using System.Threading.Tasks;
using PrayerTimeBot.DTO.TimingsByLL;
using PrayerTimeBot.Model;

namespace PrayerTimeBot.Services
{
    public interface ICacheService
    {
        Task<HttpResult<TimingsByLL>> GetOrUpdateTimingAsync(float longitude, float latitude, int date, int bugunmi = 1);
    }
}