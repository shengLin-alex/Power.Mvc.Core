namespace Power.Mvc.Helper
{
    /// <summary>
    /// 無快取(不使用快取)
    /// </summary>
    public class NullCacheHelper : ICacheHelper
    {
        /// <summary>
        /// 清除所有快取
        /// </summary>
        public void Clear()
        {
        }

        /// <summary>
        /// 取得快取內容
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public T Get<T>(string key) => default(T);

        /// <summary>
        /// 確定快取鍵值是否存在
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>Result</returns>
        public bool IsSet(string key) => false;

        /// <summary>
        /// Removes the value with the specified key from the cache 移除快取內容
        /// </summary>
        /// <param name="key">The key of the value to remove.</param>
        public void Remove(string key)
        {
        }

        /// <summary>
        /// 依據鍵值樣式移除快取內容
        /// </summary>
        /// <param name="pattern">pattern</param>
        public void RemoveByPattern(string pattern)
        {
        }

        /// <summary>
        /// 指定快取內容
        /// </summary>
        /// <param name="key">The key of the value to set.</param>
        /// <param name="data">The value associated with the specified key.</param>
        /// <param name="cacheSeconds">Cache time (seconds)</param>
        public void Set(string key, object data, int cacheSeconds)
        {
        }
    }
}