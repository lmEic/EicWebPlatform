using System;

namespace Lm.Eic.App.DomainModel.Bpm.Hrm.Archives
{
    /// <summary>
    ///身份证实体模型
    /// </summary>
    [Serializable]
    public partial class ArchivesIdentityModel
    {
        public ArchivesIdentityModel()
        { }

        #region Model

        private string _identityid;

        /// <summary>
        ///身份证号
        /// </summary>
        public string IdentityID
        {
            set { _identityid = value; }
            get { return _identityid; }
        }

        private string _name;

        /// <summary>
        ///姓名
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        private string _sex;

        /// <summary>
        ///性别
        /// </summary>
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }

        private string _birthday;

        /// <summary>
        ///出生日期
        /// </summary>
        public string Birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }

        private string _address;

        /// <summary>
        ///地址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }

        private string _nation;

        /// <summary>
        ///籍贯
        /// </summary>
        public string Nation
        {
            set { _nation = value; }
            get { return _nation; }
        }

        private string _signgovernment;

        /// <summary>
        ///签证机构
        /// </summary>
        public string SignGovernment
        {
            set { _signgovernment = value; }
            get { return _signgovernment; }
        }

        private string _limiteddate;

        /// <summary>
        ///有效期
        /// </summary>
        public string LimitedDate
        {
            set { _limiteddate = value; }
            get { return _limiteddate; }
        }

        private string _newaddress;

        /// <summary>
        ///新住址
        /// </summary>
        public string NewAddress
        {
            set { _newaddress = value; }
            get { return _newaddress; }
        }

        private string _nativeplace;

        /// <summary>
        ///籍贯
        /// </summary>
        public string NativePlace
        {
            set { _nativeplace = value; }
            get { return _nativeplace; }
        }

        private byte[] _personalpicture;

        /// <summary>
        ///照片
        /// </summary>
        public byte[] PersonalPicture
        {
            set { _personalpicture = value; }
            get { return _personalpicture; }
        }

        private string _ishaspicture;

        /// <summary>
        ///是否有照片
        /// </summary>
        public string IsHasPicture
        {
            set { _ishaspicture = value; }
            get { return _ishaspicture; }
        }

        private DateTime _scandate;

        /// <summary>
        ///扫描日期
        /// </summary>
        public DateTime ScanDate
        {
            set { _scandate = value; }
            get { return _scandate; }
        }

        private string _scanoper;

        /// <summary>
        ///操作人
        /// </summary>
        public string ScanOper
        {
            set { _scanoper = value; }
            get { return _scanoper; }
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
    ///员工基础信息模型
    /// </summary>
    [Serializable]
    public partial class ArchivesEmployeeIdentityModel
    {
        public ArchivesEmployeeIdentityModel()
        { }

        #region Model

        private string _identityid;

        /// <summary>
        ///身份证号码
        /// </summary>
        public string IdentityID
        {
            set { _identityid = value; }
            get { return _identityid; }
        }

        private string _workerid;

        /// <summary>
        ///作业工号
        /// </summary>
        public string WorkerId
        {
            set { _workerid = value; }
            get { return _workerid; }
        }

        private string _workeridnumtype;

        /// <summary>
        ///工号数字类别
        /// </summary>
        public string WorkerIdNumType
        {
            set { _workeridnumtype = value; }
            get { return _workeridnumtype; }
        }

        private string _workeridtype;

        /// <summary>
        ///工号类型
        /// </summary>
        public string WorkerIdType
        {
            set { _workeridtype = value; }
            get { return _workeridtype; }
        }

        private string _name;

        /// <summary>
        ///姓名
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        private string _cardid;

        /// <summary>
        ///考勤卡号
        /// </summary>
        public string CardID
        {
            set { _cardid = value; }
            get { return _cardid; }
        }

        private string _organizetion;

        /// <summary>
        ///部门组织
        /// </summary>
        public string Organizetion
        {
            set { _organizetion = value; }
            get { return _organizetion; }
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

        private int _departmentchangerecord;

        /// <summary>
        ///部门变更记录
        /// </summary>
        public int DepartmentChangeRecord
        {
            set { _departmentchangerecord = value; }
            get { return _departmentchangerecord; }
        }

        private string _post;

        /// <summary>
        ///岗位
        /// </summary>
        public string Post
        {
            set { _post = value; }
            get { return _post; }
        }

        private string _postnature;

        /// <summary>
        ///岗位性质
        /// </summary>
        public string PostNature
        {
            set { _postnature = value; }
            get { return _postnature; }
        }

        private string _posttype;

        /// <summary>
        ///岗位类型
        /// </summary>
        public string PostType
        {
            set { _posttype = value; }
            get { return _posttype; }
        }

        private int _postchangerecord;

        /// <summary>
        ///岗位变更记录
        /// </summary>
        public int PostChangeRecord
        {
            set { _postchangerecord = value; }
            get { return _postchangerecord; }
        }

        private string _sex;

        /// <summary>
        ///性别
        /// </summary>
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }

        private string _birthday;

        /// <summary>
        ///出生日期
        /// </summary>
        public string Birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }

        private string _address;

        /// <summary>
        ///家庭住址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }

        private string _nation;

        /// <summary>
        ///民族
        /// </summary>
        public string Nation
        {
            set { _nation = value; }
            get { return _nation; }
        }

        private string _signgovernment;

        /// <summary>
        ///签证机构
        /// </summary>
        public string SignGovernment
        {
            set { _signgovernment = value; }
            get { return _signgovernment; }
        }

        private string _limiteddate;

        /// <summary>
        ///有效期
        /// </summary>
        public string LimitedDate
        {
            set { _limiteddate = value; }
            get { return _limiteddate; }
        }

        private string _newaddress;

        /// <summary>
        ///新住址
        /// </summary>
        public string NewAddress
        {
            set { _newaddress = value; }
            get { return _newaddress; }
        }

        private string _politicalstatus;

        /// <summary>
        ///政治面貌
        /// </summary>
        public string PoliticalStatus
        {
            set { _politicalstatus = value; }
            get { return _politicalstatus; }
        }

        private string _nativeplace;

        /// <summary>
        ///居住地
        /// </summary>
        public string NativePlace
        {
            set { _nativeplace = value; }
            get { return _nativeplace; }
        }

        private string _registeredpermanent;

        /// <summary>
        ///籍贯
        /// </summary>
        public string RegisteredPermanent
        {
            set { _registeredpermanent = value; }
            get { return _registeredpermanent; }
        }

        private string _marrystatus;

        /// <summary>
        ///婚否
        /// </summary>
        public string MarryStatus
        {
            set { _marrystatus = value; }
            get { return _marrystatus; }
        }

        private string _birthmonth;

        /// <summary>
        ///出生年月
        /// </summary>
        public string BirthMonth
        {
            set { _birthmonth = value; }
            get { return _birthmonth; }
        }

        private DateTime _identityexpirationdate;

        /// <summary>
        ///身份证过期时间
        /// </summary>
        public DateTime IdentityExpirationDate
        {
            set { _identityexpirationdate = value; }
            get { return _identityexpirationdate; }
        }

        private byte[] _personalpicture;

        /// <summary>
        ///照片
        /// </summary>
        public byte[] PersonalPicture
        {
            set { _personalpicture = value; }
            get { return _personalpicture; }
        }

        private string _schoolname;

        /// <summary>
        ///学校名称
        /// </summary>
        public string SchoolName
        {
            set { _schoolname = value; }
            get { return _schoolname; }
        }

        private string _majorname;

        /// <summary>
        ///专业名称
        /// </summary>
        public string MajorName
        {
            set { _majorname = value; }
            get { return _majorname; }
        }

        private string _education;

        /// <summary>
        ///学历
        /// </summary>
        public string Education
        {
            set { _education = value; }
            get { return _education; }
        }

        private string _familyphone;

        /// <summary>
        ///家庭电话
        /// </summary>
        public string FamilyPhone
        {
            set { _familyphone = value; }
            get { return _familyphone; }
        }

        private string _telphone;

        /// <summary>
        ///手机号码
        /// </summary>
        public string TelPhone
        {
            set { _telphone = value; }
            get { return _telphone; }
        }

        private string _certificatename;

        /// <summary>
        ///证书
        /// </summary>
        public string CertificateName
        {
            set { _certificatename = value; }
            get { return _certificatename; }
        }

        private string _workingstatus;

        /// <summary>
        ///工作状态
        /// </summary>
        public string WorkingStatus
        {
            set { _workingstatus = value; }
            get { return _workingstatus; }
        }

        private DateTime _registeddate;

        /// <summary>
        ///报到日期
        /// </summary>
        public DateTime RegistedDate
        {
            set { _registeddate = value; }
            get { return _registeddate; }
        }

        private string _registedsegment;

        /// <summary>
        ///时间段
        /// </summary>
        public string RegistedSegment
        {
            set { _registedsegment = value; }
            get { return _registedsegment; }
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

        private string _opperson;

        /// <summary>
        ///操作人
        /// </summary>
        public string OpPerson
        {
            set { _opperson = value; }
            get { return _opperson; }
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
    ///员工基础信息数据传输模型
    /// </summary>
    [Serializable]
    public partial class ArchivesEmployeeIdentityDto
    {
        public ArchivesEmployeeIdentityDto()
        { }

        #region Model

        private string _identityid;

        /// <summary>
        ///身份证号码
        /// </summary>
        public string IdentityID
        {
            set { _identityid = value; }
            get { return _identityid; }
        }

        private string _workerid;

        /// <summary>
        ///作业工号
        /// </summary>
        public string WorkerId
        {
            set { _workerid = value; }
            get { return _workerid; }
        }

        private string _workeridnumtype;

        /// <summary>
        ///作业工号数字类型
        /// </summary>
        public string WorkerIdNumType
        {
            set { _workeridnumtype = value; }
            get { return _workeridnumtype; }
        }

        private string _workeridtype;

        /// <summary>
        ///作业工号类型
        /// </summary>
        public string WorkerIdType
        {
            set { _workeridtype = value; }
            get { return _workeridtype; }
        }

        private string _name;

        /// <summary>
        ///姓名
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        private string _cardid;

        /// <summary>
        ///登记号码
        /// </summary>
        public string CardID
        {
            set { _cardid = value; }
            get { return _cardid; }
        }

        private string _organizetion;

        /// <summary>
        ///所在组织
        /// </summary>
        public string Organizetion
        {
            set { _organizetion = value; }
            get { return _organizetion; }
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

        private int _departmentchangerecord;

        /// <summary>
        ///部门变更记录
        /// </summary>
        public int DepartmentChangeRecord
        {
            set { _departmentchangerecord = value; }
            get { return _departmentchangerecord; }
        }

        private string _post;

        /// <summary>
        ///岗位
        /// </summary>
        public string Post
        {
            set { _post = value; }
            get { return _post; }
        }

        private string _postnature;

        /// <summary>
        ///岗位性质
        /// </summary>
        public string PostNature
        {
            set { _postnature = value; }
            get { return _postnature; }
        }

        private int _postchangerecord;

        /// <summary>
        ///岗位变更记录
        /// </summary>
        public int PostChangeRecord
        {
            set { _postchangerecord = value; }
            get { return _postchangerecord; }
        }

        private string _sex;

        /// <summary>
        ///性别
        /// </summary>
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }

        private string _birthday;

        /// <summary>
        ///出生日期
        /// </summary>
        public string Birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }

        private string _address;

        /// <summary>
        ///家庭住址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }

        private string _nation;

        /// <summary>
        ///籍贯
        /// </summary>
        public string Nation
        {
            set { _nation = value; }
            get { return _nation; }
        }

        private string _signgovernment;

        /// <summary>
        ///签证机构
        /// </summary>
        public string SignGovernment
        {
            set { _signgovernment = value; }
            get { return _signgovernment; }
        }

        private string _limiteddate;

        /// <summary>
        ///有效期
        /// </summary>
        public string LimitedDate
        {
            set { _limiteddate = value; }
            get { return _limiteddate; }
        }

        private string _newaddress;

        /// <summary>
        ///新住址
        /// </summary>
        public string NewAddress
        {
            set { _newaddress = value; }
            get { return _newaddress; }
        }

        private string _politicalstatus;

        /// <summary>
        ///政治面貌
        /// </summary>
        public string PoliticalStatus
        {
            set { _politicalstatus = value; }
            get { return _politicalstatus; }
        }

        private string _nativeplace;

        /// <summary>
        ///居住地
        /// </summary>
        public string NativePlace
        {
            set { _nativeplace = value; }
            get { return _nativeplace; }
        }

        private string _registeredpermanent;

        /// <summary>
        ///永久居住地
        /// </summary>
        public string RegisteredPermanent
        {
            set { _registeredpermanent = value; }
            get { return _registeredpermanent; }
        }

        private string _marrystatus;

        /// <summary>
        ///婚姻状态
        /// </summary>
        public string MarryStatus
        {
            set { _marrystatus = value; }
            get { return _marrystatus; }
        }

        private int _birthmonth;

        /// <summary>
        ///出生月份
        /// </summary>
        public int BirthMonth
        {
            set { _birthmonth = value; }
            get { return _birthmonth; }
        }

        private string _identityexpirationdate;

        /// <summary>
        ///身份证有效期
        /// </summary>
        public string IdentityExpirationDate
        {
            set { _identityexpirationdate = value; }
            get { return _identityexpirationdate; }
        }

        private byte[] _personalpicture;

        /// <summary>
        ///照片
        /// </summary>
        public byte[] PersonalPicture
        {
            set { _personalpicture = value; }
            get { return _personalpicture; }
        }

        private string _schoolname;

        /// <summary>
        ///学校名称
        /// </summary>
        public string SchoolName
        {
            set { _schoolname = value; }
            get { return _schoolname; }
        }

        private DateTime _studydatefrom;

        /// <summary>
        ///起始日期
        /// </summary>
        public DateTime StudyDateFrom
        {
            set { _studydatefrom = value; }
            get { return _studydatefrom; }
        }

        private DateTime _studydateto;

        /// <summary>
        ///结束日期
        /// </summary>
        public DateTime StudyDateTo
        {
            set { _studydateto = value; }
            get { return _studydateto; }
        }

        private string _majorname;

        /// <summary>
        ///专业名称
        /// </summary>
        public string MajorName
        {
            set { _majorname = value; }
            get { return _majorname; }
        }

        private string _education;

        /// <summary>
        ///学历
        /// </summary>
        public string Education
        {
            set { _education = value; }
            get { return _education; }
        }

        private string _familyphone;

        /// <summary>
        ///家庭电话
        /// </summary>
        public string FamilyPhone
        {
            set { _familyphone = value; }
            get { return _familyphone; }
        }

        private string _personphone;

        /// <summary>
        ///个人电话
        /// </summary>
        public string PersonPhone
        {
            set { _personphone = value; }
            get { return _personphone; }
        }

        private string _telphone;

        /// <summary>
        ///联系电话
        /// </summary>
        public string TelPhone
        {
            set { _telphone = value; }
            get { return _telphone; }
        }

        private string _certificatename;

        /// <summary>
        ///证书名称
        /// </summary>
        public string CertificateName
        {
            set { _certificatename = value; }
            get { return _certificatename; }
        }

        private string _workingstatus;

        /// <summary>
        ///工作状态
        /// </summary>
        public string WorkingStatus
        {
            set { _workingstatus = value; }
            get { return _workingstatus; }
        }

        private DateTime _registeddate;

        /// <summary>
        ///报到日期
        /// </summary>
        public DateTime RegistedDate
        {
            set { _registeddate = value; }
            get { return _registeddate; }
        }

        private string _registedsegment;

        /// <summary>
        ///报到时间段
        /// </summary>
        public string RegistedSegment
        {
            set { _registedsegment = value; }
            get { return _registedsegment; }
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

        private string _opperson;

        /// <summary>
        ///操作人
        /// </summary>
        public string OpPerson
        {
            set { _opperson = value; }
            get { return _opperson; }
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
    ///岗位变更库实体模型
    /// </summary>
    [Serializable]
    public partial class ArPostChangeLibModel
    {
        public ArPostChangeLibModel()
        { }

        #region Model

        private string _workerid;

        /// <summary>
        ///作业工号
        /// </summary>
        public string WorkerId
        {
            set { _workerid = value; }
            get { return _workerid; }
        }

        private string _workername;

        /// <summary>
        ///作业姓名
        /// </summary>
        public string WorkerName
        {
            set { _workername = value; }
            get { return _workername; }
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

        private DateTime _assigndate;

        /// <summary>
        ///分配日期
        /// </summary>
        public DateTime AssignDate
        {
            set { _assigndate = value; }
            get { return _assigndate; }
        }

        private string _postnature;

        /// <summary>
        ///岗位性质
        /// </summary>
        public string PostNature
        {
            set { _postnature = value; }
            get { return _postnature; }
        }

        private string _posttype;

        /// <summary>
        ///岗位类型
        /// </summary>
        public string PostType
        {
            set { _posttype = value; }
            get { return _posttype; }
        }

        private string _oldpost;

        /// <summary>
        ///原岗位
        /// </summary>
        public string OldPost
        {
            set { _oldpost = value; }
            get { return _oldpost; }
        }

        private string _nowpost;

        /// <summary>
        ///新岗位
        /// </summary>
        public string NowPost
        {
            set { _nowpost = value; }
            get { return _nowpost; }
        }

        private string _instatus;

        /// <summary>
        ///状态
        /// </summary>
        public string InStatus
        {
            set { _instatus = value; }
            get { return _instatus; }
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

        #endregion Model
    }

    /// <summary>
    ///部门变更实体模型
    /// </summary>
    [Serializable]
    public partial class ArDepartmentChangeLibModel
    {
        public ArDepartmentChangeLibModel()
        { }

        #region Model

        private string _workerid;

        /// <summary>
        ///作业工号
        /// </summary>
        public string WorkerId
        {
            set { _workerid = value; }
            get { return _workerid; }
        }

        private string _workername;

        /// <summary>
        ///作业姓名
        /// </summary>
        public string WorkerName
        {
            set { _workername = value; }
            get { return _workername; }
        }

        private DateTime _assigndate;

        /// <summary>
        ///分配日期
        /// </summary>
        public DateTime AssignDate
        {
            set { _assigndate = value; }
            get { return _assigndate; }
        }

        private string _instatus;

        /// <summary>
        ///状态
        /// </summary>
        public string InStatus
        {
            set { _instatus = value; }
            get { return _instatus; }
        }

        private string _olddepartment;

        /// <summary>
        ///原部门
        /// </summary>
        public string OldDepartment
        {
            set { _olddepartment = value; }
            get { return _olddepartment; }
        }

        private string _nowdepartment;

        /// <summary>
        ///新部门
        /// </summary>
        public string NowDepartment
        {
            set { _nowdepartment = value; }
            get { return _nowdepartment; }
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
    ///证书实体模型
    /// </summary>
    [Serializable]
    public partial class ArCertificateModel
    {
        public ArCertificateModel()
        { }

        #region Model

        private string _workerid;

        /// <summary>
        ///作业工号
        /// </summary>
        public string WorkerId
        {
            set { _workerid = value; }
            get { return _workerid; }
        }

        private string _workername;

        /// <summary>
        ///作业姓名
        /// </summary>
        public string WorkerName
        {
            set { _workername = value; }
            get { return _workername; }
        }

        private string _certificatetype;

        /// <summary>
        ///证书类型
        /// </summary>
        public string CertificateType
        {
            set { _certificatetype = value; }
            get { return _certificatetype; }
        }

        private string _certificatename;

        /// <summary>
        ///证书名称
        /// </summary>
        public string CertificateName
        {
            set { _certificatename = value; }
            get { return _certificatename; }
        }

        private string _getdate;

        /// <summary>
        ///获得日期
        /// </summary>
        public string GetDate
        {
            set { _getdate = value; }
            get { return _getdate; }
        }

        private string _workingstatus;

        /// <summary>
        ///状态
        /// </summary>
        public string WorkingStatus
        {
            set { _workingstatus = value; }
            get { return _workingstatus; }
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

        private string _opdate;

        /// <summary>
        ///操作日期
        /// </summary>
        public string OpDate
        {
            set { _opdate = value; }
            get { return _opdate; }
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
    ///联系方式实体模型
    /// </summary>
    [Serializable]
    public partial class ArTelModel
    {
        public ArTelModel()
        { }

        #region Model

        private string _opsign;

        /// <summary>
        ///操作标志
        /// </summary>
        public string OpSign
        {
            set { _opsign = value; }
            get { return _opsign; }
        }

        private string _workerid;

        /// <summary>
        ///作业工号
        /// </summary>
        public string WorkerId
        {
            set { _workerid = value; }
            get { return _workerid; }
        }

        private string _workername;

        /// <summary>
        ///作业姓名
        /// </summary>
        public string WorkerName
        {
            set { _workername = value; }
            get { return _workername; }
        }

        private string _familyphone;

        /// <summary>
        ///家庭电话
        /// </summary>
        public string FamilyPhone
        {
            set { _familyphone = value; }
            get { return _familyphone; }
        }

        private string _personphone;

        /// <summary>
        ///个人电话
        /// </summary>
        public string PersonPhone
        {
            set { _personphone = value; }
            get { return _personphone; }
        }

        private string _telphone;

        /// <summary>
        ///联系电话
        /// </summary>
        public string TelPhone
        {
            set { _telphone = value; }
            get { return _telphone; }
        }

        private string _workingstatus;

        /// <summary>
        ///状态
        /// </summary>
        public string WorkingStatus
        {
            set { _workingstatus = value; }
            get { return _workingstatus; }
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
    ///学习信息模型
    /// </summary>
    [Serializable]
    public partial class ArStudyModel
    {
        public ArStudyModel()
        { }

        #region Model

        private string _workerid;

        /// <summary>
        ///作业工号
        /// </summary>
        public string WorkerId
        {
            set { _workerid = value; }
            get { return _workerid; }
        }

        private string _workername;

        /// <summary>
        ///作业姓名
        /// </summary>
        public string WorkerName
        {
            set { _workername = value; }
            get { return _workername; }
        }

        private DateTime _studydatefrom;

        /// <summary>
        ///起始日期
        /// </summary>
        public DateTime StudyDateFrom
        {
            set { _studydatefrom = value; }
            get { return _studydatefrom; }
        }

        private DateTime _studydateto;

        /// <summary>
        ///结束日期
        /// </summary>
        public DateTime StudyDateTo
        {
            set { _studydateto = value; }
            get { return _studydateto; }
        }

        private string _schoolname;

        /// <summary>
        ///学校名称
        /// </summary>
        public string SchoolName
        {
            set { _schoolname = value; }
            get { return _schoolname; }
        }

        private string _majorname;

        /// <summary>
        ///专业名称
        /// </summary>
        public string MajorName
        {
            set { _majorname = value; }
            get { return _majorname; }
        }

        private string _qulification;

        /// <summary>
        ///学历
        /// </summary>
        public string Qulification
        {
            set { _qulification = value; }
            get { return _qulification; }
        }

        private string _workingstatus;

        /// <summary>
        ///状态
        /// </summary>
        public string WorkingStatus
        {
            set { _workingstatus = value; }
            get { return _workingstatus; }
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
    /// 作业人员信息
    /// </summary>
    public partial class ArWorkerInfo
    {
        private string _workerid;

        /// <summary>
        ///作业工号
        /// </summary>
        public string WorkerId
        {
            set { _workerid = value; }
            get { return _workerid; }
        }

        private string _name;

        /// <summary>
        ///姓名
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
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

        private string _organizetion;

        /// <summary>
        ///部门组织
        /// </summary>
        public string Organizetion
        {
            set { _organizetion = value; }
            get { return _organizetion; }
        }

        private string _post;

        /// <summary>
        ///岗位
        /// </summary>
        public string Post
        {
            set { _post = value; }
            get { return _post; }
        }

        private string _postnature;

        /// <summary>
        ///岗位性质
        /// </summary>
        public string PostNature
        {
            set { _postnature = value; }
            get { return _postnature; }
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

        private byte[] _personalpicture;

        /// <summary>
        ///照片
        /// </summary>
        public byte[] PersonalPicture
        {
            set { _personalpicture = value; }
            get { return _personalpicture; }
        }
    }

    /// <summary>
    ///生产员工信息模型
    /// </summary>
    [Serializable]
    public partial class ProWorkerInfo
    {
        public ProWorkerInfo()
        { }

        #region Model

        private string _workerid;

        /// <summary>
        ///作业工号
        /// </summary>
        public string WorkerId
        {
            set { _workerid = value; }
            get { return _workerid; }
        }

        private string _workername;

        /// <summary>
        ///作业姓名
        /// </summary>
        public string WorkerName
        {
            set { _workername = value; }
            get { return _workername; }
        }

        private string _organizetion;

        /// <summary>
        ///组织
        /// </summary>
        public string Organizetion
        {
            set { _organizetion = value; }
            get { return _organizetion; }
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

        private string _post;

        /// <summary>
        ///岗位
        /// </summary>
        public string Post
        {
            set { _post = value; }
            get { return _post; }
        }

        private string _ispostkey;

        /// <summary>
        ///是否关键岗位
        /// </summary>
        public string IsPostKey
        {
            set { _ispostkey = value; }
            get { return _ispostkey; }
        }

        private string _posttype;

        /// <summary>
        ///直间接
        /// </summary>
        public string PostType
        {
            set { _posttype = value; }
            get { return _posttype; }
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

        private string _leadworkerid;

        /// <summary>
        ///组长工号
        /// </summary>
        public string LeadWorkerId
        {
            set { _leadworkerid = value; }
            get { return _leadworkerid; }
        }

        private string _leadworkername;

        /// <summary>
        ///组长姓名
        /// </summary>
        public string LeadWorkerName
        {
            set { _leadworkername = value; }
            get { return _leadworkername; }
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
    /// 离职人员信息
    /// </summary>
    public partial class ArWorkerLeaveOfficeModel
    {
        #region Model
        private string _id;
        /// <summary>
        ///身份证
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        private string _workerid;
        /// <summary>
        ///工号
        /// </summary>
        public string WorkerId
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
        private string _department;
        /// <summary>
        ///部门
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        private string _post;
        /// <summary>
        ///类型
        /// </summary>
        public string Post
        {
            set { _post = value; }
            get { return _post; }
        }
        private DateTime _leavedate;
        /// <summary>
        ///离职日期
        /// </summary>
        public DateTime LeaveDate
        {
            set { _leavedate = value; }
            get { return _leavedate; }
        }
        private string _leavereason;
        /// <summary>
        ///离职原因
        /// </summary>
        public string LeaveReason
        {
            set { _leavereason = value; }
            get { return _leavereason; }
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