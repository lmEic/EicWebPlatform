using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;
using Lm.Eic.App.Business.Bmp.Ast;
using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.App.Business.Bmp.Quantity;
using Lm.Eic.App.DomainModel.Bpm.Quanity;



namespace EicWorkPlatfrom.Controllers
{
    public class EquipmentController : EicBaseController
    {
        //
        // GET: /Equipment/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AstArchiveInput()
        {
            return View();
        }

        [NoAuthenCheck]
        public ActionResult EditEquipmentTpl()
        {
            return View();
        }

        #region equipment archives input module
        [NoAuthenCheck]
        public JsonResult GetAstInputConfigDatas()
        {
            var departments = ArchiveService.ArchivesManager.DepartmentMananger.Departments;
            var configData = new { departments = departments };
            return Json(configData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取设备编号             
        /// </summary>
        /// <param name="equipmentType"></param>
        /// <param name="assetType"></param>
        /// <param name="taxType"></param>
        /// <returns></returns>
         [NoAuthenCheck]
        public JsonResult GetEquipmentID(string equipmentType, string assetType, string taxType)
        {
            string id = AstService.EquipmentManager.BuildAssetNumber(equipmentType, assetType, taxType);
            return Json(id, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据录入日期查询设备档案资料
        /// </summary>
        /// <param name="inputDate"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetEquipmentArchivesBy(DateTime inputDate,string assetId,int searchMode)
        {
            var datas = AstService.EquipmentManager.FindBy(new QueryEquipmentDto()
            {
                InputDate = inputDate,
                AssetNumber = assetId,
                SearchMode = searchMode
            });
            return DateJsonResult(datas);
        }

        [NoAuthenCheck]
        public JsonResult SaveEquipmentRecord(EquipmentModel equipment)
        {
            var result = AstService.EquipmentManager.Store(equipment);
            return Json(result);
        }

        /// <summary>
        /// 设备档案总览
        /// </summary>
        /// <returns></returns>
        public ActionResult AstArchiveOverview()
        {
            return View();
        }
        #endregion

        #region equipment check module method
        /// <summary>
        /// 生成校验清单
        /// </summary>
        /// <returns></returns>
        public ActionResult AstBuildCheckList()
        {
            return View();
        }
        /// <summary>
        /// 获取校验清单
        /// </summary>
        /// <param name="planDate"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetAstCheckListByPlanDate(DateTime planDate)
        {
            var datas = AstService.EquipmentManager.CheckManager.GetWaitingCheckListBy(planDate);

            return DateJsonResult(datas);
        }
        [NoAuthenCheck]
        public FileResult CreateWaitingCheckList()
        {
            var ds = AstService.EquipmentManager.CheckManager.BuildWaitingCheckList();
            return this.ExportToExcel(ds, "设备校验清单", "设备校验清单");
        }
        /// <summary>
        /// 输入校验记录
        /// </summary>
        /// <returns></returns>
        public ActionResult AstInputCheckRecord()
        {
            return View();
        }
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult StoreInputCheckRecord(EquipmentCheckRecordModel model)
        {
            model.CheckDate = model.CheckDate.ToDate();
            var result = AstService.EquipmentManager.CheckManager.Store(model);
            return Json(result);
        }
        #endregion

        #region equipment maintenance module method
        /// <summary>
        /// 生成保养清单
        /// </summary>
        /// <returns></returns>
        public ActionResult AstBuildMaintenanceList()
        {
            return View();
        }
        /// <summary>
        /// 获取保养清单
        /// </summary>
        /// <param name="planMonth"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetAstMaintenanceListByPlanMonth(string planMonth)
        {
            var datas = AstService.EquipmentManager.MaintenanceManager.GetWaitingMaintenanceListBy(planMonth);
            return DateJsonResult(datas);
        }
        [NoAuthenCheck]
        public FileResult CreateWaitingMaintenanceList()
        {
            var ds = AstService.EquipmentManager.MaintenanceManager.BuildWaitingMaintenanceList();
            return this.ExportToExcel(ds, "设备保养清单", "设备保养清单");
        }

        /// <summary>
        /// 输入保养记录
        /// </summary>
        /// <returns></returns>
        public ActionResult AstInputMaintenanceRecord()
        {
            return View();
        }
        /// <summary>
        /// 保存保养记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult StoreInputMaintenanceRecord(EquipmentMaintenanceRecordModel model)
        {
            model.MaintenanceDate = model.MaintenanceDate.ToDate();
            var result = AstService.EquipmentManager.MaintenanceManager.Store(model);
            return Json(result);
        }
        #endregion
    }
}
