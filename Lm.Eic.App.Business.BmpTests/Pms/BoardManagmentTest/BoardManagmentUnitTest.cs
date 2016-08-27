using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Bmp.Pms.BoardManagment;
using Lm.Eic.App.Erp.Bussiness.MocManage;


namespace Lm.Eic.App.Business.BmpTests.Pms.BoardManagmentTest
{
    [TestClass]
    public class BoardManagmentUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var m = BoardService.MaterialBoardManager.GetMaterialSpecBoardBy("512-1607086");
            //if (m != null)
            //    BoardService.MaterialBoardManager.AddMaterialSpecBoard(m);
        }

        public void TestStore()
        {
            //MaterialSpecBoardModel model = new MaterialSpecBoardModel()
            //{
            //    ProductID = "24JKA014800M0RN",
            //    MaterialID = "32C0P99999211RM,34C0E99999540RM,35Z0I99989274RM,35Z7I99996434RM",
            //    OpSign = "Add",
            //    Remarks = "暂时没有图号"
            //};
            //var result = BoardService.MaterialBoardManager.AddMaterialSpecBoard(model);
            BomManage model = new BomManage();
          
            var models = model.GetBomMaterialList("24JKA015100M0RN");
           

        }
    }
}
