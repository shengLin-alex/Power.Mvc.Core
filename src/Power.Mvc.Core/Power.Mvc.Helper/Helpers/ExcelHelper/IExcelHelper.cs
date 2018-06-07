using System.Collections.Generic;
using System.IO;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// EXCEL轉換
    /// </summary>
    public interface IExcelHelper
    {
        /// <summary>
        /// 將資料集合轉換成EXCEL，於控制器 return this.File(excelStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "***.xlsx");
        /// </summary>
        /// <typeparam name="T">物件類型</typeparam>
        /// <param name="sheetList">資料集合</param>
        /// <returns>輸出EXCEL資料串流</returns>
        Stream ConvertToFile<T>(List<ExcelExportModel<T>> sheetList) where T : class;
    }
}