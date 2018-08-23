using Lm.Eic.App.Business.Bmp.Warehouse;
using Lm.Eic.App.DomainModel.Bpm.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers.WarehouseManage
{
    public class WarehouseController : EicBaseController
    {
        //
        // GET: /Warehouse/
        [NoAuthenCheck]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>  receptionExpress
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpGet]
        public ActionResult ReceptionExpress()
        {
            return View();
        }
        [NoAuthenCheck]
        public ActionResult  TakeExpress()
        {
            return View();
        }
        [NoAuthenCheck]
        public ActionResult QueriesExpress()
        {
            return View();
        }
        [NoAuthenCheck]
        public JsonResult StoreExpressData(ExpressModel model)
        {
            var opResult = WarehouseSeries.ExpressTransceiverManger.StoreExpressModel(model);
            return Json(opResult);
        }

    }
}
