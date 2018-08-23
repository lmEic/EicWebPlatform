using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.HwCollaboration.Business.MaterialManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.HwCollaboration.Business;
using Lm.Eic.App.HwCollaboration.Model;
using Lm.Eic.App.HwCollaboration.Model.LmErp;
using System.Web;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Lm.Eic.App.HwCollaboration.Business.MaterialManage.Tests
{
    [TestClass()]
    public class MaterialBaseInfoSettorTests
    {
        [TestMethod()]
        public void ttTest()
        {
            //MaterialBaseInfoSettor settor = new MaterialBaseInfoSettor();
            //var r = settor.tt();
            // Assert.Fail();
        }
    }


    public class kdApiEoOderDemo
    {
        private string EBusinessID = "102120";
        public string Appkey = "f8f2eca7839542bcb94dec48cef173dc";
        public string repURl = "http://api.kdniao.cc/api/Eorderservice";

        public string orderTracesSubByJson()
        {
            string requestData = " {'OrderCode': '012657700222'," +
               "'ShipperCode':'SF'," +
               "'PayType':1," +
               "'ExpType':1," +
               "'Cost':1.0," +
               "'OtherCost':1.0," +
               "'Sender':" +
               "{";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("RequsetDate", HttpUtility.UrlEncode(requestData, Encoding.UTF8));
            param.Add("EBusinessID", EBusinessID);
            param.Add("RequestType", "1007");
            return null;
        }
    }
}