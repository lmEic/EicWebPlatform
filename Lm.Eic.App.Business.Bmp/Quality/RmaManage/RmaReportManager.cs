using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.RmaManage
{
    /// <summary>
    /// Ram初始数据处理器
    /// </summary>
    public class RmaReportInitiateProcessor
    {
        //生成RmaId编号
        public string BuildingRmaID()
        {
            return RmaCurdFactory.RmaReportInitiate.BuildingNewRmaID();
        }
        /// <summary>
        /// 存储初始Rma表单
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
        /// 得到初始Rma表单
        /// </summary>
        /// <param name="rmaId"></param>
        /// <returns></returns>
        public RmaReportInitiateModel GetRemPeortInitiateData(string rmaId)
        {
            return RmaCurdFactory.RmaReportInitiate.GetInitiateData(rmaId);
        }
    }
    /// <summary>
    /// Rma单业务部门操作处理器
    /// </summary>
    public class RmaBussesDescriptionProcessor
    {

    }
    /// <summary>
    /// Rma单 品保部操作处理器
    /// </summary>
    public class RmaInspecitonManageProcessor
    {

    }

}
