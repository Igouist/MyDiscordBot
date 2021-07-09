using DSharpPlus;
using DSharpPlus.EventArgs;
using MyDiscordBot.Crawler;
using System;
using System.Threading.Tasks;

namespace MyDiscordBot
{
    /// <summary>
    /// Discord Bot 
    /// </summary>
    public class Bot
    {
        private readonly IStockCrawler _crawler;

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="crawler"></param>
        public Bot(IStockCrawler crawler)
        {
            this._crawler = crawler;
        }

        /// <summary>
        /// 主要 Bot 部份
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task Start(string token)
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

        #region -- Events --

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

        #endregion
    }
}
