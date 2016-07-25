using Lm.Eic.App.DbAccess.Bpm.Repository.AstRep;
using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Lm.Eic.App.Business.Bmp.Ast
{
    public interface IEquipmentChekcManager
    {
        /// <summary>
        /// 获取待校验设备列表
        /// </summary>
        /// <returns></returns>
        List<EquipmentModel> GetEquipmentNotCheck();

        List<EquipmentCheckModel> GetEquipmentCheckRecord(QueryEquipmentDto qryDto);

        /// <summary>
        /// 导出待校验设备到Excel
        /// </summary>
        /// <returns></returns>
        MemoryStream ExportEquipmentNotCheckToExcle();

        /// <summary>
        /// 录入设备校验记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        OpResult InputEquipmentCheckRecord(EquipmentCheckModel model);


    }


    public class EquipmentCheckManager:IEquipmentChekcManager
    {

        private IEquipmentCheckRepository irep = null;
        private EquipmentManager equipmentManager = null;
        private IEquipmentRepository irepEqui = null;

        public EquipmentCheckManager()
        {
            irep = new EquipmentCheckRepository();
            irepEqui = new EquipmentRepository();
            equipmentManager = new EquipmentManager();
        }

        /// <summary>
        /// 获取待校验设备列表
        /// </summary>
        /// <returns></returns>
        public List<EquipmentModel> GetEquipmentNotCheck()
        {
            var tem = irepEqui.FindAll<EquipmentModel>(m => m.CheckState == "");
            return tem.ToList();
        }

        /// <summary>
        /// 获取校验记录
        /// </summary>
        /// <param name="qryDto"></param>
        /// <returns></returns>
        public List<EquipmentCheckModel> GetEquipmentCheckRecord(QueryEquipmentDto qryDto)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 导出待校验设备列表到Excel中
        /// </summary>
        /// <returns></returns>
        public MemoryStream ExportEquipmentNotCheckToExcle()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 录入设备校验记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult InputEquipmentCheckRecord(EquipmentCheckModel model)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="qryModel">设备查询数据传输对象</param>
        /// <param name="searchMode"> 1.依据财产编号查询  </param>
        /// <returns></returns>
        List<EquipmentCheckModel> FindBy(QueryEquipmentDto qryDto)
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

                var equipmentList = equipmentManager.FindBy(new QueryEquipmentDto() { AssetNumber = model.AssetNumber ,SearchMode=1 });
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
            if (!equipmentManager.Store(equipment).Result)
                return OpResult.SetResult("更新设备校验日期时错误！", false);
            return OpResult.SetResult("增加校验记录成功", irep.Insert(model) > 0, model.Id_Key);
        }

        private OpResult EditEquipmentCheckRecord(EquipmentCheckModel model, EquipmentModel equipment)
        {
            equipment.CheckDate = model.CheckDate;
            equipment.PlannedCheckDate = model.CheckDate.AddMonths(equipment.CheckInterval);
            equipment.OpSign = OpMode.Edit;
            if (!equipmentManager.Store(equipment).Result)
                return OpResult.SetResult("更新设备校验日期时错误！", false);
            return OpResult.SetResult("更新校验记录成功", irep.Update(u => u.Id_Key == model.Id_Key, model) > 0, model.Id_Key);
        }

        private OpResult DeleteEquipmentCheckRecord(EquipmentCheckModel model, EquipmentModel equipment)
        {
            equipment.CheckDate = model.CheckDate;
            equipment.PlannedCheckDate = model.CheckDate.AddMonths(equipment.CheckInterval);
            equipment.OpSign = OpMode.Edit;
            if (!equipmentManager.Store(equipment).Result)
                return OpResult.SetResult("更新设备校验日期时错误！", false);
            return OpResult.SetResult("校验记录删除成功", irep.Delete(model.Id_Key) > 0, model.Id_Key);
        }

       
    }
}