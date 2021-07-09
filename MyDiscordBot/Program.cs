using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<ITokenService, FileTokenService>();
            services.AddTransient<App>();

            return services;
        }
    }
}
