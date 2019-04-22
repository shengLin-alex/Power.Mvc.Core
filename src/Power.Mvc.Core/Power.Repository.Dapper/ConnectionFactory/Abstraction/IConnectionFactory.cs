using System.Data;

namespace Power.Repository.Dapper
{
    /// <summary>
    /// Db 連線工廠介面
    /// </summary>
    public interface IConnectionFactory
    {
        /// <summary>
        /// 建立 Db 連線
        /// </summary>
        /// <returns> Db 連線 </returns>
        IDbConnection Connection { get; }
    }
}