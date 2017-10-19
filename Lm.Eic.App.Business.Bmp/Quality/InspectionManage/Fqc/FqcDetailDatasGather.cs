using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    public class FqcDetailDatasGather
    {
        /// <summary>
        ///  存储副表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult storeInspectionDetial(InspectionFqcDetailModel model)
        {
            //if (model != null && model.OpSign == OpMode.UploadFile)//如果是上传文件则启动上传文件处理程序
            //    return InspectionManagerCrudFactory.FqcDetailCrud.UploadFileFqcInspectionDetail(model, siteRootPath);
            return InspectionManagerCrudFactory.FqcDetailCrud.Store(model, true);
        }
        /// <summary>
        /// 得到副表的详细记录
        /// </summary>
        /// <param name="orderIdNumber">单号序号</param>
        /// <param name="orderId">单号</param>
        /// <returns></returns>
        public List<InspectionFqcDetailModel> GetFqcInspectionDetailDatasBy(string orderId, int orderIdNumber)
        {
            return InspectionManagerCrudFactory.FqcDetailCrud.GetFqcInspectionDetailDatasBy(orderId, orderIdNumber);
        }
    }

}
