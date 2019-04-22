using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Power.Mvc.Helper;
using Power.Mvc.Helper.Extensions;
using System;

namespace Power.Repository.EFCore
{
    /// <summary>
    /// DbContext 工廠基底
    /// </summary>
    public abstract class DbContextFactoryBase<TDbContext> : IDbContextFactory<TDbContext> where TDbContext : DbContext
    {
        /// <summary>
        /// 組態設定
        /// </summary>
        private readonly IConfiguration Configuration = PackageDiResolver.Current.GetService<IConfiguration>();

        /// <summary>
        /// DbContext 實例
        /// </summary>
        private TDbContext DbContextInstance;

        /// <summary>
        /// 取得 DbContext
        /// </summary>
        public TDbContext DbContext => this.DbContextInstance ??
                                       (this.DbContextInstance = (TDbContext)Activator.CreateInstance(typeof(TDbContext), this.ConnectionString));

        /// <summary>
        /// 連線字串鍵值
        /// </summary>
        protected abstract string ConnectionKey { get; set; }

        /// <summary>
        /// 連線字串
        /// </summary>
        protected string ConnectionString => this.Configuration.GetConnectionString(this.ConnectionKey);
    }
}