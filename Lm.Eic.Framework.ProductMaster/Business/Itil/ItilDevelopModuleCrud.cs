using Lm.Eic.Framework.ProductMaster.DbAccess.Repository;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Lm.Eic.Uti.Common.YleeExtension.Validation;
using Lm.Eic.Uti.Common.YleeMessage.Email;

namespace Lm.Eic.Framework.ProductMaster.Business.Itil
{

    //CRUD
    /***********************************************   模块开发管理CRUD工厂   **************************
    *                                        
    *  2017-7-27  初版   张明                  
    ***************************************************************************************************/
    /// <summary>
    /// 模块开发管理Crud工厂
    /// </summary>
    internal class ItilCrudFactory
    {
        /// <summary>
        /// 模块开发管理Crud
        /// </summary>
        public static ItilDevelopModuleManageCrud ItilDevelopModuleManageCrud
        {
            get { return OBulider.BuildInstance<ItilDevelopModuleManageCrud>(); }
        }

        /// <summary>
        /// 模块开发管理进度履历Crud
        /// </summary>
        public static ItilDevelopModuleChangeRecordCrud ItilDevelopModuleChangeRecordCrud
        {
            get { return OBulider.BuildInstance<ItilDevelopModuleChangeRecordCrud>(); }
        }
    }


    /***********************************************   模块开发管理CRUD   ****************************
   *                                        
   *  2017-7-27  初版   张明                  
   ***************************************************************************************************/
    /// <summary>
    /// 模块开发管理CRUD
    /// </summary>
    internal class ItilDevelopModuleManageCrud : CrudBase<ItilDevelopModuleManageModel, IItilDevelopModuleManageRepository>
    {
        private List<ItilDevelopModuleManageModel> _waittingSendMailList = new List<ItilDevelopModuleManageModel>();
        /// <summary>
        /// 待发送邮件列表
        /// </summary>
        internal List<ItilDevelopModuleManageModel> WaittingSendMailList
        {
            get { return _waittingSendMailList; }
        }

        /// <summary>
        /// 构造函数 初始化数据访问接口
        /// </summary>
        public ItilDevelopModuleManageCrud() : base(new ItilDevelopModuleManageRepository(), "开发任务")
        {
        }

        #region Find


        /// <summary>
        /// 获取开发任务列表  1.依据状态列表查询 2.依据函数名称查询 
        /// </summary>
        /// <param name="dto">状态列表</param>
        /// <returns></returns>
        public List<ItilDevelopModuleManageModel> GetDevelopModuleManageListBy(ItilDto dto)
        {
            List<ItilDevelopModuleManageModel> itilList = new List<ItilDevelopModuleManageModel>();
            if (dto == null) return itilList;
            switch (dto.SearchMode)
            {
                case 1:
                    if (dto.ProgressSignList != null)
                        itilList = irep.Entities.Where(m => dto.ProgressSignList.Contains(m.CurrentProgress)).ToList();
                    break;
                case 2:
                    if (!dto.FunctionName.IsNullOrEmpty())
                        itilList = irep.Entities.Where(m => m.MFunctionName == dto.FunctionName).ToList();
                    break;
            }
            return itilList;
        }

        /// <summary>
        /// 获取开发任务进度变更明细
        /// </summary>
        /// <param name="model">开发任务</param>
        /// <returns></returns>
        public List<ItilDevelopModuleManageChangeRecordModel> GetChangeRecordListBy(ItilDevelopModuleManageModel model)
        {
            if (model != null && !model.ParameterKey.IsNullOrEmpty())
            {
                string parameterKey = model.ParameterKey;
                return ItilCrudFactory.ItilDevelopModuleChangeRecordCrud.GetChangeRecordBy(parameterKey);
            }
            else return new List<ItilDevelopModuleManageChangeRecordModel>();
        }

        #endregion

