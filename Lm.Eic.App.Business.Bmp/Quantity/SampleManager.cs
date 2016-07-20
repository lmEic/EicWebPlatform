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
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.App.Business.Bmp.Quantity.SampleItermRulesManger;

using Excel;
namespace Lm.Eic.App.Business.Bmp.Quantity
{   
    /// <summary>
    /// IQC抽样项目登记表
    /// </summary>
    public class IQCSampleItemsRecordManager
    {
        IIQCSampleItemRecordReposity irep = null;
      
        public IQCSampleItemsRecordManager ()
        {
            irep = new IQCSampleItemRecordReposity();
        }
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        private bool IsExist(IQCSampleItemRecordModel model)
        {
            return irep.Entities.Where(e =>
                e.OrderID ==model .OrderID 
                &e.SampleMaterial ==model.SampleMaterial
                &e.SampleItem ==model.SampleItem 
                ).ToList ().Count >0;
        }
       
        /// <summary>
        ///保存
        /// </summary>
        /// <param name="listModels"></param>
        /// <returns></returns>
        public OpResult Store(List<IQCSampleItemRecordModel> listModels)
        {

            OpResult opResult = OpResult.SetResult("未执行任何操作！", false);
             
            try
            {
                int record = 0;
                string opContext = "IQC打印记录存储";
                if (listModels == null||listModels .Count <=0) return OpResult.SetResult("集合不能为空！", false);
                     //新增 修改
                        listModels.ForEach(model => {

                            if (IsExist(model))
                            {
                                model.PrintCount += 1;
                                record += irep.Update(u => u.Id_key == model.Id_key, model);
                            }
                            else
                            { record += irep.Insert(model); }
                        });
                        opResult = record.ToOpResult_Add(opContext);
                
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
            return opResult;
        }
        /// <summary>
       ///   由订单打印测试项目
       /// </summary>
       /// <param name="orderId">订单</param>
       /// <returns></returns>
        public  List<IQCSampleItemRecordModel> GetSamplePrintItemBy(string orderId)
        {
            return irep.Entities.Where (e=>e.OrderID ==orderId ).ToList ();
        }
        /// <summary>
        /// 得到IQC抽样项次 （单身）
        /// </summary>
        /// <param name="orderId">ERP单号</param>
        /// <param name="sampleMaterial">物料料号</param>
        /// <returns></returns>
        public  List<IQCSampleItemRecordModel> GetPringSampleItemBy(string orderId,string sampleMaterial)
        {

            List<IQCSampleItemRecordModel> models = irep.Entities.Where(e => e.OrderID == orderId & e.SampleMaterial == sampleMaterial).ToList();
            if (models ==null ||models .Count <=0)
            {
                // 记录测试方法 正常 放宽 加严 
                string  CheckWay = QuantityService.SampleItermLawManger.GetCheckWayBy(sampleMaterial, "IQC");
                IQCSampleItemRecordModel model=null;
                //单子的物料信息
                 var productInfo = GetPuroductSupplierInfo(orderId).Where (e=>e.ProductID ==sampleMaterial).FirstOrDefault();
                
                 var SampleItem = QuantityService.MaterialSampleItemManager.GetMaterilalSampleItemBy(productInfo.ProductID);
                 foreach (var f in SampleItem)
                 {
                     if (f.SampleItem.Contains("盐雾"))
                     {
                         if (!JudgeYwTest(productInfo.ProductID, productInfo.ProduceInDate))
                         {
                             if (JudgeMaterialTwoYearIsRecord(f.SampleMaterial))
                             continue;
                         }
                     }
                     if (f.SampleItem.Contains("全尺寸"))
                     {
                         if (JudgeMaterialTwoYearIsRecord(f.SampleMaterial))
                         { continue; }
                     }
                     model = new IQCSampleItemRecordModel()
                     {
                         OrderID = productInfo.OrderID,
                         SampleMaterial = productInfo.ProductID,
                         SampleMaterialDrawID = productInfo.ProductDrawID,
                         SampleMaterialName = productInfo.ProductName,
                         SampleMaterialInDate = productInfo.ProduceInDate,
                         SampleMaterialSpec = productInfo.ProductStandard,
                         SampleMaterialNumber = productInfo.ProduceNumber,
                         SampleMaterialSupplier = productInfo.ProductSupplier,
                         CheckLevel = f.CheckLevel,
                         CheckMethod = f.CheckMethod,
                         CheckWay = CheckWay,
                         EquipmentID = f.EquipmetnID,
                         Grade = f.Grade,
                         SampleItem = f.SampleItem,
                         SizeSpec = f.SizeSpec,
                         SizeSpecDown = f.SizeSpecDown,
                         SizeSpecUP = f.SizeSpecUP,
                         PrintCount = 1,
                     };
                     models.Add(model);

                 }
                        
            }
            return models;
        }
     
        /// <summary>
        /// 得到抽样物料信息 （单头）
        /// </summary>
        /// <param name="orderId">ERP单号</param>
        /// <returns></returns>
        public List<MaterialModel> GetPuroductSupplierInfo(string orderId)
        {
            return   QuantityDBManager.QuantityPurchseDb.FindMaterialBy(orderId);
        }
        /// <summary>
        /// 得到当年年始到目前为止物料抽样批次
        /// </summary>
        /// <param name="sampleMaterial">料号</param>
        /// <returns></returns>
        public int GetMaiterialConuntBy(string sampleMaterial)
        {
            string Myyear = DateTime.Now.Year.ToString() + "-01-01";
            DateTime n = Convert.ToDateTime(Myyear);
            List<IQCSampleItemRecordModel> nn = irep.Entities.Where(e => e.SampleMaterial == sampleMaterial && e.PrintCount  != 0 & e.SampleMaterialInDate >= n).ToList();
            if (nn != null)
                return nn.Count;
            else return 0;
        }


        /// <summary>
        ///  判定是否需要测试 盐雾测试
        /// </summary>
        /// <param name="sampleMaterial">物料料号</param>
        /// <param name="sampleMaterialInDate">当前物料进料日期</param>
        /// <returns></returns>
        public bool JudgeYwTest(string sampleMaterial, DateTime sampleMaterialInDate)
        {
            bool ratuenValue = true ;
            //调出此物料所有打印记录项
            var SampleItemsRecords = irep.Entities.Where(e => e.SampleMaterial == sampleMaterial).Distinct();
            //如果第一次打印 
            if (SampleItemsRecords == null | SampleItemsRecords.Count() <= 0)  return true  ;

             // 进料日期后退30天 抽测打印记录
             var SampleItemsRecord = (from t in SampleItemsRecords
                         where t.SampleMaterialInDate >= (sampleMaterialInDate.AddDays(-30))
                               & t.SampleMaterialInDate <= sampleMaterialInDate
                         select t.SampleItem).Distinct();
             //没有 测
              if (SampleItemsRecord == null | SampleItemsRecord.Count() <= 0) return true;
              // 有  每项中是否有测过  盐雾测试
                    foreach (var n in SampleItemsRecord)
                    {
                        if (n.Contains("盐雾")) { ratuenValue = false ; break; }
                    }
                return ratuenValue;
            
         
        }

        /// <summary>
        ///  判定些物料在二年内是否有录入记录 
        /// </summary>
        /// <param name="sampleMaterial">物料料号</param>
        /// <returns></returns>
        public bool JudgeMaterialTwoYearIsRecord(string sampleMaterial)
        {
            var nn = QuantityService.SampleRecordManager.GetIQCSampleRecordModelsBy(sampleMaterial).Where(e => e.InPutDate >= DateTime.Now.AddYears(-2));
            if (nn != null ||nn.Count ()>0)
                return true;
            else return false;
        }
        /// <summary>
        ///  IQC 导出Excel 数据流
        /// </summary>
        /// <param name="dataSource">数据源</param>
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
                //保存
                 Store(dataSource);
                return stream;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

       /// <summary>
       ///   模板导入到NPOI Workbook中
       /// </summary>
       /// <param name="dataSourceFilePath">数据源路经</param>
       /// <returns></returns>
        private NPOI.HSSF.UserModel.HSSFWorkbook InitializeWorkbook(string dataSourceFilePath)
        {
            try
            {
                NPOI.HSSF.UserModel.HSSFWorkbook hssfworkbook = null;
                System.IO.FileStream file = new System.IO.FileStream(dataSourceFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
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
        /// <param name="xlsSheet">Excel表</param>
        /// <param name="startRowIndex">启始行</param>
        /// <param name="rowIndex">行数</param>
        /// <param name="A">A列</param>
        /// <param name="J">J列</param>
        /// <param name="V">V列</param>
        private void ResetXlsCellStatus(Excel.Worksheet xlsSheet, int startRowIndex, int rowIndex, string A, string J, string V)
        {
            int m = startRowIndex + rowIndex + 1;

            xlsSheet.get_Range(J + startRowIndex, V + m).Borders.get_Item(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlDashDot;//加虚线
            xlsSheet.get_Range(A + m, V + m).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;    //加实线  抵部不好加  顶部加1

        }
        /// <summary>
        ///  加实线  抵部不好加  顶部加1
        /// </summary>
        /// <param name="xlsSheet">excel表</param>
        /// <param name="startRowIndex">启始行</param>
        /// <param name="rowIndex">行数</param>
        /// <param name="A">A列</param>
        /// <param name="V">V列</param>
        private void ResetXlsCellStatus(Excel.Worksheet xlsSheet, int startRowIndex, int rowIndex, string A, string V)
        {
            int m = startRowIndex + rowIndex + 1;
            xlsSheet.get_Range(A + m, V + m).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;    //加实线  抵部不好加  顶部加1

        }
        /// <summary>
        /// 合并B C 二列
        /// </summary>
        /// <param name="xlsSheet"></param>
        /// <param name="mergeRowIndex">合并二列的行数</param>
        private void MergeXlsCell(Excel.Worksheet xlsSheet, int mergeRowIndex)
        {
            xlsSheet.get_Range("B" + mergeRowIndex, "C" + mergeRowIndex).Merge();
        }

        /// <summary>
        /// BC列不合并时处理数据 ，并处理一合并时清除的一些线
        /// </summary>
        /// <param name="xlsSheet"></param>
        /// <param name="MergeRowIndex">合并的行数</param>
        /// <param name="valueUp">值上限</param>
        /// <param name="valueDown">值下限</param>
        /// <param name="standardValue">规格值</param>
        private void SetBCValueToXlsCell(Excel.Worksheet xlsSheet, int startRowIndex, string valueUp, string valueDown, string standardValue)
        {
            InsertStandardValue(xlsSheet, startRowIndex, standardValue, "B");
            InserUpDownValue(xlsSheet, startRowIndex, valueUp, valueDown, "C");
            int StopRowIndex = startRowIndex + 1;
            xlsSheet.get_Range("B" + startRowIndex, "C" + StopRowIndex).Borders.LineStyle = Excel.XlLineStyle.xlLineStyleNone;
            xlsSheet.get_Range("B" + startRowIndex, "C" + StopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("B" + startRowIndex, "C" + StopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("A" + startRowIndex, "A" + StopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("A" + startRowIndex, "A" + StopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("F" + startRowIndex, "H" + StopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("F" + startRowIndex, "H" + StopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
        }
        /// <summary>
        /// 插入上限，下限的值
        /// </summary>
        /// <param name="xlsSheet"></param>
        /// <param name="startRowIndex">起始行</param>
        /// <param name="valueUp">上限值</param>
        /// <param name="valueDown">下限值</param>
        /// <param name="column">列数</param>
        private  void InserUpDownValue(Excel.Worksheet xlsSheet, int startRowIndex, string valueUp, string valueDown, string column)
        {
            int StopRowIndex = startRowIndex + 1;

            xlsSheet.get_Range(column + startRowIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;//水平对齐靠右
            xlsSheet.get_Range(column + startRowIndex).VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;//垂直对对齐 向下
            xlsSheet.get_Range(column + startRowIndex).NumberFormatLocal = "@";//设置为文本
            xlsSheet.get_Range(column + startRowIndex).Value = valueUp;

            xlsSheet.get_Range(column + startRowIndex).Font.Size = "8"; //设置字体大小
            xlsSheet.get_Range(column + startRowIndex).Font.Name = "宋体";//设置字体


            xlsSheet.get_Range(column + StopRowIndex).NumberFormatLocal = "@";//设置为文本
            xlsSheet.get_Range(column + StopRowIndex).Value = valueDown;
            xlsSheet.get_Range(column + StopRowIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;//水平对齐靠右
            xlsSheet.get_Range(column + StopRowIndex).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;//垂直对对齐 向上
            xlsSheet.get_Range(column + StopRowIndex).Font.Size = "8"; //设置字体大小
            xlsSheet.get_Range(column + StopRowIndex).Font.Name = "宋体";//设置字体
        }
        /// <summary>
        /// B列 插入标准值
        /// </summary>
        /// <param name="xlsSheet"></param>
        /// <param name="startRowIndex">起始行</param>
        /// <param name="standardValue"></param>
        /// <param name="column">列数</param>
        private  void InsertStandardValue(Excel.Worksheet xlsSheet, int startRowIndex, string standardValue, string column)
        {
            int StopRowIndex = startRowIndex + 1;
            xlsSheet.get_Range(column + startRowIndex, "B" + StopRowIndex).Merge();//合并单元格
            xlsSheet.get_Range(column + startRowIndex, "B" + StopRowIndex).Value = standardValue;
            xlsSheet.get_Range(column + startRowIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;//水平对齐靠左
            xlsSheet.get_Range(column + startRowIndex).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;//垂直对对齐 居中
            xlsSheet.get_Range(column + startRowIndex).NumberFormatLocal = "@";//设置为文本
            xlsSheet.get_Range(column + startRowIndex).Font.Size = "12"; //设置字体大小
            xlsSheet.get_Range(column + startRowIndex).Font.Name = "宋体";//设置字体


        }
        /// <summary>
        /// BC列合并时处理数据
        /// </summary>
        /// <param name="xlsSheet"></param>
        /// <param name="startMergeRowIndex"></param>
        /// <param name="ValueUp"></param>
        /// <param name="ValueDown"></param>
        /// <param name="standardValue"></param>
        private void setBCValueToXlsCell(Excel.Worksheet xlsSheet, int startMergeRowIndex, int endMergeRowIndex, string standardValue)
        {

            xlsSheet.get_Range("B" + startMergeRowIndex, "C" + endMergeRowIndex).Merge();//合并单元格
            xlsSheet.get_Range("B" + startMergeRowIndex).Value = standardValue;
            xlsSheet.get_Range("B" + startMergeRowIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;//水平对齐靠左
            xlsSheet.get_Range("B" + startMergeRowIndex).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;//垂直对对齐 居中
            xlsSheet.get_Range("B" + startMergeRowIndex).NumberFormatLocal = "@";//设置为文本
            xlsSheet.get_Range("B" + startMergeRowIndex).Font.Size = "12"; //设置字体大小
            xlsSheet.get_Range("B" + startMergeRowIndex).Font.Name = "宋体";//设置字体
        }
        private  void setDEmethod(Excel.Worksheet xlsSheet, int startMergeRowIndex, int endMergeRowIndex)
        {
            xlsSheet.get_Range("D" + startMergeRowIndex, "E" + endMergeRowIndex).Borders.LineStyle = Excel.XlLineStyle.xlLineStyleNone;

            xlsSheet.get_Range("D" + startMergeRowIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;//水平对齐靠左
            xlsSheet.get_Range("E" + startMergeRowIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;//水平对齐靠左
            xlsSheet.get_Range("D" + startMergeRowIndex, "E" + endMergeRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("D" + startMergeRowIndex, "E" + endMergeRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("D" + startMergeRowIndex, "E" + endMergeRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("A" + startMergeRowIndex, "H" + endMergeRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
        }
        private void setCPDEmethod(Excel.Worksheet xlsSheet, int startMergeRowIndex, int endMergeRowIndex)
        {
            xlsSheet.get_Range("H" + startMergeRowIndex, "I" + endMergeRowIndex).Borders.LineStyle = Excel.XlLineStyle.xlLineStyleNone;
            xlsSheet.get_Range("H" + startMergeRowIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;//水平对齐靠左
            xlsSheet.get_Range("I" + startMergeRowIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;//水平对齐靠左
            xlsSheet.get_Range("H" + startMergeRowIndex, "I" + endMergeRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("H" + startMergeRowIndex, "I" + endMergeRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("H" + startMergeRowIndex, "I" + endMergeRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range("A" + startMergeRowIndex, "H" + endMergeRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
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
        /// <param name="startRowIndex"></param>
        /// <param name="stopRowIndex"></param>
        /// <param name="colName"></param>
        /// <param name="value"></param>
        private void setValueToXlsCell(Excel.Worksheet xlsSheet, int startRowIndex, int stopRowIndex, string colName, string value)
        {
            if (startRowIndex == stopRowIndex)
            {
                xlsSheet.get_Range(colName + startRowIndex, colName + stopRowIndex).Value = value;

            }
            else
            {
                xlsSheet.get_Range(colName + startRowIndex, colName + stopRowIndex).Merge();
                xlsSheet.get_Range(colName + startRowIndex, colName + stopRowIndex).Value = value;

            }
            xlsSheet.get_Range(colName + startRowIndex, colName + stopRowIndex).WrapText = true;
            xlsSheet.get_Range(colName + startRowIndex, colName + stopRowIndex).Borders.Weight = Excel.XlBorderWeight.xlThin;
            xlsSheet.get_Range(colName + startRowIndex, colName + stopRowIndex).HorizontalAlignment = -4108;
            xlsSheet.get_Range(colName + startRowIndex, colName + stopRowIndex).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range(colName + startRowIndex, colName + stopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range(colName + startRowIndex, colName + stopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range(colName + startRowIndex, colName + stopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsSheet.get_Range(colName + startRowIndex, colName + stopRowIndex).EntireColumn.ShrinkToFit = true;//自动缩小字体填充   
            xlsSheet.get_Range(colName + startRowIndex, colName + stopRowIndex).EntireColumn.AutoFit();//自动调整列宽 
        }
        /// <summary>
        /// 从J列开始 画13列 垂直线
        /// </summary>
        /// <param name="xlsSheet"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="stopRowIndex"></param>
        private static void xlEdgeLeftLine(Excel.Worksheet xlsSheet, int startRowIndex, int stopRowIndex, int nunmber, char startNunmber)
        {
            for (int i = 0; i < nunmber; i++)
            {
                string ColName = Convert.ToChar(startNunmber + i).ToString();
                xlsSheet.get_Range(ColName + startRowIndex, ColName + stopRowIndex).Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous;
            }
        }
        /// <summary>
        /// 打印，超出打印范围提示不打印
        /// </summary>
        /// <param name="xlsSheet"></param>
        private void PrinttheExcel(Excel.Worksheet xlsSheet)
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
        private bool JudgeNoEqual(string upValue, string downValue)
        {
            if (JudgeNoNull(upValue, downValue))
            {
                if (JudgeMach(upValue, downValue))
                {
                    double upValueNumber = Convert.ToDouble(upValue);
                    double downValueNumber = Convert.ToDouble(downValue);
                    if (!JudgeAbs(upValueNumber, downValueNumber))
                    { return true; }
                    return false;
                }
                return false;
            }
            return false;
        }
        private void JudgeUpDown(Excel.Worksheet xlsSheet, int idNumStartRowIndex, int rowIndex, string sizeSpec, string sizeSpecUP, string sizeSpecDown)
        {
            try
            {
                int IdNumStopRowIndex = idNumStartRowIndex + rowIndex - 1;
                string UpValue = sizeSpecUP;
                string DownValue = sizeSpecDown;
                if (JudgeNoNull(UpValue, DownValue) & JudgeMach(UpValue, DownValue))
                {
                    double upValue = UpValue.ToDouble ();
                    double downValue = DownValue.ToDouble ();
                    if (JudgeAbs(upValue, downValue))
                    {
                        string Value = sizeSpec + "±" + Math.Abs(upValue).ToString();
                        setBCValueToXlsCell(xlsSheet, idNumStartRowIndex, IdNumStopRowIndex, Value);
                    }
                    else
                    {
                        for (int i = 0; i < rowIndex / 2; i++)
                        {
                            int StartRowIndex = idNumStartRowIndex + i * 2;
                            SetBCValueToXlsCell(xlsSheet, StartRowIndex, sizeSpecUP, sizeSpecDown, sizeSpec);
                        }
                    }
                }
                else
                {
                    string Value = sizeSpec + UpValue + DownValue;
                    setBCValueToXlsCell(xlsSheet, idNumStartRowIndex, IdNumStopRowIndex, Value);
                }
            }
            catch (Exception ex)
            {
                OpResult.SetResult(ex.ToString(), false);  
            }

        }
        #endregion

              #region   Print IQC


          /// <summary>    
          /// 打开Excel
          /// </summary>
          /// <param name="printIQCDataXlsPath"></param>
          /// <returns></returns>
        private static Excel.Workbook OpenTheExcel(string printIQCDataXlsPath)
        {
            string fileName = printIQCDataXlsPath;
            if(printIQCDataXlsPath==string.Empty )
            { fileName = @"\\192.168.0.237\LightMasterSpc\lmSpc\System\ProductSizeSpecPicture\品保课\IQC.xls"; } 
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
        /// <param name="rowIndeX"></param>
        /// <param name="model"></param>
        private void SetMaterialInfoToXlsCell(Excel.Worksheet xlsSheet, int IdNumStartRowIndex, int rowIndeX, IQCSampleItemRecordModel model)
        {
            int IdNumStopRowIndex = IdNumStartRowIndex + rowIndeX - 1;
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
            ResetXlsCellStatus(xlsSheet, IdNumStartRowIndex, rowIndeX - 1, "A", "J", "V");
            xlEdgeLeftLine(xlsSheet, IdNumStartRowIndex, IdNumStopRowIndex, 13, 'J');

        }
        /// <summary>
        /// 填充Excel标题内容
        /// </summary>
        /// <param name="models"></param>
        /// <param name="xlsSheet"></param>
        private  void SetExcelTitle(List<IQCSampleItemRecordModel> models, Excel.Worksheet xlsSheet)
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

        private  void SetExcelTitleTestROHS(List<IQCSampleItemRecordModel> models, Excel.Worksheet xlsSheet)
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
    /// 抽样物料记录管理
    /// </summary>
    public class SampleRecordManager
    {
        IIQCSampleRecordReposity irep = null;
        public SampleRecordManager()
        { irep = new IQCSampleRecordReposity (); }

        public List<IQCSampleRecordModel> GetIQCSampleRecordModelsBy(string sampleMaterial)
        {
            return irep.Entities.Where(e => e.SampleMaterial == sampleMaterial).ToList();
        }

      
       
    }
 
    
    /// <summary>
   ///抽验查询对像
   /// </summary>
   public class SampleQueries
    {
        #region
        /// <summary>
        /// 样品单号
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
        /// 样品的供应商
        /// </summary>
        public string MaterialSupplier
        { set; get; }
        /// <summary>
        /// 样品购入日期
        /// </summary>
        public string MaterialInDate
        { set; get; }
      
       
        #endregion
    }





  

}
