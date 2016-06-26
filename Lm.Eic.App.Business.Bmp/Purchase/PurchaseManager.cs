using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.Erp.Bussiness.PurchaseManage;
using Lm.Eic.App.Erp.Domain.PurchaseManage;

namespace Lm.Eic.App.Business.Bmp.Purchase
{
  public class PurchaseManager
    {
        /// <summary>
        /// 获取某个部门某段时间内的采购单单头数据信息
        /// </summary>
        /// <param name="department">部门</param>
        /// <param name="dateFrom">起始日期</param>
        /// <param name="dateTo">结束日期</param>
        /// <returns></returns>
        public List<PurchaseHeaderModel> FindPurHeaderDatasBy(string department, DateTime dateFrom, DateTime dateTo)
        {
            return PurchaseDbManager.PurchaseDb.FindPurHeaderByDepartment(department, dateFrom, dateTo);
        }
        public List<PurchaseBodyModel> FindPurBodyDatasByID(string id)
        {
            return PurchaseDbManager.PurchaseDb.FindPurBodyByID(id);
        }
        public List<PurchaseBodyModel> FindPurBodyDatasBy(string department, DateTime dateFrom, DateTime dateTo)
        {
            return PurchaseDbManager.PurchaseDb.FindPurBodyByDepartment(department, dateFrom, dateTo);
        }
    }
}
