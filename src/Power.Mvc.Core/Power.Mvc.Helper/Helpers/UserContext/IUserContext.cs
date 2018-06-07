namespace Power.Mvc.Helper
{
    /// <summary>
    /// 使用者資訊上下文介面
    /// </summary>
    public interface IUserContext
    {
        /// <summary>
        /// 使用者資訊
        /// </summary>
        UserInfo CurrentUser { get; }
    }
}