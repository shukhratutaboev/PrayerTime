using PrayerTime.Services;
using PrayerTimeBot.Services;
using Telegram.Bot;

namespace PrayerTimeBot
{
    class Program
    {
        static Task Main(string[] args)
            => CreateHostBuilder(args)
            .Build()
            .RunAsync();
        private static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureServices(Configure);
        private static void Configure(HostBuilderContext context, IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<TelegramBotClient>(b => new TelegramBotClient("token"));
            services.AddHostedService<Bot>();
            services.AddTransient<HttpClientService>();
            services.AddTransient<IStorageService, InternalStorageService>();
            services.AddTransient<TimingsByLLCache>();
            services.AddTransient<TimingsByLLService>();
            services.AddTransient<Handlers>();
        }
    }
}