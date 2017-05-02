using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{


    public class IqcDetailDatasGather
    {



        public List<InspectionIqcDetailModel> GetIqcDetailModeDatasBy(string materailId, string inspecitonItem)
        {

            return InspectionManagerCrudFactory.IqcDetailCrud.GetIqcInspectionDetailModelDatasBy(materailId, inspecitonItem);


        }
        public List<InspectionIqcMasterModel> GetIqcMasterModeDatasBy(string materailId)
        {

            return InspectionManagerCrudFactory.IqcMasterCrud.GetIqcInspectionMasterDatasBy(materailId);


        }

        /// <summary>
        /// 通过总表 存储Iqc检验详细数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreInspectionIqcDetailModelForm(InspectionItemDataSummaryVM model, string siteRootPath)
        {
            InspectionIqcDetailModel datailModel = new InspectionIqcDetailModel()
            {
                OrderId = model.OrderId,
                EquipmentId = model.EquipmentId,
                MaterialCount = model.MaterialInCount,
                InspecitonItem = model.InspectionItem,
                InspectionAcceptCount = model.AcceptCount,
                InspectionCount = model.InspectionCount,
                InspectionRefuseCount = model.RefuseCount,
                InspectionDate = DateTime.Now,
                InspectionItemDatas = model.InspectionItemDatas,
                InspectionItemResult = model.InspectionItemResult,
                //InspectionItemStatus = model.InsptecitonItemIsFinished.ToString(),
                InspectionItemStatus = "doing",
                InspectionMode = model.InspectionMode,
                MaterialId = model.MaterialId,
                MaterialInDate = model.MaterialInDate,
                FileName = model.FileName,
                OpSign = model.OpSign,
                Memo = model.Memo,
                InspectionNGCount = model.InspectionNGCount,
                OpPerson = model.OpPerson,
                DocumentPath = model.DocumentPath,
                Id_Key = model.Id_Key
            };
            return storeInspectionDetial(datailModel, siteRootPath);


        }
        private OpResult storeInspectionDetial(InspectionIqcDetailModel model, string siteRootPath)
        {
            if (model != null && model.OpSign == OpMode.UploadFile)//如果是上传文件则启动上传文件处理程序
                return InspectionManagerCrudFactory.IqcDetailCrud.UploadFileIqcInspectionDetail(model, siteRootPath);
            /// 判断是否存在此录入的项次
            if (InspectionManagerCrudFactory.IqcDetailCrud.isExiststroe(model))
                model.OpSign = OpMode.Edit;
            else model.OpSign = OpMode.Add;
            return InspectionManagerCrudFactory.IqcDetailCrud.Store(model, true);
        }
        /// <summary>
        /// 初始存储数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult InitializestoreInspectionDetial(InspectionItemDataSummaryVM model)
        {
            InspectionIqcDetailModel datailModel = new InspectionIqcDetailModel()
            {
                OrderId = model.OrderId,
                EquipmentId = model.EquipmentId,
                MaterialCount = model.MaterialInCount,
                InspecitonItem = model.InspectionItem,
                InspectionAcceptCount = model.AcceptCount,
                InspectionCount = model.InspectionCount,
                InspectionRefuseCount = model.RefuseCount,
                InspectionDate = DateTime.Now,
                InspectionItemDatas = model.InspectionItemDatas,
                InspectionItemResult = model.InspectionItemResult,
                //InspectionItemStatus = model.InsptecitonItemIsFinished.ToString(),
                InspectionItemStatus = "doing",
                InspectionMode = model.InspectionMode,
                MaterialId = model.MaterialId,
                MaterialInDate = model.MaterialInDate,
                FileName = model.FileName,
                OpSign = model.OpSign,
                Memo = model.Memo,
                InspectionNGCount = model.InspectionNGCount,
                OpPerson = model.OpPerson,
                DocumentPath = model.DocumentPath
            };
            /// 判断是否存在此录入的项次
            if (InspectionManagerCrudFactory.IqcDetailCrud.isExiststroe(datailModel)) return null;
            datailModel.OpSign = OpMode.Add;
            return InspectionManagerCrudFactory.IqcDetailCrud.Store(datailModel, true);
        }
        /// <summary>
        /// 得到副表的详细参数
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materailId"></param>
        /// <param name="inspecitonItem"></param>
        /// <returns></returns>
        public InspectionIqcDetailModel GetIqcInspectionDetailModelBy(string orderId, string materailId, string inspecitonItem)
        {
            return InspectionManagerCrudFactory.IqcDetailCrud.GetIqcInspectionDetailModelBy(orderId, materailId, inspecitonItem);
        }

        /// <summary>
        ///  得到副表的详细参数List
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materailId"></param>
        /// <returns></returns>
        public List<InspectionIqcDetailModel> GetIqcInspectionDetailModeDatasBy(string orderId, string materailId)
        {
            return InspectionManagerCrudFactory.IqcDetailCrud.GetIqcInspectionDetailOrderIdModelBy(orderId, materailId);
        }
    }
}
