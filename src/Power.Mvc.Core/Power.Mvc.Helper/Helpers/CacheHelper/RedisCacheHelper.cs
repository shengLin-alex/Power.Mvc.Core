using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Power.Mvc.Helper.Extensions;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Net;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 實作 Redis 快取
    /// </summary>
    public class RedisCacheHelper : ICacheHelper
    {
        /// <summary>
        /// 鎖定，用於保護共用變數不被其他執行序干擾
        /// </summary>
        private static readonly object SyncRoot = new object();

        /// <summary>
        /// Redis 連線個體，使用延遲載入
        /// </summary>
        private static Lazy<ConnectionMultiplexer> RedisConnectionLazy;

        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<RedisCacheHelper> Logger;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="logger">Logger</param>
        public RedisCacheHelper(ILogger<RedisCacheHelper> logger)
        {
            this.Logger = logger;
        }

        /// <summary>
        /// 組態設定
        /// </summary>
        private static IOptions<RedisConfig> RedisConfig => PackageDiResolver.Current.GetService<IOptions<RedisConfig>>();

        /// <summary>
        /// 清除所有快取
        /// </summary>
        public void Clear()
        {
            EndPoint[] endPoints = GetConnection().GetEndPoints(true);

            foreach (EndPoint endPoint in endPoints)
            {
                IServer server = GetConnection().GetServer(endPoint);
                server.FlushDatabase(this.RedisDb().Database);
            }
        }

        /// <summary>
        /// 取得快取內容
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public T Get<T>(string key)
        {
            RedisValue cacheData = this.RedisDb().StringGetAsync(key).Result;

            try
            {
                if (cacheData.IsNullOrEmpty)
                {
                    return default(T);
                }

                if (typeof(T) == typeof(string) || !typeof(T).IsClass)
                {
                    return (T)Convert.ChangeType(cacheData.ToString(), typeof(T));
                }

                return cacheData.ToString().ToTypedObject<T>();
            }
            catch (Exception exception)
            {
                this.Logger.LogError(exception, exception.Message);

                return default(T);
            }
        }

        /// <summary>
        /// 確定快取鍵值是否存在
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>Result</returns>
        public bool IsSet(string key) => this.RedisDb().KeyExists(key);

        /// <summary>
        /// Removes the value with the specified key from the cache 移除快取內容
        /// </summary>
        /// <param name="key">The key of the value to remove.</param>
        public void Remove(string key) => this.RedisDb().KeyDelete(key);

        /// <summary>
        /// 依據鍵值樣式移除快取內容
        /// </summary>
        /// <param name="pattern">
        /// h?llo matches hello, hallo and hxllo h*lo matches hllo and heeeello h[ae]llo matches
        /// hello and hallo, but not hillo h[^e]llo matches hallo, hbllo, ... but not hello h[a-b]llo
        /// matches hallo and hbllo
        /// </param>
        public void RemoveByPattern(string pattern)
        {
            List<string> keysToRemove = new List<string>();
            EndPoint[] endpoints = GetConnection().GetEndPoints(true);

            foreach (EndPoint endpoint in endpoints)
            {
                IServer server = GetConnection().GetServer(endpoint);

                // 取得設定的 Database 中的 Keys
                foreach (RedisKey item in server.Keys(this.RedisDb().Database, pattern))
                {
                    keysToRemove.Add(item);
                }
            }

            foreach (string key in keysToRemove)
            {
                this.Remove(key);
            }
        }

        /// <summary>
        /// 指定快取內容
        /// </summary>
        /// <param name="key">The key of the value to set.</param>
        /// <param name="data">The value associated with the specified key.</param>
        /// <param name="cacheSeconds">Cache time (seconds)</param>
        public void Set(string key, object data, int cacheSeconds)
        {
            if (data == null)
            {
                return;
            }

            if (data is string || !data.GetType().IsClass)
            {
                this.RedisDb().StringSet(key, data.ToString(), TimeSpan.FromSeconds(cacheSeconds));
                return;
            }

            string cacheData = data.ToJson();
            this.RedisDb().StringSet(key, cacheData, TimeSpan.FromSeconds(cacheSeconds));
        }

        /// <summary>
        /// 建立 Redis 連線個體
        /// </summary>
        /// <exception cref="AggregateException"></exception>
        /// <returns>Redis 連線個體</returns>
        private static ConnectionMultiplexer GetConnection()
        {
            if (RedisConnectionLazy == null || !RedisConnectionLazy.IsValueCreated || !RedisConnectionLazy.Value.IsConnected)
            {
                lock (SyncRoot)
                {
                    try
                    {
                        string redisConnectionString = RedisConfig.Value.RedisConnection;
                        ConfigurationOptions option = ConfigurationOptions.Parse(redisConnectionString);
                        RedisConnectionLazy = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(option));
                    }
                    catch (Exception exception)
                    {
                        throw new AggregateException($"Exception: {exception} Message: {exception.Message}; Redis 連線錯誤");
                    }
                }
            }

            return RedisConnectionLazy.Value;
        }

        /// <summary>
        /// 取得 Redis 資料庫 (0~15)
        /// </summary>
        /// <param name="database">db 編號</param>
        /// <exception cref="AggregateException"></exception>
        /// <returns>redis db</returns>
        private IDatabase RedisDb(int database = -1)
        {
            int configDb = RedisConfig.Value.RedisDefaultDb;
            if (database == -1 && configDb > -1)
            {
                database = configDb;
            }

            if (database < -1 || database > 15)
            {
                throw new AggregateException("Redis Database 設定為 0 ~ 15");
            }

            return GetConnection().GetDatabase(database);
        }
    }
}