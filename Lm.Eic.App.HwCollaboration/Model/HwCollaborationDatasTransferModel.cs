using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.HwCollaboration.Model
{
    /// <summary>
    ///华为协同数据传输实体模型
    /// </summary>
    [Serializable]
    public partial class HwCollaborationDataTransferModel
    {
        public HwCollaborationDataTransferModel()
        { }
        #region Model
        private string _opmodule;
        /// <summary>
        ///操作模块
        /// </summary>
        public string OpModule
        {
            set { _opmodule = value; }
            get { return _opmodule; }
        }
        private string _opcontent;
        /// <summary>
        ///操作内容
        /// </summary>
        public string OpContent
        {
            set { _opcontent = value; }
            get { return _opcontent; }
        }
        private string _oplog;
        /// <summary>
        ///操作日志
        /// </summary>
        public string OpLog
        {
            set { _oplog = value; }
            get { return _oplog; }
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

    /// <summary>
    ///华为协同数据配置模型
    /// </summary>
    [Serializable]
    public partial class HwCollaborationDataConfigModel
    {
        public HwCollaborationDataConfigModel()
        { }
        #region Model
        private string _materialid;
        /// <summary>
        ///物料料号
        /// </summary>
        public string MaterialId
        {
            set { _materialid = value; }
            get { return _materialid; }
        }
        private string _materialbasedatacontent;
        /// <summary>
        ///物料基础配置内容
        /// </summary>
        public string MaterialBaseDataContent
        {
            set { _materialbasedatacontent = value; }
            get { return _materialbasedatacontent; }
        }
        private string _materialbomdatacontent;
        /// <summary>
        ///物料Bom配置信息
        /// </summary>
        public string MaterialBomDataContent
        {
            set { _materialbomdatacontent = value; }
            get { return _materialbomdatacontent; }
        }
        private string _inventorytype;
        /// <summary>
        ///物料类别
        /// </summary>
        public string InventoryType
        {
            set { _inventorytype = value; }
            get { return _inventorytype; }
        }
        private int _datastatus;
        /// <summary>
        ///数据状态
        /// </summary>
        public int DataStatus
        {
            set { _datastatus = value; }
            get { return _datastatus; }
        }
        private string _oplog;
        /// <summary>
        ///操作日志
        /// </summary>
        public string OpLog
        {
            set { _oplog = value; }
            get { return _oplog; }
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
