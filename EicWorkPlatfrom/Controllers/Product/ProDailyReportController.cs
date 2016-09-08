using System.Web.Mvc;
using System.Collections.Generic;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.App.Business.Bmp.Pms.DailyReport;
using System.IO;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;
using System.Web;

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

        #region report hour set method
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
            var result = DailyReportService.ConfigManager.ProductFlowSetter.GetProductFlowListBy(new QueryDailyReportDto() { 
             Department =department, ProductName =productName,SearchMode=2
            }); 
            return Json(result, JsonRequestBehavior.AllowGet);
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
        /// 获取产品工艺初始化数据
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetProductFlowInitData(string department)
        {
            var departments = ArchiveService.ArchivesManager.DepartmentMananger.Departments;
            var productFlowOverviews = DailyReportService.ConfigManager.ProductFlowSetter.GetProductFlowOverviewListBy(department);
            var data = new { departments = departments, overviews = productFlowOverviews };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 载入产品工艺流程模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult LoadProductFlowTemplateFile()
        {
            string filePath = @"E:\各部门日报格式\日报数据表.xls";
            MemoryStream ms = DailyReportService.ConfigManager.ProductFlowSetter.GetProductFlowTemplate(filePath);
            return this.ExportToExcel(ms, "产品工艺流程模板", "产品工艺流程模板");
        }

        public JsonResult ImportProductFlowDatas(HttpPostedFileBase file)
        {
            List<ProductFlowModel> datas = null;
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    ///待加入验证文件名称逻辑:
                    string fileName = Path.Combine(this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.Temp), file.FileName);
                    file.SaveAs(fileName);
                    datas= DailyReportService.ConfigManager.ProductFlowSetter.ImportProductFlowListBy(fileName);
                    System.IO.File.Delete(fileName);
                }
            }
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region daily report  input method
        /// <summary>
        /// 日报录入
        /// </summary>
        /// <returns></returns>
        public ActionResult DReportInput()
        {
            return View();
        }
        #endregion
    }
}