using System;
using System.Collections.Generic;
using System.Linq;

using  Lm.Eic.App.Erp.Bussiness.PurchaseManage;
using Lm.Eic.App.DomainModel.Bpm.Purchase;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;

using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;

using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeExtension.Validation;
namespace Lm.Eic.App.Business.Bmp.Purchase.SupplierManager
{
   /// <summary>
   /// 采购供应商配置管理
   /// </summary>
    public class PurSupplierConfigManager
    {
        /// <summary>
        /// 供应商证书管理
        /// </summary>
        public SupplierCertificateManager SupplierCertificateManager
        {
            get { return OBulider.BuildInstance<SupplierCertificateManager>(); }
        }
        /// <summary>
        /// 供应商考核管理
        /// </summary>
        public SupplierAuditManager SupplierAuditManager
        {
            get { return OBulider.BuildInstance<SupplierAuditManager>(); }
        }
        /// <summary>
        /// 供应商辅导管理
        /// </summary>
        public SupplierTutorManger SupplierTutorManger
        {
            get { return OBulider.BuildInstance<SupplierTutorManger>(); }
        }
        /// <summary>
        /// 供应商稽核评分管理
        /// </summary>
        public SuppliersGradeManager SuppliersGradeManager
        {
            get { return OBulider.BuildInstance<SuppliersGradeManager>(); }
        }
    }


}
