namespace Power.Mvc.Helper.Models
{
    /// <summary>
    /// 使用者帳號資訊
    /// </summary>
    public class AccountInfo : ITableIdentity<string>
    {
        /// <summary>
        /// 編號
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }
    }
}