using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport
{
    public class QueryDailyReportDto
    {

        string department = string.Empty;
        /// <summary>
        /// 部门
        /// </summary>
        public string Department
        {
            get { return department; }
            set { if (department != value) { department = value; } }
        }

        DateTime inputDate = DateTime.Now.Date;
        /// <summary>
        /// 输入日期
        /// </summary>
        public DateTime InputDate
        {
            get { return inputDate; }
            set { if (inputDate != value) { inputDate = value; } }
        }

        string productName = string.Empty;
        /// <summary>
        ///  产品名称
        /// </summary>
        public string ProductName
        {
            get { return productName; }
            set { if (productName != value) { productName = value; } }
        }

        string productFlowName = string.Empty;
        /// <summary>
        /// 工艺名称
        /// </summary>
        public string ProductFlowName
        {
            set { if (productFlowName != value) { productFlowName = value; } }
            get { return productFlowName; }
        }

        string orderId = string.Empty;
        /// <summary>
        /// 工单单号
        /// </summary>
        public string OrderId
        {
            set { if(orderId != value) { orderId = value; } }
            get { return orderId; }
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
