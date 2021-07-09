using Microsoft.Extensions.DependencyInjection;
using MyDiscordBot.Crawler;
using MyDiscordBot.TokenGetter;
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
            var crawlerUrl = "https://tw.stock.yahoo.com/q/q";

            services.AddScoped<ITokenService, FileTokenService>();
            services.AddScoped<IStockCrawler, YahooStockCrawler>(sp => new YahooStockCrawler(crawlerUrl));
            services.AddSingleton<Bot>();
            services.AddTransient<App>();
            return services;
        }
    }
}