        #region Store
        /// <summary>
        /// 添加CRUD方法到方法组
        /// </summary>
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddDevelopModuleManageRecord);
            this.AddOpItem(OpMode.Edit, EditDevelopModuleManageRecord);
            this.AddOpItem(OpMode.UpDate, UpdateDevelopModuleManageRecord);
        }
        /// <summary>
        /// 添加一条开发任务到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult AddDevelopModuleManageRecord(ItilDevelopModuleManageModel model)
        {
            model.ParameterKey = string.Format("{0}&{1}&{2}", model.ModuleName, model.MClassName, model.MFunctionName);
            if (irep.IsExist(m => m.ParameterKey == model.ParameterKey))
            {
                return OpResult.SetErrorResult("此任务已存在！");
            }
            OpResult result;
            model.CurrentProgress = "待开发";
            result = irep.Insert(model).ToOpResult_Add("开发任务", model.Id_Key);

            //保存操作纪录
            if (result.Result)
            {
                OpResult changeRecordResult = ItilCrudFactory.ItilDevelopModuleChangeRecordCrud.SavaChangeRecord(model);
                if (!changeRecordResult.Result)
                {
                    return changeRecordResult;
                }
                else { _waittingSendMailList.Add(model); }//添加至待发送邮件列表
            }
            return result;

        }
        /// <summary>
        /// 编辑一条开发任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult EditDevelopModuleManageRecord(ItilDevelopModuleManageModel model)
        {
            var dbModel = irep.Entities.Where(m => m.Id_Key == model.Id_Key).FirstOrDefault();
            var changeRecordList = GetChangeRecordListBy(dbModel);  //获取待修改的开发任务操作记录

            //修改开发任务
            model.ParameterKey = string.Format("{0}&{1}&{2}", model.ModuleName, model.MClassName, model.MFunctionName);
            model.CurrentProgress = "待开发";
            model.OpSign = OpMode.Edit;
            OpResult result = irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt("开发任务");

            //修改开发任务变更记录
            changeRecordList.ForEach(m =>
            {
                ItilCrudFactory.ItilDevelopModuleChangeRecordCrud.UpdateChangeRecord(model, m.Id_Key);
            });
            return result;
        }
        /// <summary>
        /// 更新开发任务内容
        /// </summary>
        /// <param name="model">开发任务</param>
        /// <returns></returns>
        private OpResult UpdateDevelopModuleManageRecord(ItilDevelopModuleManageModel model)
        {

            //获取待修改的开发任务操作记录
            var changeRecordList = GetChangeRecordListBy(model);

            //修改开发任务
            model.ParameterKey = string.Format("{0}&{1}&{2}", model.ModuleName, model.MClassName, model.MFunctionName);
            model.OpSign = OpMode.UpDate;
            OpResult result = irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt("开发任务");

            //修改开发任务变更记录
            changeRecordList.ForEach(m =>
            {
                ItilCrudFactory.ItilDevelopModuleChangeRecordCrud.UpdateChangeRecord(model, m.Id_Key);
            });
            return result;
        }
        /// <summary>
        /// 修改开发进度
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OpResult ChangeProgressStatus(ItilDevelopModuleManageModel model)
        {
            model.ParameterKey = string.Format("{0}&{1}&{2}", model.ModuleName, model.MClassName, model.MFunctionName);
            if (model.FinishDate != null) { model.FinishMonth = model.FinishDate.Value.ToString("yyyyMM"); }
            var datetime = DateTime.Now;
            var date = datetime.ToDate();

            var result = this.irep.Update(u => u.Id_Key == model.Id_Key, m => new ItilDevelopModuleManageModel()
            {
                CurrentProgress = model.CurrentProgress,
                FinishDate = model.FinishDate,
                FinishMonth = model.FinishMonth,
                Executor = model.Executor,
                OpDate = date,
                OpTime = datetime,
                OpSign = "edit"
            }).ToOpResult_Eidt("开发任务");

            if (!result.Result)
                return result;

            //保存操作纪录
            OpResult changeRecordResult = ItilCrudFactory.ItilDevelopModuleChangeRecordCrud.SavaChangeRecord(model);
            if (!changeRecordResult.Result)
            {
                return changeRecordResult;
            }
            else { _waittingSendMailList.Add(model); }//添加至待发送邮件列表
            return result;
        }
        #endregion


    }


    /***********************************************   模块开发管理CRUD   ****************************
    *                                        
    *  2017-7-27  初版   张明                  
    ***************************************************************************************************/
    /// <summary>
    /// 模块开发管理操作记录CRUD
    /// </summary>
    internal class ItilDevelopModuleChangeRecordCrud : CrudBase<ItilDevelopModuleManageChangeRecordModel, IItilDevelopModuleManageChangeRecordRepository>
    {
        public ItilDevelopModuleChangeRecordCrud() : base(new ItilDevelopModuleManageChangeRecordRepository(), "开发任务变更记录")
        {
        }

        /// <summary>
        /// 添加操作方法
        /// </summary>
        protected override void AddCrudOpItems()
        {
            AddOpItem(OpMode.Add, AddChangeRecord);
            AddOpItem(OpMode.Edit, EditChangeRecord);
        }

        /// <summary>
        /// 根据约束键值查找操作记录
        /// </summary>
        /// <param name="parameterKey"></param>
        /// <returns></returns>
        public List<ItilDevelopModuleManageChangeRecordModel> GetChangeRecordBy(string parameterKey)
        {
            return irep.Entities.Where(m => m.ParameterKey == parameterKey).ToList();
        }
        /// <summary>
        /// 修改数据仓库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override OpResult Store(ItilDevelopModuleManageChangeRecordModel model)
        {
            return this.PersistentDatas(model);
        }
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult AddChangeRecord(ItilDevelopModuleManageChangeRecordModel model)
        {
            return irep.Insert(model).ToOpResult_Add(this.OpContext, model.Id_Key);
        }
        /// <summary>
        /// 修改一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult EditChangeRecord(ItilDevelopModuleManageChangeRecordModel model)
        {
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt(this.OpContext);
        }
        /// <summary>
        /// 保存操作记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult SavaChangeRecord(ItilDevelopModuleManageModel model)
        {
            var changeRecordModel = BuildChangeRecordBy(model);
            if (changeRecordModel == null) { return OpResult.SetErrorResult("开发任务不能为空！"); }
            changeRecordModel.OpSign = OpMode.Add;
            return Store(changeRecordModel);
        }
        /// <summary>
        /// 更新开发任务变更记录
        /// </summary>
        /// <param name="ililModel">开发任务</param>
        /// <param name="changeRecordModel_Id_Key">开发任务记录Id_Key</param>
        /// <returns></returns>
        public OpResult UpdateChangeRecord(ItilDevelopModuleManageModel ililModel, decimal changeRecordModel_Id_Key)
        {
            var changeRecordModel = BuildChangeRecordBy(ililModel);
            if (changeRecordModel == null) { return OpResult.SetErrorResult("开发任务不能为空！"); }
            changeRecordModel.OpSign = OpMode.Edit;
            changeRecordModel.Id_Key = changeRecordModel_Id_Key;
            return Store(changeRecordModel);
        }
        /// <summary>
        /// 生成一条记录
        /// </summary>
        /// <param name="developModuleManageModel">开发任务</param>
        /// <returns></returns>
        private ItilDevelopModuleManageChangeRecordModel BuildChangeRecordBy(ItilDevelopModuleManageModel developModuleManageModel)
        {
            if (developModuleManageModel == null) return null;
            return new ItilDevelopModuleManageChangeRecordModel()
            {
                ModuleName = developModuleManageModel.ModuleName,
                MClassName = developModuleManageModel.MClassName,
                MFunctionName = developModuleManageModel.MFunctionName,
                FunctionDescription = developModuleManageModel.FunctionDescription,
                Executor = developModuleManageModel.Executor,
                ParameterKey = developModuleManageModel.ParameterKey,
                ChangeProgress = developModuleManageModel.CurrentProgress,
            };
        }




    }
}
