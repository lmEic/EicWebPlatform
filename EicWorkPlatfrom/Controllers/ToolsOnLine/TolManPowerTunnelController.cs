using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;

namespace EicWorkPlatfrom.Controllers
{
    /// <summary>
    /// 人力隧道控制器
    /// </summary>
    public class TolManPowerTunnelController : EicBaseController
    {
        //
        // GET: /TolManPowerTunnel/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TolManpowerDisk()
        {
            return View();
        }
        [NoAuthenCheck]
        public JsonResult GetWorkerAnalogDatas(string department)
        {
            CompanyWorkerAnalogDto workerAnalogDatas = ArchiveService.WorkerQueryManager.GetWorkerAnalogDatas(department);
            var departments = ArchiveService.ArchivesManager.DepartmentMananger.Departments;
            var dto = new { workerAnalogDatas, departments };
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult GetWorkersDetailBy(string department, string post, int searchMode)
        {
            var workers = ArchiveService.ArchivesManager.FindWorkers(new QueryWorkersDto()
            {
                Department = department,
                Post = post
            }, searchMode);
            return Json(workers, JsonRequestBehavior.AllowGet);
        }

        public FileResult ExportWorkersToExcel()
        {
            return this.DownLoadFile(ArchiveService.WorkerQueryManager.ExportWorkersToExcel());
        }
    }
}
