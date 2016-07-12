﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Bmp.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.Business.Bmp.Ast.Tests
{
    [TestClass()]
    public class EquipmentManagerTests
    {
        [TestMethod()]
        public void BuildAssetNumberTest()
        {
            EquipmentManager tem2 = new EquipmentManager();
           
            Lm.Eic.App.DomainModel.Bpm.Ast.EquipmentModel model = new DomainModel.Bpm.Ast.EquipmentModel();
            model.AssetNumber = "I169001";
            model.EquipmentName = "Test1";
           // model.Id_Key = 4;

             //修改
            //var ttt = tem.ChangeStorage(model, 1);

            string i = tem2.BuildAssetNumber("生产设备", "低值易耗品", "保税");
            string i2 = tem2.BuildAssetNumber("生产设备", "低值易耗品", "非保税");
            string i3 = tem2.BuildAssetNumber("生产设备", "固定资产", "保税");
          
         //  Assert.Fail();
        }
    }
}