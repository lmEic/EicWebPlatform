using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Warehouse
{
    /// <summary>
    ///快递收发管理
    /// </summary>
    [Serializable]
    public partial class ExpressModel
    {
        public ExpressModel()
        { }
        #region Model
        private string _expressid;
        /// <summary>
        ///快递编号
        /// </summary>
        public string ExpressId
        {
            set { _expressid = value; }
            get { return _expressid; }
        }
        private string _expresscompany;
        /// <summary>
        ///快递公司名称
        /// </summary>
        public string ExpressCompany
        {
            set { _expresscompany = value; }
            get { return _expresscompany; }
        }
        private string _consignee;
        /// <summary>
        ///收货人
        /// </summary>
        public string Consignee
        {
            set { _consignee = value; }
            get { return _consignee; }
        }
        private DateTime _receptiondate;
        /// <summary>
        ///接受日期
        /// </summary>
        public DateTime ReceptionDate
        {
            set { _receptiondate = value; }
            get { return _receptiondate; }
        }
        private string _sendgoodscompanyaddress;
        /// <summary>
        ///接受地址
        /// </summary>
        public string SendGoodsCompanyAddress
        {
            set { _sendgoodscompanyaddress = value; }
            get { return _sendgoodscompanyaddress; }
        }
        private int _goodsnumber;
        /// <summary>
        ///物料数量
        /// </summary>
        public int GoodsNumber
        {
            set { _goodsnumber = value; }
            get { return _goodsnumber; }
        }
        private string _receiverworkerid;
        /// <summary>
        ///接受的人的工号
        /// </summary>
        public string ReceiverWorkerId
        {
            set { _receiverworkerid = value; }
            get { return _receiverworkerid; }
        }
        private string _receivername;
        /// <summary>
        ///接受人的姓名
        /// </summary>
        public string ReceiverName
        {
            set { _receivername = value; }
            get { return _receivername; }
        }
        private string _getgoodsperson;
        /// <summary>
        ///收货人
        /// </summary>
        public string GetGoodsPerson
        {
            set { _getgoodsperson = value; }
            get { return _getgoodsperson; }
        }
        private DateTime _getgoodsdate;
        /// <summary>
        ///收货日期
        /// </summary>
        public DateTime GetGoodsDate
        {
            set { _getgoodsdate = value; }
            get { return _getgoodsdate; }
        }
        private string _goodssstatus;
        /// <summary>
        ///收货状态
        /// </summary>
        public string GoodssStatus
        {
            set { _goodssstatus = value; }
            get { return _goodssstatus; }
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
        private DateTime _opdate;
        /// <summary>
        ///操作日期
        /// </summary>
        public DateTime OpDate
        {
            set { _opdate = value; }
            get { return _opdate; }
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
        private string _opsign;
        /// <summary>
        ///操作标识
        /// </summary>
        public string OpSign
        {
            set { _opsign = value; }
            get { return _opsign; }
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
