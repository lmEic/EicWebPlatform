namespace Lm.Eic.Uti.Common.YleeExcelHanlder
{
    /// <summary>
    /// Excel操作助手
    /// Microsoft.Excel插件，
    /// 支持小批量数据导入导出与打印功能
    /// 大量数据导入导出时速度比较慢，导入
    /// 导出大量数据时，尽量不要使用此模块
    /// </summary>
    public static class ExcelHelper
    {
        /// <summary>
        /// 返回模板文件中的指定的Sheet表
        /// </summary>
        /// <param name="templateFilePath">模板文件路径</param>
        /// <param name="sheetIndex">Sheet索引</param>
        /// <param name="setDataToSheetHandler">赋值数据给Excel</param>
        public static Excel.Worksheet CreateXlsSheet(string templateFilePath, int sheetIndex, ref Excel.Application xlsApp, ref Excel.Workbook xlsWorkbook, bool isvisible = false)
        {
            xlsApp = new Excel.Application();
            Excel.Workbooks workbook = xlsApp.Workbooks;
            xlsWorkbook = workbook.Add(templateFilePath);
            Excel.Worksheet xst = xlsWorkbook.Worksheets[sheetIndex] as Excel.Worksheet;
            xlsApp.Visible = isvisible;
            xlsApp.DisplayAlerts = false;
            return xst;
        }

        /// <summary>
        /// 关闭Excel内存进程
        /// </summary>
        /// <param name="xlsApp">应用程序</param>
        /// <param name="xlsWorkbook">工作簿</param>
        /// <param name="xlsSheet">工作表</param>
        public static void CloseXls(Excel.Application xlsApp, Excel.Workbook xlsWorkbook, Excel.Worksheet xlsSheet)
        {
            xlsWorkbook.Close();
            xlsApp.Workbooks.Close();
            xlsApp.Quit();
            //关闭EXCEL进程
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsApp);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsWorkbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsSheet);
            xlsWorkbook = null;
            xlsApp = null;
        }
    }
}