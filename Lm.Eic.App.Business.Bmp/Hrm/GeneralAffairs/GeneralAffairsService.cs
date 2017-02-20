using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.App.Business.Bmp.Hrm.GeneralAffairs
{
    /// <summary>
    /// 总务管理服务接口门面
    /// </summary>
    public class GeneralAffairsService
    {
        /// <summary>
        /// 厂服管理器
        /// </summary>
        public static WorkerClothesManager WorkerClothesManager
        {
            get { return OBulider.BuildInstance<WorkerClothesManager>(); }
        }
    }
}
