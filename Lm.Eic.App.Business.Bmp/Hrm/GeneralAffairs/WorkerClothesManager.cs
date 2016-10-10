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
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using CrudFactory = Lm.Eic.App.Business.Bmp.Hrm.GeneralAffairs.GeneralAffairsFactory;
using System.IO;


namespace Lm.Eic.App.Business.Bmp.Hrm.GeneralAffairs
{
    /// <summary>
    /// 厂服管理器
    /// </summary>
    public class WorkerClothesManager
    {
        List<WorkClothesManageModel> _workClothesmangeModelList = new List<WorkClothesManageModel>();
        List<FileFieldMapping> fieldmappping = new List<FileFieldMapping>(){
                  new FileFieldMapping {FieldName ="WorkerId",FieldDiscretion="工号",},
                  new FileFieldMapping {FieldName ="WorkerName",FieldDiscretion="姓名",} ,
                  new FileFieldMapping {FieldName ="Department",FieldDiscretion="部门",} ,
                  new FileFieldMapping {FieldName ="ProductName",FieldDiscretion="厂服内类型",} ,
                  new FileFieldMapping {FieldName ="ProductSpecify",FieldDiscretion="规格",},
                  new FileFieldMapping {FieldName ="PerCount",FieldDiscretion="领取数量",},
                  new FileFieldMapping {FieldName ="InputDate",FieldDiscretion="录入日期",},
                  new FileFieldMapping {FieldName ="DealwithType",FieldDiscretion="处理类型",} ,
                  new FileFieldMapping {FieldName ="ReceiveMonth",FieldDiscretion="领用月份",}
                };
        /// <summary>
        /// 获取领用记录  搜索模式 1 => 按工号查找  2 => 按部门查找  3 => 按领取月查找 
        /// </summary>
        /// <param name="dto">总务数据查询数据传输对象</param>
        /// <returns></returns>
        public List<WorkClothesManageModel> FindReceiveRecordBy(QueryGeneralAffairsDto dto)
        {
            _workClothesmangeModelList = CrudFactory.WorkerClothesCrud.FindBy(dto);
            return _workClothesmangeModelList;
        }
        /// <summary>
        /// 是否可以以旧换新
        /// </summary>
        /// <param name="workerId">工号</param>
        /// <param name="productName">厂服名称</param>
        /// <param name="dealwithType">处理方式</param>
        /// <returns></returns>
        public bool CanOldChangeNew(string workerId, string productName, string dealwithType)
        {
            // "以旧换旧"不用判定
            //冬季厂服满三年允许更换一次  夏季厂服满两年允许更换一次
            try
            {
                WorkClothesManageModel model = new WorkClothesManageModel()
                {
                    WorkerId = workerId,
                    ProductName = productName,
                    DealwithType = dealwithType
                };

                return CrudFactory.WorkerClothesCrud.IsCanOldChangeNew(model);

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
        public OpResult StoreReceiveWorkClothes(WorkClothesManageModel model)
        {
            return CrudFactory.WorkerClothesCrud.Store(model);
        }

        /// <summary>
        /// 生成厂服领取清单
        /// </summary>
        /// <returns></returns>
        public MemoryStream BuildReceiveWorkClothesList()
        {
            var dataGroupping = _workClothesmangeModelList.GetGroupList<WorkClothesManageModel>("ReceiveMonth");
            //生成厂服领取清单
          return   dataGroupping.ExportToExcelMultiSheets<WorkClothesManageModel>(fieldmappping);
        }

    }

    /// <summary>
    /// 厂服管理CRUD
    /// </summary>
    internal class WorkerClothesCrud : CrudBase<WorkClothesManageModel, IWorkClothesManageModelRepository>
    {
        public WorkerClothesCrud()
            : base(new WorkClothesManageModelRepository(), "厂服管理")
        { }


        protected override void AddCrudOpItems()
        {    //增加
            this.AddOpItem(OpMode.Add, AddWorkClothesManageRecord);
            //编辑
            this.AddOpItem(OpMode.Edit, EditWorkClothesManageRecord);
            //修改
            this.AddOpItem(OpMode.UpDate, UpDateWorkClothesManageRecord);
        }
        #region FindBy
        /// <summary>
        /// 查询  搜索模式 1 => 按工号查找  2 => 按部门查找  3 => 按领取月查找 
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
                    case 3: //按领取月查找 
                        return irep.Entities.Where(m => m.ReceiveMonth == qryDto.ReceiveMonth).ToList();
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

        public bool IsCanOldChangeNew(WorkClothesManageModel model)
        {
            // "以旧换旧"不用判定
            //冬季厂服满三年允许更换一次  夏季厂服满两年允许更换一次
            try
            {
                if (model.DealwithType == "以旧换旧") return true;
                if (model.DealwithType == "购买新衣") return true;
                var workClothesList = CrudFactory.WorkerClothesCrud.FindBy(new QueryGeneralAffairsDto { WorkerId = model.WorkerId, SearchMode = 1 });
                if (workClothesList == null || workClothesList.Count() <= 0) return true;
                DateTime yearDate = DateTime.Now.Date.AddYears(-2);
                if (model.ProductName == "冬季厂服")
                    yearDate = yearDate.AddYears(-1);
                //排除“以旧换旧” 的时间  再判断
                var returnWorkClothes = workClothesList.Where(e => e.ProductName == model.ProductName && e.DealwithType != "以旧换旧" && e.DealwithType != "购买新衣" && e.InputDate >= yearDate);
                bool result = (returnWorkClothes == null || returnWorkClothes.Count() <= 0);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        #region     store
        /// <summary>
        /// 数据持久化
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public override OpResult Store(WorkClothesManageModel model)
        {
            model.InputDate = DateTime.Now.Date;
            model.ReceiveMonth = DateTime.Now.ToString("yyyyMM");
            return this.PersistentDatas(model);
        }


        /// <summary>
        /// 添加一条新增的信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult AddWorkClothesManageRecord(WorkClothesManageModel model)
        {

            if (irep.IsExist(m => m.Id_Key == model.Id_Key))
            {
                return OpResult.SetResult("此数据已存在！");
            }
            if ( !IsCanOldChangeNew(model))
            {
                return OpResult.SetResult("该用户暂无资格以旧换新！");
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
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt(model.WorkerName.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult UpDateWorkClothesManageRecord(WorkClothesManageModel model)
        {
            OpResult result = OpResult.SetResult("未执行任何修改");
            if (model == null) return result;
            return result;

        }
        #endregion
    }


}
