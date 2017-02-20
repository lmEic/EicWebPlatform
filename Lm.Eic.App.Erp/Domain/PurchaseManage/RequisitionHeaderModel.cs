namespace Lm.Eic.App.Erp.Domain.PurchaseManage
{
    /// <summary>
    /// 请购单信息模型
    /// PURTA:请购单单头信息档
    /// </summary>
    public class RequisitionHeaderModel : PurchaseModelBase
    {
        /// <summary>
        /// 请购单号 TA001+TA002
        /// </summary>
        public string BuyingID { get { return ID; } }

        /// <summary>
        /// 请购日期 TA003
        /// </summary>
        public string BuyingDate { get; set; }

        /// <summary>
        /// 请购部门 TA004
        /// </summary>
        public string BuyFromDepartment { get; set; }

        /// <summary>
        /// 数量合计 TA011
        /// </summary>
        public string TotalCount { get; set; }

        /// <summary>
        /// 请购人员 TA012
        /// </summary>
        public string BuyingPerson { get; set; }

        /// <summary>
        /// 单据日期 TA013
        /// </summary>
        public string RequisitionDate { get; set; }

        /// <summary>
        /// 审核人 TA014
        /// </summary>
        public string Auditor { get; set; }

        /// <summary>
        /// 备注 TA006
        /// </summary>
        public string Memo { get; set; }
    }

    /// <summary>
    /// 请购单明细模型
    /// PURTB:请购单单身信息档
    /// </summary>
    public class RequisitionBodyModel : PurchaseModelBase
    {
        /// <summary>
        /// 请购单号 TB001+TB002
        /// </summary>
        public string BuyingID { get { return ID; } }

        /// <summary>
        /// 品号 TB004
        /// </summary>
        public string ProductID { get; set; }

        /// <summary>
        /// 品名 TB005
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 规格 TB006
        /// </summary>
        public string ProductSpecify { get; set; }

        /// <summary>
        /// 仓库 TB008
        /// </summary>
        public string Warehouse { get; set; }

        /// <summary>
        /// 请购数量 TB009
        /// </summary>
        public double RequisitionCount { get; set; }

        /// <summary>
        /// 供应商 TB010
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 采购单号 TB022
        /// </summary>
        public string PurchaseID { get; set; }

        /// <summary>
        /// 采购数量 TB014
        /// </summary>
        public double PurchaseCount { get; set; }

        /// <summary>
        /// 采购人员 TB016
        /// </summary>
        public string PurchasePerson { get; set; }

        /// <summary>
        /// 采购单价 TB017
        /// </summary>
        public string PurchaseUnit { get; set; }

        /// <summary>
        /// 采购金额 TB018
        /// </summary>
        public string PurchaseAmmount { get; set; }

        /// <summary>
        /// 备注TB024
        /// </summary>
        public string Memo { get; set; }
    }
}