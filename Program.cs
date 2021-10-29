using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddSingleton<TelegramBotClient>(b => new TelegramBotClient("2047680080:AAEw3tZCtF0FOpLu3VY5tAP5dcsMHtpEc3k"));
            services.AddHostedService<Bot>();
            services.AddTransient<HttpClientService>();
            services.AddTransient<IStorageService, InternalStorageService>();
            services.AddTransient<TimingsByLLCache>();
            services.AddTransient<TimingsByLLService>();
            services.AddTransient<Handlers>();
        }
    }
}