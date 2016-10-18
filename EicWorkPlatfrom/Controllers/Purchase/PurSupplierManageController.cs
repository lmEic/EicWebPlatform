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
        /// 建立合格供应商清册
        /// </summary>
        /// <returns></returns>
        public ActionResult BuildQualifiedSupplierInventory()
        {
            return View();
        }
    }
}
