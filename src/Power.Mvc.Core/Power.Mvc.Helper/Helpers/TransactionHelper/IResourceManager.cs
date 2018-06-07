using System.Transactions;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 資源管理器介面
    /// </summary>
    /// <typeparam name="TResouce">實作交易機制的資源類別</typeparam>
    public interface IResourceManager<in TResouce> : IEnlistmentNotification where TResouce : ITransactionalResource
    {
        /// <summary>
        /// 建立資源管理器
        /// </summary>
        /// <param name="resource">實作交易機制的資源</param>
        /// <returns>資源管理器</returns>
        IResourceManager<TResouce> Manage(TResouce resource);
    }
}