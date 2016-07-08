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
        public OpResult ChangeStorage(EquipmentCheckModel model, int operationMode)
        {
            try
            {
                if (model == null)
                    return OpResult.SetResult("校验记录不能为空！", false);

                var equipmentList = equipmentManager.FindBy(new EquipmentManager.QueryEquipmentDto() { SearchValue = model.AssetNumber }, 1);
                var equipment = equipmentList.Count > 0 ? equipmentList[0] : null;
                if (equipment == null)
                    return OpResult.SetResult("未找到校验单上的设备\r\n请确定财产编号是否正确！", false);

                switch (operationMode)
                {
                    case 1: //新增
                        {
                            equipment.CheckDate = model.CheckData;
                            equipment.PlannedCheckDate = model.CheckData.AddDays(equipment.CheckInterval);
                            if (!equipmentManager.ChangeStorage(equipment, 2).Result)
                                return OpResult.SetResult("更新设备校验日期时错误！", false);
                            return OpResult.SetResult("增加校验记录成功", irep.Insert(model) > 0);
                        }
                    case 2: //修改
                        {
                            equipment.CheckDate = model.CheckData;
                            equipment.PlannedCheckDate = model.CheckData.AddDays(equipment.CheckInterval);
                            if (!equipmentManager.ChangeStorage(equipment, 2).Result)
                                return OpResult.SetResult("更新设备校验日期时错误！", false);
                            return OpResult.SetResult("更新校验记录成功", irep.Update(u => u.Id_Key == model.Id_Key, model) > 0);
                        }

                    case 3: //删除
                        {
                            equipment.CheckDate = model.CheckData;
                            equipment.PlannedCheckDate = model.CheckData.AddDays(equipment.CheckInterval);
                            if (!equipmentManager.ChangeStorage(equipment, 2).Result)
                                return OpResult.SetResult("更新设备校验日期时错误！", false);
                            return OpResult.SetResult("校验记录删除成功", irep.Delete(model.Id_Key) > 0);
                        }
                    default: return OpResult.SetResult("操作模式溢出", false);
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
        public OpResult ChangeStorage(List<EquipmentCheckModel> listModel, int operationMode)
        {
            try
            {
                int i = 0;
                switch (operationMode)
                {
                    case 1: //新增
                        return OpResult.SetResult("添加成功！", "添加失败！", irep.Insert(listModel));

                    case 2: //修改
                        i = 0;
                        listModel.ForEach(model =>
                        {
                            if (irep.Update(u => u.Id_Key == model.Id_Key, model) > 0)
                                i++;
                        });
                        return OpResult.SetResult("更新成功！", "更新失败！", i);

                    case 3: //删除
                        i = 0;
                        listModel.ForEach(model =>
                        {
                            if (irep.Delete(model.Id_Key) > 0)
                                i++;
                        });
                        return OpResult.SetResult("删除成功！", "删除失败！", i);

                    default: return OpResult.SetResult("操作模式溢出！", "操作模式溢出", 0);
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