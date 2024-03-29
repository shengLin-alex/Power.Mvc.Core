﻿using Dapper;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
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
            return new ProfiledDbConnection(new SqlConnection(this.ConnectionString), MiniProfiler.Current);
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