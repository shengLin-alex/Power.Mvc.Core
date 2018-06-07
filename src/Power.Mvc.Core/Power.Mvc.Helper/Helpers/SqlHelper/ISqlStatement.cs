namespace Power.Mvc.Helper
{
    /// <summary>
    /// SQL 敘述
    /// </summary>
    public interface ISqlStatement
    {
        /// <summary>
        /// 清除 SQL 敘述內容
        /// </summary>
        void Clear();

        /// <summary>
        /// 取得完整的 SQL 敘述
        /// </summary>
        /// <returns>完整的 SQL 敘述</returns>
        string Content();
    }
}