using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;

namespace Lm.Eic.App.Business.Bmp.Hrm.Archives
{
    /// <summary>
    /// 员工管理者
    /// </summary>
    public class WorkerQueryManager
    {
        /// <summary>
        /// 获取统计分析Dto
        /// </summary>
        /// <returns></returns>
        public CompanyWorkerAnalogDto GetWorkerAnalogDatas()
        {
            var workers = ArchiveService.ArchivesManager.FindWorkers();

            CompanyWorkerAnalogDto workerDto = new CompanyWorkerAnalogDto()
            {
                Departments = new List<DepartmentAnalogDto>(),
                Name = "公司",
                Count = workers.Count
            };

            ArDepartmentManager departmentManager = new ArDepartmentManager();
            if (workers == null) return workerDto;
            List<string> Departments = workers.Select(f => f.Department).Distinct().ToList();

            Departments.ForEach(d =>
            {
                var workersOfDepartment = workers.FindAll(e => e.Department == d);
                DepartmentAnalogDto dto = new DepartmentAnalogDto()
                {
                    Name = d,
                    Department = departmentManager.GetDepartmentText(d),
                    Count = workersOfDepartment.Count(f => f.Department == d),
                    CountOfPostList = GetPostAnalogData(workersOfDepartment)
                };
                workerDto.Departments.Add(dto);
            });
            return workerDto;
        }

        private List<PostAnalogDto> GetPostAnalogData(List<ArWorkerInfo> departments)
        {
            List<PostAnalogDto> datas = new List<PostAnalogDto>();
            var posts = departments.Select(f => f.Post).Distinct().ToList();
            posts.ForEach(p =>
            {
                datas.Add(new PostAnalogDto() { Name = p, Count = departments.Count(f => f.Post == p) });
            });
            return datas;
        }

    }
    public class AnalogDto
    {
        public string Name { get; set; }

        public int Count { get; set; }
    }

    /// <summary>
    /// 员工统计Dto
    /// </summary>
    public class DepartmentAnalogDto : AnalogDto
    {
        public string Department { get; set; }
        /// <summary>
        /// 岗位统计数量列表
        /// </summary>
        public List<PostAnalogDto> CountOfPostList { get; set; }
    }
    public class PostAnalogDto : AnalogDto
    {
        /// <summary>
        /// 岗位性质
        /// </summary>
        public string PostNature { get; set; }
    }

    public class CompanyWorkerAnalogDto : AnalogDto
    {
        public List<DepartmentAnalogDto> Departments { get; set; }
    }
}
