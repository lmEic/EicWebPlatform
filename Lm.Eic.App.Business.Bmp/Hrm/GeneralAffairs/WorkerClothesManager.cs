using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.GeneralAffairs;
using Lm.Eic.App.DomainModel.Bpm.Hrm.GeneralAffairs;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using CrudFactory = Lm.Eic.App.Business.Bmp.Hrm.GeneralAffairs.GeneralAffairsFactory;

namespace Lm.Eic.App.Business.Bmp.Hrm.GeneralAffairs
{
    /// <summary>
    /// 厂服管理器
    /// </summary>
    public class WorkerClothesManager
    {
        /// <summary>
        /// 获取领用记录  搜索模式 1 => 按工号查找  2 => 按部门查找  3 => 按录入日期查找 
        /// </summary>
        /// <param name="dto">总务数据查询数据传输对象</param>
        /// <returns></returns>
        public List<WorkClothesManageModel> GetReceiveRecord(QueryGeneralAffairsDto dto)
        {
            return CrudFactory.WorkerClothesCrud.FindBy(dto);
        }

        /// <summary>
        /// 是否可以以旧换新
        /// </summary>
        /// <param name="workerId">工号</param>
        /// <param name="productName">厂服名称</param>
        /// <returns></returns>
        public bool CanOldForNew(string workerId, string productName)
        {
            //冬季厂服满三年允许更换一次  夏季厂服满两年允许更换一次
            try
            {
                var workClothesList = CrudFactory.WorkerClothesCrud.FindBy(new QueryGeneralAffairsDto { WorkerId = workerId, SearchMode = 1 });
                if (workClothesList == null || workClothesList.Count() <= 0) return true;

                DateTime yearDate = DateTime.Now.Date.AddYears(-2);
                if (productName == "冬季厂服")
                    yearDate = yearDate.AddYears(-1);
                var returnWorkClothes = workClothesList.Where(e => e.ProductName == productName & e.InputDate >= yearDate);
                return returnWorkClothes == null || returnWorkClothes.Count() <= 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// 领取厂服
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult ReceiveWorkClothes(WorkClothesManageModel model)
        {
            //处理类型 判断是以旧换新 还是新领取 然后判断是否有资格
            try
            {
                //  处理类型只有“以旧换新”，“新领取”
                //  是  “新领取” 不用判断是否有资格
                if (model == null) return OpResult.SetResult("数据不能这空"); 
                if((model.DealwithType =="以旧换新") &(!CanOldForNew(model.WorkerId ,model.ProductName)))
                {
                    return OpResult.SetResult("不能以旧换新"); 
                }
                return CrudFactory.WorkerClothesCrud.Store(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
           
        }

    }


    



    /// <summary>
    /// 厂服管理CRUD
    /// </summary>
    internal class WorkerClothesCrud : CrudBase<WorkClothesManageModel, IWorkClothesManageModelRepository>
    {
        public WorkerClothesCrud() : base(new WorkClothesManageModelRepository())
        { }

        #region FindBy
        /// <summary>
        /// 查询  搜索模式 1 => 按工号查找  2 => 按部门查找  3 => 按录入日期查找 
        /// </summary>
        /// <param name="qryDto">数据传输对象 </param>
        /// <returns></returns>
        public List<WorkClothesManageModel> FindBy(QueryGeneralAffairsDto qryDto)
        {
            if (qryDto == null) return new List<WorkClothesManageModel>();
            try
            {
                switch (qryDto.SearchMode)
                {
                    case 1: //依据按工号查找
                        return irep.Entities.Where(m => m.WorkerId == (qryDto.WorkerId)).ToList();
                    case 2: //依据按部门查找
                        return irep.Entities.Where(m => m.Department == (qryDto.Department)).ToList();
                    case 3: //依据录入日期查找 
                        return irep.Entities.Where(m => m.InputDate == (qryDto.InputDate)).ToList();
                    default:
                        return new List<WorkClothesManageModel>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        #endregion


        #region     store
        public OpResult Store(WorkClothesManageModel model)
        {
            model.ReceiveMonth = DateTime.Now.ToString("yyyyMM");
            return this.StoreEntity(model,
                mdl =>
                {
                    var result = this.PersistentDatas(model,
                                     mAdd =>
                                         { return AddWorkClothesManageRecord(model); },
                                      mUpdate =>
                                        { return EditWorkClothesManageRecord(model); }
                                   );
                    return result;
                });
        }

        /// <summary>
        /// 添加一条新增的信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult AddWorkClothesManageRecord(WorkClothesManageModel model)
        {
            model.InputDate = DateTime.Now.Date;
            if (irep.IsExist(m => m.Id_Key == model.Id_Key))
            {
                return OpResult.SetResult("此数据已存在！");
            }
            return irep.Insert(model).ToOpResult_Add("添加完成", model.Id_Key);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult EditWorkClothesManageRecord(WorkClothesManageModel model)
        {
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt("更新信息");
        }
        #endregion
    }


}
