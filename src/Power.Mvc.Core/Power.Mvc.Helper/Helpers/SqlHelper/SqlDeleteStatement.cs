using System.Collections.Generic;
using Power.Mvc.Helper.Extensions;
using System.Text;
using Power.Mvc.Helper.Exceptions;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// Sql deleted 敘述介面
    /// </summary>
    [TraceAdvice]
    public class SqlDeleteStatement : ISqlDeleteStatement
    {
        /// <summary>
        /// 查詢條件
        /// </summary>
        private readonly List<string> WhereConditionsInstance;

        /// <summary>
        /// 表的名稱
        /// </summary>
        private string TableNameInstance;

        /// <summary>
        /// Constructor
        /// </summary>
        public SqlDeleteStatement()
        {
            this.TableNameInstance = string.Empty;
            this.WhereConditionsInstance = new List<string>();
        }

        /// <summary>
        /// 表的名稱(屬性)
        /// </summary>
        protected string TableName => this.TableNameInstance;

        /// <summary>
        /// 查詢條件(屬性)
        /// </summary>
        protected List<string> WhereConditions => this.WhereConditionsInstance;

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            this.TableNameInstance = string.Empty;
            this.WhereConditionsInstance.Clear();
        }

        /// <summary>
        /// Sql 實際內容
        /// </summary>
        /// <returns>Sql 語法</returns>
        /// <exception cref="TableNameNotSpecifiedException"></exception>
        public string Content()
        {
            if (this.TableNameInstance.IsNullOrEmpty())
            {
                throw new TableNameNotSpecifiedException();
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DELETE FROM ");
            sb.AppendLine(this.TableNameInstance);
            if (this.WhereConditionsInstance.Count > 0)
            {
                sb.AppendLine("WHERE ");
                sb.AppendLine(this.WhereConditionsInstance.Combine(" AND "));
            }

            return sb.ToString();
        }

        /// <summary>
        /// From 敘述
        /// </summary>
        /// <param name="tableName">表的名稱</param>
        /// <returns>deleted 敘述介面</returns>
        public ISqlDeleteStatement FromTable(string tableName)
        {
            this.TableNameInstance = tableName;
            return this;
        }

        /// <summary>
        /// where 條件
        /// </summary>
        /// <param name="condition">條件</param>
        /// <returns>deleted 敘述介面</returns>
        public ISqlDeleteStatement Where(string condition)
        {
            this.WhereConditionsInstance.Add(condition);
            return this;
        }
    }
}