using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.DailyReport
{

    /// <summary>
    /// 日报配置管理器
    /// </summary>
    public class ConfigManager
    {
        /// <summary>
        /// 产品工艺设置
        /// </summary>
        public ProductFlowConfig ProductFlowSetter
        {
            get { return OBulider.BuildInstance<ProductFlowConfig>(); }
        }
    }


    /// <summary>
    /// 产品工艺管理器
    /// </summary>
    public class ProductFlowConfig
    {
        #region Find

        /// <summary>
        /// 获取产品工艺总览列表 前30行
        /// </summary>
        /// <param name="departemant"> 部门</param>
        /// <returns></returns>
        public List<ProductFlowOverviewModel> GetProductFlowOverviewListBy(string departemant)
        {
            return DailyReportConfigCrudFactory.ProductFlowCrud.GetProductFlowOverviewListBy(departemant);
        }

        /// <summary>
        /// 获取产品工艺总览
        /// </summary>
        /// <param name="dto">数据传输对象 品名和部门是必须的</param>
        /// <returns></returns>
        public ProductFlowOverviewModel GetProductFlowOverviewBy(QueryDailyReportDto dto)
        {
            return DailyReportConfigCrudFactory.ProductFlowCrud.GetProductFlowOverviewBy(dto);
        }

        /// <summary>
        /// 获取工艺列表 1.根据部门  2.根据产品品名 3.根据录入日期 4.根据产品品名和工艺名称
        /// </summary>
        /// <param name="dto">数据传输对部门是必须的</param>
        /// <returns></returns>
        public List<ProductFlowModel> GetProductFlowListBy(QueryDailyReportDto dto)
        {
            return DailyReportConfigCrudFactory.ProductFlowCrud.FindBy(dto);
        }

        #endregion


        #region FileOption

        /// <summary>
        /// 获取工序Excel模板
        /// </summary>
        /// <param name="documentPath"></param>
        /// <returns></returns>
        public System.IO.MemoryStream GetProductFlowTemplate(string documentPath)
        {
            return FileOperationExtension.GetMemoryStream(documentPath);
        }

        /// <summary>
        /// 导入工序列表
        /// </summary>
        /// <param name="documentPatch">Excel文档路径</param>
        /// <returns></returns>
        public List<ProductFlowModel> ImportProductFlowListBy(string documentPatch)
        {
            StringBuilder errorStr = new StringBuilder();
            var listEntity = ExcelHelper.ExcelToEntityList<ProductFlowModel>(documentPatch, 17, out errorStr);
            string errorStoreFilePath = @"C:\ExcelToEntity\ErrorStr.txt";
            if (errorStr.ToString() != string.Empty)
            {
                errorStoreFilePath.CreateFile(errorStr.ToString());
            }
            return listEntity;
        }

        #endregion


        #region Store

        /// <summary>
        /// 仓储
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(List<ProductFlowModel> modelList)
        {
            //先依据部门和品名进行数据库清除 然后批量添加进数据库
            if (modelList.Count > 0)
            {
                DailyReportConfigCrudFactory.ProductFlowCrud.DeleteProductFlowModelBy(modelList[0].Department, modelList[0].ProductName);
                return DailyReportConfigCrudFactory.ProductFlowCrud.AddProductFlowModelList(modelList);
            }
            else
            {
                return OpResult.SetResult("列表不能为空！");
            }
        }
        #endregion

    }

}
