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
    public static class HwDbExtension
    {
        public static OpResult ToOpResult(this int record)
        {
            if (record > 0)
                return OpResult.SetSuccessResult("向华为协同平台发送配置数据成功！");
            else
                return OpResult.SetErrorResult("向华为协同平台发送配置数据失败!");
        }
    }
    public abstract class HwDbBase
    {
        protected string _tableName;
        public HwDbBase(string tableName)
        {
            this._tableName = tableName;
        }

        protected OpResult Insert<TEntity>(TEntity entity)
        {
            return DbHelper.Bpm.Insert(entity, this._tableName).ToOpResult();
        }

        protected OpResult UpDate<TEntity>(TEntity entity)
        {
            return DbHelper.Bpm.Update(entity, this._tableName).ToOpResult();
        }
    }

    /// <summary>
    /// 华为数据传输数据操作助手
    /// </summary>
    /// 
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

    public class HwMaterialBaseConfigDb : HwDbBase
    {
        public HwMaterialBaseConfigDb() : base("HwCollaboration_MaterialBaseConfig")
        {
        }
        /// <summary>
        /// 存储数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public OpResult Store(HwCollaborationMaterialBaseConfigModel entity)
        {
            var existEntity = GetSingle(entity);
            if (entity.OpSign != OpMode.Delete)
            {
                if (existEntity == null)
                    return this.Insert(entity);
                else
                {
                    entity.Id_Key = existEntity.Id_Key;
                    return this.UpDate(entity);
                }
            }
            else
            {
                return Delete(entity);
            }
        }

        private string GetSelectFields()
        {
            StringBuilder s = new StringBuilder();
            s.Append("MaterialId, MaterialName, ParentMaterialId, DisplayOrder, VendorProductModel,")
             .Append("VendorItemDesc, ItemCategory,CustomerVendorCode, CustomerItemCode, CustomerProductModel,")
             .Append("UnitOfMeasure, InventoryType, GoodPercent, LeadTime, LifeCycleStatus, Quantity,")
             .Append("SubstituteGroup, OpSign, OpPerson, OpDate, OpTime,Id_Key");
            return s.ToString();
        }

        private HwCollaborationMaterialBaseConfigModel GetSingle(HwCollaborationMaterialBaseConfigModel entity)
        {
            string sql = string.Format("Select {0} from {1} where MaterialId='{2}' And ParentMaterialId='{3}'", GetSelectFields(), this._tableName, entity.MaterialId, entity.ParentMaterialId);
            return DbHelper.Bpm.LoadEntity<HwCollaborationMaterialBaseConfigModel>(sql);
        }

        public List<HwCollaborationMaterialBaseConfigModel> GetAll()
        {
            string sqlSelect = string.Format("Select {0} from {1}", GetSelectFields(), this._tableName);
            return DbHelper.Bpm.LoadEntities<HwCollaborationMaterialBaseConfigModel>(sqlSelect);
        }

        private OpResult Delete(HwCollaborationMaterialBaseConfigModel entity)
        {
            string sqlDelete = string.Format("Delete from  {0} Where MaterialId='{1}' And ParentMaterialId='{2}'", this._tableName, entity.MaterialId, entity.ParentMaterialId);
            return DbHelper.Bpm.ExecuteNonQuery(sqlDelete).ToOpResult();
        }
    }
}
