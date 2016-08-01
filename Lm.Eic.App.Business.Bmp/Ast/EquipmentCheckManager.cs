using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.IO;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using CrudFactory = Lm.Eic.App.Business.Bmp.Ast.EquipmentCrudFactory;
using System.Reflection;
using System.Data;

namespace Lm.Eic.App.Business.Bmp.Ast
{
    public class EquipmentCheckManager
    {
        List<EquipmentModel> _waitingCheckList = new List<EquipmentModel>();

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
                  new FileFieldMapping {FieldName ="AssetNumber",FieldDiscretion="编号",}  ,
                  new FileFieldMapping {FieldName ="EquipmentName",FieldDiscretion="名称",} ,  
                  new FileFieldMapping {FieldName ="EquipmentSpec",FieldDiscretion="规格型号",} ,
                  new FileFieldMapping {FieldName ="ManufacturingNumber",FieldDiscretion="制造编号",}  ,
                  new FileFieldMapping {FieldName ="DeliveryDate",FieldDiscretion="购入日期",} , 
                  new FileFieldMapping {FieldName ="EquipmentType",FieldDiscretion="分类",} 
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
        public List<EquipmentCheckModel> FindBy(QueryEquipmentDto qryDto)
        {
            return CrudFactory.EquipmentCheckCrud.FindBy(qryDto);
        }

        /// <summary>
        /// 修改数据仓库 PS：model.OpSign = add/edit/delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(EquipmentCheckModel model)
        {
            return CrudFactory.EquipmentCheckCrud.Store(model);
        }

      
    }
}