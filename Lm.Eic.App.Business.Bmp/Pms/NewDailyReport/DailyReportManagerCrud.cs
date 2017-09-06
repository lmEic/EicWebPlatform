using Lm.Eic.App.DbAccess.Bpm.Repository.PmsRep.NewDailyReport;
using Lm.Eic.App.DomainModel.Bpm.Pms.NewDailyReport;
using Lm.Eic.App.Erp.Bussiness.MocManage;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.NewDailyReport
{
    internal class DailyReportCrudFactory
    {
        /// <summary>
        ///生产工序管理
        /// </summary>
        public static ProductionFlowCrud ProductionFlowCrud
        { get { return OBulider.BuildInstance<ProductionFlowCrud>(); } }

        public static DailyProductionReportCrud DailyProductionReport
        {
            get { return OBulider.BuildInstance<DailyProductionReportCrud>(); }
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
                SetFixFieldValue(modelList, OpMode.Add);
                //处理内部内容 UPS　UPS处理
                modelList.ForEach((m) =>
                {
                    if (m.StandardProductionTimeType == "UPH")
                    {
                        m.UPH = m.StandardProductionTime;
                        m.UPS = Math.Round(3600 / m.StandardProductionTime, 4);
                    }
                    else
                    {
                        m.UPH = Math.Round(3600 / m.StandardProductionTime, 4);
                        m.UPS = m.StandardProductionTime;
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
            //生成组合键值
            if (model.MouldId != null)
                model.ParameterKey = string.Format("{0}&{1}&{2}&{3}", model.Department, model.ProductName, model.ProcessesName, model.MouldId);
            else model.ParameterKey = string.Format("{0}&{1}&{2}", model.Department, model.ProductName, model.ProcessesName);

            if (model.StandardProductionTimeType == "UPH")
            {
                model.UPH = model.StandardProductionTime;
                model.UPS = 3600 / model.StandardProductionTime;
            }
            else
            {
                model.UPH = 3600 / model.StandardProductionTime;
                model.UPS = model.StandardProductionTime;
            }
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
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
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
            try
            {
                switch (qryDto.SearchMode)
                {
                    case 1: //依据部门查询
                        return irep.Entities.Where(m => m.Department == qryDto.Department).ToList();
                    case 2: //依据产品品名查询
                        return irep.Entities.Where(m => m.Department == qryDto.Department && m.ProductName == qryDto.ProductName).OrderBy(e => e.ProcessesIndex).ToList();
                    case 3: //依据录入日期查询
                        DateTime inputDate = qryDto.InputDate.ToDate();
                        return irep.Entities.Where(m => m.Department == qryDto.Department && m.OpDate == inputDate).ToList();
                    case 4: //依据工艺名称查询
                        return irep.Entities.Where(m => m.Department == qryDto.Department && m.ProductName == qryDto.ProductName && m.ProcessesName == qryDto.ProcessesName).ToList();
                    case 5: //依据工单单号查询
                        {
                            var orderDetails = MocService.OrderManage.GetOrderDetails(qryDto.OrderId);
                            if (orderDetails != null)
                                qryDto.ProductName = orderDetails.ProductName;
                            return irep.Entities.Where(m => m.Department == qryDto.Department && m.ProductName == qryDto.ProductName).ToList();
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
        public List<ProductFlowSummaryVm> GetProductionFlowSummaryDatesBy(string department, string productName = null)
        {
            return irep.GetProductFlowSummaryDatasBy(department, productName);
        }
        public ProductFlowSummaryVm GetProductionFlowSummaryDateBy(string productName)
        {
            return irep.GetProductFlowSummaryDataBy(productName);
        }
    }
    /// <summary>
    /// 生产日报表CRUD
    /// </summary>
    internal class DailyProductionReportCrud : CrudBase<DailyProductionReportModel, IDailyProductionReportRepository>
    {
        public DailyProductionReportCrud() : base(new DailyProductionReportRepository(), "生产日报表")
        { }

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
    }

}
