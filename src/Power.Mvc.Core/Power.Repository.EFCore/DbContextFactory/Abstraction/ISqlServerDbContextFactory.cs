namespace Power.Repository.EFCore
{
    /// <summary>
    /// SqlServerDbContext 工廠介面
    /// </summary>
    /// <typeparam name="TContext">DbContext 實際型別</typeparam>
    public interface ISqlServerDbContextFactory<out TContext> : IDbContextFactory<TContext> where TContext : SqlServerDbContextBase
    {
    }
}