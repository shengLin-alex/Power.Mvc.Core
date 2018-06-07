namespace Power.Mvc.Helper.Extensions
{
    /// <summary>
    /// ConfigHelper擴充方法
    /// </summary>
    public static class ConfigExtension
    {
        /// <summary>
        /// 是否為產品階段
        /// </summary>
        /// <param name="config">ConfigHeler</param>
        /// <returns>是/否</returns>
        public static bool IsProduction(this IConfigHelper config)
        {
            return config.Get<bool>(nameof(IsProduction));
        }

        /// <summary>
        /// 是否開啟 Linq 指定追蹤
        /// </summary>
        /// <param name="config">ConfigHeler</param>
        /// <returns>是/否</returns>
        public static bool IsEf6TracerEnabled(this IConfigHelper config)
        {
            return config.Get<bool>(nameof(IsEf6TracerEnabled));
        }

        /// <summary>
        /// 是否開啟 MiniProfiler
        /// </summary>
        /// <param name="config">ConfigHeler</param>
        /// <returns>是/否</returns>
        public static bool IsMiniProfilerEnabled(this IConfigHelper config)
        {
            return config.Get<bool>(nameof(IsMiniProfilerEnabled));
        }

        /// <summary>
        /// 使否開啟 MongoDb log 追蹤
        /// </summary>
        /// <param name="config">ConfigHeler</param>
        /// <returns>是/否</returns>
        public static bool IsMongoTracerEnable(this IConfigHelper config)
        {
            return config.Get<bool>(nameof(IsMongoTracerEnable));
        }
    }
}