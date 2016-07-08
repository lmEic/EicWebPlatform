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

        private string _producttype;
        private string _productcatalog;
        private DateTime _shippingdate;
        private double _shippingcount;
        private string _shippingstatus;
        private string _memo;
        private string _shippingtype;
        private int _analogmonth;
        private int? _analogyear;
        private decimal _id_key;

        /// <summary>
        ///
        /// </summary>
        public string ProductType
        {
            set { _producttype = value; }
            get { return _producttype; }
        }

        /// <summary>
        ///
        /// </summary>
        public string ProductCatalog
        {
            set { _productcatalog = value; }
            get { return _productcatalog; }
        }

        /// <summary>
        ///
        /// </summary>
        public DateTime ShippingDate
        {
            set { _shippingdate = value; }
            get { return _shippingdate; }
        }

        /// <summary>
        ///
        /// </summary>
        public double ShippingCount
        {
            set { _shippingcount = value; }
            get { return _shippingcount; }
        }

        /// <summary>
        ///
        /// </summary>
        public string ShippingStatus
        {
            set { _shippingstatus = value; }
            get { return _shippingstatus; }
        }

        /// <summary>
        ///
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }

        /// <summary>
        ///
        /// </summary>
        public string ShippingType
        {
            set { _shippingtype = value; }
            get { return _shippingtype; }
        }

        /// <summary>
        ///
        /// </summary>
        public int AnalogMonth
        {
            set { _analogmonth = value; }
            get { return _analogmonth; }
        }

        /// <summary>
        ///
        /// </summary>
        public int? AnalogYear
        {
            set { _analogyear = value; }
            get { return _analogyear; }
        }

        /// <summary>
        ///
        /// </summary>
        public decimal Id_Key
        {
            set { _id_key = value; }
            get { return _id_key; }
        }

        #endregion Model
    }
}