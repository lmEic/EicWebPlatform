using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.DomainModel.Bpm.Hrm.GeneralAffairs;
using Lm.Eic.App.Business.Bmp.Hrm.GeneralAffairs;

namespace Lm.Eic.App.Business.BmpTests.Hrm.GeneralAffairs
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            WorkClothesManageModel model = new WorkClothesManageModel()
            {
                       DealwithType="以旧换新" ,
                       Unit = "件",
                       PerCount=2,
                       Department="EIC" ,
                       OpSign="add",
                       ProductName="夏季厂服",
                       ProductCategory = "",
                       ProductSpecify="L",
                       ReceiveUser="张文明",
                       WorkerId="001359" ,
                       WorkerName ="万晓桥"
            };
            var relust = GeneralAffairsService.WorkerClothesManager.ReceiveWorkClothes(model);
        }
    }
}
