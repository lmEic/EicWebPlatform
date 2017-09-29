using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.HwCollaboration.Model;
using Lm.Eic.App.HwCollaboration.DbAccess;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.App.HwCollaboration.Business;

namespace Lm.Eic.App.HwCollaboration.Business.MaterialManage
{
    /// <summary>
    /// 物料配置基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class HwCollaborationMaterialConfigBase<T> : HwCollaborationBase<T> where T : class, new()
    {
        /// <summary>
        /// 配置数据访问助手
        /// </summary>
        protected HwDatasConfigDb configDbAccess = null;

        public HwCollaborationMaterialConfigBase(string modulename, string apiUrl) : base(modulename, apiUrl)
        {
            this.configDbAccess = new HwDatasConfigDb();
        }

        #region Material Config handle method
        /// <summary>
        /// 根据物料料号获取配置数据新
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public HwCollaborationDataConfigModel GetConfigData(string materialId)
        {
            HwCollaborationDataConfigModel data = this.configDbAccess.GetDataBy(materialId);
            if (data != null && data.OpLog != null)
            {
                HwDataTransferLog log = ObjectSerializer.DeserializeObject<HwDataTransferLog>(data.OpLog);
                if (log != null && log.DataStatus == 1)
                    return data;
            }
            return null;
        }
        protected HwCollaborationDataConfigModel CreateOperateInstance(HwCollaborationDataConfigModel entity)
        {
            //操作日志
            HwDataTransferLog opLog = new HwDataTransferLog()
            {
                OpModule = this.moduleName,
                OpSign = entity.OpSign,
                OpPerson = entity.OpPerson,
                DataStatus = entity.OpSign == OpMode.Delete ? 0 : 1
            };
            T dto = new T();
            bool isMaterialBase = true;
            if (entity.MaterialBaseDataContent != null && entity.MaterialBaseDataContent.Length > 30)
            {
                isMaterialBase = true;
                dto = ObjectSerializer.DeserializeObject<T>(entity.MaterialBaseDataContent);
            }
            else
            {
                isMaterialBase = false;
                if (entity.MaterialBomDataContent != null && entity.MaterialBomDataContent.Length > 20)
                    dto = ObjectSerializer.DeserializeObject<T>(entity.MaterialBomDataContent);
            }
            return new HwCollaborationDataConfigModel
            {
                MaterialId = entity.MaterialId,
                MaterialBaseDataContent = isMaterialBase ? ObjectSerializer.SerializeObject(dto) : "",
                MaterialBomDataContent = isMaterialBase ? "" : ObjectSerializer.SerializeObject(dto),
                OpDate = DateTime.Now.ToDate(),
                OpTime = DateTime.Now.ToDateTime(),
                OpSign = entity.OpSign,
                OpPerson = entity.OpPerson,
                InventoryType = entity.InventoryType,
                DataStatus = opLog.DataStatus,
                OpLog = ObjectSerializer.SerializeObject(opLog)
            };
        }
        /// <summary>
        /// 存储数据实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtoContent"></param>
        /// <param name="storeHandler"></param>
        /// <returns></returns>
        protected OpResult StoreEntity(HwCollaborationDataConfigModel entity, string dtoContent, Func<HwCollaborationDataConfigModel, OpResult> storeHandler)
        {
            var dto = ObjectSerializer.DeserializeObject<T>(dtoContent);
            try
            {
                //1.向华为平台发送数据
                string returnMsg = this.AccessApi(this.apiUrl, dto);
                HwAccessApiResult apiResult = ObjectSerializer.DeserializeObject<HwAccessApiResult>(returnMsg);
                OpResult opResult = OpResult.SetSuccessResult("初始状态！");
                if (apiResult == null || !apiResult.success)
                {
                    opResult = OpResult.SetErrorResult("向华为平台发送数据失败！失败原因：" + returnMsg);
                }
                if (!opResult.Result) return opResult;
                //2.存储到本服务器中
                var model = CreateOperateInstance(entity);
                return storeHandler(model);
            }
            catch (System.Exception ex)
            {
                return ex.ExOpResult();
            }
        }
        #endregion
    }
    /// <summary>
    /// 物料控制基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class HwCollaborationMaterialBase<T> : HwCollaborationMaterialConfigBase<T> where T : class, new()
    {
        /// <summary>
        /// ERP数据访问助手
        /// </summary>
        protected LmErpDb erpDbAccess = null;

        public HwCollaborationMaterialBase(string modulename, string apiUrl) : base(modulename, apiUrl)
        {
            erpDbAccess = new LmErpDb();
        }
        /// <summary>
        /// 自动从ERP中获取数据
        /// </summary>
        /// <returns></returns>
        public virtual T AutoGetDatasFromErp(ErpMaterialQueryCell materialQueryCell)
        {
            return default(T);
        }
        /// <summary>
        /// 获取所有的BOM配置数据
        /// </summary>
        /// <returns></returns>
        public List<SccKeyMaterialVO> GetAllBomConfigDatas()
        {
            List<SccKeyMaterialVO> rtnDatas = new List<Model.SccKeyMaterialVO>();
            List<HwCollaborationDataConfigModel> configDatas = this.configDbAccess.GetConfigDatas(1);
            if (configDatas != null && configDatas.Count > 0)
            {
                configDatas.ForEach(m =>
                {
                    KeyMaterialDto dto = ObjectSerializer.DeserializeObject<KeyMaterialDto>(m.MaterialBomDataContent);
                    if (dto != null && dto.keyMaterialList != null)
                    {
                        rtnDatas.AddRange(dto.keyMaterialList);
                    }
                });
            }
            return rtnDatas;
        }
    }

    /// <summary>
    /// 华为物料配置数据字典
    /// </summary>
    public class HwMaterialConfigDataDic
    {
        /// <summary>
        /// 成品
        /// </summary>
        public const string FG = "FG";
        /// <summary>
        /// 半成品
        /// </summary>
        public const string SEMI_FG = "SEMI-FG";
        /// <summary>
        /// 原材料
        /// </summary>
        public const string RM = "RM";
    }
}
