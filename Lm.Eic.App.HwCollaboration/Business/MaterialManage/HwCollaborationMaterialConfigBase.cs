using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.HwCollaboration.Model;
using Lm.Eic.App.HwCollaboration.DbAccess;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.App.HwCollaboration.Business;

namespace Lm.Eic.App.HwCollaboration.Business.MaterialManage
{
    /// <summary>
    /// 物料控制基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class HwCollaborationMaterialBase<T> : HwCollaborationDataBase<T> where T : class, new()
    {
        /// <summary>
        /// ERP数据访问助手
        /// </summary>
        protected LmErpDb erpDbAccess = null;
        public HwCollaborationMaterialBase(string modulename, string apiUrl) : base(modulename, apiUrl)
        {
            erpDbAccess = new LmErpDb();
        }
        /// <summary>
        /// 自动从ERP中获取数据
        /// </summary>
        /// <returns></returns>
        public virtual T AutoGetDatasFromErp(ErpMaterialQueryCell materialQueryCell)
        {
            return default(T);
        }
    }

    /// <summary>
    /// 华为物料配置数据字典
    /// </summary>
    public class HwMaterialConfigDataDic
    {
        /// <summary>
        /// 成品
        /// </summary>
        public const string FG = "FG";
        /// <summary>
        /// 半成品
        /// </summary>
        public const string SEMI_FG = "SEMI-FG";
        /// <summary>
        /// 原材料
        /// </summary>
        public const string RM = "RM";
    }
}
