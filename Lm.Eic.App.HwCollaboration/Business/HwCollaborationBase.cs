using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HwRestfulApi;
using Lm.Eic.App.HwCollaboration.Model;
using Lm.Eic.App.HwCollaboration.DbAccess;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeOOMapper;

namespace Lm.Eic.App.HwCollaboration.Business
{
    /// <summary>
    /// 华为协同业务基类
    /// </summary>
    public abstract class HwCollaborationBase<T> where T : HwDataTransferDtoBase, new()
    {
        #region property
        private HwRestfulApiManager helper
        {
            get
            {
                string url = "https://api-beta.huawei.com:443/oauth2/token";
                string key = "e24YjcnCCEW1TVG_oEKpxaQXWPca";
                string secury = "1fDV5DZWcpGh0MtjkuPH3YsYODIa";
                return new HwRestfulApiManager(key, secury, url);
            }
        }
        /// <summary>
        /// 数据访问助手
        /// </summary>
        protected HwDatasTransferDb dbAccess = null;
        #endregion

        public HwCollaborationBase()
        {
            dbAccess = new HwDatasTransferDb();
        }


        #region method
        /// <summary>
        /// 访问华为Api
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUrl"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        protected string AccessApi(string apiUrl, T datas)
        {
            return helper.AccessHwAPI<T>(apiUrl, datas);
        }
        /// <summary>
        /// 通过访问华为API将数据同步到华为系统中
        /// </summary>
        /// <param name="accessApiUrl"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected OpResult SynchronizeDatas(string accessApiUrl, HwDataEntity entity)
        {
            if (entity == null || entity.Dto == null || entity.OpLog == null)
            {
                return OpResult.SetErrorResult("数据实体模型不能为null！");
            }
            var dto = entity.Dto as T;
            try
            {
                string returnMsg = this.AccessApi(accessApiUrl, dto);
                HwAccessApiResult result = ObjectSerializer.DeserializeObject<HwAccessApiResult>(returnMsg);
                if (result == null || !result.success)
                {
                    return OpResult.SetErrorResult("本次操作失败！失败原因：" + returnMsg);
                }
                var data = CreateOperateInstance(entity);
                return this.dbAccess.Store(data);
            }
            catch (System.Exception ex)
            {
                return ex.ExOpResult();
            }
        }
        /// <summary>
        /// 通过访问华为API将数据同步到华为系统中
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract OpResult SynchronizeDatas(HwDataEntity entity);

        /// <summary>
        /// 获取最新的数据实体模型
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        protected HwDataEntity GetLatestEntity(string moduleName)
        {
            HwDataEntity entity = null;
            var data = dbAccess.GetLatestDataModel(moduleName);
            if (data != null)
            {
                entity.Dto = ObjectSerializer.DeserializeObject<T>(data.OpContent) as T;
                entity.OpLog = ObjectSerializer.DeserializeObject<HwDataTransferLog>(data.OpLog);
            }
            return entity;
        }
        /// <summary>
        /// 获取最新的数据实体模型
        /// </summary>
        /// <returns></returns>
        public abstract HwDataEntity GetLatestEntity();

        protected HwCollaborationDataTransferModel CreateOperateInstance(HwDataEntity entity)
        {
            return new HwCollaborationDataTransferModel
            {
                OpModule = entity.OpLog.OpModule,
                OpDate = DateTime.Now.ToDate(),
                OpTime = DateTime.Now.ToDateTime(),
                OpSign = entity.OpLog.OpSign,
                OpPerson = entity.OpLog.OpPerson,
                OpContent = ObjectSerializer.SerializeObject(entity.Dto),
                OpLog = ObjectSerializer.SerializeObject(entity.OpLog)
            };
        }
        #endregion
    }

    /// <summary>
    /// 华为API调用地址库
    /// </summary>
    internal class HwAccessApiUrl
    {
        /// <summary>
        /// 人力
        /// </summary>
        public const string ManPowerApiUrl = "https://api-beta.huawei.com:443/service/esupplier/importManpower/1.0.0";
        /// <summary>
        /// 库存明细
        /// </summary>
        public const string FactoryInventoryApiUrl = "https://api-beta.huawei.com:443/service/esupplier/importCapacity/1.0.0";
        /// <summary>
        /// 在制明细
        /// </summary>
        public const string MaterialMakingApiUrl = "https://api-beta.huawei.com:443/service/esupplier/importMaterialMaking/1.0.0";
        /// <summary>
        /// 发料明细
        /// </summary>
        public const string MaterialShipmentApiUrl = "https://api-beta.huawei.com:443/service/esupplier/importMaterialShipment/1.0.0";
        /// <summary>
        /// 在途明细
        /// </summary>
        public const string PurchaseOnWayApiUrl = "https://api-beta.huawei.com:443/service/esupplier/importMaterialShipment/1.0.0";
    }

    internal class HwModuleName
    {
        public const string ManPower = "人力信息";

        public const string MaterialInventory = "物料库存明细";

        public const string MaterialMaking = "物料在制明细";

        public const string MaterialShipment = "物料发料信息";

        //public const string MaterialInventory = "物料库存明细";

        //public const string MaterialMaking = "物料在制明细";
    }
}
