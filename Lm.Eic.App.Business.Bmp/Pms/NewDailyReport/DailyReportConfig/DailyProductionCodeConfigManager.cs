using Lm.Eic.App.DomainModel.Bpm.Pms.NewDailyReport;
using Lm.Eic.Uti.Common.YleeOOMapper;
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
        public OpResult Store(ProductionCodeConfigModel entity, ProductionCodeConfigModel oldEntity, string opType)
        {
            //OpResult result = OpResult.SetSuccessResult("待进行操作", false);
            //if (opType == "add")
            //{
            //    result = Add(entity);
            //}
            //else if (opType == "delete")
            //{
            //    result = Delete(entity);
            //}
            //else if (opType == "edit")
            //{
            //    result = Update(entity, oldEntity);
            //}
            return DailyReportCrudFactory.ProductionSeason.Store(entity,true);
        }
    }
}
