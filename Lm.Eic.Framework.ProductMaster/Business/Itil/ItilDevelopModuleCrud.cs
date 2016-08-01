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
    internal class ItilDevelopModuleManageCrudFactory
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
   public class ItilDevelopModuleManageCrud:CrudBase<ItilDevelopModuleManageModel,IItilDevelopModuleManageRepository>
    {
       

        private List<ItilDevelopModuleManageModel> _waitingSendMailList = new List<ItilDevelopModuleManageModel>();

        public ItilDevelopModuleManageCrud()
        {
           
        }
      
        /// <summary>
        /// 获取开发任务列表
        /// </summary>
        /// <param name="progressSignList">状态列表</param>
        /// <returns></returns>
        public List<ItilDevelopModuleManageModel> GetDevelopModuleManageListBy(List<string> progressSignList)
        {
            return irep.Entities.Where(m => m.CurrentProgress != "结案" && progressSignList.Contains(m.CurrentProgress)).ToList();
        }

        /// <summary>
        /// 修改数据仓库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public  OpResult Store(ItilDevelopModuleManageModel model)
        {
           return this.StoreEntity(model, mdl => {
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
                    OpResult changeRecordResult = SavaChangeRecord(model);
                    if (!changeRecordResult.Result)
                    {
                        return changeRecordResult;
                    }
                    else { _waitingSendMailList.Add(model); }//添加至待发送邮件列表
                }
                return result;
            });
        }

        /// <summary>
        /// 发送邮件通知
        /// </summary>
        /// <returns></returns>
        public OpResult SendMail()
        {
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
                return OpResult.SetResult("此任务已存在！", false);
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
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt("开发任务");
        }

        /// <summary>
        /// 保存操作记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult SavaChangeRecord(ItilDevelopModuleManageModel model)
        {
            return ItilDevelopModuleManageCrudFactory.ItilDevelopModuleChangeRecordCrud.Store(new ItilDevelopModuleManageChangeRecordModel()
            {
                ParameterKey = model.ParameterKey,
                ChangeProgress = model.CurrentProgress,
                 OpSign="add"
            });
        }

       protected override void InitIrep()
        {
            this.irep = new ItilDevelopModuleManageRepository();
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
            OpResult result = OpResult.SetResult("未执行任何操作！", false);
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
                        result = OpResult.SetResult("操作模式溢出", false);
                        break;
                }
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
            return result;
        }

        /// <summary>
        /// 根据约束键值查找操作记录
        /// </summary>
        /// <param name="parameterKey"></param>
        /// <returns></returns>
        public List<ItilDevelopModuleManageChangeRecordModel> FindBy(string parameterKey)
        {
            return irep.FindAll<ItilDevelopModuleManageChangeRecordModel>(m => m.ParameterKey == parameterKey).ToList();
        }
    }
}
