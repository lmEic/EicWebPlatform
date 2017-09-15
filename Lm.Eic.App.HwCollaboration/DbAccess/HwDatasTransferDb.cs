using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.App.HwCollaboration.Model;
using Lm.Eic.Uti.Common.YleeDbHandler;

namespace Lm.Eic.App.HwCollaboration.DbAccess
{
    /// <summary>
    /// 华为数据传输数据操作助手
    /// </summary>
    public class HwDatasTransferDb
    {
        public OpResult Store(HwCollaborationDataTransferModel model)
        {
            if (model == null)
            {
                return OpResult.SetErrorResult("操作信息不能为NULL!");
            }
            return Insert(model);
        }
        /// <summary>
        /// 获取给定模块最新的数据
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public HwCollaborationDataTransferModel GetLatestDataModel(string moduleName)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("select Top 1 OpModule, OpContent,OpLog, OpDate, OpTime, OpPerson, OpSign from HwCollaboration_DataTransfer")
                .AppendFormat(" where OpModule='{0}'", moduleName);
            return DbHelper.Bpm.LoadEntity<HwCollaborationDataTransferModel>(sbSql.ToString());
        }
        private OpResult Insert(HwCollaborationDataTransferModel entity)
        {
            StringBuilder sqlSb = new StringBuilder();
            sqlSb.Append("Insert into HwCollaboration_DataTransfer(OpModule, OpContent,OpLog,OpDate, OpTime, OpPerson, OpSign)")
                 .AppendFormat("values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", entity.OpModule, entity.OpContent, entity.OpLog, entity.OpDate, entity.OpTime, entity.OpPerson, entity.OpSign);
            int record = DbHelper.Bpm.ExecuteNonQuery(sqlSb.ToString());
            if (record > 0)
                return OpResult.SetSuccessResult("向华为协同平台发送数据成功！");
            else
                return OpResult.SetErrorResult("向华为协同平台发送数据失败!");
        }
    }
}
