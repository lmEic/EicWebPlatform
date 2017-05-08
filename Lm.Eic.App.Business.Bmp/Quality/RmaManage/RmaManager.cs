using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.Erp.DbAccess.CopManageDb;
using Lm.Eic.App.Erp.Domain.CopManageModel;

namespace Lm.Eic.App.Business.Bmp.Quality.RmaManage
{
    public class RmaManager
    {
        #region   Porperty  Processor   处理器
        /// <summary>
        /// 创建Ram表单处理器
        /// </summary>
        public RmaReportInitiateProcessor RmaReportBuilding
        {
            get { return OBulider.BuildInstance<RmaReportInitiateProcessor>(); }
        }
        /// <summary>
        /// 业务部门填充 物料信息 处理器
        /// </summary>
        public RmaBussesDescriptionProcessor BussesManageProcessor
        {
            get { return OBulider.BuildInstance<RmaBussesDescriptionProcessor>(); }
        }

        /// <summary>
        /// 品保部 结案 处理器
        /// </summary>
        public RmaInspecitonManageProcessor InspecitonManageProcessor
        {
            get { return OBulider.BuildInstance<RmaInspecitonManageProcessor>(); }
        }


        #endregion

        #region  Method
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public RmaReportInitiateModel GetRemPeortInitiateData()
        {
            return null;
        }

        /// <summary>
        /// 通过退料单或换货单得到相应的物料信息
        /// </summary>
        /// <param name="returnHandleOrder">退货单</param>
        /// <returns></returns>
        public List<CopReturnOrderModel> GetErpBussesInfoDatasBy(string returnHandleOrder)
        {
            try
            {
                //从ERP中得到相应的数据
                return CopOrderCrudFactory.CopReturnOrderManageDb.FindReturnOrderByID(returnHandleOrder);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }



        #endregion
    }


}
