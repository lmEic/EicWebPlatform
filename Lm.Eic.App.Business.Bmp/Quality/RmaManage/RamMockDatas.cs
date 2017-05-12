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
                mockDataSet.Add(new RmaReportInitiateModel() { RmaId = "11", CustomerShortName = "25", ProductName = "44", RmaIdStatus = "12", RmaYear = "2", RmaMonth = "5" });
                mockDataSet.Add(new RmaReportInitiateModel() { RmaId = "21", CustomerShortName = "25", ProductName = "44", RmaIdStatus = "12", RmaYear = "2", RmaMonth = "5" });
                return mockDataSet;
            }
        }
        public static List<RmaBusinessDescriptionModel> BussesDescriptionMockDataSet
        {
            get
            {
                List<RmaBusinessDescriptionModel> mockDataSet = new List<RmaBusinessDescriptionModel>();
                mockDataSet.Add(new RmaBusinessDescriptionModel() { RmaId = "11", RmaIdNumber = 22, ReturnHandleOrder = "11", ProductId = "22", ProductName = "11", ProductSpec = "11", ProductCount = 110, CustomerId = "2", CustomerName = "115", SalesOrder = "11", BadDescription = "11", CustomerHandleSuggestion = "但是", FeePaymentWay = "嗯嗯" });
                mockDataSet.Add(new RmaBusinessDescriptionModel() { RmaId = "11", RmaIdNumber = 22, ReturnHandleOrder = "11", ProductId = "22", ProductName = "11", ProductSpec = "11", ProductCount = 110, CustomerId = "2", CustomerName = "115", SalesOrder = "11", BadDescription = "11", CustomerHandleSuggestion = "但是", FeePaymentWay = "嗯嗯" });
                return mockDataSet;
            }
        }
    }
}
