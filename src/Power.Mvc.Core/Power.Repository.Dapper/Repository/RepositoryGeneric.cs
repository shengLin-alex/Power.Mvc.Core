using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Transactions;

namespace Power.Repository.Dapper
{
    /// <summary>
    /// 泛型資料儲存庫CRUD實作
    /// </summary>
    /// <typeparam name="TEntity">資料實體型別</typeparam>
    public class RepositoryGeneric<TEntity> : IRepositoryGeneric<TEntity> where TEntity : class
    {
        /// <summary>
        /// Connection factory interface. An implementation will be created for each kind of database.
        /// </summary>
        private readonly IConnectionFactory Factory;

        /// <summary>
        /// Track whether Dispose has been called.
        /// </summary>
        private bool Disposed;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">連線工廠</param>
        public RepositoryGeneric(IConnectionFactory factory)
        {
            this.Factory = factory;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="RepositoryGeneric{TEntity}"/> class.
        /// </summary>
        ~RepositoryGeneric()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// 資料庫連線
        /// </summary>
        public virtual IDbConnection Connection => this.Factory.Connection;

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">資料庫物件</param>
        /// <returns>
        /// The ID (primary key) of the newly inserted record if it is identity using the defined
        /// type, otherwise null
        /// </returns>
        public virtual object Add(TEntity entity)
        {
            // 登記指定的交易。
            DbConnection conn = this.Connection as DbConnection;
            conn?.EnlistTransaction(Transaction.Current);

            PropertyInfo keyProperty = typeof(TEntity).GetProperties()
                                                      .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));
            object key = typeof(SimpleCRUD)
                              .GetMethods(BindingFlags.Public | BindingFlags.Static)
                              .FirstOrDefault(x => x.Name == "Insert" && x.IsGenericMethod)
                              ?.MakeGenericMethod(keyProperty?.PropertyType)
                              .Invoke(null, new object[] { this.Connection, entity, null, null });

            return key;
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="entity">資料庫物件</param>
        public virtual void Delete(TEntity entity)
        {
            this.Connection.Delete(entity);
        }

        /// <summary>
        /// 根據主鍵取得物件
        /// </summary>
        /// <typeparam name="TId">主鍵類型</typeparam>
        /// <param name="id">值</param>
        /// <returns>資料庫物件</returns>
        public virtual TEntity Get<TId>(TId id)
        {
            return this.Connection.Get<TEntity>(id);
        }

        /// <summary>
        /// 根據條件取得物件列表
        /// </summary>
        /// <param name="conditions">查詢條件物件ex: new {Category = 1, SubCategory=2}</param>
        /// <returns>資料庫物件列表</returns>
        public virtual IEnumerable<TEntity> GetList(object conditions)
        {
            return this.Connection.GetList<TEntity>(conditions);
        }

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
        public virtual IEnumerable<TEntity> GetList(string conditions, object parameters = null)
        {
            return this.Connection.GetList<TEntity>(conditions, parameters);
        }

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
        public virtual IEnumerable<TEntity> GetListPaged(int pageNumber, int rowsPerPage, string conditions = "", string orderby = "", object parameters = null)
        {
            IEnumerable<TEntity> entities = this.Connection.GetListPaged<TEntity>(pageNumber, rowsPerPage, conditions, orderby, parameters);

            return entities;
        }

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
        public virtual int RecordCount(string conditions = "", object parameters = null)
        {
            return this.Connection.RecordCount<TEntity>(conditions, parameters);
        }

        /// <summary>
        /// 取得資料筆數
        /// </summary>
        /// <param name="conditions">查詢條件物件ex: new {Category = 1, SubCategory=2}</param>
        /// <returns>資料筆數</returns>
        public virtual int RecordCount(object conditions)
        {
            return this.Connection.RecordCount<TEntity>(conditions);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">資料庫物件</param>
        /// <returns>The number of effected records</returns>
        public virtual int Update(TEntity entity)
        {
            // 登記指定的交易。
            DbConnection conn = this.Connection as DbConnection;
            conn?.EnlistTransaction(Transaction.Current);

            return this.Connection.Update(entity);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            // This object will be cleaned up by the Dispose method. Therefore, you should call
            // GC.SuppressFinalize to take this object off the finalization queue and prevent
            // finalization code for this object from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing">
        /// If disposing equals true, dispose all managed and unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (this.Disposed)
            {
                return;
            }

            // If disposing equals true, dispose all managed and unmanaged resources.
            if (disposing)
            {
                // Dispose managed resources.
                this.Connection.Dispose();
            }

            // Note disposing has been done.
            this.Disposed = true;
        }
    }
}