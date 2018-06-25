using Power.Mvc.Helper.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 資料模型驗證
    /// <para>子類可繼承<see cref="ModelValidatorBase{TModel}"/>，並且定義附加<see cref="ValidatorRuleAttribute"/>之公開或保護存取的<see cref="Void"/>方法。</para>
    /// <para>必須於方法中定義單一參數，型別為<see cref="TModel"/></para>
    /// </summary>
    /// <typeparam name="TModel">資料模型類別</typeparam>
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

                                                     return attribute != null &&
                                                            attribute.ValidatorPriority.Equals(ValidatorPriority.Necessary) &&
                                                            m.ReturnType == typeof(void) &&
                                                            m.GetParameters().Count() == 1 &&
                                                            m.GetParameters().First().ParameterType == typeof(TModel);
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

                                                     return attribute != null &&
                                                            attribute.ValidatorPriority.Equals(ValidatorPriority.Skippable) &&
                                                            m.ReturnType == typeof(void) &&
                                                            m.GetParameters().Count() == 1 &&
                                                            m.GetParameters().First().ParameterType == typeof(TModel);
                                                 })
                                                .OrderBy(m => m.GetCustomAttribute<ValidatorRuleAttribute>()?.Seq);

            return rules;
        }
    }
}