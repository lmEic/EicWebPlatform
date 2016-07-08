using Lm.Eic.App.DbAccess.Mes.Optical.ProductWipDb;
using Lm.Eic.App.DomainModel.Mes.Optical.ProductWip;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System.Collections.Generic;
using System.Linq;

namespace Lm.Eic.App.Business.Mes.Optical.ProductWip
{
    /// <summary>
    /// WIP数据管理器
    /// </summary>
    public class ProductWipDataManager
    {
        private IProductWipDataRep irep = null;

        public ProductWipDataManager()
        {
            this.irep = new ProductWipDataRep();
        }

        /// <summary>
        /// 正常站别设置器
        /// </summary>
        public WipNormalFlowStationSetter NormalFlowStationSetter
        {
            get
            {
                return OBulider.BuildInstance<WipNormalFlowStationSetter>();
            }
        }

        public List<ProductedWipDataModel> LoadBy(string productType)
        {
            return irep.Entities.Where(e => e.ProductType == productType).ToList();
        }
    }

    /// <summary>
    /// WIP正常站别设置器
    /// </summary>
    public class WipNormalFlowStationSetter
    {
        private IWipNormalFlowStationSetRep irep = null;

        public WipNormalFlowStationSetter()
        {
            this.irep = new WipNormalFlowStationSetRep();
        }

        public List<WipNormalFlowStationSetModel> LoadBy(string productType)
        {
            return irep.Entities.Where(e => e.ProductType == productType && e.StationSign == "中继站").OrderBy(o => o.FlowID).ToList();
        }
    }
}