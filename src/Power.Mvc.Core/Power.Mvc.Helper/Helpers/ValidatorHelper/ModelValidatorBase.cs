using Power.Mvc.Helper.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// Model 驗證器基底類別，所有延伸驗證器皆繼承此類
    /// </summary>
    /// <typeparam name="TModel">欲驗證的模型型別</typeparam>
    public class ModelValidatorBase<TModel> : IModelValidator<TModel> where TModel : class
    {
        /// <summary>
        /// 檢驗指定 Model 是否有效
        /// </summary>
        /// <param name="model">指定 Model 物件</param>
        /// <returns>錯誤訊息之串列</returns>
        public List<string> Validate(object model)
        {
            return this.Validate((TModel)model);
        }

        /// <summary>
        /// 檢驗指定 Model 是否有效
        /// </summary>
        /// <param name="model">指定 Model 物件</param>
        /// <returns>錯誤訊息之串列</returns>
        public List<string> Validate(TModel model)
        {
            List<string> messages = new List<string>();
            IEnumerable<MethodInfo> necessaryRules = this.GetNecessaryRules();
            IEnumerable<MethodInfo> skippableRules = this.GetSkippableRules();

            // 先執行必須驗證的規則
            foreach (MethodInfo necessaryRule in necessaryRules)
            {
                try
                {
                    necessaryRule.Invoke(this, new object[] { model });
                }
                catch (Exception exception)
                {
                    messages.Add($"Necessary rule failed: {{{ BuildMessage(exception) }}}");
                }
            }

            // 再對可跳過的規則驗證
            foreach (MethodInfo skippableRule in skippableRules)
            {
                try
                {
                    skippableRule.Invoke(this, new object[] { model });
                }
                catch (Exception exception)
                {
                    messages.Add($"Skippable rule failed: {{{ BuildMessage(exception) }}}");

                    // 一有驗證失敗就跳出
                    break;
                }
            }

            // local function，將 exception 展開取得訊息
            string BuildMessage(Exception exception)
            {
                IEnumerable<string> message = exception.FromHierarchy(ex => ex.InnerException).Select(ex => ex.Message);

                return string.Join(" => InnerException : ", message);
            }

            return messages;
        }

        /// <summary>
        /// 取得必要的驗證規則方法
        /// </summary>
        /// <returns>必要的驗證規則方法集合</returns>
        protected virtual IEnumerable<MethodInfo> GetNecessaryRules()
        {
            // 取得標記驗證規則屬性，且設定為必要、無回傳值之方法
            IEnumerable<MethodInfo> rules = this.GetType()
                                                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                                .Where(m =>
                                                 {
                                                     ValidatorRuleAttribute attribute = m.GetCustomAttribute<ValidatorRuleAttribute>();

                                                     if (attribute == null)
                                                     {
                                                         return false;
                                                     }

                                                     if (attribute.ValidatorPriority.Equals(ValidatorPriority.Necessary) && m.ReturnType == typeof(void))
                                                     {
                                                         return true;
                                                     }

                                                     return false;
                                                 })
                                                .OrderBy(m => m.GetCustomAttribute<ValidatorRuleAttribute>()?.Seq);

            return rules;
        }

        /// <summary>
        /// 取得可被跳過之驗證規則方法
        /// </summary>
        /// <returns>可被跳過之驗證規則方法集合</returns>
        protected virtual IEnumerable<MethodInfo> GetSkippableRules()
        {
            // 取得標記驗證規則屬性，且設定為可跳過、無回傳值之方法
            IEnumerable<MethodInfo> rules = this.GetType()
                                                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                                .Where(m =>
                                                 {
                                                     ValidatorRuleAttribute attribute = m.GetCustomAttribute<ValidatorRuleAttribute>();

                                                     if (attribute == null)
                                                     {
                                                         return false;
                                                     }

                                                     if (attribute.ValidatorPriority.Equals(ValidatorPriority.Skippable) && m.ReturnType == typeof(void))
                                                     {
                                                         return true;
                                                     }

                                                     return false;
                                                 })
                                                .OrderBy(m => m.GetCustomAttribute<ValidatorRuleAttribute>()?.Seq);

            return rules;
        }
    }
}