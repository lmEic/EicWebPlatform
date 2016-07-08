using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.Framework.ProductMaster.Business.Config
{
    public static class PmConfigService
    {
        /// <summary>
        /// 数据字典管理器
        /// </summary>
        public static DataDictionaryManager DataDicManager
        {
            get
            {
                return OBulider.BuildInstance<DataDictionaryManager>();
            }
        }

        /// <summary>
        /// 文件路径管理器
        /// </summary>
        public static FilePathManager FilePathManager
        {
            get { return OBulider.BuildInstance<FilePathManager>(); }
        }
    }
}