using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.App.Business.Mes.Optical.ProductShipping
{
    public static class ShippingService
    {
        /// <summary>
        /// 出货排程计划管理器
        /// </summary>
        public static ShippingScheduleManager ScheduleManager
        {
            get { return OBulider.BuildInstance<ShippingScheduleManager>(); }
        }
    }
}