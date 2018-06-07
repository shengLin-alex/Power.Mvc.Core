using System.Collections.Generic;

namespace Power.Mvc.Helper.Extensions
{
    /// <summary>
    /// List with type of string 擴充方法
    /// </summary>
    public static class StringListExtension
    {
        /// <summary>
        /// 合併字串集合
        /// </summary>
        /// <param name="lines">字串集合</param>
        /// <param name="lineEndWith">結尾符號</param>
        /// <returns>新字串</returns>
        public static string Combine(this List<string> lines, string lineEndWith)
        {
            string result = string.Empty;
            int count = lines.Count;

            foreach (string line in lines)
            {
                result += line.Trim();
                if (--count > 0)
                {
                    result += lineEndWith;
                }
            }

            return result;
        }
    }
}