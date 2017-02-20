using Lm.Eic.App.DomainModel.Mes.Optical.ProductShipping;
using Lm.Eic.Uti.Common.YleeDbHandler;

namespace Lm.Eic.App.DbAccess.Mes.Optical.ProductShippingDb
{
    /// <summary>
    /// 出货排程数据持久化
    /// </summary>
    public interface IShippingScheduleRep : IRepository<PromShippingScheduleModel>
    {
    }

    public class ShippingScheduleRep : OpticalMesRepositoryBase<PromShippingScheduleModel>, IShippingScheduleRep
    {
    }
}