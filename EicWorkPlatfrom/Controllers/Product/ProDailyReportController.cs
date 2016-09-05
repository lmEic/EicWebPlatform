using System.Web.Mvc;
using System.Collections.Generic;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.App.Business.Bmp.Pms.DailyReport;

using System.IO;
using System.Web;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;

namespace EicWorkPlatfrom.Controllers.Product
{
    public class ProDailyReportController : EicBaseController
    {
        //
        // GET: /DailyReport/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DReportHoursSet()
        {
            return View();
        }

        /// <summary>
        /// 获取产品工艺流程列表
        /// </summary>
        /// <param name="department"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        [HttpGet]
        [NoAuthenCheck]
        public JsonResult GetProductFlowList(string department, string productName)
        {
            var result = 0;

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取产品工艺总览
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetProductFlowOverview(string department)
        {
            List<ProductFlowOverviewModel> overviewDatas = null;

            return Json(overviewDatas, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存产品工艺流程数据
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult StoreProductFlowDatas(List<ProductFlowModel> entities)
        {
            var datas = 0;

            return Json(datas);
        }
        /// <summary>
        /// 下载产品工艺流程模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult LoadProductFlowTemplateFile()
        {
            MemoryStream ms = null;
            return this.ExportToExcel(ms, "产品工艺流程模板", "产品工艺流程模板");
        }


        /// <summary>
        /// 导入产品工艺流程模板文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult ImportProductFlowTemplateFile(HttpPostedFileBase file)
        {
            var result = 0;
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    ///待加入验证文件名称逻辑:
                    string fileName = Path.Combine(this.CombinedFilePath("FileLibrary", "Temp"), file.FileName);
                    //file.SaveAs(fileName);
                    result = 1;
                }
            }
            return Json(result);
        }

        /// <summary>
        /// 获取工艺流程初始化数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [NoAuthenCheck]
        public JsonResult GetProductFlowInitData()
        {
            var departments = ArchiveService.ArchivesManager.DepartmentMananger.Departments;
            var initDatas = new { departments = departments };

            return Json(initDatas, JsonRequestBehavior.AllowGet);
        }
    }
}