﻿using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Data;
using NPOI.HSSF.Util;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace Lm.Eic.Uti.Common.YleeExcelHanlder
{
    /// <summary>
    /// NPOI操作助手
    /// </summary>
    public static class NPOIHelper
    {
        #region <T> 实体转化为数据流
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
        /// 一组实体数据 到Excel内存流
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="DicDataSources">数据字典</param>
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
        #endregion

        #region   DataTable 转化为Excel内存流
        /// <summary>
        ///  将DataTable数据导入到Excel内存流
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="xlsSheetName">SheetName</param>
        /// <returns></returns>
        public static MemoryStream ExportDataTableToExcel(DataTable dt, string xlsSheetName)
        {
            MemoryStream stream = new MemoryStream();
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet = book.CreateSheet(xlsSheetName);
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                row.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row2 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                    row2.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
            }
            book.Write(stream);
            return stream;
        }


        /// <summary>
        ///  一组DataTable导入到Excel内存流
        /// </summary>
        /// <param name="dicDataSources">DataTable组</param>
        /// <returns></returns>
        public static MemoryStream ExportDataTableToExcelMultiSheets(Dictionary<string, DataTable> dicDataSources)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                HSSFWorkbook workbook = new HSSFWorkbook();
                foreach (string i in dicDataSources.Keys)
                {
                    if (dicDataSources[i] == null || dicDataSources[i].Rows.Count == 0) continue;
                    ISheet sheet = DataTableCreatesheets(dicDataSources[i], i, workbook);
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
        private static ISheet DataTableCreatesheets(DataTable dt, string xlsSheetName, HSSFWorkbook book)
        {
            ISheet sheet = book.CreateSheet(xlsSheetName);
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                row.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row2 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                    row2.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
            }
            return sheet;
        }
        #endregion


        #region  NPOI Sheet  操作
        /// <summary>
        /// 格式复制
        /// </summary>
        /// <param name="wb">WorkBook</param>
        /// <param name="fromStyle">源</param>
        /// <param name="toStyle">到</param>
        public static void CopyCellStyle(IWorkbook wb, ICellStyle fromStyle, ICellStyle toStyle)
        {
            toStyle.Alignment = fromStyle.Alignment;
            //边框和边框颜色  
            toStyle.BorderBottom = fromStyle.BorderBottom;
            toStyle.BorderLeft = fromStyle.BorderLeft;
            toStyle.BorderRight = fromStyle.BorderRight;
            toStyle.BorderTop = fromStyle.BorderTop;
            toStyle.TopBorderColor = fromStyle.TopBorderColor;
            toStyle.BottomBorderColor = fromStyle.BottomBorderColor;
            toStyle.RightBorderColor = fromStyle.RightBorderColor;
            toStyle.LeftBorderColor = fromStyle.LeftBorderColor;

            //背景和前景  
            toStyle.FillBackgroundColor = fromStyle.FillBackgroundColor;
            toStyle.FillForegroundColor = fromStyle.FillForegroundColor;

            toStyle.DataFormat = fromStyle.DataFormat;
            toStyle.FillPattern = fromStyle.FillPattern;
            //toStyle.Hidden=fromStyle.Hidden;  
            toStyle.IsHidden = fromStyle.IsHidden;
            toStyle.Indention = fromStyle.Indention;//首行缩进  
            toStyle.IsLocked = fromStyle.IsLocked;
            toStyle.Rotation = fromStyle.Rotation;//旋转  
            toStyle.VerticalAlignment = fromStyle.VerticalAlignment;
            toStyle.WrapText = fromStyle.WrapText;
            toStyle.SetFont(fromStyle.GetFont(wb));
        }

        /// <summary>  
        /// 复制表  
        /// </summary>  
        /// <param name="wb"></param>  
        /// <param name="fromSheet"></param>  
        /// <param name="toSheet"></param>  
        /// <param name="copyValueFlag"></param>  
        public static void CopySheet(IWorkbook wb, ISheet fromSheet, ISheet toSheet, bool copyValueFlag)
        {
            //合并区域处理  
            CopyMergerRegion(fromSheet, toSheet);
            System.Collections.IEnumerator rows = fromSheet.GetRowEnumerator();
            while (rows.MoveNext())
            {
                IRow row = null;
                if (wb is HSSFWorkbook)
                    row = rows.Current as HSSFRow;
                else
                    row = rows.Current as NPOI.XSSF.UserModel.XSSFRow;
                IRow newRow = toSheet.CreateRow(row.RowNum);
                CopyRow(wb, row, newRow, copyValueFlag);
            }
        }

        /// <summary>  
        /// 复制行  
        /// </summary>  
        /// <param name="wb"></param>  
        /// <param name="fromRow"></param>  
        /// <param name="toRow"></param>  
        /// <param name="copyValueFlag"></param>  
        public static void CopyRow(IWorkbook wb, IRow fromRow, IRow toRow, bool copyValueFlag)
        {
            System.Collections.IEnumerator cells = fromRow.GetEnumerator(); //.GetRowEnumerator();  
            toRow.Height = fromRow.Height;
            while (cells.MoveNext())
            {
                ICell cell = null;
                //ICell cell = (wb is HSSFWorkbook) ? cells.Current as HSSFCell : cells.Current as NPOI.XSSF.UserModel.XSSFCell;  
                if (wb is HSSFWorkbook)
                    cell = cells.Current as HSSFCell;
                else
                    cell = cells.Current as NPOI.XSSF.UserModel.XSSFCell;
                ICell newCell = toRow.CreateCell(cell.ColumnIndex);
                CopyCell(wb, cell, newCell, copyValueFlag);
            }
        }

        /// <summary>  
        /// 复制单元格  
        /// </summary>  
        /// <param name="wb"></param>  
        /// <param name="srcCell"></param>  
        /// <param name="distCell"></param>  
        /// <param name="copyValueFlag"></param>  
        public static void CopyCell(IWorkbook wb, ICell srcCell, ICell distCell, bool copyValueFlag)
        {
            ICellStyle newstyle = wb.CreateCellStyle();
            CopyCellStyle(wb, srcCell.CellStyle, newstyle);

            //样式  
            distCell.CellStyle = newstyle;
            //评论  
            if (srcCell.CellComment != null)
            {
                distCell.CellComment = srcCell.CellComment;
            }
            // 不同数据类型处理  
            CellType srcCellType = srcCell.CellType;
            distCell.SetCellType(srcCellType);
            if (copyValueFlag)
            {
                if (srcCellType == CellType.Numeric)
                {

                    if (HSSFDateUtil.IsCellDateFormatted(srcCell))
                    {
                        distCell.SetCellValue(srcCell.DateCellValue);
                    }
                    else
                    {
                        distCell.SetCellValue(srcCell.NumericCellValue);
                    }
                }
                else if (srcCellType == CellType.String)
                {
                    distCell.SetCellValue(srcCell.RichStringCellValue);
                }
                else if (srcCellType == CellType.Blank)
                {
                    // nothing21  
                }
                else if (srcCellType == CellType.Boolean)
                {
                    distCell.SetCellValue(srcCell.BooleanCellValue);
                }
                else if (srcCellType == CellType.Error)
                {
                    distCell.SetCellErrorValue(srcCell.ErrorCellValue);
                }
                else if (srcCellType == CellType.Formula)
                {
                    distCell.SetCellFormula(srcCell.CellFormula);
                }
                else
                { // nothing29  
                }
            }
        }

        /// <summary>  
        /// 复制原有sheet的合并单元格到新创建的sheet  
        /// </summary>  
        /// <param name="fromSheet"></param>  
        /// <param name="toSheet"></param>  
        public static void CopyMergerRegion(ISheet fromSheet, ISheet toSheet)
        {
            int sheetMergerCount = fromSheet.NumMergedRegions;
            for (int i = 0; i < sheetMergerCount; i++)
            {
                //Region mergedRegionAt = fromSheet.GetMergedRegion(i); //.MergedRegionAt(i);  
                //CellRangeAddress[] cra = new CellRangeAddress[1];  
                //cra[0] = fromSheet.GetMergedRegion(i);  
                //Region[] rg = Region.ConvertCellRangesToRegions(cra);  
                toSheet.AddMergedRegion(fromSheet.GetMergedRegion(i));
            }
        }


        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">录入的ISheet</param>
        /// <param name="rowStart">Row开始</param>
        /// <param name="rowEnd">Row结束</param>
        /// <param name="columnStart">column开始</param>
        /// <param name="columnEnd">column结束</param>
        /// <param name="textValue">填充值</param>
        /// <returns>返回合并后的SHeet表</returns>
        public static ISheet MergedRegion(ISheet sheet,
            int rowStart, int rowEnd,
            int columnStart, int columnEnd)
        {
            //起始行号，终止行号， 起始列号，终止列号
            sheet.AddMergedRegion(new CellRangeAddress(rowStart - 1, rowEnd - 1, columnStart - 1, columnEnd - 1));
            return sheet;
        }
        /// <summary>
        /// 单元格赋值
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="columnNum"></param>
        /// <param name="rowNum"></param>
        /// <param name="textValue"></param>
        /// <returns></returns>
        public static ISheet SetCellValue(ISheet sheet,
          int rowNum, int columnNum,
          string textValue)
        {
            IRow row = sheet.GetRow(rowNum - 1);
            if (row == null) row = sheet.CreateRow(rowNum - 1);
            //在行中：建立单元格，参数为列号，从0计
            ICell cell = row.GetCell(columnNum - 1);
            if (cell == null) cell = row.CreateCell(columnNum - 1);
            //设置单元格内容
            cell.SetCellValue(textValue);
            return sheet;
        }
        /// <summary>
        /// 设置单元格样式
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="cell"></param>
        public static void setCellStyle(HSSFWorkbook workbook, ICell cell, short fontHeightInPoints,
            string fontName = "宋体", short color = 8,
            int verticalAlignment = 2,
            int alignment = 2)
        {
            if (cell == null) return;
            HSSFCellStyle fCellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFFont ffont = (HSSFFont)workbook.CreateFont();
            ffont.FontHeight = 20 * 20;
            //字体大小
            ffont.FontHeightInPoints = fontHeightInPoints;
            ffont.FontName = fontName;
            //HSSFColor.Black.Index 8; HSSFColor.Red.Index 10
            ffont.Color = color;
            fCellStyle.SetFont(ffont);
            fCellStyle.VerticalAlignment = GetVerticalAlignmentStyle(verticalAlignment);//垂直对齐
            fCellStyle.Alignment = GetHorizontalAlignmentStyle(alignment);//水平对齐
            cell.CellStyle = fCellStyle;
        }
        public static void setCellStyle(XSSFWorkbook workbook, ICell cell, short fontHeightInPoints,
          string fontName = "宋体", short color = 8,
          int verticalAlignment = 2,
          int alignment = 2)
        {
            XSSFCellStyle fCellStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            XSSFFont ffont = (XSSFFont)workbook.CreateFont();
            ffont.FontHeight = 20 * 20;
            //字体大小
            ffont.FontHeightInPoints = fontHeightInPoints;
            ffont.FontName = fontName;
            //HSSFColor.Black.Index 8; HSSFColor.Red.Index 10
            ffont.Color = color;
            fCellStyle.SetFont(ffont);
            fCellStyle.VerticalAlignment = GetVerticalAlignmentStyle(verticalAlignment);//垂直对齐
            fCellStyle.Alignment = GetHorizontalAlignmentStyle(alignment);//水平对齐
            cell.CellStyle = fCellStyle;
        }

        private static NPOI.SS.UserModel.VerticalAlignment GetVerticalAlignmentStyle(int numberIndex)
        {
            switch (numberIndex)
            {
                case 1:
                    return VerticalAlignment.Top;
                case 2:
                    return VerticalAlignment.Center;
                case 3:
                    return VerticalAlignment.Bottom;
                case 4:
                    return VerticalAlignment.Justify;
                case 5:
                    return VerticalAlignment.Distributed;
                default:
                    return VerticalAlignment.Center;
            }
        }
        private static NPOI.SS.UserModel.HorizontalAlignment GetHorizontalAlignmentStyle(int numberIndex)
        {
            switch (numberIndex)
            {
                case 0:
                    return HorizontalAlignment.General;
                case 1:
                    return HorizontalAlignment.Left;
                case 2:
                    return HorizontalAlignment.Center;
                case 3:
                    return HorizontalAlignment.Right;
                case 4:
                    return HorizontalAlignment.Fill;
                case 5:
                    return HorizontalAlignment.Justify;
                case 6:
                    return HorizontalAlignment.CenterSelection;
                case 7:
                    return HorizontalAlignment.Distributed;
                default:
                    return HorizontalAlignment.Center;
            }
        }
        #endregion
    }
}