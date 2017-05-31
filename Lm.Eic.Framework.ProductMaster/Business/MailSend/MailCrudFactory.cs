using Lm.Eic.Framework.ProductMaster.DbAccess.Repository;
using Lm.Eic.Framework.ProductMaster.Model.EmailConfigInfo;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Business.MailSend
{
  internal   class MailCrudFactory
    {
        public static EmialConfigCrud EmialConfigCrud
        {
            get { return OBulider.BuildInstance<EmialConfigCrud>(); }
        }
    }
    internal class EmialConfigCrud : CrudBase<Config_MailInfoModel, IConfig_MailInfoRepository>
   {
        public EmialConfigCrud() : base(new Config_MailInfoRepository(),"邮箱发送服务配置")
        {
        }
        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 邮件发送配置数据
        /// </summary>
        /// <returns></returns>
       internal List<Config_MailInfoModel> GetMailSenderInfoDatas()
        {
            return irep.Entities.Where(e => e.IsSender == 1).ToList();
        }
        /// <summary>
        /// 发送人列表
        /// </summary>
        /// <returns></returns>
        internal List<Config_MailInfoModel> GetResiveMailInfoDatas()
        {
            return irep.Entities.Where(e => e.IsSender == 0).ToList();
        }
    }
}
