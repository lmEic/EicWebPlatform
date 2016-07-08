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
    /// <summary>
    /// 指纹刷卡数据管理器
    /// </summary>
    public class AttendSlodPrintManager
    {
        #region member

        private AttendSlodFingerDataCurrentMonthHandler currentMonthAttendDataHandler = null;

        #endregion member

        #region constructure

        public AttendSlodPrintManager()
        {
            this.currentMonthAttendDataHandler = new AttendSlodFingerDataCurrentMonthHandler();
        }

        #endregion constructure



        #region method

        /// <summary>
        /// 载入某部门的当天的数据
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<AttendanceDataModel> LoadAttendDataInToday(string department)
        {
            return this.currentMonthAttendDataHandler.LoadAttendDataInToday(department);
        }

        /// <summary>
        /// 自动处理异常数据
        /// </summary>
        public List<AttendSlodFingerDataCurrentMonthModel> AutoCheckExceptionSlotData()
        {
            return this.currentMonthAttendDataHandler.AutoHandleExceptionSlotData();
        }

        /// <summary>
        /// 载入异常考勤数据
        /// </summary>
        /// <returns></returns>
        public List<AttendSlodFingerDataCurrentMonthModel> LoadExceptionSlotData()
        {
            return this.currentMonthAttendDataHandler.LoadExceptionSlotData();
        }

        /// <summary>
        /// 处理异常刷卡数据
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public OpResult HandleExceptionSlotCardData(List<AttendSlodFingerDataCurrentMonthModel> entities)
        {
            return OpResult.SetResult("处理异常刷卡数据成功！", this.currentMonthAttendDataHandler.HandleExceptionSlotCardData(entities) > 0);
        }

        #endregion method
    }

    /// <summary>
    /// 当月考勤时间数据处理器
    /// </summary>
    public class AttendSlodFingerDataCurrentMonthHandler
    {
        #region member

        private IAttendSlodFingerDataCurrentMonthRepository irep = null;
        private AttendFingerPrintDataInTimeHandler fingerPrintDataInTime = null;

        #endregion member

        #region constructure

        public AttendSlodFingerDataCurrentMonthHandler()
        {
            this.irep = new AttendSlodFingerDataCurrentMonthRepository();
            this.fingerPrintDataInTime = new AttendFingerPrintDataInTimeHandler();
        }

        #endregion constructure



        #region method

        #region handle attend method

        /// <summary>
        /// 载入今天的考勤数据
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<AttendanceDataModel> LoadAttendDataInToday(string department)
        {
            if (this.fingerPrintDataInTime.IsExsitAttendData)
            {
                TransimitAttendDatas();
            }
            return this.irep.LoadAttendDataOfToday(department);
        }

        /// <summary>
        /// 将实时考勤数据转移至本月数据表中
        /// </summary>
        /// <returns></returns>
        public OpResult TransimitAttendDatas()
        {
            int record = 0;
            //实时考勤数据
            var datasInTime = this.fingerPrintDataInTime.FingPrintDatas;
            var workers = ArchiveService.ArchivesManager.FindWorkers();
            if (datasInTime != null && datasInTime.Count > 0)
            {
                datasInTime.ForEach(attendTime =>
                {
                    var worker = workers.FirstOrDefault(w => w.WorkerId == attendTime.WorkerId);
                    var currentAttendData = this.irep.Entities.FirstOrDefault(e => e.WorkerId == attendTime.WorkerId && e.AttendanceDate == attendTime.SlodCardDate);
                    if (worker != null)
                    {
                        string classType = worker.ClassType;
                        string department = worker.Department;
                        DateTime dayMiddleTime = new DateTime(attendTime.SlodCardDate.Year, attendTime.SlodCardDate.Month, attendTime.SlodCardDate.Day, 12, 0, 0);
                        if (currentAttendData == null)
                        {
                            record = InitAttendData(record, attendTime, worker, dayMiddleTime, attendTime.SlodCardTime);
                        }
                        else
                        {
                            record = MergeAttendTime(record, currentAttendData, dayMiddleTime, attendTime.SlodCardTime);
                        }
                    }
                    if (record == 1)
                    {
                        this.fingerPrintDataInTime.Delete(attendTime);
                        record = 0;
                    }
                });
            }
            return OpResult.SetResult("搬运数据成功！", record > 0);
        }

        /// <summary>
        /// 合并考勤时间
        /// </summary>
        /// <param name="record"></param>
        /// <param name="currentAttendData"></param>
        /// <param name="slodCardTime"></param>
        /// <returns></returns>
        private int MergeAttendTime(int record, AttendSlodFingerDataCurrentMonthModel currentAttendData, DateTime dayMiddelTime, DateTime slodCardTime)
        {
            //若请假流程在前，则会先有考勤数据记录，但没有考勤时间，所以从刷卡时间1开始填写
            string cardtime = currentAttendData.SlotCardTime + "," + slodCardTime.ToString("HH:mm");
            if (slodCardTime <= dayMiddelTime)
            {
                record = UpdateSlotCardTime1(record, currentAttendData, slodCardTime, cardtime);
            }
            else
            {
                record = UpdateSlotCardTime2(record, currentAttendData, slodCardTime, cardtime);
            }
            return record;
        }

        private int UpdateSlotCardTime2(int record, AttendSlodFingerDataCurrentMonthModel currentAttendData, DateTime slodCardTime, string cardtime)
        {
            record += irep.Update(f => f.Id_Key == currentAttendData.Id_Key, u => new AttendSlodFingerDataCurrentMonthModel
            {
                SlotCardTime2 = slodCardTime.ToDateTimeStr(),
                SlotCardTime = currentAttendData.SlotCardTime == null || currentAttendData.SlotCardTime.Length == 0 ? slodCardTime.ToString("HH:mm") : cardtime
            });
            return record;
        }

        private int UpdateSlotCardTime1(int record, AttendSlodFingerDataCurrentMonthModel currentAttendData, DateTime slodCardTime, string cardtime)
        {
            if (currentAttendData.SlotCardTime != null)
            {
                record += irep.Update(f => f.Id_Key == currentAttendData.Id_Key, u => new AttendSlodFingerDataCurrentMonthModel
                {
                    SlotCardTime = currentAttendData.SlotCardTime == null || currentAttendData.SlotCardTime.Length == 0 ? slodCardTime.ToString("HH:mm") : cardtime
                });
            }
            else
            {
                record = 1;
            }

            return record;
        }

        /// <summary>
        /// 初次插入数据
        /// </summary>
        /// <param name="record"></param>
        /// <param name="attendTimeMdl"></param>
        /// <param name="worker"></param>
        /// <param name="slodCardTime"></param>
        /// <returns></returns>
        private int InitAttendData(int record, AttendFingerPrintDataInTimeModel attendTimeMdl, ArWorkerInfo worker, DateTime dayMiddelTime, DateTime slodCardTime)
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
            if (slodCardTime <= dayMiddelTime)
            {
                mdl.SlotCardTime1 = slodCardTime.ToDateTimeStr();
            }
            else
            {
                mdl.SlotCardTime2 = slodCardTime.ToDateTimeStr();
            }
            record += irep.Insert(mdl);
            return record;
        }

        #endregion handle attend method

        #region ask for leave method

        //----------------------------------请假数据处理-----------------------------------------
        /// <summary>
        /// 同步请假数据信息
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        internal int SyncAskLeaveData(List<AttendAskLeaveModel> entities)
        {
            int record = 0;
            if (entities == null || entities.Count == 0) return record;
            entities.ForEach(e =>
            {
                int days = (e.EndLeaveDate - e.StartLeaveDate).Days;
                if (days > 0)
                {
                    DateTime dnow = e.StartLeaveDate;
                    //循环遍历请假的每一天，如果存在考勤数据，则进行更改，不存在，则直接添加考勤数据
                    for (int dayIndex = 0; dayIndex <= days; dayIndex++)
                    {
                        if (!this.irep.IsExist(w => w.WorkerId == e.WorkerId && w.AttendanceDate == dnow))
                        {
                            record += this.irep.Insert(new AttendSlodFingerDataCurrentMonthModel()
                            {
                                AttendanceDate = dnow,
                                Department = e.Department,
                                WorkerId = e.WorkerId,
                                WorkerName = e.WorkerName,
                                ClassType = e.ClassType,
                                YearMonth = dnow.ToString("yyyyMM"),
                                WeekDay = dnow.DayOfWeek.ToString().ToChineseWeekDay(),
                                LeaveHours = e.LeaveHours,
                                LeaveMark = 1,
                                LeaveType = e.LeaveType,
                                LeaveTimeRegion = e.LeaveTimeRegionStart + "--" + e.LeaveTimeRegionEnd,
                                LeaveDescription = "请假处理,假别：" + e.LeaveType,
                                LeaveMemo = e.LeaveMemo,
                                HandleSlotExceptionStatus = 0,
                                SlotExceptionMark = 0,
                                OpSign = "init",
                            });
                        }
                        else
                        {
                            record += this.irep.Update(w => w.WorkerId == e.WorkerId && w.AttendanceDate == dnow,
                                 u => new AttendSlodFingerDataCurrentMonthModel
                                 {
                                     LeaveHours = e.LeaveHours,
                                     LeaveMark = 1,
                                     LeaveType = e.LeaveType,
                                     LeaveTimeRegion = e.LeaveTimeRegionStart + "--" + e.LeaveTimeRegionEnd,
                                     LeaveDescription = "请假处理,假别：" + e.LeaveType,
                                     LeaveMemo = e.LeaveMemo
                                 });
                        }
                        dnow = dnow.AddDays(1);
                    }
                }
            });
            return record;
        }

        /// <summary>
        /// 更改请假信息
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        internal int UpdateAskLeaveData(List<AttendSlodFingerDataCurrentMonthModel> entities)
        {
            int record = 0;
            if (entities == null || entities.Count == 0) return record;
            try
            {
                entities.ForEach(e =>
                {
                    if (e.OpSign == "handleEdit")
                    {
                        record += this.irep.Update(w => w.WorkerId == e.WorkerId && w.AttendanceDate == e.AttendanceDate,
                                       u => new AttendSlodFingerDataCurrentMonthModel
                                       {
                                           LeaveHours = e.LeaveHours,
                                           LeaveMark = e.LeaveHours < 0.01 ? 0 : 1,
                                           LeaveType = e.LeaveType,
                                           LeaveTimeRegion = e.LeaveTimeRegion,
                                           LeaveDescription = "请假处理,假别：" + e.LeaveType,
                                           LeaveMemo = e.LeaveMemo
                                       });
                    }
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return record;
        }

        /// <summary>
        /// 获取某作业人员的请假信息
        /// </summary>
        /// <param name="workerId"></param>
        /// <returns></returns>
        internal List<AttendSlodFingerDataCurrentMonthModel> GetAskLeaveDataAbout(string workerId, string qryMonth)
        {
            return this.irep.Entities.Where(e => e.WorkerId == workerId && e.YearMonth == qryMonth && e.LeaveMark == 1).ToList();
        }

        #endregion ask for leave method

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
        /// 自动处理异常数据
        /// 返回有异常的数据
        /// </summary>
        public List<AttendSlodFingerDataCurrentMonthModel> AutoHandleExceptionSlotData()
        {
            var workers = ArchiveService.ArchivesManager.FindWorkers();
            //一天的中间时间
            DateTime dayMiddleTime;
            DateTime qryDate = DateTime.Now.AddDays(-1);
            Dictionary<string, string> dicClassTypes = new Dictionary<string, string>();
            //考勤数据中没有处理过异常的数据集
            List<AttendSlodFingerDataCurrentMonthModel> attendSlotDatas = this.irep.Entities.Where(e => e.HandleSlotExceptionStatus == 0 && e.AttendanceDate <= qryDate).ToList();
            if (attendSlotDatas != null && attendSlotDatas.Count > 0)
            {
                string workerId;//工号
                string classType;//班别
                string slotExceptionType = string.Empty;//刷卡异常类别
                attendSlotDatas.ForEach(attendSd =>
                {
                    workerId = attendSd.WorkerId;
                    slotExceptionType = string.Empty;
                    classType = GetClassTypeOf(workers, ref dicClassTypes, workerId);
                    //时间点
                    int hour = classType == "白班" ? 12 : 24;
                    int day = attendSd.AttendanceDate.Day;
                    int year = attendSd.AttendanceDate.Year;
                    int month = attendSd.AttendanceDate.Month;

                    dayMiddleTime = new DateTime(year, month, day, hour, 0, 0);
                    string slotCardTime = attendSd.SlotCardTime;//刷卡时间
                    if (slotCardTime == null || slotCardTime.Length < 5)
                    {
                        if (attendSd.LeaveMark == 0)//如果有请假的话，则不做旷工标志处理
                            MergeSlotExceptionType(ref slotExceptionType, "旷工");
                    }
                    else
                    {
                        if (attendSd.SlotCardTime1 == null || attendSd.SlotCardTime2 == null)
                        {
                            if (attendSd.LeaveMark == 0)
                                MergeSlotExceptionType(ref slotExceptionType, "漏刷卡");
                            else
                                MergeSlotExceptionType(ref slotExceptionType, "请假漏刷");
                        } //继续分析刷卡时间都全面的情况
                        else
                        {
                            AnalogSlotCardTime(ref slotExceptionType, dayMiddleTime, classType, day, year, month, slotCardTime);
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
                });
            }
            return attendSlotDatas.Where(e => e.HandleSlotExceptionStatus == 1).OrderBy(o => o.AttendanceDate).ToList();
        }

        private void MergeSlotExceptionType(ref string slotExceptionType, string exceptionType)
        {
            if (!slotExceptionType.Contains(exceptionType))
                slotExceptionType = slotExceptionType.Length == 0 ? exceptionType : slotExceptionType + "," + exceptionType;
        }

        private void AnalogSlotCardTime(ref string slotExceptionType, DateTime dayMiddleTime, string classType, int day, int year, int month, string slotCardTime)
        {
            string exceptionType = slotExceptionType;
            //分析时间
            string[] slotTimes = slotCardTime.Split(',');
            if (slotTimes != null && slotTimes.Length > 0)
            {
                if (slotTimes.Length > 3)//有三次以上刷卡记录，先自动判定为异常
                {
                    MergeSlotExceptionType(ref slotExceptionType, "多次刷卡");
                }
                slotTimes.ToList().ForEach(st =>
                {
                    string[] ttimes = st.Split(':');
                    if (ttimes != null && ttimes.Length == 2)
                    {
                        int h = ttimes[0].ToInt();
                        int m = ttimes[1].ToInt();
                        //组合当前刷卡时间
                        DateTime currentTime = new DateTime(year, month, day, h, m, 0);
                        //白班迟到时间区间
                        DateTime dayAmstand = new DateTime(year, month, day, 7, 50, 0);
                        DateTime dayAmstart = new DateTime(year, month, day, 7, 51, 0);
                        DateTime dayAmend = new DateTime(year, month, day, 7, 55, 0);
                        //晚班迟到时间区间
                        DateTime dayPmstart = new DateTime(year, month, day, 19, 51, 0);
                        DateTime dayPmend = new DateTime(year, month, day, 19, 55, 0);
                        DateTime dayPmstand = new DateTime(year, month, day, 19, 50, 0);

                        if (classType == "白班")
                        {
                            AnalogDayAttendData(ref exceptionType, dayMiddleTime, currentTime, dayAmstart, dayAmend);
                        }
                        else if (classType == "晚班")
                        {
                            AnalogNightAttendData(ref exceptionType, dayMiddleTime, currentTime, dayPmstart, dayPmend);
                        }
                    }
                });
            }
            slotExceptionType = exceptionType;
        }

        private void AnalogNightAttendData(ref string slotExceptionType, DateTime dayMiddleTime, DateTime currentTime, DateTime dayPmstart, DateTime dayPmend)
        {
            if (currentTime > dayPmstart && currentTime < dayPmend)
            {
                MergeSlotExceptionType(ref slotExceptionType, "迟到");
            }
            else if (currentTime > dayPmend)
            {
                MergeSlotExceptionType(ref slotExceptionType, "旷工");
            }
        }

        private void AnalogDayAttendData(ref string slotExceptionType, DateTime dayMiddleTime, DateTime currentTime, DateTime dayAmstart, DateTime dayAmend)
        {
            if (currentTime > dayAmstart && currentTime < dayAmend)
            {
                MergeSlotExceptionType(ref slotExceptionType, "迟到");
            }
            else if (currentTime > dayAmend)
            {
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

        #endregion handle attend exception method

        #endregion method
    }

    /// <summary>
    /// 实时数据处理器
    /// </summary>
    public class AttendFingerPrintDataInTimeHandler
    {
        #region member

        private IAttendFingerPrintDataInTimeRepository irep = null;

        #endregion member

        #region constructure

        public AttendFingerPrintDataInTimeHandler()
        {
            this.irep = new AttendFingerPrintDataInTimeRepository();
        }

        #endregion constructure

        #region property

        /// <summary>
        /// 实时的刷卡数据
        /// </summary>
        public List<AttendFingerPrintDataInTimeModel> FingPrintDatas
        {
            get
            {
                return this.irep.Entities.ToList();
            }
        }

        #endregion property

        #region method

        public int Delete(AttendFingerPrintDataInTimeModel entity)
        {
            return this.irep.Delete(e => e.WorkerId == entity.WorkerId);
        }

        /// <summary>
        /// 实时考勤数据表中是否有考勤数据
        /// </summary>
        public bool IsExsitAttendData
        {
            get
            {
                var datas = this.irep.Entities;
                return datas != null && datas.Count() > 0;
            }
        }

        #endregion method
    }

    public class AttendAskLeaveManager
    {
        #region member

        private AttendSlodFingerDataCurrentMonthHandler currentMonthAttendDataHandler = null;

        #endregion member

        #region constructure

        public AttendAskLeaveManager()
        {
            this.currentMonthAttendDataHandler = new AttendSlodFingerDataCurrentMonthHandler();
        }

        #endregion constructure



        #region method

        /// <summary>
        /// 处理请假信息
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public OpResult HandleAskForLeave(List<AttendAskLeaveModel> entities)
        {
            int record = currentMonthAttendDataHandler.SyncAskLeaveData(entities);
            return OpResult.SetResult("请假操作成功！", record > 0);
        }

        public OpResult HandleAskForLeave(List<AttendSlodFingerDataCurrentMonthModel> entities)
        {
            int record = currentMonthAttendDataHandler.UpdateAskLeaveData(entities);
            return OpResult.SetResult("修改请假数据成功！", record > 0);
        }

        /// <summary>
        /// 获取该作业人员的请假信息
        /// </summary>
        /// <param name="workerId"></param>
        /// <returns></returns>
        public List<AttendSlodFingerDataCurrentMonthModel> GetAskLeaveDataAbout(string workerId, string qryMonth)
        {
            return this.currentMonthAttendDataHandler.GetAskLeaveDataAbout(workerId, qryMonth);
        }

        #endregion method
    }
}