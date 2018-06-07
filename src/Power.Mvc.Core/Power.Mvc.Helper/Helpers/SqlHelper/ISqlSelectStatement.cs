namespace Power.Mvc.Helper
{
    /// <summary>
    /// Select 敘述介面
    /// </summary>
    [TraceAdvice]
    public interface ISqlSelectStatement : ISqlStatement
    {
        /// <summary>
        /// DISTINCT
        /// </summary>
        /// <param name="value">Distinct</param>
        /// <returns>SqlSelectStatement</returns>
        ISqlSelectStatement Distinct(bool value);

        /// <summary>
        /// 選擇資料表
        /// </summary>
        /// <param name="tables">資料表</param>
        /// <returns>SqlSelectStatement</returns>
        ISqlSelectStatement From(string tables);

        /// <summary>
        /// 加入 Group By
        /// </summary>
        /// <param name="columns">Group By 欄位名稱列表</param>
        /// <returns>SqlSelectStatement</returns>
        ISqlSelectStatement GroupBy(string columns);

        /// <summary>
        /// 加入 HAVING 敘述
        /// </summary>
        /// <param name="statement">HAVING 敘述</param>
        /// <returns>SqlSelectStatement</returns>
        ISqlSelectStatement Having(string statement);

        /// <summary>
        /// 加入 JOIN TABLE
        /// </summary>
        /// <param name="clause">JOIN TABLE Clause</param>
        /// <returns>SqlSelectStatement</returns>
        ISqlSelectStatement Join(string clause);

        /// <summary>
        /// 加入 LEFT JOIN TABLE
        /// </summary>
        /// <param name="clause">LEFT JOIN TABLE Clause</param>
        /// <returns>SqlSelectStatement</returns>
        ISqlSelectStatement LeftJoin(string clause);

        /// <summary>
        /// 加入 ON
        /// </summary>
        /// <param name="clause">ON Clause</param>
        /// <returns>SqlSelectStatement</returns>
        ISqlSelectStatement On(string clause);

        /// <summary>
        /// 加入 AND 敘述
        /// </summary>
        /// <param name="clause">AND Clause</param>
        /// <returns>SqlSelectStatement</returns>
        ISqlSelectStatement And(string clause);

        /// <summary>
        /// 加入 Order By 子句
        /// </summary>
        /// <param name="clause">Order By 子句</param>
        /// <returns>SqlSelectStatement</returns>
        ISqlSelectStatement OrderBy(string clause);

        /// <summary>
        /// 選擇欄位
        /// </summary>
        /// <param name="columns">欄位</param>
        /// <returns>SqlSelectStatement</returns>
        ISqlSelectStatement Select(string columns);

        /// <summary>
        /// TOP
        /// </summary>
        /// <param name="clause">SELECT TOP</param>
        /// <returns>SqlSelectStatement</returns>
        ISqlSelectStatement SelectTop(string clause);

        /// <summary>
        /// 加入 Where 子句
        /// </summary>
        /// <param name="clause">Where 子句</param>
        /// <returns>SqlSelectStatement</returns>
        ISqlSelectStatement Where(string clause);
    }
}