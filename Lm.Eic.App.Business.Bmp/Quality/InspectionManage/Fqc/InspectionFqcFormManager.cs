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
                retrunResult = InspectionManagerCrudFactory.FqcDetailCrud.UpAuditDetailDataStatus(model.OrderId, model.OrderIdNumber, inspectionItemStatus);
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
                List<InspectionFqcMasterModel> needDatas = new List<InspectionFqcMasterModel>();
                datas.ForEach(e =>
                {
                    e.InspectionItemInspectors = HandelInspectionItemInspectors(e.InspectionItemInspectors);
                    e.InspectionItemDetails= HandelInspectionItemDetails(e.InspectionItemDetails);
                    e.OpPerson = e.InspectionStatus != "已审核" ? string.Empty : e.OpPerson;
                    needDatas.Add(e);
                });
                var dataGroupping = needDatas.GetGroupList<InspectionFqcMasterModel>();
                return dataGroupping.ExportToExcelMultiSheets<InspectionFqcMasterModel>(CreateFieldMapping()).CreateDownLoadExcelFileModel("FQC检验数据");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 分析处理详细表 返不良项目和数据
        /// </summary>
        /// <param name="inspectionItemDetails"></param>
        /// <returns></returns>
        private string HandelInspectionItemDetails(string inspectionItemDetails)
        {
            List<string> inspectionItemDetailsList = new List<string>();
            InspectionItemDataSummaryVM sumModel = null;
            string returnstring = string.Empty;
            if (inspectionItemDetails !=null&& inspectionItemDetails != string.Empty)
            {
                if (inspectionItemDetails.Contains("&"))
                {
                    inspectionItemDetailsList = inspectionItemDetails.Split('&').ToList();
                    if (inspectionItemDetailsList.Count > 1)
                        inspectionItemDetailsList.ForEach(m =>
                        {
                            sumModel = ObjectSerializer.ParseFormJson<InspectionItemDataSummaryVM>(m);
                            if (sumModel != null)
                            { returnstring += sumModel.InspectionItem + ":NG " + "Datas:" + sumModel.InspectionItemDatas; }
                        });
                }
                else
                {
                    sumModel = ObjectSerializer.ParseFormJson<InspectionItemDataSummaryVM>(inspectionItemDetails);
                    if (sumModel != null)
                    { returnstring = sumModel.InspectionItem + ":NG " + "Datas:" + sumModel.InspectionItemDatas; }
                }
            }
            return returnstring;
        }

        /// <summary>
        /// 分析处理检验人员信息
        /// </summary>
        /// <param name="inspectionItemInspectors"></param>
        /// <returns></returns>
        private string HandelInspectionItemInspectors(string inspectionItemInspectors)
        {
            string inspectorsUsers = string.Empty;
            List<string> inspectorsSplit = new List<string>();
            List<string> inspectorNameSplit = null;
            if (inspectionItemInspectors.Contains(","))
            {
                inspectorsSplit = inspectionItemInspectors.Split(',').ToList();
                if (inspectorsSplit.Count > 0)
                {
                    inspectorsSplit.ForEach(s =>
                    {
                        inspectorNameSplit = s.Contains(":") ? s.Split(':').ToList():new List<string>();
                        if (inspectorNameSplit.Count == 2)
                        {
                            if (!inspectorsUsers.Contains(inspectorNameSplit[1]))
                            {
                                inspectorsUsers = (inspectorNameSplit[1] != "StartSetValue") ? ((inspectorsUsers == String.Empty) ? inspectorNameSplit[1] : (inspectorsUsers + "," + inspectorNameSplit[1])) : string.Empty;
                            }
                        }
                    });
                }

            }
            else return inspectionItemInspectors;
            return inspectorsUsers;
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
                new FileFieldMapping ("MaterialDrawId","图号") ,
                new FileFieldMapping ("ProductDepartment","供应商") ,
                new FileFieldMapping ("FinishDate","送检日期") ,
                new FileFieldMapping ("InspectionCount","送检数") ,
                new FileFieldMapping ("InspectionMaxNumber","抽样数") ,
                new FileFieldMapping ("InspectionNgNumber","不良数") ,
                new FileFieldMapping ("InspectionStatus","状态"),
                new FileFieldMapping ("InspectionResult","检测结果") ,
                 new FileFieldMapping ("InspectionItemInspectors","抽检人"),
                new FileFieldMapping ("OpPerson","审核人"),
                new FileFieldMapping ("MaterialInCount","工单数"),
                new FileFieldMapping ("InspectionItemDetails","不良现象"),

            };
            return fieldmappping;
        }

    }
}
