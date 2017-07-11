using Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.EightDReportManage
{
    internal class EightDCrudFactory
    {

        internal static EightDReportMasterCrud MasterCrud
        {
            get { return OBulider.BuildInstance<EightDReportMasterCrud>(); }
        }

        internal static EightDReportDetailsCrud DetailsCrud
        {
            get { return OBulider.BuildInstance<EightDReportDetailsCrud>(); }
        }
    }
    internal class EightDReportMasterCrud : CrudBase<EightReportMasterModel, IEightDReportMasterRepository>
    {
        public EightDReportMasterCrud() : base(new EightDReportMasterRepository(), "8D记录总表")
        {
        }

        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }
    }

    internal class EightDReportDetailsCrud : CrudBase<EightDReportDetailModel, IEightDReportDetailsRepository>
    {
        public EightDReportDetailsCrud() : base(new EightDReportDetailsRepository(), "8D记录总表")
        {
        }

        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }
    }
}
