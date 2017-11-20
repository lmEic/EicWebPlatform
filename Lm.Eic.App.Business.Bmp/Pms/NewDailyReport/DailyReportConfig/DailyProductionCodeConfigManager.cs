using Lm.Eic.App.DomainModel.Bpm.Pms.NewDailyReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.NewDailyReport.DailyReportConfig
{
    public class DailyProductionCodeConfigManager
    {
        public List<ProductionCodeConfigModel> GetProductionDictiotry(string aboutCategory, string department)
        {
            return DailyReportCrudFactory.ProductionSeason.GetProductionDictiotry(aboutCategory, department);
        }
    }
}
