using DSharpPlus;
using Microsoft.Extensions.DependencyInjection;
using MyDiscordBot.TokenGetter;
using System;
using System.Threading.Tasks;

namespace MyDiscordBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var service = new ServiceCollection();
            Startup(service);

            var provider = service.BuildServiceProvider();
            await provider.GetRequiredService<App>().Run();
        }

        static void Startup(ServiceCollection services)
        {
            services.AddScoped<ITokenService, FileTokenService>();
            services.AddTransient<App>();
        }
    }
}
