using Power.Mvc.Helper;
using Power.Mvc.Helper.Models;
using System;

namespace Power.Repository.Dapper
{
    /// <summary>
    /// 儲存庫基底類別，用於使用 IEditColumn 的資料表
    /// </summary>
    /// <typeparam name="TEntity">資料實體型別</typeparam>
    public class RepositoryBase<TEntity> : RepositoryGeneric<TEntity> where TEntity : class, IEditColumn
    {
        /// <summary>
        /// 使用者資訊上下文
        /// </summary>
        private readonly IUserContext UserContext;

        /// <summary>
        /// ExecutionContext
        /// </summary>
        private readonly IExecutionContext ExecutionContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userContext">UserContext</param>
        /// <param name="executionContext">ExecutionContext</param>
        /// <param name="factory">DbContext 工廠</param>
        public RepositoryBase(
            IUserContext userContext,
            IExecutionContext executionContext,
            IConnectionFactory factory) : base(factory)
        {
            this.UserContext = userContext;
            this.ExecutionContext = executionContext;
        }

        /// <summary>
        /// 複寫新增方法，統一新增編輯欄位
        /// </summary>
        /// <param name="entity">實體資料</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>新增成功的實體資料pk</returns>
        public override object Add(TEntity entity)
        {
            switch (entity)
            {
                case null:
                    throw new ArgumentNullException(nameof(entity));
                case IEditColumn editModel:
                    editModel.CreatedAt = this.ExecutionContext.Now;
                    editModel.CreatedBy = this.UserContext.CurrentUser.UserId;
                    break;
            }

            return base.Add(entity);
        }

        /// <summary>
        /// 複寫更新方法，統一更新編輯欄位
        /// </summary>
        /// <param name="entity">實體資料</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>更新成功受影響的欄位數</returns>
        public override int Update(TEntity entity)
        {
            switch (entity)
            {
                case null:
                    throw new ArgumentNullException(nameof(entity));
                case IEditColumn editModel:
                    editModel.UpdatedAt = this.ExecutionContext.Now;
                    editModel.UpdatedBy = this.UserContext.CurrentUser.UserId;
                    break;
            }

            return base.Update(entity);
        }
    }
}