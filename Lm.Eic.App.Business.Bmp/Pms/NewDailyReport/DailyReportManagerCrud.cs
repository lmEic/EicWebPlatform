﻿using Lm.Eic.App.DbAccess.Bpm.Repository.PmsRep.NewDailyReport;
using Lm.Eic.App.DomainModel.Bpm.Pms.NewDailyReport;
using Lm.Eic.App.Erp.Bussiness.MocManage;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.Validation;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.NewDailyReport
{
    /// <summary>
    /// Crud 工厂数据
    /// </summary>
    internal class DailyReportCrudFactory
    {
        /// <summary>
        ///生产工序管理
        /// </summary>
        public static ProductionFlowCrud ProductionFlowCrud
        { get { return OBulider.BuildInstance<ProductionFlowCrud>(); } }

        /// <summary>
        /// 生产订单分配
        /// </summary>
        public static ProductOrderDispatchCrud ProductOrderDispatch
        {
            get { return OBulider.BuildInstance<ProductOrderDispatchCrud>(); }
        }
        /// <summary>
        /// 日报表录入
        /// </summary>
        public static DailyProductionReportCrud DailyProductionReport
        {
            get { return OBulider.BuildInstance<DailyProductionReportCrud>(); }
        }
        /// <summary>
        /// 非生产原因
        /// </summary>
        public static UnproductiveReasonConfigCrud UnproductiveSeason
        {
            get { return OBulider.BuildInstance<UnproductiveReasonConfigCrud>(); }
        }
        /// <summary>
        /// 不良制程处理
        /// </summary>
        public static DailyProductionDefectiveTreatmentCrud ProductionDefectiveTreatment
        {
            get { return OBulider.BuildInstance<DailyProductionDefectiveTreatmentCrud>(); }
        }
        /// <summary>
        /// 机台信息Crud
        /// </summary>
        public static DailyReportsMachineCrud DailyReportsMachine
        {
            get { return OBulider.BuildInstance<DailyReportsMachineCrud>(); }
        }
    }
    /// <summary>
    /// 生产工序CRUD
    /// </summary>
    internal class ProductionFlowCrud : CrudBase<StandardProductionFlowModel, IStandardProductionFlowRepository>
    {
        public ProductionFlowCrud() : base(new StandardProductionFlowRepository(), "生产工艺流程")
        { }

        #region Store

        /// <summary>
        /// 删除产品工序列表
        /// </summary>
        /// <param name="department">部门</param>
        /// <param name="productName">产品品名</param>
        /// <returns></returns>
        public OpResult DeleteProductFlowModelBy(string department, string productName)
        {
            try
            {
                return irep.Delete(m => m.Department == department && m.ProductName == productName).ToOpResult_Delete(OpContext);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 添加列表到数据库中
        /// </summary>
        /// <param name="modelList">工序列表</param>
        /// <returns></returns>
        public OpResult AddProductFlowModelList(List<StandardProductionFlowModel> modelList)
        {
            try
            {
                OpResult result = OpResult.SetSuccessResult("数据初始操作!", true);
                ///只要有一个保存失败 后面的数所也不用操作
                modelList.ForEach((m) => {  if (result.Result)  result = Store(m); });
                ///返回最后一个操作的结果
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 重写添加项
        /// </summary>
        protected override void AddCrudOpItems()
        {
            AddOpItem(OpMode.Add, Add);
            AddOpItem(OpMode.Edit, Edit);
            AddOpItem(OpMode.Delete, Delete);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Add(StandardProductionFlowModel model)
        {
            model = HandleStoreModel(model);
            //此工艺是否已经存在
            if (irep.IsExist(e => e.ParameterKey == model.ParameterKey))
                return OpResult.SetErrorResult("此数据已经添加!");
            return irep.Insert(model).ToOpResult(OpContext);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Edit(StandardProductionFlowModel model)
        {
            model= HandleStoreModel(model);
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        /// <summary>
        /// 加工处理数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private StandardProductionFlowModel HandleStoreModel(StandardProductionFlowModel model)
        {
            ///生成组合键值  如果有模穴数 则关键字添加模穴数
            if (model.MouldId != null)
                model.ParameterKey = string.Format("{0}&{1}&{2}&{3}", model.Department, model.ProductName, model.ProcessesName, model.MouldId);
            else model.ParameterKey = string.Format("{0}&{1}&{2}", model.Department, model.ProductName, model.ProcessesName);
            ///   计算UPS值　也UPS的转化
            if (model.StandardProductionTimeType == "UPH")
            {
                model.UPH = model.StandardProductionTime;
                model.UPS = model.StandardProductionTime == 0 ? 0 : Math.Round(3600 / model.StandardProductionTime, 2);
            }
            else
            {
                model.UPH = model.StandardProductionTime == 0 ? 0 : Math.Round(3600 / model.StandardProductionTime, 0);
                model.UPS = model.StandardProductionTime;
            }
            return model;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Delete(StandardProductionFlowModel model)
        {
            return (model.Id_Key > 0) ?
                irep.Delete(u => u.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext)
                : OpResult.SetErrorResult("未执行任何操作");
        }



        #endregion
        /// <summary>
        /// 查询 1.依据部门查询  2.依据产品品名查询 3.依据录入日期查询 4.依据产品品名 工艺名称查询 
        /// 5.依据工单单号查询
        /// </summary>
        /// <param name="qryDto">数据传输对象 部门是必须的 </param>
        /// <returns></returns>
        public List<StandardProductionFlowModel> FindBy(QueryDailyProductReportDto qryDto)
        {
            if (qryDto == null) return new List<StandardProductionFlowModel>();
            try
            {
                switch (qryDto.SearchMode)
                {
                    case 1: //依据部门查询
                        return irep.Entities.Where(m => m.Department == qryDto.Department && m.IsValid == qryDto.IsValid).ToList();
                    case 2: //依据产品品名查询
                        return irep.Entities.Where(m => m.Department == qryDto.Department && m.IsValid == qryDto.IsValid && m.ProductName == qryDto.ProductName).OrderBy(e => e.ProcessesIndex).ToList();
                    case 3: //依据录入日期查询
                        DateTime inputDate = qryDto.InputDate.ToDate();
                        return irep.Entities.Where(m => m.Department == qryDto.Department && m.IsValid == qryDto.IsValid && m.OpDate == inputDate).ToList();
                    case 4: //依据工艺名称查询
                        return irep.Entities.Where(m => m.Department == qryDto.Department && m.IsValid == qryDto.IsValid && m.ProductName == qryDto.ProductName && m.ProcessesName == qryDto.ProcessesName).ToList();
                    case 5: //依据工单单号查询
                        {
                            var orderDetails = MocService.OrderManage.GetOrderDetails(qryDto.OrderId);
                            if (orderDetails != null)
                                qryDto.ProductName = orderDetails.ProductName;
                            return irep.Entities.Where(m => m.Department == qryDto.Department && m.IsValid == qryDto.IsValid && m.ProductName == qryDto.ProductName).ToList();
                        }
                    default:
                        return new List<StandardProductionFlowModel>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 获取产品总概述前30行
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public List<ProductFlowSummaryVm> GetProductionFlowSummaryDatesBy(string department, string productName)
        {
            return irep.GetProductFlowSummaryDatasBy(department, productName);
        }
        public ProductFlowSummaryVm GetProductionFlowSummaryDateBy(string department, string productName)
        {
            return irep.GetProductFlowSummaryDataBy(department, productName);
        }




        /// 删除产品工序列表
        /// </summary>
        /// <param name="department">部门</param>
        /// <param name="productName">产品品名</param>
        /// <returns></returns>
        public OpResult DeleteSingleProductFlow(string productName, string processesName)
        {
            try
            {
                return irep.Delete(m => m.ProcessesName == processesName && m.ProductName == productName).ToOpResult_Delete(OpContext);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
    }

    /// <summary>
    /// 生产订单分配表
    /// </summary>
    internal class ProductOrderDispatchCrud : CrudBase<ProductOrderDispatchModel, IProductOrderDispatchRepository>
    {
        public ProductOrderDispatchCrud() : base(new ProductOrderDispatchRepository(), "订单分配")
        {
        }
        #region Store
        protected override void AddCrudOpItems()
        {
            AddOpItem(OpMode.Add, Add);
            AddOpItem(OpMode.Edit, Edit);
            AddOpItem(OpMode.Delete, Delete);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Add(ProductOrderDispatchModel model)
        {
            //生成组合键值
            model.DicpatchStatus = (model.IsValid == "True" || model.IsValid == "true") ? "已分配" : "未分配";
            if (irep.IsExist(e => e.OrderId == model.OrderId))
            {
                return irep.Update(e => e.OrderId == model.OrderId, u => new ProductOrderDispatchModel
                {
                    IsValid = model.IsValid,
                    ProductionDate = model.ProductionDate,
                    ValidDate = model.ValidDate,
                    DicpatchStatus = model.DicpatchStatus,
                }).ToOpResult_Eidt(OpContext);
            };
            switch(model.ProductionDepartment)
            {
                case "MS1":
                    model.ProductName = ( model.OrderId.Contains("511")) ? model.ProductName : model.ProductName + "(非正常)";
                    break;
                case "MS6":
                    model.ProductName = (model.OrderId.Contains("520")) ? model.ProductName : model.ProductName + "(非正常)";
                    break;
                default:
                    break;
            };
            return irep.Insert(model).ToOpResult(OpContext);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Edit(ProductOrderDispatchModel model)
        {
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Delete(ProductOrderDispatchModel model)
        {
            return (model.Id_Key > 0) ?
                irep.Delete(u => u.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext)
                : OpResult.SetErrorResult("未执行任何操作");
        }
        #endregion
        /// <summary>
        /// 获得当前已经分配的订单
        /// </summary>
        /// <param name="department">部门</param>
        /// <param name="dicpatchStatus">分配状态状态</param>
        /// <param name="isValid">是否有效</param>
        /// <returns></returns>
        internal List<ProductOrderDispatchModel> GetHaveDispatchOrderBy(string department, string dicpatchStatus, string isValid=null)
        {
            /// 1.要有效
            return  isValid == null?
            irep.Entities.Where(e => e.ProductionDepartment == department && e.DicpatchStatus == dicpatchStatus).OrderBy(e => e.OrderId).ToList():
            irep.Entities.Where(e => e.ProductionDepartment == department && e.DicpatchStatus == dicpatchStatus && e.IsValid== isValid).OrderBy(e => e.OrderId).ToList();
        }
        internal List<ProductOrderDispatchModel> GetDepartmentDispatchOrderDataBy(string department,int isVirtualOrderId=1)
        {
            /// 1.要有效
            return irep.Entities.Where(e => e.ProductionDepartment == department && e.IsVirtualOrderId == isVirtualOrderId).OrderBy(e => e.OrderId).ToList();
        }
        internal ProductOrderDispatchModel GetOrderInfoBy(string orderid)
        {
            /// 1.要有效
            return irep.Entities.FirstOrDefault(e => e.OrderId == orderid);
        }
    }

    /// <summary>
    /// 生产日报录入表CRUD
    /// </summary>
    internal class DailyProductionReportCrud : CrudBase<DailyProductionReportModel, IDailyProductionReportRepository>
    {
        public DailyProductionReportCrud() : base(new DailyProductionReportRepository(), "生产日报表")
        { }
        #region   Store
        protected override void AddCrudOpItems()
        {
            AddOpItem(OpMode.Add, Add);
            AddOpItem(OpMode.Edit, Edit);
            AddOpItem(OpMode.Delete, Delete);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Add(DailyProductionReportModel model)
        {
            ///日期格式 简化
            model.InPutDate = model.InPutDate.ToDate();
            if (model.OrderId == null || model.OrderId == string.Empty) return OpResult.SetErrorResult(OpContext);
            if(model.StandardProductionTime != 0&& model.TodayProductionCount!=0)
                model.GetProductionTime =Math.Round(model.TodayProductionCount * model.StandardProductionTime / 3600,2);
            //生成组合键值
            return irep.Insert(model).ToOpResult(OpContext);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Edit(DailyProductionReportModel model)
        {
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Delete(DailyProductionReportModel model)
        {
            return (model.Id_Key > 0) ?
                irep.Delete(u => u.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext)
                : OpResult.SetErrorResult("未执行任何操作");
        }
        #endregion

        #region find
        internal bool IsExisOrderid(string orderId)
        {
            return irep.IsExist(e => e.OrderId == orderId);
        }
        /// <summary>
        /// 入库数量
        /// </summary>
        /// <param name="department"></param>
        /// <param name="orderId"></param>
        /// <param name="processesName"></param>
        /// <returns></returns>
        internal double GetDailyProductionCountBy(string orderId, string processesName)
        {
            var datas = irep.Entities.Where(e => e.OrderId == orderId && e.ProcessesName == processesName).ToList();
            return (datas == null || datas.Count == 0) ? 0 : datas.Sum(f => f.TodayProductionCount);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal List<DailyProductionReportModel> GetDailyDatasBy(DateTime date, string orderId, string processesName)
        {
            DateTime dateShort = date.ToDate();
            return irep.Entities.Where(e => e.OrderId == orderId && e.ProcessesName == processesName).OrderBy(f => f.InPutDate).ToList();
        }
        internal List<DailyProductionReportModel> GetWorkerDailyDatasBy(string workerId)
        {
            return irep.Entities.Where(e => e.WorkerId == workerId).OrderByDescending(e => e.Id_Key).ToList();
        }

        internal OpResult SavaDailyReportList(List<DailyProductionReportModel> modelList)
        {
            try
            {
                if (!modelList.IsNullOrEmpty())
                return OpResult.SetErrorResult("日报列表不能为空！ 保存失败");
                SetFixFieldValue(modelList, OpMode.Add);
                int i = 0;
                modelList.ForEach((model) => { if (Store(model).Result) i++; });
                return (modelList.Count == i) ? new OpResult(OpContext, true) : new OpResult( "还有"+ (modelList.Count-i)+"数据没有保存", false);
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
        }
        #endregion
    }

    /// <summary>
    /// 生产代码配置表CRUD
    /// </summary>
    internal class UnproductiveReasonConfigCrud : CrudBase<UnproductiveReasonConfigModel, IUnproductiveReasonConfigRepository>
    {
        public UnproductiveReasonConfigCrud() : base(new UnproductiveReasonConfigRepository(), "非生产代码编码")
        { }
        #region   Store
        protected override void AddCrudOpItems()
        {
            AddOpItem(OpMode.Add, Add);
            AddOpItem(OpMode.Edit, Edit);
            AddOpItem(OpMode.Delete, Delete);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Add(UnproductiveReasonConfigModel model)
        {
            //生成组合键值
            return irep.Insert(model).ToOpResult(OpContext);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Edit(UnproductiveReasonConfigModel model)
        {
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Delete(UnproductiveReasonConfigModel model)
        {
            return (model.Id_Key > 0) ?
                irep.Delete(u => u.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext)
                : OpResult.SetErrorResult("未执行任何操作");
        }
        #endregion

        public List<UnproductiveReasonConfigModel> GetProductionDictiotry(string aboutCategory, string department)
        {
            return irep.Entities.Where(e => e.AboutCategory == aboutCategory && e.Department.Contains(department)).ToList();
        }


    }
    /// <summary>
    /// 不良制程处理
    /// </summary>
    internal class DailyProductionDefectiveTreatmentCrud:CrudBase<DailyProductionDefectiveTreatmentModel, IProductionDefectiveTreatmentRepository>
    {
        public DailyProductionDefectiveTreatmentCrud():base(new ProductionDefectiveTreatmentRepository(),"不良制程处理")
        { }
        #region 
        protected override void AddCrudOpItems()
        {
            AddOpItem(OpMode.Add, Add);
            AddOpItem(OpMode.Edit, Edit);
            AddOpItem(OpMode.Delete, Delete);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Add(DailyProductionDefectiveTreatmentModel model)
        {
            //
            return irep.Insert(model).ToOpResult(OpContext);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Edit(DailyProductionDefectiveTreatmentModel model)
        {
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Delete(DailyProductionDefectiveTreatmentModel model)
        {
            return (model.Id_Key > 0) ?
                irep.Delete(u => u.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext)
                : OpResult.SetErrorResult("未执行任何操作");
        }
       #endregion

    }


    /// <summary>
    /// 日报表机台信息
    /// </summary>
    internal class DailyReportsMachineCrud : CrudBase<ReportsMachineModel, IReportsMachineRepository>
    {
        public DailyReportsMachineCrud() : base(new ReportsMachineRepository(), "机台信息")
        { }
        #region  CRUD
        protected override void AddCrudOpItems()
        {
            AddOpItem(OpMode.Add, Add);
            AddOpItem(OpMode.Edit, Edit);
            AddOpItem(OpMode.Delete, Delete);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Add(ReportsMachineModel model)
        {
            //
            return irep.Insert(model).ToOpResult(OpContext);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Edit(ReportsMachineModel model)
        {
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Delete(ReportsMachineModel model)
        {
            return (model.Id_Key > 0) ?
                irep.Delete(u => u.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext)
                : OpResult.SetErrorResult("未执行任何操作");
        }
        #endregion


        #region  find

        public List<ReportsMachineModel> GetMachineDatas(string department)
        {
            return irep.Entities.Where(e => e.Department == department).ToList();
        }

        #endregion
    }
}


