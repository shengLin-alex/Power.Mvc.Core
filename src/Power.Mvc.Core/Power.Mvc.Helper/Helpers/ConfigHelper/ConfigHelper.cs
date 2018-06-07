using Power.Mvc.Helper.Extensions;
using System;
using System.Configuration;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// WebConfigHelper
    /// </summary>
    public class ConfigHelper : IConfigHelper
    {
        /// <summary>
        /// 取得 WebConfig 設定值
        /// </summary>
        /// <param name="key">AppSetting key</param>
        /// <param name="defaultValue">預設值</param>
        /// <returns>設定值</returns>
        public string Get(string key, string defaultValue = null)
        {
            return ConfigurationManager.AppSettings[key] ?? defaultValue;
        }

        /// <summary>
        /// 讀取 config 中指定鍵值的設定值內容，完成轉型傳回指定型別，無預設值
        /// </summary>
        /// <typeparam name="T">取回的內容完轉換成該型別</typeparam>
        /// <param name="key">鍵值</param>
        /// <returns>轉型後內容</returns>
        public T Get<T>(string key) where T : IConvertible
        {
            string value = this.Get(key);

            return value == null ? default(T) : (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// 讀取 config 中指定鍵值的設定值內容，完成轉型傳回指定型別，可帶入第二參數 defaultValue 作為預設值
        /// </summary>
        /// <typeparam name="T">取回的內容完轉換成該型別</typeparam>
        /// <param name="key">鍵值</param>
        /// <param name="defaultValue">預設值，該無鍵值則回傳預設值</param>
        /// <returns>轉型後內容</returns>
        public T Get<T>(string key, T defaultValue) where T : IConvertible
        {
            string value = this.Get(key);

            if (value.IsNullOrEmpty())
            {
                return defaultValue;
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}