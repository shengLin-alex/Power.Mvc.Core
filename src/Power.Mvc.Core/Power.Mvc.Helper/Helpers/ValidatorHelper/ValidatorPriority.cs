namespace Power.Mvc.Helper
{
    /// <summary>
    /// 驗證優先度
    /// </summary>
    public enum ValidatorPriority
    {
        /// <summary>
        /// 必須執行
        /// </summary>
        Necessary = 1,

        /// <summary>
        /// 表示該方法驗證前已有某一規則驗證失敗，則此驗證方法可被跳過
        /// </summary>
        Skippable = 2
    }
}