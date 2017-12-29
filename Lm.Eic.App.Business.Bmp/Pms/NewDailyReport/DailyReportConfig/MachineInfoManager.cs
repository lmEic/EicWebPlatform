using Lm.Eic.App.DomainModel.Bpm.Pms.NewDailyReport;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.NewDailyReport.DailyReportConfig
{
    public class MachineInfoManager
    {
        public List<ReportsMachineModel> GetMachineDatas(string department)
        {
            return DailyReportCrudFactory.DailyReportsMachine.GetMachineDatas(department);
        }
        /// <summary>
        /// 存储数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreOpData(ReportsMachineModel model)
        {
            return DailyReportCrudFactory.DailyReportsMachine.Store(model);
        }
    }
}
