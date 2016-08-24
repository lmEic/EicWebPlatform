using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers.Product
{
    /// <summary>
    /// 生产看板管理器
    /// </summary>
    public class ProBoardController : EicBaseController
    {
        //
        // GET: /ProBoard/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 线材看板控制器
        /// </summary>
        /// <returns></returns>
        public ActionResult JumperWireBoard()
        {
            return View();
        }

        [NoAuthenCheck]
        public JsonResult UploadMaterialBoardFile(HttpPostedFileBase file)
        {
            var result = 0;
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    string fileName = Path.Combine(this.CombinedFilePath("FileLibrary", "TwoMaterialBoard"), file.FileName);
                    file.SaveAs(fileName);
                    result = 1;
                }
            }
            return Json(result);
        }
    }
}
