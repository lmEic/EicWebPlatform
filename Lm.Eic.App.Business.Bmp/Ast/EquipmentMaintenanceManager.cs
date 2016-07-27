using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.IO;
using CrudFactory = Lm.Eic.App.Business.Bmp.Ast.EquipmentCrudFactory;

namespace Lm.Eic.App.Business.Bmp.Ast
{
   public class EquipmentMaintenanceManager
    {
        List<EquipmentModel> equipmentWithoutMaintenanceList = new List<EquipmentModel>();
      
        /// <summary>
        /// 获取待保养设备列表
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public List<EquipmentModel> GetWithoutMaintenanceEquipment(DateTime dateTime)
        {
            try
            {
                equipmentWithoutMaintenanceList = CrudFactory.EquipmentCrud.FindBy(new QueryEquipmentDto() { InputDate = dateTime, SearchMode = 5 });
                return equipmentWithoutMaintenanceList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 导出待保养设备列表 到Excel中
        /// </summary>
        /// <returns></returns>
        public MemoryStream ExportWithoutMaintenanceEquipmentListToExcle()
        {
            return NPOIHelper.ExportToExcel(equipmentWithoutMaintenanceList, "待校验设备列表");
        }

        /// <summary>
        /// 查询 1.依据财产编号查询 
        /// </summary>
        /// <param name="qryDto">设备查询数据传输对象 </param>
        /// <returns></returns>
        public List<EquipmentMaintenanceModel> FindBy(QueryEquipmentDto qryDto) { return CrudFactory.EquipmentMaintenanceCrud.FindBy(qryDto); }

        /// <summary>
        /// 修改数据仓库 PS：model.OpSign = add/edit/delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(EquipmentMaintenanceModel model) { return CrudFactory.EquipmentMaintenanceCrud.Store(model); }

    }
}
