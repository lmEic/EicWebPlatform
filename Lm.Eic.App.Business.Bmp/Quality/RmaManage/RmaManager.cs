using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.RmaManage
{
    public class RmaManager
    {
        #region  Private Porperty  Processor   处理器
        /// <summary>
        /// 创建Ram表单处理器
        /// </summary>
        private RmaReportInitiateProcessor RmaReportProcessor
        {
            get { return OBulider.BuildInstance<RmaReportInitiateProcessor>(); }
        }
        /// <summary>
        /// 业务部门填充 物料信息 处理器
        /// </summary>
        private RmaBussesDescriptionProcessor BussesManageProcessor
        {
            get { return OBulider.BuildInstance<RmaBussesDescriptionProcessor>(); }
        }

        /// <summary>
        /// 品保部 结案 处理器
        /// </summary>
        private RmaInspecitonManageProcessor InspecitonManageProcessor
        {
            get { return OBulider.BuildInstance<RmaInspecitonManageProcessor>(); }
        }


        #endregion




        #region Porperty
        /// <summary>
        /// 处理表单
        /// </summary>
        public string RmaId
        { set; get; }
        /// <summary>
        /// Rma初始信息
        /// </summary>
        public RmaReportInitiateModel RmaReportInitiate { set; get; }
        /// <summary>
        /// 业务操作信息
        /// </summary>
        public List<RmaBussesDescriptionModel> RmaBussesDescriptionDatas { set; get; }
        /// <summary>
        /// 检验表单处理信息
        /// </summary>
        public List<RmaInspectionManageModel> RmaInspectionManageDatas { set; get; }
        #endregion



        #region method
        /// <summary>
        ///自动生成Rma表单单号
        /// </summary>
        /// <returns></returns>

        public string AutoBuildingRmdId()
        {

            return RmaReportProcessor.BuildingRmaID();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public RmaReportInitiateModel GetRemPeortInitiateData()
        {
            return null;
        }


        public List<RmaBussesDescriptionModel> GetRmaBussesDescriptionDatas()
        {
            return null;
        }
        /// <summary>
        /// 存储初始化数据
        /// </summary>
        /// <returns></returns>
        public OpResult StoreRamReortInitiate(RmaReportInitiateModel model)
        {
            return RmaReportProcessor.StoreRamReortInitiate(model);
        }

        #endregion
    }


}
