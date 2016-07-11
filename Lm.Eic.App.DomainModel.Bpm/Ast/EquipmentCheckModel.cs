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

        private DateTime _checkdata;

        /// <summary>
        ///校验日期
        /// </summary>
        public DateTime CheckData
        {
            set { _checkdata = value; }
            get { return _checkdata; }
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
        ///校验人
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