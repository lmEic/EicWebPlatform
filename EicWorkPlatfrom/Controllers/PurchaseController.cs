using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.Purchase;
using Lm.Eic.App.Erp.Domain.PurchaseManage;

namespace EicWorkPlatfrom.Controllers
{
    public class PurchaseController : EicBaseController
    {
        //
        // GET: /Purchase/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PurQuery()
        {
            return View();
        }

        public ActionResult PurchaseBodyTpl()
        {
            return View();
        }
        #region requisition
        public JsonResult FindReqHeaderDatas(string department, DateTime dateFrom, DateTime dateTo)
        {
            List<RequisitionHeaderModel> reqHeaders = PurchaseService.ReqManager.FindReqHeaderDatasBy(department, dateFrom, dateTo);
            return Json(reqHeaders, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FindReqBodyDatas(string department, DateTime dateFrom, DateTime dateTo)
        {
            List<RequisitionBodyModel> reqBodys = PurchaseService.ReqManager.FindReqBodyDatasBy(department, dateFrom, dateTo);
            return Json(reqBodys, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FindReqBodyDatasByID(string requsitionID)
        {
            List<RequisitionBodyModel> reqBodys = PurchaseService.ReqManager.FindReqBodyDatasByID(requsitionID);
            return Json(reqBodys, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region purchase
        public JsonResult FindPurHeaderDatas(string department, DateTime dateFrom, DateTime dateTo)
        {
            List<PurchaseHeaderModel> reqHeaders = PurchaseService.PurManager.FindPurHeaderDatasBy(department, dateFrom, dateTo);
            return Json(reqHeaders, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FindPurBodyDatas(string department, DateTime dateFrom, DateTime dateTo)
        {
            List<PurchaseBodyModel> purBodys = PurchaseService.PurManager.FindPurBodyDatasBy(department, dateFrom, dateTo);
            return Json(purBodys, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FindPurBodyDatasByID(string purchaseID)
        {
            List<PurchaseBodyModel> purBodys = PurchaseService.PurManager.FindPurBodyDatasByID(purchaseID);
            return Json(purBodys, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region stock
        public JsonResult FindStoHeaderDatas(string department, DateTime dateFrom, DateTime dateTo)
        {
            List<StockHeaderModel> reqHeaders = PurchaseService.StoManager.FindStoHeaderDatasBy(department, dateFrom, dateTo);
            return Json(reqHeaders, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FindStoBodyDatas(string department, DateTime dateFrom, DateTime dateTo)
        {
            List<StockBodyModel> StoBodys = PurchaseService.StoManager.FindStoBodyDatasBy(department, dateFrom, dateTo);
            return Json(StoBodys, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FindStoBodyDatasByID(string stockID)
        {
            List<StockBodyModel> StoBodys = PurchaseService.StoManager.FindStoBodyDatasByID(stockID);
            return Json(StoBodys, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
