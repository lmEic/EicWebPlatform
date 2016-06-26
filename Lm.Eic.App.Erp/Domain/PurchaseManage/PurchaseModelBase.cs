using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Erp.Domain.PurchaseManage
{
  /// <summary>
  /// 采购模型基类
  /// </summary>
  public class PurchaseModelBase
    {
      /// <summary>
      /// 单别
      /// </summary>
      public string Category { get; set; }
      /// <summary>
      /// 单号
      /// </summary>
      public string Code { get; set; }

      protected string ID { get { return string.Format("{0}-{1}", Category.Trim(), Code.Trim()); } }
    }
}
