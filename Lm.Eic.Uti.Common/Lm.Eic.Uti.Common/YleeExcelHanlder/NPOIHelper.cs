using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Lm.Eic.Uti.Common.YleeExcelHanlder
{
    /// <summary>
    /// NPOI操作助手
    /// </summary>
    public static class NPOIHelper
    {
        /// <summary>
        /// 将数据导入到Excel内存流
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="dataSource">数据集合</param>
        /// <param name="xlsSheetName">Sheet名称</param>
        /// <returns></returns>
        public static MemoryStream ExportToExcel<T>(List<T> dataSource, string xlsSheetName) where T : class
        {
            MemoryStream stream = new MemoryStream();
            if (dataSource == null || dataSource.Count == 0) return stream;

            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(xlsSheetName);
            ICellStyle cellSytleDate = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            cellSytleDate.DataFormat = format.GetFormat("yyyy年mm月dd日");

            try
            {
                #region 填充列头区域

                Type t = dataSource[0].GetType();
                PropertyInfo[] pis = t.GetProperties();
                IRow rowHeader = sheet.CreateRow(0);
                //设置表头样式
                ICellStyle headStyle = workbook.CreateCellStyle();
                headStyle.Alignment = HorizontalAlignment.Center;
                IFont cellFontHeader = workbook.CreateFont();
                cellFontHeader.Boldweight = 700;
                cellFontHeader.FontHeightInPoints = 12;
                headStyle.SetFont(cellFontHeader);

                for (int colIndex = 0; colIndex < pis.Length; colIndex++)
                {
                    ICell cell = rowHeader.CreateCell(colIndex);
                    cell.SetCellValue(pis[colIndex].Name);
                    cell.CellStyle = headStyle;
                }

                #endregion 填充列头区域

                #region 填充内容区域

                for (int rowIndex = 0; rowIndex < dataSource.Count; rowIndex++)
                {
                    IRow rowContent = sheet.CreateRow(rowIndex + 1);
                    T entity = dataSource[rowIndex];
                    Type tentity = entity.GetType();
                    PropertyInfo[] tpis = tentity.GetProperties();
                    for (int colIndex = 0; colIndex < tpis.Length; colIndex++)
                    {
                        ICell cellContent = rowContent.CreateCell(colIndex);
                        object value = tpis[colIndex].GetValue(entity, null);
                        if (value == null) value = "";
                        Type type = value.GetType();
                        switch (type.ToString())
                        {
                            case "System.String"://字符串类型
                                cellContent.SetCellValue(value.ToString());
                                break;

                            case "System.DateTime"://日期类型
                                DateTime dateV;
                                DateTime.TryParse(((DateTime)value).ToString("yyyy-MM-dd HH:mm"), out dateV);
                                cellContent.SetCellValue(dateV);
                                cellContent.CellStyle = cellSytleDate;//格式化显示数据
                                break;

                            case "System.Boolean"://布尔型
                                bool boolV = false;
                                if (bool.TryParse(value.ToString(), out boolV))
                                    cellContent.SetCellValue(boolV);
                                else
                                    cellContent.SetCellValue("解析布尔型数据错误");
                                break;

                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                if (int.TryParse(value.ToString(), out intV))
                                    cellContent.SetCellValue(intV);
                                else
                                    cellContent.SetCellValue("解析整型数据错误");
                                break;

                            case "System.Decimal"://浮点型
                            case "System.Double":
                                double doubV = 0;
                                if (double.TryParse(value.ToString(), out doubV))
                                    cellContent.SetCellValue(doubV);
                                else
                                    cellContent.SetCellValue("解析浮点型或双精度型数据错误");
                                break;

                            case "System.DBNull"://空值处理
                                cellContent.SetCellValue("");
                                break;

                            default:
                                cellContent.SetCellValue("");
                                break;
                        }
                    }
                }

                #endregion 填充内容区域

                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                return stream;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 将数据导入到Excel内存流
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="dataSource">数据集合</param>
        /// <param name="xlsSheetName">Sheet名称</param>
        /// <returns></returns>
        public static MemoryStream ExportToExcelMultiSheets<T>(Dictionary<string, List<T>> DicDataSources) where T : class
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                HSSFWorkbook workbook = new HSSFWorkbook();
              foreach (string i in DicDataSources.Keys)
               {
                  if (DicDataSources[i] == null || DicDataSources[i].Count == 0) continue;
                  ISheet sheet = CreateSheet<T>(DicDataSources[i], i, workbook);
                  sheet.ForceFormulaRecalculation = true;
               }
             
                workbook.Write(stream);
                return stream;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private static ISheet CreateSheet<T>(List<T> dataSource, string xlsSheetName, HSSFWorkbook workbook) where T : class
        {
            ISheet sheet = workbook.CreateSheet(xlsSheetName);
            ICellStyle cellSytleDate = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            cellSytleDate.DataFormat = format.GetFormat("yyyy年mm月dd日");

            #region 填充列头区域

            Type t = dataSource[0].GetType();
            PropertyInfo[] pis = t.GetProperties();
            IRow rowHeader = sheet.CreateRow(0);
            //设置表头样式
            ICellStyle headStyle = workbook.CreateCellStyle();
            headStyle.Alignment = HorizontalAlignment.Center;
            IFont cellFontHeader = workbook.CreateFont();
            cellFontHeader.Boldweight = 700;
            cellFontHeader.FontHeightInPoints = 12;
            headStyle.SetFont(cellFontHeader);

            for (int colIndex = 0; colIndex < pis.Length; colIndex++)
            {
                ICell cell = rowHeader.CreateCell(colIndex);
                cell.SetCellValue(pis[colIndex].Name);
                cell.CellStyle = headStyle;
            }

            #endregion 填充列头区域

            #region 填充内容区域

            for (int rowIndex = 0; rowIndex < dataSource.Count; rowIndex++)
            {
                IRow rowContent = sheet.CreateRow(rowIndex + 1);
                T entity = dataSource[rowIndex];
                Type tentity = entity.GetType();
                PropertyInfo[] tpis = tentity.GetProperties();
                for (int colIndex = 0; colIndex < tpis.Length; colIndex++)
                {
                    ICell cellContent = rowContent.CreateCell(colIndex);
                    object value = tpis[colIndex].GetValue(entity, null);
                    if (value == null) value = "";
                    Type type = value.GetType();
                    switch (type.ToString())
                    {
                        case "System.String"://字符串类型
                            cellContent.SetCellValue(value.ToString());
                            break;

                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(((DateTime)value).ToString("yyyy-MM-dd HH:mm"), out dateV);
                            cellContent.SetCellValue(dateV);
                            cellContent.CellStyle = cellSytleDate;//格式化显示数据
                            break;

                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            if (bool.TryParse(value.ToString(), out boolV))
                                cellContent.SetCellValue(boolV);
                            else
                                cellContent.SetCellValue("解析布尔型数据错误");
                            break;

                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            if (int.TryParse(value.ToString(), out intV))
                                cellContent.SetCellValue(intV);
                            else
                                cellContent.SetCellValue("解析整型数据错误");
                            break;

                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            if (double.TryParse(value.ToString(), out doubV))
                                cellContent.SetCellValue(doubV);
                            else
                                cellContent.SetCellValue("解析浮点型或双精度型数据错误");
                            break;

                        case "System.DBNull"://空值处理
                            cellContent.SetCellValue("");
                            break;

                        default:
                            cellContent.SetCellValue("");
                            break;
                    }
                }
            }

            #endregion 填充内容区域
            return sheet;
        }

        /// <summary>
        /// 导入到现有的Excel模板文件中
        /// </summary>
        public static void ExportToExcelTemplate(string templateFilePath, string xlsSheetName, string saveFileName, Action<ISheet> setDataToSheetHandler)
        {
            FileStream fs = new FileStream(templateFilePath, FileMode.Open, FileAccess.Read);//FileAccess.Read 保证文件不被占用
            HSSFWorkbook workbook = new HSSFWorkbook(fs);
            ISheet sheet = workbook.GetSheet(xlsSheetName);
            if (setDataToSheetHandler != null)
            {
                setDataToSheetHandler(sheet);
                sheet.ForceFormulaRecalculation = true;//强制要求Excel在打开时重新计算的属性，在拥有公式的xls文件中十分有用
            }
            FileStream localFile = new FileStream(saveFileName, FileMode.Create);
            workbook.Write(localFile);
            localFile.Close();
        }
    }
}