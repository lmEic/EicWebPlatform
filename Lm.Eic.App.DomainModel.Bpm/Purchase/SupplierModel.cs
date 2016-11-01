using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Purchase
{
    /// <summary>
    /// 合格的供应商清册Model
    /// </summary>
   public  class QualifiedSupplierModel
    {
      public QualifiedSupplierModel ()
       { }
      #region Model
      private string _supplierid;
      /// <summary>
      ///供应商Id
      /// </summary>
      public string SupplierId
      {
          set { _supplierid = value; }
          get { return _supplierid; }
      }
      private string _suppliershortname;
      /// <summary>
      ///供应商简称
      /// </summary>
      public string SupplierShortName
      {
          set { _suppliershortname = value; }
          get { return _suppliershortname; }
      }
      private string _suppliername;
      /// <summary>
      ///供应商全称
      /// </summary>
      public string SupplierName
      {
          set { _suppliername = value; }
          get { return _suppliername; }
      }
      private string _supplierproperty;
      /// <summary>
      ///供应商属性
      /// </summary>
      public string SupplierProperty
      {
          set { _supplierproperty = value; }
          get { return _supplierproperty; }
      }
      private string _suppliertel;
      /// <summary>
      ///电话
      /// </summary>
      public string SupplierTel
      {
          set { _suppliertel = value; }
          get { return _suppliertel; }
      }
      private string _supplieruser;
      /// <summary>
      ///供应商联系人
      /// </summary>
      public string SupplierUser
      {
          set { _supplieruser = value; }
          get { return _supplieruser; }
      }
      private string _supplierfaxno;
      /// <summary>
      ///传真
      /// </summary>
      public string SupplierFaxNo
      {
          set { _supplierfaxno = value; }
          get { return _supplierfaxno; }
      }
      private string _supplieremail;
      /// <summary>
      /// 邮箱
      /// </summary>
      public string SupplierEmail
      {
          set { _supplieremail = value; }
          get { return _supplieremail; }
      }
      private string _supplieraddress;
      /// <summary>
      ///地址
      /// </summary>
      public string SupplierAddress
      {
          set { _supplieraddress = value; }
          get { return _supplieraddress; }
      }
      private string _billaddress;
      /// <summary>
      ///交货地址
      /// </summary>
      public string BillAddress
      {
          set { _billaddress = value; }
          get { return _billaddress; }
      }
      private string _purchaseuser;
      /// <summary>
      ///采购人员
      /// </summary>
      public string PurchaseUser
      {
          set { _purchaseuser = value; }
          get { return _purchaseuser; }
      }
      private DateTime _upperpurchasedate;
      /// <summary>
      ///上次采购日期
      /// </summary>
      public DateTime UpperPurchaseDate
      {
          set { _upperpurchasedate = value; }
          get { return _upperpurchasedate; }
      }
      private DateTime _lastpurchasedate;
      /// <summary>
      ///最近采购日期
      /// </summary>
      public DateTime LastPurchaseDate
      {
          set { _lastpurchasedate = value; }
          get { return _lastpurchasedate; }
      }
      private string _purchasetype;
      /// <summary>
      ///采购类型
      /// </summary>
      public string PurchaseType
      {
          set { _purchasetype = value; }
          get { return _purchasetype; }
      }
      private string _suppliereligibleprojects;
      /// <summary>
      ///合格项目
      /// </summary>
      public string SupplierEligibleprojects
      {
          set { _suppliereligibleprojects = value; }
          get { return _suppliereligibleprojects; }
      }
      private string _superliereligibleprojectsdate;
      /// <summary>
      ///合格项目对应的日期
      /// </summary>
      public string SuperlierEligibleprojectsDate
      {
          set { _superliereligibleprojectsdate = value; }
          get { return _superliereligibleprojectsdate; }
      }
      private string _remark;
      /// <summary>
      ///备注
      /// </summary>
      public string Remark
      {
          set { _remark = value; }
          get { return _remark; }
      }
      private string _opperson;
      /// <summary>
      ///操作人员
      /// </summary>
      public string OpPerson
      {
          set { _opperson = value; }
          get { return _opperson; }
      }
      private string _opsign;
      /// <summary>
      ///操作标识
      /// </summary>
      public string OpSign
      {
          set { _opsign = value; }
          get { return _opsign; }
      }
      private DateTime _opdate;
      /// <summary>
      ///操作日期
      /// </summary>
      public DateTime OpDate
      {
          set { _opdate = value; }
          get { return _opdate; }
      }
      private DateTime _optime;
      /// <summary>
      ///操作时间
      /// </summary>
      public DateTime OpTime
      {
          set { _optime = value; }
          get { return _optime; }
      }
      private decimal _id_key;
      /// <summary>
      ///自增键
      /// </summary>
      public decimal Id_key
      {
          set { _id_key = value; }
          get { return _id_key; }
      }
      #endregion Model
    }

    /// <summary>
    /// 供应商合格文件
    /// </summary>
   public class SupplierEligibleModel
   {
          #region Model
       private string _suppliersid;
       /// <summary>
       ///供应商Id
       /// </summary>
       public string SuppliersID
       {
           set { _suppliersid = value; }
           get { return _suppliersid; }
       }
       private string _eligibleitems;
       /// <summary>
       ///合格项目
       /// </summary>
       public string EligibleItems
       {
           set { _eligibleitems = value; }
           get { return _eligibleitems; }
       }
       private DateTime _validitydate;
       /// <summary>
       ///有效期
       /// </summary>
       public DateTime ValidityDate
       {
           set { _validitydate = value; }
           get { return _validitydate; }
       }
       private DateTime _putindate;
       /// <summary>
       ///录入日期
       /// </summary>
       public DateTime PutInDate
       {
           set { _putindate = value; }
           get { return _putindate; }
       }
       private string  _isvalidity;
       /// <summary>
       ///是否有效(是/否)
       /// </summary>
       public string  IsValidity
       {
           set { _isvalidity = value; }
           get { return _isvalidity; }
       }



       private string _filePath;
       /// <summary>
       ///文件存放路
       /// </summary>
       public string FilePath
       {
           set { _filePath = value; }
           get { return _filePath; }
       }
       private string _remark;
       /// <summary>
       ///备注
       /// </summary>
       public string Remark
       {
           set { _remark = value; }
           get { return _remark; }
       }
       private string _opperson;
       /// <summary>
       ///操作人
       /// </summary>
       public string OpPerson
       {
           set { _opperson = value; }
           get { return _opperson; }
       }
       private DateTime _opdate;
       /// <summary>
       ///操作日期
       /// </summary>
       public DateTime OpDate
       {
           set { _opdate = value; }
           get { return _opdate; }
       }
       private string _opsign;
       /// <summary>
       ///操作标识
       /// </summary>
       public string OpSign
       {
           set { _opsign = value; }
           get { return _opsign; }
       }
       private decimal _id_key;
       /// <summary>
       ///自增键
       /// </summary>
       public decimal Id_key
       {
           set { _id_key = value; }
           get { return _id_key; }
       }
       #endregion Model
   }

    /// <summary>
   /// 供应商信息表
    /// </summary>
  public class SupplierInfoModel
  {
      public SupplierInfoModel ()
      { }

      #region Model
      private string _suppliersid;
      /// <summary>
      ///供应商ID
      /// </summary>
      public string SupplierId
      {
          set { _suppliersid = value; }
          get { return _suppliersid; }
      }
      private string _purchasetype;
      /// <summary>
      ///采购类型
      /// </summary>
      public string PurchaseType
      {
          set { _purchasetype = value; }
          get { return _purchasetype; }
      }
      private string _supplierproperty;
      /// <summary>
      ///供应商属性
      /// </summary>
      public string SupplierProperty
      {
          set { _supplierproperty = value; }
          get { return _supplierproperty; }
      }
      private string _suppliershortname;
      /// <summary>
      ///供应商简称
      /// </summary>
      public string SupplierShortName
      {
          set { _suppliershortname = value; }
          get { return _suppliershortname; }
      }
      private string _suppliername;
      /// <summary>
      ///全称
      /// </summary>
      public string SupplierName
      {
          set { _suppliername = value; }
          get { return _suppliername; }
      }
      private string _purchaseuser;
      /// <summary>
      ///采购负责人
      /// </summary>
      public string PurchaseUser
      {
          set { _purchaseuser = value; }
          get { return _purchaseuser; }
      }
      private string _suppliertel;
      /// <summary>
      ///供应商电话
      /// </summary>
      public string SupplierTel
      {
          set { _suppliertel = value; }
          get { return _suppliertel; }
      }
      private string _supplieruser;
      /// <summary>
      ///供应商联系人
      /// </summary>
      public string SupplierUser
      {
          set { _supplieruser = value; }
          get { return _supplieruser; }
      }
      private string _supplierfaxno;
      /// <summary>
      ///供应商传真
      /// </summary>
      public string SupplierFaxNo
      {
          set { _supplierfaxno = value; }
          get { return _supplierfaxno; }
      }
      private string _supplieremail;
      /// <summary>
      ///供应商邮箱
      /// </summary>
      public string SupplierEmail
      {
          set { _supplieremail = value; }
          get { return _supplieremail; }
      }
      private string _supplieraddress;
      /// <summary>
      ///供应商地址
      /// </summary>
      public string SupplierAddress
      {
          set { _supplieraddress = value; }
          get { return _supplieraddress; }
      }
      private string _billaddress;
      /// <summary>
      ///供应商付款地址
      /// </summary>
      public string BillAddress
      {
          set { _billaddress = value; }
          get { return _billaddress; }
      }
      private string _paycondition;
      /// <summary>
      ///供应商付款方式
      /// </summary>
      public string PayCondition
      {
          set { _paycondition = value; }
          get { return _paycondition; }
      }
      private string _remark;
      /// <summary>
      ///备注
      /// </summary>
      public string Remark
      {
          set { _remark = value; }
          get { return _remark; }
      }
      private string _opperson;
      /// <summary>
      ///操作人
      /// </summary>
      public string OpPerson
      {
          set { _opperson = value; }
          get { return _opperson; }
      }
      private string _opsign;
      /// <summary>
      ///操作标识
      /// </summary>
      public string OpSign
      {
          set { _opsign = value; }
          get { return _opsign; }
      }
      private DateTime _opdate;
      /// <summary>
      ///操作日期
      /// </summary>
      public DateTime OpDate
      {
          set { _opdate = value; }
          get { return _opdate; }
      }
      private DateTime _optime;
      /// <summary>
      ///操作时间
      /// </summary>
      public DateTime OpTime
      {
          set { _optime = value; }
          get { return _optime; }
      }
      private decimal _id_key;
      /// <summary>
      ///自增键
      /// </summary>
      public decimal Id_key
      {
          set { _id_key = value; }
          get { return _id_key; }
      }
      #endregion Model
  }

   /// <summary>
   /// 季度考核总览表
   /// </summary>
   public class SupplieSeasonAuditModel
   {
       #region Model
       private string _supplierid;
       /// <summary>
       ///供应商ID
       /// </summary>
       public string SupplierId
       {
           set { _supplierid = value; }
           get { return _supplierid; }
       }
       private string _suppliername;
       /// <summary>
       ///供应商名称
       /// </summary>
       public string SupplierName
       {
           set { _suppliername = value; }
           get { return _suppliername; }
       }
       private double _qualitycheck;
       /// <summary>
       ///品质考核
       /// </summary>
       public double QualityCheck
       {
           set { _qualitycheck = value; }
           get { return _qualitycheck; }
       }
       private double _auditprice;
       /// <summary>
       ///审计价格
       /// </summary>
       public double AuditPrice
       {
           set { _auditprice = value; }
           get { return _auditprice; }
       }
       private double _deliverydate;
       /// <summary>
       ///交期
       /// </summary>
       public double DeliveryDate
       {
           set { _deliverydate = value; }
           get { return _deliverydate; }
       }
       private double _actionliven;
       /// <summary>
       ///配合度
       /// </summary>
       public double ActionLiven
       {
           set { _actionliven = value; }
           get { return _actionliven; }
       }
       private double _hsfgrade;
       /// <summary>
       ///HSF能力考核等级
       /// </summary>
       public double HSFGrade
       {
           set { _hsfgrade = value; }
           get { return _hsfgrade; }
       }
       private double _totalcheckscore;
       /// <summary>
       ///考核总分
       /// </summary>
       public double TotalCheckScore
       {
           set { _totalcheckscore = value; }
           get { return _totalcheckscore; }
       }
       private string _checklevel;
       /// <summary>
       ///考核分级
       /// </summary>
       public string CheckLevel
       {
           set { _checklevel = value; }
           get { return _checklevel; }
       }
       private string _rewardsway;
       /// <summary>
       ///奖惩方式
       /// </summary>
       public string RewardsWay
       {
           set { _rewardsway = value; }
           get { return _rewardsway; }
       }
       private string _materialgrade;
       /// <summary>
       ///物料级别
       /// </summary>
       public string MaterialGrade
       {
           set { _materialgrade = value; }
           get { return _materialgrade; }
       }
       private string _managerrisk;
       /// <summary>
       ///对供应商的管理风险
       /// </summary>
       public string ManagerRisk
       {
           set { _managerrisk = value; }
           get { return _managerrisk; }
       }
       private string _substitutionsupplierid;
       /// <summary>
       ///替代供应商
       /// </summary>
       public string SubstitutionSupplierId
       {
           set { _substitutionsupplierid = value; }
           get { return _substitutionsupplierid; }
       }
       private int _seasonnum;
       /// <summary>
       ///考核季度
       /// </summary>
       public int SeasonNum
       {
           set { _seasonnum = value; }
           get { return _seasonnum; }
       }
       private string _remark;
       /// <summary>
       ///备注
       /// </summary>
       public string Remark
       {
           set { _remark = value; }
           get { return _remark; }
       }
       private string _oppserson;
       /// <summary>
       ///操作人
       /// </summary>
       public string OpPserson
       {
           set { _oppserson = value; }
           get { return _oppserson; }
       }
       private DateTime _opdate;
       /// <summary>
       ///操作日期
       /// </summary>
       public DateTime OpDate
       {
           set { _opdate = value; }
           get { return _opdate; }
       }
       private string _opsign;
       /// <summary>
       ///操作标签
       /// </summary>
       public string OpSign
       {
           set { _opsign = value; }
           get { return _opsign; }
       }
       private decimal _id_key;
       /// <summary>
       ///自增建
       /// </summary>
       public decimal Id_key
       {
           set { _id_key = value; }
           get { return _id_key; }
       }
       #endregion Model
   }

}
