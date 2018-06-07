using Autofac;
using System;
using System.Collections.Generic;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 於套件使用之相依性解析，使用單例模式
    /// </summary>
    public class PackageDiResolver : IPackageDiResolver
    {
        /// <summary>
        /// 用於鎖定執行序
        /// </summary>
        private static readonly object SyncRoot = new object();

        /// <summary>
        /// DiResolverInstance，延遲載入
        /// </summary>
        private static Lazy<PackageDiResolver> DiResolverInstance;

        /// <summary>
        /// 相依性注入容器
        /// </summary>
        private IContainer ContainerInstance;

        /// <summary>
        /// 使用私有建構子
        /// </summary>
        private PackageDiResolver()
        {
        }

        /// <summary>
        /// 取得相依性解析程式的實作。
        /// </summary>
        /// <returns>相依性解析程式的實作。</returns>
        public static IPackageDiResolver Current
        {
            get
            {
                if (DiResolverInstance == null || !DiResolverInstance.IsValueCreated)
                {
                    lock (SyncRoot)
                    {
                        DiResolverInstance = new Lazy<PackageDiResolver>(() => new PackageDiResolver());
                    }
                }

                return DiResolverInstance.Value;
            }
        }

        /// <summary>
        /// 設定注入容器
        /// </summary>
        /// <param name="container">IContainer</param>
        public void SetAutofacContainer(IContainer container)
        {
            this.ContainerInstance = container;
        }

        /// <summary>
        /// 解析支援任意物件建立的單一註冊服務。
        /// </summary>
        /// <returns>要求的服務或物件。</returns>
        /// <param name="serviceType">要求之服務或物件的型別。</param>
        public object GetService(Type serviceType)
        {
            return this.ContainerInstance.ResolveOptional(serviceType);
        }

        /// <summary>
        /// 解析支援任意物件建立的單一註冊服務。使用 Key 參數取得指定註冊服務
        /// </summary>
        /// <param name="serviceType">要求之服務或物件的型別。</param>
        /// <param name="key">篩選鍵值</param>
        /// <returns>要求的服務或物件。</returns>
        public object GetService(Type serviceType, object key)
        {
            return this.ContainerInstance.ResolveKeyed(key, serviceType);
        }

        /// <summary>
        /// 解析多次註冊的服務。
        /// </summary>
        /// <returns>要求的服務。</returns>
        /// <param name="serviceType">要求之服務的型別。</param>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return (IEnumerable<object>)this.ContainerInstance.Resolve(typeof(IEnumerable<>).MakeGenericType(serviceType));
        }
    }
}