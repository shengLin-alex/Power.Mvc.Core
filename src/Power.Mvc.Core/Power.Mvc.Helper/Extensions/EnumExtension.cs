using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Power.Mvc.Helper.Extensions
{
    /// <summary>
    /// 列舉擴充
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>Gets an attribute on an enum field value</summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            MemberInfo[] member = enumVal.GetType().GetMember(enumVal.ToString());
            if (member.Length != 0)
            {
                return (T)member[0].GetCustomAttributes(typeof(T), false).FirstOrDefault();
            }

            return default(T);
        }

        /// <summary>經由資源檔取得顯示名稱，若資源檔中未定義則會嘗試使用 DisplayNameAttribute</summary>
        /// <param name="enumVal">The enum value</param>
        /// <param name="resourceManager">資源管理員</param>
        /// <returns>顯示名稱</returns>
        public static string GetDisplayName(this Enum enumVal, ResourceManager resourceManager)
        {
            string name = $"Enum_{ enumVal.GetType().Name }_{ enumVal }";
            string str = resourceManager.GetString(name);
            if (string.IsNullOrEmpty(str) == false)
            {
                return str;
            }

            return enumVal.GetDisplayName();
        }

        /// <summary>經由 DisplayAttribute 取得顯示名稱</summary>
        /// <param name="enumVal">The enum value</param>
        /// <param name="prop">公開屬性名稱</param>
        /// <returns>顯示名稱</returns>
        public static string GetDisplayName(this Enum enumVal, string prop = "Name")
        {
            Type type = enumVal.GetType();
            if (type.GetCustomAttributes(typeof(FlagsAttribute), false).Any())
            {
                Array enumValues = type.GetEnumValues();
                StringBuilder stringBuilder = new StringBuilder();
                string str = string.Empty;
                foreach (Enum @enum in enumValues.Cast<Enum>())
                {
                    if (enumVal.HasFlag(@enum) && ((int)(object)enumVal <= 0 || (int)(object)@enum != 0))
                    {
                        DisplayAttribute attributeOfType = @enum.GetAttributeOfType<DisplayAttribute>();
                        stringBuilder.Append(str);
                        stringBuilder.Append(attributeOfType == null ? @enum.ToString() : attributeOfType.GetDisplay(prop));
                        str = ", ";
                    }
                }

                return stringBuilder.ToString();
            }

            DisplayAttribute attributeOfType1 = enumVal.GetAttributeOfType<DisplayAttribute>();
            if (attributeOfType1 != null)
            {
                return attributeOfType1.GetDisplay(prop);
            }

            return enumVal.ToString();
        }

        /// <summary>
        /// 取得 Description attribute 的字串
        /// </summary>
        /// <typeparam name="T">掛 Description 的物件型別</typeparam>
        /// <param name="source">物件來源</param>
        /// <returns>Description attribute 的字串</returns>
        public static string GetDescription<T>(this T source)
        {
            FieldInfo fieldInfo = source.GetType().GetField(source.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            return source.ToString();
        }
    }
}