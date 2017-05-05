using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.RmaMange
{
    /// <summary>
    /// Ram初始数据建立管理模块
    /// </summary>
    public class RmaReport
    {
        //生成RmaId编号
        public string CreateRmaID()
        {
            return RmaCurdFactory.RmaReportInitiate.CreateNewRmaID();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreRamReortInitiate(RmaReportInitiateModel model)
        {
            if (model == null) return null;
            if (RmaCurdFactory.RmaReportInitiate.IsExist(model.RmaId))
            {
                var oldmodel = RmaCurdFactory.RmaReportInitiate.GetInitiateData(model.RmaId);
                model.RmaMonth = oldmodel.RmaMonth;
                model.RmaYear = oldmodel.RmaYear;
                model.Id_Key = oldmodel.Id_Key;
                model.RmaIdStatus = oldmodel.RmaIdStatus;
                model.OpSign = OpMode.UpDate;
            }
            else
            {
                if (model.RmaId != null && model.RmaId.Length == 8)
                {
                    model.RmaYear = model.RmaId.Substring(1, 2);
                    model.RmaMonth = model.RmaId.Substring(3, 2);
                }
                model.RmaIdStatus = "未结案";
                model.OpSign = OpMode.Add;
            }
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
    /// 业务处理管理模块
    /// </summary>
    public class BussesDescription
    {

    }
    /// <summary>
    /// 品保处理管理模块
    /// </summary>
    public class InspecitonManage
    {

    }

}
