using Lm.Eic.App.DomainModel.Mes.Optical.ProductWip;
using Lm.Eic.Uti.Common.YleeDbHandler;

namespace Lm.Eic.App.DbAccess.Mes.Optical.ProductWipDb
{
    public interface IProductWipDataRep : IRepository<ProductedWipDataModel>
    {
    }

    public class ProductWipDataRep : OpticalMesRepositoryBase<ProductedWipDataModel>, IProductWipDataRep
    { }

    public interface IWipNormalFlowStationSetRep : IRepository<WipNormalFlowStationSetModel>
    {
    }

    public class WipNormalFlowStationSetRep : OpticalMesRepositoryBase<WipNormalFlowStationSetModel>, IWipNormalFlowStationSetRep
    { }
}