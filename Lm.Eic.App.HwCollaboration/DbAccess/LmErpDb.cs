using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.HwCollaboration.Model.LmErp;

namespace Lm.Eic.App.HwCollaboration.DbAccess
{
    /// <summary>
    /// ERP Db access base class
    /// </summary>
    public class ErpDbBase
    {
        /// <summary>
        /// 载入数据列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        protected List<T> LoadDatas<T>(string sqlText) where T : class, new()
        {
            return DbHelper.Erp.LoadEntities<T>(sqlText);
        }
        protected T LoadData<T>(string sqlText) where T : class, new()
        {
            return DbHelper.Erp.LoadEntity<T>(sqlText);
        }
        /// <summary>
        /// 创建选择字段Sql语句
        /// </summary>
        /// <param name="fieldsMap"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        protected string CreateSelectFieldsSql(Dictionary<string, string> fieldsMap, string tableName)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string field in fieldsMap.Keys)
            {
                sb.AppendFormat("{0} as {1},", field, fieldsMap[field]);
            }
            string sql = sb.ToString().TrimEnd(',');
            return string.Format("Select {0}  from  {1} ", sql, tableName);
        }

        protected ErpFieldMapDictionary fieldMapDictionary;
    }

    /// <summary>
    /// ERP表中的字段与模型中的字段映射列表字典
    /// </summary>
    public class ErpFieldMapDictionary
    {
        /// <summary>
        /// 物料库存字段映射  {"MC003","VendorLocation"}
        /// </summary>
        public Dictionary<string, string> MaterialInverntoryFieldMap = new Dictionary<string, string> {
            {"MC001","MaterialId"},{"MC002","VendorStock"},{"MC007","GoodQuantity"},{"MC012","StockTime"}
        };
        /// <summary>
        /// 物料在制字段映射
        /// </summary>
        public Dictionary<string, string> MaterialMakingFieldMap = new Dictionary<string, string> {
            {"TA006","ComponentCode"},{"TA003","OrderPublishDateStr"}, {"rtrim(TA001)+'-'+TA002","OrderNumber"},{"TA015","OrderQuantity"},
            {"TA017","OrderCompletedQuantity"},{"TA011","OrderStatus"}
        };
        /// <summary>
        /// 关键物料BOM字段映射
        /// </summary>
        public Dictionary<string, string> MaterialKeyBomFieldMap = new Dictionary<string, string> {
            {"MD001","VendorItemCode"},{"MD003","SubItemCode"}, {"MD006","StandardQuantity"}
        };
    }

    /// <summary>
    /// 光圣ERP数据访问者
    /// </summary>
    public class LmErpDb : ErpDbBase
    {
        public LmErpDb()
        {
            this.fieldMapDictionary = new ErpFieldMapDictionary();
        }

        /// <summary>
        /// 载入物料库存数据
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<ErpMaterialInventoryModel> LoadMeterialInventoryDatas(string materialId)
        {
            StringBuilder sqlText = new StringBuilder();
            sqlText.Append(CreateSelectFieldsSql(this.fieldMapDictionary.MaterialInverntoryFieldMap, "INVMC"))
                .AppendFormat("where MC001='{0}' And MC002 in ('A01','A02','A04','B01','B02','B04','C01')", materialId);
            return this.LoadDatas<ErpMaterialInventoryModel>(sqlText.ToString());
        }

        /// <summary>
        /// 载入在制品数据
        /// </summary>
        /// <param name="productId">产品品号</param>
        /// <returns></returns>
        public List<ErpMaterialMakingModel> LoadMaterialMakingDatas(string productId)
        {
            StringBuilder sqlText = new StringBuilder();
            sqlText.Append(CreateSelectFieldsSql(this.fieldMapDictionary.MaterialMakingFieldMap, "MOCTA"))
                //工单单别：512代表制二课；515代表制五课，516代表制七课；517代表生技课  状态码：1.未生产,2.已发料,3.生产中,Y.已完工,y.指定完工
                .AppendFormat("where TA006='{0}' AND TA011 in ('1','2','3') AND TA001 in ('512','515','516','517')", productId);
            return this.LoadDatas<ErpMaterialMakingModel>(sqlText.ToString());
        }

        /// <summary>
        /// 载入在制品数据
        /// </summary>
        /// <param name="productId">产品品号</param>
        /// <returns></returns>
        public List<ErpMaterialShipmentModel> LoadMaterialShipmentDatas(string productId)
        {
            StringBuilder sqlText = new StringBuilder();
            sqlText.Append(CreateSelectFieldsSql(this.fieldMapDictionary.MaterialMakingFieldMap, "MOCTA"))
                //工单单别：512代表制二课；515代表制五课，516代表制七课；517代表生技课  状态码：1.未生产,2.已发料,3.生产中,Y.已完工,y.指定完工
                .AppendFormat("where TA006='{0}' AND TA011 in ('1','2','3') AND TA001 in ('512','515','516','517')", productId);
            return this.LoadDatas<ErpMaterialShipmentModel>(sqlText.ToString());
        }

        /// <summary>
        /// 根据产品品号和物料料号载入关键物料BOM信息
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public ErpMaterialKeyBomModel LoadKeyMaterialBomData(string productId, string materialId)
        {
            ErpMaterialKeyBomModel bom = null;
            StringBuilder sqlText = new StringBuilder();
            sqlText.Append(CreateSelectFieldsSql(this.fieldMapDictionary.MaterialKeyBomFieldMap, "BOMMD"))
                .AppendFormat("where MD001='{0}' AND MD003='{1}'", productId, materialId);
            bom = this.LoadData<ErpMaterialKeyBomModel>(sqlText.ToString());
            if (bom != null)
            {
                BomCell masterBom = LoadMasterBom(bom.VendorItemCode);
                if (masterBom != null)
                    bom.BaseUsedQuantity = masterBom.Count;
            }
            return bom;
        }

        //public int LoadBomUsage(string materialId)
        //{

        //}
        public BomCell LoadMasterBom(string productId)
        {
            string sql = string.Format("Select MC001 as ProductId,MC004 as Count from BOMMC where MC001='{0}'", productId);
            return this.LoadData<BomCell>(sql);
        }
    }





    #region transmit dto
    public class BomCell
    {
        public string ProductId { get; set; }
        /// <summary>
        /// 标准批量
        /// </summary>
        public double Count { get; set; }
    }
    #endregion
}
