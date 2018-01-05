using Lm.Eic.App.DomainModel.Bpm.Pms.NewDailyReport;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.NewDailyReport.DailyReportConfig
{
   public  class ProductionDefectiveTreatmentManager
    {
        public OpResult StoreOpData(DailyProductionDefectiveTreatmentModel model)
        {
            return DailyReportCrudFactory.ProductionDefectiveTreatment.Store(model, true);
        }


    }
}
