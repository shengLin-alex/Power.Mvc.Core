using Microsoft.EntityFrameworkCore;

namespace Power.Repository.EFCore
{
    /// <summary>
    /// MySqlDbContext 工廠介面
    /// </summary>
    /// <typeparam name="TContext">DbContext 實際型別</typeparam>
    public interface IMySqlDbContextFactory<out TContext> : IDbContextFactory<TContext> where TContext : DbContext
    {
    }
}