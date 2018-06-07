using System;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 例外處理器介面
    /// </summary>
    /// <typeparam name="TResult">結果的型別</typeparam>
    public interface IExceptionHandler<out TResult>
    {
        /// <summary>
        /// 處理例外，不回傳值，可指定多種處理方法
        /// </summary>
        /// <param name="target">目標例外</param>
        void HandleException(Exception target);

        /// <summary>
        /// 處理例外，回傳指定結果，只接受單一方法呼叫
        /// </summary>
        /// <param name="target">目標例外</param>
        /// <returns>處理結果</returns>
        TResult HandleExceptionResult(Exception target);
    }
}