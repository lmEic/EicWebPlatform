using Lm.Eic.Framework.ProductMaster.Business.Itil;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.Business.BmpTests.ProductMaster
{
    [TestClass()]
    public class ItilDevelopModuleManagerTests
    {
        [TestMethod()]
        public void ItilDevelopModuleManageStoreTest()
        {
            ItilDevelopModuleManageModel model = new ItilDevelopModuleManageModel();
            model.ModuleName = "ModultName";
            model.MClassName = "ClassName";
            model.MFunctionName = "FunctionName";
            model.OpSign = "add";
            var result = ItilService.ItilDevelopModuleManager.Store(model);
            if (!result.Result) { Assert.Fail(); }
        }

        [TestMethod()]
        public void ItilDevelopModuleManageFindByTest()
        {
            Assert.Fail();
        }
    }
}
