using Lm.Eic.Framework.ProductMaster.DbAccess.Repository;
using Lm.Eic.Framework.ProductMaster.Model.MessageNotify;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Business.MessageNotify
{
    internal class ConfigNotifyAddressCrudFactory
    {
        /// <summary>
        /// 配置消息通知地址CRUD
        /// </summary>
        internal static ConfigNotifyAddressCrud ConfigNotifyAddressCrud
        {
            get { return OBulider.BuildInstance<ConfigNotifyAddressCrud>(); }
        }
    }
    /// <summary>
    /// 模块配置消息通知地址CRUD
    /// </summary>
    internal class ConfigNotifyAddressCrud : CrudBase<ConfigNotifyAddressModel, IConfigNotifyAddressRepository>
    {
        public ConfigNotifyAddressCrud() : base(new ConfigNotifyAddressRepository(), "配置消息通知地址")
        { }

        /// <summary>
        /// 添加操作方法
        /// </summary>
        protected override void AddCrudOpItems()
        {
            AddOpItem(OpMode.Add, Add);
            AddOpItem(OpMode.Edit, Edit);
            AddOpItem(OpMode.Delete, DeleteData);
        }
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Add(ConfigNotifyAddressModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult Edit(ConfigNotifyAddressModel model)
        {
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult DeleteData(ConfigNotifyAddressModel model)
        {
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        /// <summary>
        /// 根据约束键值查找
        /// </summary>
        /// <param name="parameterKey"></param>
        /// <returns></returns>
        internal List<ConfigNotifyAddressModel> GetConfigNotifyAddressBy(string parameterKey)
        {
            return irep.Entities.Where(m => m.ParameterKey == parameterKey).ToList();
        }

    }
}
