using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.App.DbAccess.Mes.Optical;
using Lm.Eic.App.DomainModel.Mes.Optical.ProductWip;

namespace Lm.Eic.App.DbAccess.Mes.Optical.ProductWipDb
{
   public interface IProductWipDataRep:IRepository<ProductedWipDataModel>
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
