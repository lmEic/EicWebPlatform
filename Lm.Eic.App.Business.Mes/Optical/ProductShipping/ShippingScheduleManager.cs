using Lm.Eic.App.DbAccess.Mes.Optical.ProductShippingDb;
using Lm.Eic.App.DomainModel.Mes.Optical.ProductShipping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lm.Eic.App.Business.Mes.Optical.ProductShipping
{
    /// <summary>
    /// 排程计划负责管理器
    /// </summary>
    public class ShippingScheduleManager
    {
        private IShippingScheduleRep irep;

        public ShippingScheduleManager()
        {
            irep = new ShippingScheduleRep();
        }

        public List<PromShippingScheduleModel> ShippingScheduleDatas
        {
            get
            {
                return irep.Entities.Where(f => f.ShippingDate >= DateTime.Now).ToList();
            }
        }

        public List<PromShippingScheduleModel> GetShippingProductTypes()
        {
            List<PromShippingScheduleModel> datas = new List<PromShippingScheduleModel>();
            var productTypeList = this.ShippingScheduleDatas.Select(e => e.ProductType).Distinct().ToList();
            productTypeList.ForEach(ptype =>
            {
                var mdlsumerize = this.ShippingScheduleDatas.Where(p => p.ProductType == ptype).ToList();
                var total = mdlsumerize.Sum(s => s.ShippingCount);
                datas.Add(new PromShippingScheduleModel() { ProductType = ptype, ShippingCount = total, ProductCatalog = mdlsumerize[0].ProductCatalog });
            });
            return datas;
        }
    }
}