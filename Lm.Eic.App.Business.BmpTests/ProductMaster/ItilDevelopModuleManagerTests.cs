using Lm.Eic.Framework.ProductMaster.Business.Itil;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.Uti.Common.YleeExtension.Validation;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;

namespace Lm.Eic.App.Business.BmpTests.ProductMaster
{
    [TestClass()]
    public class ItilDevelopModuleManagerTests
    {
        [TestMethod()]
        public void AutoTest()
        {

        }


        [TestMethod()]
        public void ItilDevelopModuleManageStoreTest()
        {
            ItilDevelopModuleManageModel model = new ItilDevelopModuleManageModel();
            model.ModuleName = "ModultName";
            model.MClassName = "ClassName";
            model.MFunctionName = "FunctionName5";
            model.ParameterKey = string.Format("{0}&{1}&{2}", model.ModuleName, model.MClassName, model.MFunctionName);
            model.FunctionDescription = "功能描述";
            model.DifficultyCoefficient = 5;
            model.DevPriority = 5;
            model.Executor = "张明";
            model.StartDate = DateTime.Now.ToDate();
            model.OpSign = "add";
            var result = ItilService.ItilDevelopModuleManager.Store(model);
            if (!result.Result) { Assert.Fail(); }
        }

        [TestMethod()]
        public void ItilDevelopModuleManageFindByTest()
        {
            List<string> stateList = new List<string>() { "待开发", "待审核" };
            var devList = ItilService.ItilDevelopModuleManager.GetDevelopModuleManageListBy(stateList);
            if (devList.Count <= 0) { Assert.Fail(); }
        }

        [TestMethod()]
        public void ChangeProgressTest()
        {
            List<string> stateList = new List<string>() { "待开发", "待审核" };
            var devList = ItilService.ItilDevelopModuleManager.GetDevelopModuleManageListBy(stateList);
            var model = devList[0];
            model.CurrentProgress = "待审核";
            var result = ItilService.ItilDevelopModuleManager.ChangeProgressStatus(model);
            if (!result.Result) { Assert.Fail(); }
        }

    }
}
