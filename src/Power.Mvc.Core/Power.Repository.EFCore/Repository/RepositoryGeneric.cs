using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Power.Repository.EFCore
{
    /// <summary>
    /// 泛型儲存庫
    /// </summary>
    /// <typeparam name="TEntity">實體資料型別</typeparam>
    /// <typeparam name="TDbContext">DbContext 實際型別</typeparam>
    public class RepositoryGeneric<TEntity, TDbContext> : IRepositoryGeneric<TEntity, TDbContext>, IDisposable
        where TDbContext : DbContext
        where TEntity : class
    {
        /// <summary>
        /// DbContext 實例
        /// </summary>
        private TDbContext DbContextInstance;

        /// <summary>
        /// Track whether Dispose has been called.
        /// </summary>
        private bool Disposed;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">DbContext 工廠</param>
        public RepositoryGeneric(
            IDbContextFactory<TDbContext> factory)
        {
            this.DbContextInstance = factory.DbContext;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="RepositoryGeneric{TEntity,TContext}"/> class.
        /// </summary>
        ~RepositoryGeneric()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// 取得該儲存庫的 DbContext
        /// </summary>
        /// <returns>DbContext</returns>
        public TDbContext GetDbContext()
        {
            return this.DbContextInstance;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">資料實體</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>新增成功的資料</returns>
        public virtual TEntity Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            EntityEntry<TEntity> entityCreated = this.DbContextInstance.Set<TEntity>().Add(entity);
            this.DbContextInstance.SaveChanges();

            return entityCreated.Entity;
        }

        /// <summary>
        /// 新增(非同步方法)
        /// </summary>
        /// <param name="entity">資料實體</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>新增成功的資料</returns>
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            EntityEntry<TEntity> entityCreated = this.DbContextInstance.Set<TEntity>().Add(entity);
            await this.DbContextInstance.SaveChangesAsync();

            return entityCreated.Entity;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">資料實體</param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.DbContextInstance.Entry(entity).State = EntityState.Modified;
            this.DbContextInstance.SaveChanges();
        }

        /// <summary>
        /// 更新(非同步方法)
        /// </summary>
        /// <param name="entity">資料實體</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>非同步作業</returns>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.DbContextInstance.Entry(entity).State = EntityState.Modified;
            await this.DbContextInstance.SaveChangesAsync();
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="entity">資料實體</param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.DbContextInstance.Entry(entity).State = EntityState.Deleted;
            this.DbContextInstance.SaveChanges();
        }

        /// <summary>
        /// 刪除(非同步方法)
        /// </summary>
        /// <param name="entity">資料實體</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>非同步作業</returns>
        public virtual async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.DbContextInstance.Entry(entity).State = EntityState.Deleted;
            await this.DbContextInstance.SaveChangesAsync();
        }

        /// <summary>
        /// 取得單一筆資料
        /// </summary>
        /// <param name="predicate">查詢條件</param>
        /// <returns>資料實體</returns>
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbContextInstance.Set<TEntity>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 取得單一筆資料(非同步方法)
        /// </summary>
        /// <param name="predicate">查詢條件</param>
        /// <returns>資料實體</returns>
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.DbContextInstance.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// 取得單一筆包含指定內容與查詢條件的資料
        /// </summary>
        /// <param name="predicate">查詢條件</param>
        /// <param name="includes">包含內容</param>
        /// <returns>資料實體</returns>
        public TEntity GetInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = this.DbContextInstance.Set<TEntity>().AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(
                    query,
                    (current, include) => current.Include(include));
            }

            return query.FirstOrDefault(predicate);
        }

        /// <summary>
        /// GetIncludeAsync
        /// </summary>
        /// <param name="predicate">查詢條件</param>
        /// <param name="includes">包含內容</param>
        /// <returns>資料實體</returns>
        public async Task<TEntity> GetIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = this.DbContextInstance.Set<TEntity>().AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(
                    query,
                    (current, include) => current.Include(include));
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// 取得指定包含內容的所有資料
        /// </summary>
        /// <param name="includes">包含內容</param>
        /// <returns>資料實體集合</returns>
        public List<TEntity> GetAllInclude(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = this.DbContextInstance.Set<TEntity>().AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(
                    query,
                    (current, include) => current.Include(include));
            }

            return query.ToList();
        }

        /// <summary>
        /// 取得指定包含內容的所有資料(非同步方法)
        /// </summary>
        /// <param name="includes">包含內容</param>
        /// <returns>資料實體集合</returns>
        public async Task<List<TEntity>> GetAllIncludeAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = this.DbContextInstance.Set<TEntity>().AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(
                    query,
                    (current, include) => current.Include(include));
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns>資料實體集合</returns>
        public List<TEntity> GetAll()
        {
            return this.DbContextInstance.Set<TEntity>().ToList();
        }

        /// <summary>
        /// 取得所有資料(非同步方法)
        /// </summary>
        /// <returns>資料實體集合</returns>
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await this.DbContextInstance.Set<TEntity>().ToListAsync();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            // This object will be cleaned up by the Dispose method. Therefore, you should call
            // GC.SupressFinalize to take this object off the finalization queue and prevent
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
                this.DbContextInstance.Dispose();
                this.DbContextInstance = null;
            }

            // Note disposing has been done.
            this.Disposed = true;
        }
    }
}