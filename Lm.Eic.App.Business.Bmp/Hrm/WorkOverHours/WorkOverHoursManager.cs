using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.WorkOverHours;
using Lm.Eic.App.DomainModel.Bpm.Hrm.WorkOverHours;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Hrm.WorkOverHours
{
   public class WorkOverHoursManager
   {
       /// <summary>
       /// 查询(1、按日期查询 2、按部门查询)
       /// </summary>
       /// <param name="Dto"></param>
       /// <returns></returns>
        public List<WorkOverHoursMangeModels> FindRecordBy(WorkOverHoursDto Dto)
        {
            return WorkOverHoursFactory.WorkOverHoursCrud.FindBy(Dto);
        }
        /// <summary>
        /// 载入模板
        /// </summary>
        /// <param name="workDate"></param>
        /// <param name="departmentText"></param>
        /// <returns></returns>
        public List<WorkOverHoursMangeModels>FindRecordByModel(string departmentText,DateTime workDate)
        {
            return WorkOverHoursFactory.WorkOverHoursCrud.FindByMode(departmentText, workDate);
        }
        /// <summary>
        /// 批量保存数据
        /// </summary>
        /// <param name="workOverHourss"></param>
        /// <returns></returns>
       public OpResult HandleWorkOverHoursDatas(List<WorkOverHoursMangeModels>workOverHourss)
        {
            if(workOverHourss==null)return OpResult.SetErrorResult("列表不能为空");
            bool result = true;
            try
            {
                workOverHourss.ForEach(m =>
                {
                    result = result && WorkOverHoursFactory.WorkOverHoursCrud.Store(m).Result;
                });                                                                                  
            }
            catch (Exception ex)
            {
                return ex.ExOpResult();
            }
            return OpResult.SetResult("批量保储数据成功！", result);

        }

        /// <summary>
        /// 导入excel
        /// </summary>
        /// <param name="execlPath"></param>
        /// <returns></returns>
       public List<WorkOverHoursMangeModels>ImportWorkOverHoursListBy(string  execlPath)
        {
            return execlPath.GetEntitiesFromExcel<WorkOverHoursMangeModels>();

        }
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>

        public DownLoadFileModel WorkOverHoursDatasDLFM(List<WorkOverHoursMangeModels>datas)
        {
            try
            {
                if (datas == null || datas.Count < 0) return new DownLoadFileModel().Default();
                var dataGroupping = datas.GetGroupList<WorkOverHoursMangeModels>();
                return dataGroupping.ExportToExcelMultiSheets<WorkOverHoursMangeModels>(CreateFieldMapping()).CreateDownLoadExcelFileModel("加班人员清单");

            }
            catch (Exception)
            {

                throw;
            }

        }
        private List<FileFieldMapping> CreateFieldMapping()
        {
            List<FileFieldMapping> fieldmapping = new List<FileFieldMapping>()
            {
                new FileFieldMapping("WorkDate","日期"),
                new FileFieldMapping("DepartmentText","部门"),
                new FileFieldMapping("WorkClassType","班别"),
                new FileFieldMapping("WorkerId","工号"),
                new FileFieldMapping("WorkerName","姓名"),
                new FileFieldMapping("WorkOverHours","时数"),                         
                new FileFieldMapping("Remark","备注")
                
            };
            return fieldmapping;

        }

   }
}
