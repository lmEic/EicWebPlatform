using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.Erp.DbAccess.CopManageDb;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.RmaManage
{
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
                var oldmodel = RmaCurdFactory.RmaReportInitiate.GetInitiateDatas(model.RmaId).FirstOrDefault();
                model.RmaMonth = oldmodel.RmaMonth;
                model.RmaYear = oldmodel.RmaYear;
                model.Id_Key = oldmodel.Id_Key;
                model.RmaIdStatus = oldmodel.RmaIdStatus;
                model.OpSign = OpMode.UpDate;
            }
            else
            {
                if (model.RmaId != null && model.RmaId.Length == 8)
                {
                    model.RmaYear = model.RmaId.Substring(1, 2);
                    model.RmaMonth = model.RmaId.Substring(3, 2);
                }
                model.RmaIdStatus = "未结案";
                model.OpSign = OpMode.Add;
            }
            return RmaCurdFactory.RmaReportInitiate.Store(model);
        }
        /// <summary>
        /// 得到初始Rma表单
        /// </summary>
        /// <param name="rmaId"></param>
        /// <returns></returns>
        public List<RmaReportInitiateModel> GetRemPeortInitiateData(string rmaId)
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
        public List<RmaBussesDescriptionModel> GetRmaBussesDescriptionDatasBy(string RmaId)
        {
            return RmaCurdFactory.RmaBussesDescription.GetRmaBussesDescriptionDatasBy(RmaId);
        }
        /// <summary>
        /// 通过退料单或换货单得到相应的物料信息
        /// </summary>
        /// <param name="returnHandleOrder">退货单</param>
        /// <returns></returns>
        public List<RmaERPBusseeInfoModel> GetErpBussesInfoDatasBy(string returnHandleOrder)
        {
            try
            {
                List<RmaERPBusseeInfoModel> returnDatas = new List<RmaERPBusseeInfoModel>();
                //从ERP中得到相应的数据
                var listErpDatas = CopOrderCrudFactory.CopReturnOrderManageDb.FindReturnOrderByID(returnHandleOrder);
                if (listErpDatas == null || listErpDatas.Count <= 0) return returnDatas;
                listErpDatas.ForEach(m =>
                {
                    returnDatas.Add(new RmaERPBusseeInfoModel()
                    {
                        ReturnHandleOrder = m.OrderId,
                        CustomerId = m.CustomerId,
                        CustomerName = m.CustomerShortName,
                        ProdcutId = m.ProductID,
                        ProductName = m.ProductName,
                        ProductSpec = m.ProductSpecify,
                        ProductCount = m.ProductNumber,

                    });
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
            return RmaCurdFactory.RmaBussesDescription.Store(model);
        }

    }
    /// <summary>
    /// Rma单 品保部操作处理器
    /// </summary>
    public class RmaInspecitonManageProcessor
    {
        /// <summary>
        /// 通过RmaId，得到品何处理数据
        /// </summary>
        /// <param name="RmaId"></param>
        /// <returns></returns>
        public List<RmaInspectionManageModel> GetDatasBy(string RmaId)
        {
            return RmaCurdFactory.RmaInspectionManage.GetInspectionManageDatasBy(RmaId);
        }
    }

}
