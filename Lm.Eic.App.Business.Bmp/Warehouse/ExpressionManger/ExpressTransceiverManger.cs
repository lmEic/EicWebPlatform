using Lm.Eic.App.DomainModel.Bpm.Warehouse;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Warehouse.ExpressionManger
{
 public  class ExpressTransceiverManger
    {
        public OpResult StoreExpressModel(ExpressModel model)
        {
            return WarehouseCrudFactory.ExpressCrud.Store(model, true);
        }
    }
}
