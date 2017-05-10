using System.Web.Mvc;

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

        //public ActionResult PerStationWipDataView()
        //{
        //    return View();
        //}

        //public ContentResult LoadShippingScheduleDatas()
        //{
        //    var datas = ShippingService.ScheduleManager.ShippingScheduleDatas;

        //    return DateJsonResult(datas);
        //}

        //public ContentResult LoadProductWipDatasBy(string productType)
        //{
        //    var wipNormalStationSet = ProductWipService.WipDataManager.NormalFlowStationSetter.LoadBy(productType);
        //    var wipDatas = ProductWipService.WipDataManager.LoadBy(productType);

        //    var datas = new { normalStationSets = wipNormalStationSet, wipDatas = wipDatas };
        //    return DateJsonResult(datas);
        //}
    }
}