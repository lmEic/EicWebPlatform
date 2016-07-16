using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.DbAccess.Bpm.Repository.QuantityRep;
using Lm.Eic.App.Erp.Bussiness.QuantityManage;
using Lm.Eic.App.Erp.Domain .QuantityModel;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using Excel;
namespace Lm.Eic.App.Business.Bmp.Quantity
{
    public class IQCSampleItemsRecordManager
    {
        IIQCSampleItemRecordReposity irep = null;
        public IQCSampleItemsRecordManager ()
        {
            irep = new IQCSampleItemRecordReposity();
        }
      
         #region        UID
       public void  InsertModel(IQCSampleItemRecordModel model)
         {
             irep.Insert(model); 
         }
          
         public void UpdateModel (IQCSampleItemRecordModel model)
         {
             irep.Update(e => e.Id_key == model.Id_key, model);
         }
 
         public void DeleteModel(IQCSampleItemRecordModel model)
         {
             irep.Delete(e => e.Id_key == model.Id_key);
         }
 
         public object  Get_Id_Key_By(IQCSampleItemRecordModel model)
        {
             return irep.Entities.Where(e => e.OrderID == model.OrderID & e.SampleMaterial == model.SampleMaterial & e.SampleItem == model.SampleItem).Select(e => e.Id_key).FirstOrDefault().ToString();
         }
         #endregion
        /// <summary>
       /// 
       /// </summary>
       /// <param name="orderid"></param>
       /// <returns></returns>
        public  List<IQCSampleItemRecordModel> GetSamplePrintItemBy(string orderid)
        {
            return irep.Entities.Where (e=>e.OrderID ==orderid ).ToList ();
        }
        /// <summary>
        /// 得到IQC抽样项次 （单身）
        /// </summary>
        /// <param name="Orderid"></param>
        /// <param name="SampleMaterial"></param>
        /// <returns></returns>
        public  List<IQCSampleItemRecordModel> GetPringSampleItemBy(string Orderid,string SampleMaterial)
        {
            return irep.Entities.Where(e => e.OrderID == Orderid & e.SampleMaterial == SampleMaterial).ToList();
        }
     
        /// <summary>
        /// 得到抽样物料信息 （单头）
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public List<MaterialModel> GetPuroductSupplierInfo(string orderid)
        {
            return   QuantityDBManager.QuantityPurchseDb.FindMaterialBy(orderid);
        }

        public int GetMaiterialConunt(string SampleMaterial)
        {
            string Myyear = DateTime.Now.Year.ToString() + "-01-01";
            DateTime n = Convert.ToDateTime(Myyear);
            List<IQCSampleItemRecordModel> nn = irep.Entities.Where(e => e.SampleMaterial == SampleMaterial && e.PrintCount  != 0 & e.SampleMaterialInDate >= n).ToList();
            if (nn != null)
                return nn.Count;
            else return 0;
        }
   
