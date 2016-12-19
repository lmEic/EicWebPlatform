using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.DailyReport.LmProMasterDailyReort
{
 public class LmProDailyReportManager
    {

        public List<WipProductCompleteInputDataModel> GetLmProDailyRrportList(DateTime productDate)
        {
            return LmProDailyReportFactory.LmProDailyReport.getProdcutCompleteInPutDailyRrportList(productDate);
        }
    }
}
