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
      private string _suppliersid;
      /// <summary>
      ///供应商Id
      /// </summary>
      public string SuppliersId
      {
          set { _suppliersid = value; }
          get { return _suppliersid; }
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
      private string _supplierparty;
      /// <summary>
      ///供应商分类
      /// </summary>
      public string SupplierParty
      {
          set { _supplierparty = value; }
          get { return _supplierparty; }
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
      private string _supplierpeople;
      /// <summary>
      ///联系人
      /// </summary>
      public string SupplierPeople
      {
          set { _supplierpeople = value; }
          get { return _supplierpeople; }
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
      private string _billAddress;
      /// <summary>
      ///货单地址
      /// </summary>
      public string  BillAddress
      {
          set { _billAddress = value; }
          get { return _billAddress; }
      }
      private string _purchasepeople;
      /// <summary>
      ///采购负责人
      /// </summary>
      public string PurchasePeople
      {
          set { _purchasepeople = value; }
          get { return _purchasepeople; }
      }
      private DateTime _lastpurchasedate;
      /// <summary>
      ///上次采购时间
      /// </summary>
      public DateTime LastPurchaseDate
      {
          set { _lastpurchasedate = value; }
          get { return _lastpurchasedate; }
      }
      private DateTime _latestpurchasedate;
      /// <summary>
      ///最近采购时间
      /// </summary>
      public DateTime LatestPurchaseDate
      {
          set { _latestpurchasedate = value; }
          get { return _latestpurchasedate; }
      }
      private string _purchaseclass;
      /// <summary>
      ///采购类别
      /// </summary>
      public string PurchaseClass
      {
          set { _purchaseclass = value; }
          get { return _purchaseclass; }
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
      private string _oppersom;
      /// <summary>
      ///操作人
      /// </summary>
      public string OpPersom
      {
          set { _oppersom = value; }
          get { return _oppersom; }
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
      public DateTime Opdate
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
}
