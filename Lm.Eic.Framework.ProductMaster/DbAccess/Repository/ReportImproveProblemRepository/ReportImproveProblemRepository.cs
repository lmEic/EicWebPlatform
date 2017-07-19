using Lm.Eic.Framework.ProductMaster.DbAccess.Mapping;
using Lm.Eic.Framework.ProductMaster.Model.Tools;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.DbAccess.Repository
{
    public interface IReportImproveProblemRepository : IRepository<ReportImproveProblemModels> { }
    public class ReportImproveProblemRepository : LmProMasterRepositoryBase<ReportImproveProblemModels>, IReportImproveProblemRepository { }
}
