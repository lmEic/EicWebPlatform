using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.GeneralAffairs;
using Lm.Eic.App.DomainModel.Bpm.Hrm.GeneralAffairs;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.Business.Bmp.Hrm.GeneralAffairs
{
    /// <summary>
    /// 厂服管理器
    /// </summary>
    public class WorkerClothesManager
    {
        /// <summary>
        /// 获取领用记录
        /// </summary>
        /// <param name="dto">总务数据查询数据传输对象</param>
        /// <returns></returns>
        public List<WorkClothesManageModel> GetReceiveRecord(QueryGeneralAffairsDto dto)
        {
            //TODO:  搜索模式 1 => 按工号查找  2 => 按部门查找  3 => 按录入日期查找 
            return new List<WorkClothesManageModel>();
        }

        /// <summary>
        /// 是否可以以旧换新
        /// </summary>
        /// <param name="workerId">工号</param>
        /// <param name="productCategory">厂服类别</param>
        /// <returns></returns>
        public bool IsOldForNew(string workerId,string productCategory)
        {
            //冬季厂服满三年允许更换一次  夏季厂服满两年允许更换一次
            return false;
        }

        /// <summary>
        /// 领取厂服
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult ReceiveWorkClothes(WorkClothesManageModel model)
        {
            //处理类型 判断是以旧换新 还是新领取 然后判断是否有资格
            return null;
        }

    }

    /// <summary>
    /// 厂服管理CRUD
    /// </summary>
    internal class WorkerClothesCrud : CrudBase<WorkClothesManageModel, IWorkClothesManageModelRepository>
    {
        public WorkerClothesCrud() : base(new WorkClothesManageModelRepository())
        {
        }
    }


}
