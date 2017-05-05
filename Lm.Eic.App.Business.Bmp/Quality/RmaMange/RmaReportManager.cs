using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.RmaMange
{
    /// <summary>
    /// 
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
        public OpResult StoreRamReortInitiate(ReportInitiateModel model)
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
        public ReportInitiateModel GetRemPeortInitiateData(string rmaId)
        {
            return RmaCurdFactory.RmaReportInitiate.GetInitiateData(rmaId);
        }
    }
    /// <summary>
    /// 
    /// </summary>

    public class BussesDescription
    {

    }
    public class InspecitonManage
    {

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
        public ReportInitiateModel RmaReportInitiate { set; get; }
        /// <summary>
        /// 单身信息
        /// </summary>
        public List<BussesDescriptionModel> RmaBussesDescriptionBodays { set; get; }
        /// <summary>
        /// 检验表单处理信息
        /// </summary>
        public List<InspectionManageModel> RmaInspectionManageData { set; get; }
        #endregion

        #region method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReportInitiateModel GetRemPeortInitiateData()
        {

            if (string.IsNullOrEmpty(RmaId)) return new ReportInitiateModel();
            return RmaCurdFactory.RmaReportInitiate.GetInitiateData(RmaId);
        }

        public List<BussesDescriptionModel> GetRmaBussesDescriptionDatas()
        {
            return new List<BussesDescriptionModel>();
            //if (string.IsNullOrEmpty(RmaId)) return new List<RmaBussesDescriptionModel>();
            //return RmaService.RmaManger.GetBussesDescriptiondatas(RmaId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OpResult StoreRamReortInitiate()
        {
            if (RmaReportInitiate == null) return null;
            if (RmaCurdFactory.RmaReportInitiate.IsExist(RmaReportInitiate.RmaId))

                RmaReportInitiate.OpSign = OpMode.UpDate;
            else
            {
                if (RmaReportInitiate != null)
                {
                    if (RmaReportInitiate.RmaId != null && RmaReportInitiate.RmaId.Length == 8)
                    {
                        RmaReportInitiate.RmaYear = RmaReportInitiate.RmaId.Substring(1, 2);
                        RmaReportInitiate.RmaMonth = RmaReportInitiate.RmaId.Substring(3, 2);

                    }
                    else
                    {
                        RmaReportInitiate.RmaYear = DateTime.Now.ToString("yy");
                        RmaReportInitiate.RmaMonth = DateTime.Now.ToString("MM");
                    }

                }
                RmaReportInitiate.OpSign = OpMode.Add;
            }

            return RmaCurdFactory.RmaReportInitiate.Store(RmaReportInitiate);
        }
        #endregion
    }


}
