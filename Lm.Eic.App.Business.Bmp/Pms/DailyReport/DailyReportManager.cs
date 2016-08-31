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
            throw new NotImplementedException();
        }

        /// <summary>
        /// 仓储
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(ProductFlowModel model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 克隆一个产品
        /// </summary>
        /// <param name="sourceFlowList">源产品工序列表</param>
        /// <param name="newProductName">新产品</param>
        /// <returns></returns>
        public OpResult CloneProduct(List<ProductFlowModel> sourceFlowList,string newProductName)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        /// <summary>
        /// Excel路径转换实体
        ///
        /// </summary>
        /// <param name="putInExcelFilePath"></param>
        /// <param name="excelCo"></param>
        /// <returns></returns>
        public List<ProductFlowModel> excelToEntity(string putInExcelFilePath, int sheetColumn)
        {
            StringBuilder str = new StringBuilder();
            var listEntity = ExcelHelper.ExcelToEntityList<ProductFlowModel>(putInExcelFilePath, sheetColumn, out str);
            string FilePath = @"C:\testDir\test.txt";
            if (str.ToString() != string.Empty)
            {
                FilePath.CreateFile(str.ToString());

                return listEntity;
            }
            else return new List<ProductFlowModel>();
            
        }
        
    }


 
}
