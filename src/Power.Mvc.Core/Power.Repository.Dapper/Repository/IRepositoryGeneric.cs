using System;
using System.Collections.Generic;

namespace Power.Repository.Dapper
{
    /// <summary>
    /// 泛型資料儲存庫介面，基本CRUD
    /// </summary>
    /// <typeparam name="TEntity">實體資料型別</typeparam>
    public interface IRepositoryGeneric<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">資料庫物件</param>
        /// <returns>
        /// The ID (primary key) of the newly inserted record if it is identity using the defined
        /// type, otherwise null
        /// </returns>
        object Add(TEntity entity);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="entity">資料庫物件</param>
        void Delete(TEntity entity);

        /// <summary>
        /// 根據主鍵取得物件
        /// </summary>
        /// <typeparam name="TId">主鍵類型</typeparam>
        /// <param name="id">值</param>
        /// <returns>資料庫物件</returns>
        TEntity Get<TId>(TId id);

        /// <summary>
        /// 根據條件取得物件列表
        /// </summary>
        /// <param name="conditions">查詢條件物件ex: new {Category = 1, SubCategory=2}</param>
        /// <returns>資料庫物件列表</returns>
        IEnumerable<TEntity> GetList(object conditions);

        /// <summary>
        /// 根據條件取得物件列表
        /// </summary>
        /// <param name="conditions">
        /// conditions is an SQL where clause and/or order by clause ex: "where name='bob'" or "where
        /// age &gt;= @Age".
        /// </param>
        /// <param name="parameters">
        /// parameters is an anonymous type to pass in named parameter values: new { Age = 15 }
        /// </param>
        /// <returns>資料庫物件列表</returns>
        IEnumerable<TEntity> GetList(string conditions, object parameters = null);

        /// <summary>
        /// 資料表分頁
        /// </summary>
        /// <param name="pageNumber">[Required] 目前所在分頁</param>
        /// <param name="rowsPerPage">[Required] 每頁資料筆數</param>
        /// <param name="conditions">
        /// [Optional] 篩選條件 An SQL where clause ex: "where name='bob'" or "where age&gt;=@Age"
        /// </param>
        /// <param name="orderby">
        /// [Optional] 資料表欄位排序 A column or list of columns to order by ex: "lastname, age desc" and
        /// default is by primary key
        /// </param>
        /// <param name="parameters">
        /// [Optional] 篩選條件參數 An anonymous type to pass in named parameter values: new { Age = 15 }
        /// </param>
        /// <returns>資料庫物件列表</returns>
        IEnumerable<TEntity> GetListPaged(int pageNumber, int rowsPerPage, string conditions = "", string orderby = "", object parameters = null);

        /// <summary>
        /// 取得資料筆數
        /// </summary>
        /// <param name="conditions">
        /// [Optional] 篩選條件 An SQL where clause ex: "where name='bob'" or "where age&gt;=@Age"
        /// </param>
        /// <param name="parameters">
        /// [Optional] 篩選條件參數 An anonymous type to pass in named parameter values: new { Age = 15 }
        /// </param>
        /// <returns>資料筆數</returns>
        int RecordCount(string conditions = "", object parameters = null);

        /// <summary>
        /// 取得資料筆數
        /// </summary>
        /// <param name="conditions">查詢條件物件ex: new {Category = 1, SubCategory=2}</param>
        /// <returns>資料筆數</returns>
        int RecordCount(object conditions);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">資料庫物件</param>
        /// <returns>The number of effected records</returns>
        int Update(TEntity entity);
    }
}