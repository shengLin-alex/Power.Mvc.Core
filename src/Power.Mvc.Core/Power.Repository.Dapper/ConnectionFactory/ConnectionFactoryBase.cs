using System.Configuration;
using System.Data;

namespace Power.Repository.Dapper
{
    /// <summary>
    /// 資料庫連線工廠基底類別
    /// </summary>
    public abstract class ConnectionFactoryBase : IConnectionFactory
    {
        /// <summary>
        /// Db 連線實例
        /// </summary>
        private IDbConnection DbConnectionInstance;

        /// <summary>
        /// 連線字串
        /// </summary>
        protected string ConnectionString => ConfigurationManager.ConnectionStrings[this.ConnectionKey].ConnectionString;

        /// <summary>
        /// 連線字串鍵值
        /// </summary>
        protected abstract string ConnectionKey { get; set; }

        /// <summary>
        /// Db 連線 property
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                if (this.DbConnectionInstance == null)
                {
                    this.DbConnectionInstance = this.CreateConnection();
                }

                this.SetDialect();

                return this.DbConnectionInstance;
            }
        }

        /// <summary>
        /// 建立 Db 連線
        /// </summary>
        /// <returns> Db 連線 </returns>
        protected abstract IDbConnection CreateConnection();

        /// <summary>
        /// 設定 SimpleCRUD Dialect
        /// </summary>
        protected abstract void SetDialect();
    }
}