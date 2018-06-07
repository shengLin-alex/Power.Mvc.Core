using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Power.Mvc.Helper.Extensions
{
    /// <summary>
    /// Logger 擴充方法
    /// </summary>
    public static class LoggerExtension
    {
        /// <summary>
        /// 留白的長度
        /// </summary>
        private const int PaddingSize = 4;

        /// <summary>
        /// 分隔符號
        /// </summary>
        private const string SeperatorChar = "#";

        /// <summary>
        /// 空白
        /// </summary>
        private const string Space = " ";

        /// <summary>
        /// 開始除錯
        /// </summary>
        /// <param name="logger">Logger</param>
        public static void Start(this ILogger logger)
        {
            logger.LogInformation(SeperatorChar.Repeat(80));
        }

        /// <summary>
        /// Dump 物件內容
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="object">要 Dump 的物件</param>
        /// <param name="description">說明</param>
        public static void Dump(this ILogger logger, object @object, string description)
        {
            if (@object == null)
            {
                return;
            }

            try
            {
                logger.LogInformation(description);
                Dump(logger, @object);
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
            }
        }

        /// <summary>
        /// Dump 物件內容
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="object">要 Dump 的物件</param>
        public static void Dump(this ILogger logger, object @object)
        {
            try
            {
                if (@object == null)
                {
                    return;
                }

                Type type = @object.GetType();
                logger.LogInformation("{");

                foreach (PropertyInfo propertyInfo in type.GetProperties())
                {
                    string line = Space.Repeat(PaddingSize) + propertyInfo.Name;

                    if (propertyInfo.CanRead)
                    {
                        object value = propertyInfo.GetValue(@object, null);
                        line += " => " + value;
                    }

                    logger.LogInformation(line);
                }

                logger.LogInformation("}");
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
            }
        }

        /// <summary>
        /// Dump List 內容
        /// </summary>
        /// <typeparam name="T">List 內容的實際型別</typeparam>
        /// <param name="logger">Logger</param>
        /// <param name="list">要 Dump 的 List</param>
        public static void Dump<T>(this ILogger logger, List<T> list)
        {
            if (list == null)
            {
                return;
            }

            try
            {
                foreach (T item in list)
                {
                    logger.LogInformation(item.ToString());
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
            }
        }

        /// <summary>
        /// 進入方法
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="className">類別名稱</param>
        /// <param name="methodName">方法名稱</param>
        public static void EnterMethod(this ILogger logger, string className, string methodName)
        {
            string enterMark = "==>  ";

            string fixedClassName = className;
            if (!methodName.Equals(".ctor") && !methodName.Equals(".cctor"))
            {
                if (!fixedClassName.EndsWith("."))
                {
                    fixedClassName += ".";
                }
            }

            logger.LogInformation(enterMark + fixedClassName + methodName);
        }

        /// <summary>
        /// 離開執行方法
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="methodName">方法名稱</param>
        public static void LeaveMethod(this ILogger logger, string methodName)
        {
            string leaveMark = "<==  ";
            logger.LogInformation(leaveMark + methodName);
        }
    }
}