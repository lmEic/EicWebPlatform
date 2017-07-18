using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Xml;

namespace Lm.Eic.Uti.Common.YleeExcelHanlder.ExcelParse
{
    public class ExcelParseBaseService : IExcelParseBaseService
    {
        public bool CheckDataType(string cellType, string cellValue)
        {
            return cellValue.GetType().ToString() == cellType;
        }
        /// <summary>
        /// 除掉空格
        /// </summary>
        /// <param name="cellValue"></param>
        public void ReplaceSpace(ref string cellValue)
        {
            cellValue = TruncateString(cellValue, new char[] { ' ' }, new char[] { '　' });
        }
        // 对字符串做空格剔除处理
        private string TruncateString(string originalWord, char[] spiltWord1, char[] spiltWord2)
        {
            var result = "";
            var valueReplaceDbcCase = originalWord.Split(spiltWord1);

            if (valueReplaceDbcCase.Count() > 1)
            {
                for (int i = 0; i < valueReplaceDbcCase.Count(); i++)
                {
                    if (valueReplaceDbcCase[i] != "" && valueReplaceDbcCase[i] != " " &&
                        valueReplaceDbcCase[i] != "　")
                    {
                        result += TruncateString(valueReplaceDbcCase[i], spiltWord2, new char[0]);
                    }
                }
            }
            else
            {
                if (spiltWord2.Any())
                {
                    result = TruncateString(originalWord, spiltWord2, new char[0]);
                }
                else
                {
                    result = originalWord;
                }
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellValue"></param>
        /// <param name="nullcount"></param>
        /// <returns></returns>
        public bool CheckNull(string cellValue, ref int nullcount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 读取XML配置信息集
        /// </summary>
        /// <param name="xmlpath"></param>
        /// <returns></returns>
        public List<FixInsertRegular> GetXMLInterInfo(string xmlpath)
        {
            var reader = new XmlTextReader(xmlpath);
            var doc = new XmlDocument();
            doc.Load(reader);
            var headerList = new List<FixInsertRegular>();
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                // HeaderText PropertyName DataType FillText RowIndexStart RowIndexEnd 
                // ColumnIndexStart  ColumnIndexEnd  Ismerge 
                var header = new FixInsertRegular();
                if (node.Attributes["HeaderText"] != null)
                    header.HeaderText = node.Attributes["HeaderText"].Value;
                if (node.Attributes["PropertyName"] != null)
                    header.PropertyName = node.Attributes["PropertyName"].Value;
                if (node.Attributes["DataType"] != null)
                    header.DataType = node.Attributes["DataType"].Value;
                if (node.Attributes["FillText"] != null)
                    header.FillText = node.Attributes["FillText"].Value;
                if (node.Attributes["RowIndexStart"] != null)
                    header.RowIndexStart = int.Parse(node.Attributes["RowIndexStart"].Value);
                if (node.Attributes["RowIndexEnd"] != null)
                    header.RowIndexEnd = int.Parse(node.Attributes["RowIndexEnd"].Value);
                if (node.Attributes["ColumnIndexStart"] != null)
                    header.ColumnIndexStart = int.Parse(node.Attributes["ColumnIndexStart"].Value);
                if (node.Attributes["ColumnIndexEnd"] != null)
                    header.ColumnIndexEnd = int.Parse(node.Attributes["ColumnIndexEnd"].Value);
                if (node.Attributes["Ismerge"] != null)
                    header.Ismerge = bool.Parse(node.Attributes["Ismerge"].Value);
                //int fontHeight,  string fontName = "宋体", short color = 8, int verticalAlignment = 2, int alignment = 2
                if (node.Attributes["FontName"] != null)
                    header.FontName = node.Attributes["FontName"].Value;
                if (node.Attributes["FontHeightInPoints"] != null)
                    header.FontHeightInPoints = short.Parse(node.Attributes["FontHeightInPoints"].Value);
                if (node.Attributes["Color"] != null)
                    header.Color = short.Parse(node.Attributes["Color"].Value);
                if (node.Attributes["Alignment"] != null)
                    header.Alignment = int.Parse(node.Attributes["Alignment"].Value);
                if (node.Attributes["VerticalAlignment"] != null)
                    header.VerticalAlignment = int.Parse(node.Attributes["VerticalAlignment"].Value);
                headerList.Add(header);
            }
            return headerList;
        }
        /// <summary>
        /// 读取XML配置信息集
        /// </summary>
        /// <param name="xmlpath"></param>
        /// <returns></returns>
        public List<Regular> GetXMLInfo(string xmlpath)
        {
            var reader = new XmlTextReader(xmlpath);
            var doc = new XmlDocument();
            doc.Load(reader);
            var headerList = new List<Regular>();
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var header = new Regular();
                if (node.Attributes["firstHeaderRow"] != null)
                {
                    int ddd = int.Parse(node.Attributes["firstHeaderRow"].Value);
                    header.HeaderRegular.Add("firstHeaderRow", ddd);
                }
                if (node.Attributes["lastHeaderRow"] != null)
                    header.HeaderRegular.Add("lastHeaderRow", int.Parse(node.Attributes["lastHeaderRow"].Value.ToString()));
                if (node.Attributes["sheetCount"] != null)
                    header.HeaderRegular.Add("sheetCount", int.Parse(node.Attributes["sheetCount"].Value.ToString()));
                if (node.Attributes["headerText"] != null)
                    header.HeaderText = node.Attributes["headerText"].Value.ToString();
                if (node.Attributes["propertyName"] != null)
                    header.PropertyName = node.Attributes["propertyName"].Value.ToString();
                if (node.Attributes["dataType"] != null)
                    header.DataType = node.Attributes["dataType"].Value.ToString();
                headerList.Add(header);
            }
            return headerList;
        }

        /// <summary>
        /// 是否合并单元格
        /// </summary>
        /// <param name="cellIndex">单元格数</param>
        /// <param name="rowIndex">行</param>
        /// <param name="sheet">Sheet名称</param>
        /// <param name="firstRegionRow">开始合的行</param>
        /// <returns></returns>
        public bool IsMergedRegionCell(int cellIndex, int rowIndex, ISheet sheet, ref int firstRegionRow)
        {
            bool isMerged = false;
            var regionLists = GetMergedCellRegion(sheet);

            foreach (var cellRangeAddress in regionLists)
            {
                for (int i = cellRangeAddress.FirstRow; i <= cellRangeAddress.LastRow; i++)
                {
                    if (rowIndex == i)
                    {
                        for (int j = cellRangeAddress.FirstColumn; j <= cellRangeAddress.LastColumn; j++)
                        {
                            if (cellIndex == j)
                            {
                                isMerged = true;
                                firstRegionRow = cellRangeAddress.FirstRow;
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return isMerged;
        }

        /// <summary>
        /// 获取合并区域信息
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        private List<CellRangeAddress> GetMergedCellRegion(ISheet sheet)
        {
            int mergedRegionCellCount = sheet.NumMergedRegions;
            var returnList = new List<CellRangeAddress>();

            for (int i = 0; i < mergedRegionCellCount; i++)
            {
                returnList.Add(sheet.GetMergedRegion(i));
            }

            return returnList;
        }

    }
}
