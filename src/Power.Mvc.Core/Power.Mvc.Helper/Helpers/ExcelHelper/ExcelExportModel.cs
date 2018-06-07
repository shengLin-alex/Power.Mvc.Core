using System.Collections.Generic;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 物件集合輸出為EXCEL的worksheet
    /// </summary>
    /// <typeparam name="T">欲輸出的資料型別</typeparam>
    public class ExcelExportModel<T> where T : class
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">worksheet名稱</param>
        /// <param name="datas">excel data rows</param>
        public ExcelExportModel(string title, List<T> datas)
        {
            this.SheetTitle = title;
            this.Datas = datas;
        }

        /// <summary>
        /// Excel data rows
        /// </summary>
        public List<T> Datas { get; set; }

        /// <summary>
        /// 資料表名稱(同一個Excel 資料表名稱不可重複)
        /// </summary>
        public string SheetTitle { get; set; }
    }
}