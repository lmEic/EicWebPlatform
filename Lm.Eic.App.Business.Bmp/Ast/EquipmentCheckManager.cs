using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.IO;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using CrudFactory = Lm.Eic.App.Business.Bmp.Ast.EquipmentCrudFactory;
using System.Reflection;
using System.Data;

namespace Lm.Eic.App.Business.Bmp.Ast
{
    public class EquipmentCheckManager
    {
        List<EquipmentModel> _waitingCheckList = new List<EquipmentModel>();

        /// <summary>
        /// 获取待校验设备列表
        /// </summary>
        /// <param name="plannedCheckDate">计划校验日期</param>  
        /// <returns></returns>
        public List<EquipmentModel> GetWaitingCheckListBy(DateTime plannedCheckDate)
        {
            //todo:  dd
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
            Dictionary<string, string> dicEnglishChinese = new Dictionary<string, string>();
            dicEnglishChinese.Add("AssetNumber", " 编号");
            dicEnglishChinese.Add("EquipmentName", "名称");
            dicEnglishChinese.Add("EquipmentSpec", "规格型号");
            dicEnglishChinese.Add("ManufacturingNumber", "制造编号");
            dicEnglishChinese.Add("DeliveryDate", "购入日期");
            dicEnglishChinese.Add("EquipmentType", "分类");

              //对未超期的数据按部门分组的处理
                var inDateList = GetPeriodWaitingCheckListRule(_waitingCheckList);
                DataTable newDataTable = FileOperationExtension.GetDataTable<EquipmentModel>(inDateList, dicEnglishChinese);
                Dictionary<string, DataTable> dataTableGrouping = FileOperationExtension.GetGroupDataTables(newDataTable, "分类");

                //对超期的数据加入到数据字典中
                var overdueDateList = GetOverdueWaitingCheckListRule(_waitingCheckList);
                DataTable overdueNewDataTable = FileOperationExtension.GetDataTable<EquipmentModel>(overdueDateList, dicEnglishChinese);
                dataTableGrouping.Add("超期待校验列表", overdueNewDataTable);
                return NPOIHelper.ExportDataTableToExcelMultiSheets(dataTableGrouping);

            // 对未来超期的数据按部门分组的处理
          
           
            //return NPOIHelper.ExportToExcel(_waitingCheckList, "待校验设备列表");
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
        public List<EquipmentCheckModel> FindBy(QueryEquipmentDto qryDto)
        {
            return CrudFactory.EquipmentCheckCrud.FindBy(qryDto);
        }

        /// <summary>
        /// 修改数据仓库 PS：model.OpSign = add/edit/delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(EquipmentCheckModel model)
        {
            return CrudFactory.EquipmentCheckCrud.Store(model);
        }

      
    }
}