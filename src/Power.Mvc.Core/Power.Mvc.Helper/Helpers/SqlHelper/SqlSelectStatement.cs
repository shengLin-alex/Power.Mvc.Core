using Power.Mvc.Helper.Extensions;
using System.Collections.Generic;
using System.Text;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// Select 敘述
    /// </summary>
    public class SqlSelectStatement : ISqlSelectStatement
    {
        /// <summary>
        /// GroupBy 欄位
        /// </summary>
        private readonly List<string> GroupByColumns;

        /// <summary>
        /// HAVING 敘述
        /// </summary>
        private readonly StringBuilder HavingStatement;

        /// <summary>
        /// JOIN 敘述
        /// </summary>
        private readonly StringBuilder Joins;

        /// <summary>
        /// LEFT JOIN 敘述
        /// </summary>
        private readonly StringBuilder LeftJoins;

        /// <summary>
        /// ON 敘述
        /// </summary>
        private readonly StringBuilder OnClause;

        /// <summary>
        /// AND 敘述
        /// </summary>
        private readonly List<string> AndClauses;

        /// <summary>
        /// ORDER BY 子句
        /// </summary>
        private readonly List<string> OrderByClause;

        /// <summary>
        /// 選擇欄位
        /// </summary>
        private readonly List<string> SelectedColumns;

        /// <summary>
        /// 選擇資料表
        /// </summary>
        private readonly StringBuilder SelectedTables;

        /// <summary>
        /// WHERE 條件
        /// </summary>
        private readonly List<string> WhereClause;

        /// <summary>
        /// SQL Distince
        /// </summary>
        private bool SelectDistinct;

        /// <summary>
        /// TOP
        /// </summary>
        private string TopClause;

        /// <summary>
        /// Constructor
        /// </summary>
        public SqlSelectStatement()
        {
            this.SelectDistinct = false;
            this.TopClause = string.Empty;
            this.SelectedColumns = new List<string>();
            this.SelectedTables = new StringBuilder();
            this.Joins = new StringBuilder();
            this.LeftJoins = new StringBuilder();
            this.OnClause = new StringBuilder();
            this.AndClauses = new List<string>();
            this.WhereClause = new List<string>();
            this.GroupByColumns = new List<string>();
            this.HavingStatement = new StringBuilder();
            this.OrderByClause = new List<string>();
        }

        /// <summary>
        /// 清除內容
        /// </summary>
        public void Clear()
        {
            this.SelectDistinct = false;
            this.GroupByColumns.Clear();
            this.HavingStatement.Clear();
            this.Joins.Clear();
            this.LeftJoins.Clear();
            this.OnClause.Clear();
            this.AndClauses.Clear();
            this.OrderByClause.Clear();
            this.SelectedColumns.Clear();
            this.SelectedTables.Clear();
            this.TopClause = string.Empty;
            this.WhereClause.Clear();
        }

        /// <summary>
        /// 取得完整的 SQL 敘述
        /// </summary>
        /// <returns>SQL 敘述</returns>
        public string Content()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT ");
            if (this.SelectDistinct)
            {
                sb.AppendLine("DISTINCT ");
            }

            if (!this.TopClause.IsNullOrEmpty())
            {
                sb.AppendLine(this.TopClause);
            }

            sb.AppendLine(this.SelectedColumns.Count > 0 ? this.SelectedColumns.Combine(",") : "* ");
            sb.AppendLine("FROM ");
            sb.AppendLine(this.SelectedTables.ToString());

            if (this.Joins.Length > 0)
            {
                sb.AppendLine("JOIN ");
                sb.AppendLine(this.Joins.ToString());
            }

            if (this.LeftJoins.Length > 0)
            {
                sb.AppendLine("LEFT JOIN ");
                sb.AppendLine(this.LeftJoins.ToString());
            }

            if (this.OnClause.Length > 0)
            {
                sb.AppendLine("ON ");
                sb.AppendLine(this.OnClause.ToString());
            }

            if (this.AndClauses.Count > 0)
            {
                this.AndClauses.ForEach(clause => { sb.AppendLine(" AND "); sb.AppendLine(clause); });
            }

            if (this.WhereClause.Count > 0)
            {
                sb.AppendLine("WHERE ");
                sb.AppendLine(this.WhereClause.Combine(" AND "));
            }

            if (this.GroupByColumns.Count > 0)
            {
                sb.AppendLine("GROUP BY ");
                sb.AppendLine(this.GroupByColumns.Combine(","));
            }

            if (this.HavingStatement.Length > 0)
            {
                sb.AppendLine("HAVING ");
                sb.AppendLine(this.HavingStatement.ToString());
            }

            if (this.OrderByClause.Count > 0)
            {
                sb.AppendLine("ORDER BY ");
                sb.AppendLine(this.OrderByClause.Combine(","));
            }

            return sb.ToString();
        }

        /// <summary>
        /// DISTINCT
        /// </summary>
        /// <param name="value">Distinct</param>
        /// <returns>SqlSelectStatement</returns>
        public ISqlSelectStatement Distinct(bool value)
        {
            this.SelectDistinct = value;
            return this;
        }

        /// <summary>
        /// 選擇資料表
        /// </summary>
        /// <param name="tables">資料表</param>
        /// <returns>SqlSelectStatement</returns>
        public ISqlSelectStatement From(string tables)
        {
            this.SelectedTables.AppendLine(tables);
            return this;
        }

        /// <summary>
        /// 加入 Group By
        /// </summary>
        /// <param name="columns">Group By 欄位名稱列表</param>
        /// <returns>SqlSelectStatement</returns>
        public ISqlSelectStatement GroupBy(string columns)
        {
            this.GroupByColumns.Add(columns);
            return this;
        }

        /// <summary>
        /// 加入 HAVING 敘述
        /// </summary>
        /// <param name="statement">HAVING 敘述</param>
        /// <returns>SqlSelectStatement</returns>
        public ISqlSelectStatement Having(string statement)
        {
            this.HavingStatement.AppendLine(statement);
            return this;
        }

        /// <summary>
        /// 加入 JOIN TABLE
        /// </summary>
        /// <param name="clause">JOIN TABLE Clause</param>
        /// <returns>SqlSelectStatement</returns>
        public ISqlSelectStatement Join(string clause)
        {
            this.Joins.AppendLine(clause);
            return this;
        }

        /// <summary>
        /// 加入 LEFT JOIN TABLE
        /// </summary>
        /// <param name="clause">LEFT JOIN TABLE Clause</param>
        /// <returns>SqlSelectStatement</returns>
        public ISqlSelectStatement LeftJoin(string clause)
        {
            this.LeftJoins.AppendLine(clause);
            return this;
        }

        /// <summary>
        /// 加入 ON
        /// </summary>
        /// <param name="clause">ON Clause</param>
        /// <returns>SqlSelectStatement</returns>
        public ISqlSelectStatement On(string clause)
        {
            this.OnClause.AppendLine(clause);
            return this;
        }

        /// <summary>
        /// 加入 AND 敘述
        /// </summary>
        /// <param name="clause">AND Clause</param>
        /// <returns>SqlSelectStatement</returns>
        public ISqlSelectStatement And(string clause)
        {
            this.AndClauses.Add(clause);
            return this;
        }

        /// <summary>
        /// 加入 Order By 子句
        /// </summary>
        /// <param name="clause">Order By 子句</param>
        /// <returns>SqlSelectStatement</returns>
        public ISqlSelectStatement OrderBy(string clause)
        {
            this.OrderByClause.Add(clause);
            return this;
        }

        /// <summary>
        /// 選擇欄位
        /// </summary>
        /// <param name="columns">欄位</param>
        /// <returns>SqlSelectStatement</returns>
        public ISqlSelectStatement Select(string columns)
        {
            this.SelectedColumns.Add(columns);
            return this;
        }

        /// <summary>
        /// TOP
        /// </summary>
        /// <param name="clause">SELECT TOP</param>
        /// <returns>SqlSelectStatement</returns>
        public ISqlSelectStatement SelectTop(string clause)
        {
            this.TopClause = clause;
            return this;
        }

        /// <summary>
        /// 加入 Where 子句
        /// </summary>
        /// <param name="clause">Where 子句</param>
        /// <returns>SqlSelectStatement</returns>
        public ISqlSelectStatement Where(string clause)
        {
            this.WhereClause.Add(clause);
            return this;
        }
    }
}