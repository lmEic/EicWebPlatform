using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.Quality.Qua8DReportManage;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.Business.Bmp.Quality.InspectionManage;
using Lm.Eic.Framework.ProductMaster.Model.CommonManage;
using Lm.Eic.App.Business.Bmp.WorkFlow.GeneralForm;
using Lm.Eic.Framework.ProductMaster.Business.Config;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using System.IO;
using Lm.Eic.Uti.Common.YleeOOMapper;

namespace EicWorkPlatfrom.Controllers
{
    public class Qua8DManageController : EicBaseController
    {
        //
        // GET: /Qua8DManage/

        public ActionResult Index()
        {
            return View();
        }

        #region Create8DForm
        [NoAuthenCheck]
        public ActionResult Create8DForm()
        {
            return View();
        }
        /// <summary>
        /// 查询模式
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetQueryDatas(int searchModel, string orderId)
        {
            switch(searchModel)
            {
                case 1:
                    return DateJsonResult(InspectionService.DataGatherManager.IqcDataGather.MasterDatasGather.GetIqcMasterContainDatasBy(orderId));
                case 2:
                    var datas = InspectionService.DataGatherManager.FqcDataGather.MasterDatasGather.GetFqcMasterOrderIdDatasBy(orderId);
                    return DateJsonResult(datas);
                case 3:
                    return DateJsonResult(InspectionService.DataGatherManager.IqcDataGather.MasterDatasGather.GetIqcMasterContainDatasBy(orderId));
                 default:
                    return DateJsonResult(InspectionService.DataGatherManager.IqcDataGather.MasterDatasGather.GetIqcMasterContainDatasBy(orderId));
            }
        }

