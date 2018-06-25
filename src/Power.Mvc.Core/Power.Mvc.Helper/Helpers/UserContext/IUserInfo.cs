namespace Power.Mvc.Helper
{
    /// <summary>
    /// 使用者資訊
    /// </summary>
    public interface IUserInfo
    {
        /// <summary>
        /// 辨識使用者之編號
        /// </summary>
        string UserId { get; set; }
    }
}