using System;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 表示處理指定例外
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class HandleExceptionAttribute : Attribute
    {
        /// <summary>
        /// 建構子，若 IsHandleParent為 ture，則處理該指定例外包含之所有延伸例外類型
        /// </summary>
        /// <param name="exceptionType">例外類型</param>
        /// <param name="isHandleParent">是否為處理父類例外，預設為 false</param>
        public HandleExceptionAttribute(Type exceptionType, bool isHandleParent = false)
        {
            this.IsHandleParent = isHandleParent;

            if (isHandleParent)
            {
                this.ParentExceptionType = exceptionType;
            }
            else
            {
                this.ExactExceptionType = exceptionType;
            }
        }

        /// <summary>
        /// 是否為處理父類例外
        /// </summary>
        public bool IsHandleParent { get; }

        /// <summary>
        /// 指定單一例外類型
        /// </summary>
        public Type ExactExceptionType { get; }

        /// <summary>
        /// 指定父類例外類型(表示一併處理延伸例外類型)
        /// </summary>
        public Type ParentExceptionType { get; }
    }
}