using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport
{
    /// <summary>
    /// 产品工艺概述模型
    /// </summary>
    public class ProductFlowOverviewModel
    {
        /// <summary>
        /// 产品品名
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 工序总数
        /// </summary>
        public int ProductFlowCount { get; set; }
     
        /// <summary>
        /// 总工时
        /// </summary>
        public double StandardHoursCount { get; set; }
    }


    /// <summary>
    ///产品工艺模型
    /// </summary>
    [Serializable]
    public partial class ProductFlowModel
    {
        public ProductFlowModel()
        { }
        #region Model
        private string _department;
        /// <summary>
        ///部门
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        private string _productname;
        /// <summary>
        ///产品品名
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        private int _productflowsign;
        /// <summary>
        ///工艺标示（0起始,1中间,2结尾）
        /// </summary>
        public int ProductFlowSign
        {
            set { _productflowsign = value; }
            get { return _productflowsign; }
        }
        private string _productflowid;
        /// <summary>
        ///工艺编号
        /// </summary>
        public string ProductFlowId
        {
            set { _productflowid = value; }
            get { return _productflowid; }
        }
        private string _productflowname;
        /// <summary>
        ///工艺名称
        /// </summary>
        public string ProductFlowName
        {
            set { _productflowname = value; }
            get { return _productflowname; }
        }

        public string _parameterKey;
        /// <summary>
        /// 标识键
        /// </summary>
        public string ParameterKey
        {
            set { _parameterKey = value; }
            get { return _parameterKey; }
        }
        private int _standardhourstype;
        /// <summary>
        ///标准工时类别(1s,2m,3pcs/h)
        /// </summary>
        public int StandardHoursType
        {
            set { _standardhourstype = value; }
            get { return _standardhourstype; }
        }
        private decimal _standardhours;
        /// <summary>
        ///标准工时
        /// </summary>
        public decimal StandardHours
        {
            set { _standardhours = value; }
            get { return _standardhours; }
        }
        private decimal _relaxcoefficient;
        /// <summary>
        ///狂放系数
        /// </summary>
        public decimal RelaxCoefficient
        {
            set { _relaxcoefficient = value; }
            get { return _relaxcoefficient; }
        }
        private int _minmachinecount;
        /// <summary>
        ///最小机台数
        /// </summary>
        public int MinMachineCount
        {
            set { _minmachinecount = value; }
            get { return _minmachinecount; }
        }
        private int _maxmachinecount;
        /// <summary>
        ///最大机台数
        /// </summary>
        public int MaxMachineCount
        {
            set { _maxmachinecount = value; }
            get { return _maxmachinecount; }
        }
        private decimal _difficultycoefficient;
        /// <summary>
        ///难度系数
        /// </summary>
        public decimal DifficultyCoefficient
        {
            set { _difficultycoefficient = value; }
            get { return _difficultycoefficient; }
        }
        private string _mouldid;
        /// <summary>
        ///模板编号
        /// </summary>
        public string MouldId
        {
            set { _mouldid = value; }
            get { return _mouldid; }
        }
        private string _mouldname;
        /// <summary>
        ///模板名称
        /// </summary>
        public string MouldName
        {
            set { _mouldname = value; }
            get { return _mouldname; }
        }
        private int _mouldcavitycount;
        /// <summary>
        ///模穴数
        /// </summary>
        public int MouldCavityCount
        {
            set { _mouldcavitycount = value; }
            get { return _mouldcavitycount; }
        }
        private string _remark;
        /// <summary>
        ///备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
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
