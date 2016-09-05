using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.IO ;
using System.Collections.Generic;
using System.Text;


namespace Lm.Eic.App.Business.Bmp.Pms.DailyReport
{
    /// <summary>
    /// 日报管理器
    /// </summary>
    public class DailyReportManager
    {

    }

    /// <summary>
    /// 日报模板管理器
    /// </summary>
    public class DailyReportTemplateManager
    {

    }

    /// <summary>
    /// 工序管理器
    /// </summary>
    public class ProductFlowManager
    {
        /// <summary>
        /// 导入工序列表
        /// </summary>
        /// <param name="documentPatch">Excel文档路径</param>
        /// <returns></returns>
        public List<ProductFlowModel> ImportProductFlowListBy(string documentPatch)
        {
            StringBuilder errorStr = new StringBuilder();
            var listEntity = ExcelHelper.ExcelToEntityList<ProductFlowModel>(documentPatch, 15, out errorStr);
            string errorStoreFilePath = @"C:\ExcelToEntity\ErrorStr.txt";
            if (errorStr.ToString() != string.Empty)
            {
                errorStoreFilePath.CreateFile(errorStr.ToString());
            }
            return listEntity;
        }

        /// <summary>
        /// 获取工序Excel模板
        /// </summary>
        /// <param name="modelfilePath"></param>
        /// <returns></returns>
        public System.IO.MemoryStream GetProductFlowExcelModel(string modelfilePath)
        {
            try
            {
                //数据为Null时返回数值
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                NPOI.HSSF.UserModel.HSSFWorkbook workbook = InitializeWorkbook(modelfilePath);


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
        /// 模板导入到NPOI Workbook中
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

        /// <summary>
        /// 仓储
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(ProductFlowModel model)
        {
            return BorardCrudFactory.ProductFlowCrud.Store(model);
        }

        /// <summary>
        /// 克隆一个产品
        /// </summary>
        /// <param name="sourceFlowList">源产品工序列表</param>
        /// <param name="newProductName">新产品</param>
        /// <returns></returns>
        public OpResult CloneProductStore(List<ProductFlowModel> sourceFlowList)
        {
            try
            {
                OpResult result = OpResult.SetResult("数据保存失败!");
                List<ProductFlowModel> newFlowList = new List<ProductFlowModel>();
                if (sourceFlowList != null && sourceFlowList.Count > 0)
                {
                    sourceFlowList.ForEach(e =>
                    {
                        result = Store(e);
                    });
                }
                return result;
            }
            catch (Exception ex)
            { throw new Exception(ex.InnerException.Message); }
        }

        /// <summary>
        /// 获取工序列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<ProductFlowModel> GetProductFlowListBy(QueryDailyReportDto dto)
        {
            return BorardCrudFactory.ProductFlowCrud.FindBy(dto);
        }

        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<ProductFlowOverviewModel> GetProductList()
        {
            return null;

        }
    }
}
