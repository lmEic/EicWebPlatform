using Lm.Eic.App.DomainModel.Bpm.Quanity;
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
            return Qua8DCrudFactory.MasterCrud.Store(model);
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
    }



    public class Qua8DDatailManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public List<ShowStepViewModel> ShowQua8DDetailDatasBy(string reportId)
        {
            List<ShowStepViewModel> steps = new List<ShowStepViewModel>();
            ShowStepViewModel data = null;
            var HanldeStepInfodatas = Qua8DCrudFactory.DetailsCrud.GetQua8DDetailDatasBy(reportId);
            HanldeStepInfodatas.ForEach(m =>
            {
                data = new ShowStepViewModel
                {
                    HandelQua8DStepDatas = m
                };
                if (!steps.Contains(data))
                    steps.Add(data);
            });
            return steps;
        }
        public Qua8DReportDetailModel GetQua8DDetailDatasBy(string reportId, int stepId)
        {
            return Qua8DCrudFactory.DetailsCrud.GetQua8DDetailDatasBy(reportId).FirstOrDefault(e => e.StepId == stepId);
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
}