        /// <summary>
        /// 保存 初始8Dp初始数据
        /// </summary>
        /// <param name="initiateData"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult StoreCraet8DInitialData(Qua8DReportMasterModel initialData)
        {
            if (initialData == null) return Json(new OpResult("数据为空", false));
            if (initialData.FileName != null && initialData.FileName.Length > 1)
            {
                initialData.FilePath = Path.Combine(initialData.FilePath, initialData.FileName);
                initialData.FilePath = initialData.FilePath.Replace(this.SiteRootPath, "");
            }
            var result = Qua8DService.Qua8DManager.Qua8DMaster.StoreQua8DMaster(initialData);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 自动生成8D单单号
        /// </summary>
        /// <param name="mmm"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult AutoBuildingReportId(string discoverPosition)
        {
            var data = Qua8DService.Qua8DManager.Qua8DMaster.AutoBuildingReportId(discoverPosition);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 上传文件附件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult UploadCreate8DAttachFile(HttpPostedFileBase file)
        {
            FormAttachFileManageModel dto = ConvertFormDataToTEntity<FormAttachFileManageModel>("attachFileDto");
            string filePath = this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.Qua8DUpAttachFile, dto.ModuleName);
            string customizeFileName = GeneralFormService.InternalContactFormManager.AttachFileHandler.SetAttachFileName(dto.ModuleName, dto.FormId + "-" + "1");
            UploadFileResult result = SaveFileToServer(file, filePath, customizeFileName);
            if (result.Result)
            {
                dto.DocumentFilePath = filePath;
                dto.FileName = customizeFileName;
                result.PreviewFileName = (result.DocumentFilePath + "\\" + result.FileName).ToPhotoByte().ToBase64Url();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Handle8DFolw
        [NoAuthenCheck]
        public ActionResult Handle8DFolw()
        {
            return View();
        }
        /// <summary>
        /// 得到Rma单的数据
        /// </summary>
        /// <param name="rmaId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult ShowQua8DDetailDatas(string reportId)
        {
            var ShowQua8DMasterData = Qua8DService.Qua8DManager.Qua8DMaster.Show8DReportMasterInfo(reportId);
            if (ShowQua8DMasterData == null) return null;
            var Stepdatas = Qua8DService.Qua8DManager.Qua8DDatail.ShowQua8DDetailDatasBy(ShowQua8DMasterData.DiscoverPosition);
            var datas = new { Stepdatas, ShowQua8DMasterData };
            return DateJsonResult(datas);
        }

        /// <summary>
        /// 通过单号 序号得到设置模板
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="stepId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetQua8DReportStepData(string reportId, ShowStepViewModel step)
        {
            var stepData = Qua8DService.Qua8DManager.Qua8DDatail.Get8DStepDetailDatasBy(reportId, step);
            
            return Json(stepData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="handelData"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult SaveQua8DHandleDatas(Qua8DReportDetailModel handelData)
        {
            if (handelData == null) return Json(new OpResult("数据为空", false));
            if (handelData.FileName != null && handelData.FileName.Length > 1)
            {
                handelData.FilePath = Path.Combine(handelData.FilePath, handelData.FileName);
                handelData.FilePath = handelData.FilePath.Replace(this.SiteRootPath, "");
            }
            var data = Qua8DService.Qua8DManager.Qua8DDatail.StoreQua8DHandleDatas(handelData);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 得到8D处理信息Model
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult Get8DStepInfo(string discoverPosition)
        {
            var datas = Qua8DService.Qua8DManager.Qua8DDatail.Get8DStepInfo(discoverPosition);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 上传文件附件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult Upload8DAttachFile(HttpPostedFileBase file, string step)
        {
            FormAttachFileManageModel dto = ConvertFormDataToTEntity<FormAttachFileManageModel>("attachFileDto");
            string filePath = this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.Qua8DUpAttachFile, dto.ModuleName);
            string customizeFileName = GeneralFormService.InternalContactFormManager.AttachFileHandler.SetAttachFileName(dto.ModuleName, dto.FormId + "-" + step);
            UploadFileResult result = SaveFileToServer(file, filePath, customizeFileName);
            if (result.Result)
            {
                dto.DocumentFilePath = filePath;
                dto.FileName = customizeFileName;
                string imgFilePath = (result.DocumentFilePath + "\\" + result.FileName);
                var imgBytes = imgFilePath.ToPhotoByte();
                result.PreviewFileName = imgBytes.ToBase64Url();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #region Close8DForm
        [NoAuthenCheck]
        public ActionResult Close8DForm()
        {
            return View();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="searchFrom"></param>
        /// <param name="searchTo"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult Query8DDatas(string searchFrom, string searchTo)
        {
            var datas = Qua8DService.Qua8DManager.Qua8DFileStroe.Query8DData(searchFrom, searchTo);
            return DateJsonResult(datas);
        }
        /// <summary>
        /// 得到详细信息
        /// </summary>
        /// <param name="reporetId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult Query8DDetailDatas(string reportId)
        {
            var datas = Qua8DService.Qua8DManager.Qua8DDatail.GetQua8DDetailDatasBy(reportId);
            return DateJsonResult(datas);
        }
        /// <summary>
        /// 归档处理
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult ChangeReportIdStatus(string reportId, string status, string fileName, string filePath)
        {
            var dataResult = Qua8DService.Qua8DManager.Qua8DFileStroe.ChangeReportIdStatus(reportId, status, fileName, filePath);
            return Json(dataResult, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 上传文件归档
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult Upload8DGatherFile(HttpPostedFileBase file)
        {
            FormAttachFileManageModel dto = ConvertFormDataToTEntity<FormAttachFileManageModel>("attachFileDto");
            string filePath = this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.Qua8DUpAttachFile, dto.ModuleName);
            string customizeFileName = GeneralFormService.InternalContactFormManager.AttachFileHandler.SetAttachFileName(dto.ModuleName, dto.FormId + "-" + "结案");
            UploadFileResult result = SaveFileToServer(file, filePath, customizeFileName);
            if (result.Result)
            {
                dto.DocumentFilePath = filePath;
                dto.FileName = customizeFileName;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 下载8D归档文件
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="stepId"></param>
        /// <param name="fileProperty"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult Load8dDownLoadDarchivingFile(string reportId, int stepId, string fileProperty)
        {
           // DownLoadFileModel dlfm = InspectionService.DataGatherManager.FqcDataGather.GetFqcDatasDownLoadFileModel(SiteRootPath, orderId, orderIdNumber, inspectionItem);
            DownLoadFileModel dlfm = null;
            return this.DownLoadFile(dlfm);
        }
        #endregion



    }
}
