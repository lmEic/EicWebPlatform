using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Lm.Eic.Uti.Common.YleeExtension.FileOperation
{
    public static class FileOperationExtension
    {
        /// <summary>
        /// 删除文件夹内的所有文件
        /// </summary>
        /// <param name="directory"></param>
        public static void DeleteFiles(this string directory)
        {
            if (Directory.Exists(directory))
            {
                var filesArr = Directory.GetFiles(directory);
                if (filesArr != null && filesArr.Length > 0)
                {
                    foreach (string filename in filesArr)
                    {
                        File.Delete(filename);
                    }
                }
            }
        }

        /// <summary>
        /// 获取文件中的内容,按行存储到列表中
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">读取编码</param>
        /// <returns></returns>
        public static List<string> GetFileLines(this string filePath, Encoding encoding)
        {
            List<string> Lines = new List<string>();
            if (!File.Exists(filePath))
            {
                Lm.Eic.Uti.Common.YleeMessage.Windows.WinMessageHelper.ShowMsg(string.Format("{0}文件不存在！", filePath), 3);
                return Lines;
            }
            using (StreamReader sr = new StreamReader(filePath, encoding))
            {
                string line = sr.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    Lines.Add(line);
                    line = sr.ReadLine();
                }
            }
            return Lines;
        }

        /// <summary>
        /// 获取文件中的内容,按行存储到列表中
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<string> GetFileLines(this string filePath)
        {
            return filePath.GetFileLines(Encoding.Default);
        }

        /// <summary>
        /// 对已经存在的文件进行内容附加
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileContent">要附加的文件内容</param>
        /// <param name="encoding">写入文件时采用的编码格式</param>
        public static void AppendFile(this string filePath, string fileContent, Encoding encoding)
        {
            string DirectoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(DirectoryPath)) Directory.CreateDirectory(DirectoryPath);
            using (StreamWriter sw = new StreamWriter(filePath, true, encoding))
            {
                sw.WriteLine(fileContent);
                sw.Flush();
            }
        }

        /// <summary>
        /// 向文件中写入文本内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileContent">文件内容</param>
        public static void AppendFile(this string filePath, string fileContent)
        {
            filePath.AppendFile(fileContent, Encoding.Default);
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileContent">创建文件时要写入的文件内容</param>
        /// <param name="encoding">写入文件时采用的编码格式</param>
        public static void CreateFile(this string filePath, string fileContent, Encoding encoding)
        {
            string DirectoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(DirectoryPath)) Directory.CreateDirectory(DirectoryPath);
            using (StreamWriter sw = new StreamWriter(filePath, false, encoding))
            {
                sw.WriteLine(fileContent);
                sw.Flush();
            }
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileContent">文件内容</param>
        public static void CreateFile(this string filePath, string fileContent)
        {
            filePath.CreateFile(fileContent, Encoding.Default);
        }

        /// <summary>
        /// 判定文件夹是否存在，若不存在，则自动创建
        /// </summary>
        /// <param name="directoryPath">文件夹路径</param>
        public static void ExistDirectory(this string directoryPath)
        {
            directoryPath = directoryPath.EndsWith("\\") ? directoryPath : directoryPath + "\\";
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
        }

        /// <summary>
        /// 判定文件是否存在
        /// </summary>
        /// <param name="filePath">文件名称路径</param>
        /// <returns></returns>
        public static bool ExistFile(this string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 获取文件留
        /// </summary>
        /// <param name="documentPatch">文档路径</param>
        /// <returns></returns>
        public static MemoryStream GetMemoryStream(string documentPatch)
        {
            if (!File.Exists(documentPatch))
                return new MemoryStream();

            byte[] data = File.ReadAllBytes(documentPatch);
            return new MemoryStream(data);
        }

        /// <summary>
        /// 扩展方法：使用NPOI将数据导入到Excel内存流
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="dataSource">数据集合</param>
        /// <param name="xlsSheetName">Sheet名称</param>
        /// <returns></returns>
        public static MemoryStream ExportToExcel<T>(this List<T> dataSource, string xlsSheetName) where T : class
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
                        FillIcell<T>(cellSytleDate, rowContent, entity, tpis, colIndex, colIndex);
                       
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
        /// 扩展方法：导入到现有的Excel模板文件中
        /// </summary>
        public static void ExportToExcelTemplateFile(this string templateFilePath, string xlsSheetName, string saveFileName, Action<ISheet> setDataToSheetHandler)
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
        /// <summary>
        ///  扩展方法：按所需字段导入到现有的Excel模板文件中
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="dataSource">实体数据源</param>
        /// <param name="xlsSheetName"></param>
        /// <param name="FieldMapList">所需字段</param>
        /// <returns></returns>
        public static MemoryStream ExportToExcel<T>(this List<T> dataSource, string xlsSheetName, List<FileFieldMapping> FieldMapList) where T : class ,new()
        {

            MemoryStream stream = new MemoryStream();
            if (dataSource == null || dataSource.Count == 0) return stream;
            HSSFWorkbook workbook = new HSSFWorkbook();
            try
            {
                ISheet sheet = WorkbookCreateSheet<T>(dataSource, xlsSheetName, FieldMapList, workbook);

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
        /// 创建Sheet表
        /// </summary>
        private static ISheet WorkbookCreateSheet<T>(List<T> dataSource, string xlsSheetName, List<FileFieldMapping> FieldMapList, HSSFWorkbook workbook) where T : class, new()
        {
            ISheet sheet = workbook.CreateSheet(xlsSheetName);
       
            ICellStyle cellSytleDate = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            cellSytleDate.DataFormat = format.GetFormat("yyyy年mm月dd日");

            #region 填充列头区域
            IRow rowHeader = sheet.CreateRow(0);
            //设置表头样式
            ICellStyle headStyle = workbook.CreateCellStyle();
            headStyle.Alignment = HorizontalAlignment.Center;
            IFont cellFontHeader = workbook.CreateFont(); 
            cellFontHeader.Boldweight = 700;
            cellFontHeader.FontHeightInPoints = 12;
            headStyle.SetFont(cellFontHeader);
            int forEachindex = 0;
            FieldMapList.ForEach(e =>
            {
                ICell cell = rowHeader.CreateCell(forEachindex);
                cell.SetCellValue(e.FieldDiscretion);
                cell.CellStyle = headStyle;
                forEachindex++;
            });
            #endregion 填充列头区域

            #region 对所需字段依数 填充内容区域

            for (int rowIndex = 0; rowIndex < dataSource.Count; rowIndex++)
            {
                IRow rowContent = sheet.CreateRow(rowIndex + 1);
                T entity = dataSource[rowIndex];
                Type tentity = entity.GetType();
                PropertyInfo[] tpis = tentity.GetProperties();
                int colIndex = 0;
                FieldMapList.ForEach(e =>
                {
                    //添加项次序号
                    if (e.FieldDiscretion == "项次")
                    {
                        ICell cellContent = rowContent.CreateCell(colIndex);
                        cellContent.SetCellValue((rowIndex+1).ToString());
                        colIndex++; 
                    }
                    else 
                    {
                      for (int tipsIndex = 0; tipsIndex < tpis.Length; tipsIndex++)
                      {
                        //如不是所需字段 跳过
                        if (e.FieldName != tpis[tipsIndex].Name) continue;
                        FillIcell<T>(cellSytleDate, rowContent, entity, tpis, tipsIndex, colIndex);
                        colIndex++; 
                       }
                    }
                
                });
            }


            #endregion 填充内容区域
            return sheet;
        }
        /// <summary>
        /// 填充表格值
        /// </summary>
        private static void FillIcell<T>(ICellStyle cellSytleDate, IRow rowContent, T entity, PropertyInfo[] tpis, int tipsIndex, int colIndex)
        {
            ICell cellContent = rowContent.CreateCell(colIndex);
            object value = tpis[tipsIndex].GetValue(entity, null);
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

        /// <summary>
        ///  扩展方法：数据按字段分组
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="dataSource">List数组</param>
        /// <param name="propertyStr">要分组的字段</param>
        /// <returns></returns>
        public static Dictionary<string, List<T>> GetGroupList<T>(this List<T> dataSource, string propertyStr) where T : class ,new ()
        {
            try
            {
                if (dataSource == null || dataSource.Count <= 0)
                    return null;

                Dictionary<string, List<T>> dicGroupingEntity = new Dictionary<string, List<T>>();
                List<string> groupList = new List<string>();

                T model = dataSource[0];
                PropertyInfo[] tpis = model.GetType().GetProperties();

                #region 获取属性待Index
                bool isFind = false;
                int propertyInext = 0;
                for (int index = 0; index < tpis.Length; index++)
                {
                    if (tpis[index].Name == propertyStr)
                    {
                        propertyInext = index;
                        isFind = true;
                        break;
                    }
                }
                #endregion

                #region 如果未找到指定待属性，返回
                if (!isFind)
                    return dicGroupingEntity;

                string groupName = string.Empty;
                //获取分组列表
                dataSource.ForEach(e =>
                {
                    groupName = e.GetType().GetProperties()[propertyInext].GetValue(e, null).ToString();
                    if (!groupList.Contains(groupName))
                    {
                        groupList.Add(groupName);
                    }
                });
                #endregion

                #region 依据分列表 进行分组
                foreach (string group in groupList)
                {
                    var trm = dataSource.FindAll(e => e.GetType().GetProperties()[propertyInext].GetValue(e, null).ToString() == group);
                    dicGroupingEntity.Add(group, trm);
                }
                #endregion

                return dicGroupingEntity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

       /// <summary>
       /// 扩展方法：把一组实体数据 安所需字段 导入到现有的Excel模板文件中
       /// </summary>
       /// <typeparam name="T">实体</typeparam>
       /// <param name="DicDataSources">一组实体数据</param>
       /// <param name="FieldMapList">所需字段</param>
       /// <returns></returns>
        public static MemoryStream ExportToExcelMultiSheets<T>(this Dictionary<string, List<T>> DicDataSources, List<FileFieldMapping> FieldMapList) where T : class ,new ()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                HSSFWorkbook workbook = new HSSFWorkbook();
                foreach (string i in DicDataSources.Keys)
                {
                    if (DicDataSources[i] == null || DicDataSources[i].Count == 0) continue;
                    ISheet sheet = WorkbookCreateSheet<T>(DicDataSources[i], i, FieldMapList, workbook);
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

    }
     /// <summary>
     /// 文件字段对应的描述
     /// </summary>
    public class FileFieldMapping
    {
        /// <summary>
        /// 文件字段描述
        /// </summary>
        public  string FieldDiscretion { set; get; }
        /// <summary>
        /// 文件字段名
        /// </summary>
        public string FieldName { set; get; }

        public void  SetField(string fieldName,string fieldDiscretion )
        {
            this.FieldDiscretion =fieldDiscretion;
            this.FieldName = fieldName;
        }
       
    }
}