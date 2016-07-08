using Lm.Eic.App.DbAccess.Bpm.Repository.AstRep;
using Lm.Eic.App.DomainModel.Bpm.Ast;
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
                if (assetType == "低值易耗")
                {
                    assetNumber_1 = "Z";
                }
                else
                {
                    assetNumber_1 = taxType == "保税" ? "I" : "E";
                }
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
        public OpResult ChangeStorage(List<EquipmentModel> listModel, int operationMode)
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

                    default: return OpResult.SetResult("操作模式溢出！", false);
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
        public OpResult ChangeStorage(EquipmentModel model, int operationMode)
        {
            try
            {
                switch (operationMode)
                {
                    case 1: //新增
                        return OpResult.SetResult("增加设备成功", irep.Insert(model) > 0);

                    case 2: //修改
                        return OpResult.SetResult("修改设备成功", irep.Update(u => u.Id_Key == model.Id_Key, model) > 0);

                    case 3: //删除
                        return OpResult.SetResult("删除设备成功", irep.Delete(model.Id_Key) > 0);

                    default: return OpResult.SetResult("操作模式溢出", false);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="qryModel">设备查询数据传输对象</param>
        /// <param name="searchMode"> 1.依据财产编号查询 2.依据保管部门查询 3.依据规格查询 </param>
        /// <returns></returns>
        public List<EquipmentModel> FindBy(QueryEquipmentDto qryDto, int searchMode)
        {
            try
            {
                switch (searchMode)
                {
                    case 1: //依据财产编号查询
                        return irep.FindAll<EquipmentModel>(m => m.AssetNumber.StartsWith(qryDto.SearchValue)).ToList();

                    case 2: //依据保管部门查询
                        return irep.FindAll<EquipmentModel>(m => m.SafekeepDepartment.StartsWith(qryDto.SearchValue)).ToList();

                    case 3: //依据规格查询
                        return irep.FindAll<EquipmentModel>(m => m.EquipmentSpec.StartsWith(qryDto.SearchValue)).ToList();

                    default: return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

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
    }
}