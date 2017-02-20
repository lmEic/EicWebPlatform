using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Erp.Domain.InvManageModel
{
    /// <summary>
    /// 成品仓位模块   INVMC
    /// </summary>
   public  class FinishedProductStoreModel
    {
        /// <summary>
        /// 品号 MC001
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 品名  
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 规格 
        /// </summary>
        public string ProductSpecify { get; set; }
       /// <summary>
       ///入库数量 MC007
       /// </summary>
        public double InStroeNumber { set; get; }
       /// <summary>
       /// 仓位 MC002
       /// </summary>
        public string  StroeId { set; get; }
       /// <summary>
       /// 备注 MC003
       /// </summary>
        public string More { set; get; }
       /// <summary>
       /// 入库日期 MC012
       /// </summary>
        public DateTime PutInStoreDate
        {set;get; }
    }
}
