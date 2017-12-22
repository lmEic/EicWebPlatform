using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.App.Business.Bmp.Hrm.GeneralAffairs
{
    /// <summary>
    /// 总务管理工厂
    /// </summary>
    internal static class GeneralAffairsFactory
    {
        /// <summary>
        /// 厂服管理CRUD
        /// </summary>
        internal static WorkerClothesCrud WorkerClothesCrud
        {
            get { return OBulider.BuildInstance<WorkerClothesCrud>(); }
        }
        /// <summary>
        /// 报餐存储
        /// </summary>
        internal static ReportMealStore ReportMealStore
        {
            get { return OBulider.BuildInstance<ReportMealStore>(); }
        }
    }
}
