namespace Power.Mvc.Helper
{
    /// <summary>
    /// 資料庫查詢字串建立介面
    /// </summary>
    public interface IQueryBuilder
    {
        /// <summary>
        /// 建置 Sql
        /// </summary>
        /// <returns>查詢語法</returns>
        string Build();
    }
}