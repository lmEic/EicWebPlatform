using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.DbAccess.Bpm.Repository.QuantityRep;
using Lm.Eic.App.Erp.Bussiness.QuantityManage;
using Lm.Eic.App.Erp.Domain .QuantityModel;
using Lm.Eic.Uti.Common.YleeOOMapper;

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
       /// 
       /// </summary>
       /// <param name="orderid"></param>
       /// <returns></returns>
        public  List<IQCSampleItemRecordModel> GetSamplePrintItemBy(string orderid)
        {
            return irep .Entities .Where (e=>e.OrderID ==orderid ).ToList ();
        }



        /// <summary>
        /// 得到IQC抽样项次 （单身）
        /// </summary>
        /// <param name="Orderid"></param>
        /// <param name="SampleMaterial"></param>
        /// <returns></returns>
        public  List<IQCSampleItemRecordModel> GetPringSampleItemBy(string Orderid,string SampleMaterial)
        {
            return irep.Entities.Where(e => e.OrderID == Orderid & e.SampleMaterial == SampleMaterial).ToList();
        }


        public OpResult Store(List<IQCSampleItemRecordModel> listModel)
        {
            try
            {
                if (listModel == null || listModel.Count <= 0)
                    return OpResult.SetResult("集合不能为空！", false);

                int record = 0;
                string opSign = string.Empty;
                opSign = listModel[0].OpSign;

                switch (opSign)
                {
                    case OpMode.Add: //新增
                        record = 0;
                        listModel.ForEach(model => { record += irep.Insert(model); });
                        return OpResult.SetResult("添加成功！", "添加失败！", record);

                    case OpMode.Edit: //修改
                        record = 0;
                        listModel.ForEach(model => { record += irep.Update(u => u.Id_key  == model.Id_key, model); });
                        return OpResult.SetResult("更新成功！", "更新失败！", record);

                    case OpMode.Delete: //删除
                        record = 0;
                        listModel.ForEach(model => { record += irep.Delete(model.Id_key); });
                        return OpResult.SetResult("删除成功！", "删除失败！", record);

                    default: return OpResult.SetResult("操作模式溢出！", false);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        
        /// <summary>
        /// 得到抽样物料信息 （单头）
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
