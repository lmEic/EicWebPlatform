using System;

namespace Lm.Eic.App.Erp.Domain.QuantityModel
{

    #region  委外进货
    /// <summary>
    /// 委外进货单头
    /// 1.委外进货单别 +单号 (TH001 +TH002)
    ///3.单据日期 (TH029)
    ///5.委外供应商 (TH005)
    ///6.进货日期 (TH003)
    ///5.备注 (TH010)
    ///7.数量合计 (TH022)
    /// </summary>

    public class OutsourcingMaterialHeaderModel : QuantitySampleMaodelBase
    {   
        /// <summary>
        /// //1.委外进货单别 单号 (TH001  TH002)
        /// </summary>
        public  string  OutsourcingID{get{return ID ;}}
         /// <summary>
         ///  单据日期 (TH029)
         /// </summary>
        public DateTime  IDDate{set;get;}
          /// <summary>
          ///  委外供应商 (TH005)
          /// </summary>
        public string   Supplier{set;get;}
        /// <summary>
        ///    进货日期   TH003 
        /// </summary>
        public DateTime PutInDate
        { set; get; }
        /// <summary>
        ///  供应商代码  TH005 
        /// </summary>
        public string PutInSupperId
        { set; get; }
       
        /// <summary>
        /// 备注 TH010
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        ///   数量合计 (TH022)
        /// </summary>
        public double count
        { set; get; }

    }
  /// <summary>
    /// 1.委外进货单别 +单号 (TH001 +TH002)
    ///4.品号 (TI004)
    ///5.品名 (TI005)
    ///6.规格 (TI006)
    ///7.进货数量 (TI007)
    ///14.生产日期(TI054)
    ///23.计价单位 (TI023)
    ///32.进货费用 (TI027)
    ///43.备注 (TI040)
  /// </summary>
    public class OutsourcingMaterialBodyModel : QuantitySampleMaodelBase
    {
        /// <summary>
        /// 委外进货单别 +单号 (TH001 +TH002)
        /// </summary>
        public string OutsourcingID { get { return ID; } }
        /// <summary>
        /// 品号 (TI004)
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 品名 (TI005)
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 规格(TI006)
        /// </summary>
        public string ProductSpecify { get; set; }
        /// <summary>
        /// 物料进货数量 TI007
        /// </summary>
        public double PutInCount { get; set; }
        /// <summary>
        ///  生产日期(TI054)
        /// </summary>
        public DateTime ProduceDate { set; get; }
        /// <summary>
        /// 计价单位 TI023
        /// </summary>
        public string UnitedPrice { get; set; }
          /// <summary>
        ///  进货费用 (TI027)
          /// </summary>
        public double Cost { set; get; }
        /// <summary>
        /// 备注  (TI040)
        /// </summary>
        public string Memo { get; set; }

      

    }
    #endregion




    /// <summary>
   /// 抽样物料信息
   /// </summary>
    public class MaterialModel : QuantitySampleMaodelBase
    {
        /// <summary>
        /// ERP导出的物料模块
        /// </summary>
        public MaterialModel()
        { }
        #region model
        /// <summary>
        /// 工单单号  
        /// </summary>
        public string OrderID { get { return ID; } }
        /// <summary>
        /// 进料日期 
        /// </summary>
        public DateTime  ProduceInDate { get; set; }
        /// <summary>
        ///  产品品号 
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 产品品名  
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 产品规格
        /// </summary>
        public string ProductStandard { get; set; }
        /// <summary>
        /// 供应商 
        /// </summary>
        public string ProductSupplier { get; set; }
        /// <summary>
        /// 产品图号
        /// </summary>
        public string ProductDrawID { get; set; }
        /// <summary>
        /// 进料数量 
        /// </summary>
        public Int64  ProduceNumber { get; set; }

        #endregion
    }
    /// <summary>
    ///  产品物料信息
     /// </summary>
    public class ProductModel  
   {
        
       /// <summary>
       /// 品号 MB001
       /// </summary>
       public string ProductID { get; set; }
       /// <summary>
       /// 品名 MB002
       /// </summary>
       public string ProductName { get; set; }
       /// <summary>
       /// 规格 MB003
       /// </summary>
       public string ProductSpecify { get; set; }
       /// <summary>
       /// 单位名称 MB004
       /// </summary>
       public string  UnitedName { get; set; }
        /// <summary>
        /// 单位计量 MB015
        /// </summary>
       public string UniteCount { get;set; }
       /// <summary>
       /// 产品图号 MB029
       /// </summary>
       public string ProductDrawID { get; set; }
        /// <summary>
        /// 物料属于部门 TM068
        /// </summary>
       public string ProductBelongDepartment
       { get; set; }
       /// <summary>
       /// 备注 TM028
       /// </summary>
       public string Memo { get; set; }

   }
}
