using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.App.Business.Bmp.Hrm.Archives
{
    public class ArchiveService
    {
        /// <summary>
        /// 档案管理者
        /// </summary>
        public static ArchivesManager ArchivesManager
        {
            get { return OBulider.BuildInstance<ArchivesManager>(); }
        }

        /// <summary>
        /// 生产作业人员管理器
        /// </summary>
        public static ProWorkerManager ProWorkerManager
        {
            get
            {
                return OBulider.BuildInstance<ProWorkerManager>();
            }
        }


        /// <summary>
        /// 生产行事历管理器
        /// </summary>
        public static ArCalendarManger ArCalendarManger
        {
            get
            {
                return OBulider.BuildInstance<ArCalendarManger>();
            }
        }
    }
}