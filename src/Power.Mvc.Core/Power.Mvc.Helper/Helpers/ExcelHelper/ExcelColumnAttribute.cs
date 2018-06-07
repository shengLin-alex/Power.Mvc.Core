using System;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// EXCEL欄位屬性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ExcelColumnAttribute : Attribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ExcelColumnAttribute()
        {
        }

        /// <summary>
        /// Constructor with title
        /// </summary>
        /// <param name="title">the title of column</param>
        public ExcelColumnAttribute(string title)
        {
            this.Title = title;
        }

        /// <summary>
        /// 是否要輸出，TRUE:做為輸出的欄位；FALSE:不做為輸出欄位
        /// </summary>
        public bool IsExportShow { get; set; } = true;

        /// <summary>
        /// 若Excel的欄位標頭與物件屬性名稱不同，可用該屬性做定義 例如Excel欄位標頭為中文，即可用該屬性對應到物件屬性
        /// </summary>
        public string Title { get; set; }
    }
}