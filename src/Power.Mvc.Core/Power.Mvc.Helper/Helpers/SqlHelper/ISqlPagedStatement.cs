namespace Power.Mvc.Helper
{
    /// <summary>
    /// 分頁查詢敘述
    /// </summary>
    public interface ISqlPagedStatement : ISqlStatement
    {
        /// <summary>
        /// 計算數量
        /// </summary>
        /// <returns>SqlPagedStatement</returns>
        ISqlPagedStatement Count();

        /// <summary>
        /// 選擇資料表
        /// </summary>
        /// <param name="table">資料表</param>
        /// <returns>SqlPagedStatement</returns>
        ISqlPagedStatement From(string table);

        /// <summary>
        /// 加入 JOIN TABLE
        /// </summary>
        /// <param name="clause">JOIN TABLE Clause</param>
        /// <returns>SqlPagedStatement</returns>
        ISqlPagedStatement Join(string clause);

        /// <summary>
        /// 加入 LEFT JOIN TABLE
        /// </summary>
        /// <param name="clause">LEFT JOIN TABLE Clause</param>
        /// <returns>SqlSelectStatement</returns>
        ISqlPagedStatement LeftJoin(string clause);

        /// <summary>
        /// 加入 ON
        /// </summary>
        /// <param name="clause">ON Clause</param>
        /// <returns>SqlSelectStatement</returns>
        ISqlPagedStatement On(string clause);

        /// <summary>
        /// 加入 AND 敘述
        /// </summary>
        /// <param name="clause">AND Clause</param>
        /// <returns>SqlSelectStatement</returns>
        ISqlPagedStatement And(string clause);

        /// <summary>
        /// 加入 Order By 子句
        /// </summary>
        /// <param name="clause">Order By 子句</param>
        /// <returns>SqlPagedStatement</returns>
        ISqlPagedStatement OrderBy(string clause);

        /// <summary>
        /// 目前在第幾頁
        /// </summary>
        /// <param name="value">當前頁面</param>
        /// <returns>SqlPagedStatement</returns>
        ISqlPagedStatement Page(int value);

        /// <summary>
        /// 每頁包含的筆數
        /// </summary>
        /// <param name="value">筆數</param>
        /// <returns>SqlPagedStatement</returns>
        ISqlPagedStatement PageSize(int value);

        /// <summary>
        /// 選擇欄位
        /// </summary>
        /// <param name="columns">欄位</param>
        /// <returns>SqlPagedStatement</returns>
        ISqlPagedStatement Select(string columns);

        /// <summary>
        /// 加入 Where 子句
        /// </summary>
        /// <param name="clause">Where 子句</param>
        /// <returns>SqlPagedStatement</returns>
        ISqlPagedStatement Where(string clause);
    }
}