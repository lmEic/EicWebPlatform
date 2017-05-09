using Lm.Eic.App.Erp.DbAccess.QuantitySampleDb;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Erp.Bussiness.QmsManage
{
   public  class QmsDbManager
    {

        /// <summary>
        /// 质量管理单号访问接口
        /// </summary>
        public static OrderIdInspectionDb QuantityPurchseDb
        {
            get { return OBulider.BuildInstance<OrderIdInspectionDb>(); }
        }
        /// <summary>
        /// 质量物料访问接口
        /// </summary>
        public static MaterialInfoDb MaterialInfoDb
        {
            get { return OBulider.BuildInstance <MaterialInfoDb>(); }
        }
    }
}
