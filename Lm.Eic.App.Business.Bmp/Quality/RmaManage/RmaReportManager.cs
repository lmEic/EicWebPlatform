using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.App.Erp.Bussiness.CopManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeExcelHanlder.ExcelParse;

namespace Lm.Eic.App.Business.Bmp.Quality.RmaManage
{
    /// <summary>
    /// Rma进度状态
    /// </summary>
    public class RmaHandleStatus
    {
        /// <summary>
        /// 初始建立RMA单时为空单号
        /// </summary>
        public const string InitiateStatus = "空单号";
        /// <summary>
        /// RMA 单只有负数
        /// </summary>
        public const string BusinessMinusStatus = "已补货";
        /// <summary>
        /// RMA 单只有正数
        /// </summary>
        public const string BusinessPlusStatus = "已收货";
        /// <summary>
        /// RMA 单有正负数 业务处理完成转化到品保处理中
        /// </summary>
        public const string InspecitonStatus = "品保处理中";
        /// <summary>
        /// RMA 单 品处理完成 就结案
        /// </summary>
        public const string FinishStatus = "已结案";
    }
    /// <summary>
    /// Ram表单创建器
    /// </summary>
    public class RmaReportCreator
    {
        /// <summary>
        /// 自动生成RmaId编号
        /// </summary>
        /// <returns></returns>
        public string AutoBuildingRmaId()
        {
            int nowYaer = DateTime.Now.Year;
            string maxRmaId = RmaCrudFactory.RmaReportInitiate.CountNowYaerMonthRmaIdNumber(nowYaer);
            int count = Convert.ToInt16(maxRmaId.Substring(maxRmaId.Length - 3, 3)) + 1;
            return "R" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + count.ToString("000");
        }

