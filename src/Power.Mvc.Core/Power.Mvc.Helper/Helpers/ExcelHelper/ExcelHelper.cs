using OfficeOpenXml;
using OfficeOpenXml.Style;
using Power.Mvc.Helper.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// EXCEL轉換
    /// </summary>
    public class ExcelHelper : IExcelHelper
    {
        /// <summary>
        /// 將資料集合轉換成EXCEL，於控制器 return this.File(excelStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "***.xlsx");
        /// </summary>
        /// <typeparam name="T">物件類型</typeparam>
        /// <param name="sheetList">資料集合</param>
        /// <returns>輸出EXCEL資料串流</returns>
        public Stream ConvertToFile<T>(List<ExcelExportModel<T>> sheetList)
            where T : class
        {
            using (ExcelPackage ep = new ExcelPackage())
            {
                foreach (ExcelExportModel<T> sheet in sheetList)
                {
                    // 資料表名稱不可重複，且要有資料才執行。
                    if (ep.Workbook.Worksheets.All(x => x.Name != sheet.SheetTitle) && sheet.Datas != null)
                    {
                        ExcelWorksheet ews = ep.Workbook.Worksheets.Add(sheet.SheetTitle);

                        // 取Datas屬性資訊
                        PropertyInfo[] props = sheet.Datas.GetType().GetGenericArguments()[0].GetProperties();
                        List<int> wrapColIdx = new List<int>();

                        // 表頭設定
                        int headerColIdx = 1;
                        foreach (PropertyInfo prop in props)
                        {
                            ExcelColumnAttribute excelColunm =
                                prop.GetCustomAttributes(typeof(ExcelColumnAttribute), true)
                                    .Cast<ExcelColumnAttribute>()
                                   ?.SingleOrDefault();

                            // 完全沒設定屬性已預設欄位名稱，有設定屬性要檢查是否顯示。
                            if (excelColunm == null || excelColunm.IsExportShow)
                            {
                                this.AddHeader(ref ews, ref headerColIdx, prop);
                            }
                        }

                        // 設定內容
                        int rowIdx = 2;
                        foreach (T data in sheet.Datas)
                        {
                            int contentColIdx = 1;

                            foreach (PropertyInfo prop in props)
                            {
                                ExcelColumnAttribute excelColunm = prop.GetCustomAttributes(typeof(ExcelColumnAttribute), true)
                                                                       .Cast<ExcelColumnAttribute>()
                                                                       .SingleOrDefault();

                                if (excelColunm == null || excelColunm.IsExportShow)
                                {
                                    this.AddColunm(ref ews, ref rowIdx, ref contentColIdx, ref wrapColIdx, data, prop);
                                }
                            }

                            rowIdx++;
                        }

                        // 自動欄寬
                        ews.Cells[ews.Dimension.Address].AutoFitColumns();
                    }
                }

                MemoryStream stream = new MemoryStream();
                ep.SaveAs(stream);
                stream.Position = 0;
                return stream;
            }
        }

        /// <summary>
        /// EXCEL 資料內容
        /// </summary>
        /// <typeparam name="T">原始資料物件</typeparam>
        /// <param name="ews">excel work sheet</param>
        /// <param name="rowIdx">row index</param>
        /// <param name="contentColIdx">column index</param>
        /// <param name="wrapColIdx">column index collection</param>
        /// <param name="data">row data</param>
        /// <param name="prop">物件屬性</param>
        private void AddColunm<T>(ref ExcelWorksheet ews, ref int rowIdx, ref int contentColIdx, ref List<int> wrapColIdx, T data, PropertyInfo prop)
            where T : class
        {
            object value = prop.GetValue(data);
            ExcelRange cell = ews.Cells[rowIdx, contentColIdx++];

            cell.Value = value;
            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Top;

            // 自動換行設定_ EPPUS只認得\r\n這個換行符號
            if (value != null && value.ToString().Contains("\r\n"))
            {
                cell.Style.WrapText = true;
                if (!wrapColIdx.Contains(contentColIdx - 1))
                {
                    wrapColIdx.Add(contentColIdx - 1);
                }
            }
        }

        /// <summary>
        /// EXCEL表頭
        /// </summary>
        /// <param name="ews">excel work sheet</param>
        /// <param name="colIdx">column index</param>
        /// <param name="prop">物件屬性</param>
        private void AddHeader(ref ExcelWorksheet ews, ref int colIdx, PropertyInfo prop)
        {
            ExcelRange cell = ews.Cells[1, colIdx];

            // 預設以屬性ExcelColumnAttribute Title 為表頭，若無此Attribute，則使用原始欄位名稱
            ExcelColumnAttribute diaplay = prop.GetCustomAttributes(typeof(ExcelColumnAttribute), true)
                                               .Cast<ExcelColumnAttribute>()
                                               .SingleOrDefault();
            cell.Value =
                diaplay != null && !diaplay.Title.IsNullOrEmpty() ? diaplay.Title :
                !prop.GetDisplayName().IsNullOrEmpty() ? prop.GetDisplayName() :
                prop.Name;
            cell.Style.Font.Bold = true;
            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Bottom;
            colIdx++;
        }
    }
}