using Lm.Eic.Framework.ProductMaster.Model.MessageNotify;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Business.MessageNotify
{
    public class NotifyModuleManager
    {
        public OpResult StoreNotifyInfo(ConfigNotifyAddressModel modelData)
        {
            return ConfigNotifyAddressCrudFactory.ConfigNotifyAddressCrud.Store(modelData);
        }
        public List<ConfigNotifyAddressModel> GetConfigNotifyAddressBy(string finddata)
        {
            return null;
        }
    }
}
