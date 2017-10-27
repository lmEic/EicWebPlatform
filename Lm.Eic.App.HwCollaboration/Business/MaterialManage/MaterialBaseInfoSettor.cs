using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.HwCollaboration.Business;
using Lm.Eic.App.HwCollaboration.Model;
using Lm.Eic.App.HwCollaboration.Model.LmErp;
using Lm.Eic.Uti.Common.YleeOOMapper;

namespace Lm.Eic.App.HwCollaboration.Business.MaterialManage
{
    /// <summary>
    /// 物料基础信息设置器
    /// </summary>
    public class MaterialBaseInfoSettor : HwCollaborationMaterialConfigBase<VendorItemRelationDto>
    {
        public MaterialBaseInfoSettor() : base(HwModuleName.MaterialBaseInfo, HwAccessApiUrl.MaterialBaseInfoApiUrl)
        {
        }
        /// <summary>
        /// 同步数据
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public OpResult SynchronizeDatas(List<HwCollaborationDataConfigModel> entities)
        {
            bool result = true;
            if (entities == null || entities.Count == 0)
            {
                return OpResult.SetErrorResult("配置数据实体模型不能为null！");
            }
            foreach (var e in entities)
            {
                var opResult = StoreEntity(e, e.MaterialBaseDataContent, m => this.configDbAccess.StoreMaterialBase(m));
                result = result && opResult.Result;
                if (!result) return opResult;
            }
            return OpResult.SetSuccessResult("向华为系统平台发送配置数据成功！");
        }
    }

    /// <summary>
    /// 关键物料BOM信息管理器
    /// </summary>
    public class MaterialKeyBomManager : HwCollaborationMaterialConfigBase<KeyMaterialDto>
    {
        public MaterialKeyBomManager() : base(HwModuleName.MaterialKeyBom, HwAccessApiUrl.MaterialKeyBomApiUrl)
        {
        }
        /// <summary>
        /// 同步数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public OpResult SynchronizeDatas(HwCollaborationDataConfigModel entity)
        {
            if (entity == null)
            {
                return OpResult.SetErrorResult("配置数据实体模型不能为null！");
            }
            return this.StoreEntity(entity, entity.MaterialBomDataContent, m => configDbAccess.StoreMaterialBom(m));
        }
    }
}
