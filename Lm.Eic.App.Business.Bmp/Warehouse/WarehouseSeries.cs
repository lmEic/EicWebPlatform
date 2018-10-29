using Lm.Eic.App.Business.Bmp.Warehouse.ExpressionManger;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Warehouse
{
   public  class WarehouseSeries
    {
        public static ExpressTransceiverManger ExpressTransceiverManger
        {
            get { return OBulider.BuildInstance<ExpressTransceiverManger>(); }
        }

    }
}
