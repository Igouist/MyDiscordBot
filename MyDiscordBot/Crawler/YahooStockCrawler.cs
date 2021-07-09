using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Io;
using MyDiscordBot.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyDiscordBot.Crawler
{
    /// <summary>
    /// Yahoo 奇摩股市
    /// </summary>
    public class YahooStockCrawler : IStockCrawler
    {
        private readonly string _baseUrl = "";

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="baseUrl"></param>
        public YahooStockCrawler(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        /// <summary>
        /// 查詢股票資訊
        /// </summary>
        /// <param name="id">編號 ex: 2330</param>
        /// <returns></returns>
        public async Task<Stock> Fetch(int id)
        {
            var url = $"{this._baseUrl}?s={id}";

            var config = Configuration.Default.WithDefaultLoader(
            new LoaderOptions
            {
                IsResourceLoadingEnabled = true
            });

            var browser = BrowsingContext.New(config);
            var document = await browser.OpenAsync(url);

            if (document is null) return null;

            var tables = document.QuerySelectorAll("table");
            document.Close();

            var elements = tables[2]?.QuerySelectorAll("td");
            var data = elements?.Select(element => Scrubbing(element)).ToList();

            if (data is null) return null;

            return new Stock
            {
                Name = data[0],
                Time = data[1],
                FinalPrice = data[2],
                BuyPrice = data[3],
                SellPrice = data[4],
                UpDown = data[5],
                Lot = data[6],
                YesterdayPrice = data[7],
                OpeningPrice = data[8],
                HighestPrice = data[9],
                LowestPrice = data[10],
            };
        }

        /// <summary>
        /// 整理資料內容
        /// </summary>
        private string Scrubbing(IElement element)
        {
            var html = element.InnerHtml.Replace("加到投資組合", "").Trim();
            return StripTags(html);
        }

        /// <summary>
        /// 移除 HTML 標籤
        /// </summary>
        private static string StripTags(string content) => Regex.Replace(content, "<[^>]*>| ", "");
    }
}
