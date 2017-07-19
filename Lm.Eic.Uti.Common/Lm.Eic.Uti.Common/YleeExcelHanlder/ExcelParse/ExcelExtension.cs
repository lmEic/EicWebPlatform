using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Uti.Common.YleeExcelHanlder.ExcelParse
{
    ///	<summary>															
    ///	表示单元格的维度，通常用于表达合并单元格的维度														
    ///	</summary>														
    public struct Dimension
    {
        ///	<summary>														
        ///	含有数据的单元格(通常表示合并单元格的第一个跨度行第一个跨度列)，该字段可能为null														
        ///	</summary>														
        public ICell DataCell;
        ///	<summary>														
        ///	行跨度(跨越了多少行)														
        ///	</summary>														
        public int RowSpan;
        ///	<summary>														
        ///	列跨度(跨越了多少列)														
        ///	</summary>														
        public int ColumnSpan;
        ///	<summary>														
        ///	合并单元格的起始行索引														
        ///	</summary>														
        public int FirstRowIndex;
        ///	<summary>														
        ///	合并单元格的结束行索引														
        ///	</summary>														
        public int LastRowIndex;
        ///	<summary>														
        ///	合并单元格的起始列索引														
        ///	</summary>														
        public int FirstColumnIndex;
        ///	<summary>														
        ///	合并单元格的结束列索引														
        ///	</summary>														
        public int LastColumnIndex;
    }

    public static class ExcelExtension
    {
        ///	<summary>														
        ///	判断指定行列所在的单元格是否为合并单元格，并且输出该单元格的维度														
        ///	</summary>														
        ///	<param	name="sheet">Excel工作表</param>													
        ///	<param	name="rowIndex">行索引，从0开始</param>													
        ///	<param	name="columnIndex">列索引，从0开始</param>													
        ///	<param	name="dimension">单元格维度</param>													
        ///	<returns>返回是否为合并单元格的布尔(Boolean)值</returns>														
        public static bool IsMergeCell(this ISheet sheet, int rowIndex, int columnIndex, out Dimension dimension)
        {
            dimension = new Dimension
            {
                DataCell = null,
                RowSpan = 1,
                ColumnSpan = 1,
                FirstRowIndex = rowIndex,
                LastRowIndex = rowIndex,
                FirstColumnIndex = columnIndex,
                LastColumnIndex = columnIndex
            };
            for (int i = 0; i < sheet.NumMergedRegions; i++)
            {
                CellRangeAddress range = sheet.GetMergedRegion(i);
                sheet.IsMergedRegion(range);

                //这种算法只有当指定行列索引刚好是合并单元格的第一个跨度行第一个跨度列时才能取得合并单元格的跨度															
                //if	(range.FirstRow	==	rowIndex	&&	range.FirstColumn	==	columnIndex)								
                //{															
                //	dimension.DataCell	=	sheet.GetRow(range.FirstRow).GetCell(range.FirstColumn);												
                //	dimension.RowSpan	=	range.LastRow	-	range.FirstRow	+	1;								
                //	dimension.ColumnSpan	=	range.LastColumn	-	range.FirstColumn	+	1;								
                //	dimension.FirstRowIndex	=	range.FirstRow;												
                //	dimension.LastRowIndex	=	range.LastRow;												
                //	dimension.FirstColumnIndex	=	range.FirstColumn;												
                //	dimension.LastColumnIndex	=	range.LastColumn;												
                //	break;														
                //}															

                if ((rowIndex >= range.FirstRow && range.LastRow >= rowIndex) && (columnIndex >= range.FirstColumn && range.LastColumn >= columnIndex))
                {
                    dimension.DataCell = sheet.GetRow(range.FirstRow).GetCell(range.FirstColumn);
                    dimension.RowSpan = range.LastRow - range.FirstRow + 1;
                    dimension.ColumnSpan = range.LastColumn - range.FirstColumn + 1;
                    dimension.FirstRowIndex = range.FirstRow;
                    dimension.LastRowIndex = range.LastRow;
                    dimension.FirstColumnIndex = range.FirstColumn;
                    dimension.LastColumnIndex = range.LastColumn;
                    break;
                }
            }

            bool result;
            if (rowIndex >= 0 && sheet.LastRowNum > rowIndex)
            {
                IRow row = sheet.GetRow(rowIndex);
                if (columnIndex >= 0 && row.LastCellNum > columnIndex)
                {
                    ICell cell = row.GetCell(columnIndex);
                    result = cell.IsMergedCell;

                    if (dimension.DataCell == null)
                    {
                        dimension.DataCell = cell;
                    }
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            //try																
            //{																
            //	result	=	sheet.GetRow(rowIndex).GetCell(columnIndex).IsMergedCell;													
            //}																
            //catch																
            //{																
            //	result	=	false;													
            //}																

            return result;
        }
        ///	<summary>															
        ///	判断指定行列所在的单元格是否为合并单元格，并且输出该单元格的行列跨度															
        ///	</summary>															
        ///	<param	name="sheet">Excel工作表</param>														
        ///	<param	name="rowIndex">行索引，从0开始</param>														
        ///	<param	name="columnIndex">列索引，从0开始</param>														
        ///	<param	name="rowSpan">行跨度，返回值最小为1，同时表示没有行合并</param>														
        ///	<param	name="columnSpan">列跨度，返回值最小为1，同时表示没有列合并</param>														
        ///	<returns>返回是否为合并单元格的布尔(Boolean)值</returns>															
        public static bool IsMergeCell(this ISheet sheet, int rowIndex, int columnIndex, out int rowSpan, out int columnSpan)
        {
            Dimension dimension;
            bool result = sheet.IsMergeCell(rowIndex, columnIndex, out dimension);
            rowSpan = dimension.RowSpan;
            columnSpan = dimension.ColumnSpan;
            return result;
        }
        ///	<summary>															
        ///	判断指定单元格是否为合并单元格，并且输出该单元格的维度															
        ///	</summary>															
        ///	<param	name="cell">单元格</param>														
        ///	<param	name="dimension">单元格维度</param>														
        ///	<returns>返回是否为合并单元格的布尔(Boolean)值</returns>															
        public static bool IsMergeCell(this ICell cell, out Dimension dimension)
        {
            return cell.Sheet.IsMergeCell(cell.RowIndex, cell.ColumnIndex, out dimension);
        }
        ///	<summary>															
        ///	判断指定单元格是否为合并单元格，并且输出该单元格的行列跨度															
        ///	</summary>															
        ///	<param	name="cell">单元格</param>														
        ///	<param	name="rowSpan">行跨度，返回值最小为1，同时表示没有行合并</param>														
        ///	<param	name="columnSpan">列跨度，返回值最小为1，同时表示没有列合并</param>														
        ///	<returns>返回是否为合并单元格的布尔(Boolean)值</returns>															
        public static bool IsMergeCell(this ICell cell, out int rowSpan, out int columnSpan)
        {
            return cell.Sheet.IsMergeCell(cell.RowIndex, cell.ColumnIndex, out rowSpan, out columnSpan);
        }
        ///	<summary>															
        ///	返回上一个跨度行，如果rowIndex为第一行，则返回null															
        ///	</summary>															
        ///	<param	name="sheet">Excel工作表</param>														
        ///	<param	name="rowIndex">行索引，从0开始</param>														
        ///	<param	name="columnIndex">列索引，从0开始</param>														
        ///	<returns>返回上一个跨度行</returns>															
        public static IRow PrevSpanRow(this ISheet sheet, int rowIndex, int columnIndex)
        {
            return sheet.FuncSheet(rowIndex, columnIndex, (currentDimension, isMerge) =>
            {
                //上一个单元格维度																
                Dimension prevDimension;
                sheet.IsMergeCell(currentDimension.FirstRowIndex - 1, columnIndex, out prevDimension);
                return prevDimension.DataCell.Row;
            });
        }
        ///	<summary>															
        ///	返回下一个跨度行，如果rowIndex为最后一行，则返回null															
        ///	</summary>															
        ///	<param	name="sheet">Excel工作表</param>														
        ///	<param	name="rowIndex">行索引，从0开始</param>														
        ///	<param	name="columnIndex">列索引，从0开始</param>														
        ///	<returns>返回下一个跨度行</returns>															
        public static IRow NextSpanRow(this ISheet sheet, int rowIndex, int columnIndex)
        {
            return sheet.FuncSheet(rowIndex, columnIndex, (currentDimension, isMerge) =>
                  isMerge ? sheet.GetRow(currentDimension.FirstRowIndex + currentDimension.RowSpan) : sheet.GetRow(rowIndex));
        }
        /// <summary>
        /// 返回下一个跨度列，如果columnIndex为最后一列，则返回null
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="columnIndex">列索引，从0开始</param>
        /// <returns>返回下一个跨度列</returns>
        public static ICell NextSpanCell(this IRow row, int columnIndex)
        {
            ////当前单元格维度
            //Dimension currentDimension;
            //row.Sheet.IsMergeCell(row.RowNum, columnIndex, out currentDimension);

            //return row.GetCell(currentDimension.FirstColumnIndex + currentDimension.ColumnSpan);

            return row.Sheet.FuncSheet(row.RowNum, columnIndex, (currentDimension, isMerge) =>
                row.GetCell(currentDimension.FirstColumnIndex + currentDimension.ColumnSpan));
        }
        ///	<summary>															
        ///	返回上一个跨度行，如果row为第一行，则返回null															
        ///	</summary>															
        ///	<param	name="row">行</param>														
        ///	<returns>返回上一个跨度行</returns>															
        public static IRow PrevSpanRow(this IRow row)
        {
            return row.Sheet.PrevSpanRow(row.RowNum, row.FirstCellNum);
        }
        ///	<summary>															
        ///	返回下一个跨度行，如果row为最后一行，则返回null															
        ///	</summary>															
        ///	<param	name="row">行</param>														
        ///	<returns>返回下一个跨度行</returns>															
        public static IRow NextSpanRow(this IRow row)
        {
            return row.Sheet.NextSpanRow(row.RowNum, row.FirstCellNum);
        }
        /// <summary>
        /// 返回上一个跨度列，如果cell为第一列，则返回null
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <returns>返回上一个跨度列</returns>
        public static ICell PrevSpanCell(this ICell cell)
        {
            return cell.Row.PrevSpanCell(cell.ColumnIndex);
        }
        /// <summary>
        /// 返回下一个跨度列，如果columnIndex为最后一列，则返回null
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <returns>返回下一个跨度列</returns>
        public static ICell NextSpanCell(this ICell cell)
        {
            return cell.Row.NextSpanCell(cell.ColumnIndex);
        }
        ///	<summary>															
        ///	返回上一个跨度列，如果columnIndex为第一列，则返回null															
        ///	</summary>															
        ///	<param	name="row">行</param>														
        ///	<param	name="columnIndex">列索引，从0开始</param>														
        ///	<returns>返回上一个跨度列</returns>															
        public static ICell PrevSpanCell(this IRow row, int columnIndex)
        {
            ////当前单元格维度																
            //Dimension	currentDimension;															
            //row.Sheet.IsMergeCell(row.RowNum,	columnIndex,	out	currentDimension);													

            ////上一个单元格维度																
            //Dimension	prevDimension;															
            //row.Sheet.IsMergeCell(row.RowNum,	currentDimension.FirstColumnIndex	-	1,	out	prevDimension);											
            //return	prevDimension.DataCell;															

            return row.Sheet.FuncSheet(row.RowNum, columnIndex, (currentDimension, isMerge) =>
            {
                //上一个单元格维度																
                Dimension prevDimension;
                row.Sheet.IsMergeCell(row.RowNum, currentDimension.FirstColumnIndex - 1, out prevDimension);
                return prevDimension.DataCell;
            });
        }
        /// <summary>
        /// 返回指定列索引所在的合并单元格(区域)中的第一行第一列(通常是含有数据的单元格)
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="columnIndex">列索引</param>
        /// <returns></returns>
        public static ICell GetDataCell(this IRow row, int columnIndex)
        {
            ////当前单元格维度
            //Dimension currentDimension;
            ////是否为合并单元格
            //bool isMerge = row.Sheet.IsMergeCell(row.RowNum, columnIndex, out currentDimension);

            //return currentDimension.DataCell;

            return row.Sheet.FuncSheet(row.RowNum, columnIndex, (currentDimension, isMerge) => currentDimension.DataCell);
        }

        private static T FuncSheet<T>(this ISheet sheet, int rowIndex, int columnIndex, Func<Dimension, bool, T> func)
        {
            //当前单元格维度
            Dimension currentDimension;
            //是否为合并单元格
            bool isMerge = sheet.IsMergeCell(rowIndex, columnIndex, out currentDimension);

            return func(currentDimension, isMerge);
        }

    }
}
