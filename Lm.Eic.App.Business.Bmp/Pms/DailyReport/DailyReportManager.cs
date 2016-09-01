using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
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

                return listEntity;
            }
            else return new List<ProductFlowModel>();
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
                OpResult result = OpResult.SetResult("数据克隆保存失败!");
                List<ProductFlowModel> newFlowList = new List<ProductFlowModel>();
                if (sourceFlowList != null && sourceFlowList.Count > 0)
                {
                    sourceFlowList.ForEach(e =>
                    {
                        result=Store(e);
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
        public List<string> GetProductListBy(QueryDailyReportDto dto)
        {
            List<string> returnstr = new List<string>();

            var productFlowList = GetProductFlowListBy(dto);
            if (productFlowList != null && productFlowList.Count > 0)
            {
                productFlowList.ForEach(e => { returnstr.Add(e.ProductName); });
            }
            return returnstr;
          
        }
       
        
    }


 
}
