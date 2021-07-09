using MyDiscordBot.Models;
using System.Threading.Tasks;

namespace MyDiscordBot.Crawler
{
    /// <summary>
    /// 股票爬蟲
    /// </summary>
    public interface IStockCrawler
    {
        /// <summary>
        /// 查詢股票資訊
        /// </summary>
        /// <param name="id">編號 ex: 2330</param>
        /// <returns></returns>
        Task<Stock> Fetch(int id);
    }
}
