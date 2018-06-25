using System.Collections.Generic;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 資料模型驗證介面
    /// </summary>
    public interface IModelValidator
    {
        /// <summary>
        /// 檢驗指定資料模型是否有效
        /// </summary>
        /// <param name="model">指定 Model 物件</param>
        /// <returns>錯誤訊息之串列</returns>
        List<string> Validate(object model);
    }
}