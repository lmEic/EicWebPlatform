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
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;



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

        
        /// <summary>
        ///  IQC 导出Excel 数据流
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="xlsSheetName"></param>
        /// <returns></returns>
        public  System.IO.MemoryStream   ExportPrintToExcel(List<IQCSampleItemRecordModel> dataSource, string xlsSheetName) 
        {
            //return NPOIHelper.ExportToExcel<IQCSampleItemRecordModel>(dataSource, xlsSheetName);
            //数据为Null时返回数值

         
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            if (dataSource == null || dataSource.Count == 0) return stream;
            NPOI.HSSF.UserModel.HSSFWorkbook workbook = null ;
            string fielname =  @"C:\\lmSpc\\System\\ProductSizeSpecPicture\\品保课\\IQC.xls";
            workbook= InitializeWorkbook(fielname);
            
            if (null ==workbook )
            {
                workbook = new NPOI.HSSF.UserModel.HSSFWorkbook();
            }
            NPOI.SS.UserModel.ISheet sheet = workbook.GetSheet(xlsSheetName);
            try
            {
                #region 填充列头区域
               

                SetISheetTitle(sheet, dataSource.FirstOrDefault());

                #endregion 填充列头区域

                #region 填充内容区域

               
               
                int DataGetStartNumber =6;
                
                int rowindex = 0;
                
                //数据源第一项赋值 
                for (int Datarow = 0; Datarow < dataSource.Count; Datarow++)
                {
                    rowindex++; 
                   //得到数据源
                    IQCSampleItemRecordModel entity = dataSource[Datarow];
                     //ROHS打印测试项除掉
                    if (entity.SampleItem.Contains("ROHS"))
                    {
                        rowindex--;
                        continue;
                    }
                    //插入的行数
                    int InsertRowNumber = 0;
                    
                    //初始化行
                    NPOI.SS.UserModel.IRow rowContent = sheet.GetRow( DataGetStartNumber);

                    Int64 Number =Convert.ToInt64( entity.CheckNumber);
                    InsertRowNumber = Convert.ToInt16(Number / 13); Int64 Remainder = Number % 13;
                    if ((Remainder != 0) | (Number==0))
                    {
                        InsertRowNumber = InsertRowNumber+1;
                    }
                    if (JudgeNoEqual(entity.SizeSpecUP, entity.SizeSpecDown))
                    {
                        Int64 remainder = InsertRowNumber % 2;
                        if (!(remainder == 0))
                        {
                            InsertRowNumber = InsertRowNumber + 1;
                        }
                    }
                    if (entity.SampleItem.Contains("外观") || entity.CheckNumber == 0 || entity.SampleItem.Contains("Liv") || entity.SampleItem.Contains("3D"))
                    {
                        InsertRowNumber = 2;
                    }
                    if (DataGetStartNumber < sheet.LastRowNum)
                    {
                        MyInsertRow(sheet, DataGetStartNumber, InsertRowNumber, rowContent);
                    }
                 
                    //确定行数
                    //插入行数
          
                    Type tentity = entity.GetType();
                    System.Reflection.PropertyInfo[] tpis = tentity.GetProperties();
                    List<object> values = new List<object>();
                    // 检验项目
                    object value1 = tpis[8].GetValue(entity, null);
                    values.Add(value1);

                    //规格值
                    object value2 = tpis[14].GetValue(entity, null);
                    object value3 = tpis[15].GetValue(entity, null);
                    object value4 = tpis[16].GetValue(entity, null);

                    values.Add(value2);
                    List<string> mm= JudgeUpDown(value3.ToString(), value4.ToString());
                    if (mm.Count ==2)
                    {
                        values.Add(value3.ToString());
                    }
                    else 
                    {
                        values.Add(mm.FirstOrDefault()); 
                    }

                    //检验水平
                    object value5 = tpis[11].GetValue(entity, null);
                    object value6 = tpis[12].GetValue(entity, null);
                    values.Add(value5);
                    values.Add(value6);

                    //抽样方式
                    object value7 = tpis[18].GetValue(entity, null);
                    object value8 = tpis[17].GetValue(entity, null);
                    object value9 = tpis[19].GetValue(entity, null);
                    values.Add(value7 + "/" + value8 + "/" + value9);
                    // 检验方法
                    object value10 = tpis[10].GetValue(entity, null);
                    values.Add(value10);

                    //量具编号 
                    object value11 = tpis[9].GetValue(entity, null);
                    values.Add(value11);
                    for (int i = 0; i < values.LongCount();i++ )
                    {

                         NPOI.SS.UserModel.ICell cellContent = rowContent.GetCell(i);
                            if (cellContent == null)
                            {
                                cellContent = rowContent.CreateCell(i);
                            }
                            
                            Type type = values[i].GetType();
                            OperationSetCellvalue(cellContent, values[i], type);
                            setCellStyle(workbook, cellContent);
                            setCellStyleLine(workbook, cellContent);
                           
                            if (i == 2)
                            {
                                if (values[2].ToString().Trim() == string.Empty)
                                {
                           
                                    sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(DataGetStartNumber, DataGetStartNumber + InsertRowNumber - 1, i-(InsertRowNumber-1), i ));
                                }
                                else
                                {
                                    NPOI.SS.UserModel.IRow rowContent11 = sheet.GetRow(DataGetStartNumber + InsertRowNumber - 1);
                                    NPOI.SS.UserModel.ICell cellContent11 = rowContent11.GetCell(i);
                                    if (cellContent11 == null)
                                    {
                                        cellContent11 = rowContent11.CreateCell(i);
                                    }
                                   
                                    OperationSetCellvalue(cellContent11, mm.LastOrDefault(), type);
                                    setCellStyle(workbook, cellContent11);
                                    setCellStyleLine(workbook, cellContent11);
                                   
                                }
                               
                            }
                            else
                            { sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(DataGetStartNumber, DataGetStartNumber + InsertRowNumber - 1, i, i)); }
                        
                    }
                    DataGetStartNumber = DataGetStartNumber + InsertRowNumber;

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

     



         private void setCellStyle(HSSFWorkbook workbook, ICell cell)
        {
            HSSFCellStyle fCellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFFont ffont = (HSSFFont)workbook.CreateFont();
            ffont.FontHeight = 10 * 10;
            ffont.FontName = "宋体";
            ffont.Color = HSSFColor.Black.Index;
            fCellStyle.SetFont(ffont);

            fCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Bottom;//垂直对齐
            fCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;//水平对齐
            cell.CellStyle = fCellStyle;
        }

        private void setCellStyleLine(HSSFWorkbook workbook, ICell cell)
         {
             ICellStyle style = workbook.CreateCellStyle();
             style.BorderBottom = BorderStyle.Thin;
             style.BorderLeft = BorderStyle.Thin;
             style.BorderRight = BorderStyle.Thin;
             style.BorderTop = BorderStyle.Thin;
             style.BottomBorderColor = HSSFColor.Black.Index;
             style.LeftBorderColor = HSSFColor.Black.Index;
             style.RightBorderColor = HSSFColor.Black.Index;
             style.TopBorderColor = HSSFColor.Black.Index;

             cell.CellStyle = style;
         }



        
        private void MyInsertRow(NPOI.SS.UserModel.ISheet sheet, int InsertStartRowNumber, int InsertRowNumberSum, NPOI.SS.UserModel.IRow InsertStartRow)
        {
            #region 批量移动行
            sheet.ShiftRows
             (
                      InsertStartRowNumber,                                 //--开始行
                      sheet.LastRowNum,                            //--结束行
                      InsertRowNumberSum,                             //--移动大小(行数)--往下移动
                      true,                                   //是否复制行高
                      false                                  //是否移动批注
              );
            #endregion

            #region 对批量移动后空出的空行插，创建相应的行，并以InsertRowNumber的上一行为格式源(即：InsertRowNumber-1的那一行)
            for (int i = InsertStartRowNumber; i < InsertStartRowNumber + InsertRowNumberSum - 1; i++)
            {
                NPOI.SS.UserModel.IRow targetRow = null;
                NPOI.SS.UserModel.ICell sourceCell = null;
                NPOI.SS.UserModel.ICell targetCell = null;

                targetRow = sheet.CreateRow(i + 1);

                for (int m = InsertStartRow.FirstCellNum; m < InsertStartRow.LastCellNum; m++)
                {
                    sourceCell = InsertStartRow.GetCell(m);
                    if (sourceCell == null)
                        continue;
                    targetCell = targetRow.CreateCell(m);

                    //targetCell.Encoding = sourceCell.Encoding;
                    targetCell.CellStyle = sourceCell.CellStyle;
                    targetCell.SetCellType(sourceCell.CellType);
                }
                //CopyRow(sourceRow, targetRow);

                //Util.CopyRow(sheet, sourceRow, targetRow);
            }

            NPOI.SS.UserModel.IRow firstTargetRow = sheet.GetRow(InsertStartRowNumber);
            NPOI.SS.UserModel.ICell firstSourceCell = null;
            NPOI.SS.UserModel.ICell firstTargetCell = null;

            for (int m = InsertStartRow.FirstCellNum; m < InsertStartRow.LastCellNum; m++)
            {
                firstSourceCell = InsertStartRow.GetCell(m);
                if (firstSourceCell == null)
                    continue;
                firstTargetCell = firstTargetRow.CreateCell(m);

                //firstTargetCell.Encoding = firstSourceCell.Encoding;
                firstTargetCell.CellStyle = firstSourceCell.CellStyle;
                firstTargetCell.SetCellType(firstSourceCell.CellType);
            }
            #endregion
        }


     
        /// <summary>
        /// 初始化工作簿
        /// </summary>
        private NPOI.HSSF.UserModel.HSSFWorkbook InitializeWorkbook(string FileName)
        {
            System.IO.FileStream file = new System.IO.FileStream(FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            if (null == file)
            { return null ; }
            NPOI.HSSF.UserModel.HSSFWorkbook hssfworkbook = new NPOI.HSSF.UserModel.HSSFWorkbook(file);
            if (null == hssfworkbook)
            { return hssfworkbook; }
            //create a entry of DocumentSummaryInformation
            NPOI.HPSF.DocumentSummaryInformation dsi = NPOI.HPSF.PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "IQCPrint";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            NPOI.HPSF.SummaryInformation si = NPOI.HPSF.PropertySetFactory.CreateSummaryInformation();
            si.Subject = "IQCPrint";
            hssfworkbook.SummaryInformation = si;
            return hssfworkbook;
        }


        private static Dictionary<int, NPOI.SS.UserModel.IRow> thisMyRow = new Dictionary<int, NPOI.SS.UserModel.IRow>();
        private static Dictionary<int, NPOI.SS.UserModel.ICell> thisMyCell = new Dictionary<int, NPOI.SS.UserModel.ICell>();
        private  NPOI.SS.UserModel.ICell ReturnIcell(NPOI.SS.UserModel.ISheet sheet,  NPOI.SS.UserModel.IRow row, int StartColumnIndex)
        {
            //  单元格的列
            NPOI.SS.UserModel.ICell cellContent = null ;

            if (thisMyCell.Keys.Contains(StartColumnIndex))
            {
                cellContent = thisMyCell[StartColumnIndex];
            }
            else
            {
                cellContent = row.CreateCell(StartColumnIndex); ;
                thisMyCell.Add(StartColumnIndex, cellContent);
            }


            return cellContent;
        }


       
        #region    ExportToExcelPrintOP
        /// <summary>
          /// 处理每个类型的格式
          /// </summary>
          /// <param name="cellSytleDate"></param>
          /// <param name="cellContent"></param>
          /// <param name="value"></param>
          /// <param name="type"></param>
        private  void OperationSetCellvalue( NPOI.SS.UserModel.ICell cellContent, object value, Type type)
        {
            switch (type.ToString())
            {

                case "System.String"://字符串类型
                    cellContent.SetCellValue(value.ToString());
                    break;

                case "System.DateTime"://日期类型
                    DateTime dateV;
                    DateTime.TryParse(((DateTime)value).ToString("yyyy-MM-dd"), out dateV);
                    cellContent.SetCellValue(dateV);
                    break;

                case "System.Boolean"://布尔型
                    bool boolV = false;
                    if (bool.TryParse(value.ToString(), out boolV))
                        cellContent.SetCellValue(boolV);
                    else
                        cellContent.SetCellValue("解析布尔型数据错误");
                    break;

                case "System.Int16"://整型
                    Int16 Int16V = 0;
                    if (Int16.TryParse(value.ToString(), out Int16V))
                        cellContent.SetCellValue(Int16V);
                    else
                        cellContent.SetCellValue("解析16位数据型错误");
                    break;
                case "System.Int32":
                    Int32 Int32V = 0;
                    if (Int32.TryParse(value.ToString(), out Int32V))
                        cellContent.SetCellValue(Int32V);
                    else
                        cellContent.SetCellValue("解析32位数据型错误");
                    break;
                case "System.Int64":
                    Int64 Int64V = 0;
                    if (Int64.TryParse(value.ToString(), out Int64V))
                        cellContent.SetCellValue(Int64V);
                    else
                        cellContent.SetCellValue("解析64位数据型错误");
                    break;
                case "System.Byte":
                    int intV = 0;
                    if (int.TryParse(value.ToString(), out intV))
                        cellContent.SetCellValue(intV);
                    else
                        cellContent.SetCellValue("解析整型数据错误");
                    break;

                case "System.Decimal"://浮点型
                    double DecimalV = 0;
                    if (double.TryParse(value.ToString(), out DecimalV))
                    {
                        Math.Round(DecimalV, 2);
                        cellContent.SetCellValue(DecimalV);
                    }
                    else
                        cellContent.SetCellValue("解析浮点型数据错误");
                    break;
                case "System.Double":
                    double doubV = 0;
                    if (double.TryParse(value.ToString(), out doubV))
                    {
                        Math.Round(doubV, 2);
                        cellContent.SetCellValue(doubV);
                    }
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


        private NPOI.SS.UserModel.ICellStyle OpDateSytle(NPOI.HSSF.UserModel.HSSFWorkbook workbook)
        {  
            NPOI.SS.UserModel.ICellStyle cellSytleDate = workbook.CreateCellStyle();
            NPOI.SS.UserModel.IDataFormat format = workbook.CreateDataFormat();
            cellSytleDate.DataFormat = format.GetFormat("yyyy年mm月dd日");
            return cellSytleDate;
        }




        #region   上下值判断条件
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


        private List<string>  JudgeUpDown ( string SizeSpecUP, string SizeSpecDown)
        {
            try
            {
                string UpValue = SizeSpecUP;
                string DownValue = SizeSpecDown;
                List<string> returnValue = new List<string>();
                if (JudgeNoNull(UpValue, DownValue) & JudgeMach(UpValue, DownValue))
                {
                    double upValue = Convert.ToDouble(UpValue);
                    double downValue = Convert.ToDouble(DownValue);
                    if (JudgeAbs(upValue, downValue))
                    {
                        string Value =  "±" + Math.Abs(upValue).ToString();
                        returnValue.Add(Value);
                    }
                    else
                    {
                        returnValue.Add(SizeSpecUP);
                        returnValue.Add(SizeSpecDown);
                    }
                }
                else
                {
                    string Value =  UpValue + DownValue;
                    returnValue.Add(Value);  
                }
                return returnValue;
            }
                 
            catch (Exception ex)
            {
                
                OpResult.SetResult(ex.ToString(), true);
                return null;
            }

        }
         /// <summary>
         /// 合并行 填充值
         /// </summary>
         /// <param name="xlsSheet"></param>
         /// <param name="firstRow"></param>
         /// <param name="lastRow"></param>
         /// <param name="StartMergeRowIndex"></param>
         /// <param name="EndMergeRowIndex"></param>
         /// <param name="StandardValue"></param>
        private void setMergeValueToXlsCell(NPOI.SS.UserModel.ISheet xlsSheet, int firstRow, int lastRow, int StartMergeRowIndex, int EndMergeRowIndex, string StandardValue)
        {
              //合并行，列
             mergeCell(xlsSheet, firstRow, lastRow, StartMergeRowIndex, EndMergeRowIndex);
             //填充值
             SetcellVaule(xlsSheet, firstRow, StartMergeRowIndex, StandardValue);
        }

        private void setBCValueToXlsCell(NPOI.SS.UserModel.ISheet xlsSheet, int StartRowIndex, string ValueUp, string ValueDown, string StandardValue)
        {
            //"B"
            InsertStandardValue(xlsSheet, StandardValue, StartRowIndex, 2, 2);
            //"C"
            InserUpDownValue(xlsSheet, ValueUp, ValueDown, StartRowIndex, 3);

            int StopRowIndex = StartRowIndex + 1;
          
        }

        private void InsertStandardValue(NPOI.SS.UserModel.ISheet xlsSheet, string StandardValue, int StartRowIndex, int StartColumnIndex, int StopColumnIndex)
        {
            int StopRowIndex = StartRowIndex + 1;


            setMergeValueToXlsCell(xlsSheet, StartRowIndex, StopRowIndex, StartColumnIndex, StopColumnIndex, StandardValue);

        }

        private void InserUpDownValue(NPOI.SS.UserModel.ISheet xlsSheet, string ValueUp, string ValueDown, int StartRowIndex, int Column)
        {
            int StopRowIndex = StartRowIndex + 1;
            SetcellVaule(xlsSheet, StartRowIndex, Column, ValueUp);
            SetcellVaule(xlsSheet, StopRowIndex, Column, ValueDown);
       
        }


        #endregion
        /// <summary>
        /// 填充 IQC打印条
        /// </summary>
        /// <param name="ISheet"></param>
        /// <param name="models"></param>
        private void SetISheetTitle(NPOI.SS.UserModel.ISheet ISheet, IQCSampleItemRecordModel models)
        {

            //ISheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0,13));
         

            SetcellVaule(ISheet, 1, 0, "日期：");
            SetcellVaule(ISheet, 1, 1, DateTime.Now.Date.ToString("yyyy-MM-dd"));
            SetcellVaule(ISheet, 1, 7, "品号：" + models.SampleMaterial);
         
            SetcellVaule(ISheet, 1, 16, "NO:" + models.OrderID);

            SetcellVaule(ISheet, 2, 0, "品名：" + models.SampleMaterialName);
            SetcellVaule(ISheet, 2, 4, "规格：" + models.SampleMaterialSpec);
            SetcellVaule(ISheet, 2, 10, "数量：" + models.SampleMaterialNumber.ToString());
            //cellVaule(ISheet, 2, 13, "ROHS结果：□OK □NG  □NA");
            SetcellVaule(ISheet, 2, 13, "不用测ROSA");
            SetcellVaule(ISheet, 2, 14, "□ OK   □NG  □NA");

            SetcellVaule(ISheet, 3, 0, "供应商:" + models.SampleMaterialSupplier);
            SetcellVaule(ISheet, 3, 4, "图号/检验规范：" + models.SampleMaterialDrawID);
            SetcellVaule(ISheet, 3, 10, "检验方式：" + models.CheckWay);
        }
        /// <summary>
        ///  给单元格赋值
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="row">row</param>
        /// <param name="column">cell</param>
        /// <param name="value"></param>
        public void SetcellVaule(NPOI.SS.UserModel.ISheet sheet, int row, int column, string value)
        {
               NPOI.SS.UserModel.IRow Row = null;
                 if ( thisMyRow .Keys .Contains (row) )
                {
                    Row = thisMyRow[row];
                }
                 else 
                 {
                     Row = sheet.GetRow(row);
                     thisMyRow.Add(row, Row);
                 }
            
            Row.CreateCell(column, NPOI.SS.UserModel.CellType.String).SetCellValue(value);//创建第row行第cell列string类型表格，并赋值  value
        }

        private NPOI.SS.UserModel.IRow ReturnRow(NPOI.SS.UserModel.ISheet sheet, int row)
        {
            NPOI.SS.UserModel.IRow Row = null;
            if (thisMyRow.Keys.Contains(row))
            {
                Row = thisMyRow[row];
            }
            else
            {
                Row = sheet.CreateRow(row);
                thisMyRow.Add(row, Row);
            }
            return Row;
        }

        /// <summary>
        /// 设置单元格为下拉框并限制输入值
        /// <param name="sheet"></param>
        /// <param name="SelectVaules">下拉框数值</param>
        /// </summary>
        private void setCellDropdownlist(NPOI.SS.UserModel.ISheet sheet, string[] SelectVaules)
        {
            //设置生成下拉框的行和列
            var cellRegions = new NPOI.SS.Util.CellRangeAddressList(0, 65535, 0, 0);
            //设置 下拉框内容
            NPOI.HSSF.UserModel.DVConstraint constraint = NPOI.HSSF.UserModel.DVConstraint.CreateExplicitListConstraint(
                SelectVaules);

            //绑定下拉框和作用区域，并设置错误提示信息
            NPOI.HSSF.UserModel.HSSFDataValidation dataValidate = new NPOI.HSSF.UserModel.HSSFDataValidation(cellRegions, constraint);
            dataValidate.CreateErrorBox("输入不合法", "请输入下拉列表中的值。");
            dataValidate.ShowPromptBox = true;

            sheet.AddValidationData(dataValidate);
        }


        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">表格接口</param>
        /// <param name="firstRow">启始列</param>
        /// <param name="lastRow">结束列</param>
        /// <param name="firstCell">启始行</param>
        /// <param name="lastCell">结束行</param>
        private void mergeCell(NPOI.SS.UserModel.ISheet sheet, int firstRow, int lastRow, int firstCell, int lastCell)
        {
           
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(firstRow, lastRow, firstCell, lastCell));//2.0使用 2.0以下为Region
        }

        private void SetMergeCell(NPOI.SS.UserModel.ISheet sheet, int StartRow, int MergeRowNumber, int StartCell, int MergeCellNumber)
        {
            int lastRow = StartRow + MergeRowNumber;
            int lastCell =StartCell+ MergeCellNumber;
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(StartRow, lastRow, StartCell, lastCell));//2.0使用 2.0以下为Region
        }
        /// <summary>
        /// 设置单元格样式
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="cell">单元格</param>
        private void setCellStyle(NPOI.HSSF.UserModel.HSSFWorkbook workbook, NPOI.SS.UserModel.ICell cell, double FontHeight)
        {
            NPOI.HSSF.UserModel.HSSFCellStyle fCellStyle = (NPOI.HSSF.UserModel.HSSFCellStyle)workbook.CreateCellStyle();
            NPOI.HSSF.UserModel.HSSFFont ffont = (NPOI.HSSF.UserModel.HSSFFont)workbook.CreateFont();
            ffont.FontHeight = FontHeight;
            ffont.FontName = "宋体";
            ffont.Color = NPOI.HSSF.Util.HSSFColor.Yellow.Index;
            fCellStyle.SetFont(ffont);
            fCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直对齐 居中
            fCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;//水平对齐 靠左
            cell.CellStyle = fCellStyle;
        }

        #endregion

    }
  
    public class MaterialSampleItemManager
    {
        IMaterialSampleSetReposity irep = null;
        public MaterialSampleItemManager ()
        {
            irep = new MaterialSampleSetReposity();
        }
        public List<MaterialSampleSet> GetMaterilalSampleItem(string SampleMaterialProductID)
        {
            return irep.Entities.Where(e => e.SampleMaterial == SampleMaterialProductID).ToList();
        }
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
}
