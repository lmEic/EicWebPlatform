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
    /// 华为协同基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class HwCollaborationBase<T> where T : class, new()
    {
        #region property
        private HwRestfulApiManager helper
        {
            get
            {
                string url = "https://openapi.huawei.com:443/oauth2/token";
                string key = "E0v2QffhIb2FKS1QpfHOiWffh_wa";
                string secury = "ZNpu5EZfuxfsB6L10f4sL8cstOAa";
                return new HwRestfulApiManager(key, secury, url);
            }
        }
        protected string moduleName = null;
        protected string apiUrl = null;
        /// <summary>
        /// 是否是测试环境，如果是测试环境
        /// 则不像华为平台发送数据
        /// </summary>
        protected bool isTestMode = false;
        #endregion

        public HwCollaborationBase(string modulename, string apiUrl)
        {
            this.moduleName = modulename;
            this.apiUrl = apiUrl;
        }
        /// <summary>
        /// 访问华为Api
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUrl"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        protected string AccessApi(string apiUrl, T datas)
        {
            if (!isTestMode)
                return helper.AccessHwAPI<T>(apiUrl, datas);
            else
                return ObjectSerializer.SerializeObject(new HwAccessApiResult() { errorCode = "", errorMessage = "", success = true });
        }
    }
    /// <summary>
    /// 华为协同业务数据操作基类
    /// </summary>
    public abstract class HwCollaborationDataBase<T> : HwCollaborationBase<T> where T : class, new()
    {
        #region property
        /// <summary>
        /// 数据访问助手
        /// </summary>
        protected HwDatasTransferDb dbAccess = null;
        #endregion

        public HwCollaborationDataBase(string modulename, string apiUrl) : base(modulename, apiUrl)
        {
            dbAccess = new HwDatasTransferDb();
        }

        #region method
        /// <summary>
        /// 通过访问华为API将数据同步到华为系统中
        /// </summary>
        /// <param name="accessApiUrl"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected OpResult SynchronizeDatas(string accessApiUrl, HwCollaborationDataTransferModel entity, Func<HwCollaborationDataTransferModel, OpResult> storeHandler = null)
        {
            if (entity == null)
            {
                return OpResult.SetErrorResult("数据实体模型不能为null！");
            }
            var dto = ObjectSerializer.DeserializeObject<T>(entity.OpContent);

            try
            {
                string returnMsg = this.AccessApi(accessApiUrl, dto);
                HwAccessApiResult result = ObjectSerializer.DeserializeObject<HwAccessApiResult>(returnMsg);
                if (result == null || !result.success)
                {
                    return OpResult.SetErrorResult("本次操作失败！失败原因：" + returnMsg);
                }
                if (storeHandler == null)
                {
                    var dataEntity = CreateOperateInstance(entity);
                    return this.dbAccess.Store(dataEntity);
                }
                else
                {
                    return storeHandler(entity);
                }
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
        public virtual OpResult SynchronizeDatas(HwCollaborationDataTransferModel entity)
        {
            return this.SynchronizeDatas(this.apiUrl, entity);
        }
        /// <summary>
        /// 获取最新的数据实体模型
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        protected HwCollaborationDataTransferModel GetLatestEntity(string moduleName)
        {
            return dbAccess.GetLatestDataModel(moduleName);
        }
        /// <summary>
        /// 获取最新的数据实体模型
        /// </summary>
        /// <returns></returns>
        public virtual HwCollaborationDataTransferModel GetLatestEntity()
        {
            return this.GetLatestEntity(moduleName);
        }

        protected HwCollaborationDataTransferModel CreateOperateInstance(HwCollaborationDataTransferModel entity)
        {
            //操作日志
            HwDataTransferLog opLog = new HwDataTransferLog()
            {
                OpModule = this.moduleName,
                OpSign = entity.OpSign,
                OpPerson = entity.OpPerson
            };
            T dto = ObjectSerializer.DeserializeObject<T>(entity.OpContent);
            return new HwCollaborationDataTransferModel
            {
                OpModule = this.moduleName,
                OpDate = DateTime.Now.ToDate(),
                OpTime = DateTime.Now.ToDateTime(),
                OpSign = entity.OpSign,
                OpPerson = entity.OpPerson,
                OpContent = ObjectSerializer.SerializeObject(dto),
                OpLog = ObjectSerializer.SerializeObject(opLog)
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
        /// 物料基础信息
        /// </summary>
        public const string MaterialBaseInfoApiUrl = "https://openapi.huawei.com:443/service/esupplier/importVendorItems/1.0.0";

        /// <summary>
        /// 关键物料BOM信息
        /// </summary>
        public const string MaterialKeyBomApiUrl = "https://openapi.huawei.com:443/service/esupplier/importKeyMaterials/1.0.0";

        /// <summary>
        /// 人力
        /// </summary>
        public const string ManPowerApiUrl = "https://api-beta.huawei.com:443/service/esupplier/importManpower/1.0.0";
        /// <summary>
        /// 库存明细
        /// </summary>
        public const string FactoryInventoryApiUrl = "https://openapi.huawei.com:443/service/esupplier/importInventory/1.0.0";
        /// <summary>
        /// 在制明细
        /// </summary>
        public const string MaterialMakingApiUrl = "https://openapi.huawei.com:443/service/esupplier/importMaterialMaking/1.0.0";
        /// <summary>
        /// 发料明细
        /// </summary>
        public const string MaterialShipmentApiUrl = "https://openapi.huawei.com:443/service/esupplier/importMaterialShipment/1.0.0";
        /// <summary>
        /// 在途明细
        /// </summary>
        public const string PurchaseOnWayApiUrl = "https://openapi.huawei.com:443/service/esupplier/importOpenPoData/1.0.0";
    }

    internal class HwModuleName
    {
        public const string ManPower = "人力信息";

        public const string MaterialInventory = "物料库存明细";

        public const string MaterialMaking = "物料在制明细";

        public const string MaterialShipment = "物料发料信息";

        public const string MaterialBaseInfo = "物料基础信息设置";

        public const string MaterialKeyBom = "关键物料BOM信息";

        public const string PurchaseOnWay = "采购在途明细";
    }
    /// <summary>
    /// 上传成功物料数据传输对象
    /// </summary>
    public class UploadSuccessMaterialDto
    {
        /// <summary>
        /// 物料编号
        /// </summary>
        public string MaterialId { get; set; }
    }
    /// <summary>
    /// 扩展类
    /// </summary>
    public static class HwCollaborationExtension
    {
        public static string ToFormatDate(this string date)
        {
            return string.Format("{0}-{1}-{2}", date.Substring(0, 4), date.Substring(4, 2), date.Substring(6, 2));
        }

        public static string ToDiscriptionOrderStatus(this string orderStatus)
        {
            Dictionary<string, string> statusDic = new Dictionary<string, string>() {
                { "1","未生产"},
                { "2","发布"},//已发料
                { "3","在制"},//生产中
                { "Y","完成"},//已完工
                { "y","指定完工"}
            };
            return statusDic[orderStatus.Trim()];
        }

        public static HwAccessApiResult AsHwAccessApiResult(this string accessApiResultMessage)
        {
            HwAccessApiResult apiResult = ObjectSerializer.DeserializeObject<HwAccessApiResult>(accessApiResultMessage);
            return apiResult;
        }

        public static OpResult AsOpResult(this HwAccessApiResult accessApiResult)
        {
            bool isSuccess = accessApiResult != null && accessApiResult.success;
            if (isSuccess)
            {
                return OpResult.SetSuccessResult("向华为平台发送信息成功");
            }
            else
            {
                string msg = accessApiResult.errorMessage;
                return OpResult.SetErrorResult(string.Format("向华为平台发送信息出现错误：{0}", msg));
            }
        }
    }
}
