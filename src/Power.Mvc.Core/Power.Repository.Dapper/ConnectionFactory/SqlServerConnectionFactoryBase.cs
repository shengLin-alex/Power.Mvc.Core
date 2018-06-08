using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Power.Repository.Dapper
{
    /// <summary>
    /// SqlServer 連線工廠基底類別
    /// </summary>
    public abstract class SqlServerConnectionFactoryBase : ConnectionFactoryBase, ISqlServerConnectionFactory
    {
        /// <summary>
        /// 建立 Db 連線
        /// </summary>
        /// <returns> Db 連線 </returns>
        protected override IDbConnection CreateConnection()
        {
            return new SqlConnection(this.ConnectionString);
        }

        /// <summary>
        /// 設定 SimpleCRUD Dialect
        /// </summary>
        protected override void SetDialect()
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLServer);
        }
    }
}