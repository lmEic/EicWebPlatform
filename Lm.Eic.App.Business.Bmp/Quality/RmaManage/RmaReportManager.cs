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
    /// Rma常用状态常量
    /// </summary>
    public static class RmaCommomStatus
    {
        public const string InitiateStatus = "未结案";
        public const string HandleStatust = "处理中";
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
            if (model == null) return null;

            if (RmaCurdFactory.RmaReportInitiate.IsExist(model.RmaId))
            {
                model.OpSign = OpMode.Edit;
                ModelSetVaule(model, model.RmaIdStatus);
            }
            else
            {
                model.OpSign = OpMode.Add;
                ModelSetVaule(model, RmaCommomStatus.InitiateStatus);
            }

            return RmaCurdFactory.RmaReportInitiate.Store(model);
        }

        private void ModelSetVaule(RmaReportInitiateModel model, string InitiateStatus)
        {
            if (model.RmaId != null && model.RmaId.Length == 8)
            {
                model.RmaYear = model.RmaId.Substring(1, 2);
                model.RmaMonth = model.RmaId.Substring(3, 2);
            }
            model.RmaIdStatus = InitiateStatus;
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
                return RmaCurdFactory.RmaReportInitiate.getRmaReportInitiateDatas(year, month);
            }
            catch (Exception es)
            {
                throw new Exception(es.InnerException.Message);
            }
        }
    }
    /// <summary>
    /// Rma单业务部门操作处理器
    /// </summary>
    public class RmaBussesDescriptionProcessor
    {
        /// <summary>
        /// 通过RmaId，得到业务处理数据
        /// </summary>
        /// <param name="RmaId"></param>
        /// <returns></returns>
        public List<RmaBussesDescriptionModel> GetRmaBussesDescriptionDatasBy(string rmaId)
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
        public OpResult StoreRmaBussesDescriptionData(RmaBussesDescriptionModel model)
        {
            ///1.判断是否存在  在Rma初始表中是否存在 然后判断业务描述表是否存在  关键字（RmaId, RmaIdNumber） 
            ///2.如果存在 操作符为 Edit
            ///3.那些字段不能为空  ProductsShipDate 不能为空 不能为“0001-01-01”
            ///4.如果存储成功，改变初始Rma单状态
            try
            {
                OpResult result = OpResult.SetResult("存储数据表");
                if (model == null) return OpResult.SetResult("存储的数据不能为空");
                if (model.ProductsShipDate == DateTime.MinValue || model.ProductsShipDate == null) return OpResult.SetResult("存储的完成日期不对");
                if (!RmaCurdFactory.RmaReportInitiate.IsExist(model.RmaId)) return OpResult.SetResult("此表单不存在");
                if (RmaCurdFactory.RmaBussesDescription.IsExist(model.RmaId, model.ProductId))
                    model.OpSign = OpMode.Edit;
                else model.OpSign = OpMode.Add;
                result = RmaCurdFactory.RmaBussesDescription.Store(model, true);
                if (result.Result)
                    RmaCurdFactory.RmaReportInitiate.UpDataInitiateRmaIdStatus(model.RmaId, RmaCommomStatus.HandleStatust);
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
            
            ///1.如果存在 操作符为 Edit
            ///2.如果存储成功，改变Rma的业务描述表单状态和 初始状态
            OpResult result = OpResult.SetResult("存储数据表");
            if (model == null) return OpResult.SetResult("存储表不能为空");
            if (RmaCurdFactory.RmaInspectionManage.IsExist(model.RmaId, model.ProductId))
                model.OpSign = OpMode.Edit;
            else model.OpSign = OpMode.Add;
            result = RmaCurdFactory.RmaInspectionManage.Store(model, true);
            if (result.Result)
            {
                RmaCurdFactory.RmaReportInitiate.UpDataInitiateRmaIdStatus(model.RmaId, RmaCommomStatus.FinishStatus);
                RmaCurdFactory.RmaBussesDescription.UpDataBussesDescriptionStatus(model.RmaId, model.ProductId, RmaCommomStatus.FinishStatus);
            }
            return result;
        }


    }


}
