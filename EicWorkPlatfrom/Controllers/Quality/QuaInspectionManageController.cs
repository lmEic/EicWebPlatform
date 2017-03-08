using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.Quality.InspectionManage;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using System.IO;
using Lm.Eic.App.Erp.Bussiness.QmsManage;

namespace EicWorkPlatfrom.Controllers
{
    public class QuaInspectionManageController : EicBaseController
    {
        //
        // GET: /QuaInspectionManage/
       
        
        public ActionResult Index()
        {
            return View();
        }

        #region 检验配置
        /// <summary>
        /// IQC检验项目配置
        /// </summary>
        /// <returns></returns>
        public ActionResult IqcInspectionItemConfiguration()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpGet]
        public JsonResult GetIqcspectionItemConfigDatas(string materialId)
        {
            //var InspectionItemConfigModelList = InspectionService.InspectionItemConfigurator.GetIqcspectionItemConfigDatasBy(materialId);
            var InspectionItemConfigModelList = InspectionService.ConfigManager.IqcItemConfigManager.GetIqcspectionItemConfigDatasBy(materialId);
            var ProductMaterailModel = QmsDbManager.MaterialInfoDb.GetProductInfoBy(materialId).FirstOrDefault();
            var datas= new {ProductMaterailModel, InspectionItemConfigModelList };
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 在数据库中是否存在此料号
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpGet]
        public JsonResult CheckIqcspectionItemConfigMaterialId(string materialId)
        {
            var result = InspectionService.ConfigManager.IqcItemConfigManager.IsExistInspectionConfigMaterailId(materialId);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除进料检验配置数据 deleteIqlInspectionConfigItem
        /// </summary>
        /// <param name="configItem"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpPost]
        public JsonResult DeleteIqlInspectionConfigItem(InspectionIqCItemConfigModel configItem) 
        {
            var opResult = InspectionService.ConfigManager.IqcItemConfigManager.StoreIqcInspectionItemConfig(configItem);
           return Json(opResult);
        }
        /// <summary>
        /// 批量保存IQC进料检验项目配置数据
        /// </summary>
        /// <param name="iqcInspectionConfigItems"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult SaveIqcInspectionItemConfigDatas(List<InspectionIqCItemConfigModel> iqcInspectionConfigItems)
        {
            var opResult = InspectionService.ConfigManager.IqcItemConfigManager.StoreIqcInspectionItemConfig(iqcInspectionConfigItems);
            return Json(opResult);
        }
        /// <summary>
        /// 导入EXCEL数据到IQC物料检验配置
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult ImportIqcInspectionItemConfigDatas(HttpPostedFileBase file)
        {
            List<InspectionIqCItemConfigModel> datas = null;
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    ///待加入验证文件名称逻辑:
                    string fileName = Path.Combine(this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.Temp), file.FileName);
                    file.SaveAs(fileName);
                    datas = InspectionService.ConfigManager.IqcItemConfigManager.ImportProductFlowListBy(fileName);
                    if (datas != null && datas.Count > 0)
                    //批量保存数据
                    { var opResult = InspectionService.ConfigManager.IqcItemConfigManager.StoreIqcInspectionItemConfig(datas); }
                   
                    System.IO.File.Delete(fileName);
                }
            }
           
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 载入IQC物料检验配置模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult LoadIqcInspectionItemConfigFile()
        {
            string filePath = @"E:\各部门日报格式\IQC物料检验配置数据表.xls";
            MemoryStream ms = InspectionService.ConfigManager.IqcItemConfigManager.GetIqcInspectionItemConfigTemplate(filePath);
            return this.ExportToExcel(ms, "IQC物料检验配置模板", "IQC物料检验配置模板");
            //return null;
        }
        #endregion

        #region  检验方式
        public ActionResult IqcInspectionModeConfiguration()
        {
            return View();
        }
        /// <summary>
        /// 存储  检验方式配置
        /// </summary>
        /// <param name="inspectionModeConfigEntity"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult StoreInspectionModeConfigData(InspectionModeConfigModel inspectionModeConfigEntity)
        {
            var opResult = InspectionService.ConfigManager.ModeConfigManager.StoreInspectionModeConfig(inspectionModeConfigEntity);
            return Json(opResult);
        }
        #endregion


      
        #region  检验项目数据收集

        [NoAuthenCheck]
        public ActionResult InspectionDataGatheringOfIQC()
        {
            return View();
        }
        /// <summary>
        /// 由单号得到物料所有信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetIqcMaterialInfoDatas(string orderId)
        {
            var datas = InspectionService.DataGatherManager.IqcDataGather.GetPuroductSupplierInfo(orderId);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 由料号得到检验配置数据
        /// <param name="materialId">料号</param>
        /// <param name="orderId">单号</param>
        /// <returns></returns>
        /// </summary>
        [NoAuthenCheck]
        [HttpGet]
        public JsonResult GetIqcInspectionItemDataSummaryLabelList(string orderId,string materialId)
        {
            var datas = InspectionService.DataGatherManager.IqcDataGather.BuildingIqcInspectionItemDataSummaryLabelListBy(orderId, materialId);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        [HttpPost]
        public JsonResult StoreIqcInspectionGatherDatas(InspectionIqcItemDataSummaryLabelModel gatherData)
        {

            var opResult= InspectionService.DataGatherManager.IqcDataGather.StoreInspectionIqcDetailModelForm(gatherData);
            return Json(opResult);
        }
        #endregion



        #region 检验单管理
        #region iqc检验单管理
        [NoAuthenCheck]
        public ActionResult InspectionFormManageOfIqc()
        {
            return View();
        }
        /// <summary>
        /// 根据单据状态获得检验单数据  
        /// </summary>  selectedFormStatus,dateFrom,dateTo
        /// <returns></returns>

        [NoAuthenCheck]
        public ContentResult GetInspectionFormManageOfIqcDatas(string formStatus, DateTime dateFrom, DateTime dateTo)
        {
            var datas = InspectionService.InspectionFormManager.GetInspectionFormManagerListBy(formStatus, dateFrom, dateTo);
            return DateJsonResult(datas);
        }
        [NoAuthenCheck]
        public JsonResult GetInspectionFormDetailDatas(string orderId, string materialId)
        {
            var datas = InspectionService.DataGatherManager.IqcDataGather.FindIqcInspectionItemDataSummaryLabelListBy(orderId, materialId);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult PostInspectionFormManageCheckedData(InspectionIqcMasterModel model)
        {
            var opResult = InspectionService.DataGatherManager .IqcDataGather .StoreIqcInspectionMasterModel (model);
            return Json(opResult);
        }
        #endregion

        #region fqc检验单管理
        /// <summary>
        /// Fqc检验单管理
        /// </summary>
        /// <returns></returns>
        public ActionResult InspectionFormManageOfFqc()
        {
            return View();
        }
        #endregion
        #endregion

    }
}
