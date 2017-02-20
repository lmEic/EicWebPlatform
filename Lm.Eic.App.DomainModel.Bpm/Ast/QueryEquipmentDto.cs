using System;

namespace Lm.Eic.App.DomainModel.Bpm.Ast
{
    [Serializable]
    public class QueryEquipmentDto
    {
        string assetNumber = string.Empty;

        /// <summary>
        /// 财产编号
        /// </summary>
        public string AssetNumber
        {
            get { return assetNumber; }
            set { if (assetNumber != value) { assetNumber = value; } }
        }

        string department;
        /// <summary>
        /// 部门
        /// </summary>
        public string Department
        {
            get { return department; }
            set { if (department != value) { department = value; } }
        }

        DateTime inputDate;
        /// <summary>
        /// 录入日期
        /// </summary>
        public DateTime InputDate
        {
            get { return inputDate; }
            set { if (inputDate != value) { inputDate = value; } }
        }

         DateTime _plannedcheckdate;
        /// <summary>
        ///计划校验日期
        /// </summary>
        public DateTime PlannedCheckDate
        {
            get { return _plannedcheckdate; }
            set { if (_plannedcheckdate != value) { _plannedcheckdate = value; } }
        }

        private string _plannedMaintenanceMonth;
        /// <summary>
        ///计划保养月份
        /// </summary>
        public string PlannedMaintenanceMonth
        {
            get { return _plannedMaintenanceMonth; }
            set { if (_plannedMaintenanceMonth != value) { _plannedMaintenanceMonth = value; } }
        }
        private int searchMode = 0;
        /// <summary>
        /// 搜索模式
        /// </summary>
        public int SearchMode
        {
            get { return searchMode; }
            set { if (searchMode != value) { searchMode = value; } }
        }

    }
}
