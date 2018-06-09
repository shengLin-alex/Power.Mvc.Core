namespace Power.Mvc.Helper
{
    /// <summary>
    /// appsetting.json 設定值讀取程式之介面
    /// </summary>
    public interface ISettingHelper
    {
        /// <summary>
        /// 取得連線字串
        /// </summary>
        /// <param name="connectionKey">連線字串鍵值</param>
        /// <returns>連線字串</returns>
        string GetConnectionString(string connectionKey);

        /// <summary>
        /// 取得強型別設定組態
        /// </summary>
        /// <typeparam name="TSection">組態實際型別</typeparam>
        /// <param name="sectionName">組態 section 名稱</param>
        /// <returns>強型別設定組態</returns>
        TSection GetSection<TSection>(string sectionName);
    }
}