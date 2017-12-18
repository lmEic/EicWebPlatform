using Lm.Eic.App.DomainModel.Bpm.Pms.NewDailyReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.NewDailyReport.DailyReportConfig
{
    public class MachineInfoManager
    {
        public List<ReportsMachineModel> getDatas(string department)
        {
            return DailyReportCrudFactory.DailyReportsMachine.GetMachineDatas(department);
        }
    }
}
