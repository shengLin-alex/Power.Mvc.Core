using Power.Mvc.Helper.Exceptions;
using Power.Mvc.Helper.Extensions;
using System.Collections.Generic;
using System.Text;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 分頁查詢敘述
    /// </summary>
    public class SqlPagedStatement : ISqlPagedStatement
    {
        /// <summary>
        /// JOIN 敘述
        /// </summary>
        private readonly StringBuilder Joins = new StringBuilder();

        /// <summary>
        /// LEFT JOIN 敘述
        /// </summary>
        private readonly StringBuilder LeftJoins = new StringBuilder();

        /// <summary>
        /// ON 敘述
        /// </summary>
        private readonly StringBuilder OnClause = new StringBuilder();

        /// <summary>
        /// AND 敘述
        /// </summary>
        private readonly List<string> AndClauses = new List<string>();

        /// <summary>
        /// ORDER BY 子句
        /// </summary>
        private readonly List<string> OrderByClause = new List<string>();

        /// <summary>
        /// 選擇欄位
        /// </summary>
        private readonly List<string> SelectedColumns = new List<string>();

        /// <summary>
        /// WHERE 條件
        /// </summary>
        private readonly List<string> WhereClause = new List<string>();

        /// <summary>
        /// 計算數量
        /// </summary>
        private bool IsCountRecord;

        /// <summary>
        /// 目前在第幾頁
        /// </summary>
        private int? PageInstance;

        /// <summary>
        /// 每頁包含的筆數
        /// </summary>
        private int? PageSizeInstance;

        /// <summary>
        /// 選擇資料表
        /// </summary>
        private string SelectedTable = string.Empty;

        /// <summary>
        /// 清除內容
        /// </summary>
        public void Clear()
        {
            this.Joins.Clear();
            this.LeftJoins.Clear();
            this.OnClause.Clear();
            this.AndClauses.Clear();
            this.OrderByClause.Clear();
            this.SelectedColumns.Clear();
            this.SelectedTable = string.Empty;
            this.WhereClause.Clear();
            this.IsCountRecord = false;
            this.PageInstance = null;
            this.PageSizeInstance = null;
        }

        /// <summary>
        /// Sql 實際內容
        /// </summary>
        /// <returns>Sql 語法</returns>
        /// <exception cref="OrderbyClauseNotSpecifiedException"></exception>
        /// <exception cref="TableNameNotSpecifiedException"></exception>
        public string Content()
        {
            this.CheckStatements();

            SqlSelectStatement innerStatment = new SqlSelectStatement();
            innerStatment.Select($"ROW_NUMBER() OVER(ORDER BY {string.Join(",", this.OrderByClause)}) AS PagedNumber ");
            if (this.SelectedColumns.Count == 0)
            {
                innerStatment.Select($"{this.SelectedTable}.* ");
            }
            else
            {
                this.SelectedColumns.ForEach(x => innerStatment.Select(x));
            }

            if (this.Joins.Length > 0)
            {
                innerStatment.Join(this.Joins.ToString());
            }

            if (this.LeftJoins.Length > 0)
            {
                innerStatment.LeftJoin(this.LeftJoins.ToString());
            }

            if (this.OnClause.Length > 0)
            {
                innerStatment.On(this.OnClause.ToString());
            }

            if (this.AndClauses.Count > 0)
            {
                this.AndClauses.ForEach(clause => innerStatment.And(clause));
            }

            innerStatment.From(this.SelectedTable);
            innerStatment.Where(" 1=1 ");

            if (this.WhereClause.Count > 0)
            {
                this.WhereClause.ForEach(x => { innerStatment.Where(x); });
            }

            SqlSelectStatement outterStatment = new SqlSelectStatement();
            outterStatment.From($"({innerStatment.Content()}) t");

            // 計算筆數
            if (this.IsCountRecord)
            {
                outterStatment.Select("COUNT(1) ");
            }

            // 資料表分頁
            if (this.PageInstance != null && this.PageSizeInstance != null)
            {
                outterStatment.Where($"PagedNumber BETWEEN (({this.PageInstance}-1) * {this.PageSizeInstance} + 1) AND ({this.PageInstance} * {this.PageSizeInstance})");
            }

            return outterStatment.Content();
        }

        /// <summary>
        /// 計算數量
        /// </summary>
        /// <returns>SqlPagedStatement</returns>
        public ISqlPagedStatement Count()
        {
            this.IsCountRecord = true;
            return this;
        }

        /// <summary>
        /// 選擇資料表
        /// </summary>
        /// <param name="table">資料表</param>
        /// <returns>SqlPagedStatement</returns>
        public ISqlPagedStatement From(string table)
        {
            this.SelectedTable = table;
            return this;
        }

        /// <summary>
        /// 加入 JOIN TABLE
        /// </summary>
        /// <param name="clause">JOIN TABLE Clause</param>
        /// <returns>SqlPagedStatement</returns>
        public ISqlPagedStatement Join(string clause)
        {
            this.Joins.AppendLine(clause);
            return this;
        }

        /// <summary>
        /// 加入 LEFT JOIN TABLE
        /// </summary>
        /// <param name="clause">LEFT JOIN TABLE Clause</param>
        /// <returns>SqlSelectStatement</returns>
        public ISqlPagedStatement LeftJoin(string clause)
        {
            this.LeftJoins.AppendLine(clause);
            return this;
        }

        /// <summary>
        /// 加入 ON
        /// </summary>
        /// <param name="clause">ON Clause</param>
        /// <returns>SqlSelectStatement</returns>
        public ISqlPagedStatement On(string clause)
        {
            this.OnClause.AppendLine(clause);
            return this;
        }

        /// <summary>
        /// 加入 AND 敘述
        /// </summary>
        /// <param name="clause">AND Clause</param>
        /// <returns>SqlSelectStatement</returns>
        public ISqlPagedStatement And(string clause)
        {
            this.AndClauses.Add(clause);
            return this;
        }

        /// <summary>
        /// 加入 Order By 子句
        /// </summary>
        /// <param name="clause">Order By 子句</param>
        /// <returns>SqlPagedStatement</returns>
        public ISqlPagedStatement OrderBy(string clause)
        {
            this.OrderByClause.Add(clause);
            return this;
        }

        /// <summary>
        /// 目前在第幾頁
        /// </summary>
        /// <param name="value">當前頁面</param>
        /// <returns>SqlPagedStatement</returns>
        public ISqlPagedStatement Page(int value)
        {
            this.PageInstance = value;
            return this;
        }

        /// <summary>
        /// 每頁包含的筆數
        /// </summary>
        /// <param name="value">筆數</param>
        /// <returns>SqlPagedStatement</returns>
        public ISqlPagedStatement PageSize(int value)
        {
            this.PageSizeInstance = value;
            return this;
        }

        /// <summary>
        /// 選擇欄位
        /// </summary>
        /// <param name="columns">欄位</param>
        /// <returns>SqlPagedStatement</returns>
        public ISqlPagedStatement Select(string columns)
        {
            this.SelectedColumns.Add(columns);
            return this;
        }

        /// <summary>
        /// 加入 Where 子句
        /// </summary>
        /// <param name="clause">Where 子句</param>
        /// <returns>SqlPagedStatement</returns>
        public ISqlPagedStatement Where(string clause)
        {
            this.WhereClause.Add(clause);
            return this;
        }

        #region private methods

        /// <summary>
        /// 必要設定檢查
        /// </summary>
        /// <exception cref="OrderbyClauseNotSpecifiedException"></exception>
        /// <exception cref="TableNameNotSpecifiedException"></exception>
        private void CheckStatements()
        {
            if (this.OrderByClause.Count == 0)
            {
                throw new OrderbyClauseNotSpecifiedException();
            }

            if (this.SelectedTable.IsNullOrEmpty())
            {
                throw new TableNameNotSpecifiedException();
            }
        }

        #endregion private methods
    }
}