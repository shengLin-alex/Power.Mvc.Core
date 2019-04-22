using Microsoft.Extensions.Configuration;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// appsetting.json 設定值讀取程式之介面
    /// </summary>
    public class SettingHelper : ISettingHelper
    {
        /// <summary>
        /// 設定組態
        /// </summary>
        private readonly IConfiguration Configuration;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="configuration">設定組態</param>
        public SettingHelper(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// 取得連線字串
        /// </summary>
        /// <param name="connectionKey">連線字串鍵值</param>
        /// <returns>連線字串</returns>
        public string GetConnectionString(string connectionKey)
        {
            return this.Configuration.GetConnectionString(connectionKey);
        }

        /// <summary>
        /// 取得強型別設定組態
        /// </summary>
        /// <typeparam name="TSection">組態實際型別</typeparam>
        /// <param name="sectionName">組態 section 名稱</param>
        /// <returns>強型別設定組態</returns>
        public TSection GetSection<TSection>(string sectionName)
        {
            return this.Configuration.GetSection(sectionName).Get<TSection>();
        }
    }
}