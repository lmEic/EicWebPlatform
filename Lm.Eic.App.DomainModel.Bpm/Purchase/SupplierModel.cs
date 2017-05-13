using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Purchase
{


    #region   映射到数据库模型

    /// <summary>
    /// 供应商合格证书
    /// SuppliersQualifiedCertificate
    /// </summary>
    public class SupplierQualifiedCertificateModel
    {
        #region Model
        private string _supplierid;
        /// <summary>
        ///供应商编号
        /// </summary>
        public string SupplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        private string _eligiblecertificate;
        /// <summary>
        ///合格证书
        /// </summary>
        public string EligibleCertificate
        {
            set { _eligiblecertificate = value; }
            get { return _eligiblecertificate; }
        }
        private DateTime _dateofcertificate;
        /// <summary>
        ///获证日期
        /// </summary>
        public DateTime DateOfCertificate
        {
            set { _dateofcertificate = value; }
            get { return _dateofcertificate; }
        }
        private string _isefficacy;
        /// <summary>
        ///是否有效
        /// </summary>
        public string IsEfficacy
        {
            set { _isefficacy = value; }
            get { return _isefficacy; }
        }
        private string _filepath;
        /// <summary>
        ///保存路径
        /// </summary>
        public string FilePath
        {
            set { _filepath = value; }
            get { return _filepath; }
        }
        private string _certificatefilename;
        /// <summary>
        ///证书文件名称
        /// </summary>
        public string CertificateFileName
        {
            set { _certificatefilename = value; }
            get { return _certificatefilename; }
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
        private string _opsign;
        /// <summary>
        ///操作标志
        /// </summary>
        public string OpSign
        {
            set { _opsign = value; }
            get { return _opsign; }
        }
        private decimal _id_Key;
        /// <summary>
        ///自增键
        /// </summary>
        public decimal Id_Key
        {
            set { _id_Key = value; }
            get { return _id_Key; }
        }
        #endregion Model
    }

    /// <summary>
    /// 供应商信息表
    /// SuppliersInfo
    /// </summary>
    public class SupplierInfoModel
    {
        #region Model
        private string _supplierid;
        /// <summary>
        ///供应商ID
        /// </summary>
        public string SupplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        private string _purchasetype;
        /// <summary>
        ///采购类别
        /// </summary>
        public string PurchaseType
        {
            set { _purchasetype = value; }
            get { return _purchasetype; }
        }
        private string _supplierproperty;
        /// <summary>
        ///供应商属性
        /// </summary>
        public string SupplierProperty
        {
            set { _supplierproperty = value; }
            get { return _supplierproperty; }
        }
        private string _suppliershortname;
        /// <summary>
        ///供应商简称
        /// </summary>
        public string SupplierShortName
        {
            set { _suppliershortname = value; }
            get { return _suppliershortname; }
        }
        private string _suppliername;
        /// <summary>
        ///供应商全称
        /// </summary>
        public string SupplierName
        {
            set { _suppliername = value; }
            get { return _suppliername; }
        }
        private string _purchaseuser;
        /// <summary>
        ///采购人员
        /// </summary>
        public string PurchaseUser
        {
            set { _purchaseuser = value; }
            get { return _purchaseuser; }
        }
        private string _suppliertel;
        /// <summary>
        ///供应商电话
        /// </summary>
        public string SupplierTel
        {
            set { _suppliertel = value; }
            get { return _suppliertel; }
        }
        private string _supplieruser;
        /// <summary>
        ///供应商联系人
        /// </summary>
        public string SupplierUser
        {
            set { _supplieruser = value; }
            get { return _supplieruser; }
        }
        private string _supplierfaxno;
        /// <summary>
        ///供应商传真
        /// </summary>
        public string SupplierFaxNo
        {
            set { _supplierfaxno = value; }
            get { return _supplierfaxno; }
        }
        private string _supplieremail;
        /// <summary>
        ///供应商邮箱
        /// </summary>
        public string SupplierEmail
        {
            set { _supplieremail = value; }
            get { return _supplieremail; }
        }
        private string _supplieraddress;
        /// <summary>
        ///供应商地址
        /// </summary>
        public string SupplierAddress
        {
            set { _supplieraddress = value; }
            get { return _supplieraddress; }
        }
        private string _supplierPrincipal;
        /// <summary>
        ///供应商负责人
        /// </summary>
        public string SupplierPrincipal
        {
            set { _supplierPrincipal = value; }
            get { return _supplierPrincipal; }
        }
        private string _paycondition;
        /// <summary>
        ///付款条件
        /// </summary>
        public string PayCondition
        {
            set { _paycondition = value; }
            get { return _paycondition; }
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
        ///操作标签
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
        private decimal _id_Key;
        /// <summary>
        ///自增建
        /// </summary>
        public decimal Id_Key
        {
            set { _id_Key = value; }
            get { return _id_Key; }
        }
        #endregion Model
    }
    /// <summary>
    /// 季度审查总览表
    /// SuppliersSeasonAuditTable
    /// </summary>
    public class SupplierSeasonAuditModel
    {
        #region Model
        private string _supplierid;
        /// <summary>
        ///供应商Id
        /// </summary>
        public string SupplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        private string _suppliershortname;
        /// <summary>
        ///供应商简称
        /// </summary>
        public string SupplierShortName
        {
            set { _suppliershortname = value; }
            get { return _suppliershortname; }
        }
        private string _suppliername;
        /// <summary>
        ///供应商名称名
        /// </summary>
        public string SupplierName
        {
            set { _suppliername = value; }
            get { return _suppliername; }
        }
        private double _qualitycheck;
        /// <summary>
        ///质量考核分
        /// </summary>
        public double QualityCheck
        {
            set { _qualitycheck = value; }
            get { return _qualitycheck; }
        }
        private double _auditprice;
        /// <summary>
        ///价格考核分
        /// </summary>
        public double AuditPrice
        {
            set { _auditprice = value; }
            get { return _auditprice; }
        }
        private double _deliverydate;
        /// <summary>
        ///交期考核分
        /// </summary>
        public double DeliveryDate
        {
            set { _deliverydate = value; }
            get { return _deliverydate; }
        }
        private double _actionliven;
        /// <summary>
        ///配合度考核分
        /// </summary>
        public double ActionLiven
        {
            set { _actionliven = value; }
            get { return _actionliven; }
        }
        private double _hsfgrade;
        /// <summary>
        ///HSF能力考核等级分
        /// </summary>
        public double HSFGrade
        {
            set { _hsfgrade = value; }
            get { return _hsfgrade; }
        }
        private double _totalcheckscore;
        /// <summary>
        ///考核总分
        /// </summary>
        public double TotalCheckScore
        {
            set { _totalcheckscore = value; }
            get { return _totalcheckscore; }
        }
        private string _checklevel;
        /// <summary>
        ///考核级别
        /// </summary>
        public string CheckLevel
        {
            set { _checklevel = value; }
            get { return _checklevel; }
        }
        private string _rewardsway;
        /// <summary>
        ///奖惩方式
        /// </summary>
        public string RewardsWay
        {
            set { _rewardsway = value; }
            get { return _rewardsway; }
        }
        private string _materialgrade;
        /// <summary>
        ///供应商风险等级
        /// </summary>
        public string MaterialGrade
        {
            set { _materialgrade = value; }
            get { return _materialgrade; }
        }
        private string _managerrisk;
        /// <summary>
        ///供应商管理风险
        /// </summary>
        public string ManagerRisk
        {
            set { _managerrisk = value; }
            get { return _managerrisk; }
        }
        private string _substitutionsupplierid;
        /// <summary>
        ///替代厂商
        /// </summary>
        public string SubstitutionSupplierId
        {
            set { _substitutionsupplierid = value; }
            get { return _substitutionsupplierid; }
        }
        private string _seasondatenum;
        /// <summary>
        ///第几季度
        /// </summary>
        public string SeasonDateNum
        {
            set { _seasondatenum = value; }
            get { return _seasondatenum; }
        }
        private string _parameterkey;
        /// <summary>
        ///关键字
        /// </summary>
        public string ParameterKey
        {
            set { _parameterkey = value; }
            get { return _parameterkey; }
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
        private string _oppserson;
        /// <summary>
        ///操作人
        /// </summary>
        public string OpPserson
        {
            set { _oppserson = value; }
            get { return _oppserson; }
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
        private string _opsign;
        /// <summary>
        ///操作标识
        /// </summary>
        public string OpSign
        {
            set { _opsign = value; }
            get { return _opsign; }
        }
        private decimal _id_Key;
        /// <summary>
        ///自增键
        /// </summary>
        public decimal Id_Key
        {
            set { _id_Key = value; }
            get { return _id_Key; }
        }
        #endregion Model
    }
    /// <summary>
    ///品质考核——实地辅导计划/执行表
    /// </summary>
    public class SupplierSeasonTutorModel
    {
        #region Model
        private string _supplierid;
        /// <summary>
        ///供应商Id
        /// </summary>
        public string SupplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        private string _suppilershortname;
        /// <summary>
        ///供应商简称
        /// </summary>
        public string SuppilerShortName
        {
            set { _suppilershortname = value; }
            get { return _suppilershortname; }
        }
        private string _suppliername;
        /// <summary>
        ///供应商名称
        /// </summary>
        public string SupplierName
        {
            set { _suppliername = value; }
            get { return _suppliername; }
        }
        private double _qualitycheck;
        /// <summary>
        ///质量考核
        /// </summary>
        public double QualityCheck
        {
            set { _qualitycheck = value; }
            get { return _qualitycheck; }
        }
        private double _auditprice;
        /// <summary>
        ///价格考核
        /// </summary>
        public double AuditPrice
        {
            set { _auditprice = value; }
            get { return _auditprice; }
        }
        private double _deliverydate;
        /// <summary>
        ///交期考核
        /// </summary>
        public double DeliveryDate
        {
            set { _deliverydate = value; }
            get { return _deliverydate; }
        }
        private double _actionliven;
        /// <summary>
        ///配合度考核
        /// </summary>
        public double ActionLiven
        {
            set { _actionliven = value; }
            get { return _actionliven; }
        }
        private double _hsfgrade;
        /// <summary>
        ///HSF能力考核等级
        /// </summary>
        public double HSFGrade
        {
            set { _hsfgrade = value; }
            get { return _hsfgrade; }
        }
        private double _totalcheckscore;
        /// <summary>
        ///考核总分
        /// </summary>
        public double TotalCheckScore
        {
            set { _totalcheckscore = value; }
            get { return _totalcheckscore; }
        }
        private string _checklevel;
        /// <summary>
        ///等级考核
        /// </summary>
        public string CheckLevel
        {
            set { _checklevel = value; }
            get { return _checklevel; }
        }
        private string _rewardsway;
        /// <summary>
        ///奖惩方式
        /// </summary>
        public string RewardsWay
        {
            set { _rewardsway = value; }
            get { return _rewardsway; }
        }
        private string _materialgrade;
        /// <summary>
        ///风险等级
        /// </summary>
        public string MaterialGrade
        {
            set { _materialgrade = value; }
            get { return _materialgrade; }
        }
        private string _managerrisk;
        /// <summary>
        ///不良比率
        /// </summary>
        public string ManagerRisk
        {
            set { _managerrisk = value; }
            get { return _managerrisk; }
        }
        private string _seasonnum;
        /// <summary>
        ///季度
        /// </summary>
        public string SeasonNum
        {
            set { _seasonnum = value; }
            get { return _seasonnum; }
        }
        private string _plantutordate;
        /// <summary>
        ///计划辅导日期
        /// </summary>
        public string PlanTutorDate
        {
            set { _plantutordate = value; }
            get { return _plantutordate; }
        }
        private string _plantutorcontent;
        /// <summary>
        ///计划辅导内容
        /// </summary>
        public string PlanTutorContent
        {
            set { _plantutorcontent = value; }
            get { return _plantutorcontent; }
        }
        private string _actiontutordate;
        /// <summary>
        ///实地辅导日期
        /// </summary>
        public string ActionTutorDate
        {
            set { _actiontutordate = value; }
            get { return _actiontutordate; }
        }
        private string _actiontutorcontent;
        /// <summary>
        ///实地辅导内容
        /// </summary>
        public string ActionTutorContent
        {
            set { _actiontutorcontent = value; }
            get { return _actiontutorcontent; }
        }
        private string _tutorresult;
        /// <summary>
        ///辅导结果
        /// </summary>
        public string TutorResult
        {
            set { _tutorresult = value; }
            get { return _tutorresult; }
        }
        private string _tutorcategory;
        /// <summary>
        ///辅导类别
        /// </summary>
        public string TutorCategory
        {
            set { _tutorcategory = value; }
            get { return _tutorcategory; }
        }
        private string _parameterkey;
        /// <summary>
        ///关键字
        /// </summary>
        public string ParameterKey
        {
            set { _parameterkey = value; }
            get { return _parameterkey; }
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
        private string _yearmonth;
        /// <summary>
        ///年月
        /// </summary>
        public string YearMonth
        {
            set { _yearmonth = value; }
            get { return _yearmonth; }
        }
        private string _oppserson;
        /// <summary>
        ///操作人
        /// </summary>
        public string OpPserson
        {
            set { _oppserson = value; }
            get { return _oppserson; }
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
        private string _opsign;
        /// <summary>
        ///操作标识
        /// </summary>
        public string OpSign
        {
            set { _opsign = value; }
            get { return _opsign; }
        }
        private decimal _id_Key;
        /// <summary>
        ///自增键
        /// </summary>
        public decimal Id_Key
        {
            set { _id_Key = value; }
            get { return _id_Key; }
        }
        #endregion Model
    }
    /// <summary>
    /// 供应商自评复评明细表 
    /// </summary>
    public class SupplierGradeInfoModel
    {
        #region Model
        private string _supplierid;
        /// <summary>
        ///供应商编号
        /// </summary>
        public string SupplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        private string _suppliername;
        /// <summary>
        ///供应商名称
        /// </summary>
        public string SupplierName
        {
            set { _suppliername = value; }
            get { return _suppliername; }
        }
        private string _supplierproperty;
        /// <summary>
        ///供应商类别
        /// </summary>
        public string SupplierProperty
        {
            set { _supplierproperty = value; }
            get { return _supplierproperty; }
        }
        private string _purchasetype;
        /// <summary>
        ///采购类别
        /// </summary>
        public string PurchaseType
        {
            set { _purchasetype = value; }
            get { return _purchasetype; }
        }
        private string _purchasematerial;
        /// <summary>
        ///采购料件
        /// </summary>
        public string PurchaseMaterial
        {
            set { _purchasematerial = value; }
            get { return _purchasematerial; }
        }
        private DateTime _lastpurchasedate;
        /// <summary>
        ///上次采购日期
        /// </summary>
        public DateTime LastPurchaseDate
        {
            set { _lastpurchasedate = value; }
            get { return _lastpurchasedate; }
        }
        private string _supgradetype;
        /// <summary>
        ///评分类别
        /// </summary>
        public string SupGradeType
        {
            set { _supgradetype = value; }
            get { return _supgradetype; }
        }
        private double _firstgradescore;
        /// <summary>
        ///首评分数
        /// </summary>
        public double FirstGradeScore
        {
            set { _firstgradescore = value; }
            get { return _firstgradescore; }
        }
        private DateTime _firstgradedate;
        /// <summary>
        ///首评日期
        /// </summary>
        public DateTime FirstGradeDate
        {
            set { _firstgradedate = value; }
            get { return _firstgradedate; }
        }
        private double _secondgradescore;
        /// <summary>
        ///复评分数
        /// </summary>
        public double SecondGradeScore
        {
            set { _secondgradescore = value; }
            get { return _secondgradescore; }
        }
        private string _parameterkey;
        /// <summary>
        ///关建字
        /// </summary>
        public string ParameterKey
        {
            set { _parameterkey = value; }
            get { return _parameterkey; }
        }
        private string _gradeyear;
        /// <summary>
        ///评估年限
        /// </summary>
        public string GradeYear
        {
            set { _gradeyear = value; }
            get { return _gradeyear; }
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
        private decimal _id_Key;
        /// <summary>
        ///自增键
        /// </summary>
        public decimal Id_Key
        {
            set { _id_Key = value; }
            get { return _id_Key; }
        }
        #endregion Model
    }
    #endregion

    #region  展示模型
    /// <summary>
    /// 供应商编辑输入模形
    /// </summary>
    public class InPutSupplieCertificateInfoModel
    {
        #region Model
        /// <summary>
        /// 供应商ID
        /// </summary>
        public string SupplierId
        { set; get; }
        /// <summary>
        /// 采购类别
        /// /// </summary>
        public string PurchaseType
        { set; get; }
        /// <summary>
        /// 供应商属性
        /// </summary>
        public string SupplierProperty
        { set; get; }
        /// <summary>
        /// 证书名称
        /// </summary>
        public string EligibleCertificate
        { set; get; }
        /// <summary>
        /// 证书文件名称
        /// </summary>
        public string CertificateFileName
        { set; get; }
        /// <summary>
        ///  证书文件路经
        /// </summary>
        public string FilePath
        { set; get; }
        /// <summary>
        /// 获取证书日期
        /// </summary>
        public DateTime DateOfCertificate
        { set; get; }
        /// <summary>
        /// 有效性（是/否）
        /// </summary>
        public string IsEfficacy
        { set; get; }

        /// <summary>
        ///备注
        /// </summary>
        public string Remark { set; get; }
        /// <summary>
        ///操作人
        /// </summary>
        public string OpPerson { get; set; }
        /// <summary>
        ///操作日期
        /// </summary>
        public DateTime OpDate { get; set; }
        /// <summary>
        ///操作时间
        /// </summary>
        public DateTime OpTime { get; set; }
        /// <summary>
        ///操作标志
        /// </summary>
        public string OpSign { get; set; }
        /// <summary>
        ///自增键
        /// </summary>
        public decimal Id_Key { get; set; }
        #endregion Model

    }



    /// <summary>
    /// 合格的供应商清册Model
    /// EligibleSuppliersTable
    /// </summary>
    public class EligibleSuppliersVM
    {
        public EligibleSuppliersVM()
        { }
        #region Model
        #region 供应商信息 
        private string _supplierid;
        /// <summary>
        ///供应商Id
        /// </summary>
        public string SupplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        private string _suppliershortname;
        /// <summary>
        ///供应商简称
        /// </summary>
        public string SupplierShortName
        {
            set { _suppliershortname = value; }
            get { return _suppliershortname; }
        }
        private string _suppliername;
        /// <summary>
        ///供应商全称
        /// </summary>
        public string SupplierName
        {
            set { _suppliername = value; }
            get { return _suppliername; }
        }
        private string _supplierproperty;
        /// <summary>
        ///供应商属性
        /// </summary>
        public string SupplierProperty
        {
            set { _supplierproperty = value; }
            get { return _supplierproperty; }
        }
        private string _suppliertel;
        /// <summary>
        ///供应商电话
        /// </summary>
        public string SupplierTel
        {
            set { _suppliertel = value; }
            get { return _suppliertel; }
        }
        private string _supplieruser;
        /// <summary>
        ///供应商联系人
        /// </summary>
        public string SupplierUser
        {
            set { _supplieruser = value; }
            get { return _supplieruser; }
        }
        private string _supplierfaxno;
        /// <summary>
        ///供应商传真
        /// </summary>
        public string SupplierFaxNo
        {
            set { _supplierfaxno = value; }
            get { return _supplierfaxno; }
        }
        private string _supplieremail;
        /// <summary>
        ///供应商邮箱
        /// </summary>
        public string SupplierEmail
        {
            set { _supplieremail = value; }
            get { return _supplieremail; }
        }
        private string _supplieraddress;
        /// <summary>
        ///供应商地址
        /// </summary>
        public string SupplierAddress
        {
            set { _supplieraddress = value; }
            get { return _supplieraddress; }
        }
        private string _supplierPrincipal;
        /// <summary>
        ///交货地址
        /// </summary>
        public string SupplierPrincipal
        {
            set { _supplierPrincipal = value; }
            get { return _supplierPrincipal; }
        }
        private string _purchaseuser;
        /// <summary>
        ///采购人员
        /// </summary>
        public string PurchaseUser
        {
            set { _purchaseuser = value; }
            get { return _purchaseuser; }
        }
        private DateTime _upperpurchasedate;
        /// <summary>
        ///上次采购时间
        /// </summary>
        public DateTime UpperPurchaseDate
        {
            set { _upperpurchasedate = value; }
            get { return _upperpurchasedate; }
        }
        private DateTime _lastpurchasedate;
        /// <summary>
        ///最近采购时间
        /// </summary>
        public DateTime LastPurchaseDate
        {
            set { _lastpurchasedate = value; }
            get { return _lastpurchasedate; }
        }
        private string _purchasetype;
        /// <summary>
        ///采购类型
        /// </summary>
        public string PurchaseType
        {
            set { _purchasetype = value; }
            get { return _purchasetype; }
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
        ///
        public decimal Id_key { set; get; }
        #endregion

        //ISO9001 ISO14001 SupplierBaseDocument	SupplierComment	 NotUseChildLabor	
        //EnvironmentalInvestigation	HonestCommitment	PCN_Protocol	
        //QualityAssuranceProtocol	HSF_Guarantee	REACH_Guarantee	SVHC_Guarantee
        // ISO9001 ISO14001 供应商基本资料表 供应商评鉴表  不使用童工申明 
        //供应商环境调查表  廉洁承诺书  PCN协议
        // 质量保证协议 	HSF保证书  REACH保证书	SVHC调查表 

        #region  合格证书
        /// <summary>
        ///  ISO9001
        /// </summary>
        public string ISO9001
        { set; get; }
        /// <summary>
        /// ISO14001
        /// </summary>
        public string ISO14001
        { set; get; }
        /// <summary>
        /// 供应商基本资料表
        /// </summary>
        public string SupplierBaseDocument
        { set; get; }
        /// <summary>
        /// 供应商评鉴表
        /// </summary>
        public string SupplierComment
        { set; get; }
        /// <summary>
        /// 不使用童工申明
        /// </summary>
        public string NotUseChildLabor
        { set; get; }
        /// <summary>
        /// 供应商环境调查表
        /// </summary>
        public string EnvironmentalInvestigation
        { set; get; }
        /// <summary>
        /// 廉洁承诺书 
        /// </summary>
        public string HonestCommitment
        { set; get; }
        /// <summary>
        /// PCN协议
        /// </summary>
        public string PCN_Protocol
        { set; get; }
        /// <summary>
        /// 质量保证协议
        /// </summary>
        public string QualityAssuranceProtocol
        { set; get; }
        /// <summary>
        /// HSF保证书
        /// </summary>
        public string HSF_Guarantee
        { set; get; }
        /// <summary>
        /// REACH保证书
        /// </summary>
        public string REACH_Guarantee
        { set; get; }
        /// <summary>
        /// SVHC调查表
        /// </summary>
        public string SVHC_Guarantee
        { set; get; }
        #endregion
        #endregion
    }
    #endregion

}
