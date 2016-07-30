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
                var GetDicGroupListDataSources = FileOperationExtension.GetGroupList<EquipmentModel>(_waitingMaintenanceList, "SafekeepDepartment");
               
                Dictionary <string ,string > dic=new Dictionary<string,string> ();
                dic.Add("AssetNumber", "财产编号");
                dic.Add("EquipmentName", "名称");
                dic.Add("EquipmentSpec", "规格型号");
                dic.Add("FunctionDescription", "功能描述");
                dic.Add("PlannedCheckDate", "计划校验日期");
                dic.Add("SafekeepDepartment", "保管部门");
                dic.Add("ManufacturingNumber", "制造编号");

                DataTable dd = FileOperationExtension.GetDataTable<EquipmentModel>(_waitingMaintenanceList, dic);

                Dictionary<string, DataTable> dest = FileOperationExtension.GetGroupDataTableList(dd, "保管部门");

                return NPOIHelper.ExportDataTableToExcelMultiSheets(dest);
              

                //return NPOIHelper.ExportToExcelMultiSheets(GetDicGroupListDataSources);
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
