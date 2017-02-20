using Lm.Eic.App.Business.Bmp.Hrm.Archives;
using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Attendance;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Attendance;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lm.Eic.App.Business.Bmp.Hrm.Attendance
{
    public class AttendClassTypeSetter
    {
        #region member

        private IAttendClassTypeRepository irep = null;
        private IAttendClassTypeDetailRepository irepDetail = null;
        private AttendSlodFingerDataCurrentMonthHandler attendanceHandler = null;
        #endregion member

        #region constructure

        public AttendClassTypeSetter()
        {
            this.irep = new AttendClassTypeRepository();
            this.irepDetail = new AttendClassTypeDetailRepository();
            this.attendanceHandler = new AttendSlodFingerDataCurrentMonthHandler();
        }

        #endregion constructure

        #region method
        /// <summary>
        /// 初始化班别信息,存储到班次明细信息中
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InitClassType(AttendClassTypeDetailModel entity)
        {
            int record = 0;
            if (this.irepDetail.IsExist(e => e.WorkerId == entity.WorkerId))
            {
                this.irepDetail.Delete (e => e.WorkerId == entity.WorkerId);
            }
            record = this.irepDetail.Insert(entity);
            return record;
        }
        /// <summary>
        /// 根据部门载入班别信息
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<AttendClassTypeModel> LoadDatasBy(string department, string workerId = null, string classType = null)
        {
            List<AttendClassTypeModel> getDatas = new List<AttendClassTypeModel>();
            try
            {
                QueryWorkersDto qry = new QueryWorkersDto() { Department = department, WorkerId = workerId };
                //在档案中载入部门数据
                List<ArWorkerInfo> workers = workerId == null ? ArchiveService.ArchivesManager.FindWorkers(qry, 1) : ArchiveService.ArchivesManager.FindWorkers(qry, 2);
                //在班别设置中载入部门班别数据信息
                List<AttendClassTypeModel> classTypes = GetClassTypeDatas(department, workerId, classType);
                AttendClassTypeModel mdl = null;
                //合并数据
                workers.ForEach(w =>
                {
                    mdl = new AttendClassTypeModel();
                    var ct = classTypes.FirstOrDefault(c => c.WorkerId == w.WorkerId);
                    if (ct != null)
                    {
                        //根据部门信息添加数据
                        getDatas.Add(ct);
                        //移除要搜索的数据，减少查询时间
                        classTypes.Remove(ct);
                    }
                    else
                    {
                        mdl.WorkerId = w.WorkerId;
                        mdl.WorkerName = w.Name;
                        mdl.Department = w.Department;
                        mdl.ClassType = w.ClassType;
                        getDatas.Add(mdl);
                    }
                });
                //清空班别设置中离职人员数据信息
                if (classTypes != null && classTypes.Count > 0)
                {
                    classTypes.ForEach(c =>
                    {
                        var item = workers.FirstOrDefault(e => e.WorkerId == c.WorkerId);
                        //人员信息列表中已经找不到此数据，则清空
                        if (item == null)
                        {
                            this.irep.Delete(d => d.WorkerId == c.WorkerId);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            getDatas = getDatas.OrderBy(e => e.WorkerId).ToList();
            return getDatas;
        }
        private List<AttendClassTypeModel> GetClassTypeDatas(string department, string workerId, string classType)
        {
            List<AttendClassTypeModel> classTypes = null;
            //若有工号值则按照工号进行查询
            if (workerId != null)
            {
                classTypes = this.irep.Entities.Where(e => e.WorkerId == workerId).OrderBy(w => w.WorkerId).ToList();
            }
            else
            {
                if (classType == null)
                {
                    classTypes = this.irep.Entities.Where(e => e.Department == department).OrderBy(w => w.WorkerId).ToList();
                }
                else
                {
                    classTypes = this.irep.Entities.Where(e => e.Department == department && e.ClassType == classType).OrderBy(w => w.WorkerId).ToList();
                }
            }
            return classTypes;
        }

        /// <summary>
        /// 设置班别数据
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public OpResult SetClassType(List<AttendClassTypeModel> entities, string opPerson)
        {
            int record = 0;
            try
            {
                if (entities == null || entities.Count == 0) return OpResult.SetResult("entities can't be null", false);
                AttendClassTypeDetailModel mdl = null;
                AttendClassTypeModel ctMdl = null;
                entities.ForEach(dto =>
                {
                    dto.DateFrom = dto.DateFrom.AddDays(1).ToDate();
                    dto.DateTo = dto.DateTo.AddDays(1).ToDate();
                    dto.OpDate = DateTime.Now;
                    dto.IsAlwaysDay = "否";
                    dto.OpPerson = opPerson;
                    StoreClassTypeData(ref record, ctMdl, dto);
                    StoreClassTypeDetailData(ref record, mdl, dto);
                    //同步总表中的班别信息
                    ArchiveService.ArchivesManager.ChangeClassType(dto.WorkerId, dto.ClassType);
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return OpResult.SetResult("设置班别成功!", record > 0);
        }
        private void StoreClassTypeData(ref int record,AttendClassTypeModel ctMdl,AttendClassTypeModel dto)
        {
            ctMdl = this.irep.Entities.FirstOrDefault(e => e.WorkerId == dto.WorkerId);
            if (ctMdl != null)
            {
                dto.Id_Key = ctMdl.Id_Key;
                //如果存在，则直接修改信息
                record += this.irep.Update(u => u.WorkerId == dto.WorkerId, dto);
            }
            else
            {
                //不存在，则直接添加
                record += this.irep.Insert(dto);
            }
        }
        private void StoreClassTypeDetailData(ref int record, AttendClassTypeDetailModel mdl, AttendClassTypeModel dto)
        {
            DateTime dateFrom = dto.DateFrom;
            DateTime dateTo = dto.DateTo;
            while (dateFrom <= dateTo)
            {
                mdl = this.irepDetail.Entities.FirstOrDefault(e => e.WorkerId == dto.WorkerId && e.DateAt == dateFrom);
                if (mdl != null)
                {
                    mdl.ClassType = dto.ClassType;
                    //如果存在，则直接修改信息
                    record += this.irepDetail.Update(u => u.WorkerId == dto.WorkerId && u.DateAt == dateFrom, mdl);
                }
                else
                {
                    mdl = new AttendClassTypeDetailModel()
                    {
                        ClassType = dto.ClassType,
                        DateAt = dateFrom,
                        Department = dto.Department,
                        IsAlwaysDay = dto.IsAlwaysDay,
                        OpPerson = dto.OpPerson,
                        WorkerId = dto.WorkerId,
                        WorkerName = dto.WorkerName,
                        OpDate=dto.DateFrom,
                        OpSign="add"
                    };
                    //不存在，则直接添加
                    record += this.irepDetail.Insert(mdl);
                }
                //同步汇总考勤中的数据信息，待写
                this.attendanceHandler.UpdateClassTypeInfo(dto.ClassType, dateFrom);
                dateFrom = dateFrom.AddDays(1);
            }
        }
        /// <summary>
        /// 获取班次日期模型
        /// </summary>
        /// <param name="workerId"></param>
        /// <param name="dateAt"></param>
        /// <returns></returns>
        internal AttendClassTypeDetailModel GetClassTypeDetailModel(string workerId, DateTime dateAt)
        {
            return this.irepDetail.FirstOfDefault(e => e.WorkerId == workerId && e.DateAt == dateAt);
        }
        #endregion method
    }
}