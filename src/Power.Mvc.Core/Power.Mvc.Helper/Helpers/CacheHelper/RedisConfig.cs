namespace Power.Mvc.Helper
{
    /// <summary>
    /// Redis 快取組態設定 Usage : services.Configure
    /// </summary>
    public class RedisConfig
    {
        /// <summary>
        /// 連線字串
        /// </summary>
        public string RedisConnection { get; set; }

        /// <summary>
        /// 預設 Db 編號
        /// </summary>
        public int RedisDefaultDb { get; set; }
    }
}