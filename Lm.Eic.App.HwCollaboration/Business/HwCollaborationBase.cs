using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HwRestfulApi;

namespace Lm.Eic.App.HwCollaboration.Business
{
    /// <summary>
    /// 华为协同业务基类
    /// </summary>
    public abstract class HwCollaborationBase<T> where T : class, new()
    {
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
        protected string apiUrl = string.Empty;
        protected T dto = null;

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
        /// 通过华为API接口同步数据
        /// </summary>
        /// <returns></returns>
        public string SynchronizeDatas()
        {
            SetApiUrlAndDto();
            return AccessApi(this.apiUrl, dto);
        }
        /// <summary>
        /// 设定ApiUrl值和Dto传输模型的值
        /// </summary>
        protected abstract void SetApiUrlAndDto();
    }
}
