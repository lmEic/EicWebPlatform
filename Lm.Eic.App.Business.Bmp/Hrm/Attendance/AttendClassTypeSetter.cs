using Lm.Eic.App.Business.Bmp.Hrm.Archives;
using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Attendance;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Attendance;
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

        #endregion member

        #region constructure

        public AttendClassTypeSetter()
        {
            this.irep = new AttendClassTypeRepository();
        }

        #endregion constructure



        #region method

        /// <summary>
        /// 初始化班别信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InitClassType(AttendClassTypeModel entity)
        {
            int record = 0;
            if (!this.irep.IsExist(e => e.WorkerId == entity.WorkerId))
            {
                record = this.irep.Insert(entity);
            }
            return record;
        }

        /// <summary>
        /// 根据部门载入班别信息
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<AttendClassTypeModel> LoadDatasBy(string department)
        {
            List<AttendClassTypeModel> getDatas = new List<AttendClassTypeModel>();
            try
            {
                QueryWorkersDto qry = new QueryWorkersDto() { Department = department };
                //在档案中载入部门数据
                List<ArWorkerInfo> workers = ArchiveService.ArchivesManager.FindWorkers(qry, 1);
                //在班别设置中载入部门班别数据信息
                List<AttendClassTypeModel> classTypes = this.irep.Entities.Where(e => e.Department == department).OrderBy(w => w.WorkerId).ToList();

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

            return getDatas;
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
                entities.ForEach(entity =>
                {
                    entity.OpDate = DateTime.Now;
                    entity.IsAlwaysDay = "否";
                    entity.OpPerson = opPerson;
                    var existMdl = this.irep.Entities.FirstOrDefault(e => e.WorkerId == entity.WorkerId);
                    if (existMdl != null)
                    {
                        entity.Id_Key = existMdl.Id_Key;
                        //如果存在，则直接修改信息
                        record += this.irep.Update(u => u.WorkerId == entity.WorkerId, entity);
                    }
                    else
                    {
                        //不存在，则直接添加
                        record += this.irep.Insert(entity);
                    }
                    //同步总表中的班别信息
                    ArchiveService.ArchivesManager.ChangeClassType(entity.WorkerId, entity.ClassType);
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return OpResult.SetResult("设置班别成功!", record > 0);
        }

        #endregion method
    }
}