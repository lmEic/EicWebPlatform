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
    /// 设备校验管理器
    /// </summary>
    public class EquipmentCheckManager
    {
        private List<EquipmentModel> _waitingCheckList = new List<EquipmentModel>();

        /// <summary>
        /// 获取待校验设备列表
        /// </summary>
        /// <param name="plannedCheckDate">计划校验日期</param>
        /// <returns></returns>
        public List<EquipmentModel> GetWaitingCheckListBy(DateTime plannedCheckDate)
        {
            try
            {
                _waitingCheckList = CrudFactory.EquipmentCrud.FindBy(new QueryEquipmentDto() { PlannedCheckDate = plannedCheckDate, SearchMode = 4 });
                return _waitingCheckList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 生成校验清单
        /// </summary>
        /// <returns></returns>
        public MemoryStream BuildWaitingCheckList()
        {
            List<FileFieldMapping> fieldmappping = new List<FileFieldMapping>(){
                 new FileFieldMapping ("Number","项次") ,
                  new FileFieldMapping ("AssetNumber","编号") ,
                  new FileFieldMapping ("EquipmentName","名称") ,
                  new FileFieldMapping ("EquipmentSpec","规格型号") ,
                  new FileFieldMapping ("ManufacturingNumber","制造编号")  ,
                  new FileFieldMapping ("DeliveryDate","购入日期") ,
                  new FileFieldMapping ("EquipmentType","分类")
                };
            //对未超期的数据按部门分组的处理
            var inDateList = GetPeriodWaitingCheckListRule(_waitingCheckList);
            var dataTableGrouping = inDateList.GetGroupList<EquipmentModel>("EquipmentType");
            //对超期的数据加入到数据字典中
            var overdueDateList = GetOverdueWaitingCheckListRule(_waitingCheckList);

            // 加入到数据字典中
            dataTableGrouping.Add("超期待校验列表", overdueDateList);
            return dataTableGrouping.ExportToExcelMultiSheets<EquipmentModel>(fieldmappping);
        }

        /// <summary>
        /// 得到已超期待校验设备列表
        /// </summary>
        /// <param name="waitingChecklist"></param>
        /// <returns></returns>
        private List<EquipmentModel> GetOverdueWaitingCheckListRule(List<EquipmentModel> waitingChecklist)
        {
            DateTime NowDate = DateTime.Now.Date.ToDate();
            return waitingChecklist.FindAll(e => e.PlannedCheckDate <= NowDate);
        }

        /// <summary>
        /// 获取未超期待校验列表
        /// </summary>
        /// <param name="waitingChecklist"></param>
        /// <returns></returns>
        private List<EquipmentModel> GetPeriodWaitingCheckListRule(List<EquipmentModel> waitingChecklist)
        {
            DateTime NowDate = DateTime.Now.Date.ToDate();
            return waitingChecklist.FindAll(e => e.PlannedCheckDate > NowDate);
        }

        /// <summary>
        /// 查询 1.依据财产编号查询
        /// </summary>
        /// <param name="qryDto">设备查询数据传输对象 </param>
        /// <returns></returns>
        public List<EquipmentCheckRecordModel> FindBy(QueryEquipmentDto qryDto)
        {
            return CrudFactory.EquipmentCheckCrud.FindBy(qryDto);
        }

        /// <summary>
        /// 修改数据仓库 PS：model.OpSign = add/edit/delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(EquipmentCheckRecordModel model)
        {
            return CrudFactory.EquipmentCheckCrud.Store(model);
        }
    }
}
