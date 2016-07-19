namespace Lm.Eic.App.Erp.Domain.PurchaseManage
{
    /// <summary>
    /// 进货单头信息模型
    /// PURTG:进货单单头档
    /// </summary>
    public class StockHeaderModel : PurchaseModelBase
    {
        /// <summary>
        /// 进货单号TG001+TG002
        /// </summary>
        public string StockID { get { return ID; } }

        /// <summary>
        /// 进货日期:TG003
        /// </summary>
        public string StockDate { get; set; }

        /// <summary>
        /// 供应商 TG005
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 单据日期 TG014
        /// </summary>
        public string BillsDate { get; set; }

        /// <summary>
        /// 进货金额 TG017
        /// </summary>
        public double StockAmount { get; set; }

        /// <summary>
        /// 进货费用 TG020
        /// </summary>
        public double StockCost { get; set; }

        /// <summary>
        /// 供应商全称 TG021
        /// </summary>
        public string SupplierFullName { get; set; }

        /// <summary>
        /// 数量合计 TG026
        /// </summary>
        public double TotalCount { get; set; }

        /// <summary>
        /// 采购单号 TG034+TG035
        /// ERP中现在没有用
        /// </summary>
        public string PurchaseID { get; set; }
    }

    /// <summary>
    /// 进货单单身信息模型
    /// PURTH:进货单单身档
    /// </summary>
    public class StockBodyModel : PurchaseModelBase
    {
        /// <summary>
        /// 进货单号TH001+TH002
        /// </summary>
        public string StockID { get { return ID; } }

        /// <summary>
        /// 进货数量 TH007
        /// </summary>
        public long StockCount { get; set; }

        /// <summary>
        /// 品号 TH004
        /// </summary>
        public string ProductID { get; set; }

        /// <summary>
        /// 品名 TH005
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 规格 TH006
        /// </summary>
        public string ProductSpecify { get; set; }

        /// <summary>
        /// 仓库 TH009
        /// </summary>
        public string Warehouse { get; set; }

        /// <summary>
        /// 采购单号 TH011+TH012
        /// </summary>
        public string PurchaseID { get; set; }

        /// <summary>
        /// 原币单位进价TH018
        /// </summary>
        public double StockUnit { get; set; }

        /// <summary>
        /// 原币进货金额 TH019
        /// </summary>
        public double StockAmount { get; set; }

        /// <summary>
        /// 验收日期 TH014
        /// </summary>
        public string CheckDate { get; set; }

        /// <summary>
        /// 验收数量 TH015
        /// </summary>
        public double CheckCount { get; set; }

        /// <summary>
        /// 备注 TH033
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 库存数量 TH034
        /// </summary>
        public double InventoryCount { get; set; }

        /// <summary>
        /// 审核者 TH038
        /// </summary>
        public string Auditor { get; set; }
    }
}