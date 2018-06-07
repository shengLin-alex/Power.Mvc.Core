using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Power.Mvc.Helper.Extensions
{
    /// <summary>
    /// Attribute 擴充
    /// </summary>
    public static class AttributeExtension
    {
        /// <summary>取得DisplayAttribute公開屬性內容</summary>
        /// <param name="attr">DisplayAttribute</param>
        /// <param name="propName">公開屬性名稱，預設Name</param>
        /// <returns>The value that is used for display in the UI</returns>
        public static string GetDisplay(this DisplayAttribute attr, string propName = "Name")
        {
            string empty = string.Empty;
            PropertyInfo property = attr.GetType().GetProperty(propName);
            if (property != null)
            {
                empty = (string)property.GetValue(attr);
            }

            return empty;
        }

        /// <summary>
        /// 取得DisplayAttribute公開屬性內容
        /// </summary>
        /// <param name="propertyInfo">Discovers the attributes of a property and provides access to property metadata.</param>
        /// <returns>The value that is used for display in the UI.</returns>
        public static string GetDisplayName(this PropertyInfo propertyInfo)
        {
            if ((PropertyInfo)null == propertyInfo)
            {
                return string.Empty;
            }

            DisplayAttribute attr = propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), true).Cast<DisplayAttribute>().SingleOrDefault<DisplayAttribute>();
            if (attr == null)
            {
                return string.Empty;
            }

            return attr.GetDisplay();
        }
    }
}