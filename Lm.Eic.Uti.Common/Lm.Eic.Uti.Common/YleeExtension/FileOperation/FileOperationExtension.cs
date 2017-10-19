using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using Lm.Eic.Uti.Common.YleeMessage.Log;
using System.Linq;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;

namespace Lm.Eic.Uti.Common.YleeExtension.FileOperation
{
    public static class FileOperationExtension
    { 
        //林旺雷 2017-09-15
      /// <summary>
      ///  将实体类列表到处到Excel中 支持Excel2007
      /// </summary>
      /// <typeparam name="T">类型</typeparam>
      /// <param name="dt"></param>
      /// <param name="patch">绝对路径</param>
      /// <param name="isCreateTitle">是否生成标题</param>
        public static void ExportToExcel<T>(this List<T> dt, string patch, bool isCreateTitle, int inputStartRow)
        {
           // m_ExportToExcel(dt, patch, isCreateTitle, inputStartRow);
        }



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
        /// 删除文档
        /// </summary>
        /// <param name="FileDocumentationName"></param>
        /// <returns></returns>
        public static bool DeleteFileDocumentation(this string FileDocumentationName)
        {
            try
            {
                if (File.Exists(FileDocumentationName))
                {
                    File.Delete(FileDocumentationName);
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.InnerException.Message);
            }
        }
        /// <summary>
        /// 比较新旧文档是否相同，如果相同，则删除旧文档
        /// </summary>
        /// <param name="oldFileName"></param>
        /// <param name="newFileName"></param>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static void DeleteExistFile(this string oldFileName, string newFileName, string rootPath)
        {
            try
            {
                if (oldFileName != newFileName && oldFileName != string.Empty && oldFileName != null)
                {
                    if (rootPath != string.Empty && rootPath != null)
                    {
                        string fileName = Path.Combine(rootPath, oldFileName);
                        fileName = fileName.Replace("/", @"\");
                        if (File.Exists(fileName))
                            File.Delete(fileName);//删除旧的文件
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        /// <summary>
        /// 比较新旧文档是否相同，如果相同，则删除旧文档
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static void DeleteExistFile(this string fileName, string rootPath)
        {
            try
            {
                if (fileName != string.Empty && fileName != null)
                {
                    if (rootPath != string.Empty && rootPath != null)
                    {
                        string fileNamePath = Path.Combine(rootPath, fileName);
                        fileNamePath = fileNamePath.Replace("/", @"\");
                        if (File.Exists(fileNamePath))
                            File.Delete(fileNamePath);//删除旧的文件
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        /// <summary>
        /// 从文件夹中获取文件
        /// </summary>
        /// <param name="dirctoryPath"></param>
        /// <returns></returns>
        public static List<string> GetFiles(this string dirctoryPath)
        {
            List<string> fileList = new List<string>();
            if (!Directory.Exists(dirctoryPath))
            {
                Directory.CreateDirectory(dirctoryPath);
                return fileList;
            }
            string[] files = Directory.GetFiles(dirctoryPath);
            if (files != null && files.Length > 0)
                return files.ToList();
            return fileList;
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
            directoryPath = directoryPath.EndsWith("\\", StringComparison.CurrentCulture) ? directoryPath : directoryPath + "\\";
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
        /// 扩展方法：将数据下载到Excel文件
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="dataSource">数据集合</param>
        /// <param name="xlsSheetName">Sheet名称</param>
        /// <param name="fileDownLoadName">下载的文件名</param>
        /// <returns></returns>
        public static DownLoadFileModel ToDownLoadExcelFileModel<T>(this List<T> dataSource, string xlsSheetName, string fileDownLoadName) where T : class
        {

            if (dataSource == null || dataSource.Count == 0) return new DownLoadFileModel().Default();
            return dataSource.ExportToExcel(xlsSheetName).CreateDownLoadExcelFileModel(fileDownLoadName);
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
        public static MemoryStream ExportToExcel<T>(this List<T> dataSource, string xlsSheetName, List<FileFieldMapping> FieldMapList) where T : class, new()
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
            if (xlsSheetName == string.Empty) xlsSheetName = "Sheet1";
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

            if (FieldMapList == null || FieldMapList.Count < 1)
            {

                Type t = dataSource[0].GetType();
                PropertyInfo[] pis = t.GetProperties();
                for (int colIndex = 0; colIndex < pis.Length; colIndex++)
                {
                    ICell cell = rowHeader.CreateCell(colIndex);
                    cell.SetCellValue(pis[colIndex].Name);
                    cell.CellStyle = headStyle;
                }
            }
            else
            {
                  
                FieldMapList.ForEach(e =>
                {
                    ICell cell = rowHeader.CreateCell(forEachindex);
                    cell.SetCellValue(e.FieldDiscretion);
                    cell.CellStyle = headStyle;
                    forEachindex++;
                });
            }

            #endregion 填充列头区域

            #region 对所需字段依数 填充内容区域
            for (int rowIndex = 0; rowIndex < dataSource.Count; rowIndex++)
            {
                IRow rowContent = sheet.CreateRow(rowIndex + 1);
                T entity = dataSource[rowIndex];
                Type tentity = entity.GetType();
                PropertyInfo[] tpis = tentity.GetProperties();
                int colIndex = 0;
                if (FieldMapList == null || FieldMapList.Count < 1)
                {
                    for (int Index = 0; Index < tpis.Length; Index++)
                    {
                        FillIcell<T>(cellSytleDate, rowContent, entity, tpis, Index, Index);
                    }
                }
                else
                {
                    FieldMapList.ForEach(e =>
                    {
                        //添加项次序号
                        if (e.FieldDiscretion == "项次")
                        {
                            ICell cellContent = rowContent.CreateCell(colIndex);
                            cellContent.SetCellValue((rowIndex + 1).ToString());
                            colIndex++;
                        }
                        else
                        {
                            for (int tipsIndex = 0; tipsIndex < tpis.Length; tipsIndex++)
                            { //如不是所需字段 跳过
                                if (e.FieldName == tpis[tipsIndex].Name)
                                {
                                    FillIcell<T>(cellSytleDate, rowContent, entity, tpis, tipsIndex, colIndex);
                                    colIndex++;
                                    break;
                                }
                            }
                        }
                    });
                }
            }
            #endregion 填充内容区域
            return sheet;
        }
        /// <summary>
        /// 创建加班Excel表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSource"></param>
        /// <param name="xlsSheetName"></param>
        /// <param name="FieldMapList"></param>
        /// <param name="workbook"></param>
        /// <returns></returns>
        private static ISheet WorkOverHoursCreateSheet<T>(List<T> dataSource, string xlsSheetName, List<FileFieldMapping> FieldMapList, HSSFWorkbook workbook) where T : class, new()
        {
            if (xlsSheetName == string.Empty) xlsSheetName = "Sheet12";
            ISheet sheet = workbook.GetSheet(workbook.GetSheetName(0));         
            ICellStyle cellSytleDate = workbook.CreateCellStyle();         
            IDataFormat format = workbook.CreateDataFormat();
            cellSytleDate.DataFormat = format.GetFormat("yyyy-mm-dd");

            #region 填充列头区域
            // IRow rowHeader = sheet.CreateRow(2);
            // 设置表头样式
            //ICellStyle headStyle = workbook.CreateCellStyle();
            //headStyle.Alignment = HorizontalAlignment.Center;
            // IFont cellFontHeader = workbook.CreateFont();
            // cellFontHeader.Boldweight = 700;
            // cellFontHeader.FontHeightInPoints = 12;
            // headStyle.SetFont(cellFontHeader);           
            //  int forEachindex = 0;
            // if (FieldMapList == null || FieldMapList.Count < 1)
            // {
            //     Type t = dataSource[0].GetType();
            //     PropertyInfo[] pis = t.GetProperties();
            //     for (int colIndex = 0; colIndex < pis.Length; colIndex++)
            //     {
            //         ICell cell = rowHeader.CreateCell(colIndex);
            //        cell.SetCellValue(pis[colIndex].Name);
            //        //cell.CellStyle = headStyle;
            //     }
            // }
            // else
            // {

            //     FieldMapList.ForEach(e =>
            //     {
            //         ICell cell = rowHeader.CreateCell(forEachindex);//日期0，部门1，工号2
            //       //  cell.SetCellValue(e.FieldDiscretion);
            //        // cell.CellStyle = headStyle;
            //         forEachindex++;
            //     });
            // }

            #endregion 填充列头区域

            #region 填充内容区域

            #region 设定单元格参数
            IRow rowDepmentAndWorkdate = sheet.GetRow(2);
            IRow rowWorkType = sheet.GetRow(1);
            int row_day = 0;
            int row_day_45 = 0;
            int row_day_67 = 0;
            int row_night = 0;
            int row_night_45 = 0;
            int row_night_67 = 0;

            int colIndex1 = 2;
            int colIndex2 = 3;
            int colIndex3 = 4;

            int colIndex6 = 6;
            int colIndex7 = 7;
            int colIndex8 = 8;

            int colIndex10 = 10;
            int colIndex11 = 11;
            int colIndex12 = 12;

            int colIndex14 = 14;
            int colIndex15 = 15;
            int colIndex16 = 16;

            #endregion

            for (int rowIndex = 0; rowIndex < dataSource.Count; rowIndex++)
            {
                #region 设定行参数
                IRow rowContent_day = sheet.GetRow(rowIndex + 6);//白班行 6,7,8,9              
                IRow cowConter_line23 = sheet.GetRow((rowIndex - row_day) + 5);//换列(2,3)
                IRow cowConter_line45 = sheet.GetRow((rowIndex - row_day_45) + 5);//换列(4,5)
                IRow cowConter_line67 = sheet.GetRow((rowIndex - row_day_67) + 5);//换列(6,7)

                IRow rowConten_night = sheet.GetRow((rowIndex - row_day) + 23);//晚班行
                IRow cowConter__line23 = sheet.GetRow((rowIndex - row_day - row_night) + 23);//换列(2,3)
                IRow cowConter__line45 = sheet.GetRow((rowIndex - row_day - row_night_45) + 23);//换列(4,5)
                IRow cowConter__line67 = sheet.GetRow((rowIndex - row_day - row_night_67) + 23);//换列(6,7)

                #endregion

                T entity = dataSource[rowIndex];             
                Type tentity = entity.GetType();
                PropertyInfo[] tpis = tentity.GetProperties();
                int colIndex = 1;
                if (FieldMapList == null || FieldMapList.Count < 1)
                {
                    for (int Index = 0; Index < tpis.Length; Index++)
                    {
                        WorkHoursFillIcell<T>(cellSytleDate, rowContent_day, rowDepmentAndWorkdate, rowWorkType, entity, tpis, colIndex1, colIndex2, colIndex3);
                    }
                }
                else
                {
                    FieldMapList.ForEach(e =>
                    {
                        //添加项次序号
                        if (e.FieldDiscretion == "项次")
                        {
                            ICell cellContent = rowContent_day.CreateCell(colIndex);
                            cellContent.SetCellValue((rowIndex + 1).ToString());
                            colIndex++;//colIndex=1
                        }
                        else
                        {                                            
                            for (int tipsIndex = 0; tipsIndex < tpis.Length; tipsIndex++)//tpis=11
                            {                                
                                if (e.FieldName == tpis[tipsIndex].Name)//workdate 
                                {                                  
                                    object workClassType = tpis[6].GetValue(entity, null);
                                    if (workClassType.ToString() == "白班")
                                    {
                                        #region 导出白班
                                        //0,1
                                        if (rowIndex < 2)
                                        {
                                            WorkHoursFillIcell<T>(cellSytleDate, rowContent_day, rowDepmentAndWorkdate, rowWorkType, entity, tpis, colIndex1, colIndex2, colIndex3);
                                            colIndex++;
                                            row_day = rowIndex;//1
                                            break;
                                        }
                                        else
                                        {
                                            if (rowIndex > 5)
                                            {
                                                //6,7
                                                WorkHoursFillIcell<T>(cellSytleDate, cowConter_line67, rowDepmentAndWorkdate, rowWorkType, entity, tpis, colIndex14, colIndex15, colIndex16);
                                                colIndex++;
                                                row_day = rowIndex;
                                                break;
                                            }
                                            else
                                            {
                                                if (rowIndex > 3)
                                                {
                                                    //4,5
                                                    WorkHoursFillIcell<T>(cellSytleDate, cowConter_line45, rowDepmentAndWorkdate, rowWorkType, entity, tpis, colIndex10, colIndex11, colIndex12);
                                                    colIndex++;
                                                    row_day_67 = rowIndex;
                                                    break;
                                                }
                                                else
                                                {
                                                    // 2,3                                                                
                                                    WorkHoursFillIcell<T>(cellSytleDate, cowConter_line23, rowDepmentAndWorkdate, rowWorkType, entity, tpis, colIndex6, colIndex7, colIndex8);
                                                    colIndex++;
                                                    row_day_45 = rowIndex;
                                                    break;

                                                }
                                            }
                                        }

                                        #endregion
                                    }
                                    else
                                    {
                                        #region 导出晚班
                                        //8,9
                                        if (rowIndex < 10)
                                        {
                                            WorkHoursFillIcell<T>(cellSytleDate, rowConten_night, rowDepmentAndWorkdate, rowWorkType, entity, tpis, colIndex1, colIndex2, colIndex3);
                                            colIndex++;
                                            row_night = rowIndex - row_day;//1
                                            break;
                                        }
                                        else
                                        {
                                            if (rowIndex > 13)
                                            {
                                                //14,15
                                                WorkHoursFillIcell<T>(cellSytleDate, cowConter__line67, rowDepmentAndWorkdate, rowWorkType, entity, tpis, colIndex14, colIndex15, colIndex16);
                                                colIndex++;
                                                break;
                                            }
                                            else
                                            {
                                                if (rowIndex > 11)
                                                {
                                                    //12,13
                                                    WorkHoursFillIcell<T>(cellSytleDate, cowConter__line45, rowDepmentAndWorkdate, rowWorkType, entity, tpis, colIndex10, colIndex11, colIndex12);
                                                    colIndex++;
                                                    row_night_67 = rowIndex - row_day;
                                                    break;
                                                }
                                                else
                                                {
                                                    // 10,11                                                             
                                                    WorkHoursFillIcell<T>(cellSytleDate, cowConter__line23, rowDepmentAndWorkdate, rowWorkType, entity, tpis, colIndex6, colIndex7, colIndex8);
                                                    colIndex++;
                                                    row_night_45 = rowIndex - row_day;
                                                    break;

                                                }
                                            }
                                        }

                                        #endregion
                                    }

                                }                             
                            }
                        }
                    });
                }

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
        /// 填充表格值
        /// </summary>
        private static void WorkHoursFillIcell<T>(ICellStyle cellSytleDate, IRow rowContent,IRow rowDeparmentAndWorkdate,IRow rowWorkType, T entity, PropertyInfo[] tpis, int colindex1, int colindex2,int colindex3)
        {
          //  ICell cellContent = rowContent.GetCell(colIndex);//1,2，3，4，5，6，7       
          //  object value = tpis[tipsIndex].GetValue(entity, null);//2017-10-13,部门名称
            object workId = tpis[0].GetValue(entity, null);//1187
            object workName = tpis[1].GetValue(entity, null);//张三
            object department = tpis[2].GetValue(entity, null);//企业信息中心
            object workdate = tpis[3].GetValue(entity, null);//2017/10/16
            object workhours = tpis[4].GetValue(entity, null);//2.5
            object worktype = tpis[5].GetValue(entity, null);//平时加班
            object workclasstype = tpis[6].GetValue(entity, null);//白班      
       
            rowWorkType.GetCell(15).SetCellValue(worktype.ToString());
            rowDeparmentAndWorkdate.GetCell(1).SetCellValue(department.ToString());
            DateTime dateV;
            DateTime.TryParse(((DateTime)workdate).ToString("yyyy-MM-dd HH:mm"), out dateV);
            rowDeparmentAndWorkdate.GetCell(11).SetCellValue(dateV.ToShortDateString());
           // rowContent.GetCell(1).SetCellValue(workclasstype.ToString()); 
            rowContent.GetCell(colindex1).SetCellValue(workId.ToString());
            rowContent.GetCell(colindex2).SetCellValue(workName.ToString());         
            double doubV = 0;
            double.TryParse(workhours.ToString(), out doubV);
            rowContent.GetCell(colindex3).SetCellValue(doubV);
                  
            //switch (type.ToString())
            //{
            //    case "System.String"://字符串类型
            //       cellContent.SetCellValue(value.ToString());                     
            //        break;
            //    case "System.DateTime"://日期类型
            //        DateTime dateV;
            //        DateTime.TryParse(((DateTime)value).ToString("yyyy-MM-dd HH:mm"), out dateV);
            //        cellContent.SetCellValue(dateV.ToShortDateString());  
            //        //rowContent.GetCell(5).SetCellValue(dateV.ToShortDateString());                  
            //        break;
            //    case "System.Boolean"://布尔型
            //        bool boolV = false;
            //        if (bool.TryParse(value.ToString(), out boolV))
            //            cellContent.SetCellValue(boolV);
            //        else
            //            cellContent.SetCellValue("解析布尔型数据错误");
            //        break;

            //    case "System.Int16"://整型
            //    case "System.Int32":
            //    case "System.Int64":
            //    case "System.Byte":
            //        int intV = 0;
            //        if (int.TryParse(value.ToString(), out intV))
            //            cellContent.SetCellValue(intV);
            //        else
            //            cellContent.SetCellValue("解析整型数据错误");
            //        break;

            //    case "System.Decimal"://浮点型
            //    case "System.Double":
            //        double doubV = 0;
            //        if (double.TryParse(value.ToString(), out doubV))
            //            cellContent.SetCellValue(doubV);
            //        else
            //            cellContent.SetCellValue("解析浮点型或双精度型数据错误");
            //        break;

            //    case "System.DBNull"://空值处理
            //        cellContent.SetCellValue("");
            //        break;

            //    default:
            //        cellContent.SetCellValue("");
            //        break;
            //}
        }

        /// <summary>
        ///  扩展方法：数据按字段分组
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="dataSource">List数组</param>
        /// <param name="propertyStr">要分组的字段</param>
        /// <returns></returns>
        public static Dictionary<string, List<T>> GetGroupList<T>(this List<T> dataSource, string propertyStr=null ) where T : class, new()
        {
            try
            {
                if (dataSource == null || dataSource.Count <= 0)
                    return null;
                if (propertyStr == null) propertyStr = string.Empty;
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
                {
                    dicGroupingEntity.Add(propertyStr, dataSource);
                    return dicGroupingEntity;
                }

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
        /// 扩展方法，从Excel文件中读取数据到泛型集合模型中
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<TEntity> GetEntitiesFromExcel<TEntity>(this string fileName)where TEntity:class,new ()
        {
            string errMsg = string.Empty;
            List<TEntity> datas = ExcelHelper.ExcelToEntityDatas<TEntity>(fileName, out errMsg);
            MessageLogger.LogMsgToFile("GetEntitiesFromExcel<TEntity>", errMsg);
            return datas;
        }

        /// <summary>
        /// 扩展方法：把一组实体数据 安所需字段 导入到现有的Excel模板文件中
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="DicDataSources">一组实体数据</param>
        /// <param name="FieldMapList">所需字段</param>
        /// <returns></returns>
        public static MemoryStream ExportToExcelMultiSheets<T>(this Dictionary<string, List<T>> DicDataSources, List<FileFieldMapping> FieldMapList) where T : class, new()
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
        /// <summary>
        /// 扩展方法二:导入Excel模板文件中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="DicDataSources"></param>
        /// <param name="FieldMapList"></param>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static MemoryStream WorkOverHoursListToExcel<T>(this Dictionary<string, List<T>> DicDataSources, List<FileFieldMapping> FieldMapList,string  filepath) where T : class, new()
        {
            try
            {
                MemoryStream stream = new MemoryStream();             
                HSSFWorkbook workbook =(HSSFWorkbook)WorkbookFactory.Create(filepath);          
                foreach (string i in DicDataSources.Keys)
                {
                    if (DicDataSources[i] == null || DicDataSources[i].Count == 0) continue;

                    ISheet sheet = WorkOverHoursCreateSheet<T>(DicDataSources[i], i, FieldMapList, workbook);
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


        /// <summary>
        /// 根据文件名称获取下载文件类型
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetDownLoadContentType(this string fileName)
        {
            Dictionary<string, string> dicContentType = new Dictionary<string, string>() {
                {".txt","text/plain"},
                {".zip","application/x-zip-compressed"},
                {".pdf","application/pdf"},
                {".doc","application/msword"},
                {".ppt","application/x-ppt"},
                {".xls","application/vnd.ms-excel"},
                {".png","application/x-png"},
                {".jpeg","image/jpeg"},
                {".jpg","application/x-jpg"},
            };
            string extentionName = Path.GetExtension(fileName);
            return dicContentType[extentionName];
        }
        /// <summary>
        /// 根据站点根路径和文件相对路径获取文件下载全路径路径
        /// </summary>
        /// <param name="webSiteRootPath">站点根路径</param>
        /// <param name="fileRelativePath">文件相对路径</param>
        /// <returns></returns>
        public static string GetDownLoadFilePath(this string webSiteRootPath, string fileRelativePath)
        {
            return Path.Combine(webSiteRootPath, fileRelativePath.Replace('/', '\\'));
        }
        /// <summary>
        /// 创建Excel文件下载模型
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DownLoadFileModel CreateDownLoadExcelFileModel(this MemoryStream ms, string fileName)
        {
            DownLoadFileModel dlfm = new DownLoadFileModel(2);
            if (ms == null) return dlfm.Default();
            dlfm.FileStream = ms;
            dlfm.ContentType = "application/vnd.ms-excel";
            dlfm.FileDownLoadName = fileName+ ".xls";
            return dlfm;



        }
        /// <summary>
        /// 获取加班Excel模板
        /// </summary>
        /// <param name="documentPath"></param>
        /// <returns></returns>
        public static DownLoadFileModel WorkOverExcelTemplae(this MemoryStream ms, string fileName)
        {
            DownLoadFileModel dlfm = new DownLoadFileModel(2);
            if (ms == null) return dlfm.Default();
            dlfm.FileStream = ms;
            dlfm.ContentType = "application/vnd.ms-excel";
            dlfm.FileDownLoadName = fileName+ ".xls";
            return dlfm;
        }
       
        /// <summary>
        /// 获取工序Excel模板
        /// </summary>
        /// <param name="documentPath"></param>
        /// <returns></returns>
        public static DownLoadFileModel GetProductFlowTemplate(string siteRootPath, string documentPath, string fileName)
        {
            DownLoadFileModel dlfm = new DownLoadFileModel();
            return dlfm.CreateInstance
                 (siteRootPath.GetDownLoadFilePath(documentPath),
                 fileName.GetDownLoadContentType(),
                  fileName);
        }
        /// <summary>
        /// 创建异常信息下载模型
        /// </summary>
        /// <param name="exceptionId"></param>
        /// <returns></returns>
        public static DownLoadFileModel CreateExceptionDownLoadFileModel(this string exceptionId)
        {
            DownLoadFileModel dlfm = new DownLoadFileModel();
            string errorLogPath = @"C:\EicSystem\WebPlatform\ErrorMsgTrace\";
            errorLogPath.GetFiles().ForEach(f =>
            {
                if (f.IndexOf(exceptionId, StringComparison.CurrentCulture) > 0)
                {
                    StringBuilder sbMsg = new StringBuilder();
                    f.GetFileLines().ForEach(line =>
                    {
                        sbMsg.AppendLine(line);
                    });
                    dlfm = dlfm.Default(sbMsg.ToString());
                    File.Delete(f);
                }
                else
                {
                    dlfm = dlfm.Default();
                }
            });
            return dlfm;
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
        string _fieldDiscretion;
        public string FieldDiscretion { get { return _fieldDiscretion; } }
        /// <summary>
        /// 文件字段名
        /// </summary>
        string _fieldName;
        public string FieldName { get { return _fieldName; } }

        public FileFieldMapping(string fieldName, string fieldDiscretion)
        {
            this._fieldDiscretion = fieldDiscretion;
            this._fieldName = fieldName;
        }

    }
    /// <summary>
    /// 下载文件模型
    /// </summary>
    public class DownLoadFileModel
    {
        /// <summary>
        /// 下载文件处理类型，只读属性
        /// 默认为0:给定文件路径进行下载
        /// 1：给定字节进行下载
        /// 2：给定文件流进行下载
        /// </summary>
        public int HandleMode { get; private set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 下载文件名称
        /// </summary>
        public string FileDownLoadName { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 文件内容字节
        /// </summary>
        public byte[] FileContnet { get; set; }
        /// <summary>
        /// 文件流
        /// </summary>
        public MemoryStream FileStream { get; set; }
        public DownLoadFileModel()
        {
            //默认为
            this.HandleMode = 0;
        }

        public DownLoadFileModel(int handleMode)
        {
            this.HandleMode = handleMode;
        }
        /// <summary>
        /// 默认模型实例，返回含有错误信息的文件模型
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public DownLoadFileModel Default(string errorMsg = "没有找到此文件记录")
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(errorMsg);
            return new DownLoadFileModel()
            {
                HandleMode = 1,
                ContentType = "text/plain",
                FileContnet = data,
                FileDownLoadName = "error.txt"
            };

        }

        public DownLoadFileModel CreateInstance(string filePath, string contentType, string downFileName)
        {
            return new DownLoadFileModel()
            {
                FilePath = filePath,
                ContentType = contentType,
                FileDownLoadName = downFileName
            };
        }
    }
    /// <summary>
    /// 异常追踪模型
    /// </summary>
    public class ExceptionTraceModel
    {
        public string ExceptionId { get; set; }

        public string FnName { get; set; }

        public string MsgContent { get; set; }

        public string StackTrace { get; set; }

        public string MsgSource { get; set; }

        public string OccurTime { get; set; }
    }


   



}