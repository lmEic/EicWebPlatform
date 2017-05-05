using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.RmaMange
{
    public class RmaManager
    {
        #region  ModelManagerPorperty
        /// <summary>
        /// 创建Ram表单管理
        /// </summary>
        public RmaReport RmaReport
        {
            get { return OBulider.BuildInstance<RmaReport>(); }
        }
        /// <summary>
        /// 业务处理表单
        /// </summary>
        public BussesDescription BussesManage
        {
            get { return OBulider.BuildInstance<BussesDescription>(); }
        }

        /// <summary>
        /// 品保结案处理
        /// </summary>
        public InspecitonManage InspecitonManage
        {
            get { return OBulider.BuildInstance<InspecitonManage>(); }
        }


        #endregion


        #region Porperty
        string _ramId;
        public string RmaId
        {
            private set { _ramId = value; }
            get
            {
                if (_ramId == null || _ramId == string.Empty)
                    return RmaReport.CreateRmaID();
                return _ramId;
            }
        }
        /// <summary>
        /// Rma初始信息
        /// </summary>
        public RmaReportInitiateModel RmaReportInitiate { set; get; }
        /// <summary>
        /// 业务操作信息
        /// </summary>
        public List<RmaBussesDescriptionModel> RmaBussesDescriptionDs { set; get; }
        /// <summary>
        /// 检验表单处理信息
        /// </summary>
        public List<RmaInspectionManageModel> RmaInspectionManageData { set; get; }
        #endregion

        #region method
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
        /// 
        /// </summary>
        /// <returns></returns>
        public OpResult StoreRamReortInitiate()
        {
            return null;
        }

        #endregion
    }
}
