using Autofac;
using System;
using System.Collections.Generic;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 於套件使用之相依性解析介面
    /// </summary>
    public interface IPackageDiResolver
    {
        /// <summary>
        /// 設定注入容器
        /// </summary>
        /// <param name="container">IContainer</param>
        void SetAutofacContainer(ILifetimeScope container);

        /// <summary>
        /// 解析支援任意物件建立的單一註冊服務。
        /// </summary>
        /// <returns>要求的服務或物件。</returns>
        /// <param name="serviceType">要求之服務或物件的型別。</param>
        object GetService(Type serviceType);

        /// <summary>
        /// 解析支援任意物件建立的單一註冊服務。使用 Key 參數取得指定註冊服務
        /// </summary>
        /// <param name="serviceType">要求之服務或物件的型別。</param>
        /// <param name="key">篩選鍵值</param>
        /// <returns>要求的服務或物件。</returns>
        object GetService(Type serviceType, object key);

        /// <summary>
        /// 解析多次註冊的服務。
        /// </summary>
        /// <returns>要求的服務。</returns>
        /// <param name="serviceType">要求之服務的型別。</param>
        IEnumerable<object> GetServices(Type serviceType);
    }
}