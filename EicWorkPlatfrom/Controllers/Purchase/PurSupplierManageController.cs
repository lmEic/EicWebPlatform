using Lm.Eic.App.Business.Bmp.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers.Purchase
{
    public class PurSupplierManageController : EicBaseController
    {
        //
        // GET: /PurSupplierManage/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 供应商录入
        /// </summary>
        /// <returns></returns>
        public ActionResult PurSupplierInput()
        {
            return View();
        }

        /// <summary>
        /// 建立合格供应商清册
        /// </summary>
        /// <returns></returns>
        public ActionResult BuildQualifiedSupplierInventory()
        {
            return View();
        }


        /// <summary>
        /// 获取合格供应商列表
        /// </summary>
        /// <param name="yearStr">年份</param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetPurQualifiedSupplierListBy(string yearStr)
        {
            var datas = PurchaseService.PurSupplierManager.PutInManage.FindQualifiedSupplierList(yearStr);
            return DateJsonResult(datas);
        }

        /// <summary>
        /// 获取合格供应商信息
        /// </summary>
        /// <param name="supplierId">供应商编号</param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetErpSuppplierInfoBy(string supplierId)
        {
            var datas = PurchaseService.PurSupplierManager.PutInManage.GetSuppplierInfoBy(supplierId);
            return DateJsonResult(datas);
        }

    }
}
