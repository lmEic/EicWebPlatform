using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Bmp.Quality.RmaManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.DomainModel.Bpm.Quanity;

namespace Lm.Eic.App.Business.Bmp.Quality.RmaManage.Tests
{
    [TestClass()]
    public class RmaBusinessDescriptionProcessorTests
    {
        [TestMethod()]
        public void StoreRmaBussesDescriptionDataTest()
        {
            RmaBusinessDescriptionProcessor r = new RmaBusinessDescriptionProcessor();
            r.StoreRmaBussesDescriptionData(new RmaBusinessDescriptionModel()
            {
                RmaId = "11",
                RmaIdNumber = 1,
                ReturnHandleOrder = "11",
                ProductId = "11",
                ProductName = "11",
                ProductSpec = "11",
                ProductCount = 11,
                CustomerId = "11",
                CustomerName = "11",
                SalesOrder = "11",
                BadDescription = "11",
                ProductsShipDate = DateTime.Now,
                CustomerHandleSuggestion = "11",
                FeePaymentWay = "11",
                HandleStatus = "11",
                OpPerson = "11",
                OpSign = "add"
            });
            Assert.Fail();
        }
    }
}