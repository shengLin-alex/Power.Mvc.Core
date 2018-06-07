using Power.Mvc.Helper.Exceptions;
using Power.Mvc.Helper.Extensions;
using System.Collections.Generic;
using System.Text;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// Sql update 敘述介面
    /// </summary>
    [TraceAdvice]
    public class SqlUpdateStatement : ISqlUpdateStatement
    {
        /// <summary>
        /// Set 之欄位
        /// </summary>
        private readonly List<string> SetColumnsInstance;

        /// <summary>
        /// Where 條件
        /// </summary>
        private readonly List<string> WhereConditionsInstance;

        /// <summary>
        /// 表的名稱
        /// </summary>
        private string TableNameInstance;

        /// <summary>
        /// 建構子
        /// </summary>
        public SqlUpdateStatement()
        {
            this.TableNameInstance = string.Empty;
            this.SetColumnsInstance = new List<string>();
            this.WhereConditionsInstance = new List<string>();
        }

        /// <summary>
        /// Set 之欄位(屬性)
        /// </summary>
        protected List<string> SetColumns => this.SetColumnsInstance;

        /// <summary>
        /// 表的名稱(屬性)
        /// </summary>
        protected string TableName => this.TableNameInstance;

        /// <summary>
        /// Where 條件(屬性)
        /// </summary>
        protected List<string> WhereConditions => this.WhereConditionsInstance;

        /// <summary>
        /// 清除 SQL 敘述內容
        /// </summary>
        public void Clear()
        {
            this.TableNameInstance = string.Empty;
            this.WhereConditionsInstance.Clear();
        }

        /// <summary>
        /// 取得完整的 SQL 敘述
        /// </summary>
        /// <exception cref="NoColumnSpecifiedException"></exception>
        /// <exception cref="TableNameNotSpecifiedException"></exception>
        /// <returns>完整的 SQL 敘述</returns>
        public string Content()
        {
            this.CheckStatements();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE ");
            sb.AppendLine(this.TableNameInstance);
            sb.AppendLine("SET ");
            sb.AppendLine(this.SetColumnsInstance.Combine(", "));

            if (this.WhereConditionsInstance.Count > 0)
            {
                sb.AppendLine("WHERE ");
                sb.AppendLine(this.WhereConditionsInstance.Combine(" AND "));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Set value 敘述
        /// </summary>
        /// <param name="column">欄位</param>
        /// <param name="value">值</param>
        /// <returns>Sql 敘述</returns>
        public ISqlUpdateStatement Set(string column, object value)
        {
            string statement = column + " = ";

            if (value is string)
            {
                statement += "'" + value + "'";
            }
            else
            {
                statement += value;
            }

            this.SetColumnsInstance.Add(statement);

            return this;
        }

        /// <summary>
        /// Update 敘述
        /// </summary>
        /// <param name="tableName">表名稱</param>
        /// <returns>Sql 敘述</returns>
        public ISqlUpdateStatement UpdateTable(string tableName)
        {
            this.TableNameInstance = tableName;
            return this;
        }

        /// <summary>
        /// Where 敘述
        /// </summary>
        /// <param name="condition">條件</param>
        /// <returns>Sql 敘述</returns>
        public ISqlUpdateStatement Where(string condition)
        {
            this.WhereConditionsInstance.Add(condition);
            return this;
        }

        /// <summary>
        /// 檢查語句
        /// </summary>
        /// <exception cref="NoColumnSpecifiedException"></exception>
        /// <exception cref="TableNameNotSpecifiedException"></exception>
        private void CheckStatements()
        {
            if (this.SetColumnsInstance.Count == 0)
            {
                throw new NoColumnSpecifiedException();
            }

            if (this.TableNameInstance.IsNullOrEmpty())
            {
                throw new TableNameNotSpecifiedException();
            }
        }
    }
}