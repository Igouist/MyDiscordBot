using DSharpPlus;
using DSharpPlus.EventArgs;
using MyDiscordBot.Crawler;
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
        private IStockCrawler _crawler;
        private ITokenService _tokenService;

        /// <summary>
        /// Init
        /// </summary>
        public App(
            IStockCrawler crawler,
            ITokenService tokenService)
        {
            this._crawler = crawler;
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
        private async Task MainAsync(string token)
        {
            // 參考資料: https://dsharpplus.github.io/articles/basics/first_bot.html
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = token,
                TokenType = TokenType.Bot
            });

            discord.MessageCreated += FetchStockInfo;

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }

        /// <summary>
        /// 查詢股票功能
        /// </summary>
        /// <param name="client"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private async Task FetchStockInfo(DiscordClient client, MessageCreateEventArgs args)
        {
            if (args.Message.Content.ToLower().StartsWith("getstock"))
            {
                var content = args.Message.Content;
                var inputs = content.Split(':');

                if (inputs.Length > 1 && Int16.TryParse(inputs[1], out var idx))
                {
                    var stock = await this._crawler.Fetch(idx);
                    var rn = Environment.NewLine;

                    var message =
                        $"名稱: {stock.Name}{rn}" +
                        $"成交: {stock.FinalPrice}{rn}" +
                        $"買進: {stock.BuyPrice}{rn}" +
                        $"賣出: {stock.SellPrice}{rn}" +
                        $"漲跌: {stock.UpDown.Trim()}{rn}" +
                        $"張數: {stock.Lot}{rn}" +
                        $"昨收: {stock.YesterdayPrice}{rn}" +
                        $"開盤: {stock.OpeningPrice}{rn}" +
                        $"最高: {stock.HighestPrice}{rn}" +
                        $"最低: {stock.LowestPrice}{rn}";

                    await args.Message.RespondAsync(message);
                }
            }
        }
    }
}
