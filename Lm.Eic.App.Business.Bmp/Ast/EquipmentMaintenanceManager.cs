using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using CrudFactory = Lm.Eic.App.Business.Bmp.Ast.EquipmentCrudFactory;

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
            {
                return NPOIHelper.ExportToExcelMultiSheets(GetDicGroupListRule(_waitingMaintenanceList));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
            //return NPOIHelper.ExportToExcel(_waitingMaintenanceList, "待保养设备列表");
        }

        /// <summary>
        /// 依每个部门保养列表
        /// </summary>
        /// <param name="waitingMaintenanceList">需要保养列表</param>
        /// <returns></returns>

        private Dictionary<string, List<EquipmentModel>> GetDicGroupListRule(List<EquipmentModel> waitingMaintenanceList)
        {
            Dictionary<string, List<EquipmentModel>> dicWaitingMaintenaceSheets = new Dictionary<string, List<EquipmentModel>>();
            List<string> DepartmentList = new List<string>();

            if (waitingMaintenanceList == null || waitingMaintenanceList.Count <= 0)
                return dicWaitingMaintenaceSheets;

            waitingMaintenanceList.ForEach(e =>
            {
                if (!DepartmentList.Contains(e.SafekeepDepartment))
                { DepartmentList.Add(e.SafekeepDepartment); }
            });

            foreach (string Department in DepartmentList)
            {
                var trm = waitingMaintenanceList.FindAll(e => e.SafekeepDepartment == Department);
                dicWaitingMaintenaceSheets.Add(Department, trm);
            }

            return dicWaitingMaintenaceSheets;
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
