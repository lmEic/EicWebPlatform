using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.WorkFlow.GeneralForm
{
    /// <summary>
    ///表单签核流程模型
    /// </summary>
    [Serializable]
    public class WfFormCheckFlowModel
    {
        public WfFormCheckFlowModel()
        { }
        #region Model
        private string _formid;
        /// <summary>
        ///表单编号
        /// </summary>
        public string FormId
        {
            set { _formid = value; }
            get { return _formid; }
        }
        private string _formtext;
        /// <summary>
        ///表单名称
        /// </summary>
        public string FormText
        {
            set { _formtext = value; }
            get { return _formtext; }
        }
        private string _flowcheckstatus;
        /// <summary>
        ///流程签核状态
        /// </summary>
        public string FlowCheckStatus
        {
            set { _flowcheckstatus = value; }
            get { return _flowcheckstatus; }
        }
        private string _workerid;
        /// <summary>
        ///签核人工号
        /// </summary>
        public string WorkerId
        {
            set { _workerid = value; }
            get { return _workerid; }
        }
        private string _workername;
        /// <summary>
        ///签核人姓名
        /// </summary>
        public string WorkerName
        {
            set { _workername = value; }
            get { return _workername; }
        }
        private string _department;
        /// <summary>
        ///签核部门
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
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
