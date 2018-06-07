using System;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 表示為驗證規則之方法
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidatorRuleAttribute : Attribute
    {
        /// <summary>
        /// 建構子，初始化該驗證規則的優先度與排序
        /// </summary>
        /// <param name="priority">驗證優先度</param>
        /// <param name="seq">驗證排序，預設為 0(必要的驗證規則不須排序)</param>
        public ValidatorRuleAttribute(ValidatorPriority priority, int seq = 0)
        {
            this.ValidatorPriority = priority;
            this.Seq = seq;
        }

        /// <summary>
        /// 驗證優先度
        /// </summary>
        public ValidatorPriority ValidatorPriority { get; }

        /// <summary>
        /// 驗證排序
        /// </summary>
        public int Seq { get; }
    }
}