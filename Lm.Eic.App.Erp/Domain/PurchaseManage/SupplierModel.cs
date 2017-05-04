namespace Lm.Eic.App.Erp.Domain.PurchaseManage
{
    /// <summary>
    /// 供应商信息模型 PURMA:供应商信息基本档
    /// </summary>
    public class SupplierModel
    {
        /// <summary>
        /// 供应商编号 MA001
        /// </summary>
        public string SupplierID { get; set; }

        /// <summary>
        /// 供应商名称简称 MA002
        /// </summary>
        public string SupplierShortName { get; set; }

        /// <summary>
        /// 公司全称 MA003
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 联系方式 MA008
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 传真 MA010
        /// </summary>
        public string FaxNo { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 负责人 MA012
        /// </summary>
        public string Principal { get; set; }

        /// <summary>
        /// 联系人 MA013
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 联系地址 MA014
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 付款条件 MA025
        /// </summary>
        public string PayCondition { get; set; }

        /// <summary>
        /// 账单地址 MA051
        /// </summary>
        public string BillAddress { get; set; }
        /// <summary>
        ///是否在合作 MA004
        /// </summary>
        public string IsCooperate { get; set; }
    }

    /// <summary>
    /// 供应商品号信息模型 PURMB 品号供应商单头档
    /// </summary>
    public class SupplyPnModel
    {
        /// <summary>
        /// 品号 MB001
        /// </summary>
        public string PNNumer { get; set; }

        /// <summary>
        /// 供应商编号 MB002
        /// </summary>
        public string SupplierID { get; set; }

        /// <summary>
        /// 计价单位 MB004
        /// </summary>
        public string ChargeUnit { get; set; }

        /// <summary>
        /// 采购单价 MB011
        /// </summary>
        public string PurchaseUnit { get; set; }

        /// <summary>
        /// 含税 Y/N MB013
        /// </summary>
        public string TaxInclusive { get; set; }
    }
}