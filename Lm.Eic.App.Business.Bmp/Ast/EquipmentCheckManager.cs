using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.IO;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using CrudFactory = Lm.Eic.App.Business.Bmp.Ast.EquipmentCrudFactory;


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
            //todo:  dd
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
            
            Dictionary<string, List<EquipmentModel>> DicDataSources = new Dictionary<string, List<EquipmentModel>>();
            DicDataSources.Add("待校验设备列表", GetInLimitedDateWaitingCheckListRule(_waitingCheckList));
            DicDataSources.Add("超期待校验列表", GetOutdatedWaitingCheckListRule(_waitingCheckList));


            return NPOIHelper.ExportToExcelMultiSheets(DicDataSources);
           
            //return NPOIHelper.ExportToExcel(_waitingCheckList, "待校验设备列表");
        }

        /// <summary>
        /// 得到已超期待校验设备列表
        /// </summary>
        /// <param name="waitingChecklist"></param>
        /// <returns></returns>
        private List<EquipmentModel> GetOutdatedWaitingCheckListRule(List<EquipmentModel> waitingChecklist)
        {
            DateTime NowDate = DateTime.Now.Date.ToDate();
            return waitingChecklist.FindAll(e => e.PlannedCheckDate <= NowDate);
        }

        /// <summary>
        /// 获取未超期待校验列表
        /// </summary>
        /// <param name="waitingChecklist"></param>
        /// <returns></returns>
        private List<EquipmentModel> GetInLimitedDateWaitingCheckListRule(List<EquipmentModel> waitingChecklist)
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
        public OpResult EquipmentCheckStore(EquipmentCheckModel model)
        {
            return CrudFactory.EquipmentCheckCrud.Store(model);
        }

      
    }
}