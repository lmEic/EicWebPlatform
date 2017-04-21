using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.RmaMange
{
    public class RmaReportManager
    {
        //生成RmaId编号
        public string GetNewRmaID()
        {
            return RmaCurdFactory.RmaReportInitiate.GetNewRmaID();
        }
        /// <summary>
        /// 存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreRamReortInitiate(RmaReportInitiateModel model)
        {
            return RmaCurdFactory.RmaReportInitiate.Store(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmaId"></param>
        /// <returns></returns>
        public RmaReportInitiateModel GetRemPeortInitiateData(string rmaId)
        {
            return RmaCurdFactory.RmaReportInitiate.GetInitiateData(rmaId);
        }

        
    }
    /// <summary>
    /// 操作界面数据 只是做为一个选项
    /// </summary>
    public class RmaBussesDescriPtionVm
    {

        #region Porperty
        public string RmaId { set; get; }
        /// <summary>
        /// 单头信息
        /// </summary>
        public RmaReportInitiateModel RmaReportInitiate { set; get; }
        /// <summary>
        /// 单身信息
        /// </summary>
        public List<RmaBussesDescriptionModel> RmaBussesDescriptionBodays { set; get; }
        /// <summary>
        /// 检验表单处理信息
        /// </summary>
        public List<RmaInspectionManageModel> RmaInspectionManageData { set; get; }
        #endregion

        #region method




        #endregion
    }


}
