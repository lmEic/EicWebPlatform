using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.Business.Bmp.Quantity.SampleManger;
using Lm.Eic.App.DbAccess.Bpm.Repository.QuantityRep;

namespace Lm.Eic.App.Business.Bmp.Quantity.SampleManager.IPQCSample
{
    public class IPQCSampleItems
    {
        ISampleItemsIpqcDataReosity irep = null;
        public IPQCSampleItems()
        {
            irep = new SampleItemsIpqcDataReosity();
        }
    }
}
