using Autofac;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// AspNet.Core 物件註冊介面
    /// </summary>
    public interface ITypeRegister
    {
        /// <summary>
        /// 註冊排序
        /// </summary>
        int Seq { get; }

        /// <summary>
        /// 註冊
        /// </summary>
        /// <param name="builder">ContainerBuilder</param>
        void RegisterTypes(ContainerBuilder builder);
    }
}