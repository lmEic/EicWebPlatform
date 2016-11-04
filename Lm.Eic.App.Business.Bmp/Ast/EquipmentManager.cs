using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeExtension.Validation;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CrudFactory = Lm.Eic.App.Business.Bmp.Ast.EquipmentCrudFactory;

namespace Lm.Eic.App.Business.Bmp.Ast
{
    /// <summary>
    /// 设备管理器
    /// </summary>
    public class EquipmentManager
    {
        #region Equipment

        /// <summary>
        /// 获取设备总览表
        /// </summary>
        /// <returns></returns>
        public List<EquipmentModel> GetAstArchiveOverview()
        {
            return CrudFactory.EquipmentCrud.FindBy(new QueryEquipmentDto() { SearchMode = 6 });
        }

        /// <summary>
        /// 生成财产编号
        /// </summary>
        /// <param name="equipmentType">设备类别 （生产设备，量测设备）</param>
        /// <param name="assetType">资产类别 （固定资产，低质易耗品）</param>
        /// <param name="taxType">税务类别 （保税，非保税）</param>
        /// <returns></returns>
        public string BuildAssetNumber(string equipmentType, string assetType, string taxType)
        {
            /*设备编码共七码
              第一位：     类别码，保税设备为I、非保税设备为E、低质易耗品为Z ' PS如果冲突以设备类别为主。
              第二、三位： 年度码，例2016年记为16。
              第四位：     设备代码，生产设备为9，显示其它数字为量测设备。
              后三位：     流水码 找最大的号码+1。   */
            string assetNumber_1 = string.Empty,
                assetNumber_2_3 = DateTime.Now.Date.ToString("yy"),
                assetNumber_4 = string.Empty,
                assetNumber_5_7 = string.Empty;
            try
            {
                assetNumber_1 = assetType == "低质易耗品" ? "Z" : taxType == "保税" ? "I" : "E";
                assetNumber_4 = equipmentType == "生产设备" ? "9" : "0";

                string temAssetNumber = string.Format("{0}{1}{2}", assetNumber_1, assetNumber_2_3, assetNumber_4);
                var temEntitylist = CrudFactory.EquipmentCrud.FindBy(new QueryEquipmentDto() { AssetNumber = temAssetNumber, SearchMode = 1 });

                if (temEntitylist != null && temEntitylist.Count > 0)
                {
                    List<int> numList = new List<int>();
                    temEntitylist.ForEach((m) =>
                    {
                        if (m.AssetNumber != null && m.AssetNumber.Length > 3)
                        {
                            int temNum = 0;
                            int.TryParse(m.AssetNumber.Substring(m.AssetNumber.Length - 3, 3), out temNum);
                            numList.Add(temNum);
                        }
                    });
                    assetNumber_5_7 = (numList.Max() + 1).ToString("000");
                }
                else
                {
                    assetNumber_5_7 = "001";
                }

                return assetNumber_5_7.IsNullOrEmpty() ? "" : string.Format("{0}{1}{2}{3}", assetNumber_1, assetNumber_2_3, assetNumber_4, assetNumber_5_7);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 查询 1.依据财产编号查询 2.依据保管部门查询 3.依据录入日期查询
        /// 4.依据录入日期查询待校验设备 5.依据录入日期查询待保养设备
        /// </summary>
        /// <param name="qryDto">设备查询数据传输对象 </param>
        /// <returns></returns>
        public List<EquipmentModel> FindBy(QueryEquipmentDto qryDto)
        {
            return CrudFactory.EquipmentCrud.FindBy(qryDto);
        }

        /// <summary>
        /// 修改数据仓库 PS：model.OpSign = add/edit/delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(EquipmentModel model)
        {
            return CrudFactory.EquipmentCrud.Store(model);
        }

        #endregion Equipment

        /// <summary>
        /// 校验管理
        /// </summary>
        public EquipmentCheckManager CheckManager
        {
            get { return OBulider.BuildInstance<EquipmentCheckManager>(); }
        }

        /// <summary>
        /// 保养管理
        /// </summary>
        public EquipmentMaintenanceManager MaintenanceManager
        {
            get { return OBulider.BuildInstance<EquipmentMaintenanceManager>(); }
        }

        /// <summary>
        /// 报废管理
        /// </summary>
        public EquipmentDiscardManager DiscardManager
        {
            get { return OBulider.BuildInstance<EquipmentDiscardManager>(); }
        }

        /// <summary>
        /// 维修管理
        /// </summary>
        public EquipmentRepairedManager RepairedManager
        {
            get { return OBulider.BuildInstance<EquipmentRepairedManager>(); }
        }
    }

    /// <summary>
    /// 设备校验管理器
    /// </summary>
    public class EquipmentCheckManager
    {
        private List<EquipmentModel> _waitingCheckList = new List<EquipmentModel>();

        /// <summary>
        /// 获取待校验设备列表
        /// </summary>
        /// <param name="plannedCheckDate">计划校验日期</param>
        /// <returns></returns>
        public List<EquipmentModel> GetWaitingCheckListBy(DateTime plannedCheckDate)
        {
            try
            {
                _waitingCheckList = CrudFactory.EquipmentCrud.FindBy(new QueryEquipmentDto() { PlannedCheckDate = plannedCheckDate, SearchMode = 4 });
                return _waitingCheckList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 生成校验清单
        /// </summary>
        /// <returns></returns>
        public MemoryStream BuildWaitingCheckList()
        {
            List<FileFieldMapping> fieldmappping = new List<FileFieldMapping>(){
                 new FileFieldMapping {FieldName ="Number",FieldDiscretion="项次",} ,
                  new FileFieldMapping {FieldName ="AssetNumber",FieldDiscretion="编号",}  ,
                  new FileFieldMapping {FieldName ="EquipmentName",FieldDiscretion="名称",} ,
                  new FileFieldMapping {FieldName ="EquipmentSpec",FieldDiscretion="规格型号",} ,
                  new FileFieldMapping {FieldName ="ManufacturingNumber",FieldDiscretion="制造编号",}  ,
                  new FileFieldMapping {FieldName ="DeliveryDate",FieldDiscretion="购入日期",} ,
                  new FileFieldMapping {FieldName ="EquipmentType",FieldDiscretion="分类",}
                };
            //对未超期的数据按部门分组的处理
            var inDateList = GetPeriodWaitingCheckListRule(_waitingCheckList);
            var dataTableGrouping = inDateList.GetGroupList<EquipmentModel>("EquipmentType");
            //对超期的数据加入到数据字典中
            var overdueDateList = GetOverdueWaitingCheckListRule(_waitingCheckList);

            // 加入到数据字典中
            dataTableGrouping.Add("超期待校验列表", overdueDateList);
            return dataTableGrouping.ExportToExcelMultiSheets<EquipmentModel>(fieldmappping);
        }

        /// <summary>
        /// 得到已超期待校验设备列表
        /// </summary>
        /// <param name="waitingChecklist"></param>
        /// <returns></returns>
        private List<EquipmentModel> GetOverdueWaitingCheckListRule(List<EquipmentModel> waitingChecklist)
        {
            DateTime NowDate = DateTime.Now.Date.ToDate();
            return waitingChecklist.FindAll(e => e.PlannedCheckDate <= NowDate);
        }

        /// <summary>
        /// 获取未超期待校验列表
        /// </summary>
        /// <param name="waitingChecklist"></param>
        /// <returns></returns>
        private List<EquipmentModel> GetPeriodWaitingCheckListRule(List<EquipmentModel> waitingChecklist)
        {
            DateTime NowDate = DateTime.Now.Date.ToDate();
            return waitingChecklist.FindAll(e => e.PlannedCheckDate > NowDate);
        }

        /// <summary>
        /// 查询 1.依据财产编号查询
        /// </summary>
        /// <param name="qryDto">设备查询数据传输对象 </param>
        /// <returns></returns>
        public List<EquipmentCheckRecordModel> FindBy(QueryEquipmentDto qryDto)
        {
            return CrudFactory.EquipmentCheckCrud.FindBy(qryDto);
        }

        /// <summary>
        /// 修改数据仓库 PS：model.OpSign = add/edit/delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(EquipmentCheckRecordModel model)
        {
            return CrudFactory.EquipmentCheckCrud.Store(model);
        }
    }

    /// <summary>
    /// 设备保养管理器
    /// </summary>
    public class EquipmentMaintenanceManager
    {
        private List<EquipmentModel> _waitingMaintenanceList = new List<EquipmentModel>();

        /// <summary>
        /// 导出Excel
        /// </summary>
        private List<FileFieldMapping> fieldmappping = new List<FileFieldMapping>(){
                  new FileFieldMapping {FieldName ="Number",FieldDiscretion="项次",},
                  new FileFieldMapping {FieldName ="AssetNumber",FieldDiscretion="财产编号",} ,
                  new FileFieldMapping {FieldName ="EquipmentName",FieldDiscretion="名称",} ,
                  new FileFieldMapping {FieldName ="EquipmentSpec",FieldDiscretion="规格型号",} ,
                  new FileFieldMapping {FieldName ="FunctionDescription",FieldDiscretion="使用范围",},
                  new FileFieldMapping {FieldName ="PlannedCheckDate",FieldDiscretion="计划校验日期",},
                  new FileFieldMapping {FieldName ="SafekeepDepartment",FieldDiscretion="保管部门",},
                  new FileFieldMapping {FieldName ="ManufacturingNumber",FieldDiscretion="制造编号",} ,
                  new FileFieldMapping {FieldName ="More",FieldDiscretion="备注",}
                };

        /// <summary>
        /// 获取待保养清单
        /// </summary>
        /// <param name="plannedMaintenanceMonth">计划保养月</param>
        /// <returns></returns>
        public List<EquipmentModel> GetWaitingMaintenanceListBy(string plannedMaintenanceMonth)
        {
            try
            {
                _waitingMaintenanceList = CrudFactory.EquipmentCrud.FindBy(new QueryEquipmentDto() { PlannedMaintenanceMonth = plannedMaintenanceMonth, SearchMode = 5 });
                return _waitingMaintenanceList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 生成保养清单
        /// </summary>
        /// <returns></returns>
        public MemoryStream BuildWaitingMaintenanceList()
        {
            try
            {
                var dataGroupping = _waitingMaintenanceList.GetGroupList<EquipmentModel>("SafekeepDepartment");
                return dataGroupping.ExportToExcelMultiSheets<EquipmentModel>(fieldmappping);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
            //return NPOIHelper.ExportToExcel(_waitingMaintenanceList, "待保养设备列表");
        }

        /// <summary>
        /// 查询 1.依据财产编号查询
        /// </summary>
        /// <param name="qryDto">设备查询数据传输对象 </param>
        /// <returns></returns>
        public List<EquipmentMaintenanceRecordModel> FindBy(QueryEquipmentDto qryDto)
        {
            return CrudFactory.EquipmentMaintenanceCrud.FindBy(qryDto);
        }

        /// <summary>
        /// 修改数据仓库 PS：model.OpSign = add/edit/delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(EquipmentMaintenanceRecordModel model)
        {
            return CrudFactory.EquipmentMaintenanceCrud.Store(model);
        }
    }

    /// <summary>
    ///设备报废管理
    /// </summary>
    public class EquipmentDiscardManager
    {
        /// <summary>
        /// 获取设备报废记录
        /// </summary>
        /// <param name="assetNumber">财产编号</param>
        /// <returns></returns>
        public EquipmentDiscardRecordModel GetEquipmentDiscardRecord(string assetNumber)
        {
            return CrudFactory.EquipmentDiscarCrud.GetEquipmentDiscardRecord(assetNumber);
        }

        /// <summary>
        /// 仓储操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(EquipmentDiscardRecordModel model)
        {
            return CrudFactory.EquipmentDiscarCrud.Store(model);
        }

        /// <summary>
        /// 获取设备报废总览表
        /// </summary>
        /// <returns></returns>
        public List<EquipmentDiscardRecordModel> GetEquipmentDiscardOverView()
        {
            return CrudFactory.EquipmentDiscarCrud.GetEquipmentDiscardOverView();
        }
    }

    /// <summary>
    /// 设备维修管理
    /// </summary>
    public class EquipmentRepairedManager
    {
         
        /// <summary>
        /// 添加一条设备维修记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult AddEquipmentRepairedRecord(EquipmentRepairedRecordModel model)
        {
            model.OpSign = OpMode.Add;
            return CrudFactory.EquipmentRepairedRecordCrud.Store(model);
        }

        /// <summary>
        /// 获取设备维修记录
        /// </summary>
        /// <param name="assetNumber">财产编号</param>
        /// <returns></returns>
        public List<EquipmentRepairedRecordModel> GetEquipmentRepairedRecordBy(string assetNumber)
        {
            return CrudFactory.EquipmentRepairedRecordCrud.GetEquipmentRepairedRecordBy(assetNumber);
        }

        /// <summary>
        /// 获取设备维修总览表
        /// </summary>
        /// <returns></returns>
        public List<EquipmentRepairedRecordModel> GetEquipmentRepairedOverView()
        {
            return CrudFactory.EquipmentRepairedRecordCrud.GetEquipmentRepairedOverView();
        }

    }
}