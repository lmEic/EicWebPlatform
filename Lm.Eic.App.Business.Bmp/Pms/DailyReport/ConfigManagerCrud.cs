using Lm.Eic.App.DbAccess.Bpm.Repository.PmsRep.DailyReport;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.App.Erp.Bussiness.MocManage;
using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.App.Business.Bmp.Pms.DailyReport.LmProMasterDailyReort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.DailyReport
{

    internal class DailyReportCrudFactory
    {
        /// <summary>
        ///工序管理
        /// </summary>
        public static ProductFlowCrud ProductFlowCrud
        { get { return OBulider.BuildInstance<ProductFlowCrud>(); } }

        /// <summary>
        /// 机台
        /// </summary>
        public static MachineCrud MachineCrud
        {
            get { return OBulider.BuildInstance<MachineCrud>(); }
        }

        /// <summary>
        /// 非生产原因
        /// </summary>
        public static NonProductionReasonCrud NonProductionReasonCrud
        {
            get { return OBulider.BuildInstance<NonProductionReasonCrud>(); }
        }

        /// <summary>
        /// 工单信息
        /// </summary>
        public static DailyOrderCrud DailyOrderCrud
        {
            get { return OBulider.BuildInstance<DailyOrderCrud>(); }
        }
        /// <summary>
        /// 出勤信息
        /// </summary>

        public static ReportsAttendenceCrud ReportsAttendenceCrud
        {

            get { return OBulider.BuildInstance<ReportsAttendenceCrud>(); }
        }

        /// <summary>
        /// 制三部日报表CRUD
        /// </summary>
        public static LmProDailyReportCrud LmProDailyReportCrud
        {
            get { return OBulider.BuildInstance<LmProDailyReportCrud>(); }
        }
    }


    /// <summary>
    /// 工序CRUD
    /// </summary>
    internal class ProductFlowCrud : CrudBase<ProductFlowModel, IProductFlowRepositoryRepository>
    {
        public ProductFlowCrud() : base(new ProductFlowRepositoryRepository(), "工艺")
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
        public OpResult AddProductFlowModelList(List<ProductFlowModel> modelList)
        {
            try
            {
                SetFixFieldValue(modelList, OpMode.Add);
                //处理内部内容
                modelList.ForEach((m) =>
                {
                    if (m.MaxMachineCount != 0)
                    {
                        m.MinMachineCount = 1;
                        m.DifficultyCoefficient = m.MinMachineCount / m.MaxMachineCount;
                    }
                });
                return irep.Insert(modelList).ToOpResult_Add(OpContext);
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
            AddOpItem(OpMode.Add, AddProductFlowModel);
            AddOpItem(OpMode.Edit, EditProductFlowModel);
            AddOpItem(OpMode.Delete, DeleteProductFlowModel);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult AddProductFlowModel(ProductFlowModel model)
        {
            //生成组合键值
            if (model.MouldId != null)
                model.ParameterKey = string.Format("{0}&{1}&{2}&{3}", model.Department, model.ProductName, model.ProductFlowName, model.MouldId);
            else model.ParameterKey = string.Format("{0}&{1}&{2}", model.Department, model.ProductName, model.ProductFlowName);

            //此工艺是否已经存在
            if (irep.IsExist(e => e.ParameterKey == model.ParameterKey))
                return OpResult.SetResult("此数据已经添加!");

            return irep.Insert(model).ToOpResult(OpContext);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult EditProductFlowModel(ProductFlowModel model)
        {
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult DeleteProductFlowModel(ProductFlowModel model)
        {
            return (model.Id_Key > 0) ?
                irep.Delete(u => u.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext)
                : OpResult.SetResult("未执行任何操作");
        }
        #endregion
        /// <summary>
        /// 查询 1.依据部门查询  2.依据产品品名查询 3.依据录入日期查询 4.依据产品品名 工艺名称查询 
        /// 5.依据工单单号查询
        /// </summary>
        /// <param name="qryDto">数据传输对象 部门是必须的 </param>
        /// <returns></returns>
        public List<ProductFlowModel> FindBy(QueryDailyReportDto qryDto)
        {
            try
            {
                switch (qryDto.SearchMode)
                {
                    case 1: //依据部门查询
                        return irep.Entities.Where(m => m.Department == qryDto.Department).ToList();
                    case 2: //依据产品品名查询
                        return irep.Entities.Where(m => m.Department == qryDto.Department && m.ProductName == qryDto.ProductName).OrderBy(e => e.ProductFlowId).ToList();
                    case 3: //依据录入日期查询
                        DateTime inputDate = qryDto.InputDate.ToDate();
                        return irep.Entities.Where(m => m.Department == qryDto.Department && m.OpDate == inputDate).ToList();
                    case 4: //依据工艺名称查询
                        return irep.Entities.Where(m => m.Department == qryDto.Department && m.ProductName == qryDto.ProductName && m.ProductFlowName == qryDto.ProductFlowName).ToList();
                    case 5: //依据工单单号查询
                        {
                            var orderDetails = MocService.OrderManage.GetOrderDetails(qryDto.OrderId);

                            if (orderDetails == null)
                                orderDetails = DailyReportCrudFactory.DailyOrderCrud.GetOrderDetails(qryDto.OrderId);

                            if (orderDetails != null)
                                qryDto.ProductName = orderDetails.ProductName;
                            return irep.Entities.Where(m => m.Department == qryDto.Department && m.ProductName == qryDto.ProductName).ToList();
                        }
                    default:
                        return new List<ProductFlowModel>();
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
        public List<ProductFlowOverviewModel> GetProductFlowOverviewListBy(string department)
        {
            return irep.GetProductFlowOverviewListBy(department);
        }

        public List<ProductFlowOverviewModel> GetProductFlowOverviewListBy(string department,string containsProductName)
        {
            return irep.GetProductFlowOverviewListBy(department, containsProductName);
        }
        /// <summary>
        /// 获取产品工艺总览 =》品名和部门是必须的
        /// </summary>
        /// <param name="dto">数据传输对象 请设置部门和品名</param>
        /// <returns></returns>
        public ProductFlowOverviewModel GetProductFlowOverviewBy(QueryDailyReportDto dto)
        {
            return irep.GetProductFlowOverviewBy(dto);
        }

    }

    /// <summary>
    /// 机台CRUD
    /// </summary>
    internal class MachineCrud : CrudBase<MachineModel, IMachineRepositoryRepository>
    {
        public MachineCrud() : base(new MachineRepositoryRepository(), "机台管理")
        {
        }

        protected override void AddCrudOpItems()
        {
            //增加
            //this.AddOpItem(OpMode.Add, AddMachineRecord);
        }

        /// <summary>
        /// 获取机台列表
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public List<MachineModel> GetMachineListBy(string department)
        {
            try
            {
                return irep.Entities.Where(m => m.Department == department).ToList();
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
        }

        /// <summary>
        /// 添加一条机台记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult AddMachineRecord(MachineModel model)
        {
            try
            {
                model.OpSign = OpMode.Add;
                SetFixFieldValue(model);
                return irep.Insert(model).ToOpResult_Add(OpContext);
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
        }
    }


    /// <summary>
    /// 非生产原因CRUD
    /// </summary>
    internal class NonProductionReasonCrud : CrudBase<NonProductionReasonModel, INonProductionReasonModelRepository>
    {
        public NonProductionReasonCrud() : base(new NonProductionReasonModelRepository(), "非生产原因")
        {
        }

        protected override void AddCrudOpItems()
        {
            // throw new NotImplementedException();
        }

        /// <summary>
        /// 获取非生产原因列表
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<NonProductionReasonModel> GetNonProductionListBy(string department)
        {
            try
            {
                return irep.Entities.Where(m => m.Department == m.Department).ToList();

            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
        }

        /// <summary>
        /// 添加一条非生产原因
        /// </summary>
        /// <param name="model">非生产原因实体模型</param>
        /// <returns></returns>
        public OpResult AddNonProductionRecord(NonProductionReasonModel model)
        {
            try
            {
                model.OpSign = OpMode.Add;
                SetFixFieldValue(model);
                return irep.Insert(model).ToOpResult_Add(OpContext);

            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
        }
    }

    /// <summary>
    /// 工单CRUD
    /// </summary>
    internal class DailyOrderCrud : CrudBase<DReportsOrderModel, IDReportsOrderModelRepository>
    {
        public DailyOrderCrud() : base(new DReportsOrderModelRepository(), "工单配置")
        {
        }

        protected override void AddCrudOpItems()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 获取工单详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderModel GetOrderDetails(string orderId)
        {
            var orderDetails = irep.Entities.FirstOrDefault(m => m.OrderId == orderId);
            return ConvertToErpOrderModel(orderDetails);
        }

        /// <summary>
        /// 转换为Erp工单模型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OrderModel ConvertToErpOrderModel(DReportsOrderModel model)
        {
            try
            {
                if (model == null)
                    return null;

                var orderDetails = new OrderModel();
                OOMaper.Mapper( model ,orderDetails);
                return orderDetails;
            }
            catch (Exception) {throw new NotImplementedException();}
        }

    }


    internal class ReportsAttendenceCrud : CrudBase<ReportsAttendenceModel, IReportsAttendenceModelRepository>
    {
        public ReportsAttendenceCrud():base (new ReportsAttendenceModelRepository(),"日报出勤人员时数表")
        { }
        protected override void AddCrudOpItems()
        {
            AddOpItem(OpMode.Add, AddReportAttendence);
            AddOpItem(OpMode.Edit, EditReportAttendece);

        }
        OpResult AddReportAttendence(ReportsAttendenceModel entity)
        {
            return irep.Insert(entity).ToOpResult(OpContext + "保存操作成功", OpContext + "保存操作失败"); 
        }

        OpResult EditReportAttendece(ReportsAttendenceModel entity)
        {
            entity.Id_key = GetIdkeyBy(entity);
            return irep.Update(e => e.Id_key == entity.Id_key,
                                     entity).ToOpResult(OpContext + "修改操作成功", OpContext + "修改操作失败");
        }
        public bool IsExist(ReportsAttendenceModel entity)
        {
            return irep.IsExist(e => e.Department == entity.Department
                                   && e.ReportDate == entity.ReportDate
                                   && e.AttendenceStation == entity.AttendenceStation);
        }
        private decimal GetIdkeyBy(ReportsAttendenceModel entity)
        {
            if (entity.Id_key == 0)
            {
                return irep.Entities.Where(e => e.Department == entity.Department
                                       && e.ReportDate == entity.ReportDate
                                       && e.AttendenceStation == entity.AttendenceStation).Select(e => e.Id_key).ToList().FirstOrDefault();
            }
            else return entity.Id_key;
        }

        /// <summary>
        /// 得到部门的所有出勤数据
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public ReportsAttendenceModel GetReportsAttendence(string department, string attendenceStation, DateTime reportDate)
        {
            return irep.Entities.Where(e => e.Department == department && e.ReportDate == reportDate && e.AttendenceStation == attendenceStation).ToList().FirstOrDefault ();
        }
    }




 

}
