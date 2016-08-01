using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.Framework.ProductMaster.Business.Itil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;

namespace Lm.Eic.Framework.ProductMaster.Business.Itil.Tests
{
    [TestClass()]
    public class ItilDevelopModuleManagerTests
    {
        [TestMethod()]
        public void ItilDevelopModuleManageStoreTest()
        {
            ItilDevelopModuleManageModel model = new ItilDevelopModuleManageModel();
            model.ModuleName = "ModultName";
            model.ClassName = "ClassName";
            model.FunctionName = "FunctionName";
            model.OpSign = "add";
           var result= ItilService.ItilDevelopModuleManager.Store(model);
            if (!result.Result) { Assert.Fail(); }
        }

        [TestMethod()]
        public void ItilDevelopModuleManageFindByTest()
        {
            Assert.Fail();
        }
    }
}