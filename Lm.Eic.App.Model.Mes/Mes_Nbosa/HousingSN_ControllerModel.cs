using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Mes.Mes_Nbosa
{
    /// <summary>
    ///生产条码管控
    /// </summary>
    [Serializable]
    public partial class HousingSN_ControllerModel
    {
        public HousingSN_ControllerModel()
        { }
        #region Model
        private string _producttype;
        /// <summary>
        ///对内型号
        /// </summary>
        public string ProductType
        {
            set { _producttype = value; }
            get { return _producttype; }
        }
        private string _producttypecommon;
        /// <summary>
        ///对外型号
        /// </summary>
        public string ProductTypeCommon
        {
            set { _producttypecommon = value; }
            get { return _producttypecommon; }
        }
        private string _materialid;
        /// <summary>
        ///物料料号
        /// </summary>
        public string MaterialID
        {
            set { _materialid = value; }
            get { return _materialid; }
        }
        private string _startsn;
        /// <summary>
        ///开始SN
        /// </summary>
        public string StartSN
        {
            set { _startsn = value; }
            get { return _startsn; }
        }
        private string _endsn;
        /// <summary>
        ///结束SN
        /// </summary>
        public string EndSN
        {
            set { _endsn = value; }
            get { return _endsn; }
        }
        private int _isengraved;
        /// <summary>
        ///是否雕刻
        /// </summary>
        public int IsEngraved
        {
            set { _isengraved = value; }
            get { return _isengraved; }
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
        private string _opperson;
        /// <summary>
        ///操作人
        /// </summary>
        public string OpPerson
        {
            set { _opperson = value; }
            get { return _opperson; }
        }
        private DateTime _optime;
        /// <summary>
        ///操作时间
        /// </summary>
        public DateTime OpTime
        {
            set { _optime = value; }
            get { return _optime; }
        }
        private int _iscompute;
        /// <summary>
        ///是否启用
        /// </summary>
        public int IsCompute
        {
            set { _iscompute = value; }
            get { return _iscompute; }
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
