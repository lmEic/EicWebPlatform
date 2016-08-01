using System;

namespace Lm.Eic.App.DomainModel.Bpm.Ast
{
    /// <summary>
    ///设备校验
    /// </summary>
    [Serializable]
    public partial class EquipmentCheckModel
    {
        public EquipmentCheckModel()
        { }

        #region Model
        private string _assetnumber;
        /// <summary>
        ///财产编号
        /// </summary>
        public string AssetNumber
        {
            set { _assetnumber = value; }
            get { return _assetnumber; }
        }
        private string _equipmentname;
        /// <summary>
        ///设备名称
        /// </summary>
        public string EquipmentName
        {
            set { _equipmentname = value; }
            get { return _equipmentname; }
        }
        private DateTime _checkdate;
        /// <summary>
        ///校验日期
        /// </summary>
        public DateTime CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        private string _checkworkerid;
        /// <summary>
        ///校验人工号
        /// </summary>
        public string CheckWorkerID
        {
            set { _checkworkerid = value; }
            get { return _checkworkerid; }
        }
        private string _checkuser;
        /// <summary>
        ///校验人姓名
        /// </summary>
        public string CheckUser
        {
            set { _checkuser = value; }
            get { return _checkuser; }
        }
        private string _checkresult;
        /// <summary>
        ///校验结果
        /// </summary>
        public string CheckResult
        {
            set { _checkresult = value; }
            get { return _checkresult; }
        }
        private string _verifyuser;
        /// <summary>
        ///确认人
        /// </summary>
        public string VerifyUser
        {
            set { _verifyuser = value; }
            get { return _verifyuser; }
        }
        private string _documentpath;
        /// <summary>
        ///文档路径
        /// </summary>
        public string DocumentPath
        {
            set { _documentpath = value; }
            get { return _documentpath; }
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
        private string _opperson;
        /// <summary>
        ///操作人
        /// </summary>
        public string OpPerson
        {
            set { _opperson = value; }
            get { return _opperson; }
        }
        private string _opsign;
        /// <summary>
        ///操作标志
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