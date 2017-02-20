using Lm.Eic.App.Erp.Bussiness.PurchaseManage;
using Lm.Eic.App.Erp.Domain.PurchaseManage;
using System;
using System.Collections.Generic;

namespace Lm.Eic.App.Business.Bmp.Purchase
{
    /// <summary>
    /// 请购管理器
    /// </summary>
    public class RequisitionManager
    {
        /// <summary>
        /// 获取某个部门某段时间内的请购单单头数据信息
        /// </summary>
        /// <param name="department">部门</param>
        /// <param name="dateFrom">起始日期</param>
        /// <param name="dateTo">结束日期</param>
        /// <returns></returns>
        public List<RequisitionHeaderModel> FindReqHeaderDatasBy(string department, DateTime dateFrom, DateTime dateTo)
        {
            return PurchaseDbManager.RequisitionDb.FindReqHeaderBy(department, dateFrom, dateTo);
        }
        /// <summary>
        /// 获取的请购单单身数据信息
        /// </summary>
        /// <param name="requsitionID"></param>
        /// <returns></returns>
        public List<RequisitionBodyModel> FindReqBodyDatasByID(string requsitionID)
        {
            return PurchaseDbManager.RequisitionDb.FindReqBodyByID(requsitionID);
        }
        /// <summary>
        /// 根据采购部门,起止日期获取请购单单头数据信息
        /// </summary>
        /// <param name="department">部门</param>
        /// <param name="dateFrom">起始日期</param>
        /// <param name="dateTo">结束日期</param>
        /// <returns></returns>
        public List<RequisitionBodyModel> FindReqBodyDatasBy(string department, DateTime dateFrom, DateTime dateTo)
        {
            return PurchaseDbManager.RequisitionDb.FindReqBodyByDepartment(department, dateFrom, dateTo);
        }
    }
}