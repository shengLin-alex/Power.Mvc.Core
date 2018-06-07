using System.Text;

namespace Power.Mvc.Helper.Extensions
{
    /// <summary>
    /// 字串擴充方法
    /// </summary>
    public static class StringExtension
    {
        /// <summary>是否為NULL或是空值</summary>
        /// <param name="source">目標字串</param>
        /// <returns>判斷結果</returns>
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        /// <summary>
        /// 新執行個體初始化為由重複指定次數的指定字串所指示的值。
        /// </summary>
        /// <param name="stringToRepeat">字串</param>
        /// <param name="repeat">重複指定次數</param>
        /// <returns>新字串</returns>
        public static string Repeat(this string stringToRepeat, int repeat)
        {
            StringBuilder builder = new StringBuilder(repeat * stringToRepeat.Length);
            for (int i = 0; i < repeat; i++)
            {
                builder.Append(stringToRepeat);
            }

            return builder.ToString();
        }

        /// <summary>
        /// 字串轉小駝峰
        /// </summary>
        /// <param name="str">字串來源</param>
        /// <returns>轉換結果</returns>
        public static string ToCamelCase(this string str)
        {
            if (!str.IsNullOrEmpty() && str.Length > 1)
            {
                return char.ToLowerInvariant(str[0]) + str.Substring(1);
            }

            return str;
        }
    }
}