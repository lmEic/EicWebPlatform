using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using CrudFactory = Lm.Eic.App.Business.Bmp.Ast.EquipmentCrudFactory;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using System.Data;

namespace Lm.Eic.App.Business.Bmp.Ast
{
   public class EquipmentMaintenanceManager
    {
        List<EquipmentModel> _waitingMaintenanceList = new List<EquipmentModel>();

        /// <summary>
        /// 获取待保养清单
        /// </summary>
        /// <param name="plannedMaintenanceMonth">计划保养月</param>
        /// <returns></returns>
        public List<EquipmentModel> GetWaitingMaintenanceListBy(string  plannedMaintenanceMonth)
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
            {    //依”部门“字段对各个部门生成保养列表
                //字段对应的中文
                Dictionary<string, string> dicEnglishChinese = new Dictionary<string, string>();
                dicEnglishChinese.Add("AssetNumber", "财产编号");
                dicEnglishChinese.Add("EquipmentName", "名称");
                dicEnglishChinese.Add("EquipmentSpec", "规格型号");
                dicEnglishChinese.Add("FunctionDescription", "功能描述");
                dicEnglishChinese.Add("PlannedCheckDate", "计划校验日期");
                dicEnglishChinese.Add("SafekeepDepartment", "保管部门");
                dicEnglishChinese.Add("ManufacturingNumber", "制造编号");

                DataTable newDataTable = FileOperationExtension.GetDataTable<EquipmentModel>(_waitingMaintenanceList, dicEnglishChinese);
                ///按部门对其分组
                Dictionary<string, DataTable> dataTableGrouping = FileOperationExtension.GetGroupDataTables(newDataTable, "保管部门");

                return NPOIHelper.ExportDataTableToExcelMultiSheets(dataTableGrouping);

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
        public List<EquipmentMaintenanceModel> FindBy(QueryEquipmentDto qryDto)
        {
            return CrudFactory.EquipmentMaintenanceCrud.FindBy(qryDto);
        }

        /// <summary>
        /// 修改数据仓库 PS：model.OpSign = add/edit/delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(EquipmentMaintenanceModel model)
        {
            return CrudFactory.EquipmentMaintenanceCrud.Store(model);
        }

    }
}
