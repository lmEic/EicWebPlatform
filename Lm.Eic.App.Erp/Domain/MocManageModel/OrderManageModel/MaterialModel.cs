using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel
{
    /// <summary>
    /// 工单物料
    /// </summary>
    public class MaterialModel
    {
        //ToDo:料号，品名，规格，单位，需领用量，已领用量，等......
        /// <summary>
        ///  料号
        /// </summary>
        public string MaterialId
        {
            set;
            get;
        }
        /// <summary>
        ///   品名
        /// </summary>
        public string MaterialName
        {
            set;
            get;
        }
        /// <summary>
        ///   规格
        /// </summary>
        public string MaterialSpecify
        {
            set;
            get;
        }
        /// <summary>
        ///    单位
        /// </summary>
        public string MaterialUnit
        { 
            set; 
            get; 
        }
        /// <summary>
        ///    需领用量
        /// </summary>
        public double MaterialNeedCount
        {
            set;
            get;
        }
        /// <summary>
        ///      已领用量
        /// </summary>
        public double MaterialReceiveCount
        {
            set;
            get;
        }
    }
}
