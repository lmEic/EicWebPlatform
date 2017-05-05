using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.DomainModel.Bpm.Quanity;


namespace Lm.Eic.App.Business.Bmp.Quality.RmaManage
{
    public class RmaMockDatas
    {
        public static List<RmaReportInitiateModel> ReportInitiateMockDataSet
        {
            get
            {
                List<RmaReportInitiateModel> mockDataSet = new List<RmaReportInitiateModel>();
                mockDataSet.Add(new RmaReportInitiateModel() { RmaId = "21", CustomerShortName = "25", ProductName = "44", RmaIdStatus = "12", RmaYear = "2", RmaMonth = "5" });
                mockDataSet.Add(new RmaReportInitiateModel() { RmaId = "21", CustomerShortName = "25", ProductName = "44", RmaIdStatus = "12", RmaYear = "2", RmaMonth = "5" });
                return mockDataSet;
            }
        }
    }
}
