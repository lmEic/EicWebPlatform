using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
namespace Lm.Eic.App.Erp.Bussiness.CopManage
{
    public class CopService
    {
        /// <summary>
        /// 工单与订单对比管理
        /// </summary>
        public static CopOrderWorkorderManage OrderWorkorderManager
        {
            get { return OBulider.BuildInstance<CopOrderWorkorderManage>(); }
        }
        /// <summary>
        /// 退料处理单管理
        /// </summary>
        public static CopReturnOrderManage ReturnOrderManage
        {
            get { return OBulider.BuildInstance<CopReturnOrderManage>(); }
        }
    }
}
