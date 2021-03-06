﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel
{
    /// <summary>
    /// 工单物料
    /// </summary>
    public class OrderMaterialModel
    {

        /// <summary>
        ///  料号
        /// </summary>
        public string MaterialId { set; get; }
        /// <summary>
        ///  品名
        /// </summary>
        public string MaterialName { set; get; }
        /// <summary>
        ///  规格
        /// </summary>
        public string MaterialSpecify { set; get; }
        /// <summary>
        ///  单位
        /// </summary>
        public string Unit { set; get; }
        /// <summary>
        ///  需领用量
        /// </summary>
        public double NeedCount { set; get; }
        /// <summary>
        ///  已领用量
        /// </summary>
        public double ReceiveCount { set; get; }
    }
    /// <summary>
    /// Bom表信息
    /// </summary>
    public class BomMaterialModel
    {
        /// <summary>
        ///  主件料号
        /// </summary>
        public string MainMaterialId { set; get; }
        /// <summary> 
        ///  阶  次 （计算出来）
        /// </summary>
        public string Grade { get; set; }
        /// <summary>
        ///  组件料号
        /// </summary>
        public string MaterialId { set; get; }

        /// <summary>
        /// 料号信息
        /// </summary>
        public MarterialBaseInfo MaterialIdInfo { set;get; } 
     
        /// <summary>
        /// 组成用量  (MD006)
        /// </summary>
        public double NeedNumber { get; set; }
        /// <summary>
        /// 低数   (MD007)
        /// </summary>
        public double  BaseNumber { get; set; }
    }
    /// <summary>
    /// 物料基本信息
    /// </summary>
    public class MarterialBaseInfo
    {
        public MarterialBaseInfo()
        { }
        /// <summary>
        ///  组件料号
        /// </summary>
        public string MaterialId { set; get; }
        /// <summary>
        /// 组件物料名称
        /// </summary>
        public string MaterialName { set; get; }
        /// <summary>
        ///  规格
        /// </summary>
        public string MaterialSpecify { set; get; }
        /// <summary>
        /// 组件属性
        /// </summary>
        public string Property { set; get; }
        /// <summary>
        ///  单位
        /// </summary>
        public string Unit { set; get; }


    }
    /// <summary>
    /// 替代料件
    /// </summary>
    public class AgentMaterilModel
    {
        public string MatreialID { set; get; }
        public string AgentMaterialId { set; get; }

    }
}
