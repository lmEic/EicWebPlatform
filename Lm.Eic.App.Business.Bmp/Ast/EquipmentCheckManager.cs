using Lm.Eic.App.DbAccess.Bpm.Repository.AstRep;
using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExcelHanlder;

namespace Lm.Eic.App.Business.Bmp.Ast
{
    public class EquipmentCheckManager
    {
        List<EquipmentModel> equipmentNotCheckList = new List<EquipmentModel>();
        EquipmentCheckCrud crud = null;
        EquipmentCrud equipmentCrud = null;

        public EquipmentCheckManager()
        {
            crud = new EquipmentCheckCrud();
            equipmentCrud = new EquipmentCrud();
        }

       /// <summary>
       /// 获取待校验设备列表
       /// </summary>
       /// <param name="dateTime"></param>
       /// <returns></returns>
        public List<EquipmentModel> GetWithoutCheckEquipment(DateTime dateTime)
        {
            try
            {
                equipmentNotCheckList = equipmentCrud.FindBy(new QueryEquipmentDto() { InputDate = dateTime, SearchMode = 4 });
                return equipmentNotCheckList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 导出待校验设备列表到Excel中
        /// </summary>
        /// <returns></returns>
        public MemoryStream ExportWithoutCheckEquipmentListToExcle()
        {
            return NPOIHelper.ExportToExcel(equipmentNotCheckList, "待校验设备列表");
        }


        /// <summary>
        /// 查询 1.依据财产编号查询 
        /// </summary>
        /// <param name="qryDto">设备查询数据传输对象 </param>
        /// <returns></returns>
        public List<EquipmentCheckModel> FindBy(QueryEquipmentDto qryDto) { return crud.FindBy(qryDto); }

        /// <summary>
        /// 修改数据仓库 PS：model.OpSign = add/edit/delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(EquipmentCheckModel model){ return crud.Store(model); }

    }


}