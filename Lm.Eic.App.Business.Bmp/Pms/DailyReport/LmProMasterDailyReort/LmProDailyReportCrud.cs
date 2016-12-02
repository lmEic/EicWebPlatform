
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.App.DbAccess.Bpm.Mapping.LightMaterMapping;
using Lm.Eic.App.DbAccess.Bpm.Repository.PmsRep.DailyReport;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.DailyReport.LmProMasterDailyReort
{
    public class LmProDailyReportCrud : CrudBase<WipProductCompleteInputDataModel, ILmProDailyReportRepository>
    {
        public LmProDailyReportCrud() : base(new LmProDailyReportRepository(), "制三部日报表")
        { }

        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }
        public List<WipProductCompleteInputDataModel> getProdcutCompleteInPutDailyRrportList(DateTime productDate)
        {
        
            DateTime   convertProductDate = productDate.ToDate();
            return irep.Entities.Where(e => e.ProductDate == convertProductDate).ToList();
        }
    }
}
