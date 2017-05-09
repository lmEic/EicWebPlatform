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
    /// 设备保养管理器
    /// </summary>
    public class EquipmentMaintenanceManager
    {
        private List<EquipmentModel> _waitingMaintenanceList = new List<EquipmentModel>();

        /// <summary>
        /// 导出Excel
        /// </summary>
        private List<FileFieldMapping> fieldmappping = new List<FileFieldMapping>(){
                  new FileFieldMapping ("Number","项次"),
                  new FileFieldMapping ("AssetNumber","财产编号") ,
                  new FileFieldMapping ("EquipmentName","名称") ,
                  new FileFieldMapping ("EquipmentSpec","规格型号") ,
                  new FileFieldMapping ("FunctionDescription","使用范围"),
                  new FileFieldMapping ("PlannedCheckDate","计划校验日期"),
                  new FileFieldMapping ("SafekeepDepartment","保管部门"),
                  new FileFieldMapping ("ManufacturingNumber","制造编号") ,
                  new FileFieldMapping ("More","备注")
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
}
