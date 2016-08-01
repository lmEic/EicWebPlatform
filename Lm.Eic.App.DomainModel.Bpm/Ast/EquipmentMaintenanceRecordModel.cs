using System;
namespace Lm.Eic.App.DomainModel.Bpm.Ast
{
    /// <summary>
    ///设备保养记录模型
    /// </summary>
    [Serializable]
    public partial class EquipmentMaintenanceRecordModel
    {
        public EquipmentMaintenanceRecordModel()
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
        private DateTime _maintenancedate;
        /// <summary>
        ///保养日期
        /// </summary>
        public DateTime MaintenanceDate
        {
            set { _maintenancedate = value; }
            get { return _maintenancedate; }
        }
        private string _maintenanceworkerid;
        /// <summary>
        ///保养人工号
        /// </summary>
        public string MaintenanceWorkerID
        {
            set { _maintenanceworkerid = value; }
            get { return _maintenanceworkerid; }
        }
        private string _maintenanceuser;
        /// <summary>
        ///保养人姓名
        /// </summary>
        public string MaintenanceUser
        {
            set { _maintenanceuser = value; }
            get { return _maintenanceuser; }
        }
        private string _maintenanceresult;
        /// <summary>
        ///保养结果
        /// </summary>
        public string MaintenanceResult
        {
            set { _maintenanceresult = value; }
            get { return _maintenanceresult; }
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
        ///操作标识
        /// </summary>
        public string OpSign
        {
            set { _opsign = value; }
            get { return _opsign; }
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



