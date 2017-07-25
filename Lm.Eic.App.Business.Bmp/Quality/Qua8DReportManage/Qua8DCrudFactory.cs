using Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.Qua8DReportManage
{
    internal class Qua8DCrudFactory
    {

        internal static Qua8DReportMasterCrud MasterCrud
        {
            get { return OBulider.BuildInstance<Qua8DReportMasterCrud>(); }
        }

        internal static Qua8DReportDetailsCrud DetailsCrud
        {
            get { return OBulider.BuildInstance<Qua8DReportDetailsCrud>(); }
        }
    }
    internal class Qua8DReportMasterCrud : CrudBase<Qua8DReportMasterModel, IQua8DReportMasterRepository>
    {
        public Qua8DReportMasterCrud() : base(new Qua8DReportMasterRepository(), "8D记录总表")
        {
        }

        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }
    }

    internal class Qua8DReportDetailsCrud : CrudBase<Qua8DReportDetailModel, IQua8DReportDetailsRepository>
    {
        public Qua8DReportDetailsCrud() : base(new Qua8DReportDetailsRepository(), "8D记录总表")
        {
        }

        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public List<Qua8DReportDetailModel> GetQua8DDetailDatasBy(string reportId)
        {
            return irep.Entities.Where(e => e.ReportId == reportId).ToList();
        }
    }
}
