using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.Erp.Bussiness.QuantityManage;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    public class InspectionFqcFormManager
    {
        public List<InspectionFqcMasterModel> GetInspectionFormManagerListBy(string selectedDepartment, string formStatus, DateTime dateFrom, DateTime dateTo)
        {
            //查询ERP中所有物料和单号 
            return InspectionManagerCrudFactory.FqcMasterCrud.GetFqcInspectionMasterModelListBy(dateFrom, dateTo, selectedDepartment, formStatus);
        }

        public List<InspectionFqcDetailModel> GetInspectionDatailListBy(string orderId, int orderIdNumber)
        {
            return InspectionManagerCrudFactory.FqcDetailCrud.GetFqcInspectionDetailDatasBy(orderId, orderIdNumber);
        }
        /// <summary>
        ///审核主表数据
        /// </summary>
        /// <returns></returns>
        public OpResult AuditFqcInspectionModel(InspectionFqcMasterModel model)
        {
            try
            {
                if (model == null) return OpResult.SetErrorResult("FQC主表不能为空");
                //先改变主表的状态
                var retrunResult = InspectionManagerCrudFactory.FqcMasterCrud.Store(model, true);
                if (!retrunResult.Result) return OpResult.SetErrorResult("FQC主表审核状态更新失败");
                string inspectionItemStatus = "Done";
                //主要更新成功 再   更新详细表的信息
                if (model.InspectionStatus == "待审核") inspectionItemStatus = "doing";
                retrunResult = InspectionManagerCrudFactory.FqcMasterCrud.UpAuditDetailData(model.OrderId, model.OrderIdNumber, inspectionItemStatus);
                if (!retrunResult.Result) return OpResult.SetErrorResult("FQC详细表审核状态更新失败");
                return retrunResult;
            }
            catch (Exception ex)
            {
                return OpResult.SetErrorResult(ex.Message);
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// 查询ERP中FQC检验状态
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<ProductFqcMaterailInfoVm> GetERPOrderAndMaterialBy(string department, DateTime startTime, DateTime endTime)
        {

            List<ProductFqcMaterailInfoVm> retrunList = new List<ProductFqcMaterailInfoVm>();
            ProductFqcMaterailInfoVm FqcMaterailInfoVm = null;
            var OrderIdList = GetOrderIdList(startTime, endTime, department);
            if (OrderIdList == null || OrderIdList.Count <= 0) return retrunList;
            OrderIdList.ForEach(e =>
            {
                FqcMaterailInfoVm = new ProductFqcMaterailInfoVm();
                var FqcMasterDatas = FqcMasterDatasBy(e.OrderID);
                OOMaper.Mapper<MaterialModel, ProductFqcMaterailInfoVm>(e, FqcMaterailInfoVm);
                if (FqcMasterDatas != null && FqcMasterDatas.Count > 0)
                {
                    FqcMaterailInfoVm.HaveInspectionCount = FqcMasterDatas.Sum(m => m.InspectionCount);
                    FqcMaterailInfoVm.InspectionNumber = FqcMasterDatas.Count;
                }
                FqcMaterailInfoVm.NoInspectionCount = FqcMaterailInfoVm.ProduceNumber - FqcMaterailInfoVm.HaveInspectionCount;
                retrunList.Add(FqcMaterailInfoVm);
            });
            return retrunList.OrderBy(e => e.OrderID).ToList();
        }

        public List<InspectionFqcMasterModel> FqcMasterDatasBy(string orderId)
        {
            return InspectionManagerCrudFactory.FqcMasterCrud.GetFqcInspectionMasterModelListBy(orderId);
        }
        List<MaterialModel> GetOrderIdList(DateTime starDate, DateTime endDate, string department)
        {
            return QualityDBManager.OrderIdInpectionDb.FindErpAllMasterilBy(starDate, endDate, department);
        }


        /// <summary>
        /// 生成合格供应商清单
        /// </summary>
        /// <returns></returns>
        public DownLoadFileModel BuildDownLoadFileModel(List<InspectionFqcMasterModel> datas)
        {
            try
            {
                if (datas == null || datas.Count == 0) return new DownLoadFileModel().Default();
                var dataGroupping = datas.GetGroupList<InspectionFqcMasterModel>();
                return dataGroupping.ExportToExcelMultiSheets<InspectionFqcMasterModel>(CreateFieldMapping()).CreateDownLoadExcelFileModel("FQC检验数据");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<FileFieldMapping> CreateFieldMapping()
        {
            //OrderId, OrderIdNumber, ProductDepartment, MaterialId, MaterialName, MaterialSpec, MaterialSupplier, 
            //    MaterialInDate, MaterialDrawId, MaterialInCount, InspectionMode, InspectionResult, InspectionCount, InspectionStatus, 
            //    InspectionItemCount, InspectionItems, FinishDate, Memo, OpPerson, OpDate, OpTime, OpSign, Id_Key
            List<FileFieldMapping> fieldmappping = new List<FileFieldMapping>(){
                new FileFieldMapping ("OrderId","单号") ,
                new FileFieldMapping ("OrderIdNumber","序号") ,
                new FileFieldMapping ("MaterialId","料号") ,
                new FileFieldMapping ("MaterialName","品名") ,
                new FileFieldMapping ("MaterialSpec","规格") ,
                new FileFieldMapping ("MaterialSupplier","供应商") ,
                new FileFieldMapping ("MaterialInDate","进货日期") ,
                new FileFieldMapping ("MaterialInCount","进货数量") ,
                new FileFieldMapping ("MaterialDrawId","图号") ,
                new FileFieldMapping ("InspectionCount","抽样数量") ,
                new FileFieldMapping ("InspectionStatus","状态"),
                new FileFieldMapping ("InspectionResult","检测结果") ,
                new FileFieldMapping ("FinishDate","完成时间") ,
                new FileFieldMapping ("OpPerson","抽检人"),
            };
            return fieldmappping;
        }

    }
}
