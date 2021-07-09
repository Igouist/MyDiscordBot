namespace MyDiscordBot.Models
{
    /// <summary>
    /// 股票資訊
    /// </summary>
    public class Stock
    {
        /// <summary>
        /// 股票名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 時間
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 成交價
        /// </summary>
        public string FinalPrice { get; set; }

        /// <summary>
        /// 買進
        /// </summary>
        public string BuyPrice { get; set; }

        /// <summary>
        /// 賣出
        /// </summary>
        public string SellPrice { get; set; }

        /// <summary>
        /// 漲跌
        /// </summary>
        public string UpDown { get; set; }

        /// <summary>
        /// 張數
        /// </summary>
        public string Lot { get; set; }

        /// <summary>
        /// 前日收盤價
        /// </summary>
        public string YesterdayPrice { get; set; }

        /// <summary>
        /// 今日開盤價
        /// </summary>
        public string OpeningPrice { get; set; }

        /// <summary>
        /// 最高價
        /// </summary>
        public string HighestPrice { get; set; }

        /// <summary>
        /// 最低價
        /// </summary>
        public string LowestPrice { get; set; }
    }
}
