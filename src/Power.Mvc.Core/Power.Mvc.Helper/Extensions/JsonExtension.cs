using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.RegularExpressions;

namespace Power.Mvc.Helper.Extensions
{
    /// <summary>
    /// JSON 相關擴充方法
    /// </summary>
    public static class JsonExtension
    {
        /// <summary>物件轉JSON字串</summary>
        /// <param name="target">物件</param>
        /// <param name="isCamelCase">JSON鍵值是否為小駝峰，預設true</param>
        /// <returns>JSON字串</returns>
        public static string ToJson(this object target, bool isCamelCase = true)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = isCamelCase ? new CamelCasePropertyNamesContractResolver() : new DefaultContractResolver()
            };

            return JsonConvert.SerializeObject(target, Formatting.None, settings);
        }

        /// <summary>JSON字串轉物件</summary>
        /// <typeparam name="T">物件類型</typeparam>
        /// <param name="source">JSON字串</param>
        /// <returns>物件</returns>
        public static T ToTypedObject<T>(this string source)
        {
            // 去掉字串的前後空白、換行、tab 符號
            source = source?.Trim('\r', '\n', '\t', ' ');
            if (source.IsNullOrEmpty() || !Regex.IsMatch(source, @"^(\[|\{)(.|\n)*(\]|\})$", RegexOptions.Compiled))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(source);
        }
    }
}