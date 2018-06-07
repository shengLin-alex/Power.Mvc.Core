using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Features.Scanning;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Power.Mvc.Helper.Extensions
{
    /// <summary>
    /// AutofacLogExtension
    /// </summary>
    public static class AutofacLogExtension
    {
        /// <summary>
        /// 實際註冊的型別
        /// </summary>
        private static readonly List<string> ActualRegistration = new List<string>();

        /// <summary>
        /// 非預期註冊的型別
        /// </summary>
        private static readonly List<string> ExpectRegistration = new List<string>();

        /// <summary>
        /// DI Container 紀錄
        /// </summary>
        private static readonly List<string> ContainerLog = new List<string>();

        /// <summary>
        /// 紀錄非預期註冊型別
        /// </summary>
        /// <param name="builder">Used to build an Autofac.IContainer from component registrations.</param>
        public static void DumpUnexpectRegistration(this ContainerBuilder builder)
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IocRegistrationLog");
            List<string> logContent = ActualRegistration.Except(ExpectRegistration).ToList();

            try
            {
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                int unexpectedCount = logContent.Count;

                logContent.Insert(0, DateTime.Now.ToString(CultureInfo.InvariantCulture));

                if (unexpectedCount > 0)
                {
                    logContent.Insert(1, "非預期註冊型別：");
                }
                else
                {
                    logContent.Add("沒有非預期註冊的型別。");
                }

                logContent.Add(Environment.NewLine);
            }
            catch (Exception exception)
            {
                logContent.Add(exception.Message);
            }

            System.IO.File.AppendAllLines(System.IO.Path.Combine(path, $"{DateTime.Now:yyyy-MM-dd}.log"), logContent, Encoding.UTF8);
        }

        /// <summary>
        /// Dump DI 容器紀錄
        /// </summary>
        /// <param name="container">IContainer</param>
        public static void DumpContainerLog(this IContainer container)
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IocContainerLog");
            List<string> logContent = new List<string>();

            try
            {
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                logContent.Insert(0, DateTime.Now.ToString(CultureInfo.InvariantCulture));
                logContent.AddRange(ContainerLog);
                logContent.Add(Environment.NewLine);
            }
            catch (Exception exception)
            {
                logContent.Add(exception.Message);
            }

            System.IO.File.AppendAllLines(System.IO.Path.Combine(path, $"{DateTime.Now:yyyy-MM-dd}.log"), logContent, Encoding.UTF8);
            ContainerLog.Clear();
        }

        #region Registration Log

        /// <summary>
        /// 紀錄非預期註冊型別
        /// </summary>
        /// <param name="registion">Describes a logical component within the container.</param>
        public static void LogActualRegistration(this IComponentRegistration registion)
        {
            ActualRegistration.Add(registion.Activator.LimitType.FullName);
        }

        /// <summary>
        /// 紀錄非預期註冊型別
        /// </summary>
        /// <typeparam name="TLimit">
        /// The most specific type to which instances of the registration can be cast.
        /// </typeparam>
        /// <typeparam name="TActivatorData">Activator builder type.</typeparam>
        /// <typeparam name="TSingleRegistrationStyle">Registration style type.</typeparam>
        /// <param name="registration">Data structure used to construct registrations.</param>
        public static void LogExpectRegistration<TLimit, TActivatorData, TSingleRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> registration) where TSingleRegistrationStyle : SingleRegistrationStyle
        {
            registration.OnRegistered(p => ExpectRegistration.Add(p.ComponentRegistration.Activator.LimitType.FullName));
        }

        /// <summary>
        /// 紀錄非預期註冊型別
        /// </summary>
        /// <typeparam name="TLimit">
        /// The most specific type to which instances of the registration can be cast.
        /// </typeparam>
        /// <typeparam name="TRegistrationStyle">Registration style type.</typeparam>
        /// <param name="registration">Data structure used to construct registrations.</param>
        public static void LogExpectRegistration<TLimit, TRegistrationStyle>(this IRegistrationBuilder<TLimit, ScanningActivatorData, TRegistrationStyle> registration)
        {
            registration.OnRegistered(p => ExpectRegistration.Add(p.ComponentRegistration.Activator.LimitType.FullName));
        }

        /// <summary>
        /// 紀錄非預期註冊型別
        /// </summary>
        /// <typeparam name="TLimit">
        /// The most specific type to which instances of the registration can be cast.
        /// </typeparam>
        /// <typeparam name="TConcreteActivatorData">Activator builder type.</typeparam>
        /// <param name="registration">Data structure used to construct registrations.</param>
        public static void LogExpectRegistration<TLimit, TConcreteActivatorData>(this IRegistrationBuilder<TLimit, TConcreteActivatorData, SingleRegistrationStyle> registration)
        {
            registration.OnRegistered(p => ExpectRegistration.Add(p.ComponentRegistration.Activator.LimitType.FullName));
        }

        /// <summary>
        /// 紀錄非預期註冊型別
        /// </summary>
        /// <typeparam name="T">Log the full name of the type of T</typeparam>
        /// <param name="builder">Used to build an Autofac.IContainer from component registrations.</param>
        public static void LogExpectRegistration<T>(this ContainerBuilder builder)
        {
            ExpectRegistration.Add(typeof(T).FullName);
        }

        #endregion Registration Log

        #region Container Log

        /// <summary>
        /// 紀錄容器是否已經建構
        /// </summary>
        /// <param name="container">IContainer</param>
        /// <param name="message">其他訊息</param>
        /// <param name="args">訊息格式化參數</param>
        public static void LogContainerInstance(this IContainer container, string message, params object[] args)
        {
            ContainerLog.Add($"IContainer is already an instance of Container : { container is Container }.");
            ContainerLog.Add(string.Format(message, args));
        }

        #endregion Container Log
    }
}