using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.App.Business.Bmp.Hrm.GeneralAffairs
{
    /// <summary>
    /// 总务管理工厂
    /// </summary>
    public static class GeneralAffairsFactory
    {
        /// <summary>
        /// 厂服管理CRUD
        /// </summary>
        internal static WorkerClothesCrud WorkerClothesCrud
        {
            get { return OBulider.BuildInstance<WorkerClothesCrud>(); }
        }
    }
}
