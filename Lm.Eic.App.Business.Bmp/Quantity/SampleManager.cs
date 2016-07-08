using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.DbAccess.Bpm.Repository.QuantityRep;
using Lm.Eic.App.Erp.Bussiness.QuantityManage;
using Lm.Eic.App.Erp.Domain .QuantityModel;

namespace Lm.Eic.App.Business.Bmp.Quantity
{
    public class IQCSampleItemsRecordManager
    {
        IIQCSampleItemRecordReposity irep = null;
        public IQCSampleItemsRecordManager ()
        {
            irep = new IQCSampleItemRecordReposity();
        }
       /// <summary>
       /// 得到IQC抽样项次
       /// </summary>
       /// <param name="orderid"></param>
       /// <returns></returns>
        public  List<IQCSampleItemRecordModel> GetSamplePrintItemBy(string orderid)
        {
            return irep .Entities .Where (e=>e.OrderID ==orderid ).ToList ();
        }
        /// <summary>
        /// 得到抽样物料信息
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public List<MaterialModel> GetPuroductSupplierInfo(string orderid)
        {
            return   QuantityDBManager.QuantityPurchseDb.FindMaterialBy(orderid);
        }
    }
  



   /// <summary>
   ///抽验查询对像
   /// </summary>
   public class SampleQueries
    {
        #region
        /// <summary>
        /// 样品订单号
        /// </summary>
        public string OrderId
        { set; get; }
        /// <summary>
        /// 样品料号
        /// </summary>
        public string Material
        { set; get; }
        /// <summary>
        /// 样品名称
        /// </summary>
        public string MaterialName
        { set; get; }
        /// <summary>
        /// 样品规格
        /// </summary>
        public string MaterialSpec
        { set; get; }
        /// <summary>
        /// 样品的供应商
        /// </summary>
        public string MaterialSupplier
        { set; get; }
        /// <summary>
        /// 样品购入日期
        /// </summary>
        public string MaterialInDate
        { set; get; }
        /// <summary>
        /// 样品提供ERP中图号
        /// </summary>
        public string MaterialDrawID
        { set; get; }
        /// <summary>
        /// 抽样数量
        /// </summary>
        public string MaterialNumber
        { set; get; }
        #endregion
    }
}