        /// <summary>
        /// 存储初始Rma表单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreRamReortInitiate(RmaReportInitiateModel model)
        {
            return RmaCrudFactory.RmaReportInitiate.Store(model, true);
        }
        /// <summary>
        /// 得到初始Rma表单
        /// </summary>
        /// <param name="rmaId"></param>
        /// <returns></returns>
        public List<RmaReportInitiateModel> GetInitiateDatas(string rmaId)
        {
            return RmaCrudFactory.RmaReportInitiate.GetInitiateDatas(rmaId);

        }
        public List<RmaReportInitiateModel> GetInitiateDatas(string formDate, string toDate)
        {
            try
            {
                if (formDate.Length != 6 && toDate.Length != 0) return null;
                if (Convert.ToInt32(formDate) > Convert.ToInt32(toDate)) return null;
                int formDateYear = Convert.ToInt32(formDate.Substring(0, 4));
                int formDateMonth = Convert.ToInt32(formDate.Substring(4, 2));
                int toDateYear = Convert.ToInt32(toDate.Substring(0, 4));
                int toDateMonth = Convert.ToInt32(toDate.Substring(4, 2));
                return RmaCrudFactory.RmaReportInitiate.GetInitiateDatasBy(formDateYear, formDateMonth, toDateYear, toDateMonth);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// 通过年月份得到RamId
        /// </summary>
        /// <param name="yearMonth">yyyyMM</param>
        /// <returns></returns>
        public List<RmaReportInitiateModel> GetRmaReportInitiateDatasBy(string yearMonth)
        {
            try
            {
                if (yearMonth.Length != 6) return null;
                //201701
                int year = Convert.ToInt16(yearMonth.Substring(0, 4));
                int month = Convert.ToInt16(yearMonth.Substring(4, 2));
                return RmaCrudFactory.RmaReportInitiate.GetRmaReportInitiateDatas(year, month);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    /// <summary>
    /// Rma单业务部门操作处理器
    /// </summary>
    public class RmaBusinessDescriptionProcessor
    {
        /// <summary>
        /// 通过RmaId，得到业务处理数据
        /// </summary>
        /// <param name="RmaId"></param>
        /// <returns></returns>
        public List<RmaBusinessDescriptionModel> GetRmaBusinessDescriptionDatasBy(string rmaId)
        {
            if (rmaId != null && rmaId == string.Empty) return null;
            return RmaCrudFactory.RmaBussesDescription.GetRmaBussesDescriptionDatasBy(rmaId);
        }
        /// <summary>
        /// 通过退料单或换货单得到相应的物料信息
        /// </summary>
        /// <param name="returnHandleOrder">退货单</param>
        /// <returns></returns>
        public List<RmaRetrunOrderInfoModel> GetErpBusinessInfoDatasBy(string returnHandleOrder)
        {
            try
            {
                List<RmaRetrunOrderInfoModel> returnDatas = new List<RmaRetrunOrderInfoModel>();
                RmaRetrunOrderInfoModel mdl = null;
                //从ERP中得到相应的数据
                var ErpReturnOrderDatas = CopService.ReturnOrderManage.GetCopReturnOrderInfoBy(returnHandleOrder);
                if (ErpReturnOrderDatas == null || ErpReturnOrderDatas.Count <= 0) return returnDatas;
                ErpReturnOrderDatas.ForEach(m =>
                {
                    mdl = new RmaRetrunOrderInfoModel()
                    {
                        ReturnHandleOrder = m.OrderId,
                        CustomerId = m.CustomerId,
                        CustomerName = m.CustomerShortName,
                        ProductId = m.ProductID,
                        ProductName = m.ProductName,
                        ProductSpec = m.ProductSpecify,
                        ProductCount = m.ProductNumber
                    };
                    returnDatas.Add(mdl);
                });
                return returnDatas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// 存储RMA业务处理数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreRmaBusinessDescriptionData(RmaBusinessDescriptionModel model)
        {
            try
            {
                if (model.ProductsShipDates == string.Empty) return OpResult.SetErrorResult("存储的完成日期不对");
                var result = RmaCrudFactory.RmaBussesDescription.Store(model, true);
                if (result.Result && model.OpSign == OpMode.Add)
                {
                    string rmaHandleStatus = GetBusinessStatus(model.RmaId);
                    if (rmaHandleStatus != string.Empty)
                        RmaCrudFactory.RmaReportInitiate.UpdateHandleStatus(model.RmaId, rmaHandleStatus);
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.ExOpResult();
            }
        }
        private string GetBusinessStatus(string rmaId)
        {
            try
            {
                if (string.IsNullOrEmpty(rmaId)) return string.Empty;
                var getRmaBusinessDescriptionData = RmaCrudFactory.RmaBussesDescription.GetRmaBussesDescriptionDatasBy(rmaId);
                if (getRmaBusinessDescriptionData == null || getRmaBusinessDescriptionData.Count == 0)
                    return string.Empty;
                List<double> minusNumberList = new List<double>();
                List<double> plusNumberList = new List<double>();
                getRmaBusinessDescriptionData.ForEach(e =>
                {
                    if (e.ProductCount < 0)
                    { minusNumberList.Add(e.ProductCount); }
                    else plusNumberList.Add(e.ProductCount);
                });
                if (minusNumberList.Count > 0 && plusNumberList.Count == 0)
                    ///只有负数时
                    return RmaHandleStatus.BusinessMinusStatus;
                if (minusNumberList.Count == 0 && plusNumberList.Count > 0)
                    ///只有正数时
                    return RmaHandleStatus.BusinessPlusStatus;
                if (minusNumberList.Count > 0 && plusNumberList.Count > 0)
                    ///有正数又有负数
                    return RmaHandleStatus.InspecitonStatus;
                return string.Empty;
            }
            catch (Exception ex)
            { ex.ExOpResult(); return string.Empty; }
        }
    }
    /// <summary>
    /// Rma单 品保部操作处理器
    /// </summary>
    public class RmaInspecitonManageProcessor
    {
        /// <summary>
        /// 通过RmaId，得到品保处理数据
        /// </summary>
        /// <param name="rmaId"></param>
        /// <returns></returns>
        public List<RmaInspectionManageModel> GetDatasBy(string rmaId)
        {
            if (rmaId != null && rmaId == string.Empty) return null;
            return RmaCrudFactory.RmaInspectionManage.GetInspectionManageDatasBy(rmaId);
        }

        /// <summary>
        /// 存储品保处理据的
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreInspectionManageData(RmaInspectionManageModel model)
        {
            var result = RmaCrudFactory.RmaInspectionManage.Store(model, true);
            if (result.Result && model.OpSign == OpMode.Add)
            {
                RmaCrudFactory.RmaReportInitiate.UpdateHandleStatus(model.RmaId, RmaHandleStatus.InspecitonStatus);
                if (model.RmaBussesesNumberStr.Contains(",") && !string.IsNullOrEmpty(model.RmaBussesesNumberStr))
                {
                    var number = model.RmaBussesesNumberStr.Split(',');
                    if (number.Count() > 0)
                        foreach (var i in number)
                        {
                            int bussesIndexNumber = Convert.ToInt32(i);
                            RmaCrudFactory.RmaBussesDescription.UpdateHandleStatus(model.RmaId, bussesIndexNumber, RmaHandleStatus.InspecitonStatus);
                        }
                }
                else
                {
                    RmaCrudFactory.RmaBussesDescription.UpdateHandleStatus(model.RmaId, Convert.ToInt32(model.RmaBussesesNumberStr), RmaHandleStatus.InspecitonStatus);
                }
                RmaCrudFactory.RmaInspectionManage.UpdateHandleStatus(model.ParameterKey);
            }
            HandleFinishRmaId(model.RmaId);
            return result;
        }

        /// <summary>
        /// 处理完成的RMD单
        /// </summary>
        /// <param name="rmaId"></param>
        private void HandleFinishRmaId(string rmaId)
        {
            var updateResult = RmaCrudFactory.RmaBussesDescription.UpdateHandleStatus(rmaId);
            if (updateResult.Result)
            {
                updateResult = RmaCrudFactory.RmaInspectionManage.UpdateFinishStatus(rmaId);
                if (updateResult.Result)
                    updateResult = RmaCrudFactory.RmaReportInitiate.UpdateHandleStatus(rmaId, RmaHandleStatus.FinishStatus);
            }
        }


        public DownLoadFileModel GetPrintDatialDataDLFM(string siteRootPath, string rmaId)
        {
            //DownLoadFileModel dlfm = new DownLoadFileModel();
            //string filePath = "";
            //string certificateFileName = "RMASD1.1-11-01.xls";
            var modelDatas = RmaCrudFactory.RmaReportInitiate.GetInitiateDatas("R1707067").FirstOrDefault();
            //if (model == null)
            //    return dlfm.Default();
            // RmaInspectionManageModel 

            ExcelImportService mm = new ExcelImportService(@"E:\RMASD1.1-11-01.xls", @"E:\121.xml", @"E:\4567.xml");
            //var qqq = mm.Import<RmaReportInitiateModel>();
            System.IO.MemoryStream stream = mm.GetInseerFixModel<RmaReportInitiateModel>(modelDatas);
            return stream.CreateDownLoadExcelFileModel("Rma模板");

        }
    }
}
