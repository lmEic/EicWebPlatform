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
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Framework.ProductMaster.Business.Config;
using Lm.Eic.Framework.ProductMaster.Model.CommonManage;
using Lm.Eic.App.Business.Bmp.WorkFlow.GeneralForm;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace EicWorkPlatfrom.Controllers
{
    /********************************************************************
    	created:	2017/03/27
    	file ext:	cs
    	author:		YLxx
    	purpose:
    *********************************************************************/
    public class QuaInspectionManageController : EicBaseController
    {
        //
        // GET: /QuaInspectionManage/


        public ActionResult Index()
        {
            return View();
        }

        #region 检验项目配置

        #region IQC
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
            //得到此物料的品名 ，规格 ，供应商，图号
            var ProductMaterailModel = QmsDbManager.MaterialInfoDb.GetProductInfoBy(materialId).FirstOrDefault();
            //添加物料检验项
            var InspectionItemConfigModelList = InspectionService.ConfigManager.IqcItemConfigManager.GetIqcspectionItemConfigDatasBy(materialId);

            var datas = new { ProductMaterailModel, InspectionItemConfigModelList };
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
            var productMaterailModel = QmsDbManager.MaterialInfoDb.GetProductInfoBy(materialId).FirstOrDefault();
            var datas = new { result, productMaterailModel };
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除进料检验配置数据
        /// </summary>
        /// <param name="configItem"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpPost]
        public JsonResult DeleteIqlInspectionConfigItem(InspectionIqcItemConfigModel configItem)
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
        public JsonResult SaveIqcInspectionItemConfigDatas(List<InspectionIqcItemConfigModel> iqcInspectionConfigItems)
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
            List<InspectionIqcItemConfigModel> datas = null;
            string filePath = this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.Temp);
            this.SaveFileToServer(file, filePath, () =>
            {
                string fileName = Path.Combine(filePath, file.FileName);
                datas = InspectionService.ConfigManager.IqcItemConfigManager.ImportProductFlowListBy(fileName);
                if (datas != null && datas.Count > 0)
                //批量保存数据
                { var opResult = InspectionService.ConfigManager.IqcItemConfigManager.StoreIqcInspectionItemConfig(datas); }

                System.IO.File.Delete(fileName);
            });
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 载入IQC物料检验配置模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult LoadIqcInspectionItemConfigFile()
        {
            ///路经下载
            string filePath = @"E:\各部门日报格式\IQC物料检验配置数据表.xls";
            var dlfm = InspectionService.ConfigManager.IqcItemConfigManager.GetIqcInspectionItemConfigTemplate(filePath, "IQC物料检验配置模板");
            return this.DownLoadFile(dlfm);
            //return null;
        }
        #endregion

        #region FQC
        /// <summary>
        /// FQC检验项目配置
        /// </summary>
        /// <returns></returns>
        public ActionResult FqcInspectionItemConfiguration()
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
        public JsonResult GetFqcInspectionItemConfigDatas(string materialId)
        {
            //添加物料检验项
            var InspectionItemConfigModelList = InspectionService.ConfigManager.FqcItemConfigManager.GetFqcInspectionItemConfigDatasBy(materialId);
            //得到此物料的品名 ，规格 ，供应商，图号
            var ProductMaterailModel = QmsDbManager.MaterialInfoDb.GetProductInfoBy(materialId).FirstOrDefault();
            //得到ORT信息
            var OrtDatas = InspectionService.ConfigManager.FqcItemConfigManager.GetMaterialORTConfigBy(materialId);
            var datas = new { ProductMaterailModel, InspectionItemConfigModelList, OrtDatas };
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 在数据库中是否存在此料号
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpGet]
        public JsonResult CheckFqcInspectionItemConfigMaterialId(string materialId)
        {

            var dataResult = InspectionService.ConfigManager.FqcItemConfigManager.IsExistFqcConfigMaterailId(materialId);
            return Json(dataResult, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除进料检验配置数据 deleteIqlInspectionConfigItem
        /// </summary>
        /// <param name="configItem"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpPost]
        public JsonResult DeleteFqcInspectionConfigItem(InspectionFqcItemConfigModel configItem)
        {

            var opResult = InspectionService.ConfigManager.FqcItemConfigManager.StoreFqcInspectionItemConfig(configItem);
            return Json(opResult);
        }

        /// <summary>
        /// 批量保存IQC进料检验项目配置数据
        /// </summary>
        /// <param name="fqcInspectionConfigItems"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult SaveFqcInspectionItemConfigDatas(List<InspectionFqcItemConfigModel> fqcInspectionConfigItems, string isNeedORt)
        {
            var opResult = InspectionService.ConfigManager.FqcItemConfigManager.StoreFqcInspectionItemConfig(fqcInspectionConfigItems, isNeedORt);
            return Json(opResult);
        }
        /// <summary>
        /// 存ORT数据
        /// </summary>
        /// <param name="ortModel"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult SaveOrtModel(MaterialOrtConfigModel ortModel)
        {
            var returnResult = InspectionService.ConfigManager.FqcItemConfigManager.SaveOrtData(ortModel);
            return Json(returnResult);
        }
        /// <summary>
        /// 得到ORT配置数据
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetOrtMaterialConfigData(string materialId)
        {
            var datas = InspectionService.ConfigManager.FqcItemConfigManager.GetMaterialORTConfigBy(materialId);
            return Json(datas);
        }
        /// <summary>
        /// 导入Excel  ImportFqcInspectionItemConfigDatas
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult ImportFqcInspectionItemConfigDatas(HttpPostedFileBase file)
        {
            List<InspectionFqcItemConfigModel> datas = null;
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    ///待加入验证文件名称逻辑:
                    string fileName = Path.Combine(this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.Temp), file.FileName);
                    file.SaveAs(fileName);
                    datas = InspectionService.ConfigManager.FqcItemConfigManager.ImportInspectionFqcItemConfigBy(fileName);
                    if (datas != null && datas.Count > 0)
                    //批量保存数据
                    { var opResult = InspectionService.ConfigManager.FqcItemConfigManager.StoreFqcInspectionItemConfig(datas); }
                    System.IO.File.Delete(fileName);
                }
            }

            return Json(datas, JsonRequestBehavior.AllowGet);

        }

        #endregion


        #endregion

        #region  检验方式
        public ActionResult IqcInspectionModeConfiguration()
        {
            return View();
        }
        /// <summary>
        /// 存储  检验方式配置
        /// </summary>
        /// <param name="inspectionMode"></param>
        /// <returns></returns>
        ///获取检验水平数据
        [NoAuthenCheck]
        public JsonResult GetInspectionLevelValues(string inspectionMode)
        {
            var data = InspectionService.ConfigManager.ModeConfigManager.GetInspectionModeConfigStrList(inspectionMode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        ///获取AQL数据
        [NoAuthenCheck]
        public JsonResult GetInspectionAQLValues(string inspectionMode, string inspectionLevel)
        {
            var data = InspectionService.ConfigManager.ModeConfigManager.GetInspectionModeConfigStrList(inspectionMode, inspectionLevel);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 存储配置方式
        /// </summary>
        /// <param name="inspectionModeConfigEntity"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult StoreInspectionModeConfigData(InspectionModeConfigModel inspectionModeConfigEntity)
        {
            var opResult = InspectionService.ConfigManager.ModeConfigManager.StoreInspectionModeConfig(inspectionModeConfigEntity);
            return Json(opResult);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="inspectionMode"></param>
        /// <param name="inspectionLevel"></param>
        /// <param name="inspectionAQL"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetIqcInspectionModeDatas(string inspectionMode, string inspectionLevel, string inspectionAQL)
        {
            var datas = InspectionService.ConfigManager.ModeConfigManager.GetInInspectionModeConfigModelList(inspectionMode, inspectionLevel, inspectionAQL);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 检验方式转换配置
        public ActionResult InspectionModeSwitchConfiguration()
        {
            return View();
        }
        [NoAuthenCheck]
        public JsonResult GetModeSwitchDatas(string inspectionModeType)
        {
            var datas = InspectionService.ConfigManager.ModeSwithConfigManager.GetInspectionModeSwithConfig(inspectionModeType);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult SaveModeSwitchDatas(string inspectionModeType, List<InspectionModeSwitchConfigModel> switchModeList)
        {
            var opResult = InspectionService.ConfigManager.ModeSwithConfigManager.StroeInspectionModeSwithConfig(inspectionModeType, switchModeList);
            return Json(opResult);
        }
        #endregion

        #region  检验项目数据收集
        #region IQC

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
            var datas = InspectionService.DataGatherManager.IqcDataGather.GetIqcInspectionMastersDatasBy(orderId);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 由料号得到检验配置数据
        /// <param name="materialId">料号</param>
        /// <param name="orderId">单号</param>
        /// <returns></returns>
        /// </summary>
        [NoAuthenCheck]
        public JsonResult GetIqcInspectionItemDataSummaryDatas(string orderId, string materialId)
        {
            var datas = InspectionService.DataGatherManager.IqcDataGather.BuildingIqcInspectionItemDataSummaryLabelListBy(orderId, materialId);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除抽检项目
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="materialId"></param>
        /// <param name="inspecitonItem"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult DeleteIqcInspectionItemData(string orderid, string materialId, string inspectionItem)
        {
            var opResult = InspectionService.DataGatherManager.IqcDataGather.DetailDatasGather.DeleteInspectionItems(orderid, materialId, inspectionItem);
            return Json(opResult);
        }
        /// <summary>
        /// 上传IQC/FQC检验采集数据附件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult UploadGatherDataAttachFile(HttpPostedFileBase file)
        {
            FormAttachFileManageModel dto = ConvertFormDataToTEntity<FormAttachFileManageModel>("attachFileDto");
            string filePath = this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.InspectionGatherDataFile, dto.ModuleName);
            string customizeFileName = GeneralFormService.InternalContactFormManager.AttachFileHandler.SetAttachFileName(dto.ModuleName, dto.FormId);
            UploadFileResult result = SaveFileToServer(file, filePath, customizeFileName);
            if (result.Result)
            {
                dto.DocumentFilePath = filePath;
                dto.FileName = customizeFileName;
            }
            return Json(result);
        }
        /// <summary>
        /// 存储采集的数据
        /// </summary>
        /// <param name="gatherData"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpPost]
        public JsonResult StoreIqcInspectionGatherDatas(InspectionItemDataSummaryVM gatherData)
        {
            if (gatherData == null) return Json(new OpResult("数据为空", false));
            if (gatherData.FileName != null && gatherData.FileName.Length > 1)
            {
                gatherData.DocumentPath = Path.Combine(gatherData.DocumentPath, gatherData.FileName);
                gatherData.DocumentPath = gatherData.DocumentPath.Replace(this.SiteRootPath, "");
            }
            var opResult = InspectionService.DataGatherManager.IqcDataGather.StoreInspectionIqcGatherDatas(gatherData);
            return Json(opResult);
        }


        #endregion

        #region FQC
        [NoAuthenCheck]
        public ActionResult InspectionDataGatheringOfFQC()
        {
            return View();
        }
        /// <summary>
        /// 获取FQC工单物料信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetFqcOrderInfoDatas(string orderId)
        {
            var orderInfo = InspectionService.DataGatherManager.FqcDataGather.GetFqcInspectionFqcOrderIdInfoBy(orderId);
            var sampledDatas = InspectionService.DataGatherManager.FqcDataGather.MasterDatasGather.GetFqcMasterOrderIdDatasBy(orderId);

            var datas = new { orderInfo = orderInfo, sampledDatas = sampledDatas };
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 创建抽检表单项
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="sampleCount"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult CreateFqcSampleFormItem(string orderId, int sampleCount)
        {
            var datas = InspectionService.DataGatherManager.FqcDataGather.BuildingFqcInspectionSummaryDatasBy(orderId, sampleCount);

            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 找到已检验中所有检验项目
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderIdNumber"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetFqcSampleFormItems(string orderId, int orderIdNumber)
        {
            var datas = InspectionService.DataGatherManager.FqcDataGather.FindFqcInspectionSummaryDataBy(orderId, orderIdNumber);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 保存FQC检验采集数据
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult StoreFqcInspectionGatherDatas(InspectionItemDataSummaryVM gatherData)
        {
            if (gatherData == null) return Json(new OpResult("数据为空，保存失败", false));
            if (gatherData.FileName != null && gatherData.FileName.Length > 1)//上传文件
            {
                gatherData.DocumentPath = Path.Combine(gatherData.DocumentPath, gatherData.FileName);
                ///除掉根目录
                gatherData.DocumentPath = gatherData.DocumentPath.Replace(this.SiteRootPath, "");
            }
            var datas = InspectionService.DataGatherManager.FqcDataGather.StoreFqcDataGather(gatherData);
            return Json(datas);
        }
        /// <summary>
        ///  orderId, orderIdNumber
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult DeleleFqcInspectionAllGatherDatas(string orderId, int orderIdNumber)
        {
            var opResult = InspectionService.DataGatherManager.FqcDataGather.DeletFqcDetailDatasAndMasterDatasBy(orderId, orderIdNumber);
            return Json(opResult);
        }

        #endregion
        #endregion

        #region 检验单管理
        #region iqc检验单管理
        [NoAuthenCheck]
        public ActionResult InspectionFormManageOfIqc()
        {
            return View();
        }
        /// <summary>
        /// 根据单据状态获得检验单数据 selectedFormStatus,dateFrom,dateTo
        /// </summary>  
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetInspectionFormManageOfIqcDatas(string formQueryString, int queryOpModel, DateTime dateFrom, DateTime dateTo)
        {
            var datas = InspectionService.InspectionFormManager.IqcFromManager.GetInspectionFormManagerDatas(formQueryString, queryOpModel, dateFrom, dateTo);
            TempData["QuaDatas"] = datas;
            return DateJsonResult(datas);
        }
        /// <summary>
        /// 导出EXCEl表清册
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult ExportIqcDateToExcel()
        {
            var datas = TempData["QuaDatas"] as List<InspectionIqcMasterModel>;
            //Excel
            var dlfm = InspectionService.InspectionFormManager.IqcFromManager.BuildDownLoadFileModel(datas);
            return this.DownLoadFile(dlfm);
        }
        /// <summary>
        /// 查找已经有生成项目
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetInspectionFormDetailOfIqcDatas(string orderId, string materialId)
        {
            var datas = InspectionService.DataGatherManager.IqcDataGather.FindIqcInspectionItemDataSummaryLabelListBy(orderId, materialId);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 下载数据文档
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult LoadIqcDatasDownLoadFile(string orderId, string materialId, string inspectionItem)
        {
            DownLoadFileModel dlfm = InspectionService.DataGatherManager.IqcDataGather.GetIqcDatasDownLoadFileModel(SiteRootPath, orderId, materialId, inspectionItem);
            return this.DownLoadFile(dlfm);
        }
        /// <summary>
        /// IQC审核/ 撤消IQC审核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult InspectionFormManageCheckedOfIqcData(InspectionIqcMasterModel model, bool isCheck)
        {
            var opResult = InspectionService.InspectionFormManager.IqcFromManager.AuditIqcInspectionMasterModel(model, isCheck);
            return Json(opResult);
        }


        #endregion

        #region fqc检验单管理
        /// <summary>
        /// Fqc检验单管理
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult InspectionFormManageOfFqc()
        {
            return View();
        }
        /// <summary>
        /// FQC 查询 抽抽验状态
        /// </summary>
        /// <param name="selectedDepartment"></param>
        /// <param name="formStatus"></param>
        /// <param name="fqcDateFrom"></param>
        /// <param name="fqcDateTo"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetInspectionMasterFqcDatas(string selectedDepartment, string formStatus, DateTime fqcDateFrom, DateTime fqcDateTo)
        {
            var Fqcdatas = InspectionService.InspectionFormManager.FqcFromManager.GetInspectionFormManagerListBy(selectedDepartment, formStatus, fqcDateFrom, fqcDateTo);
            TempData["QuaFqcDatas"] = Fqcdatas;
            return DateJsonResult(Fqcdatas);
        }
        /// <summary>
        /// 查询FQC中Erp订单检验信息
        /// </summary>
        /// <param name="selectedDepartment">部门</param>
        /// <param name="dateFrom">起始日期</param>
        /// <param name="dateTo">结束日期</param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult QueryFqcERPOrderInspectionInfos(string selectedDepartment, DateTime dateFrom, DateTime dateTo)
        {
            //12131231313
            var datas = InspectionService.InspectionFormManager.FqcFromManager.GetERPOrderAndMaterialBy(selectedDepartment, dateFrom, dateTo);

            return DateJsonResult(datas);
        }
        /// <summary>
        /// 得到Master抽检验信息 GetInspectionFormMasterOfFqcDatas
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetInspectionFormMasterOfFqcDatas(string orderId)
        {

            var datas = InspectionService.InspectionFormManager.FqcFromManager.FqcMasterDatasBy(orderId);

            return DateJsonResult(datas);
        }
        /// <summary>
        /// 通订单号和序列号得测试详细数据
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderIdNumber"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetInspectionFormDetailOfFqcDatas(string orderId, int orderIdNumber)
        {

            var datas = InspectionService.InspectionFormManager.FqcFromManager.GetInspectionDatailListBy(orderId, orderIdNumber);

            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult PostInspectionFormManageCheckedOfFqcData(InspectionFqcMasterModel model)
        {
            var opResult = InspectionService.InspectionFormManager.FqcFromManager.AuditFqcInspectionModel(model);
            return Json(opResult);
        }
        /// <summary>
        /// 载入部门信息
        /// </summary>
        /// <param name="treeModuleKey"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetConfigDicData(string treeModuleKey)
        {
            var modules = PmConfigService.DataDicManager.FindConfigDatasBy(treeModuleKey);
            return Json(modules, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 导出EXCEl表清册 FileResult
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult ExportFqcDateToExcel()
        {
            var datas = TempData["QuaFqcDatas"] as List<InspectionFqcMasterModel>;
            //Excel
            var dlfm = InspectionService.InspectionFormManager.FqcFromManager.BuildDownLoadFileModel(datas);
            return this.DownLoadFile(dlfm);
        }

        /// <summary>
        /// 下载数据文档
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult LoadFqcDatasDownLoadFile(string orderId, int orderIdNumber, string inspectionItem)
        {
            DownLoadFileModel dlfm = InspectionService.DataGatherManager.FqcDataGather.GetFqcDatasDownLoadFileModel(SiteRootPath, orderId, orderIdNumber, inspectionItem);
            return this.DownLoadFile(dlfm);
        }
        #endregion
        #endregion

    }

    /// <summary>
    /// 上传文件附件数据模型
    /// </summary>
    public class FileAttatchData
    {
        public string OrderId { get; set; }
        public int OrderIdNumber { get; set; }
        public string MaterialId { get; set; }
        public string InspectionItem { get; set; }
    }
}