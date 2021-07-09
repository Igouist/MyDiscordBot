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
        private readonly Bot _bot;
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Init
        /// </summary>
        public App(
            Bot bot,
            ITokenService tokenService)
        {
            this._bot = bot;
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
            if (string.IsNullOrWhiteSpace(token))
            {
                Console.WriteLine($"token 取得失敗，Bot 已關閉");
                return;
            }
            Console.WriteLine($"已取得 Token:{token}");

            Console.WriteLine($"正在連接 Discord...");
            await this._bot.Start(token);

            Console.WriteLine($"Bot 已關閉");
        }
    }
}
