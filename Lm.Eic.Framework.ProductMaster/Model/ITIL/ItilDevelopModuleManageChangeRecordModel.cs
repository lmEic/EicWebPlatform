using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Model.ITIL
{
    /// <summary>
    ///开发模块管理变更记录模型
    /// </summary>
    [Serializable]
    public partial class ItilDevelopModuleManageChangeRecordModel
    {
        public ItilDevelopModuleManageChangeRecordModel()
        { }
        #region Model
        private string _executor;
        /// <summary>
        ///执行人
        /// </summary>
        public string Executor
        {
            set { _executor = value; }
            get { return _executor; }
        }
        private string _modulename;
        /// <summary>
        ///模块名称
        /// </summary>
        public string ModuleName
        {
            set { _modulename = value; }
            get { return _modulename; }
        }
        private string _mclassname;
        /// <summary>
        ///类名称
        /// </summary>
        public string MClassName
        {
            set { _mclassname = value; }
            get { return _mclassname; }
        }
        private string _mfunctionname;
        /// <summary>
        ///函数名称
        /// </summary>
        public string MFunctionName
        {
            set { _mfunctionname = value; }
            get { return _mfunctionname; }
        }
        private string _functiondescription;
        /// <summary>
        ///功能描述
        /// </summary>
        public string FunctionDescription
        {
            set { _functiondescription = value; }
            get { return _functiondescription; }
        }
        private string _changeprogress;
        /// <summary>
        ///开发进度
        /// </summary>
        public string ChangeProgress
        {
            set { _changeprogress = value; }
            get { return _changeprogress; }
        }
        private string _executor;
        /// <summary>
        ///执行人
        /// </summary>
        public string Executor
        {
            set { _executor = value; }
            get { return _executor; }
        }
        private string _parameterkey;
        /// <summary>
        ///模块名&类名&方法名
        /// </summary>
        public string ParameterKey
        {
            set { _parameterkey = value; }
            get { return _parameterkey; }
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
        ///自增减
        /// </summary>
        public decimal Id_Key
        {
            set { _id_key = value; }
            get { return _id_key; }
        }
        #endregion Model
    }

}
