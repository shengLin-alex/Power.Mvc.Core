using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Power.Repository.EFCore
{
    /// <summary>
    /// 泛型儲存庫介面
    /// </summary>
    /// <typeparam name="TDbContext">DbContext 實際型別</typeparam>
    /// <typeparam name="TEntity">實體資料型別</typeparam>
    public interface IRepositoryGeneric<TEntity, out TDbContext>
        where TDbContext : DbContext
        where TEntity : class
    {
        /// <summary>
        /// 取得該儲存庫的 DbContext
        /// </summary>
        /// <returns>DbContext</returns>
        TDbContext GetDbContext();

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">資料實體</param>
        /// <returns>新增成功的資料</returns>
        TEntity Create(TEntity entity);

        /// <summary>
        /// 新增(非同步方法)
        /// </summary>
        /// <param name="entity">資料實體</param>
        /// <returns>新增成功的資料</returns>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">資料實體</param>
        void Update(TEntity entity);

        /// <summary>
        /// 更新(非同步方法)
        /// </summary>
        /// <param name="entity">資料實體</param>
        /// <returns>非同步作業</returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="entity">資料實體</param>
        void Delete(TEntity entity);

        /// <summary>
        /// 刪除(非同步方法)
        /// </summary>
        /// <param name="entity">資料實體</param>
        /// <returns>非同步作業</returns>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// 取得單一筆資料
        /// </summary>
        /// <param name="predicate">查詢條件</param>
        /// <returns>資料實體</returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 取得單一筆資料(非同步方法)
        /// </summary>
        /// <param name="predicate">查詢條件</param>
        /// <returns>資料實體</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 取得單一筆包含指定內容與查詢條件的資料
        /// </summary>
        /// <param name="predicate">查詢條件</param>
        /// <param name="includes">包含內容</param>
        /// <returns>資料實體</returns>
        TEntity GetInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// 取得單一筆包含指定內容與查詢條件的資料(非同步方法)
        /// </summary>
        /// <param name="predicate">查詢條件</param>
        /// <param name="includes">包含內容</param>
        /// <returns>資料實體</returns>
        Task<TEntity> GetIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// 取得指定包含內容的所有資料
        /// </summary>
        /// <param name="includes">包含內容</param>
        /// <returns>資料實體集合</returns>
        List<TEntity> GetAllInclude(params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// 取得指定包含內容的所有資料(非同步方法)
        /// </summary>
        /// <param name="includes">包含內容</param>
        /// <returns>資料實體集合</returns>
        Task<List<TEntity>> GetAllIncludeAsync(params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns>資料實體集合</returns>
        List<TEntity> GetAll();

        /// <summary>
        /// 取得所有資料(非同步方法)
        /// </summary>
        /// <returns>資料實體集合</returns>
        Task<List<TEntity>> GetAllAsync();
    }
}