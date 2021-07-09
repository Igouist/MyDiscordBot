using System.Threading.Tasks;

namespace MyDiscordBot.TokenGetter
{
    public interface ITokenService
    {
        /// <summary>
        /// 取得令牌
        /// </summary>
        /// <returns></returns>
        Task<string> Get();
    }
}
