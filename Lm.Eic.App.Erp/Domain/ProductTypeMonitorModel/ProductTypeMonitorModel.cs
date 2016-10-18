using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Erp.Domain.ProductTypeMonitorModel
{
  public  class ProductTypeMonitorModel
    {
      /// <summary>
      /// 生产型号
      /// </summary>
      public string ProductType
      { set; get; }
      /// <summary>
      /// 生产规格
      /// </summary>
      public string ProductSpecify
      { set; get; }
      /// <summary>
      /// 汇总数量
      /// </summary>
      public double SumCount
      { set; get; }
      /// <summary>
      /// 在制工单数量
      /// </summary>
      public double OrderCount
      { set;  get; }
      /// <summary>
      /// 现场成品仓量
      /// </summary>
      public double LocaleFinishedCount
      {  set; get; }
      /// <summary>
      /// 保税库存成品量
      /// </summary>
      public double FreeTradeInHouseCount
      { set; get; }
      /// <summary>
      /// 全检工单量
      /// </summary>
      public double AllCheckOrderCount
      { set; get; }
      /// <summary>
      /// 来料成品
      /// </summary>
      public double PutInMaterialCount
      { set; get; }
      /// <summary>
      /// 差异量
      /// </summary>
      public double DifferenceCount
      { set; get; }
      /// <summary>
      /// 注释说明
      /// </summary>
      public string More
      { set; get; }
    }
}
