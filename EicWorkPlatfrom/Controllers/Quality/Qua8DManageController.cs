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
        [NoAuthenCheck]
        public ContentResult GetQueryDatas(int searchModel, string orderId)
        {
            var datas = InspectionService.DataGatherManager.IqcDataGather.MasterDatasGather.GetIqcMasterContainDatasBy(orderId);
            return DateJsonResult(datas);
        }

        /// <summary>
        /// 保存 初始8Dp初始数据
        /// </summary>
        /// <param name="initiateData"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult StoreCraet8DInitialData(Qua8DReportMasterModel initialData)
        {
            var result = Qua8DService.Qua8DManager.Qua8DMaster.StoreQua8DMaster(initialData);
            return Json(result);
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
        public JsonResult ShowQua8DDetailDatas(string reportId)
        {
            var ShowQua8DMasterData = Qua8DService.Qua8DManager.Qua8DMaster.Show8DReportMasterInfo(reportId);

            var Stepdatas = Qua8DService.Qua8DManager.Qua8DDatail.ShowQua8DDetailDatasBy(ShowQua8DMasterData.DiscoverPosition);
            var datas = new { Stepdatas, ShowQua8DMasterData };
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 通过单号 序号得到设置模板
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="stepId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetRua8DReportStepData(string reportId, ShowStepViewModel step)
        {
            var data = Qua8DService.Qua8DManager.Qua8DDatail.GetQua8DDetailDatasBy(reportId, step);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="handelData"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult SaveQua8DHandleDatas(Qua8DReportDetailModel handelData)
        {
            var data = Qua8DService.Qua8DManager.Qua8DDatail.StoreQua8DHandleDatas(handelData);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 上传文件附件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult Upload8DHandleFile(HttpPostedFileBase file, int step)
        {
            FormAttachFileManageModel dto = ConvertFormDataToTEntity<FormAttachFileManageModel>("attachFileDto");
            string filePath = this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.Qua8DUpAttachFile, dto.ModuleName);
            string customizeFileName = GeneralFormService.InternalContactFormManager.AttachFileHandler.SetAttachFileName(dto.ModuleName, dto.FormId + "-" + step);
            UploadFileResult result = SaveFileToServer(file, filePath, customizeFileName);
            if (result.Result)
            {
                dto.DocumentFilePath = filePath;
                dto.FileName = customizeFileName;
                GeneralFormService.InternalContactFormManager.AttachFileHandler.StoreOnlyOneTime(dto);
            }
            return Json(result);
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
        #endregion



        #region Close8DForm
        [NoAuthenCheck]
        public ActionResult Close8DForm()
        {
            return View();
        }
        #endregion

    }
}
