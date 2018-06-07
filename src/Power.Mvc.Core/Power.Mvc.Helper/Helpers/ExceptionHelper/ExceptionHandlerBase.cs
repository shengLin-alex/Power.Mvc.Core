using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 例外處理器基底類別
    /// </summary>
    /// <typeparam name="TResult">結果的型別</typeparam>
    public abstract class ExceptionHandlerBase<TResult> : IExceptionHandler<TResult>
    {
        /// <summary>
        /// 處理例外，不回傳值，可指定多種處理方法
        /// </summary>
        /// <param name="target">目標例外</param>
        public virtual void HandleException(Exception target)
        {
            Type exceptionType = target.GetType();

            // 取得這個類別內掛上HandleException 且為 void 的方法
            List<MethodInfo> methods = this.GetType()
                                           .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                           .Where(m =>
                                            {
                                                HandleExceptionAttribute attribute = m.GetCustomAttribute<HandleExceptionAttribute>();

                                                if (attribute == null)
                                                {
                                                    return false;
                                                }

                                                if (attribute.ExactExceptionType != null && !attribute.IsHandleParent)
                                                {
                                                    return attribute.ExactExceptionType == exceptionType &&
                                                           m.ReturnType == typeof(void);
                                                }

                                                return attribute.ParentExceptionType != null &&
                                                       exceptionType.IsSubclassOf(attribute.ParentExceptionType) &&
                                                       m.ReturnType == typeof(void);
                                            })
                                           .ToList();

            if (methods.Any())
            {
                methods.ForEach(m => m.Invoke(this, new object[] { target }));
            }
            else
            {
                this.GeneralHandle(target);
            }
        }

        /// <summary>
        /// 處理例外，回傳指定結果，只接受單一種方法
        /// </summary>
        /// <param name="target">目標例外</param>
        /// <returns>處理結果</returns>
        public virtual TResult HandleExceptionResult(Exception target)
        {
            Type exceptionType = target.GetType();

            // 取得這個類別內掛上HandleException 且為回傳 TResult 的方法
            MethodInfo method = this.GetType()
                                    .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                    .FirstOrDefault(m =>
                                     {
                                         HandleExceptionAttribute attribute = m.GetCustomAttribute<HandleExceptionAttribute>();

                                         if (attribute == null)
                                         {
                                             return false;
                                         }

                                         if (attribute.ExactExceptionType != null && !attribute.IsHandleParent)
                                         {
                                             return attribute.ExactExceptionType == exceptionType &&
                                                    m.ReturnType == typeof(TResult);
                                         }

                                         return attribute.ParentExceptionType != null &&
                                                exceptionType.IsSubclassOf(attribute.ParentExceptionType) &&
                                                m.ReturnType == typeof(TResult);
                                     });

            if (method != null)
            {
                return (TResult)method.Invoke(this, new object[] { target });
            }

            return this.GeneralHandleResult(target);
        }

        /// <summary>
        /// 例外處理器類別中未有指定方法時所呼叫之無回傳值泛用處理
        /// </summary>
        /// <param name="target">目標例外</param>
        protected abstract void GeneralHandle(Exception target);

        /// <summary>
        /// 例外處理器類別中未有指定方法時所呼叫之回傳 TResult 泛用處理
        /// </summary>
        /// <param name="target">目標例外</param>
        /// <returns>結果</returns>
        protected abstract TResult GeneralHandleResult(Exception target);
    }
}