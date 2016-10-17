using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers.Product
{
    /// <summary>
    /// 2016-10-17
    /// 排程管理子模块
    /// 工单管理控制器
    /// </summary>
    public class ProMocManageController : EicBaseController
    {
        //
        // GET: /MocManage/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 订单工单比对视图
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckOrderBills()
        {
            return View();
        }
    }
}
