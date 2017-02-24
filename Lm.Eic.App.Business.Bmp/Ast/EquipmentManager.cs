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
    /// 设备管理器
    /// </summary>
    public class EquipmentManager
    {
        #region Equipment

        /// <summary>
        /// 获取设备总览表
        /// </summary>
        /// <returns></returns>
        public List<EquipmentModel> GetAstArchiveOverview()
        {
            return CrudFactory.EquipmentCrud.FindBy(new QueryEquipmentDto() { SearchMode = 6 });
        }

        /// <summary>
        /// 生成盘点清单
        /// </summary>
        /// <returns></returns>
        public MemoryStream BuildInventoryList()
        {
              //AssetNumber, EquipmentName, EquipmentSpec, ManufacturingNumber, MaintenanceDate, SafekeepDepartment, AssetType
            List<FileFieldMapping> fieldmappping = new List<FileFieldMapping>(){
                 new FileFieldMapping ("Number","项次") ,
                  new FileFieldMapping ("AssetNumber","编号")  ,
                  new FileFieldMapping ("EquipmentName","名称") ,
                  new FileFieldMapping ("EquipmentSpec","规格型号/厂名") ,
                  new FileFieldMapping ("ManufacturingNumber","制造编号")  ,
                  new FileFieldMapping ("MaintenanceDate","登录日期"),
                  new FileFieldMapping ("SafekeepDepartment","部门"),
                    new FileFieldMapping ("AssetType","资产分类")
                };
            var modelList = CrudFactory.EquipmentCrud.FindBy(new QueryEquipmentDto() { SearchMode = 6 });
            var dataTableGrouping = modelList.GetGroupList<EquipmentModel>("SafekeepDepartment");
            return dataTableGrouping.ExportToExcelMultiSheets<EquipmentModel>(fieldmappping);
        }
        /// <summary>
        /// 生成财产编号
        /// </summary>
        /// <param name="equipmentType">设备类别 （生产设备，量测设备）</param>
        /// <param name="assetType">资产类别 （固定资产，低质易耗品）</param>
        /// <param name="taxType">税务类别 （保税，非保税）</param>
        /// <returns></returns>
        public string BuildAssetNumber(string equipmentType, string assetType, string taxType)
        {
            /*设备编码共七码
              第一位：     类别码，保税设备为I、非保税设备为E、低质易耗品为Z ' PS如果冲突以设备类别为主。
              第二、三位： 年度码，例2016年记为16。
              第四位：     设备代码，生产设备为9，显示其它数字为量测设备。
              后三位：     流水码 找最大的号码+1。   */
            string assetNumber_1 = string.Empty,
                assetNumber_2_3 = DateTime.Now.Date.ToString("yy"),
                assetNumber_4 = string.Empty,
                assetNumber_5_7 = string.Empty;
            try
            {
                assetNumber_1 = assetType == "低质易耗品" ? "Z" : taxType == "保税" ? "I" : "E";
                assetNumber_4 = equipmentType == "生产设备" ? "9" : "0";

                string temAssetNumber = string.Format("{0}{1}{2}", assetNumber_1, assetNumber_2_3, assetNumber_4);
                var temEntitylist = CrudFactory.EquipmentCrud.FindBy(new QueryEquipmentDto() { AssetNumber = temAssetNumber, SearchMode = 7 });

                if (temEntitylist != null && temEntitylist.Count > 0)
                {
                    List<int> numList = new List<int>();
                    temEntitylist.ForEach((m) =>
                    {
                        if (m.AssetNumber != null && m.AssetNumber.Length > 3)
                        {
                            int temNum = 0;
                            int.TryParse(m.AssetNumber.Substring(m.AssetNumber.Length - 3, 3), out temNum);
                            numList.Add(temNum);
                        }
                    });
                    assetNumber_5_7 = (numList.Max() + 1).ToString("000");
                }
                else
                {
                    assetNumber_5_7 = "001";
                }

                return assetNumber_5_7.IsNullOrEmpty() ? "" : string.Format("{0}{1}{2}{3}", assetNumber_1, assetNumber_2_3, assetNumber_4, assetNumber_5_7);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 查询 1.依据财产编号查询 2.依据保管部门查询 3.依据录入日期查询
        /// 4.依据录入日期查询待校验设备 5.依据录入日期查询待保养设备
        /// </summary>
        /// <param name="qryDto">设备查询数据传输对象 </param>
        /// <returns></returns>
        public List<EquipmentModel> FindBy(QueryEquipmentDto qryDto)
        {
            return CrudFactory.EquipmentCrud.FindBy(qryDto);
        }

        /// <summary>
        /// 修改数据仓库 PS：model.OpSign = add/edit/delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(EquipmentModel model)
        {
            return CrudFactory.EquipmentCrud.Store(model);
        }

        #endregion Equipment

        /// <summary>
        /// 校验管理
        /// </summary>
        public EquipmentCheckManager CheckManager
        {
            get { return OBulider.BuildInstance<EquipmentCheckManager>(); }
        }

        /// <summary>
        /// 保养管理
        /// </summary>
        public EquipmentMaintenanceManager MaintenanceManager
        {
            get { return OBulider.BuildInstance<EquipmentMaintenanceManager>(); }
        }

        /// <summary>
        /// 报废管理
        /// </summary>
        public EquipmentDiscardManager DiscardManager
        {
            get { return OBulider.BuildInstance<EquipmentDiscardManager>(); }
        }

        /// <summary>
        /// 维修管理
        /// </summary>
        public EquipmentRepairedManager RepairedManager
        {
            get { return OBulider.BuildInstance<EquipmentRepairedManager>(); }
        }
    }




 

   
}