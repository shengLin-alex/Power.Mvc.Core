namespace Power.Mvc.Helper
{
    /// <summary>
    /// 必須實作交易機制的物件介面
    /// </summary>
    public interface ITransactionalResource
    {
        /// <summary>
        /// 已準備認可
        /// </summary>
        bool Prepared { get; }

        /// <summary>
        /// 認可交易操作
        /// </summary>
        /// <returns>是否成功</returns>
        bool Commit();

        /// <summary>
        /// 復原交易操作
        /// </summary>
        /// <returns>是否成功</returns>
        bool Rollback();
    }
}