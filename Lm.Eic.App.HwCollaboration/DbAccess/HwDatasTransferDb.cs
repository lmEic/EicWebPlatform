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
        private string selectFileds = "OpModule, OpContent,OpLog, OpDate, OpTime, OpPerson, OpSign from HwCollaboration_DataTransfer";


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
            sbSql.Append("select Top 1 " + selectFileds)
                .AppendFormat(" where OpModule='{0}' order by Id_Key Desc", moduleName);
            return DbHelper.Bpm.LoadEntity<HwCollaborationDataTransferModel>(sbSql.ToString());
        }
        /// <summary>
        /// 载入数据模型集合
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public List<HwCollaborationDataTransferModel> GetDataModels(string moduleName)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("select " + selectFileds)
                .AppendFormat(" where OpModule='{0}' order by Id_Key Desc", moduleName);
            return DbHelper.Bpm.LoadEntities<HwCollaborationDataTransferModel>(sbSql.ToString());
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
    /// <summary>
    /// 华为系统配置数据助手
    /// </summary>
    public class HwDatasConfigDb
    {
        private string selectFileds = "MaterialId, MaterialBaseDataContent, MaterialBomDataContent, OpLog, OpSign, OpPerson, OpDate, OpTime from HwCollaboration_DataConfig";

        public OpResult Store(HwCollaborationDataConfigModel model)
        {
            if (model == null)
            {
                return OpResult.SetErrorResult("配置信息不能为NULL!");
            }
            if (!IsExist(model.MaterialId))
                return Insert(model);
            else
                return Update(model);
        }

        private OpResult GetOpResult(int record)
        {
            if (record > 0)
                return OpResult.SetSuccessResult("向华为协同平台发送配置数据成功！");
            else
                return OpResult.SetErrorResult("向华为协同平台发送配置数据失败!");
        }

        private OpResult Insert(HwCollaborationDataConfigModel entity)
        {
            StringBuilder sqlSb = new StringBuilder();
            sqlSb.Append("Insert into HwCollaboration_DataConfig(MaterialId, MaterialBaseDataContent, MaterialBomDataContent, OpLog, OpSign, OpPerson, OpDate, OpTime)")
                 .AppendFormat(" values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", entity.MaterialId, entity.MaterialBaseDataContent, entity.MaterialBomDataContent, entity.OpLog, entity.OpSign, entity.OpPerson, entity.OpDate, entity.OpTime);
            int record = DbHelper.Bpm.ExecuteNonQuery(sqlSb.ToString());
            return GetOpResult(record);
        }
        private OpResult Update(HwCollaborationDataConfigModel entity)
        {
            StringBuilder sqlSb = new StringBuilder();
            sqlSb.Append("Update HwCollaboration_DataConfig ")
                .AppendFormat("Set MaterialBaseDataContent='{0}',", entity.MaterialBaseDataContent)
                 .AppendFormat("MaterialBomDataContent='{0}',", entity.MaterialBomDataContent)
                .AppendFormat("OpLog='{0}',", entity.OpLog)
                .AppendFormat("OpSign='{0}',", entity.OpSign)
                .AppendFormat("OpPerson='{0}',", entity.OpPerson)
                .AppendFormat("OpDate='{0}',", entity.OpDate)
                .AppendFormat("OpTime='{0}' ", entity.OpTime)
                .AppendFormat("where MaterialId='{0}'", entity.MaterialId);
            int record = DbHelper.Bpm.ExecuteNonQuery(sqlSb.ToString());
            return GetOpResult(record);
        }
        /// <summary>
        /// 获取配置数据模型
        /// </summary>
        /// <param name="materilId"></param>
        /// <returns></returns>
        public HwCollaborationDataConfigModel GetDataBy(string materilId)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("select Top 1 " + selectFileds)
                .AppendFormat(" where MaterialId='{0}' order by Id_Key Desc", materilId);
            return DbHelper.Bpm.LoadEntity<HwCollaborationDataConfigModel>(sbSql.ToString());
        }
        private bool IsExist(string materialId)
        {
            return DbHelper.Bpm.IsExist(string.Format("select MaterialId from HwCollaboration_DataConfig where MaterialId='{0}'", materialId));
        }
    }
}
