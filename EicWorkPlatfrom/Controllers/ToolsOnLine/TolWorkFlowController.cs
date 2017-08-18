using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.WorkFlow.GeneralForm;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Lm.Eic.App.DomainModel.Bpm.WorkFlow.GeneralForm;
using Lm.Eic.Framework.ProductMaster.Model.CommonManage;

namespace EicWorkPlatfrom.Controllers
{
    public class TolWorkFlowController : EicBaseController
    {
        //
        // GET: /TolWorkFlow/

        public ActionResult Index()
        {
            return View();
        }


        #region WFInternalContactForm
        public ActionResult WFInternalContactForm()
        {
            return View();
        }
        [NoAuthenCheck]
        public JsonResult GetWorkerMails(string department)
        {
            var datas = GeneralFormService.InternalContactFormManager.GetWorkerMails(department);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult GetInternalContactFormId(string department)
        {
            string formId = GeneralFormService.InternalContactFormManager.AutoCreateFormId(department);
            return Json(formId, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 上传内部联络单附件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult UploadInternalContactFormAttachFile(HttpPostedFileBase file)
        {
            FormAttachFileManageModel dto = ConvertFormDataToTEntity<FormAttachFileManageModel>("attachFileDto");
            string filePath = this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.ElectronicForm, dto.ModuleName);
            string customizeFileName = GeneralFormService.InternalContactFormManager.AttachFileHandler.SetAttachFileName(dto.ModuleName, dto.FormId);
            UploadFileResult result = SaveFileToServer(file, filePath, customizeFileName);
            if (result.Result)
            {
                dto.DocumentFilePath = filePath;
                dto.FileName = customizeFileName;
                GeneralFormService.InternalContactFormManager.AttachFileHandler.StoreOnlyOneTime(dto);
            }
            return Json(result);
        }

        [NoAuthenCheck]
        public JsonResult CreateInternalForm(InternalContactFormModel entity)
        {
            var opresult = GeneralFormService.InternalContactFormManager.CreateInternalForm(entity);
            return Json(opresult);
        }
        #endregion

    }
}
