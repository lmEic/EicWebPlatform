using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.Business.Bmp.Quantity.Model
{
  public   class QuantityProductNgRecordModel
    {
      public QuantityProductNgRecordModel ()
      { }
      #region Model
      private string _orderid;
      private string _samplematerial;
      private string _samplematerialname;
      private string _samplematerialspec;
      private string _samplematerialsupplier;
      private DateTime? _samplematerialindate;
      private string _samplematerialdrawid;
      private decimal? _samplematerialnumber;
      private string _checkway;
      private int? _samplenumber;
      private int? _samplebadnumber;
      private decimal? _sampleratio;
      private string _sampleresult;
      private string _badreanson;
      private string _samplepersons;
      private string _resultdoway;
      private string _specialorderid;
      private int? _fullcheckworktime;
      private DateTime? _finishdate;
      private DateTime? _inputdate;
      private string _memo;
      private decimal _id_key;
      /// <summary>
      /// 
      /// </summary>
      public string OrderID
      {
          set { _orderid = value; }
          get { return _orderid; }
      }
      /// <summary>
      /// 
      /// </summary>
      public string SampleMaterial
      {
          set { _samplematerial = value; }
          get { return _samplematerial; }
      }
      /// <summary>
      /// 
      /// </summary>
      public string SampleMaterialName
      {
          set { _samplematerialname = value; }
          get { return _samplematerialname; }
      }
      /// <summary>
      /// 
      /// </summary>
      public string SampleMaterialSpec
      {
          set { _samplematerialspec = value; }
          get { return _samplematerialspec; }
      }
      /// <summary>
      /// 
      /// </summary>
      public string SampleMaterialSupplier
      {
          set { _samplematerialsupplier = value; }
          get { return _samplematerialsupplier; }
      }
      /// <summary>
      /// 
      /// </summary>
      public DateTime? SampleMaterialInDate
      {
          set { _samplematerialindate = value; }
          get { return _samplematerialindate; }
      }
      /// <summary>
      /// 
      /// </summary>
      public string SampleMaterialDrawID
      {
          set { _samplematerialdrawid = value; }
          get { return _samplematerialdrawid; }
      }
      /// <summary>
      /// 
      /// </summary>
      public decimal? SampleMaterialNumber
      {
          set { _samplematerialnumber = value; }
          get { return _samplematerialnumber; }
      }
      /// <summary>
      /// 
      /// </summary>
      public string CheckWay
      {
          set { _checkway = value; }
          get { return _checkway; }
      }
      /// <summary>
      /// 
      /// </summary>
      public int? SampleNumber
      {
          set { _samplenumber = value; }
          get { return _samplenumber; }
      }
      /// <summary>
      /// 
      /// </summary>
      public int? SampleBadNumber
      {
          set { _samplebadnumber = value; }
          get { return _samplebadnumber; }
      }
      /// <summary>
      /// 
      /// </summary>
      public decimal? SampleRatio
      {
          set { _sampleratio = value; }
          get { return _sampleratio; }
      }
      /// <summary>
      /// 
      /// </summary>
      public string SampleResult
      {
          set { _sampleresult = value; }
          get { return _sampleresult; }
      }
      /// <summary>
      /// 
      /// </summary>
      public string BadReanson
      {
          set { _badreanson = value; }
          get { return _badreanson; }
      }
      /// <summary>
      /// 
      /// </summary>
      public string SamplePersons
      {
          set { _samplepersons = value; }
          get { return _samplepersons; }
      }
      /// <summary>
      /// 
      /// </summary>
      public string ResultDoWay
      {
          set { _resultdoway = value; }
          get { return _resultdoway; }
      }
      /// <summary>
      /// 
      /// </summary>
      public string SpecialOrderId
      {
          set { _specialorderid = value; }
          get { return _specialorderid; }
      }
      /// <summary>
      /// 
      /// </summary>
      public int? FullCheckWorkTime
      {
          set { _fullcheckworktime = value; }
          get { return _fullcheckworktime; }
      }
      /// <summary>
      /// 
      /// </summary>
      public DateTime? FinishDate
      {
          set { _finishdate = value; }
          get { return _finishdate; }
      }
      /// <summary>
      /// 
      /// </summary>
      public DateTime? InPutDate
      {
          set { _inputdate = value; }
          get { return _inputdate; }
      }
      /// <summary>
      /// 
      /// </summary>
      public string Memo
      {
          set { _memo = value; }
          get { return _memo; }
      }
      /// <summary>
      /// 
      /// </summary>
      public decimal Id_key
      {

          set { _id_key = value; }
          get { return _id_key; }

      }
      #endregion Model
    }
}
