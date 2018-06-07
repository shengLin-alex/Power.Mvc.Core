using System.Collections.Generic;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 對指定型別的 Model 類進行有效檢查
    /// </summary>
    public interface IModelValidator
    {
        /// <summary>
        /// 檢驗指定 Model 是否有效
        /// </summary>
        /// <param name="model">指定 Model 物件</param>
        /// <returns>錯誤訊息之串列</returns>
        List<string> Validate(object model);
    }
}