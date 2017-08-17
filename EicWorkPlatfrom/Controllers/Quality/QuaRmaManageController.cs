using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.Quality.RmaManage;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Newtonsoft.Json;
using System.IO;
using Lm.Eic.Framework.ProductMaster.Model;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Framework.ProductMaster.Business.Config;

namespace EicWorkPlatfrom.Controllers.Quality
{
    public class QuaRmaManageController : EicBaseController
    {
        //
        // GET: /QuaRma/

        public ActionResult Index()
        {
            return View();
        }

        #region CreateRmaForm
        [NoAuthenCheck]
        public ActionResult CreateRmaForm()
        {
            return View();
        }
        /// <summary>
        ///自动生成RmaId单号
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult AutoBuildingRmaId()
        {
            var data = RmaService.RmaManager.RmaReportBuilding.AutoBuildingRmaId();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 保存 初始RMA单数据
        /// </summary>
        /// <param name="initiateData"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult StoreinitiateDataData(RmaReportInitiateModel initiateData)
        {
            var result = RmaService.RmaManager.RmaReportBuilding.StoreRamReortInitiate(initiateData);
            return Json(result);
        }
        /// <summary>
        /// 得到Rma单的数据
        /// </summary>
        /// <param name="rmaId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetRmaReportMaster(string rmaId)
        {
            var datas = RmaService.RmaManager.RmaReportBuilding.GetInitiateDatas(rmaId);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="archiveConfig"></param>
        /// <param name="rmaCustomerShortName"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetCustomerShortNameDatas(string archiveConfig, string rmaCustomerShortName)
        {
            var datas = PmConfigService.DataDicManager.LoadConfigDatasBy("CustomerConfigDataSet", "CustomerName");
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region RmaInputDescription
        [NoAuthenCheck]
        public ActionResult RmaInputDescription()
        {
            return View();
        }

        [NoAuthenCheck]
        public ContentResult GetReturnOrderInfo(string orderId)
        {
            var datas = RmaService.RmaManager.BusinessManageProcessor.GetErpBusinessInfoDatasBy(orderId);
            return DateJsonResult(datas);
        }
        /// <summary>
        /// 由Rma单量   描述信息
        /// </summary>
        /// <param name="rmaId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetRmaDescriptionDatas(string rmaId)
        {
            /// Rma 初始表的数据
            var rmaInitiateData = RmaService.RmaManager.RmaReportBuilding.GetInitiateDatas(rmaId).FirstOrDefault();
            /// 业务部处理的数据
            var bussesDescriptionDatas = RmaService.RmaManager.BusinessManageProcessor.GetRmaBusinessDescriptionDatasBy(rmaId);

            var datas = new { rmaInitiateData, bussesDescriptionDatas };

            return DateJsonResult(datas);
        }

        /// <summary>
        /// 存储 业务部描述信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpPost]
        public JsonResult StoreRmaInputDescriptionData(RmaBusinessDescriptionModel model)
        {
            var opResult = RmaService.RmaManager.BusinessManageProcessor.StoreRmaBusinessDescriptionData(model);
            return Json(opResult);
        }
        #endregion

        #region RmaInspectionHandle
        [NoAuthenCheck]
        public ActionResult RmaInspectionHandle()
        {
            return View();
        }
        /// <summary>
        /// 由Rma单量得到 描述信息和处理信息
        /// </summary>
        /// <param name="rmaId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetRmaInspectionHandleDatas(string rmaId)
        {
            /// Rma 初始表的数据
            var rmaInitiateData = RmaService.RmaManager.RmaReportBuilding.GetInitiateDatas(rmaId).FirstOrDefault();
            /// 业务部处理的数据
            var bussesDescriptionDatas = RmaService.RmaManager.BusinessManageProcessor.GetRmaBusinessDescriptionDatasBy(rmaId);
            /// 检验处理数据
            var inspectionHandleDatas = RmaService.RmaManager.InspecitonManageProcessor.GetDatasBy(rmaId);


            var datas = new { rmaInitiateData, inspectionHandleDatas, bussesDescriptionDatas };

            return DateJsonResult(datas);
        }
        /// <summary>
        /// 存储 检验处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult StoreRmaInspectionHandleDatas(RmaInspectionManageModel model)
        {
            var opReult = RmaService.RmaManager.InspecitonManageProcessor.StoreInspectionManageData(model);

            return Json(opReult);
        }
        /// <summary>
        /// 上传Rma 处理文档
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult UploadRmaHandleFile(HttpPostedFileBase file)
        {
            var FailResult = new { Result = "FAIL" };
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    FileAttatchData data = JsonConvert.DeserializeObject<FileAttatchData>(Request.Params["fileAttachData"]);
                    //待加入验证文件名称逻辑:
                    if (data == null) return Json(FailResult);
                    string extensionName = System.IO.Path.GetExtension(file.FileName);
                    string fileName = String.Format("{0}-{1}{2}", data.RmaId, data.RmaIdNumber, extensionName);
                    string fullFileName = Path.Combine(this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.RmaHandleDataFile), fileName);
                    file.DeleteExistFile(fullFileName).SaveAs(fullFileName);
                    return Json(new { Result = "OK", FullFileName = fullFileName, FileName = fileName });
                }
            }
            return Json(FailResult);
        }
        /// <summary>
        /// 打印导出EXCEL数据模板
        /// </summary>
        /// <param name="rmaId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult PrintDetailsDatas(string rmaId)
        {
            //Excel
            DownLoadFileModel dlfm = RmaService.RmaManager.InspecitonManageProcessor.GetPrintDatialDataDLFM(SiteRootPath, rmaId);
            return this.DownLoadFile(dlfm);
        }
        #endregion

        #region RmaReportQuery

        [NoAuthenCheck]
        public ActionResult RmaReportQuery()
        {
            return View();
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        [HttpPost]
        [NoAuthenCheck]
        public ContentResult GetRmaDatas(string dateFrom, string dateTo)
        {

            var rmaInitiateDatas = RmaService.RmaManager.RmaReportBuilding.GetInitiateDatas(dateFrom, dateTo);

            return DateJsonResult(rmaInitiateDatas);
        }
        #endregion
    }

    #region view model
    /// <summary>
    /// 上传文件附件数据模型
    /// </summary>
    public class FileAttatchData
    {
        public string RmaId { get; set; }

        public string RmaIdNumber { get; set; }
    }
    #endregion
}
