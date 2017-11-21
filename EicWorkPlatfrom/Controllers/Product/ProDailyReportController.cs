using System;
using System.Web.Mvc;
using System.Collections.Generic;
using Lm.Eic.App.DomainModel.Bpm.Pms.NewDailyReport;
using Lm.Eic.App.Business.Bmp.Pms.NewDailyReport;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.App.Business.Bmp.Pms.DailyReport;
using System.IO;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;
using System.Web;
using Lm.Eic.App.Erp.Bussiness.QuantityManage;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;

namespace EicWorkPlatfrom.Controllers.Product
{
    public class ProDailyReportController : EicBaseController
    {
        //
        // GET: /DailyReport/

        public ActionResult Index()
        {
            return View();
        }

        #region Date Report Hours Set method
        public ActionResult DReportHoursSet()
        {
            return View();
        }
        #endregion

        #region DReportBadReasonSet 不良原因配置
        public ActionResult DReportBadReasonSet()
        {
            return View();
        }
        #endregion

        #region DReportUnproductionSet 非生产原因配置
        public ActionResult DReportUnproductionSet()
        {
            return View();
        }

        [NoAuthenCheck]
        public JsonResult LoadUnProductionConfigDicData(string department, string aboutCategory)
        {
            var modules = DailyProductionReportService.ProductionConfigManager.DailyProductionCodeConfig.GetProductionDictiotry(aboutCategory, department);
            return Json(modules, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult SaveUnProductionConfigDicData(ProductionCodeConfigModel model, ProductionCodeConfigModel oldModel, string opType)
        {
            var result = DailyProductionReportService.ProductionConfigManager.DailyProductionCodeConfig.Store(model, oldModel, opType);
            return Json(result);
        }
        #endregion

        #region report Flow Set set method  生产工艺录入
        public ActionResult DReportFlowSet()
        {
            return View();
        }

        /// <summary>
        /// 获取产品工艺流程列表 GetProductionFlowDatas
        /// </summary>
        /// <param name="department"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetProductionFlowDatas(QueryDailyProductReportDto queryDto)
        {
            //工单没有用到
            //用品名得到多处数据 把数据转化为 ProductsFlowOverModel
           var datas = DailyProductionReportService.ProductionConfigManager.ProductionFlowSet.GetProductFlowInfoBy(queryDto);
           return Json(datas, JsonRequestBehavior.AllowGet);
           // return DateJsonResult(datas);
        }

        /// <summary>
        /// 查找包含品名数据
        /// </summary>
        /// <param name="department"></param>
        /// <param name="likeProductName">包函的品名</param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetProductFlowListBy(string department, string likeProductName)
        {
            var productFlowOverviews = DailyProductionReportService.ProductionConfigManager.ProductionFlowSet.GetFlowShowSummaryInfosBy(department, likeProductName);
            return Json(productFlowOverviews, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 批量保存产品工艺流程数据
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult StoreProductFlowDatas(List<StandardProductionFlowModel> entities)
        {
            var datas = DailyProductionReportService.ProductionConfigManager.ProductionFlowSet.StoreModelList(entities);
            return Json(datas);
        }
        /// <summary>
        /// 保存产品工艺流程数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult StoreFlowData(StandardProductionFlowModel entity)
        {
            var datasResult = DailyProductionReportService.ProductionConfigManager.ProductionFlowSet.StoreProductFlow(entity);
            return Json(datasResult);
        }


        [NoAuthenCheck]
        public JsonResult ImmediatelyDeleteProcessesFlow(string productName, string processesName)
        {
            var datasResult = DailyProductionReportService.ProductionConfigManager.ProductionFlowSet.DeleteSingleProcessesFlow(productName, processesName);
            return Json(datasResult);
        }
        /// <summary>
        /// 获取产品工艺初始化数据
        /// searchMode:0查询全部；1按名称模糊查询
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetProductionFlowOverview(string department, string productName, int searchMode)
        {

            if (searchMode == 0)
            {
                var ProductFlowdatas = DailyProductionReportService.ProductionConfigManager.ProductionFlowSet.GetFlowShowSummaryInfosBy(department);
                return Json(ProductFlowdatas, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var ProductFlowdatas = DailyProductionReportService.ProductionConfigManager.ProductionFlowSet.GetFlowShowSummaryInfosBy(department, productName);
                return Json(ProductFlowdatas, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 载入产品工艺流程模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult LoadProductFlowTemplateFile()
        {
            ///路径下载
            string filePath = SiteRootPath + @"FileLibrary\DailyProductionReport\ProductFlow\日报工序模板.xls";
            string fileName = "日报工序模板.xls";
            var dlfm = DailyProductionReportService.ProductionConfigManager.ProductionFlowSet.GetProductFlowTemplate(SiteRootPath, filePath, fileName);
            return this.DownLoadFile(dlfm);

        }
        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult ImportProductFlowDatas(HttpPostedFileBase file)
        {
            List<StandardProductionFlowModel> datas = null;
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    ///待加入验证文件名称逻辑:
                    string fileName = Path.Combine(this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.Temp), file.FileName);
                    file.SaveAs(fileName);
                    datas = DailyProductionReportService.ProductionConfigManager.ProductionFlowSet.ImportProductFlowListBy(fileName);
                    if (datas != null && datas.Count > 0)
                    //批量保存数据
                    { var opResult = DailyProductionReportService.ProductionConfigManager.ProductionFlowSet.StoreModelList(datas); }
                    System.IO.File.Delete(fileName);
                }
            }

            return Json(datas, JsonRequestBehavior.AllowGet);

        }
        #endregion


        #region DRProductDispatching method 生产订单分派
        /// <summary>
        /// 生产工单管理
        /// </summary>
        /// <returns></returns>
        public ActionResult DRProductOrderDispatching()
        {
            return View();
        }
        /// <summary>
        /// 得到工单分配信息
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetOrderDispatchInfoDatas(string queryString, int  opType)
        {
            switch (opType)
            {
                case 1:
                    var erpOrderDatas = DailyProductionReportService.ProductionConfigManager.ProductOrderDispatch.GetNeedDispatchOrderBy(queryString);
                    var virtualOrderDatas = DailyProductionReportService.ProductionConfigManager.ProductOrderDispatch.GetVirtualOrderDataBy(queryString);
                    var datas = new { erpOrderDatas, virtualOrderDatas };
                    return DateJsonResult(datas);
                case 2:
                    var data = DailyProductionReportService.ProductionConfigManager.ProductOrderDispatch.GetQueryOrderBy(queryString);
                    return DateJsonResult(data);
                default: 
                    return DateJsonResult("操作类型不对");
            }
        }
        /// <summary>
        /// 存储数据 StoreOrderDispatchDatas
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult StoreOrderDispatchDatas(ProductOrderDispatchModel entity)
        {
            var opDatasResult = DailyProductionReportService.ProductionConfigManager.ProductOrderDispatch.StoreOrderDispatchData(entity);
            return Json(opDatasResult);
        }
        #endregion


        #region daily report  input method 日报录入
        /// <summary>
        /// 日报录入
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult DReportInput()
        {
            return View();
        }
        /// <summary>
        ///  得到在制生产工单
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetInProductionOrderDatas(string department)
        {
            var datas = DailyProductionReportService.ProductionConfigManager.ProductOrderDispatch.GetHaveDispatchOrderBy(department, "已分配");
            return DateJsonResult(datas);
        }
        /// <summary>
        /// 得到订单所有工艺的统计数 department, productName, orderId
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetProductionFlowCountDatas(string department, string orderId, string productName)
        {
            var datas = DailyProductionReportService.ProductionConfigManager.DailyReport.GetProductionFlowCountDatas(department, orderId, productName);
            return DateJsonResult(datas);
        }
        /// <summary>
        /// 由工号得到最后一次录入信息
        /// </summary>
        /// <param name="wokerId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetWorkerDailyInfoBy(string workerId)
        {
            var datas = DailyProductionReportService.ProductionConfigManager.DailyReport.GetWorkerDailyDatasBy(workerId);

            return DateJsonResult(datas);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="wokerId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult getProcessesNameDailyDataBy(DateTime date, string orderId, string processesName)
        {
            var datas = DailyProductionReportService.ProductionConfigManager.DailyReport.GetDailyDataBy(date, orderId, processesName);
            return DateJsonResult(datas);
        }
        /// <summary>
        /// 得到数部门机台信息
        /// </summary>
        /// <param name="Department"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetMachineInfoDataBy(string Department)
        {
            var datas = "";
            return DateJsonResult(datas);
        }

        /// <summary>
        /// 得到数部门机台信息
        /// </summary>
        /// <param name="Department"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetNoProductSeasonBy(string Department, string NoProductId)
        {
            var datas = "";
            return DateJsonResult(datas);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult SaveDailyReportData(DailyProductionReportModel entity)
        {
            var datasResult = DailyProductionReportService.ProductionConfigManager.DailyReport.StoreDailyReport(entity);
            return Json(datasResult);
        }
        [NoAuthenCheck]
        public JsonResult SaveGroupDailyReportData(DailyProductionReportModel entity, List<UserInfoVm> groupUserInfos)
        {
            var dataslist = new List<DailyProductionReportModel>();
            var opResult = DailyProductionReportService.ProductionConfigManager.DailyReport.StoreDailyReport(entity, groupUserInfos, out dataslist);
            var datasResult = new { opResult, dataslist };
            return Json(datasResult);
        }
        /// <summary>
        /// 批量存储数量
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult SaveMachineDailyReportDatas(List<DailyProductionReportModel> entitys)
        {
            var opResult = DailyProductionReportService.ProductionConfigManager.DailyReport.StoreDailyReport(entitys);
            var datasResult = new { opResult, entitys };
            return Json(datasResult);
        }
        /// <summary>
        /// 处理多项数据 DisposeMultitermDailyReportdDatas
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="inputSign"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult DisposeMultitermDailyReportdDatas(List<DailyProductionReportModel> entitys, string inputSign)
        {
            var datas = DailyProductionReportService.ProductionConfigManager.DailyReport.DisposeMultitermDailyReportdDatas(entitys, inputSign);
            return Json(datas);
        }

        #endregion
    }
}