namespace Power.Mvc.Helper.Models
{
    /// <summary>
    /// 資料表識別介面
    /// </summary>
    /// <typeparam name="TId">編號類型</typeparam>
    public interface ITableIdentity<TId>
    {
        /// <summary>
        /// 編號
        /// </summary>
        TId Id { get; set; }
    }
}