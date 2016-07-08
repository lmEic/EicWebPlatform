namespace Lm.Eic.App.Erp.Domain.PurchaseManage
{
    /// <summary>
    /// 采购单单头信息模型
    /// PURTC：采购单单头信息档
    /// </summary>
    public class PurchaseHeaderModel : PurchaseModelBase
    {
        /// <summary>
        /// 采购单号 TC001+TC002
        /// </summary>
        public string PurchaseID { get { return ID; } }

        /// <summary>
        /// 采购日期 TC003
        /// </summary>
        public string PurchaseDate { get; set; }

        /// <summary>
        /// 供应商 TC004
        /// </summary>
        public string SupplierID { get; set; }

        /// <summary>
        /// 采购人员 TC011
        /// </summary>
        public string PurchasePerson { get; set; }

        /// <summary>
        /// 采购金额 TC019
        /// </summary>
        public double PurchaseAmount { get; set; }
    }

    /// <summary>
    /// 采购单单身信息模型
    /// PURTD:采购单单身信息档
    /// </summary>
    public class PurchaseBodyModel : PurchaseModelBase
    {
        /// <summary>
        /// 采购单号 TD001+TD002
        /// </summary>
        public string PurchaseID { get { return ID; } }

        /// <summary>
        /// 品号 TD004
        /// </summary>
        public string ProductID { get; set; }

        /// <summary>
        /// 品名 TD005
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 规格 TD006
        /// </summary>
        public string ProductSpecify { get; set; }

        /// <summary>
        /// 仓库 TD007
        /// </summary>
        public string Warehouse { get; set; }

        /// <summary>
        /// 采购单价 TD010
        /// </summary>
        public string PurchaseUnit { get; set; }

        /// <summary>
        /// 采购金额 TD011
        /// </summary>
        public string PurchaseAmmount { get; set; }

        /// <summary>
        /// 采购数量 TD008
        /// </summary>
        public double PurchaseCount { get; set; }

        /// <summary>
        /// 预交货日 TD012
        /// </summary>
        public string PlanDeliverDate { get; set; }

        /// <summary>
        /// 已交数量 TD015
        /// </summary>
        public double DeliveredCount { get; set; }

        /// <summary>
        /// 库存数量 TD019
        /// </summary>
        public double InventoryCount { get; set; }

        /// <summary>
        /// 请购单号 TD026+TD027
        /// </summary>
        public string BuyingID { get; set; }
    }
}