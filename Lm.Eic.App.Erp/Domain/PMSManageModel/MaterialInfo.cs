using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.Erp.Domain.PurchaseManage;

namespace Lm.Eic.App.Erp.Domain.PMSManageModel
{
  public class MaterialHaaderModel : PurchaseModelBase
    {
        /// <summary>
        /// 进货单号 +
        /// </summary>
      public string OrderID { get { return ID; } }
        /// <summary>
        /// 品号 
        /// </summary>
        public string ProductID { get; set; }

        /// <summary>
        /// 品名 
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 规格 
        /// </summary>
        public string ProductSpecify { get; set; }
        /// <summary>
        /// 生产日期 
        /// </summary>
        public string ProductDate { get; set; }
        /// <summary>
        /// 进货数量 
        /// </summary>
        public long ProductCount { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public double Unit { get; set; }

     
      
   
        
    }
  public class MaterialBodyModel 
 {

     /// <summary>
     /// 进货单号 +
     /// </summary>
     public string OrderID { set; get; }
     /// <summary>
     ///物料 品号 
     /// </summary>
     public string MaterialID { get; set; }
     /// <summary>
     ///物料 品名 
     /// </summary>
     public string MaterialName { get; set; }
     /// <summary>
     /// 物料规格 
     /// </summary>
     public string MaterialSpecify { get; set; }
     /// <summary>
     /// 单位
     /// </summary>
     public string  Unit { get; set; }
     /// <summary>
     /// 生产日期 
     /// </summary>
     public double  MaterialNeedCount { get; set; }
     /// <summary>
     /// 进货数量 
     /// </summary>
     public double  MaterialReceiveCount { get; set; }
 
 }
}
