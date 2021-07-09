using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyDiscordBot.Crawler;
using MyDiscordBot.Models;
using MyDiscordBot.TokenGetter;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading.Tasks;

namespace MyDiscordBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await
                 ConfigureServices(new ServiceCollection())
                .BuildServiceProvider()
                .GetRequiredService<App>()
                .Run();
        }

        static ServiceCollection ConfigureServices(ServiceCollection services)
        {
            var appSetting = JObject.Parse(File.ReadAllText(@"appsettings.json")).ToObject<AppSetting>();
            var tokenFile = appSetting.TokenFile;
            var crawlerUrl = appSetting.CrawlerUrl;

            services.AddScoped<ITokenService, FileTokenService>(sp => new FileTokenService(tokenFile));
            services.AddScoped<IStockCrawler, YahooStockCrawler>(sp => new YahooStockCrawler(crawlerUrl));
            services.AddSingleton<Bot>();
            services.AddTransient<App>();
            return services;
        }
    }
}
