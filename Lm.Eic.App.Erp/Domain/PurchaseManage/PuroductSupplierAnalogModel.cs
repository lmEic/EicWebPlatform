using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.Erp.Domain.PurchaseManage
{
  /// <summary>
  /// 品号供应商每月统计维护数据模型
  /// PURLB
  /// </summary>
  public class PuroductSupplierAnalogModel
    {
      /// <summary>
      /// 供应商编号 LB001
      /// </summary>
      public string SupplierID { get; set; }
      /// <summary>
      /// 产品品号 LB002
      /// </summary>
      public string ProductID { get; set; }
      /// <summary>
      /// 统计年月 LB003
      /// </summary>
      public string AnalogYearMonth { get; set; }
      /// <summary>
      /// 进货数量 LB004
      /// </summary>
      public double PurchaseCount { get; set; }
      /// <summary>
      /// 进货金额 LB005
      /// </summary>
      public double PurchaseAmont { get; set; }
      /// <summary>
      /// 进货笔数 LB006
      /// </summary>
      public double PurchaseNumber { get; set; }
    }

  /// <summary>
  /// 供应商每月统计维护数据模型
  /// PURLC
  /// </summary>
  public class SupplierAnalogModel
  {
      /// <summary>
      /// 供应商编号 LC001
      /// </summary>
      public string SupplierID { get; set; }
      /// <summary>
      /// 统计年月 LC002
      /// </summary>
      public string AnalogYearMonth { get; set; }
      /// <summary>
      /// 进货数量 LC003
      /// </summary>
      public double PurchaseCount { get; set; }
      /// <summary>
      /// 进货金额 LC004
      /// </summary>
      public double PurchaseAmont { get; set; }
      /// <summary>
      /// 进货笔数 LC005
      /// </summary>
      public double PurchaseNumber { get; set; }
  }
}
