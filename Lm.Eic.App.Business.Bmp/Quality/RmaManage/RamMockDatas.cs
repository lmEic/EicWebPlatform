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
        public static List<RmaBusinessDescriptionModel> uMockDataSet
        {
            get
            {
                List<RmaBusinessDescriptionModel> mockDataSet = new List<RmaBusinessDescriptionModel>();
                mockDataSet.Add(new RmaBusinessDescriptionModel() { RmaId = "11", RmaIdNumber = 1, ReturnHandleOrder = "11", ProductId = "11", ProductName = "11", ProductSpec = "11", ProductCount = 11, CustomerId = "11", CustomerName = "11", SalesOrder = "11", BadDescription = "11", ProductsShipDate = DateTime.Now, CustomerHandleSuggestion = "11", FeePaymentWay = "11", HandleStatus = "11", OpPerson = "11", OpSign = "add" });
                return mockDataSet;
            }
        }
    }
}
