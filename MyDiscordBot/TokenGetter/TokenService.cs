﻿using System.IO;
using System.Threading.Tasks;

namespace MyDiscordBot.TokenGetter
{
    /// <summary>
    /// 檔案令牌服務
    /// </summary>
    public class FileTokenService : ITokenService
    {
        private readonly string _filePath = @"D:\DiscordToken.txt";

        /// <summary>
        /// 取得令牌
        /// </summary>
        /// <returns></returns>
        public async Task<string> Get()
        {
            if (File.Exists(this._filePath) is false)
            {
                return string.Empty;
            }

            string token = await File.ReadAllTextAsync(this._filePath);
            return token;
        }
    }
}
