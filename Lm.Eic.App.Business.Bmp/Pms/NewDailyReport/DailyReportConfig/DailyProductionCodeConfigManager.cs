using Lm.Eic.App.DomainModel.Bpm.Pms.NewDailyReport;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.NewDailyReport.DailyReportConfig
{
    public class UnproductiveReasonConfigManager
    {
        public List<UnproductiveReasonConfigModel> GetProductionDictiotry(string aboutCategory, string department)
        {
            return DailyReportCrudFactory.UnproductiveSeason.GetProductionDictiotry(aboutCategory, department);
        }
        public OpResult Store(UnproductiveReasonConfigModel entity, UnproductiveReasonConfigModel oldEntity, string opType)
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
            return DailyReportCrudFactory.UnproductiveSeason.Store(entity,true);
        }
    }
}
