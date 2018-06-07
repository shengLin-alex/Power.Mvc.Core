namespace Power.Mvc.Helper
{
    /// <summary>
    /// Sql deleted 敘述介面
    /// </summary>
    [TraceAdvice]
    public interface ISqlDeleteStatement : ISqlStatement
    {
        /// <summary>
        /// From 敘述
        /// </summary>
        /// <param name="tableName">表的名稱</param>
        /// <returns>deleted 敘述介面</returns>
        ISqlDeleteStatement FromTable(string tableName);

        /// <summary>
        /// where 條件
        /// </summary>
        /// <param name="condition">條件</param>
        /// <returns>deleted 敘述介面</returns>
        ISqlDeleteStatement Where(string condition);
    }
}