        /// <summary>
        ///  IQC 导出Excel 数据流
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="xlsSheetName"></param>
        /// <returns></returns>
        public  System.IO.MemoryStream   ExportPrintToExcel(List<IQCSampleItemRecordModel> dataSource) 
        {
            try
            {
            //数据为Null时返回数值
                 System.IO.MemoryStream stream = new System.IO.MemoryStream();
                 if (dataSource == null || dataSource.Count == 0) return stream;
                 string filePath = System.IO.Path.GetFullPath(PrintSampleModel(dataSource));
                 NPOI.HSSF.UserModel.HSSFWorkbook workbook = InitializeWorkbook(filePath);
                 if (workbook == null) return null;
                 NPOI.SS.UserModel.ISheet sheet = workbook.GetSheetAt(0);
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
        /// 初始化工作簿
        /// </summary>
        private NPOI.HSSF.UserModel.HSSFWorkbook InitializeWorkbook(string sNewFileName)
        {
            try
            {
                NPOI.HSSF.UserModel.HSSFWorkbook hssfworkbook = null;
                System.IO.FileStream file = new System.IO.FileStream(sNewFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                if (null == file)
                { return hssfworkbook; }
                hssfworkbook = new NPOI.HSSF.UserModel.HSSFWorkbook(file);
                if (null == hssfworkbook)
                { return hssfworkbook; }
                //create a entry of DocumentSummaryInformation
                NPOI.HPSF.DocumentSummaryInformation dsi = NPOI.HPSF.PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "test";
                hssfworkbook.DocumentSummaryInformation = dsi;
                //create a entry of SummaryInformation
                NPOI.HPSF.SummaryInformation si = NPOI.HPSF.PropertySetFactory.CreateSummaryInformation();
                si.Subject = "test";
                hssfworkbook.SummaryInformation = si;
                return hssfworkbook;
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.ToString());
            }
           
        }

        #region  PrintExcel
              #region EXCEL表格数据处理

        /// <summary>
        /// J列到V列 加虚线，A到V列 加实线
        /// </summary>
        /// <param name="xlsSheet"></param>
        /// <param name="StartRowIndex"></param>
        /// <param name="RowIndex"></param>
        private void ResetXlsCellStatus(Excel.Worksheet xlsSheet, int StartRowIndex, int RowIndex, string A, string J, string V)
        {
            int m = StartRowIndex + RowIndex + 1;

            xlsSheet.get_Range(J + StartRowIndex, V + m).Borders.get_Item(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlDashDot;//加虚线
            xlsSheet.get_Range(A + m, V + m).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;    //加实线  抵部不好加  顶部加1

        }
        private void ResetXlsCellStatus(Excel.Worksheet xlsSheet, int StartRowIndex, int RowIndex, string A, string V)
        {
            int m = StartRowIndex + RowIndex + 1;
            xlsSheet.get_Range(A + m, V + m).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;    //加实线  抵部不好加  顶部加1

        }
        /// <summary>
        /// 合并B C 二列
        /// </summary>
        /// <param name="xlsSheet"></param>
        /// <param name="MergeRowIndex">合并二列的行数</param>
        //private void MergeXlsCell(Excel.Worksheet xlsSheet, int MergeRowIndex)
        //{
        //    xlsSheet.get_Range("B" + MergeRowIndex, "C" + MergeRowIndex).Merge();
        //}

        /// <summary>
        /// BC列不合并时处理数据 ，并处理一合并时清除的一些线
        /// </summary>
        /// <param name="xlsSheet"></param>
        /// <param name="MergeRowIndex"></param>
        /// <param name="ValueUp"></param>
        /// <param name="ValueDown"></param>
        /// <param name="StandardValue"></param>
        private void setBCValueToXlsCell(Excel.Worksheet xlsSheet, int StartRowIndex, string ValueUp, string ValueDown, string StandardValue)
        {
            InsertStandardValue(xlsSheet, StartRowIndex, StandardValue, "B");
            InserUpDownValue(xlsSheet, StartRowIndex, ValueUp, ValueDown, "C");
            int StopRowIndex = StartRowIndex + 1;
            xlsSheet.get_Range("B" + StartRowIndex, "C" + StopRowIndex).Borders.LineStyle = Excel.XlLineStyle.xlLineStyleNone;
            xlsSheet.get_Range("B" + StartRowIndex, "C" + StopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("B" + StartRowIndex, "C" + StopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("A" + StartRowIndex, "A" + StopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("A" + StartRowIndex, "A" + StopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("F" + StartRowIndex, "H" + StopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("F" + StartRowIndex, "H" + StopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
        }
        /// <summary>
        /// 插入上限，下限的值
        /// </summary>
        /// <param name="xlsSheet"></param>
        /// <param name="StartRowIndex"></param>
        /// <param name="ValueUp"></param>
        /// <param name="ValueDown"></param>
        /// <param name="Column"></param>
        private static void InserUpDownValue(Excel.Worksheet xlsSheet, int StartRowIndex, string ValueUp, string ValueDown, string Column)
        {
            int StopRowIndex = StartRowIndex + 1;

            xlsSheet.get_Range(Column + StartRowIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;//水平对齐靠右
            xlsSheet.get_Range(Column + StartRowIndex).VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;//垂直对对齐 向下
            xlsSheet.get_Range(Column + StartRowIndex).NumberFormatLocal = "@";//设置为文本
            xlsSheet.get_Range(Column + StartRowIndex).Value = ValueUp;

            xlsSheet.get_Range(Column + StartRowIndex).Font.Size = "8"; //设置字体大小
            xlsSheet.get_Range(Column + StartRowIndex).Font.Name = "宋体";//设置字体


            xlsSheet.get_Range(Column + StopRowIndex).NumberFormatLocal = "@";//设置为文本
            xlsSheet.get_Range(Column + StopRowIndex).Value = ValueDown;
            xlsSheet.get_Range(Column + StopRowIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;//水平对齐靠右
            xlsSheet.get_Range(Column + StopRowIndex).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;//垂直对对齐 向上
            xlsSheet.get_Range(Column + StopRowIndex).Font.Size = "8"; //设置字体大小
            xlsSheet.get_Range(Column + StopRowIndex).Font.Name = "宋体";//设置字体
        }
        /// <summary>
        /// B 插入标准值
        /// </summary>
        /// <param name="xlsSheet"></param>
        /// <param name="StartRowIndex"></param>
        /// <param name="StandardValue"></param>
        /// <param name="Column"></param>
        private static void InsertStandardValue(Excel.Worksheet xlsSheet, int StartRowIndex, string StandardValue, string Column)
        {
            int StopRowIndex = StartRowIndex + 1;
            xlsSheet.get_Range(Column + StartRowIndex, "B" + StopRowIndex).Merge();//合并单元格
            xlsSheet.get_Range(Column + StartRowIndex, "B" + StopRowIndex).Value = StandardValue;
            xlsSheet.get_Range(Column + StartRowIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;//水平对齐靠左
            xlsSheet.get_Range(Column + StartRowIndex).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;//垂直对对齐 居中
            xlsSheet.get_Range(Column + StartRowIndex).NumberFormatLocal = "@";//设置为文本
            xlsSheet.get_Range(Column + StartRowIndex).Font.Size = "12"; //设置字体大小
            xlsSheet.get_Range(Column + StartRowIndex).Font.Name = "宋体";//设置字体


        }
        /// <summary>
        /// BC列合并时处理数据
        /// </summary>
        /// <param name="xlsSheet"></param>
        /// <param name="StartMergeRowIndex"></param>
        /// <param name="ValueUp"></param>
        /// <param name="ValueDown"></param>
        /// <param name="StandardValue"></param>
        private void setBCValueToXlsCell(Excel.Worksheet xlsSheet, int StartMergeRowIndex, int EndMergeRowIndex, string StandardValue)
        {

            xlsSheet.get_Range("B" + StartMergeRowIndex, "C" + EndMergeRowIndex).Merge();//合并单元格
            xlsSheet.get_Range("B" + StartMergeRowIndex).Value = StandardValue;
            xlsSheet.get_Range("B" + StartMergeRowIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;//水平对齐靠左
            xlsSheet.get_Range("B" + StartMergeRowIndex).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;//垂直对对齐 居中
            xlsSheet.get_Range("B" + StartMergeRowIndex).NumberFormatLocal = "@";//设置为文本
            xlsSheet.get_Range("B" + StartMergeRowIndex).Font.Size = "12"; //设置字体大小
            xlsSheet.get_Range("B" + StartMergeRowIndex).Font.Name = "宋体";//设置字体
        }
        private static void setDEmethod(Excel.Worksheet xlsSheet, int StartMergeRowIndex, int EndMergeRowIndex)
        {
            xlsSheet.get_Range("D" + StartMergeRowIndex, "E" + EndMergeRowIndex).Borders.LineStyle = Excel.XlLineStyle.xlLineStyleNone;

            xlsSheet.get_Range("D" + StartMergeRowIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;//水平对齐靠左
            xlsSheet.get_Range("E" + StartMergeRowIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;//水平对齐靠左
            xlsSheet.get_Range("D" + StartMergeRowIndex, "E" + EndMergeRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("D" + StartMergeRowIndex, "E" + EndMergeRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("D" + StartMergeRowIndex, "E" + EndMergeRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("A" + StartMergeRowIndex, "H" + EndMergeRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
        }
        private static void setCPDEmethod(Excel.Worksheet xlsSheet, int StartMergeRowIndex, int EndMergeRowIndex)
        {
            xlsSheet.get_Range("H" + StartMergeRowIndex, "I" + EndMergeRowIndex).Borders.LineStyle = Excel.XlLineStyle.xlLineStyleNone;
            xlsSheet.get_Range("H" + StartMergeRowIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;//水平对齐靠左
            xlsSheet.get_Range("I" + StartMergeRowIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;//水平对齐靠左
            xlsSheet.get_Range("H" + StartMergeRowIndex, "I" + EndMergeRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("H" + StartMergeRowIndex, "I" + EndMergeRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("H" + StartMergeRowIndex, "I" + EndMergeRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("A" + StartMergeRowIndex, "H" + EndMergeRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
        }
        /// <summary>
        /// 清除填充区域内的内容
        /// </summary>
        /// <param name="xlsSheet"></param>
        private void ClearContentsXlsCell(Excel.Worksheet xlsSheet, string statIndex)
        {
            xlsSheet.get_Range(statIndex).ClearContents();
        }
        /// <summary>
        /// 打印并退出
        /// </summary>
        /// <param name="xlsSheet"></param>
        private void PrintXls(Excel.Worksheet xlsSheet)
        {

            ((Excel._Worksheet)xlsSheet).Activate();
            xlsSheet.PrintOut();


        }
        /// <summary>
        /// 填充标准值
        /// </summary>
        /// <param name="xlsSheet"></param>
        /// <param name="StartRowIndex"></param>
        /// <param name="StopRowIndex"></param>
        /// <param name="ColName"></param>
        /// <param name="Value"></param>
        private void setValueToXlsCell(Excel.Worksheet xlsSheet, int StartRowIndex, int StopRowIndex, string ColName, string Value)
        {
            if (StartRowIndex == StopRowIndex)
            {
                xlsSheet.get_Range(ColName + StartRowIndex, ColName + StopRowIndex).Value = Value;

            }
            else
            {
                xlsSheet.get_Range(ColName + StartRowIndex, ColName + StopRowIndex).Merge();
                xlsSheet.get_Range(ColName + StartRowIndex, ColName + StopRowIndex).Value = Value;

            }
            xlsSheet.get_Range(ColName + StartRowIndex, ColName + StopRowIndex).WrapText = true;
            xlsSheet.get_Range(ColName + StartRowIndex, ColName + StopRowIndex).Borders.Weight = Excel.XlBorderWeight.xlThin;
            xlsSheet.get_Range(ColName + StartRowIndex, ColName + StopRowIndex).HorizontalAlignment = -4108;
            xlsSheet.get_Range(ColName + StartRowIndex, ColName + StopRowIndex).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range(ColName + StartRowIndex, ColName + StopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range(ColName + StartRowIndex, ColName + StopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range(ColName + StartRowIndex, ColName + StopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range(ColName + StartRowIndex, ColName + StopRowIndex).EntireColumn.ShrinkToFit = true;//自动缩小字体填充   
            xlsSheet.get_Range(ColName + StartRowIndex, ColName + StopRowIndex).EntireColumn.AutoFit();//自动调整列宽 
        }
        /// <summary>
        /// 从J列开始 画13列 垂直线
        /// </summary>
        /// <param name="xlsSheet"></param>
        /// <param name="StartRowIndex"></param>
        /// <param name="StopRowIndex"></param>
        private static void xlEdgeLeftLine(Excel.Worksheet xlsSheet, int StartRowIndex, int StopRowIndex, int Nunmber, char StartNunmber)
        {
            for (int i = 0; i < Nunmber; i++)
            {
                string ColName = Convert.ToChar(StartNunmber + i).ToString();
                xlsSheet.get_Range(ColName + StartRowIndex, ColName + StopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous;
            }
        }
        /// <summary>
        /// 打印，超出打印范围提示不打印
        /// </summary>
        /// <param name="xlsSheet"></param>
        private void PrinttheExcel(Excel.Worksheet xlsSheet, string desktopPath)
        {
            double mm = 0;
            foreach (Range col in xlsSheet.UsedRange.Columns)
            {
                mm = mm + col.ColumnWidth;
            }
            if (mm > 130)  //超出打印
            {



            }
            else { PrintXls(xlsSheet); }

            //打印后清除填充区域内的内容
            //ClearContentsXlsCell(xlsSheet);
            //解除打印前的合并单元格，及去掉合并单元格的外边框线
            //unMergeXlsCell(xlsSheet);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="xlsSheet"></param>
        /// <param name="xlsBook"></param>
        /// <param name="xlsSheetName"></param>
        private string SaveAsExcel(Excel.Worksheet xlsSheet, Excel.Workbook xlsBook)
        {
            //string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) ;
            string desktopPath = @"C:\NPOI";
            if (System.IO.Directory.Exists(desktopPath) == false)
            {
                System.IO.Directory.CreateDirectory(desktopPath);
            }
            desktopPath = desktopPath + "\\" + "IQC";
            if (System.IO.Directory.Exists(desktopPath) == true)
            {
                System.IO.Directory.Delete(desktopPath);
            }
            xlsSheet.SaveAs(desktopPath, 56); //保存在
            ////OpenXls(desktopPath);
            //PrinttheExcel(xlsSheet, desktopPath);
            xlsBook.Close();
            xlsSheet = null;
            KillProcess("Excel");
            string dd = System.IO.Path.GetExtension(desktopPath);


            return desktopPath + ".xls";

        }
        /// <summary>
        /// 结束 Excel
        /// </summary>
        /// <param name="processName"></param>
        public void KillProcess(string processName)
        {
            foreach (var process in System.Diagnostics.Process.GetProcesses())
            {
                if (process.ProcessName == processName)
                {

                    process.Kill();
                }
            }
        }
        #endregion

              #region 判定条件
        private bool JudgeNoNull(string up, string down)
        {
            if (up == null || down == null) return false;
            if (up.Length > 0 || down.Length > 0)
            { return true; }
            else { return false; }
        }
        private bool JudgeMach(string up, string down)
        {
            try
            {
                double upValue = Convert.ToDouble(up);
                double downValue = Convert.ToDouble(down);
                return true;
            }
            catch { return false; }

        }
        private bool JudgeAbs(double up, double down)
        {
            if (Math.Abs(up) == Math.Abs(down))
            { return true; }
            else { return false; }
        }
        private bool JudgeNoEqual(string UpValue, string DownValue)
        {
            if (JudgeNoNull(UpValue, DownValue))
            {
                if (JudgeMach(UpValue, DownValue))
                {
                    double upValue = Convert.ToDouble(UpValue);
                    double downValue = Convert.ToDouble(DownValue);
                    if (!JudgeAbs(upValue, downValue))
                    { return true; }
                    return false;
                }
                return false;
            }
            return false;
        }
        private void JudgeUpDown(Excel.Worksheet xlsSheet, int IdNumStartRowIndex, int RowIndex, string SizeSpec, string SizeSpecUP, string SizeSpecDown)
        {
            try
            {
                int IdNumStopRowIndex = IdNumStartRowIndex + RowIndex - 1;
                string UpValue = SizeSpecUP;
                string DownValue = SizeSpecDown;
                if (JudgeNoNull(UpValue, DownValue) & JudgeMach(UpValue, DownValue))
                {
                    double upValue = Convert.ToDouble(UpValue);
                    double downValue = Convert.ToDouble(DownValue);
                    if (JudgeAbs(upValue, downValue))
                    {
                        string Value = SizeSpec + "±" + Math.Abs(upValue).ToString();
                        setBCValueToXlsCell(xlsSheet, IdNumStartRowIndex, IdNumStopRowIndex, Value);
                    }
                    else
                    {
                        for (int i = 0; i < RowIndex / 2; i++)
                        {
                            int StartRowIndex = IdNumStartRowIndex + i * 2;
                            setBCValueToXlsCell(xlsSheet, StartRowIndex, SizeSpecUP, SizeSpecDown, SizeSpec);
                        }
                    }
                }
                else
                {
                    string Value = SizeSpec + UpValue + DownValue;
                    setBCValueToXlsCell(xlsSheet, IdNumStartRowIndex, IdNumStopRowIndex, Value);
                }
            }

            catch (Exception ex)
            {

            }

        }
        #endregion

              #region   Print IQC


        /// <summary>
        /// 打开Excel
        /// </summary>
        /// <param name="ExcelsheetIndex"></param>
        /// <returns></returns>
        private static Excel.Workbook OpenTheExcel(string PrintIQCDataXlsPath)
        {
            string fileName = @"C:\lmSpc\System\ProductSizeSpecPicture\品保课\IQC.xls";
            Excel.Application xlsApp = new Excel.Application();
            Excel.Workbook xlsBook = xlsApp.Workbooks.Add(fileName);
            xlsApp.DisplayAlerts = false;
            xlsApp.Visible = false;
            return xlsBook;
        }
        /// <summary>
        /// 填充上下差值
        /// </summary>
        /// <param name="xlsSheet"></param>
        /// <param name="IdNumStartRowIndex"></param>
        /// <param name="RowIndeX"></param>
        /// <param name="model"></param>
        private void SetMaterialInfoToXlsCell(Excel.Worksheet xlsSheet, int IdNumStartRowIndex, int RowIndeX, IQCSampleItemRecordModel model)
        {
            int IdNumStopRowIndex = IdNumStartRowIndex + RowIndeX - 1;
            setValueToXlsCell(xlsSheet, IdNumStartRowIndex, IdNumStopRowIndex, "A", model.SampleItem);
            setValueToXlsCell(xlsSheet, IdNumStartRowIndex, IdNumStopRowIndex, "D", model.CheckLevel);
            if (model.Grade.Contains("=")) { setValueToXlsCell(xlsSheet, IdNumStartRowIndex, IdNumStopRowIndex, "E", model.Grade + ""); }
            else { setValueToXlsCell(xlsSheet, IdNumStartRowIndex, IdNumStopRowIndex, "E", "AQL=" + model.Grade + ""); }

            setDEmethod(xlsSheet, IdNumStartRowIndex, IdNumStopRowIndex);

            if (model.CheckNumber == 0) { setValueToXlsCell(xlsSheet, IdNumStartRowIndex, IdNumStopRowIndex, "F", ""); }
            else
            {
                string mm = model.CheckNumber.ToString() + "/" + model.AcceptGradeNumber.ToString() + "/" + model.RefuseGradeNumber.ToString();
                setValueToXlsCell(xlsSheet, IdNumStartRowIndex, IdNumStopRowIndex, "F", mm);
            }
            string CheckMethod = "";
            if (model.CheckMethod != null) { CheckMethod = model.CheckMethod.ToString(); }
            setValueToXlsCell(xlsSheet, IdNumStartRowIndex, IdNumStopRowIndex, "G", CheckMethod);
            string EquipmetID = "";
            if (model.EquipmentID != null) { EquipmetID = model.EquipmentID.ToString(); }
            setValueToXlsCell(xlsSheet, IdNumStartRowIndex, IdNumStopRowIndex, "H", EquipmetID);
            setValueToXlsCell(xlsSheet, IdNumStartRowIndex, IdNumStopRowIndex, "I", "");
            ResetXlsCellStatus(xlsSheet, IdNumStartRowIndex, RowIndeX - 1, "A", "J", "V");
            xlEdgeLeftLine(xlsSheet, IdNumStartRowIndex, IdNumStopRowIndex, 13, 'J');

        }
        /// <summary>
        /// 填充Excel标题内容
        /// </summary>
        /// <param name="models"></param>
        /// <param name="xlsSheet"></param>
        private static void SetExcelTitle(List<IQCSampleItemRecordModel> models, Excel.Worksheet xlsSheet)
        {
            xlsSheet.Cells[2, 2] = DateTime.Now.ToString("yyyy-MM-dd");
            xlsSheet.Cells[2, 8] = "品号：" + models[0].SampleMaterial;
            xlsSheet.Cells[2, 17] = "NO:" + models[0].OrderID;
            xlsSheet.Cells[3, 1] = "品名：" + models[0].SampleMaterialName;
            xlsSheet.Cells[3, 5] = "规格：" + models[0].SampleMaterialSpec;
            xlsSheet.Cells[3, 11] = "数量：" + models[0].SampleMaterialNumber.ToString();
            xlsSheet.Cells[4, 1] = "供应商:" + models[0].SampleMaterialSupplier;
            xlsSheet.Cells[4, 5] = "图号/检验规范：" + models[0].SampleMaterialDrawID;
            xlsSheet.Cells[4, 11] = "检验方式：" + models[0].CheckWay;

        }

        private static void SetExcelTitleTestROHS(List<IQCSampleItemRecordModel> models, Excel.Worksheet xlsSheet)
        {
            foreach (var m in models)
            {
                if (m.SampleItem.Contains("ROHS检验"))
                {
                    xlsSheet.Cells[3, 14] = "ROHS结果：□OK □NG  □NA";
                    xlsSheet.Cells[4, 14] = "测试编号：";
                    return;
                }
                if (m.SampleItem.Contains("NOT ROHS"))
                {
                    xlsSheet.Cells[3, 14] = "不用测 ROHS";
                    xlsSheet.Cells[4, 14] = "      ";
                    return;
                }
            }
            //SpamleRecordDal RecordDal = new SpamleRecordDal();
            //Int64 Remainder = RecordDal.GetMaiterialConunt(models[0].SampleMaterial) % 2;
            Int64 Remainder = 2;
            if (Remainder == 0)
            { xlsSheet.Cells[3, 14] = "ROHS结果：□OK □NG  □NA"; xlsSheet.Cells[4, 14] = "测试编号："; }
            else { xlsSheet.Cells[3, 14] = "不用测 ROHS"; xlsSheet.Cells[4, 14] = "      "; }
        }


        /// <summary>
        /// 打印数据报表
        /// </summary>
        /// <param name="models">打印数据模块</param>
        public string PrintSampleModel(List<IQCSampleItemRecordModel> models)
        {

            if (models.Count > 0)
            {
                int i = 1;
                int j = 1;
                Excel.Workbook xlsBook = OpenTheExcel("PrintIQCDataXlsPath");
                Excel.Worksheet xlsSheet = (Excel.Worksheet)xlsBook.Worksheets[i];
                //清除填充区域内的内容
                ClearContentsXlsCell(xlsSheet, "A7");
                //设置填充EXCEL表头内容
                SetExcelTitle(models, xlsSheet);
                //设置RHOS测试表头
                SetExcelTitleTestROHS(models, xlsSheet);
                int StartIndex = 7;
                int RowIndex = 0;
                //递归循环对 每个物料抽样项目 进行数据填充
                foreach (IQCSampleItemRecordModel model in models)
                {
                    if (model.SampleItem.Contains("ROHS"))
                    { continue; }
                    Int64 Number = (Int64)model.CheckNumber;
                    RowIndex = Convert.ToInt16(Number / 13); Int64 Remainder = Number % 13;
                    if (!(Remainder == 0))
                    {
                        RowIndex = RowIndex + 1;
                    }
                    if (JudgeNoEqual(model.SizeSpecUP, model.SizeSpecDown))
                    {
                        Int64 remainder = RowIndex % 2;
                        if (!(remainder == 0))
                        {
                            RowIndex = RowIndex + 1;
                        }
                    }
                    if (model.SampleItem.Contains("外观") || model.CheckNumber == 0 || model.SampleItem.Contains("Liv") || model.SampleItem.Contains("3D"))
                    {
                        RowIndex = 2;
                    }

                    if ((StartIndex + RowIndex) > (4 + 30 * i))
                    {
                        for (int ii = 1; ii <= 30; ii++)
                        {
                            int iii = (4 + 30 * i);//从弟34行开始插入30行
                            xlsSheet.Rows[iii].Insert();
                        }
                        i++;
                        //RowIndex = RowIndex222;
                    }
                    if ((StartIndex + RowIndex) > (7 + 30 * j))
                    {
                        int mm = 7 + 30 * j;
                        int RowIndex111 = mm - StartIndex;
                        if (RowIndex111 > 0)
                        {

                            SetMaterialInfoToXlsCell(xlsSheet, StartIndex, RowIndex111, model);
                            JudgeUpDown(xlsSheet, StartIndex, RowIndex111, model.SizeSpec, model.SizeSpecUP, model.SizeSpecDown);

                        }
                        //xlsSheet = (Excel.Worksheet)xlsBook.Worksheets[i];
                        int RowIndex222 = RowIndex - RowIndex111;
                        if (RowIndex222 > 0)
                        {
                            int StartIndex22 = StartIndex + RowIndex111;
                            //SetExcelTitle(models, xlsSheet);
                            //StartIndex = 7;
                            SetMaterialInfoToXlsCell(xlsSheet, StartIndex22, RowIndex222, model);
                            JudgeUpDown(xlsSheet, StartIndex22, RowIndex222, model.SizeSpec, model.SizeSpecUP, model.SizeSpecDown);
                        };
                        j++;
                    }
                    else
                    {
                        SetMaterialInfoToXlsCell(xlsSheet, StartIndex, RowIndex, model);
                        JudgeUpDown(xlsSheet, StartIndex, RowIndex, model.SizeSpec, model.SizeSpecUP, model.SizeSpecDown);
                    }
                    StartIndex = StartIndex + RowIndex;
                }
                //PrintExcel(xlsSheet);
                return SaveAsExcel(xlsSheet, xlsBook);
            }
            else return string.Empty;
        }

        #endregion
        #endregion
      
    }

   
   /// <summary>
   ///抽验查询对像
   /// </summary>
   public class SampleQueries
    {
        #region
        /// <summary>
        /// 样品订单号
        /// </summary>
        public string OrderId
        { set; get; }
        /// <summary>
        /// 样品料号
        /// </summary>
        public string Material
        { set; get; }
        /// <summary>
        /// 样品名称
        /// </summary>
        public string MaterialName
        { set; get; }
        /// <summary>
        /// 样品规格
        /// </summary>
        public string MaterialSpec
        { set; get; }
        /// <summary>
        /// 样品的供应商
        /// </summary>
        public string MaterialSupplier
        { set; get; }
        /// <summary>
        /// 样品购入日期
        /// </summary>
        public string MaterialInDate
        { set; get; }
        /// <summary>
        /// 样品提供ERP中图号
        /// </summary>
        public string MaterialDrawID
        { set; get; }
        /// <summary>
        /// 抽样数量
        /// </summary>
        public string MaterialNumber
        { set; get; }
        #endregion
    }





    public class MaterialSampleItemManager
    {
        IMaterialSampleSetReposity irep = null;
        public MaterialSampleItemManager()
        {
            irep = new MaterialSampleSetReposity();
        }
        public List<MaterialSampleSet> GetMaterilalSampleItem(string Materi)
        {
            return irep.Entities.Where(e => e.SampleItem == Materi).ToList();
        }

    }

}
