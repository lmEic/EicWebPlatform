using Lm.Eic.Framework.ProductMaster.DbAccess.Repository;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeExtension.Validation;

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
        /// 构造函数 初始化数据访问接口
        /// </summary>
        public ItilDevelopModuleManageCrud() : base(new ItilDevelopModuleManageRepository())
        {

        }

        /// <summary>
        /// 获取开发任务列表
        /// </summary>
        /// <param name="progressSignList">状态列表</param>
        /// <returns></returns>
        public List<ItilDevelopModuleManageModel> GetDevelopModuleManageListBy(List<string> progressSignList)
        {
            if (progressSignList != null)
                return irep.Entities.Where(m => m.CurrentProgress != "结案" && progressSignList.Contains(m.CurrentProgress)).ToList();
            else
                return new List<ItilDevelopModuleManageModel>();
        }

        /// <summary>
        /// 修改数据仓库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(ItilDevelopModuleManageModel model)
        {

            //TODO: 修改一下  修改为 先存储开发管理记录 然后存储开发任务，如果开发任务存储失败 Delete 开发管理记录
            return this.StoreEntity(model, mdl =>
            {
                model.ParameterKey = string.Format("{0}&{1}&{2}", model.ModuleName, model.MClassName, model.MFunctionName);
                var result = this.PersistentDatas(model,
                madd =>
                {
                    return AddDevelopModuleManageRecord(model);
                },
                mupdate =>
                {
                    return EditDevelopModuleManageRecord(model);
                });

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
            });
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

        /// <summary>
        /// 发送邮件通知
        /// </summary>
        /// <returns></returns>
        public OpResult SendMail()
        {
            //TODO：根据 _waitingSendMailList 发送邮件进行通知
            return null;
        }

        /// <summary>
        /// 添加一条开发任务到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult AddDevelopModuleManageRecord(ItilDevelopModuleManageModel model)
        {
            if (irep.IsExist(m => m.ParameterKey == model.ParameterKey))
            {
                return OpResult.SetResult("此任务已存在！");
            }
            OpResult result;
            model.CurrentProgress = "待开发";
            result = irep.Insert(model).ToOpResult_Add("开发任务", model.Id_Key);
            return result;
        }

        /// <summary>
        /// 更新一条开发任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult EditDevelopModuleManageRecord(ItilDevelopModuleManageModel model)
        {
            model.CurrentProgress = "待开发";
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt("开发任务");
        }
    }


    /***********************************************   模块开发管理CRUD   ****************************
    *                                        
    *  2017-7-27  初版   张明                  
    ***************************************************************************************************/
    /// <summary>
    /// 模块开发管理操作记录CRUD
    /// </summary>
    internal class ItilDevelopModuleChangeRecordCrud
    {
        private IItilDevelopModuleManageChangeRecordRepository irep = null;

        public ItilDevelopModuleChangeRecordCrud()
        {
            irep = new ItilDevelopModuleManageChangeRecordRepository();
        }

        /// <summary>
        /// 修改数据仓库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(ItilDevelopModuleManageChangeRecordModel model)
        {
            OpResult result = OpResult.SetResult("未执行任何操作！");
            if (model == null) return result;
            DateTime dateTime = DateTime.Now;
            model.OpTime = dateTime;
            model.OpDate = dateTime.ToDate();
            try
            {
                switch (model.OpSign)
                {
                    case OpMode.Add: //新增
                        result = irep.Insert(model).ToOpResult_Add("开发管控修改记录", model.Id_Key);
                        break;

                    default:
                        result = OpResult.SetResult("操作模式溢出");
                        break;
                }
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
            return result;
        }

        /// <summary>
        /// 保存操作记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult SavaChangeRecord(ItilDevelopModuleManageModel model)
        {
            return ItilCrudFactory.ItilDevelopModuleChangeRecordCrud.Store(new ItilDevelopModuleManageChangeRecordModel()
            {
                ModuleName = model.ModuleName,
                MClassName = model.MClassName,
                MFunctionName = model.MFunctionName,
                FunctionDescription = model.MFunctionName,
                ParameterKey = model.ParameterKey,
                ChangeProgress = model.CurrentProgress,
                OpSign = "add"
            });
        }

        /// <summary>
        /// 根据约束键值查找操作记录
        /// </summary>
        /// <param name="parameterKey"></param>
        /// <returns></returns>
        public List<ItilDevelopModuleManageChangeRecordModel> FindBy(string parameterKey)
        {
            return irep.Entities.Where(m => m.ParameterKey == parameterKey).ToList();
        }
    }
}
