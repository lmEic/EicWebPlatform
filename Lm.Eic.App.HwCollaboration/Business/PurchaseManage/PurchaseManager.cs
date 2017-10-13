using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.HwCollaboration.Business;
using Lm.Eic.App.HwCollaboration.Model;
using Lm.Eic.App.HwCollaboration.Model.LmErp;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.App.HwCollaboration.Business.MaterialManage;

namespace Lm.Eic.App.HwCollaboration.Business.PurchaseManage
{
    public class PurchaseManager : HwCollaborationMaterialBase<SccOpenPOVO>
    {
        public PurchaseManager(string modulename, string apiUrl) : base(modulename, apiUrl)
        {

        }

        public override SccOpenPOVO AutoGetDatasFromErp(ErpMaterialQueryCell materialQueryCell)
        {
            return base.AutoGetDatasFromErp(materialQueryCell);
        }
    }
}
