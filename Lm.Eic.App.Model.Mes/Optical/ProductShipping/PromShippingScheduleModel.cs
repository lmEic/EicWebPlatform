using System;

namespace Lm.Eic.App.DomainModel.Mes.Optical.ProductShipping
{
    /// <summary>
    /// Prom_ShippingSchedule:实体类(属性说明自动提取数据库字段的描述信息)
    /// 出货排程模型
    /// </summary>
    [Serializable]
    public partial class PromShippingScheduleModel
    {
        public PromShippingScheduleModel()
        { }

        #region Model

        /// <summary>
        ///
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ProductCatalog { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime ShippingDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double ShippingCount { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ShippingStatus { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ShippingType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int AnalogMonth { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int? AnalogYear { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal Id_Key { get; set; }

        #endregion Model
    }
}