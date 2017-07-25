using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.Qua8DReportManage
{
    public class Qua8DManager
    {
        public Qua8DMasterManager Qua8DMaster
        {
            get { return OBulider.BuildInstance<Qua8DMasterManager>(); }
        }

        public Qua8DDatailManager Qua8DDatail
        {
            get { return OBulider.BuildInstance<Qua8DDatailManager>(); }
        }
    }
    public class Qua8DMasterManager
    {

    }
    public class Qua8DDatailManager
    {
        public List<Qua8DReportDetailModel> GetQua8DDetailDatasBy(string reportId)
        {
            return Qua8DCrudFactory.DetailsCrud.GetQua8DDetailDatasBy(reportId);
        }
        public Qua8DReportDetailModel GetQua8DDetailDatasBy(string reportId, int stepId)
        {
            return Qua8DCrudFactory.DetailsCrud.GetQua8DDetailDatasBy(reportId).FirstOrDefault(e => e.StepId == stepId);
        }
    }
}
