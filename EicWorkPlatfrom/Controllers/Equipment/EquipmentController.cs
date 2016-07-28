using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;
using Lm.Eic.App.Business.Bmp.Ast;
using Lm.Eic.App.DomainModel.Bpm.Ast;


using Lm.Eic.App.Business.Bmp.Quantity.SampleManger;
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
        public ActionResult AstArchiveScreening()
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
        public ContentResult GetAstCheckListByPlanDate(DateTime planDate)
        {
            var datas = AstService.EquipmentManager.CheckManager.GetWithoutCheckEquipment(planDate);

            return DateJsonResult(datas);
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
        public ContentResult GetAstMaintenanceListByPlanMonth(string planMonth)
        {
            var datas = 0;

            return DateJsonResult(datas);
        }
        #endregion

        [NoAuthenCheck]
        public FileResult ExportToExcel()
        {
            var ds = QuantitySampleService.IQCSampleItemsRecordManager.GetPringSampleItemBy("591-1607032", "32AAP00001200RM");

            var ms= QuantitySampleService.IQCSampleItemsRecordManager.ExportPrintToExcel(ds);

            return this.ExportToExcel(ms, "aaa", "AAA");
        }
    }
}
