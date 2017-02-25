using Lm.Eic.App.DbAccess.Bpm.Repository.QuantityRep;

namespace Lm.Eic.App.Business.Bmp.Quantity.SampleManager
{
    public class IPQCSampleItemsManager
    {
        ISampleItemsIpqcDataReosity irep = null;
        public IPQCSampleItemsManager()
        {
            irep = new SampleItemsIpqcDataReosity();
        }
    }
}
