using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.App.DbAccess.Mes.Optical;
using Lm.Eic.App.DomainModel.Mes.Optical.ProductShipping;

namespace Lm.Eic.App.DbAccess.Mes.Optical.ProductShippingDb
{
   /// <summary>
   /// 出货排程数据持久化
   /// </summary>
   public interface IShippingScheduleRep:IRepository<PromShippingScheduleModel>
    {
    }
   public class ShippingScheduleRep : OpticalMesRepositoryBase<PromShippingScheduleModel>, IShippingScheduleRep
   {
       
   }
}
