using System;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 自訂的 Assert 工具
    /// </summary>
    public static class AssertHelper
    {
        /// <summary>
        /// 判斷條件是否成立，不成立就拋出指定的例外類別與錯誤訊息
        /// </summary>
        /// <typeparam name="TExceptionType">例外類別</typeparam>
        /// <param name="condition">條件</param>
        /// <param name="message">條件不成立的錯誤訊息</param>
        /// <param name="callback">Assert 失敗時引動之委派</param>
        public static void IsTrue<TExceptionType>(bool condition, string message, Action callback = null) where TExceptionType : Exception, new()
        {
            if (condition)
            {
                return;
            }

            callback?.Invoke();
            TExceptionType exception = (TExceptionType)Activator.CreateInstance(typeof(TExceptionType), message);
            throw exception;
        }

        /// <summary>
        /// 判斷條件是否成立，不成立就拋出指定的例外類別與錯誤訊息(使用委派參數)
        /// </summary>
        /// <typeparam name="TExceptionType">例外類別</typeparam>
        /// <param name="condition">條件委派</param>
        /// <param name="message">條件不成立的錯誤訊息</param>
        /// <param name="callback">Assert 失敗時引動之委派</param>
        public static void IsTrue<TExceptionType>(Func<bool> condition, string message, Action callback = null) where TExceptionType : Exception, new()
        {
            if (condition())
            {
                return;
            }

            callback?.Invoke();
            TExceptionType exception = (TExceptionType)Activator.CreateInstance(typeof(TExceptionType), message);
            throw exception;
        }
    }
}