using System;

namespace Power.Mvc.Helper.Extensions
{
    /// <summary>
    /// 快取擴充方法
    /// </summary>
    public static class CacheHelperExtension
    {
        /// <summary>
        /// 取得或依據委派初始化快取內容，存活時間預設 60秒
        /// </summary>
        /// <typeparam name="T">快取物件型別</typeparam>
        /// <param name="cacheHelper">CacheHelper</param>
        /// <param name="key">快取鍵值</param>
        /// <param name="acquire">初始化的快取物件委派</param>
        /// <returns>快取的物件</returns>
        public static T Get<T>(this ICacheHelper cacheHelper, string key, Func<T> acquire)
        {
            return Get(cacheHelper, key, 60, acquire);
        }

        /// <summary>
        /// 取得或依據委派初始化快取內容, 設定的快取內容存留時間
        /// </summary>
        /// <typeparam name="T">快取物件型別</typeparam>
        /// <param name="cacheHelper">CacheHelper</param>
        /// <param name="key">快取鍵值</param>
        /// <param name="cacheSeconds">快取存活時間</param>
        /// <param name="acquire">初始化的快取物件委派</param>
        /// <returns>快取的物件</returns>
        public static T Get<T>(this ICacheHelper cacheHelper, string key, int cacheSeconds, Func<T> acquire)
        {
            if (cacheHelper.IsSet(key))
            {
                return cacheHelper.Get<T>(key);
            }

            T result = acquire();

            cacheHelper.Set(key, result, cacheSeconds);

            return result;
        }
    }
}