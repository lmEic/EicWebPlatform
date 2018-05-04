using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Framework.ProductMaster.Business.Config;
using Lm.Eic.Framework.ProductMaster.Model;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.Qua8DReportManage
{
    public class Qua8DManager
    {
        public Qua8DMasterManager Qua8DMaster
        {
            get { return OBulider.BuildInstance<Qua8DMasterManager>(); }
        }

        public Qua8DDatailManager Qua8DDatail
        {
            get { return OBulider.BuildInstance<Qua8DDatailManager>(); }
        }
        public Qua8DFileStroeManager Qua8DFileStroe
        {
            get { return OBulider.BuildInstance<Qua8DFileStroeManager>(); }
        }
    }
    public class Qua8DMasterManager
    {
        public string AutoBuildingReportId(string discoverPosition)
        {
            string reportstr = string.Empty;
            switch (discoverPosition)
            {
                case "内部制造":
                    reportstr = "M";
                    break;
                case "客户抱怨":
                    reportstr = "N";
                    break;
                case "供应商":
                    reportstr = "P";
                    break;
                case "客诉":
                    reportstr = string.Empty;
                    break;
                default:
                    reportstr = string.Empty;
                    break;
            }
            string yearMonth = DateTime.Now.ToString("yyyyMM");
            string antherYearMonth = DateTime.Now.ToString("yyMM");
            int count8DNumber = Qua8DCrudFactory.MasterCrud.Get8DMasterCountNumber(reportstr, yearMonth) + 1;
            return reportstr + antherYearMonth + count8DNumber.ToString("000");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreQua8DMaster(Qua8DReportMasterModel model)
        {
            if(model!=null)
            {
                if (model.AccountabilityDepartment == null || model.AccountabilityDepartment == string.Empty)
                    return OpResult.SetErrorResult("负责单位不能为空");
                if (model.MaterialName == null || model.MaterialName == string.Empty)
                    return OpResult.SetErrorResult("物料品名不能为空");
            }

            return Qua8DCrudFactory.MasterCrud.Store(model, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public Qua8DReportMasterModel Show8DReportMasterInfo(string reportId)
        {
            return Qua8DCrudFactory.MasterCrud.getDReportMasterInfo(reportId);
        }
        public OpResult ChangeQua8DMasterStatus(string reportId, string status, string fileName, string filePath)
        {
            return Qua8DCrudFactory.MasterCrud.ChangeMasterStatus(reportId, status, fileName, filePath);
        }
    }



    public class Qua8DDatailManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public List<ShowStepViewModel> ShowQua8DDetailDatasBy(string discoverPosition)
        {
            ShowStepViewModel data = null;
            List<ShowStepViewModel> steps = new List<ShowStepViewModel>();
            var configDataDictionary = Get8DStepInfo(discoverPosition);
            if (configDataDictionary == null) return null;
            var handleSteps = configDataDictionary.Where(e => e.AtLevel == 4).ToList();
            if (handleSteps == null) return null;
            handleSteps.ForEach(m =>
            {
                int setpid = Convert.ToInt32(m.DisplayOrder.ToString().Substring(3, 1));
                data = new ShowStepViewModel
                {
                    StepName = m.DataNodeName,
                    StepTitle = m.DataNodeText,
                    StepId = setpid,
                    StepTitleConnect = m.Memo,
                };
                if (!steps.Contains(data))
                    steps.Add(data);
            });
            return steps;
        }
        public Qua8DReportDetailModel Get8DStepDetailDatasBy(string reportId, ShowStepViewModel step)
        {
            if (step == null || reportId == null) return new Qua8DReportDetailModel();
            var data = GetQua8DDetailDatasBy(reportId).FirstOrDefault(e => e.StepId == step.StepId);
            if (data == null)
                return new Qua8DReportDetailModel()
                {
                    StepId = step.StepId,
                    ReportId = reportId,
                    StepTitle = step.StepTitle,
                    StepDescription = step.StepTitleConnect
                };

            return data;
        }

        /// <summary>
        /// Query8DDetailDatas
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public List<Qua8DReportDetailModel> GetQua8DDetailDatasBy(string reportId)
        {
            return Qua8DCrudFactory.DetailsCrud.GetQua8DDetailDatasBy(reportId);
        }
        public List<ConfigDataDictionaryModel> Get8DStepInfo(string discoverPosition)
        {
            string reportstr = string.Empty;
            switch (discoverPosition)
            {
                case "内部制造":
                    reportstr = "InternalStepConfigDataSet";
                    break;
                case "客户抱怨":
                    reportstr = "ExternalStepConfigDataSet";
                    break;
                case "供应商":
                    reportstr = "ExternalStepConfigDataSet";
                    break;
                case "客诉":
                    reportstr = "ExternalStepConfigDataSet";
                    break;
                default:
                    reportstr = "InternalStepConfigDataSet";
                    break;
            }
            return PmConfigService.DataDicManager.LoadConfigDatasBy("Qua8DModelConfigDataSet", reportstr);
        }
        /// <summary>
        /// 存储数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreQua8DHandleDatas(Qua8DReportDetailModel model)
        {
            return Qua8DCrudFactory.DetailsCrud.Store(model);
        }
    }

    public class Qua8DFileStroeManager
    {
        public OpResult ChangeReportIdStatus(string reportId, string status, string fileName, string filePath)
        {
            return Qua8DCrudFactory.MasterCrud.ChangeMasterStatus(reportId, status, fileName, filePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchFrom"></param>
        /// <param name="searchTo"></param>
        /// <returns></returns>
        public List<Qua8DReportMasterModel> Query8DData(string searchFrom, string searchTo)
        {
            return Qua8DCrudFactory.MasterCrud.getDReportMasterDatas(searchFrom, searchTo);
        }
    }
}
