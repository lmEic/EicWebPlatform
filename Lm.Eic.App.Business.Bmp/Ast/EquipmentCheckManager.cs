using Lm.Eic.App.DbAccess.Bpm.Repository.AstRep;
using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lm.Eic.App.Business.Bmp.Ast
{
    public class EquipmentCheckManager
    {
      
        private IEquipmentCheckRepository irep = null;
        private EquipmentManager equipmentManager = null;

        public EquipmentCheckManager()
        {
            irep = new EquipmentCheckRepository();
            equipmentManager = new EquipmentManager();
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="qryModel">设备查询数据传输对象</param>
        /// <param name="searchMode"> 1.依据财产编号查询  </param>
        /// <returns></returns>
        public List<EquipmentCheckModel> FindBy(QueryEquipmentDto qryDto, int searchMode)
        {
            try
            {
                switch (searchMode)
                {
                    case 1: //依据财产编号查询
                        return irep.FindAll<EquipmentCheckModel>(m => m.AssetNumber.StartsWith(qryDto.SearchValue)).ToList();

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
        public OpResult Store(EquipmentCheckModel model, string opSign)
        {
            try
            {
                if (model == null)
                    return OpResult.SetResult("校验记录不能为空！", false);

                var equipmentList = equipmentManager.FindBy(new EquipmentManager.QueryEquipmentDto() { SearchValue = model.AssetNumber }, 1);
                var equipment = equipmentList.Count > 0 ? equipmentList[0] : null;
                if (equipment == null)
                    return OpResult.SetResult("未找到校验单上的设备\r\n请确定财产编号是否正确！", false);

                switch (opSign)
                {
                    case OpMode.Add: //新增
                        {
                            equipment.CheckDate = model.CheckData;
                            equipment.PlannedCheckDate = model.CheckData.AddDays(equipment.CheckInterval);
                            if (!equipmentManager.Store(equipment, OpMode.Edit).Result)
                                return OpResult.SetResult("更新设备校验日期时错误！", false);
                            return OpResult.SetResult("增加校验记录成功", irep.Insert(model) > 0,model.Id_Key);
                        }
                    case OpMode.Edit: //修改
                        {
                            equipment.CheckDate = model.CheckData;
                            equipment.PlannedCheckDate = model.CheckData.AddDays(equipment.CheckInterval);
                            if (!equipmentManager.Store(equipment, OpMode.Edit).Result)
                                return OpResult.SetResult("更新设备校验日期时错误！", false);
                            return OpResult.SetResult("更新校验记录成功", irep.Update(u => u.Id_Key == model.Id_Key, model) > 0,model.Id_Key);
                        }

                    case OpMode.Delete: //删除
                        {
                            equipment.CheckDate = model.CheckData;
                            equipment.PlannedCheckDate = model.CheckData.AddDays(equipment.CheckInterval);
                            if (!equipmentManager.Store(equipment, OpMode.Edit).Result)
                                return OpResult.SetResult("更新设备校验日期时错误！", false);
                            return OpResult.SetResult("校验记录删除成功", irep.Delete(model.Id_Key) > 0,model.Id_Key);
                        }
                    default: return OpResult.SetResult("操作模式溢出", false);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        #region 查询参数类

        public class QueryEquipmentDto
        {
            private string searchValue = string.Empty;

            /// <summary>
            /// 单条件搜索参数
            /// </summary>
            public string SearchValue
            {
                get { return searchValue; }
                set { if (searchValue != value) { searchValue = value; } }
            }
        }

        #endregion 查询参数类
    }
}