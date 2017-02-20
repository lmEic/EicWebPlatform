using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Pms.BoardManagment
{
    /// <summary>
    ///物料规格看板
    /// </summary>
    [Serializable]
    public partial class MaterialSpecBoardModel
    {
        public MaterialSpecBoardModel()
        { }
        #region Model
        private string _productid;
        /// <summary>
        ///产品品号
        /// </summary>
        public string ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        private string _materialid;
        /// <summary>
        ///物料品号
        /// </summary>
        public string MaterialID
        {
            set { _materialid = value; }
            get { return _materialid; }
        }
        private string _documenturl;
        /// <summary>
        ///文档Url
        /// </summary>
        public string DocumentUrl
        {
            set { _documenturl = value; }
            get { return _documenturl; }
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
        private string _department;
        /// <summary>
        ///部门
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        private string _state;
        /// <summary>
        ///状态
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
        }
        private string _remarks;
        /// <summary>
        ///备注
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
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
        ///操作标示
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
