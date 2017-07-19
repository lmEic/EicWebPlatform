using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Model.Tools
{
    [Serializable]
  public partial  class ReportImproveProblemModels
    {
        public ReportImproveProblemModels() { }
        private string _caseId;
        /// <summary>
        /// 编号
        /// </summary>
        public string CaseId
        {
            set { _caseId = value; }
            get { return _caseId; }
        }

        private int _caseIdYear;
        public int CaseIdYear
        {
            set { _caseIdYear = value; }
            get{ return _caseIdYear; }
        }
        private string _workerId;
        /// <summary>
        /// 工号
        /// </summary>
        public string WorkerId
        {
            set { _workerId = value; }
            get { return _workerId; }
        }
        private string _name;
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        private string _department;
        /// <summary>
        /// 部门
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        private string _systemName;
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName
        {
            set { _systemName = value; }
            get { return _systemName; }

        }
        private string _modeuleName;
        /// <summary>
        /// 模块类别
        /// </summary>
        public string ModuleName
        {
            set { _modeuleName = value; }
            get { return _modeuleName; }
        }
        private DateTime _problemDate;
        /// <summary>
        /// 提报日期
        /// </summary>
        public DateTime ProblemDate
        {
            set { _problemDate = value; }
            get { return _problemDate; }
        }
        private string _problemDesc;
        /// <summary>
        /// 问题描述
        /// </summary>
        public string ProblemDesc
        {
            set { _problemDesc = value; }
            get { return _problemDesc; }
        }
        private string problemSolve;
        /// <summary>
        /// 问题状态
        /// </summary>
        public string ProblemSolve
        {
            set { problemSolve = value; }
            get { return problemSolve; }
        }
        private string _problemAttach;
        /// <summary>
        /// 上传附件
        /// </summary>
        public string ProblemAttach
        {
            set { _problemAttach = value; }
            get { return _problemAttach; }
        }
        private string _problemDegree;
        /// <summary>
        /// 问题性质
        /// </summary>
        public string ProblemDegree
        {
            set { _problemDegree = value; }
            get { return _problemDegree; }
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
        private string _problemSolveMethod;
        /// <summary>
        /// 问题解决方法
        /// </summary>
        public string ProblemSolveMethod
        {
            set { _problemSolveMethod = value; }
            get { return _problemSolveMethod; }
        }
        private string _problemProcess;
        /// <summary>
        /// 问题处理人员
        /// </summary>
        public string   ProblemProcess
        {
            set { _problemProcess = value; }
            get { return _problemProcess; }
        }

        private int _caseIdnumber=0;
        /// <summary>
        ///序号
        /// </summary>

        //public int CaseIdNumber 
        //{
        //    set { _caseIdnumber = value; }
        //    get { return _caseIdnumber; }
        //}
        //private string _problemBussesesNumberStr;
        ///// <summary>
        /////处理问题序号
        ///// </summary>
        //public string ProblemBussesesNumberStr
        //{
        //    set { _problemBussesesNumberStr = value; }
        //    get { return _problemBussesesNumberStr; }
        //}

        //private string _filepath;
        ///// <summary>
        /////文档路径
        ///// </summary>
        //public string FilePath
        //{
        //    set { _filepath = value; }
        //    get { return _filepath; }
        //}
        //private string _filename;
        ///// <summary>
        /////文件名
        ///// </summary>
        //public string FileName
        //{
        //    set { _filename = value; }
        //    get { return _filename; }
        //}
        //private string _parameterkey;
        ///// <summary>
        /////关键字
        ///// </summary>
        //public string ParameterKey
        //{
        //    set { _parameterkey = value; }
        //    get { return _parameterkey; }
        //}
    }
}
