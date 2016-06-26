using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.DomainModel.Mes.Optical.ProductShipping;
using Lm.Eic.App.Business.Mes.Optical.ProductShipping;
using Lm.Eic.App.DomainModel.Mes.Optical.ProductWip;
using Lm.Eic.App.Business.Mes.Optical.ProductWip;

namespace EicWorkPlatfrom.Controllers
{
    public class ShippingController : EicBaseController
    {

        //
        // GET: /Shipping/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowWipDetailBoard()
        {
            return View();
        }
        public ActionResult PerStationWipDataView()
        {
            return View();
        }
        public ContentResult LoadShippingScheduleDatas()
        {
            var datas = ShippingService.ScheduleManager.ShippingScheduleDatas;

            return DateJsonResult(datas);
        }

        public ContentResult LoadProductWipDatasBy(string productType)
        {
            var wipNormalStationSet = ProductWipService.WipDataManager.NormalFlowStationSetter.LoadBy(productType);
            var wipDatas = ProductWipService.WipDataManager.LoadBy(productType);

            var datas = new { normalStationSets = wipNormalStationSet, wipDatas = wipDatas };
            return DateJsonResult(datas);
        }
    }
}
