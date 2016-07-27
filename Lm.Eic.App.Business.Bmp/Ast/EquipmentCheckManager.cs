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
        /// PS：输入日期为起始日期 结束日期为输入日期加一个月 
        /// </summary>
        /// <param name="dateTime">起始时间</param>
        /// <returns>计划校验日期 （大于等于起始日期 && 小于等于结束日期）|| 小于今天日期</returns>
        public List<EquipmentModel> GetEquipmentNotCheck(DateTime dateTime)
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
        public MemoryStream ExportEquipmentNotCheckToExcle()
        {
            return NPOIHelper.ExportToExcel(equipmentNotCheckList, "待校验设备列表");
        }


        /// <summary>
        /// 查询 1.依据财产编号查询 
        /// </summary>
        /// <param name="qryDto">设备查询数据传输对象 </param>
        /// <returns></returns>
        public List<EquipmentCheckModel> FindBy(QueryEquipmentDto qryDto)
        {
            return crud.FindBy(qryDto);
        }

        /// <summary>
        /// 修改数据仓库
        /// </summary>
        /// <param name="listModel">模型</param>
        /// <param name="operationMode">操作模式 1.新增 2.修改 3.删除</param>
        /// <returns></returns>
        public OpResult Store(EquipmentCheckModel model)
        {
            return crud.Store(model);
        }

    }


    public class EquipmentCheckCrud
    {
        private IEquipmentCheckRepository irep = null;
        private EquipmentCrud equipmentCrud = new EquipmentCrud();
        
        public EquipmentCheckCrud()
        {
            irep = new EquipmentCheckRepository();
        }

        #region FindBy

        /// <summary>
        /// 查询 1.依据财产编号查询 
        /// </summary>
        /// <param name="qryDto">设备查询数据传输对象 </param>
        /// <returns></returns>
        public List<EquipmentCheckModel> FindBy(QueryEquipmentDto qryDto)
        {
            try
            {
                switch (qryDto.SearchMode)
                {
                    case 1: //依据财产编号查询
                        return irep.FindAll<EquipmentCheckModel>(m => m.AssetNumber.StartsWith(qryDto.AssetNumber)).ToList();
                    default: return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        #endregion

        #region Store

        /// <summary>
        /// 修改数据仓库
        /// </summary>
        /// <param name="listModel">模型</param>
        /// <param name="operationMode">操作模式 1.新增 2.修改 3.删除</param>
        /// <returns></returns>
        public OpResult Store(EquipmentCheckModel model)
        {
            try
            {
                if (model == null)
                    return OpResult.SetResult("校验记录不能为空！", false);

                var equipmentList = equipmentCrud.FindBy(new QueryEquipmentDto() { AssetNumber = model.AssetNumber, SearchMode = 1 });
                var equipment = equipmentList.Count > 0 ? equipmentList[0] : null;
                if (equipment == null)
                    return OpResult.SetResult("未找到校验单上的设备\r\n请确定财产编号是否正确！", false);

                switch (model.OpSign)
                {
                    case OpMode.Add: //新增
                        return AddEquipmentCheckRecord(model, equipment);

                    case OpMode.Edit: //修改
                        return EditEquipmentCheckRecord(model, equipment);

                    case OpMode.Delete: //删除
                        return DeleteEquipmentCheckRecord(model, equipment);

                    default: return OpResult.SetResult("操作模式溢出", false);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        private OpResult AddEquipmentCheckRecord(EquipmentCheckModel model, EquipmentModel equipment)
        {
            equipment.CheckDate = model.CheckDate;
            equipment.PlannedCheckDate = model.CheckDate.AddMonths(equipment.CheckInterval);
            equipment.OpSign = OpMode.Edit;
            if (!equipmentCrud.Store(equipment).Result)
                return OpResult.SetResult("更新设备校验日期时错误！", false);
            return OpResult.SetResult("增加校验记录成功", irep.Insert(model) > 0, model.Id_Key);
        }

        private OpResult EditEquipmentCheckRecord(EquipmentCheckModel model, EquipmentModel equipment)
        {
            equipment.CheckDate = model.CheckDate;
            equipment.PlannedCheckDate = model.CheckDate.AddMonths(equipment.CheckInterval);
            equipment.OpSign = OpMode.Edit;
            if (!equipmentCrud.Store(equipment).Result)
                return OpResult.SetResult("更新设备校验日期时错误！", false);
            return OpResult.SetResult("更新校验记录成功", irep.Update(u => u.Id_Key == model.Id_Key, model) > 0, model.Id_Key);
        }

        private OpResult DeleteEquipmentCheckRecord(EquipmentCheckModel model, EquipmentModel equipment)
        {
            equipment.CheckDate = model.CheckDate;
            equipment.PlannedCheckDate = model.CheckDate.AddMonths(equipment.CheckInterval);
            equipment.OpSign = OpMode.Edit;
            if (!equipmentCrud.Store(equipment).Result)
                return OpResult.SetResult("更新设备校验日期时错误！", false);
            return OpResult.SetResult("校验记录删除成功", irep.Delete(model.Id_Key) > 0, model.Id_Key);
        }

        #endregion
    }
}