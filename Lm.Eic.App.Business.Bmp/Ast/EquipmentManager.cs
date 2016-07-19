using Lm.Eic.App.DbAccess.Bpm.Repository.AstRep;
using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.Validation;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lm.Eic.App.Business.Bmp.Ast
{
    /// <summary>
    /// 设备管理具体实现
    /// </summary>
    public class EquipmentManager
    {
        private IEquipmentRepository irep = null;

        public EquipmentManager()
        {
            irep = new EquipmentRepository();
        }

        /// <summary>
        /// 生成财产编号
        /// </summary>
        /// <param name="equipmentType">设备类别 （生产设备，量测设备）</param>
        /// <param name="assetType">资产类别 （固定资产，低值易耗品）</param>
        /// <param name="taxType">税务类别 （保税，非保税）</param>
        /// <returns></returns>
        public string BuildAssetNumber(string equipmentType, string assetType, string taxType)
        {
            /*设备编码共七码
              第一位：     类别码，保税设备为I、非保税设备为E、低质易耗品为Z ' PS如果冲突以设备类别为主。
              第二、三位： 年度码，例2016年记为16。
              第四位：     设备代码，生产设备为9，显示其它数字为量测设备。
              后三位：     编号码。   */
            string assetNumber_1 = string.Empty,
                assetNumber_2_3 = DateTime.Now.Date.ToString("yy"),
                assetNumber_4 = string.Empty,
                assetNumber_5_7 = string.Empty;
            try
            {
                assetNumber_1 = assetType == "低质易耗品" ? "Z" : taxType == "保税" ? "I" : "E";
                assetNumber_4 = equipmentType == "生产设备" ? "9" : "0";
                string temAssetNumber = string.Format("{0}{1}{2}", assetNumber_1, assetNumber_2_3, assetNumber_4);
                var temEntitylist = irep.FindAll<EquipmentModel>(m => m.AssetNumber.StartsWith(temAssetNumber));
                assetNumber_5_7 = (temEntitylist.Count + 1).ToString("000");
                return assetNumber_5_7.IsNullOrEmpty() ? "" : string.Format("{0}{1}{2}{3}", assetNumber_1, assetNumber_2_3, assetNumber_4, assetNumber_5_7);
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
        public OpResult Store(List<EquipmentModel> listModel)
        {
            int record = 0;
            string opContext = "设备档案", opSign = string.Empty;
            OpResult opResult = OpResult.SetResult("未执行任何操作！", false);

            if (listModel == null || listModel.Count <= 0)
                return OpResult.SetResult("集合不能为空！", false);

            opSign = listModel[0].OpSign;
            if (opSign.IsNullOrEmpty())
                return OpResult.SetResult("操作模式不能为空！", false);

            try
            {
                switch (opSign)
                {
                    case OpMode.Add: //新增
                        listModel.ForEach(model => { record += irep.Insert(model); });
                        opResult = record.ToOpResult_Add(opContext);
                        break;

                    case OpMode.Edit: //修改
                        listModel.ForEach(model => { record += irep.Update(u => u.Id_Key == model.Id_Key, model); });
                        opResult = record.ToOpResult_Eidt(opContext);
                        break;

                    case OpMode.Delete: //删除
                        listModel.ForEach(model => { record += irep.Delete(model.Id_Key); });
                        opResult = record.ToOpResult_Delete(opContext);
                        break;

                    default:
                        opResult = OpResult.SetResult("操作模式溢出！", false);
                        break;
                }
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
            return opResult;
        }

        /// <summary>
        /// 查询 1.依据财产编号查询 2.依据保管部门查询 3.依据录入日期查询
        /// </summary>
        /// <param name="qryDto">设备查询数据传输对象 </param>
        /// <returns></returns>
        public List<EquipmentModel> FindBy(QueryEquipmentDto qryDto)
        {
            try
            {
                switch (qryDto.SearchMode)
                {
                    case 1: //依据财产编号查询
                        return irep.FindAll<EquipmentModel>(m => m.AssetNumber.StartsWith(qryDto.AssetNumber)).ToList();

                    case 2: //依据保管部门查询
                        return irep.FindAll<EquipmentModel>(m => m.SafekeepDepartment.StartsWith(qryDto.Department)).ToList();

                    case 3: //依据录入日期
                        DateTime qryDate = qryDto.InputDate.ToDate();
                        return irep.FindAll<EquipmentModel>(m => m.InputDate == qryDate).ToList();

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
        public OpResult Store(EquipmentModel model)
        {
            OpResult result = OpResult.SetResult("财产编号不能为空！", false);
            if (model == null || model.AssetNumber.IsNullOrEmpty())
                return result;
            try
            {
                switch (model.OpSign)
                {
                    case OpMode.Add: //新增
                        result = AddEquipmentRecord(model);
                        break;

                    case OpMode.Edit: //修改
                        result = EditEquipmentRecord(model);
                        break;

                    case OpMode.Delete: //删除
                        result = DeleteEquipmentRecord(model);
                        break;

                    default:
                        result = OpResult.SetResult("操作模式溢出", false);
                        break;
                }
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
            return result;
        }

        private OpResult AddEquipmentRecord(EquipmentModel model)
        {
            DateTime defaultDate = DateTime.Now.ToDate();
            //基础设置
            model.InputDate = defaultDate;
            model.OpDate = defaultDate;
            model.PlannedCheckDate = defaultDate;
            model.PlannedMaintenanceDate = defaultDate;
            SetEquipmentMaintenanceRule(model);
            SetEquipmentCheckRule(model);
            //设备状态初始化
            if (model.State == null)
                model.State = "运行正常";
            if (model.IsScrapped == null)
                model.IsScrapped = "未报废";
            //仓储操作
            return irep.Insert(model).ToOpResult_Add("设备档案", model.Id_Key);
        }

        private OpResult EditEquipmentRecord(EquipmentModel model)
        {
            SetEquipmentMaintenanceRule(model);
            SetEquipmentCheckRule(model);

            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt("设备档案");
        }

        private OpResult DeleteEquipmentRecord(EquipmentModel model)
        {
            return irep.Delete(model.Id_Key).ToOpResult_Delete("设备档案");
        }

        /// <summary>
        /// 设置设备校验规则
        /// </summary>
        /// <param name="model"></param>
        private void SetEquipmentCheckRule(EquipmentModel model)
        {
            //校验处理
            model.IsCheck = (model.CheckInterval == 0) ? "不校验" : "校验";
            if (model.IsCheck == "校验")
            {
                model.PlannedCheckDate = model.CheckDate.AddMonths(model.CheckInterval);
                model.CheckState = model.PlannedCheckDate > DateTime.Now ? "在期" : "超期";
            }
            else
            {
                model.CheckDate = DateTime.Now.ToDate();
                model.CheckInterval = 0;
            }
        }

        /// <summary>
        /// 设置设备保养规则
        /// </summary>
        /// <param name="model"></param>
        private void SetEquipmentMaintenanceRule(EquipmentModel model)
        {
            //保养处理
            //  model.IsMaintenance = (model.MaintenanceDate == null && model.MaintenanceInterval == 0) ? "不保养" : "保养";
            model.IsMaintenance = model.AssetType == "低质易耗品" ? "不保养" : "保养";
            if (model.IsMaintenance == "保养")
            {
                model.PlannedMaintenanceDate = model.MaintenanceDate.AddMonths(model.MaintenanceInterval);
                model.MaintenanceState = model.PlannedMaintenanceDate > DateTime.Now ? "在期" : "超期";
            }
            else
            {
                model.MaintenanceDate = DateTime.Now.ToDate();//设置为默认日期
                model.MaintenanceInterval = 0;
            }
        }
    }
}