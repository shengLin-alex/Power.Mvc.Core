using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Power.Repository.Dapper
{
    /// <summary>
    /// MySql 連線工廠基底類別
    /// </summary>
    public abstract class MySqlDbConnectionFactoryBase : ConnectionFactoryBase, IMySqlConnectionFactory
    {
        /// <summary>
        /// 建立 Db 連線
        /// </summary>
        /// <returns> Db 連線 </returns>
        protected override IDbConnection CreateConnection()
        {
            return new MySqlConnection(this.ConnectionString);
        }

        /// <summary>
        /// 設定 SimpleCRUD Dialect
        /// </summary>
        protected override void SetDialect()
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
        }
    }
}