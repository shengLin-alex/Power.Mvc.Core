using System.Collections.Generic;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 泛型資料模型驗證介面
    /// </summary>
    /// <typeparam name="TModel">檢驗物件的型別</typeparam>
    public interface IModelValidator<in TModel> : IModelValidator where TModel : class
    {
        /// <summary>
        /// 檢驗指定資料模型是否有效
        /// </summary>
        /// <param name="model">指定 Model 物件</param>
        /// <returns>錯誤訊息之串列</returns>
        List<string> Validate(TModel model);
    }
}