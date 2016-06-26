using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Model.ITIL
{
    /// <summary>
    ///厂商联系信息模型
    /// </summary>
    [Serializable]
    public partial class ItilSupplierTelModel
    {
        public ItilSupplierTelModel()
        { }
        #region Model
        private string _suppliername;
        /// <summary>
        ///厂商名称
        /// </summary>
        public string SupplierName
        {
            set { _suppliername = value; }
            get { return _suppliername; }
        }
        private string _bussinesscontent;
        /// <summary>
        ///往来业务
        /// </summary>
        public string BussinessContent
        {
            set { _bussinesscontent = value; }
            get { return _bussinesscontent; }
        }
        private string _bussinesstype;
        /// <summary>
        ///业务类别
        /// </summary>
        public string BussinessType
        {
            set { _bussinesstype = value; }
            get { return _bussinesstype; }
        }
        private string _address;
        /// <summary>
        ///地址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        private string _tel;
        /// <summary>
        ///联系电话
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        private string _qq;
        /// <summary>
        ///QQ
        /// </summary>
        public string QQ
        {
            set { _qq = value; }
            get { return _qq; }
        }
        private string _order;
        /// <summary>
        ///联系人
        /// </summary>
        public string Order
        {
            set { _order = value; }
            get { return _order; }
        }
        private string _memo;
        /// <summary>
        ///备注
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        private decimal _id_key;
        /// <summary>
        ///自增键
        /// </summary>
        public decimal Id_Key
        {
            set { _id_key = value; }
            get { return _id_key; }
        }
        #endregion Model
    }
}
