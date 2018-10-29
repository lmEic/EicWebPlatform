﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Attendance;
using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Attendance;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;

namespace Lm.Eic.App.Business.Bmp.Hrm.Attendance
{
    /// <summary>
    /// 考勤Crud工厂
    /// </summary>
    internal class AttendCrudFactory
    {
        internal static AttendAskLeaveCrud AskLeaveCrud
        {
            get { return OBulider.BuildInstance<AttendAskLeaveCrud>(); }
        }
        /// <summary>
        /// 当月考勤数据操作助手
        /// </summary>
        internal static AttendSlodFingerDataCurrentMonthCurd CurrentMonthAttendDataCrud
        {
            get { return OBulider.BuildInstance<AttendSlodFingerDataCurrentMonthCurd>(); }
        }
        internal static AttendClassTypeDetailCrud ClassTypeDetailCrud
        {
            get { return OBulider.BuildInstance<AttendClassTypeDetailCrud>(); }
        }
    }
    /// <summary>
    /// 请假数据操作助手
    /// </summary>
    internal class AttendAskLeaveCrud : CrudBase<AttendAskLeaveModel, IAttendAskLeaveRepository>
    {
        public AttendAskLeaveCrud() : base(new AttendAskLeaveRepository(), "请假处理")
        {

        }

