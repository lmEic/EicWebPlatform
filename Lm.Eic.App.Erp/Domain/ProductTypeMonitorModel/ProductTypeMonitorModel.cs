using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Erp.Domain.ProductTypeMonitorModel
{
  public  class ProductTypeMonitorModel
    {
      /// <summary>
      /// 品名
      /// </summary>
      public string ProductType
      { set; get; }
      /// <summary>
      /// 规格
      /// </summary>
      public string ProductSpecify
      { set; get; }
      /// <summary>
      /// 汇总
      /// </summary>
      public double SumCount
      { set; get; }
      /// <summary>
      /// 工单
      /// </summary>
      public double OrderCount
      { set;  get; }
      /// <summary>
      /// 现场成品仓
      /// </summary>
      public double LocaleFinishedCount
      {  set; get; }
      /// <summary>
      /// 库存成品
      /// </summary>
      public double FreeTradeInHouseCount
      { set; get; }
      /// <summary>
      /// 全检工单
      /// </summary>
      public double AllCheckOrderCount
      { set; get; }
      /// <summary>
      /// 来料成品
      /// </summary>
      public double PutInMaterialCount
      { set; get; }
      /// <summary>
      /// 差异
      /// </summary>
      public double DifferenceCount
      { set; get; }
      /// <summary>
      /// 备注
      /// </summary>
      public string More
      { set; get; }
    }
}
