namespace Power.Mvc.Helper
{
    /// <summary>
    /// Sql update 敘述介面
    /// </summary>
    public interface ISqlUpdateStatement : ISqlStatement
    {
        /// <summary>
        /// Set value 敘述
        /// </summary>
        /// <param name="column">欄位</param>
        /// <param name="value">值</param>
        /// <returns>Sql 敘述</returns>
        ISqlUpdateStatement Set(string column, object value);

        /// <summary>
        /// Update 敘述
        /// </summary>
        /// <param name="tableName">表名稱</param>
        /// <returns>Sql 敘述</returns>
        ISqlUpdateStatement UpdateTable(string tableName);

        /// <summary>
        /// Where 敘述
        /// </summary>
        /// <param name="condition">條件</param>
        /// <returns>Sql 敘述</returns>
        ISqlUpdateStatement Where(string condition);
    }
}