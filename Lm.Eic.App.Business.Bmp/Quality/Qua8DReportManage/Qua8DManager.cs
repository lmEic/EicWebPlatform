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
            if (discoverPosition == "内部制造")
                return "M1708017";
            if (discoverPosition == "客户抱怨")
                return "R1708017";
            if (discoverPosition == "供应商")
                return "r1708017";
            else return "R1708087";
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
