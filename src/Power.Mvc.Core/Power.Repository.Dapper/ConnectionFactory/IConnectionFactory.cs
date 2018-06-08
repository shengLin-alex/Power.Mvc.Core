using System.Data;

namespace Power.Repository.Dapper
{
    public interface IConnectionFactory
    {
        /// <summary>
        /// 建立 Db 連線
        /// </summary>
        /// <returns> Db 連線 </returns>
        IDbConnection Connection { get; }
    }
}