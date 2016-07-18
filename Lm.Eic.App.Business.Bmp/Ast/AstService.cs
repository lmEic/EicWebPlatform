using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.App.Business.Bmp.Ast
{
    public class AstService
    {
        /// <summary>
        /// 设备管理器
        /// </summary>
        public static EquipmentManager EquipmentManager
        {
            get { return OBulider.BuildInstance<EquipmentManager>(); }
        }

        /// <summary>
        /// 校验管理器
        /// </summary>
        public static EquipmentCheckManager EquipmentCheckManager
        {
            get { return OBulider.BuildInstance<EquipmentCheckManager>(); }
        }
    }
}