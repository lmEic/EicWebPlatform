using Lm.Eic.App.Business.Bmp.Purchase;
using Lm.Eic.App.DomainModel.Bpm.Purchase;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;

namespace EicWorkPlatfrom.Controllers.Purchase
{
    public class PurSupplierManageController : EicBaseController
    {
        //
        // GET: /PurSupplierManage/
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 供应商录入
        /// </summary>
        /// <returns></returns>
        public ActionResult PurSupplierInput()
        {
            return View();
        }
        #region SupplierArchiveOverview 供应商档案总览
        public ActionResult SupplierArchiveOverview()
        {
            return View();
        }
        #endregion


        #region PurQualifiedSupplier 供应商证书管理
        /// <summary>
        /// 供应商证书管理
        /// </summary>
        /// <returns></returns>
        ///
        [NoAuthenCheck]
        public ActionResult SupplierCertificateManage()
        {
            return View();
        }
        /// <summary>
        /// 获取合格供应商列表
        /// </summary>
        /// <param name="yearStr">年份</param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetPurQualifiedSupplierListBy(string yearMonth)
        {
            var datas = PurchaseService.PurSupplierManager.CertificateManager.GetQualifiedSupplierList(yearMonth);
            TempData["QualifiedSupplierDatas"] = datas;
            return DateJsonResult(datas);

        }
        /// <summary>
        /// 导出合格供应商EXCEl表清册
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult CreateQualifiedSupplierInfoList()
        {
            var datas = TempData["QualifiedSupplierDatas"] as List<SuppliersSumInfoVM>;
            //Excel
            var dlfm = PurchaseService.PurSupplierManager.CertificateManager.BuildQualifiedSupplierInfoList(datas);
            return this.DownLoadFile(dlfm);
        }
        [NoAuthenCheck]
        public FileResult LoadQualifiedCertificateFile(string suppliserId, string eligibleCertificate)
        {
            //Excel
            DownLoadFileModel dlfm = PurchaseService.PurSupplierManager.CertificateManager.GetSupQuaCertificateDLFM(SiteRootPath, suppliserId, eligibleCertificate);
            return this.DownLoadFile(dlfm);
        }
        /// <summary>
        /// 获取合格供应商信息
        /// </summary>
        /// <param name="supplierId">供应商编号</param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetErpSuppplierInfoBy(string supplierId)
        {
            var datas = PurchaseService.PurSupplierManager.CertificateManager.GetSuppplierInfoBy(supplierId);

            return DateJsonResult(datas);
        }
        /// <summary>
        /// 获取采购供应商用户数据列表
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetPurSupplierDataList(string supplierId, string dataType)
        {
            var datas = 0;
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 上传采购供应商证书文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpPost]
        public JsonResult UploadPurSupplierCertificateFile(HttpPostedFileBase file)
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
                    string fileName = String.Format("{0}{1}{2}", data.SupplierId, data.EligibleCertificate, extensionName);
                    string fullFileName = Path.Combine(this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.PurSupplierCertificate), fileName);
                    file.DeleteExistFile(fullFileName).SaveAs(fullFileName);
                    return Json(new { Result = "OK", FileName = fileName });
                }
            }
            return Json(FailResult);
        }
        /// <summary>
        /// 保存供应商证书信息
        /// </summary>
        /// <param name="certificateDatas"></param>
        /// <returns></returns>
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult StorePurSupplierCertificateInfo(InPutSupplieCertificateInfoModel certificateData)
        {
            var opResult = PurchaseService.PurSupplierManager.CertificateManager.SaveSupplierCertificateData(certificateData, this.SiteRootPath);
            return Json(opResult);
        }

        /// <summary>
        /// 获供应商合格的证书列表
        /// </summary>
        /// <param name="supplierId">供应商Id</param>
        /// <returns></returns>
        /// UploadPurSupplierCertificateFile
        /// StorePurSupplierCertificateInfo
        /// DelPurSupplierCertificateFile
        [NoAuthenCheck]

        public ContentResult GetSupplierQualifiedCertificateListBy(string supplierId)
        {
            var datas = PurchaseService.PurSupplierManager.CertificateManager.GetSupplierQualifiedCertificateListBy(supplierId);
            return DateJsonResult(datas);
        }

        /// <summary>
        /// 删除供应商证书文件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult DelPurSupplierCertificateFile(SupplierQualifiedCertificateModel entity)
        {
            var datas = PurchaseService.PurSupplierManager.CertificateManager.SaveSupplierCertificateData(entity, this.SiteRootPath);
            return Json(datas);
        }
        #endregion

        #region SupplierEvaluationManage 供应商考核登记
        [NoAuthenCheck]
        public ActionResult SupplierEvaluationManage()
        {
            return View();
        }
        /// <summary>
        /// 获取要考核的供应商列表
        /// </summary>
        /// <param name="season"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetAuditSupplierList(string yearSeason)
        {
            var datas = PurchaseService.PurSupplierManager.AuditManager.GetSeasonSupplierList(yearSeason);
            TempData["SupplierSeasonDatas"] = datas;
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 供应商考核导出EXcel
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult CreateSupplierEvaluationToExcel()
        {
            var datas = TempData["SupplierSeasonDatas"] as List<SupplierSeasonAuditModel>;
            ///导出Excel
            var dlfm = PurchaseService.PurSupplierManager.AuditManager.SupplierSeasonDataDLFM(datas);
            return this.DownLoadFile(dlfm);
        }
        /// <summary>
        /// <summary>
        /// 保存供应商季度考核数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// SaveAuditSupplierInfo
        [NoAuthenCheck]

        public JsonResult SaveAuditSupplierInfo(SupplierSeasonAuditModel entity)
        {
            var datas = PurchaseService.PurSupplierManager.AuditManager.SaveAuditSupplierInfo(entity);

            return Json(datas);
        }
        #endregion

        #region SupplierToturManage 供应商辅导管理
        [NoAuthenCheck]
        public ActionResult SupplierToturManage()
        {
            return View();
        }
        /// <summary>
        /// 供应商辅导计划
        /// </summary>
        /// <param name="yearQuarter"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult GetWaittingTourSupplier(string yearQuarter, double limitTotalCheckScore, double limitQualityCheck)
        {
            var datas = PurchaseService.PurSupplierManager.TutorManger.GetWaittingTourSupplier(yearQuarter, limitTotalCheckScore, limitQualityCheck);
            TempData["SupplierTourData"] = datas;
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 供应商辅导数据导出EXcel
        /// </summary>
        /// <returns></returns>
        ///
        [NoAuthenCheck]
        public FileResult CreateSupplierTourToExcel()
        {
            var datas = TempData["SupplierTourData"] as List<SupplierSeasonTutorModel>;

            var dlfm = PurchaseService.PurSupplierManager.TutorManger.DownLoadTourSupplier(datas);

            return this.DownLoadFile(dlfm);
        }
        /// <summary>
        /// 编辑供应商辅导信息模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult EditPurSupplierToturViewTpl()
        {
            return View();
        }
        /// <summary>
        /// 保存供应商辅导信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult SavePurSupTourInfo(SupplierSeasonTutorModel entity)
        {
            var opResult = PurchaseService.PurSupplierManager.TutorManger.SaveSupplierTutorModel(entity);
            return Json(opResult);
        }
        #endregion

        #region SupplierAuditToGrade 供应商考核评分
        public ActionResult SupplierAuditToGrade()
        {
            return View();
        }
        /// <summary>
        /// 编辑供应商考评分数模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult EditPurSupAuditToGradeTpl()
        {
            return View();
        }

        /// <summary>
        /// 获取要评分的供应商信息列表
        /// </summary>
        /// <param name="yearQuarter"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetPurSupGradeInfo(string yearQuarter)
        {

            var datas = PurchaseService.PurSupplierManager.GradeManager.GetPurSupGradeInfoBy(yearQuarter);
            TempData["SupplierGradeInfoData"] = datas;
            return DateJsonResult(datas);
        }
        [NoAuthenCheck]
        public ContentResult GetPurSupGradeInfoList(string supplierId, string yearQuarter)
        {
            var datas = PurchaseService.PurSupplierManager.GradeManager.GetPurSupGradeInfoDataBy(supplierId);
            return DateJsonResult(datas);
        }
        /// <summary>
        /// 评分的供应商信息列表导出EXcel
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult CreateSupplierGradeInfoDataToExcel()
        {
            var datas = TempData["SupplierGradeInfoData"] as List<SupplierSeasonTutorModel>;
            ///Excell
            var dlfm = datas.ToDownLoadExcelFileModel<SupplierSeasonTutorModel>("供应商考评分模板", "供应商考评分数模板清单");
            return this.DownLoadFile(dlfm);
        }
        /// <summary>
        /// 保存供应商评分数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult SavePurSupGradeData(SupplierGradeInfoModel entity)
        {
            var opResult = PurchaseService.PurSupplierManager.GradeManager.SavePurSupGradeData(entity);
            return Json(opResult);
        }
        #endregion
    }
    #region view model
    /// <summary>
    /// 上传文件附件数据模型
    /// </summary>
    public class FileAttatchData
    {
        public string SupplierId { get; set; }

        public string EligibleCertificate { get; set; }
    }
    #endregion
}
