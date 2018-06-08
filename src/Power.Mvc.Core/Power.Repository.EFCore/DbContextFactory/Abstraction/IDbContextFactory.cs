using Microsoft.EntityFrameworkCore;

namespace Power.Repository.EFCore
{
    /// <summary>
    /// Db 連線工廠
    /// </summary>
    public interface IDbContextFactory<out TDbContext> where TDbContext : DbContext
    {
        /// <summary>
        /// 取得 DbContext
        /// </summary>
        TDbContext DbContext { get; }
    }
}