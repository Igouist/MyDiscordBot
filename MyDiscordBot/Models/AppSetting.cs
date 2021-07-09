namespace MyDiscordBot.Models
{
    /// <summary>
    /// 設定檔資訊
    /// </summary>
    public class AppSetting
    {
        /// <summary>
        /// 令牌檔案路徑
        /// </summary>
        public string TokenFile { get; set; }

        /// <summary>
        /// 爬蟲預設路徑
        /// </summary>
        public string CrawlerUrl { get; set; }
    }
}
