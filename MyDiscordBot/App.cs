using DSharpPlus;
using MyDiscordBot.TokenGetter;
using System;
using System.Threading.Tasks;

namespace MyDiscordBot
{
    /// <summary>
    /// 主要應用程式
    /// </summary>
    public class App
    {
        private ITokenService _tokenService;

        /// <summary>
        /// Init
        /// </summary>
        public App(ITokenService tokenService)
        {
            this._tokenService = tokenService;
        }

        /// <summary>
        /// 主流程
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            Console.WriteLine($"正在啟動 Bot...");
            Console.WriteLine($"正在取得 Token...");

            var token = await _tokenService.Get();

            Console.WriteLine($"已取得 Token:{token}");

            if (string.IsNullOrWhiteSpace(token))
            {
                return;
            }

            Console.WriteLine($"正在連接 Discord...");

            await MainAsync(token);

            Console.WriteLine($"Bot 已關閉");
        }


        /// <summary>
        /// 主要 Bot 部份
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        static async Task MainAsync(string token)
        {
            // 參考資料: https://dsharpplus.github.io/articles/basics/first_bot.html
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = token,
                TokenType = TokenType.Bot
            });

            discord.MessageCreated += async (s, e) =>
            {
                if (e.Message.Content.ToLower().StartsWith("test"))
                {
                    await e.Message.RespondAsync("測你娘啦");
                }
            };

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
