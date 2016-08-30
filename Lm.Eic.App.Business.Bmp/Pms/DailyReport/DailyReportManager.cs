using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// 添加工序
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult AddProductFlow(ProductFlowModel model)
        {
            throw new NotImplementedException();
        } 

        /// <summary>
        /// 添加工序
        /// </summary>
        /// <param name="modleList"></param>
        /// <returns></returns>
        public OpResult AddProductFlow(List<ProductFlowModel> modleList)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除工序
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult DeleteProductFlow(ProductFlowModel model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 导入工序
        /// </summary>
        /// <param name="documentPatch">文档路径</param>
        /// <returns></returns>
        public List<ProductFlowModel> ExportProductFlow(string documentPatch)
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
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<ProductFlowModel> FindBy(QueryDailyReportDto dto)
        {
            throw new NotImplementedException();
        }
    }


 
}
