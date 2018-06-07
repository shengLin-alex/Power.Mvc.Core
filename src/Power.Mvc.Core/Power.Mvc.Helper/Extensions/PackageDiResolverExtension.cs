using System.Collections.Generic;
using System.Linq;

namespace Power.Mvc.Helper.Extensions
{
    /// <summary>
    /// 於套件使用之相依性解析之擴充方法
    /// </summary>
    public static class PackageDiResolverExtension
    {
        /// <summary>
        /// 解析支援任意物件建立的單一註冊服務。
        /// </summary>
        /// <returns>要求的服務或物件。</returns>
        /// <param name="resolver">此方法擴充的相依性解析程式執行個體。</param>
        /// <typeparam name="TService">要求之服務或物件的型別。</typeparam>
        public static TService GetService<TService>(this IPackageDiResolver resolver)
        {
            return (TService)resolver.GetService(typeof(TService));
        }

        /// <summary>
        /// 解析支援任意物件建立的單一註冊服務。使用 Key 參數取得指定註冊服務
        /// </summary>
        /// <typeparam name="TService">要求之服務或物件的型別</typeparam>
        /// <param name="resolver">此方法擴充的相依性解析程式執行個體</param>
        /// <param name="key">篩選鍵值</param>
        /// <returns>要求的服務或物件</returns>
        public static TService GetService<TService>(this IPackageDiResolver resolver, object key)
        {
            return (TService)resolver.GetService(typeof(TService), key);
        }

        /// <summary>
        /// 解析多次註冊的服務。
        /// </summary>
        /// <returns>要求的服務。</returns>
        /// <param name="resolver">此方法擴充的相依性解析程式執行個體。</param>
        /// <typeparam name="TService">要求之服務的型別。</typeparam>
        public static IEnumerable<TService> GetServices<TService>(this IPackageDiResolver resolver)
        {
            return resolver.GetServices(typeof(TService)).Cast<TService>();
        }
    }
}