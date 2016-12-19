using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport
{
    /// <summary>
    /// 制三部日报表模型
    /// </summary>
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


    /// <summary>
    /// WIP完工录入流程卡模型
    /// </summary>
    public class WipProductCompleteInputDataModel
    {
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
        private string _classtype;
        /// <summary>
        ///班别
        /// </summary>
        public string ClassType
        {
            set { _classtype = value; }
            get { return _classtype; }
        }
        private DateTime _productdate;
        /// <summary>
        ///生产日期
        /// </summary>
        public DateTime ProductDate
        {
            set { _productdate = value; }
            get { return _productdate; }
        }
        private string _productstatus;
        /// <summary>
        ///生产状态
        /// </summary>
        public string ProductStatus
        {
            set { _productstatus = value; }
            get { return _productstatus; }
        }
        private string _producttype;
        /// <summary>
        ///生产型号
        /// </summary>
        public string ProductType
        {
            set { _producttype = value; }
            get { return _producttype; }
        }
        private string _productbigstation;
        /// <summary>
        ///生产大站别
        /// </summary>
        public string ProductBigStation
        {
            set { _productbigstation = value; }
            get { return _productbigstation; }
        }
        private string _productstation;
        /// <summary>
        ///生产站别
        /// </summary>
        public string ProductStation
        {
            set { _productstation = value; }
            get { return _productstation; }
        }
        private string _workerid;
        /// <summary>
        ///工号
        /// </summary>
        public string WorkerID
        {
            set { _workerid = value; }
            get { return _workerid; }
        }
        private string _workername;
        /// <summary>
        ///姓名
        /// </summary>
        public string WorkerName
        {
            set { _workername = value; }
            get { return _workername; }
        }
        private string _indepartment;
        /// <summary>
        ///录入部门
        /// </summary>
        public string InDepartment
        {
            set { _indepartment = value; }
            get { return _indepartment; }
        }
        private string _flowcardid;
        /// <summary>
        ///流程卡号
        /// </summary>
        public string FlowCardID
        {
            set { _flowcardid = value; }
            get { return _flowcardid; }
        }
        private int _productcount;
        /// <summary>
        ///生产数量
        /// </summary>
        public int ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        private int _goodcount;
        /// <summary>
        ///良品数量
        /// </summary>
        public int GoodCount
        {
            set { _goodcount = value; }
            get { return _goodcount; }
        }
        private int _badcount;
        /// <summary>
        ///不良数量
        /// </summary>
        public int BadCount
        {
            set { _badcount = value; }
            get { return _badcount; }
        }
        private string _materiallotnumname;
        /// <summary>
        ///物料编号名称
        /// </summary>
        public string MaterialLotNumName
        {
            set { _materiallotnumname = value; }
            get { return _materiallotnumname; }
        }
        private string _materiallotnum;
        /// <summary>
        ///物料编号
        /// </summary>
        public string MaterialLotNum
        {
            set { _materiallotnum = value; }
            get { return _materiallotnum; }
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
        private DateTime _inputtime;
        /// <summary>
        ///录入时间
        /// </summary>
        public DateTime InputTime
        {
            set { _inputtime = value; }
            get { return _inputtime; }
        }
        private string _field1;
        /// <summary>
        ///Field1
        /// </summary>
        public string Field1
        {
            set { _field1 = value; }
            get { return _field1; }
        }
        private string _field2;
        /// <summary>
        ///Field2
        /// </summary>
        public string Field2
        {
            set { _field2 = value; }
            get { return _field2; }
        }
        private string _field3;
        /// <summary>
        ///Field3
        /// </summary>
        public string Field3
        {
            set { _field3 = value; }
            get { return _field3; }
        }
        private string _field4;
        /// <summary>
        ///上站站名
        /// </summary>
        public string Field4
        {
            set { _field4 = value; }
            get { return _field4; }
        }
        private string _field5;
        /// <summary>
        ///下站站名
        /// </summary>
        public string Field5
        {
            set { _field5 = value; }
            get { return _field5; }
        }
        private decimal _id_key;
        /// <summary>
        ///自增建
        /// </summary>
        public decimal Id_Key
        {
            set { _id_key = value; }
            get { return _id_key; }
        }
        #endregion Model
    }


    /// <summary>
    /// 标准工时表
    /// </summary>
    public class WipProductPUH
    {
        #region Model
        private string _bigproductstation;
        /// <summary>
        ///大站站别
        /// </summary>
        public string BigProductStation
        {
            set { _bigproductstation = value; }
            get { return _bigproductstation; }
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
        private string _productcatalog;
        /// <summary>
        ///产品类型
        /// </summary>
        public string ProductCatalog
        {
            set { _productcatalog = value; }
            get { return _productcatalog; }
        }
        private double _upscount;
        /// <summary>
        ///秒/颗
        /// </summary>
        public double UpsCount
        {
            set { _upscount = value; }
            get { return _upscount; }
        }
        private double _uphcount;
        /// <summary>
        ///颗/时
        /// </summary>
        public double UphCount
        {
            set { _uphcount = value; }
            get { return _uphcount; }
        }
        private DateTime _updatedate;
        /// <summary>
        ///上传日期
        /// </summary>
        public DateTime UpdateDate
        {
            set { _updatedate = value; }
            get { return _updatedate; }
        }
        private string _productcommonstation;
        /// <summary>
        ///生产通用站别
        /// </summary>
        public string ProductCommonStation
        {
            set { _productcommonstation = value; }
            get { return _productcommonstation; }
        }
        private int _idlevel;
        /// <summary>
        ///ID级别
        /// </summary>
        public int IdLevel
        {
            set { _idlevel = value; }
            get { return _idlevel; }
        }
        private string _stationkeysign;
        /// <summary>
        ///站别的关建键
        /// </summary>
        public string StationKeySign
        {
            set { _stationkeysign = value; }
            get { return _stationkeysign; }
        }
        private int _planproductcount;
        /// <summary>
        ///计划产出
        /// </summary>
        public int PlanProductCount
        {
            set { _planproductcount = value; }
            get { return _planproductcount; }
        }
        private double _productcycle;
        /// <summary>
        ///生产周期
        /// </summary>
        public double ProductCycle
        {
            set { _productcycle = value; }
            get { return _productcycle; }
        }
        private string _productstationmapping;
        /// <summary>
        ///生产站别导航
        /// </summary>
        public string ProductStationMapping
        {
            set { _productstationmapping = value; }
            get { return _productstationmapping; }
        }
        private int _limitinputtimes;
        /// <summary>
        ///限制时间
        /// </summary>
        public int LimitInputTimes
        {
            set { _limitinputtimes = value; }
            get { return _limitinputtimes; }
        }
        private string _productstationexclude;
        /// <summary>
        ///站别的扩展名
        /// </summary>
        public string ProductStationExclude
        {
            set { _productstationexclude = value; }
            get { return _productstationexclude; }
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
