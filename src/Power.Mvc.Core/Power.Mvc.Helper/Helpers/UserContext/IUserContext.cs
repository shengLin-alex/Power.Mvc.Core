namespace Power.Mvc.Helper
{
    /// <summary>
    /// 使用者資訊上下文介面
    /// </summary>
    /// <typeparam name="TUser">使用者資訊型別</typeparam>
    public interface IUserContext<out TUser> where TUser : IUserInfo
    {
        /// <summary>
        /// 取得當前使用者
        /// </summary>
        /// <returns>使用者資訊</returns>
        TUser Current { get; }
    }
}