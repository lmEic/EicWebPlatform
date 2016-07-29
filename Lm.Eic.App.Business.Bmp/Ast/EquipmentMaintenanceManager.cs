using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using CrudFactory = Lm.Eic.App.Business.Bmp.Ast.EquipmentCrudFactory;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;

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
                var GetDicGroupListDataSources = FileOperationExtension.GetDicGroupListRuleT<EquipmentModel>(_waitingMaintenanceList, "SafekeepDepartment");
               
                return NPOIHelper.ExportToExcelMultiSheets(GetDicGroupListDataSources);
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
