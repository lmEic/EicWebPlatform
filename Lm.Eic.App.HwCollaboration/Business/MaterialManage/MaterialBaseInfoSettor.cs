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
    public class MaterialBaseInfoSettor : HwCollaborationBase<VendorItemRelationDto>
    {
        public MaterialBaseInfoSettor() : base(HwModuleName.MaterialBaseInfo, HwAccessApiUrl.MaterialBaseInfoApiUrl)
        {
        }


        public override OpResult SynchronizeDatas(HwCollaborationDataTransferModel entity)
        {
            return this.SynchronizeDatas(this.apiUrl, entity, model => { return OpResult.SetSuccessResult("成功！"); });
        }

        public OpResult tt()
        {
            HwCollaborationDataTransferModel d = new HwCollaborationDataTransferModel()
            {
                OpContent = ObjectSerializer.SerializeObject(HwMockDatas.VendorItems)
            };
            return this.SynchronizeDatas(d);
        }
    }

    /// <summary>
    /// 关键物料BOM信息管理器
    /// </summary>
    public class MaterialKeyBomManager : HwCollaborationBase<KeyMaterialDto>
    {
        public MaterialKeyBomManager() : base(HwModuleName.MaterialKeyBom, HwAccessApiUrl.MaterialKeyBomApiUrl)
        {
        }
    }
}
