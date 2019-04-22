using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;

namespace Power.Mvc.Helper.Extensions
{
    /// <summary>
    /// Autofac 註冊擴充方法
    /// </summary>
    public static class AutofacRegistrarExtension
    {
        /// <summary>
        /// 註冊 Web 模組
        /// </summary>
        /// <param name="builder">Used to build an <see cref="T:Autofac.IContainer" /> from component registrations.</param>
        /// <param name="moduleJson">Web 模組的設定 json 檔案名稱(路徑)</param>
        public static void RegisterWebModule(this ContainerBuilder builder, string moduleJson = "webmodule.json")
        {
            // ConfigurationBuilder for build json config
            ConfigurationBuilder config = new ConfigurationBuilder();
            config.AddJsonFile(moduleJson);

            // Register the ConfigurationModule with Autofac.
            ConfigurationModule module = new ConfigurationModule(config.Build());
            builder.RegisterModule(module);
        }
    }
}