        #region crud
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, Add);
            this.AddOpItem(OpMode.Edit, Upate);
            this.AddOpItem(OpMode.Delete, Delete);
        }

        internal OpResult Add(AttendAskLeaveModel entity)
        {
            return this.irep.Insert(entity).ToOpResult_Add(this.OpContext);
        }
        internal OpResult Upate(AttendAskLeaveModel entity)
        {
            return irep.Update(e => e.Id_Key == entity.Id_Key, entity).ToOpResult_Eidt(OpContext);
        }
        internal OpResult Delete(AttendAskLeaveModel entity)
        {
            return irep.Delete(e => e.Id_Key == entity.Id_Key).ToOpResult_Delete(OpContext);
        }
        #endregion

        #region query
        internal List<AttendAskLeaveModel> GetAskLeaveDatas(string workerId, string yearMonth)
        {
            return irep.Entities.Where(e => e.YearMonth == yearMonth && e.WorkerId == workerId).ToList();
        }
        internal List<AttendAskLeaveModel> GetAskLeaveDatas(string yearMonth)
        {
            return irep.Entities.Where(e => e.YearMonth == yearMonth).ToList();
        }
        /// <summary>
        /// 获取作业员的请假条目数据
        /// </summary>
        /// <param name="workerId"></param>
        /// <param name="attendDate"></param>
        /// <returns></returns>
        internal AttendAskLeaveEntry GetAskLeaveDatasOfWorker(string workerId, DateTime attendDate, List<AttendAskLeaveModel> dataSource)
        {
            attendDate = attendDate.ToDate();
            var datas = dataSource == null ? dataSource.Where(e => e.AttendanceDate == attendDate && e.WorkerId == workerId).ToList()
                : irep.Entities.Where(e => e.AttendanceDate == attendDate && e.WorkerId == workerId).ToList();
            AttendAskLeaveEntry entry = null;
            if (datas != null && datas.Count > 0)
            {
                entry = new AttendAskLeaveEntry();
                StringBuilder leaveTypeSb = new StringBuilder();
                StringBuilder leaveDesciptionSb = new StringBuilder();
                StringBuilder leaveRegion = new StringBuilder();
                datas.ForEach(ad =>
                {
                    entry.AskLeaveHours += ad.LeaveHours;
                    leaveTypeSb.Append(ad.LeaveType + ",");
                    leaveRegion.Append(ad.LeaveTimeRegion + ",");
                    leaveDesciptionSb.AppendLine(string.Format("*假别：{0},时数：{1},时间段：{2}*", ad.LeaveType, ad.LeaveHours, ad.LeaveTimeRegion));
                });
                entry.AskLeaveType = leaveTypeSb.ToString().TrimEnd(',');
                entry.AskLeaveRegion = leaveRegion.ToString().TrimEnd(',');
                entry.AskLeaveDescription = leaveDesciptionSb.ToString().TrimEnd();
            }
            return entry;
        }
        #endregion
    }

    /// <summary>
    /// 班别管理数据操作助手
    /// </summary>
    internal class AttendClassTypeDetailCrud : CrudBase<AttendClassTypeDetailModel, IAttendClassTypeDetailRepository>
    {
        public AttendClassTypeDetailCrud() : base(new AttendClassTypeDetailRepository(), "班别操作")
        {
        }

        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取班次日期模型
        /// </summary>
        /// <param name="workerId"></param>
        /// <param name="dateAt"></param>
        /// <returns></returns>
        internal AttendClassTypeDetailModel GetClassTypeDetailModel(string workerId, DateTime dateAt)
        {
            var mdl = this.irep.FirstOfDefault(e => e.WorkerId == workerId && e.DateAt == dateAt);
            return mdl;
        }
    }

    /// <summary>
    /// 当月考勤时间数据操作助手
    /// </summary>
    public class AttendSlodFingerDataCurrentMonthCurd : CrudBase<AttendSlodFingerDataCurrentMonthModel, IAttendSlodFingerDataCurrentMonthRepository>
    {
        #region member
        private AttendFingerPrintDataInTimeHandler fingerPrintDataInTime = null;

        #endregion member

        #region constructure

        public AttendSlodFingerDataCurrentMonthCurd() : base(new AttendSlodFingerDataCurrentMonthRepository(), "考勤数据")
        {

            this.fingerPrintDataInTime = new AttendFingerPrintDataInTimeHandler();
        }

        #endregion constructure

        #region method
        #region handle attend method
        /// <summary>
        /// 获取该员工的考勤数据
        /// </summary>
        /// <param name="workerId"></param>
        /// <param name="attendDate"></param>
        /// <returns></returns>
        internal AttendSlodFingerDataCurrentMonthModel GetAttendanceDataBy(string workerId, DateTime attendDate)
        {
            return this.irep.FirstOfDefault(e => e.WorkerId == workerId && e.AttendanceDate == attendDate);
        }


        /// <summary>
        /// searchMode:
        /// 0:按考勤日期查询
        /// 1:按考勤日期与部门查询
        /// 2:按工号查询
        /// 3:按年月份查询
        /// </summary>
        /// <param name="qryDto"></param>
        /// <returns></returns>
        internal List<AttendanceDataModel> LoadAttendanceDatasBy(AttendanceDataQueryDto qryDto)
        {

            return this.irep.LoadAttendanceDatasBy(qryDto);
        }
        internal List<AttendanceDataModel> LoadAttendanceDatasBy(string  yearMonth)
        {
            List<AttendanceDataModel> returnDatas = new List<AttendanceDataModel>();
            AttendanceDataModel addmodel = null;
            AttendSlodFingerDataCurrentMonthModel model = null;
            AttendanceClassTypeInfo classTypeInfo = null;
            string year = yearMonth.Substring(0, 4);
            string month = yearMonth.Substring(4, 2);
            var wokrersInfoList= this.irep.WorkerIdList(new DateTime(year.ToInt(), month.ToInt(), 1));
            var calenderListInfo = this.irep.CalenderListInfo(year, month);
            wokrersInfoList.ForEach(e =>
            {
                DateTime WorkDate= e.LeaveDate.ToDate();
              
                foreach(var d in calenderListInfo)
                {
                    string classType = "白班";
                  
                    if (d.CalendarDate > WorkDate) continue;
                    ///当天的出勤数据
                     model = this.irep.Entities.Where(q => q.WorkerId == e.workerid && q.AttendanceDate == d.CalendarDate).FirstOrDefault();
                    ///得到当然转班的详细数据
                    classTypeInfo = this.irep.WorkerClassTypeInfoBY(e.workerid, d.CalendarDate).FirstOrDefault();
                    ///针对 晚班 做一特别的数据处理
                    if (classTypeInfo != null&&classTypeInfo.ClassType == "晚班")
                    {
                        DateTime secondLeaveWorkDate = d.CalendarDate.Date.AddDays(1).ToDate();
                        ///取第二天的时间
                        var secondmodel = this.irep.Entities.Where(q => q.WorkerId == e.workerid && q.AttendanceDate == secondLeaveWorkDate).FirstOrDefault();
                        if (secondmodel != null)
                        {
                            if (model != null)
                            {
                                ///第一天上班时间  是正常的下班时间  而 第二天下班时间  是正常的上班时间 
                                string dd = model.SlotCardTime2;
                                
                                model.SlotCardTime1 = dd;
                                model.SlotCardTime2 = secondmodel.SlotCardTime1;
                            }
                            ///第一天数据为空 而第二天的数不为空
                            else
                            {
                                model = new AttendSlodFingerDataCurrentMonthModel();
                                OOMaper.Mapper<AttendSlodFingerDataCurrentMonthModel, AttendSlodFingerDataCurrentMonthModel>(secondmodel, model);
                                string dd = secondmodel.SlotCardTime1;
                                model.AttendanceDate = d.CalendarDate;
                                model.SlotCardTime1 = "";
                                model.SlotCardTime2 = dd;
                            }
                        }
                        else
                        {
                            ///第一天的数据有 第二天的数据为空
                            if (model != null)
                            {
                              
                                string dd = model.SlotCardTime2;
                                
                                model.SlotCardTime1 = dd;

                                model.SlotCardTime2 = "";
                            }
                        ///都为空用不管    
                        }
                        classType = classTypeInfo.ClassType;
                    }
                    if (model != null)
                    {
                        addmodel = new AttendanceDataModel();
                        OOMaper.Mapper<AttendSlodFingerDataCurrentMonthModel, AttendanceDataModel>(model, addmodel);
                        addmodel.ClassType = classType;
                    }
                    else
                    {
                       string  leaveDescription= d.DateProperty == "星期六日"?" ":"无数据";
                       addmodel = new AttendanceDataModel()
                        {
                            WorkerId = e.workerid,
                            WorkerName = e.workerName,
                            Department = e.Department,
                            AttendanceDate = d.CalendarDate,
                            ClassType = classType,
                            WeekDay = ChangeWeekDay(d.CalendarWeek),
                            LeaveDescription = leaveDescription
                       };
                    };
                    returnDatas.Add(addmodel);
                }
            });
            return returnDatas;
        }
        string ChangeWeekDay(string calendarWeek)
        {
            switch (calendarWeek)
            {
                case "0":
                    return "星期日";
                case "1":
                    return "星期一";
                case "2":
                    return "星期二";
                case "3":
                    return "星期三";
                case "4":
                    return "星期四";
                case "5":
                    return "星期五";
                case "6":
                    return "星期六";
                default:
                    return calendarWeek;
            }
        }
        
        /// <summary>
        /// 载入今天的考勤数据
        /// </summary>
        /// <param name="qryDate"></param>
        /// <returns></returns>
        internal List<AttendanceDataModel> LoadAttendDataInToday(DateTime qryDate)
        {
            //TransimitAttendDatas(qryDate);
            return this.irep.LoadAttendanceDatasBy(new AttendanceDataQueryDto() { SearchMode = 0, AttendanceDate = qryDate });
        }
        internal List<AttendanceDataModel> LoadAttendDataInToday(DateTime dateFrom, DateTime dateTo, string department)
        {
            var dfrom = dateFrom.ToDate();
            var dto = dateTo.ToDate();
            return this.irep.LoadAttendanceDatasBy(new AttendanceDataQueryDto() { SearchMode = 1, DateFrom = dateFrom, DateTo = dto, Department = department });
        }
        /// <summary>
        /// 将实时考勤数据转移至本月数据表中
        /// </summary>
        /// <returns></returns>
        internal OpResult TransimitAttendDatas(DateTime qryDate)
        {
            qryDate = qryDate.ToDate();
            //处理总记录数
            int totalRecord = 0, record = 0;
            var datasInTime = this.fingerPrintDataInTime.FingPrintDatas.FindAll(e => e.SlodCardDate == qryDate);
            if (datasInTime == null || datasInTime.Count == 0) return OpResult.SetErrorResult("没有考勤数据要进行汇总");
            AttendanceDbHelpHandler.BackupData(datasInTime);
            //一次载入该日期的所有考勤数据到内存中
            var dayAttendDatas = this.irep.Entities.Where(e => e.AttendanceDate == qryDate);
            //获取所有人员信息到内存中
            var workers = AttendanceDbHelpHandler.GetWorkerInfos();
            var departments = AttendanceDbHelpHandler.GetDepartmentDatas();
            //中间时间
            DateTime middleTime = new DateTime(qryDate.Year, qryDate.Month, qryDate.Day, 13, 0, 0);
            //处理实时考勤数据
            ArWorkerInfo worker = null;
            //将考勤中数据中的人进行分组
            List<string> attendWorkerIdList = datasInTime.Select(e => e.WorkerId).Distinct().ToList();
            ClassTypeModel ctmdl = null;
            DepartmentModel depm = null;
            attendWorkerIdList.ForEach(workerId =>
            {
                record = 0;
                //获取每个人的信息
                worker = workers.FirstOrDefault(w => w.WorkerId == workerId);
                if (worker == null)
                {
                    var m = ArchiveService.ArchivesManager.WorkerIdChangeManager.GetModel(workerId);
                    if (m != null)
                        worker = workers.FirstOrDefault(w => w.WorkerId == m.NewWorkerId);
                }
                //从实时考勤数据表中获取该员工的考勤数据
                var attendDataPerWorker = datasInTime.FindAll(f => f.WorkerId == workerId).OrderBy(o => o.SlodCardTime).ToList();
                var currentAttendData = dayAttendDatas.FirstOrDefault(e => e.WorkerId == workerId);//从内存中进行查找
                if (worker != null)
                {
                    ctmdl = AttendanceDbHelpHandler.GetClassType(workerId, qryDate);
                    depm = departments.FirstOrDefault(d => d.DataNodeName == worker.Department);
                    worker.ClassType = ctmdl == null ? "白班" : ctmdl.ClassType;
                    worker.Department = depm == null ? worker.Department : depm.DataNodeText;

                    int len = attendDataPerWorker.Count;
                    for (int i = 0; i < len; i++)
                    {
                        var attendance = attendDataPerWorker[i];

                        //如果考勤数据表没有该人员的考勤数据
                        if (currentAttendData == null)
                        {
                            //则初始化考勤数据
                            record += InitAttendData(attendance, worker, attendance.SlodCardTime, out currentAttendData, middleTime);
                        }
                        else
                        {
                            //反之则合并数据
                            record += MergeAttendTime(currentAttendData, attendance.SlodCardTime);
                        }
                    }
                    if (record == len)//如果处理记录与目标数量一致则进行备份数据
                    {
                        this.fingerPrintDataInTime.BackupData(attendDataPerWorker, record);
                        //从内存中移除数据，减少查询时间
                        //workers.Remove(worker);
                        //从内存中移除该人员的考勤数据，减少查询时间
                        attendDataPerWorker.ForEach(m => datasInTime.Remove(m));
                        totalRecord += record;
                    }
                }
                else
                {
                    this.fingerPrintDataInTime.StoreNoIdentityWorkerInfo(attendDataPerWorker[0]);
                }
            });
            return OpResult.SetSuccessResult("处理考勤数据成功！", record > 0);
        }
        /// <summary>
        /// 重新排序连接的时间字符串
        /// </summary>
        /// <param name="slotCardTime"></param>
        /// <param name="attendTime"></param>
        /// <returns></returns>
        private string ConcatSlotCardTime(string slotCardTime, DateTime attendTime)
        {
            List<string> timeStrList = null;
            var cardTimeList = slotCardTime.Split(',');
            if (cardTimeList.Length == 0)
            {
                slotCardTime = string.Format("{0},{1}", slotCardTime, attendTime.ToString("HH:mm"));
                cardTimeList = slotCardTime.Split(',');
                timeStrList = cardTimeList.ToList();
            }
            else
            {
                timeStrList = cardTimeList.ToList();
                timeStrList.Add(attendTime.ToString("HH:mm"));
            }
            timeStrList.Sort();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < timeStrList.Count; i++)
            {
                sb.Append(timeStrList[i] + ",");
            }
            return sb.ToString().Trim(',');
        }
        /// <summary>
        /// 合并考勤时间
        /// </summary>
        /// <param name="currentAttendData"></param>
        /// <param name="slodCardTime"></param>
        /// <returns></returns>
        private int MergeAttendTime(AttendSlodFingerDataCurrentMonthModel currentAttendData, DateTime slodCardTime)
        {
            int record = 0;
            //若请假流程在前，则会先有考勤数据记录，但没有考勤时间，所以从刷卡时间1开始填写
            currentAttendData.SlotCardTime = ConcatSlotCardTime(currentAttendData.SlotCardTime, slodCardTime);

            if (currentAttendData.SlotCardTime1 == null || currentAttendData.SlotCardTime1.Length == 0)
            {
                record = UpdateSlotCardTime1(currentAttendData, slodCardTime, currentAttendData.SlotCardTime);
            }
            else
            {
                record = UpdateSlotCardTime2(currentAttendData, slodCardTime, currentAttendData.SlotCardTime);
            }
            return record;
        }
        /// <summary>
        /// 更新刷卡时间2
        /// </summary>
        /// <param name="currentAttendData"></param>
        /// <param name="slodCardTime"></param>
        /// <param name="cardtime"></param>
        /// <returns></returns>
        private int UpdateSlotCardTime2(AttendSlodFingerDataCurrentMonthModel currentAttendData, DateTime slodCardTime, string cardtime)
        {
            return irep.Update(f => f.Id_Key == currentAttendData.Id_Key, u => new AttendSlodFingerDataCurrentMonthModel
            {
                //直接进行更新替代
                SlotCardTime2 = slodCardTime.ToString("yyyy-MM-dd HH:mm"),
                SlotCardTime = currentAttendData.SlotCardTime == null || currentAttendData.SlotCardTime.Length == 0 ? slodCardTime.ToString("HH:mm") : cardtime
            });
        }
        /// <summary>
        /// 更新刷卡时间1
        /// </summary>
        /// <param name="currentAttendData"></param>
        /// <param name="slodCardTime"></param>
        /// <param name="cardtime"></param>
        /// <returns></returns>
        private int UpdateSlotCardTime1(AttendSlodFingerDataCurrentMonthModel currentAttendData, DateTime slodCardTime, string cardtime)
        {
            var ctimes = cardtime.Split(',').ToList();
            if (ctimes.Count >= 2)
            {
                ctimes.Sort();
                string cardTimeStr = currentAttendData.AttendanceDate.ToDateStr() + " " + ctimes[ctimes.Count - 1];
                return irep.Update(f => f.Id_Key == currentAttendData.Id_Key, u => new AttendSlodFingerDataCurrentMonthModel
                {
                    //重新调整刷卡时间2
                    //SlotCardTime1 = ctimes[0],
                    SlotCardTime2 = cardTimeStr,
                    SlotCardTime = currentAttendData.SlotCardTime == null || currentAttendData.SlotCardTime.Length == 0 ? slodCardTime.ToString("HH:mm") : cardtime,

                });
            }
            else
            {
                return irep.Update(f => f.Id_Key == currentAttendData.Id_Key, u => new AttendSlodFingerDataCurrentMonthModel
                {
                    //刷卡时间1若有就不进行更新，反之则进行更新
                    SlotCardTime1 = currentAttendData.SlotCardTime1 == null || currentAttendData.SlotCardTime1.Length == 0 ? slodCardTime.ToString("yyyy-MM-dd HH:mm") : currentAttendData.SlotCardTime1,
                    SlotCardTime = currentAttendData.SlotCardTime == null || currentAttendData.SlotCardTime.Length == 0 ? slodCardTime.ToString("HH:mm") : cardtime,

                });
            }
        }

        /// <summary>
        /// 初次插入数据
        /// </summary>
        /// <param name="attendTimeMdl"></param>
        /// <param name="worker"></param>
        /// <param name="slodCardTime"></param>
        /// <returns></returns>
        private int InitAttendData(AttendFingerPrintDataInTimeModel attendTimeMdl, ArWorkerInfo worker, DateTime slodCardTime, out AttendSlodFingerDataCurrentMonthModel initMdl, DateTime middleTime)
        {
            initMdl = null;
            int record = 0;
            var mdl = CreateAttendDataModel(attendTimeMdl, worker, slodCardTime);
            //首次赋值需要加中间判定时间
            if (slodCardTime > middleTime)
                mdl.SlotCardTime2 = slodCardTime.ToString("yyyy-MM-dd HH:mm");
            else
                mdl.SlotCardTime1 = slodCardTime.ToString("yyyy-MM-dd HH:mm");
            record = irep.Insert(mdl);
            if (record == 1)
            {
                initMdl = CreateAttendDataModel(attendTimeMdl, worker, slodCardTime);
                initMdl.Id_Key = mdl.Id_Key;
                initMdl.SlotCardTime1 = mdl.SlotCardTime1;
            }
            return record;
        }



        private AttendSlodFingerDataCurrentMonthModel CreateAttendDataModel(AttendFingerPrintDataInTimeModel attendTimeMdl, ArWorkerInfo worker, DateTime slodCardTime)
        {
            var mdl = new AttendSlodFingerDataCurrentMonthModel()
            {
                AttendanceDate = attendTimeMdl.SlodCardDate,
                WorkerId = worker.WorkerId,
                CardID = attendTimeMdl.CardID,
                CardType = attendTimeMdl.CardType,
                ClassType = worker.ClassType,
                Department = worker.Department,
                WorkerName = worker.Name,
                WeekDay = attendTimeMdl.SlodCardDate.DayOfWeek.ToString().ToChineseWeekDay(),
                LeaveHours = 0,
                LeaveMark = 0,
                YearMonth = slodCardTime.ToString("yyyyMM"),
                SlotCardTime = slodCardTime.ToString("HH:mm"),
                HandleSlotExceptionStatus = 0,
                SlotExceptionMark = 0,
                OpSign = "init",
                OpPerson = "system",
            };
            return mdl;
        }

        /// <summary>
        /// 更改考勤中的班次信息
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="slodCardDate"></param>
        /// <returns></returns>
        internal int UpdateClassTypeInfo(string classType, DateTime slodCardDate)
        {
            return this.irep.UpdateClassTypeInfo(classType, slodCardDate);
        }
        #endregion handle attend method

        #region handle attend askleave method
        /// <summary>
        /// 初始化空考勤数据
        /// </summary>
        /// <param name="askLeaveMdl"></param>
        /// <param name="classMdl"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal int InitEmptyAttendData(AttendAskLeaveModel askLeaveMdl, AttendClassTypeDetailModel classMdl, ref AttendSlodFingerDataCurrentMonthModel data)
        {
            string classType = "白班";
            if (classMdl != null)
                classType = classMdl.ClassType;
            data = new AttendSlodFingerDataCurrentMonthModel()
            {
                AttendanceDate = askLeaveMdl.AttendanceDate,
                WorkerId = askLeaveMdl.WorkerId,
                CardID = "",
                CardType = "",
                ClassType = classType,
                Department = askLeaveMdl.Department,
                WorkerName = askLeaveMdl.WorkerName,
                WeekDay = askLeaveMdl.AttendanceDate.DayOfWeek.ToString().ToChineseWeekDay(),
                LeaveHours = askLeaveMdl.LeaveHours,
                LeaveMark = 1,
                LeaveType = askLeaveMdl.LeaveType,
                LeaveTimeRegion = askLeaveMdl.LeaveTimeRegion,
                LeaveMemo = askLeaveMdl.LeaveMemo,
                LeaveDescription = GetAskLeaveDescription(askLeaveMdl, data),
                YearMonth = askLeaveMdl.YearMonth,
                SlotCardTime = "",
                HandleSlotExceptionStatus = 0,
                SlotExceptionMark = 0,
                OpSign = "initEmpty",
                OpPerson = "system",
            };
            return irep.Insert(data);
        }
        /// <summary>
        /// 更新考勤中的请假区域
        /// </summary>
        /// <param name="askLeaveMdl"></param>
        /// <param name="hours"></param>
        /// <param name="askLeaveDescription"></param>
        /// <returns></returns>
        private int UpdateAskLeaveArea(AttendAskLeaveModel askLeaveMdl, double hours, string askLeaveDescription)
        {
            return irep.Update(e => e.WorkerId == askLeaveMdl.WorkerId && e.AttendanceDate == askLeaveMdl.AttendanceDate, u => new AttendSlodFingerDataCurrentMonthModel()
            {
                LeaveHours = hours,
                LeaveType = askLeaveMdl.LeaveType,
                LeaveTimeRegion = askLeaveMdl.LeaveTimeRegion,
                LeaveMemo = askLeaveMdl.LeaveMemo,
                LeaveDescription = askLeaveDescription
            });
        }
        private string GetAskLeaveDescription(AttendAskLeaveModel askLeaveMdl, AttendSlodFingerDataCurrentMonthModel data)
        {
            string leaveDescription = string.Empty;
            AskLeaveCell cell = new AskLeaveCell()
            {
                LeaveHours = askLeaveMdl.LeaveHours,
                LeaveMemo = askLeaveMdl.LeaveMemo,
                LeaveTimeRegion = askLeaveMdl.LeaveTimeRegion,
                LeaveType = askLeaveMdl.LeaveType
            };
            if (data == null)
            {
                cell.Id = 1;
                leaveDescription = "[" + ObjectSerializer.SerializeObject(cell) + "]";
            }
            else
            {
                List<AskLeaveCell> descriptions = new List<AskLeaveCell>();
                if (string.IsNullOrEmpty(data.LeaveDescription))
                {
                    descriptions.Add(cell);
                }
                else
                {
                    descriptions = ObjectSerializer.DeserializeObject<List<AskLeaveCell>>(data.LeaveDescription);
                    var m = descriptions.FirstOrDefault(e => e.LeaveType == cell.LeaveType && e.LeaveTimeRegion == cell.LeaveTimeRegion);
                    if (m != null)
                    {
                        if (askLeaveMdl.OpSign == OpMode.Edit)
                            m = cell;
                    }
                    else
                    {
                        cell.Id = descriptions.Count + 1;
                        descriptions.Add(cell);
                    }
                }
                leaveDescription = ObjectSerializer.SerializeObject(descriptions);
            }
            return leaveDescription;
        }
        private int SetAskLeaveDataToAttendData(AttendAskLeaveModel askLeaveMdl, AttendSlodFingerDataCurrentMonthModel data)
        {
            string leaveDescription = GetAskLeaveDescription(askLeaveMdl, data);
            if (data.LeaveMark == 1)
            {
                return UpdateAskLeaveArea(askLeaveMdl, askLeaveMdl.LeaveHours + data.LeaveHours, leaveDescription);
            }
            else
            {
                return UpdateAskLeaveArea(askLeaveMdl, askLeaveMdl.LeaveHours, leaveDescription);
            }
        }
        private int ClearAskLeaveDataToAttendData(AttendAskLeaveModel askLeaveMdl, AttendSlodFingerDataCurrentMonthModel data)
        {
            AskLeaveCell cell = null;
            string description = string.Empty;
            if (string.IsNullOrEmpty(data.LeaveDescription))
            {
                cell = new AskLeaveCell() { LeaveHours = 0, LeaveMemo = "", LeaveTimeRegion = "", LeaveType = "" };
            }
            else
            {
                List<AskLeaveCell> descriptions = ObjectSerializer.DeserializeObject<List<AskLeaveCell>>(data.LeaveDescription);
                var m = descriptions.FirstOrDefault(e => e.LeaveType == askLeaveMdl.LeaveType && e.LeaveTimeRegion == askLeaveMdl.LeaveTimeRegion);
                if (m != null)
                {
                    descriptions.Remove(m);
                }
                if (descriptions.Count > 0)
                {
                    cell = descriptions[descriptions.Count - 1];
                    cell.LeaveHours = descriptions.Sum(s => s.LeaveHours);
                    description = ObjectSerializer.SerializeObject(descriptions);
                }
                else
                {
                    cell = new AskLeaveCell() { LeaveHours = 0, LeaveMemo = "", LeaveTimeRegion = "", LeaveType = "" };
                }
            }
            return irep.Update(e => e.WorkerId == askLeaveMdl.WorkerId && e.AttendanceDate == askLeaveMdl.AttendanceDate, u => new AttendSlodFingerDataCurrentMonthModel()
            {
                LeaveHours = cell.LeaveHours,
                LeaveMark = cell.LeaveHours > 0 ? 1 : 0,
                LeaveType = cell.LeaveType,
                LeaveTimeRegion = cell.LeaveTimeRegion,
                LeaveMemo = cell.LeaveMemo,
                LeaveDescription = description
            });
        }
        /// <summary>
        /// 同步请假信息到考勤数据中
        /// </summary>
        /// <param name="askLeaveMdl"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal int SyncAskLeaveDataToAttendData(AttendAskLeaveModel askLeaveMdl, AttendClassTypeDetailModel classMdl, ref AttendSlodFingerDataCurrentMonthModel data)
        {
            if (data == null)
            {
                if (askLeaveMdl.OpSign == OpMode.Add || askLeaveMdl.OpSign == OpMode.Edit)
                    return this.InitEmptyAttendData(askLeaveMdl, classMdl, ref data);
                else
                    return 1;
            }
            else
            {
                if (askLeaveMdl.OpSign == OpMode.Add || askLeaveMdl.OpSign == OpMode.Edit)
                    return this.SetAskLeaveDataToAttendData(askLeaveMdl, data);
                else if (askLeaveMdl.OpSign == OpMode.None)
                    return 1;
                else
                    return this.ClearAskLeaveDataToAttendData(askLeaveMdl, data);
            }
        }
        #endregion

        #region handle attend exception method

        //---------------------------------------异常处理--------------------------------------------

        /// <summary>
        /// 载入异常刷卡数据
        /// </summary>
        /// <returns></returns>
        public List<AttendSlodFingerDataCurrentMonthModel> LoadExceptionSlotData()
        {
            DateTime qryDate = DateTime.Now.AddDays(-1);
            return this.irep.Entities.Where(e => e.HandleSlotExceptionStatus == 1 && e.AttendanceDate <= qryDate).OrderBy(o => o.AttendanceDate).ToList();
        }

        /// <summary>
        /// 返回有异常的数据
        /// </summary>
        public List<AttendSlodFingerDataCurrentMonthModel> AutoHandleExceptionSlotData(string yearMonth)
        {
            List<AttendSlodFingerDataCurrentMonthModel> attendExceptionSlodDatas = new List<AttendSlodFingerDataCurrentMonthModel>();
            string[] dfields = yearMonth.Split('-');
            if (dfields.Length != 2) return attendExceptionSlodDatas;
            int qurYear = dfields[0].Trim().ToInt(), qurMonth = dfields[1].Trim().ToInt();
            //考勤数据中没有处理过异常的数据集
            List<AttendSlodFingerDataCurrentMonthModel> attendSlotDatas = this.irep.Entities.Where(e => e.HandleSlotExceptionStatus == 0 && e.YearMonth == yearMonth).ToList();
            if (attendSlotDatas == null || attendSlotDatas.Count == 0) return attendExceptionSlodDatas;
            //本月考勤中所有员工列表
            List<AttendSlodWorkerModel> attendSlodWorkers = null; //this.GetAttendSlodWorkers(qurYear, qurMonth);
            AttendClassTypeSetter classTypeSetter = new AttendClassTypeSetter();
            //获取本月份的日期天数
            int daysInMonths = DateTime.DaysInMonth(qurYear, qurMonth);

            Dictionary<string, string> dicClassTypes = new Dictionary<string, string>();
            //一天的中间时间
            DateTime dayMiddleTime;
            DateTime currentDay;
            string workerId;//工号
            string classType;//班别
            string slotExceptionType = string.Empty;//刷卡异常类别
            AttendClassTypeDetailModel ctdMdl = null;
            //每个人考勤数据
            List<AttendSlodFingerDataCurrentMonthModel> attendSlotDatasOfWorker = null;
            AttendSlodFingerDataCurrentMonthModel attendSd = null;
            attendSlodWorkers.ForEach(worker =>
            {
                attendSlotDatasOfWorker = attendSlotDatas.Where(m => m.WorkerId == worker.WorkerId).ToList();
                while (worker.AttendDateStart <= worker.AttendDateEnd)
                {
                    currentDay = worker.AttendDateStart;
                    //每天的考勤数据
                    attendSd = attendSlotDatasOfWorker.FirstOrDefault(d => d.AttendanceDate == currentDay);
                    if (attendSd == null)//如果不存在考勤数据
                    {
                        //初始化该日期的考勤数据
                        ctdMdl = AttendCrudFactory.ClassTypeDetailCrud.GetClassTypeDetailModel(attendSd.WorkerId, attendSd.AttendanceDate);
                        var mdl = new AttendSlodFingerDataCurrentMonthModel()
                        {
                            AttendanceDate = currentDay,
                            WorkerId = worker.WorkerId,
                            CardID = "",
                            CardType = "",
                            ClassType = ctdMdl == null ? "白班" : ctdMdl.ClassType,
                            Department = worker.Department,
                            WorkerName = worker.WorkerName,
                            WeekDay = currentDay.DayOfWeek.ToString().ToChineseWeekDay(),
                            LeaveHours = 0,
                            LeaveMark = 0,
                            YearMonth = currentDay.ToString("yyyyMM"),
                            SlotCardTime = currentDay.ToString("HH:mm"),
                            HandleSlotExceptionStatus = 0,
                            SlotExceptionMark = 0,
                            OpSign = "init",
                            OpPerson = "system",
                        };
                        this.irep.Insert(mdl);
                    }
                    else
                    {
                        //存在的话则进行检测处理
                        workerId = attendSd.WorkerId;
                        //刷卡异常类型
                        slotExceptionType = string.Empty;
                        classType = attendSd.ClassType;
                        //时间点
                        int hour = classType == "白班" ? 12 : 24;
                        int day = attendSd.AttendanceDate.Day;
                        int year = attendSd.AttendanceDate.Year;
                        int month = attendSd.AttendanceDate.Month;
                        //中间时间
                        dayMiddleTime = new DateTime(year, month, day, hour, 0, 0);
                        string slotCardTime = attendSd.SlotCardTime;//刷卡时间
                        if (slotCardTime == null || slotCardTime.Length < 5)
                        {
                            if (attendSd.LeaveMark == 0)//如果有请假的话，则不做旷工标志处理
                                MergeSlotExceptionType(ref slotExceptionType, "旷工");//标志异常考勤信息为旷工
                        }
                        else
                        {
                            if (attendSd.SlotCardTime1 == null || attendSd.SlotCardTime2 == null)//刷卡时间不完整的情况分析
                            {
                                if (attendSd.LeaveMark == 0)
                                    MergeSlotExceptionType(ref slotExceptionType, "漏刷卡");
                                else
                                    MergeSlotExceptionType(ref slotExceptionType, "请假漏刷");
                            } //继续分析刷卡时间都全面的情况
                            else
                            {
                                //在都完整的情况下，分析考勤异常情况
                                AnalogSlotCardTime(attendSd, ref slotExceptionType, classType, day, year, month, slotCardTime);
                            }
                        }
                        attendSd.SlotExceptionType = slotExceptionType;
                        attendSd.SlotExceptionMark = slotExceptionType != null && slotExceptionType.Length >= 2 ? 1 : 0;
                        attendSd.HandleSlotExceptionStatus = attendSd.SlotExceptionMark == 1 ? 1 : -1;//若有异常，则状态为1，没有异常则状态为-1代表已经检测过
                                                                                                      //同步到数据库中
                        this.irep.Update(f => f.WorkerId == attendSd.WorkerId && f.AttendanceDate == attendSd.AttendanceDate, u => new AttendSlodFingerDataCurrentMonthModel
                        {
                            HandleSlotExceptionStatus = attendSd.HandleSlotExceptionStatus,
                            SlotExceptionType = slotExceptionType,
                            SlotExceptionMark = attendSd.SlotExceptionMark
                        });
                    }
                    //日期递增
                    worker.AttendDateStart = worker.AttendDateStart.AddDays(1);
                }
            });
            attendExceptionSlodDatas = attendSlotDatas.Where(e => e.HandleSlotExceptionStatus == 1).OrderBy(o => o.AttendanceDate).ToList();
            return attendExceptionSlodDatas;
        }
        /// <summary>
        /// 合并异常类型信息
        /// </summary>
        /// <param name="slotExceptionType"></param>
        /// <param name="exceptionType"></param>
        private void MergeSlotExceptionType(ref string slotExceptionType, string exceptionType)
        {
            if (!slotExceptionType.Contains(exceptionType))
                slotExceptionType = slotExceptionType.Length == 0 ? exceptionType : slotExceptionType + "," + exceptionType;
        }
        /// <summary>
        /// 分析刷卡时间
        /// </summary>
        /// <param name="slotExceptionType"></param>
        /// <param name="classType"></param>
        /// <param name="day"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="slotCardTime"></param>
        private void AnalogSlotCardTime(AttendSlodFingerDataCurrentMonthModel attendSd, ref string slotExceptionType, string classType, int day, int year, int month, string slotCardTime)
        {
            string exceptionType = slotExceptionType;
            //分析时间
            string[] slotTimes = slotCardTime.Split(',');
            if (slotTimes != null && slotTimes.Length > 0)
            {
                if (slotTimes.Length > 3)//有三次以上刷卡记录，不做处理
                {
                    //MergeSlotExceptionType(ref slotExceptionType, "多次刷卡");
                }
                slotTimes.ToList().ForEach(st =>
                {
                    string[] ttimes = st.Split(':');
                    if (ttimes != null && ttimes.Length == 2)//分析刷卡具体时间
                    {
                        int h = ttimes[0].ToInt();
                        int m = ttimes[1].ToInt();
                        //组合当前刷卡时间
                        DateTime currentTime = new DateTime(year, month, day, h, m, 0);
                        //白班迟到时间区间7:51--7:55
                        DateTime dayAmstand = new DateTime(year, month, day, 7, 50, 0);//上班时间
                        DateTime dayAmstart = new DateTime(year, month, day, 7, 51, 0);
                        DateTime dayAmend = new DateTime(year, month, day, 7, 55, 0);
                        //晚班迟到时间区间19:51--19:55
                        DateTime dayPmstart = new DateTime(year, month, day, 19, 51, 0);
                        DateTime dayPmend = new DateTime(year, month, day, 19, 55, 0);
                        DateTime dayPmstand = new DateTime(year, month, day, 19, 50, 0);
                        if (classType == "白班")
                        {
                            AnalogDayAttendData(attendSd, ref exceptionType, currentTime, dayAmstart, dayAmend);
                        }
                        else if (classType == "晚班")
                        {
                            AnalogNightAttendData(attendSd, ref exceptionType, currentTime, dayPmstart, dayPmend);
                        }
                    }
                });
            }
            slotExceptionType = exceptionType;
        }
        /// <summary>
        /// 分析处理晚班数据
        /// </summary>
        /// <param name="slotExceptionType"></param>
        /// <param name="currentTime"></param>
        /// <param name="dayPmstart"></param>
        /// <param name="dayPmend"></param>
        private void AnalogNightAttendData(AttendSlodFingerDataCurrentMonthModel attendSd, ref string slotExceptionType, DateTime currentTime, DateTime dayPmstart, DateTime dayPmend)
        {
            if (currentTime > dayPmstart && currentTime < dayPmend)
            {
                MergeSlotExceptionType(ref slotExceptionType, "迟到");
            }
            else if (currentTime > dayPmend)
            {
                //先判定是否有请假状况
                if (attendSd.LeaveMark == 0)
                    MergeSlotExceptionType(ref slotExceptionType, "旷工");
            }
        }
        /// <summary>
        /// 分析处理白天数据
        /// </summary>
        /// <param name="slotExceptionType"></param>
        /// <param name="currentTime"></param>
        /// <param name="dayAmstart"></param>
        /// <param name="dayAmend"></param>
        private void AnalogDayAttendData(AttendSlodFingerDataCurrentMonthModel attendSd, ref string slotExceptionType, DateTime currentTime, DateTime dayAmstart, DateTime dayAmend)
        {
            if (currentTime > dayAmstart && currentTime < dayAmend)
            {
                MergeSlotExceptionType(ref slotExceptionType, "迟到");
            }
            else if (currentTime > dayAmend)
            {
                //先判定是否有请假状况
                if (attendSd.LeaveMark == 0)
                    MergeSlotExceptionType(ref slotExceptionType, "旷工");
            }
        }

        /// <summary>
        /// 获取某个作业人员的班别信息
        /// </summary>
        /// <param name="workers"></param>
        /// <param name="dicClassTypes"></param>
        /// <param name="workerId"></param>
        /// <returns></returns>
        private string GetClassTypeOf(List<ArWorkerInfo> workers, ref Dictionary<string, string> dicClassTypes, string workerId)
        {
            string classType = "白班";
            if (dicClassTypes.ContainsKey(workerId))
            {
                classType = dicClassTypes[workerId];
            }
            else
            {
                var wr = workers.FirstOrDefault(e => e.WorkerId == workerId);
                if (wr != null)
                {
                    classType = wr.ClassType;
                    dicClassTypes.Add(workerId, classType);
                }
            }
            return classType;
        }

        /// <summary>
        /// 处理异常数据
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        internal int HandleExceptionSlotCardData(List<AttendSlodFingerDataCurrentMonthModel> entities)
        {
            int record = 0;
            if (entities == null || entities.Count == 0) return record;
            entities = entities.FindAll(e => e.HandleSlotExceptionStatus == 2);
            entities.ForEach(e =>
            {
                if (e.OpSign == "handleAskLeave")//处理请假
                {
                    record = HandleAskLeaveAboutException(record, e);
                }
                else if (e.OpSign == "handleForgetSlot")//处理漏刷卡
                {
                    record = HandleForgetSlotAboutException(record, e);
                }
                else if (e.OpSign == "handleAbsent" || e.OpSign == "handleLate")//处理旷工或者迟到
                {
                    record = HandleAbsentAboutException(record, e);
                }
            });
            return record;
        }
        /// <summary>
        /// 关于异常的请假处理
        /// </summary>
        /// <param name="record"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private int HandleAskLeaveAboutException(int record, AttendSlodFingerDataCurrentMonthModel e)
        {
            record += this.irep.Update(w => w.WorkerId == e.WorkerId && w.AttendanceDate == e.AttendanceDate,
                               u => new AttendSlodFingerDataCurrentMonthModel
                               {
                                   LeaveHours = e.LeaveHours,
                                   LeaveMark = e.LeaveHours < 0.01 ? 0 : 1,
                                   LeaveType = e.LeaveType,
                                   LeaveTimeRegion = e.LeaveTimeRegion,
                                   LeaveDescription = "请假处理,假别：" + e.LeaveType,
                                   LeaveMemo = e.LeaveMemo,
                                   SlotExceptionMark = 1,
                                   SlotExceptionMemo = e.SlotExceptionMemo,
                                   HandleSlotExceptionStatus = e.HandleSlotExceptionStatus
                               });
            return record;
        }

        /// <summary>
        /// 关于异常的漏刷卡处理
        /// </summary>
        /// <param name="record"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private int HandleForgetSlotAboutException(int record, AttendSlodFingerDataCurrentMonthModel e)
        {
            record += this.irep.Update(w => w.WorkerId == e.WorkerId && w.AttendanceDate == e.AttendanceDate,
                               u => new AttendSlodFingerDataCurrentMonthModel
                               {
                                   SlotCardTime1 = e.SlotCardTime1,
                                   SlotCardTime2 = e.SlotCardTime2,
                                   SlotCardTime = e.SlotCardTime,
                                   SlotExceptionMark = 1,
                                   SlotExceptionMemo = e.SlotExceptionMemo,
                                   ForgetSlotReason = e.ForgetSlotReason,
                                   HandleSlotExceptionStatus = e.HandleSlotExceptionStatus
                               });
            return record;
        }

        /// <summary>
        /// 关于异常的旷工或者迟到处理
        /// </summary>
        /// <param name="record"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private int HandleAbsentAboutException(int record, AttendSlodFingerDataCurrentMonthModel e)
        {
            record += this.irep.Update(w => w.WorkerId == e.WorkerId && w.AttendanceDate == e.AttendanceDate,
                               u => new AttendSlodFingerDataCurrentMonthModel
                               {
                                   SlotExceptionMark = 1,
                                   SlotExceptionMemo = e.SlotExceptionMemo,
                                   HandleSlotExceptionStatus = e.HandleSlotExceptionStatus
                               });
            return record;
        }
        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }
        #endregion handle attend exception method

        #endregion method
    }


    internal class AskLeaveCell
    {
        private int _id;
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }

        private string _leavetype;
        /// <summary>
        ///请假类别
        /// </summary>
        public string LeaveType
        {
            set { _leavetype = value; }
            get { return _leavetype; }
        }
        private double _leavehours;
        /// <summary>
        ///请假时数
        /// </summary>
        public double LeaveHours
        {
            set { _leavehours = value; }
            get { return _leavehours; }
        }
        private string _leavetimeregion;
        /// <summary>
        ///时间段
        /// </summary>
        public string LeaveTimeRegion
        {
            set { _leavetimeregion = value; }
            get { return _leavetimeregion; }
        }
        private string _leavememo;
        /// <summary>
        ///备注
        /// </summary>
        public string LeaveMemo
        {
            set { _leavememo = value; }
            get { return _leavememo; }
        }
    }
}
