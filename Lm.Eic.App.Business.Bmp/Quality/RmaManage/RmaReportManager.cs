using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.App.Erp.Bussiness.CopManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.RmaManage
{
    /// <summary>
    /// Rma进度状态
    /// </summary>
    public class RmaHandleStatus
    {
        public const string InitiateStatus = "未结案";
        public const string BusinessStatust = "业务处理中";
        public const string InspecitonStatus = "品保处理中";
        public const string FinishStatus = "已结案";
    }
    /// <summary>
    /// Ram初始数据处理器
    /// </summary>
    public class RmaReportInitiateProcessor
    {

        /// <summary>
        /// 自动生成RmaId编号
        /// </summary>
        /// <returns></returns>
        public string AutoBuildingRmaId()
        {
            return RmaCurdFactory.RmaReportInitiate.BuildingNewRmaId();
        }
        /// <summary>
        /// 存储初始Rma表单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreRamReortInitiate(RmaReportInitiateModel model)
        {
            //if (model.OpSign == OpMode.Add)
            //    return RmaCurdFactory.RmaReportInitiate.AddModel(model);


            //if (model == null) return null;

            //if (RmaCurdFactory.RmaReportInitiate.IsExist(model.RmaId))
            //{
            //    model.OpSign = OpMode.Edit;

            //}
            //else
            //{
            //    model.OpSign = OpMode.Add;

            //}

            return RmaCurdFactory.RmaReportInitiate.Store(model);
        }



        /// <summary>
        /// 得到初始Rma表单
        /// </summary>
        /// <param name="rmaId"></param>
        /// <returns></returns>
        public List<RmaReportInitiateModel> GetInitiateDatas(string rmaId)
        {
            return RmaCurdFactory.RmaReportInitiate.GetInitiateDatas(rmaId);
        }
        /// <summary>
        /// 通过年月份得到RamId
        /// </summary>
        /// <param name="yearMonth">yyyyMM</param>
        /// <returns></returns>
        public List<RmaReportInitiateModel> getRmaReportInitiateDatasBy(string yearMonth)
        {
            try
            {
                if (yearMonth.Length != 6) return null;
                //201701
                string year = yearMonth.Substring(0, 4);
                string month = yearMonth.Substring(4, 2);
                return RmaCurdFactory.RmaReportInitiate.GetRmaReportInitiateDatas(year, month);
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
            return RmaCurdFactory.RmaBussesDescription.GetRmaBussesDescriptionDatasBy(rmaId);
        }
        /// <summary>
        /// 通过退料单或换货单得到相应的物料信息
        /// </summary>
        /// <param name="returnHandleOrder">退货单</param>
        /// <returns></returns>
        public List<RmaRetrunOrderInfoModel> GetErpBussesInfoDatasBy(string returnHandleOrder)
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
                throw new Exception(ex.InnerException.Message);
            }

        }
        /// <summary>
        /// 存储
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreRmaBusinessDescriptionData(RmaBusinessDescriptionModel model)
        {
            ///2.如果存在 操作符为 Edit
            ///3.那些字段不能为空  ProductsShipDate 不能为空 不能为“0001-01-01”
            ///4.如果存储成功，改变初始Rma单状态
            try
            {
                OpResult result = OpResult.SetResult("存储数据表");
                if (model == null) return OpResult.SetResult("存储的数据不能为空");
                if (model.ProductsShipDate == DateTime.MinValue || model.ProductsShipDate == null) return OpResult.SetResult("存储的完成日期不对");
                result = RmaCurdFactory.RmaBussesDescription.Store(model, true);
                if (result.Result)
                    RmaCurdFactory.RmaReportInitiate.UpdateInitiateRmaIdStatus(model.RmaId, RmaHandleStatus.InspecitonStatus);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

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
            return RmaCurdFactory.RmaInspectionManage.GetInspectionManageDatasBy(rmaId);
        }

        /// <summary>
        /// 存储品保处理据的
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreInspectionManageData(RmaInspectionManageModel model)
        {
            var result = RmaCurdFactory.RmaInspectionManage.Store(model, true);
            if (result.Result)
            {
                RmaCurdFactory.RmaReportInitiate.UpdateInitiateRmaIdStatus(model.RmaId, RmaHandleStatus.FinishStatus);
                RmaCurdFactory.RmaBussesDescription.UpdateBussesDescriptionStatus(model.RmaId, model.ProductId, RmaHandleStatus.FinishStatus);
            }
            return result;
        }
    }
}
