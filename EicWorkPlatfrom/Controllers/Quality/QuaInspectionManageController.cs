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

        #region IQC 检验项目配置
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
            var InspectionItemConfigModelList = InspectionService.InspectionItemConfigurator.GetIqcspectionItemConfigDatasBy(materialId);
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
            var result = InspectionService.InspectionItemConfigurator.IsExistInspectionConfigMaterId(materialId);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除进料检验配置数据 deleteIqlInspectionConfigItem
        /// </summary>
        /// <param name="configItem"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpPost]
        public JsonResult DeleteIqlInspectionConfigItem(IqcInspectionItemConfigModel configItem) 
        {
            var opResult = InspectionService.InspectionItemConfigurator.StoreIqcInspectionItemConfig(configItem);
           return Json(opResult);
        }
        /// <summary>
        /// 批量保存IQC进料检验项目配置数据
        /// </summary>
        /// <param name="iqcInspectionConfigItems"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult SaveIqcInspectionItemConfigDatas(List<IqcInspectionItemConfigModel> iqcInspectionConfigItems)
        {
            var opResult = InspectionService.InspectionItemConfigurator.StoreIqcInspectionItemConfig(iqcInspectionConfigItems);
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
            List<IqcInspectionItemConfigModel> datas = null;
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    ///待加入验证文件名称逻辑:
                    string fileName = Path.Combine(this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.Temp), file.FileName);
                    file.SaveAs(fileName);
                    datas = InspectionService.InspectionItemConfigurator.ImportProductFlowListBy(fileName);
                    if (datas != null && datas.Count > 0)
                    //批量保存数据
                    { var opResult = InspectionService.InspectionItemConfigurator.StoreIqcInspectionItemConfig(datas); }
                   
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
            MemoryStream ms = InspectionService.InspectionItemConfigurator.GetIqcInspectionItemConfigTemplate(filePath);
            return this.ExportToExcel(ms, "IQC物料检验配置模板", "IQC物料检验配置模板");
            //return null;
        }
        #endregion

        #region  检验方式
        public ActionResult IqcInspectionModeConfiguration()
        {
            return View();
        }
        [NoAuthenCheck]
        public JsonResult StoreIqcInspectionModeData(InspectionModeConfigModel iqcInspectionModeItem)
        {
            var opResult = InspectionService.InspectionModeConfigManager.StoreInspectionModeConfig(iqcInspectionModeItem);
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
            var datas = InspectionService.InspectionDataGather.GetPuroductSupplierInfo(orderId);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 由料号得到检验配置数据
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetIqcInspectionItemConfigDatas(string materialId)
        {
            var datas = InspectionService.InspectionItemConfigurator.GetIqcspectionItemConfigDatasBy(materialId);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 得到检验项目的所有信息
        /// </summary>
        /// <param name="inMaterialCount">进料数量</param>
        /// <param name="materialId">料号</param>
        /// <param name="inspectionItem">检验项</param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetIqcInspectionItemAllInfo(int inMaterialCount, string materialId,string inspectionItem)
        {
            var datas = InspectionService.InspectionDataGather.GetInspectionItemAllInfo(inMaterialCount,materialId,inspectionItem);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region FQC检验项目数据采集
        public ActionResult InspectionDataGatheringOfFQC()
        {
            return View();
        }
        #endregion

        #region 检验单管理
        public ActionResult InspectionFormManage()
        {
            return View();
        }
        #endregion
    }
}
