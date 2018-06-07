using System;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 資料庫查詢字串建立
    /// </summary>
    /// <typeparam name="TSqlStatement">Sql 查詢語法類型</typeparam>
    public class QueryBuilder<TSqlStatement> : IQueryBuilder
        where TSqlStatement : ISqlStatement
    {
        /// <summary>
        /// 查詢字串
        /// </summary>
        private readonly TSqlStatement SqlStatementInstance;

        /// <summary>
        /// Constructor
        /// </summary>
        public QueryBuilder()
        {
            this.SqlStatementInstance = (TSqlStatement)Activator.CreateInstance(typeof(TSqlStatement));
        }

        /// <summary>
        /// 查詢字串 (保護屬性)
        /// </summary>
        protected TSqlStatement SqlStatement => this.SqlStatementInstance;

        /// <summary>
        /// 建置 Sql
        /// </summary>
        /// <returns>查詢語法</returns>
        public string Build()
        {
            return this.SqlStatementInstance.Content();
        }
    }
}