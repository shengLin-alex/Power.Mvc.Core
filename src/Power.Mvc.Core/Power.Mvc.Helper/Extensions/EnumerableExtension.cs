using System;
using System.Collections.Generic;
using System.Linq;

namespace Power.Mvc.Helper.Extensions
{
    /// <summary>
    /// IEnumerable 擴充方法
    /// </summary>
    public static class EnumerableExtension
    {
        /// <summary>
        /// Lambda 式 foreach
        /// </summary>
        /// <typeparam name="T">集合的個別物件型別</typeparam>
        /// <param name="source">來源集合</param>
        /// <param name="action">執行的單一參數方法</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
            {
                action.Invoke(item);
            }
        }

        /// <summary>
        /// 包含索引
        /// </summary>
        /// <typeparam name="T">型別T</typeparam>
        /// <param name="self">自己</param>
        /// <returns>含索引的清單</returns>
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
        {
            return self.Select((item, index) => (item, index));
        }

        /// <summary>
        /// 將一個值塞入集合的最前端
        /// </summary>
        /// <typeparam name="T">值的型別</typeparam>
        /// <param name="enumerable">集合</param>
        /// <param name="value">值</param>
        /// <returns>塞入後的集合</returns>
        public static IEnumerable<T> PushFront<T>(this IEnumerable<T> enumerable, T value)
        {
            yield return value;

            foreach (T fallow in enumerable)
            {
                yield return fallow;
            }
        }

        /// <summary>
        /// 將一個值塞入集合的最後端
        /// </summary>
        /// <typeparam name="T">值的型別</typeparam>
        /// <param name="enumerable">集合</param>
        /// <param name="value">值</param>
        /// <returns>塞入後的集合</returns>
        public static IEnumerable<T> PushBack<T>(this IEnumerable<T> enumerable, T value)
        {
            foreach (T fallow in enumerable)
            {
                yield return fallow;
            }

            yield return value;
        }
    }
}