using Lm.Eic.App.DomainModel.Bpm.Quanity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
  public  class InspectionIpqcDataGather
    {
        public  List<InspectionIpqcReportModel> getReportInfoDatas(DateTime qudate,string department)
        {
            return InspectionManagerCrudFactory.IpqcReportCrud.GetReportDatas(qudate,department);
        }
    }
}
