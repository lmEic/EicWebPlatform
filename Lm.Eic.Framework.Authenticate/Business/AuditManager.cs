using Lm.Eic.Framework.Authenticate.Model;
using Lm.Eic.Framework.Authenticate.Repository;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.Authenticate.Business
{
    /// <summary>
    /// 审核管理器
    /// </summary>
    public class AuditManager
    {

        private AuditManagerCrud AuditManagerCrud
        {
            get { return OBulider.BuildInstance<AuditManagerCrud>(); }
        }

        /// <summary>
        /// 添加一条审核记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult AddAuditRecoud(AuditModel model)
        {
           return AuditManagerCrud.AddAuditRecoud(model);
        }

        /// <summary>
        /// 是否已经审核
        /// </summary>
        /// <param name="parameterKey"></param>
        /// <returns></returns>
        public bool HasAudit(string parameterKey)
        {
            var auditList = AuditManagerCrud.GetAuditListBy(parameterKey).FirstOrDefault();
            return auditList != null;
        }

    }

    /// <summary>
    /// 审核管理器 CRUD
    /// </summary>
    internal class AuditManagerCrud: CrudBase<AuditModel, IAuditModelMappingRepository>
    {
        public AuditManagerCrud() : base(new AuditModelMappingRepository(), "审核记录")
        {
        }

        protected override void AddCrudOpItems()
        {
           // throw new NotImplementedException();
        }
        /// <summary>
        /// 添加一条审核记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult AddAuditRecoud(AuditModel model)
        {
            SetFixFieldValue(model);
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }

        /// <summary>
        /// 获取审核列表
        /// </summary>
        /// <param name="parameterKey"></param>
        /// <returns></returns>
        public List<AuditModel> GetAuditListBy(string parameterKey)
        {
            return irep.Entities.Where(m => m.ParameterKey == parameterKey).ToList();
        }

   
    }
}
