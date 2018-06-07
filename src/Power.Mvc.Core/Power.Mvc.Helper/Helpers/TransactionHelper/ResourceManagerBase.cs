using System.Transactions;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 資料管理基底類別，實作資源管理介面
    /// </summary>
    /// <typeparam name="TResource">受管理的資源型別</typeparam>
    public abstract class ResourceManagerBase<TResource> : IResourceManager<TResource> where TResource : ITransactionalResource
    {
        /// <summary>
        /// 預設建構子
        /// </summary>
        protected ResourceManagerBase()
        {
        }

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="resource">受管理的資源</param>
        protected ResourceManagerBase(TResource resource)
        {
            this.TransactionalResource = resource;
        }

        /// <summary>
        /// 實作交易機制的資源物件
        /// </summary>
        protected ITransactionalResource TransactionalResource { get; }

        /// <summary>
        /// 建立資源管理器
        /// </summary>
        /// <param name="resource">欲交給管理器的資料</param>
        /// <returns>資源管理器</returns>
        public abstract IResourceManager<TResource> Manage(TResource resource);

        /// <summary>
        /// 告知登記物件正在認可交易。
        /// </summary>
        /// <param name="enlistment"><see cref="T:System.Transactions.Enlistment" /> 物件，用來將回應傳送至交易管理員。</param>
        public void Commit(Enlistment enlistment)
        {
            this.OnCommit(enlistment);
        }

        /// <summary>
        /// 告知登記的交易狀態不確定的物件。
        /// </summary>
        /// <param name="enlistment"><see cref="T:System.Transactions.Enlistment" /> 物件，用來將回應傳送至交易管理員。</param>
        public void InDoubt(Enlistment enlistment)
        {
            this.OnDoubt(enlistment);
        }

        /// <summary>
        /// 告知登記正準備認可交易的物件。
        /// </summary>
        /// <param name="preparingEnlistment"><see cref="T:System.Transactions.PreparingEnlistment" /> 用來將回應傳送至交易管理員物件。</param>
        public void Prepare(PreparingEnlistment preparingEnlistment)
        {
            this.OnPrepare(preparingEnlistment);
        }

        /// <summary>
        /// 告知登記的物件正在復原交易 （中止）。
        /// </summary>
        /// <param name="enlistment"><see cref="T:System.Transactions.Enlistment" /> 用來將回應傳送至交易管理員物件。</param>
        public void Rollback(Enlistment enlistment)
        {
            this.OnRollback(enlistment);
        }

        /// <summary>
        /// 處理 Commit 實際操作
        /// </summary>
        /// <param name="enlistment"><see cref="T:System.Transactions.Enlistment" /> 物件，用來將回應傳送至交易管理員。</param>
        protected abstract void OnCommit(Enlistment enlistment);

        /// <summary>
        /// 交易狀態不確認時的操作
        /// </summary>
        /// <param name="enlistment"><see cref="T:System.Transactions.Enlistment" /> 物件，用來將回應傳送至交易管理員。</param>
        protected abstract void OnDoubt(Enlistment enlistment);

        /// <summary>
        /// 準備認可交易的物件
        /// </summary>
        /// <param name="preparingEnlistment"><see cref="T:System.Transactions.PreparingEnlistment" /> 物件，用來將回應傳送至交易管理員。</param>
        protected abstract void OnPrepare(PreparingEnlistment preparingEnlistment);

        /// <summary>
        /// 處理復原的實際操作
        /// </summary>
        /// <param name="enlistment"><see cref="T:System.Transactions.Enlistment" /> 物件，用來將回應傳送至交易管理員。</param>
        protected abstract void OnRollback(Enlistment enlistment);
    }
}