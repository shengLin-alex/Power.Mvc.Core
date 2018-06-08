using Microsoft.EntityFrameworkCore;
using Power.Mvc.Helper;
using Power.Mvc.Helper.Models;
using System;
using System.Threading.Tasks;

namespace Power.Repository.EFCore
{
    /// <summary>
    /// 儲存庫基底類別，用於使用 IEditColumn 的資料表
    /// </summary>
    /// <typeparam name="TDbContext">DbContext 實際型別</typeparam>
    /// <typeparam name="TEntity">資料實體型別</typeparam>
    public class RepositoryBase<TEntity, TDbContext> : RepositoryGeneric<TEntity, TDbContext>
        where TDbContext : DbContext
        where TEntity : class, IEditColumn
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
            IDbContextFactory<TDbContext> factory) : base(factory)
        {
            this.UserContext = userContext;
            this.ExecutionContext = executionContext;
        }

        /// <summary>
        /// 複寫新增方法，統一更新編輯欄位
        /// </summary>
        /// <param name="entity">實體資料</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>新增成功的實體資料</returns>
        public override TEntity Create(TEntity entity)
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

            return base.Create(entity);
        }

        /// <summary>
        /// 複寫新增方法，統一更新編輯欄位(非同步工作)
        /// </summary>
        /// <param name="entity">實體資料</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>新增成功的實體資料</returns>
        public override async Task<TEntity> CreateAsync(TEntity entity)
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

            return await base.CreateAsync(entity);
        }

        /// <summary>
        /// 複寫更新方法，統一更新編輯欄位(非同步工作)
        /// </summary>
        /// <param name="entity">欲更新的實體資料</param>
        /// <exception cref="ArgumentNullException"></exception>
        public override void Update(TEntity entity)
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

            base.Update(entity);
        }

        /// <summary>
        /// 複寫更新方法，統一更新編輯欄位(非同步工作)
        /// </summary>
        /// <param name="entity">欲更新的實體資料</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>非同步工作</returns>
        public override async Task UpdateAsync(TEntity entity)
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

            await base.UpdateAsync(entity);
        }
    }
}