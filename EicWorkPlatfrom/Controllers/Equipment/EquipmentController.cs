using System;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;
using Lm.Eic.App.Business.Bmp.Ast;
using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using System.IO;



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
        
        #region Ast EquipmentInfo View
        /// <summary>
        /// 生成校验清单
        /// </summary>
        /// <returns></returns>
        public ActionResult AstEquipmentInfoView()
        {
            return View();
        }

        /// <summary>
        /// 生成盘点清单
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult CreateInventoryList()
        {
            var ds = AstService.EquipmentManager.BuildInventoryList();
            return this.ExportToExcel(ds, "设备盘点清单", "设备盘点清单");
        }
        #endregion


        #region Ast Archive Overview
        /// <summary>
        /// 获取设备总览数据
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetAstArchiveOverview()
        {
            var datas = AstService.EquipmentManager.GetAstArchiveOverview();
            return DateJsonResult(datas);
        }
        #endregion

        
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
        /// <summary>
        /// 设备报废登记
        /// </summary>
        /// <returns></returns>
        public ActionResult AstScrapInput() 
        {
            return View();
        }
        /// <summary>
        /// 保存报废数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult StoreAstScrapData(EquipmentDiscardRecordModel model)
        {
            model.DiscardDate = model.DiscardDate.ToDate();
            var result = AstService.EquipmentManager.DiscardManager.Store(model);
            return Json(result);
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
        [HttpGet]
        [NoAuthenCheck]
        public ContentResult GetAstCheckListByPlanDate(DateTime planDate)
        {
            var datas = AstService.EquipmentManager.CheckManager.GetWaitingCheckListBy(planDate);

            return DateJsonResult(datas);
        }
        /// <summary>
        /// 获取校验清单
        /// </summary>
        /// <param name="planDate"></param>
        /// <returns></returns>
        [HttpGet]
        [NoAuthenCheck]
        public ContentResult GetAstCheckListByAssetNumber(string assetNumber)
        {
            var datas = AstService.EquipmentManager.CheckManager.FindBy(new QueryEquipmentDto()
            {
                AssetNumber = assetNumber,
                SearchMode = 1
            });
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

        /// <summary>
        /// 获取保养记录
        /// </summary>
        /// <param name="assetNumber">财产编号</param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetAstMaintenanceListByAssetNumber(string assetNumber)
        {
            var datas = AstService.EquipmentManager.MaintenanceManager.FindBy(new QueryEquipmentDto() { AssetNumber = assetNumber, SearchMode = 1 });
            return DateJsonResult(datas);
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
        [NoAuthenCheck]
        public JsonResult HandleFile(string moduleName, string fileName)
        {
            bool result = false;
            string dateFile = DateTime.Now.ToDateStr();
            string[] f = fileName.Split('-');
            string assetNum = string.Empty;
            string imgUrl = string.Empty;
            if (f.Length == 3)
            {
                assetNum = f[1];
                string sourceFileName = CombinedFilePath("FileLibrary", "PreviewFiles") +"\\"+ fileName;
                var imgBytes = sourceFileName.ToPhotoByte();
                imgUrl = GetBase64Url(imgBytes);
                string destFileName = CombinedFilePath("FileLibrary", moduleName, dateFile) + "\\" + assetNum + ".jpg";
                if (System.IO.File.Exists(destFileName)) System.IO.File.Delete(destFileName);
                System.IO.File.Move(sourceFileName, destFileName);
                result = true;
            }
            var data = new { exist = result, dateFile = dateFile, imgUrl = imgUrl };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        
        #region equipment repair module method
      
        /// <summary>
        /// 设备维修录入
        /// </summary>
        /// <returns></returns>
        public ActionResult AstInputRepairRecord()
        {
            return View();
        }
        public ActionResult EditEquipmentRepairTpl()
        {
            return View();
        }

        /// <summary>
        /// 保存维修数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult StoreAstRepairedData(EquipmentRepairedRecordModel model)
        {
            var result = AstService.EquipmentManager.RepairedManager.AddEquipmentRepairedRecord(model);
            return Json(result);
        }

        /// <summary>
        /// 获取维修记录
        /// </summary>
        /// <param name="assetNumber">财产编号</param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetAstRepairListByAssetNumber(string assetNumber)
        {
            var datas = AstService.EquipmentManager.RepairedManager.GetEquipmentRepairedRecordBy(assetNumber);
            return DateJsonResult(datas);
        }

        /// <summary>
        /// 获取设备维修总览表
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetEquipmentRepairedOverView()
        {
            var datas = AstService.EquipmentManager.RepairedManager.GetEquipmentRepairedOverView();
            return DateJsonResult(datas);
        }
        /// <summary>
        /// 获取设备维修查询记录
        /// </summary>
        /// <param name="assetNumber"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetEquipmentRepairAssetNumberDatas(string assetNumber)
        {

            var datas = AstService.EquipmentManager.RepairedManager.GetEquipmentRepairedRecordBy(assetNumber);
            return DateJsonResult(datas);

        }
        /// <summary>
        ///  获取设备维修表单
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetEquipmentRepairFormIdDatas(string formId)
        {

            var datas = AstService.EquipmentManager.RepairedManager.GetEquipmentRepairedRecordFormBy(formId);
            return DateJsonResult(datas);
        }
        #endregion


        #region equipment Discard module method

        /// <summary>
        /// 获取设备报废总览表
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetEquipmentDiscardOverView()
        {
            var datas = AstService.EquipmentManager.DiscardManager.GetEquipmentDiscardOverView();
            return DateJsonResult(datas);
        }
        /// <summary>
        /// 获取报废记录
        /// </summary>
        /// <param name="assetNumber">财产编号</param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetAstDiscardListByAssetNumber(string assetNumber)
        {
            var datas = AstService.EquipmentManager.DiscardManager.GetEquipmentDiscardDetails(assetNumber);
            return DateJsonResult(datas);
        }
        #endregion
    }
}
