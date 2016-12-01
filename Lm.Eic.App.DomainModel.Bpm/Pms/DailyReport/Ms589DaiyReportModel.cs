using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport
{
    public class Ms589DaiyReportModel
    {
        public Ms589DaiyReportModel()
        { }

        #region Model
        private string _departmentname;
        /// <summary>
        ///部门
        /// </summary>
        public string DepartmentName
        {
            set { _departmentname = value; }
            get { return _departmentname; }
        }
        private string _typecatalog;
        /// <summary>
        ///类型
        /// </summary>
        public string TypeCatalog
        {
            set { _typecatalog = value; }
            get { return _typecatalog; }
        }
        private string _productstation;
        /// <summary>
        ///站别
        /// </summary>
        public string ProductStation
        {
            set { _productstation = value; }
            get { return _productstation; }
        }
        private string _productdate;
        /// <summary>
        ///生产日期
        /// </summary>
        public string ProductDate
        {
            set { _productdate = value; }
            get { return _productdate; }
        }
        private string _classtype;
        /// <summary>
        ///班别
        /// </summary>
        public string ClassType
        {
            set { _classtype = value; }
            get { return _classtype; }
        }
        private string _producttype;
        /// <summary>
        ///生产类型
        /// </summary>
        public string ProductType
        {
            set { _producttype = value; }
            get { return _producttype; }
        }
        private int _productcount;
        /// <summary>
        ///产量
        /// </summary>
        public int ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        private int _reworkcount;
        /// <summary>
        ///重工数量
        /// </summary>
        public int reworkcount
        {
            set { _reworkcount = value; }
            get { return _reworkcount; }
        }
        private int _badcount;
        /// <summary>
        ///不良数
        /// </summary>
        public int BadCount
        {
            set { _badcount = value; }
            get { return _badcount; }
        }
        private double _devotionhours;
        /// <summary>
        ///标准工时
        /// </summary>
        public double DevotionHours
        {
            set { _devotionhours = value; }
            get { return _devotionhours; }
        }
        private double _producthours;
        /// <summary>
        ///生产时数
        /// </summary>
        public double ProductHours
        {
            set { _producthours = value; }
            get { return _producthours; }
        }
        private double _unproducthours;
        /// <summary>
        ///非生产时数
        /// </summary>
        public double UnProductHours
        {
            set { _unproducthours = value; }
            get { return _unproducthours; }
        }
        private string _memo;
        /// <summary>
        ///备注
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
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
