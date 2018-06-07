using System;

namespace Power.Mvc.Helper.Extensions
{
    /// <summary>
    /// 類型擴充方法
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// 檢查泛型型別是否為泛型父類之子類(t.IsSubClassOfRawGeneric(raw generic))
        /// </summary>
        /// <param name="source">欲檢查之型別</param>
        /// <param name="generic">欲比對之父類型別</param>
        /// <returns>是否為泛型父類之子類</returns>
        public static bool IsSubClassOfRawGeneric(this Type source, Type generic)
        {
            while (source != null && source != typeof(object))
            {
                // 是否為泛型類別且非抽象
                Type cursor = source.IsGenericType ? source.GetGenericTypeDefinition() : source;

                // 檢查當前 cursor 型別與欲比對之泛型父類型別是否相同
                if (generic == cursor)
                {
                    return true;
                }

                source = source.BaseType;
            }

            return false;
        }
    }
}