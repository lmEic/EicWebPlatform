using Lm.Eic.App.DbAccess.Bpm.Repository.PmsRep.DailyReport;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.DailyReport
{

    internal class DailyReportConfigCrudFactory
    {
        /// <summary>
        /// 日报录入
        /// </summary>
        public static ProductFlowCrud ProductFlowCrud
        { get { return OBulider.BuildInstance<ProductFlowCrud>(); } }

    }


    /// <summary>
    /// 工序CRUD
    /// </summary>
    public class ProductFlowCrud : CrudBase<ProductFlowModel, IProductFlowRepositoryRepository>
    {
        public ProductFlowCrud() : base(new ProductFlowRepositoryRepository(), "工艺")
        { }

        #region Store

        public OpResult Store(List<ProductFlowModel> modelList)
        {
            try
            {
                if (modelList == null || modelList.Count < 1)
                    return OpResult.SetResult("列表不能为空");
                int errCout = 0;
                modelList.ForEach((model) =>
                {
                    OpResult storeResult = Store(model);
                    if (!storeResult.Result) errCout++;
                });

                int total = modelList.Count;
                int victory = modelList.Count - errCout;
                return errCout > 0 ? OpResult.SetResult(string.Format("保存失败！ 总数：{0} 成功:{1} 失败:{2}", total, victory, errCout)) : OpResult.SetResult(string.Format("保存成功！记录数", total), true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

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
            OpResult opResult = OpResult.SetResult("未执行任何操作");
            if (model.Id_Key == 0)
                return OpResult.SetResult("Id_Key未设置！");

            opResult = irep.Delete(u => u.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
            return opResult;
        }
        #endregion




        /// <summary>
        /// 查询 1.依据部门查询  2.依据产品品名查询 3.依据录入日期查询 4.依据产品品名&工艺名称查询 
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
                        return irep.Entities.Where(m => m.Department == qryDto.Department && m.ProductName == qryDto.ProductName).ToList();
                    case 3: //依据录入日期查询
                        DateTime inputDate = qryDto.InputDate.ToDate();
                        return irep.Entities.Where(m => m.Department == qryDto.Department && m.OpDate == inputDate).ToList();
                    case 4: //依据工艺名称查询
                        return irep.Entities.Where(m => m.Department == qryDto.Department && m.ProductName == qryDto.ProductName && m.ProductFlowName == qryDto.ProductFlowName).ToList();
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

}
