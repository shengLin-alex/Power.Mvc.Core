using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Power.Mvc.Helper.Extensions
{
    /// <summary>
    /// 通用物件擴充方法
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 將匿名類型的屬性轉為物件陣列
        /// </summary>
        /// <param name="anonymous">匿名類型物件</param>
        /// <returns>物件陣列</returns>
        public static object[] PropertiesToObjects(this object anonymous)
        {
            Type type = anonymous.GetType();
            PropertyInfo[] propertyInfos = type.GetProperties();

            return propertyInfos.Select(propertyInfo => propertyInfo.GetValue(anonymous, null)).ToArray();
        }

        /// <summary>
        /// 將物件轉為 Request Url
        /// </summary>
        /// <param name="request">Request 物件</param>
        /// <param name="separator">分隔符號</param>
        /// <param name="isCamelCase">是否為小駝峰</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns> Request Url </returns>
        public static string ToRequestUrl(this object request, string separator = ",", bool isCamelCase = true)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            // Get all properties on the object
            Dictionary<string, object> properties = request.GetType()
                                                           .GetProperties()
                                                           .Where(x => x.CanRead)
                                                           .Where(x => x.GetValue(request, null) != null)
                                                           .ToDictionary(x => isCamelCase ? x.Name.ToCamelCase() : x.Name, x => x.GetValue(request, null));

            // Get names for all IEnumerable properties (exclude string)
            List<string> propertyNames = properties.Where(x => !(x.Value is string) && x.Value is IEnumerable)
                                                   .Select(x => x.Key)
                                                   .ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (string key in propertyNames)
            {
                Type valueType = properties[key].GetType();
                Type valueElemType = valueType.IsGenericType
                    ? valueType.GetGenericArguments()[0]
                    : valueType.GetElementType();

                if (valueElemType != null && !valueElemType.IsPrimitive && valueElemType != typeof(string))
                {
                    continue;
                }

                if (properties[key] is IEnumerable enumerable)
                {
                    properties[key] = string.Join(separator, enumerable.Cast<object>());
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join(
                "&",
                properties.Select(
                    x => string.Concat(
                        Uri.EscapeDataString(x.Key),
                        "=",
                        Uri.EscapeDataString(x.Value.ToString()))));
        }

        /// <summary>
        /// 將階層物件(如 Exception)轉為展開的 IEnumerable
        /// </summary>
        /// <typeparam name="TSource">來源的物件型別</typeparam>
        /// <param name="source">來源</param>
        /// <param name="nextItem">取得下一個物件的委派</param>
        /// <param name="canContinue">是否繼續往下取得的委派</param>
        /// <returns>展開的物件集合</returns>
        public static IEnumerable<TSource> FromHierarchy<TSource>(
            this TSource source,
            Func<TSource, TSource> nextItem,
            Func<TSource, bool> canContinue)
        {
            for (TSource current = source; canContinue(current); current = nextItem(current))
            {
                yield return current;
            }
        }

        /// <summary>
        /// 將階層物件(如 Exception)轉為展開的 IEnumerable
        /// </summary>
        /// <typeparam name="TSource">來源的物件型別</typeparam>
        /// <param name="source">來源</param>
        /// <param name="nextItem">取得下一個物件的委派</param>
        /// <returns>展開的物件集合</returns>
        public static IEnumerable<TSource> FromHierarchy<TSource>(
            this TSource source,
            Func<TSource, TSource> nextItem)
            where TSource : class
        {
            return FromHierarchy(source, nextItem, s => s != null);
        }
    }
}