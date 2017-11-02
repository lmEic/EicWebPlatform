using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Hrm.Archives
{
    /// <summary>
    /// 员工管理者
    /// </summary>
    public class WorkerManager
    {
        //public WorkerAnalogDto GetAnalogDto()
        //{
        //    var workers = ArchiveService.ArchivesManager.FindWorkers();
        //    List<string> Departments = workers.Select(f => f.Department).Distinct().ToList();

        //}

    }

    /// <summary>
    /// 员工统计Dto
    /// </summary>
    public class WorkerAnalogDto
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 该部门人员总数量
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 岗位统计数量列表
        /// </summary>
        public Dictionary<string, int> CountOfPostList { get; set; }
    }
}
