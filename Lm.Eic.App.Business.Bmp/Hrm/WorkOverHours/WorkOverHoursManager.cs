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
        /// 查询汇总(1、按部门汇总查询，2、工号汇总)
        /// </summary>
        /// <param name="Dto"></param>
        /// <returns></returns>
        public List<WorkOverHoursMangeModels> FindRecordBySum(WorkOverHoursDto Dto)
        {
            var  modelList = new List<WorkOverHoursMangeModels>();
            List<WorkOverHoursMangeModels> GetWorkOverHoursList = WorkOverHoursFactory.WorkOverHoursCrud.FindBySum(Dto); 
         
            foreach (var item in GetWorkOverHoursList)
            {
               if(modelList.FirstOrDefault(m=>m.WorkerId==item.WorkerId&&m.WorkoverType==item.WorkoverType)==null) 
                {
                    var temModel = new WorkOverHoursMangeModels();
                    temModel.WorkerId = item.WorkerId;
                    temModel.WorkerName = item.WorkerName;
                    temModel.DepartmentText = item.DepartmentText;
                    temModel.WorkoverType = item.WorkoverType;
                    temModel.WorkDate = item.WorkDate;//2018/1/2 0:00:00
                    temModel.WorkClassType = item.WorkClassType;
                    temModel.WorkOverHours = item.WorkOverHours;
                    temModel.WorkOverHoursCount = GetWorkOverHoursList.Where(m => m.WorkoverType == item.WorkoverType && m.WorkerId == item.WorkerId).Sum(m => m.WorkOverHours);              
                    temModel.Remark = item.Remark;
                    temModel.WorkReason = item.WorkReason;
                    temModel.WorkDayTime = item.WorkDayTime;
                    temModel.WorkNightTime = item.WorkNightTime;
                    temModel.WorkStatus = item.WorkStatus;
                    temModel.QryDate = item.QryDate;
                    modelList.Add(temModel);
                }             
            }
            return modelList;
        }
        /// <summary>
        /// 查询个人明细
        /// </summary>
        /// <param name="qDto"></param>
        /// <returns></returns>
        public List<WorkOverHoursMangeModels>FindRecordByDetail(WorkOverHoursDto qDto)
        {
            return WorkOverHoursFactory.WorkOverHoursCrud.FindBySum(qDto);
        }

        /// <summary>
        /// 载入模板
        /// </summary>
        /// <param name="workDate"></param>
        /// <param name="departmentText"></param>
        /// <returns></returns>
        public List<WorkOverHoursMangeModels>FindRecordByModel(string departmentText,string postNature,DateTime workDate,int mode)
        {
            return WorkOverHoursFactory.WorkOverHoursCrud.FindByMode(departmentText,postNature, workDate,mode);
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
            return OpResult.SetResult("批量保存数据成功！", result);

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
        public DownLoadFileModel WorkOverHoursDatasDLFM(List<WorkOverHoursMangeModels>datas,string  SiteRootPath,string  filePath,string  fileName)
        {
            try
            {
                if (datas == null || datas.Count < 0) return new DownLoadFileModel().Default();
                var dataGroupping = datas.GetGroupList<WorkOverHoursMangeModels>();             
                 return dataGroupping.WorkOverHoursListToExcel<WorkOverHoursMangeModels>(CreateFieldMapping(), filePath).WorkOverExcelTemplae("加班报表"); 
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public DownLoadFileModel WorkOverHoursDatasSumDLFM(List<WorkOverHoursMangeModels> datas, string SiteRootPath1, string filePath1, string fileName1)
        {
            try
            {
                if (datas == null || datas.Count < 0) return new DownLoadFileModel().Default();
                var dataGroupping = datas.GetGroupList<WorkOverHoursMangeModels>();

                return dataGroupping.WorkOverHoursListToExcelSum<WorkOverHoursMangeModels>(CreateFieldMapping(), filePath1).WorkOverExcelTemplae("汇总报表");
           

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        private List<FileFieldMapping> CreateFieldMapping()
        {
            List<FileFieldMapping> fieldmapping = new List<FileFieldMapping>()
            {
                new FileFieldMapping("WorkDate","日期"),
                new FileFieldMapping("WorkerId","工号"),
                new FileFieldMapping("WorkerName","姓名"),
                new FileFieldMapping("WorkOverHours","时数"),
                new FileFieldMapping("DepartmentText","部门"),
                new FileFieldMapping("WorkoverType","加班类型"),
                new FileFieldMapping("WorkClassType","班别"),                                         
                new FileFieldMapping("Remark","备注"),
                new FileFieldMapping("WorkClassType","班别"),
                new FileFieldMapping("WorkDate","申请日期"),
                new FileFieldMapping("WorkStatus","员工状态"),
                new FileFieldMapping("QryDate","年月份"),
                new FileFieldMapping("WorkReason","加班原因"),
                new FileFieldMapping("WorkDayTime","白班日期"),
                new FileFieldMapping("WorkNightTime","晚班日期")
               // new FileFieldMapping("WorkOverHoursCount","加班总时数")
              



            };
            return fieldmapping;

        }
        /// <summary>
        /// 后台编辑保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreWorkOverHours(WorkOverHoursMangeModels model)
        {
            try
            {
                return WorkOverHoursFactory.WorkOverHoursCrud.Store(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="departmentText"></param>
        /// <param name="workDate"></param>
        /// <returns></returns>
        public OpResult HandleDeleteWorkOverHours(string departmentText,DateTime workDate)
        {
            return WorkOverHoursFactory.WorkOverHoursCrud.HandleDeleteWorkOverHours(departmentText, workDate);
        }



   }
}
