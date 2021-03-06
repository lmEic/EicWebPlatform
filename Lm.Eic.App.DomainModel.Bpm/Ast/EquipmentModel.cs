﻿using System;

namespace Lm.Eic.App.DomainModel.Bpm.Ast
{
    /// <summary>
    ///设备档案模型
    /// </summary>
    [Serializable]
    public partial class EquipmentModel
    {
        public EquipmentModel()
        { }

        #region Model

        private string _assetnumber;

        /// <summary>
        ///财产编号
        /// </summary>
        public string AssetNumber
        {
            set { _assetnumber = value; }
            get { return _assetnumber; }
        }

        private string _equipmentname;

        /// <summary>
        ///设备名称
        /// </summary>
        public string EquipmentName
        {
            set { _equipmentname = value; }
            get { return _equipmentname; }
        }

        private string _equipmentspec;

        /// <summary>
        ///设备型号
        /// </summary>
        public string EquipmentSpec
        {
            set { _equipmentspec = value; }
            get { return _equipmentspec; }
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

        private int _servicelife;

        /// <summary>
        ///使用寿命
        /// </summary>
        public int ServiceLife
        {
            set { _servicelife = value; }
            get { return _servicelife; }
        }

        private string _equipmentphoto;

        /// <summary>
        ///设备照片
        /// </summary>
        public string EquipmentPhoto
        {
            set { _equipmentphoto = value; }
            get { return _equipmentphoto; }
        }

        private string _assettype;

        /// <summary>
        ///资产类别
        /// </summary>
        public string AssetType
        {
            set { _assettype = value; }
            get { return _assettype; }
        }

        private string _equipmenttype;

        /// <summary>
        ///设备类别
        /// </summary>
        public string EquipmentType
        {
            set { _equipmenttype = value; }
            get { return _equipmenttype; }
        }

        private string _taxtype;

        /// <summary>
        ///税务类别
        /// </summary>
        public string TaxType
        {
            set { _taxtype = value; }
            get { return _taxtype; }
        }

        private string _freeordernumber;

        /// <summary>
        ///免单号
        /// </summary>
        public string FreeOrderNumber
        {
            set { _freeordernumber = value; }
            get { return _freeordernumber; }
        }

        private string _declarationnumber;

        /// <summary>
        ///报关单号
        /// </summary>
        public string DeclarationNumber
        {
            set { _declarationnumber = value; }
            get { return _declarationnumber; }
        }

        private string _unit;

        /// <summary>
        ///单位
        /// </summary>
        public string Unit
        {
            set { _unit = value; }
            get { return _unit; }
        }

        private string _manufacturer;

        /// <summary>
        ///生产厂家
        /// </summary>
        public string Manufacturer
        {
            set { _manufacturer = value; }
            get { return _manufacturer; }
        }

        private string _manufacturingnumber;

        /// <summary>
        ///制造编号
        /// </summary>
        public string ManufacturingNumber
        {
            set { _manufacturingnumber = value; }
            get { return _manufacturingnumber; }
        }

        private string _manufacturerwebsite;

        /// <summary>
        ///产品官网
        /// </summary>
        public string ManufacturerWebsite
        {
            set { _manufacturerwebsite = value; }
            get { return _manufacturerwebsite; }
        }

        private string _manufacturertel;

        /// <summary>
        ///供应商电话
        /// </summary>
        public string ManufacturerTel
        {
            set { _manufacturertel = value; }
            get { return _manufacturertel; }
        }

        private string _aftersalestel;

        /// <summary>
        ///售后电话
        /// </summary>
        public string AfterSalesTel
        {
            set { _aftersalestel = value; }
            get { return _aftersalestel; }
        }

        private string _addmode;

        /// <summary>
        ///增加方式
        /// </summary>
        public string AddMode
        {
            set { _addmode = value; }
            get { return _addmode; }
        }

        private DateTime _deliverydate;

        /// <summary>
        ///购入日期
        /// </summary>
        public DateTime DeliveryDate
        {
            set { _deliverydate = value; }
            get { return _deliverydate; }
        }

        private string _deliveryuser;

        /// <summary>
        ///交付人
        /// </summary>
        public string DeliveryUser
        {
            set { _deliveryuser = value; }
            get { return _deliveryuser; }
        }

        private string _deliverycheckuser;

        /// <summary>
        ///验收人
        /// </summary>
        public string DeliveryCheckUser
        {
            set { _deliverycheckuser = value; }
            get { return _deliverycheckuser; }
        }

        private string _safekeepworkerid;

        /// <summary>
        ///保管人工号
        /// </summary>
        public string SafekeepWorkerID
        {
            set { _safekeepworkerid = value; }
            get { return _safekeepworkerid; }
        }

        private string _safekeepuser;

        /// <summary>
        ///保管人
        /// </summary>
        public string SafekeepUser
        {
            set { _safekeepuser = value; }
            get { return _safekeepuser; }
        }

        private string _safekeepdepartment;

        /// <summary>
        ///保管单位
        /// </summary>
        public string SafekeepDepartment
        {
            set { _safekeepdepartment = value; }
            get { return _safekeepdepartment; }
        }

        private string _installationlocation;

        /// <summary>
        ///安装位置
        /// </summary>
        public string Installationlocation
        {
            set { _installationlocation = value; }
            get { return _installationlocation; }
        }

        private string _ismaintenance;

        /// <summary>
        ///是否保养
        /// </summary>
        public string IsMaintenance
        {
            set { _ismaintenance = value; }
            get { return _ismaintenance; }
        }

        private DateTime _maintenancedate;

        /// <summary>
        ///保养日期
        /// </summary>
        public DateTime MaintenanceDate
        {
            set { _maintenancedate = value; }
            get { return _maintenancedate; }
        }

        private int _maintenanceinterval;

        /// <summary>
        ///保养间隔
        /// </summary>
        public int MaintenanceInterval
        {
            set { _maintenanceinterval = value; }
            get { return _maintenanceinterval; }
        }

        private DateTime _plannedmaintenancedate;

        /// <summary>
        ///计划保养日期
        /// </summary>
        public DateTime PlannedMaintenanceDate
        {
            set { _plannedmaintenancedate = value; }
            get { return _plannedmaintenancedate; }
        }

        private string _plannedMaintenanceMonth;

        /// <summary>
        ///计划保养月份
        /// </summary>
        public string PlannedMaintenanceMonth
        {
            set { _plannedMaintenanceMonth = value; }
            get { return _plannedMaintenanceMonth; }
        }

        private string _maintenancestate;

        /// <summary>
        ///保养状态
        /// </summary>
        public string MaintenanceState
        {
            set { _maintenancestate = value; }
            get { return _maintenancestate; }
        }

        private string _ischeck;

        /// <summary>
        ///是否校验
        /// </summary>
        public string IsCheck
        {
            set { _ischeck = value; }
            get { return _ischeck; }
        }

        private string _checktype;

        /// <summary>
        ///校验类型
        /// </summary>
        public string CheckType
        {
            set { _checktype = value; }
            get { return _checktype; }
        }

        private DateTime _checkdate;

        /// <summary>
        ///校验日期
        /// </summary>
        public DateTime CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }

        private int _checkinterval;

        /// <summary>
        ///校验间隔
        /// </summary>
        public int CheckInterval
        {
            set { _checkinterval = value; }
            get { return _checkinterval; }
        }

        private DateTime _plannedcheckdate;

        /// <summary>
        ///计划校验日期
        /// </summary>
        public DateTime PlannedCheckDate
        {
            set { _plannedcheckdate = value; }
            get { return _plannedcheckdate; }
        }

        private string _checkstate;

        /// <summary>
        ///校验状态
        /// </summary>
        public string CheckState
        {
            set { _checkstate = value; }
            get { return _checkstate; }
        }

        private string _state;

        /// <summary>
        ///设备状态
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
        }

        private string _isscrapped;

        /// <summary>
        ///是否报废
        /// </summary>
        public string IsScrapped
        {
            set { _isscrapped = value; }
            get { return _isscrapped; }
        }

        private DateTime _inputdate;

        /// <summary>
        ///输入日期
        /// </summary>
        public DateTime InputDate
        {
            set { _inputdate = value; }
            get { return _inputdate; }
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
    ///设备校验记录模型
    /// </summary>
    [Serializable]
    public partial class EquipmentCheckRecordModel
    {
        public EquipmentCheckRecordModel()
        { }

        #region Model

        private string _assetnumber;

        /// <summary>
        ///财产编号
        /// </summary>
        public string AssetNumber
        {
            set { _assetnumber = value; }
            get { return _assetnumber; }
        }

        private string _equipmentname;

        /// <summary>
        ///设备名称
        /// </summary>
        public string EquipmentName
        {
            set { _equipmentname = value; }
            get { return _equipmentname; }
        }

        private DateTime _checkdate;

        /// <summary>
        ///校验日期
        /// </summary>
        public DateTime CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }

        private string _checkworkerid;

        /// <summary>
        ///校验人工号
        /// </summary>
        public string CheckWorkerID
        {
            set { _checkworkerid = value; }
            get { return _checkworkerid; }
        }

        private string _checkuser;

        /// <summary>
        ///校验人姓名
        /// </summary>
        public string CheckUser
        {
            set { _checkuser = value; }
            get { return _checkuser; }
        }

        private string _checkresult;

        /// <summary>
        ///校验结果
        /// </summary>
        public string CheckResult
        {
            set { _checkresult = value; }
            get { return _checkresult; }
        }

        private string _verifyuser;

        /// <summary>
        ///确认人
        /// </summary>
        public string VerifyUser
        {
            set { _verifyuser = value; }
            get { return _verifyuser; }
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
        ///操作标识
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

    /// <summary>
    ///设备保养记录模型
    /// </summary>
    [Serializable]
    public partial class EquipmentMaintenanceRecordModel
    {
        public EquipmentMaintenanceRecordModel()
        { }

        #region Model

        private string _assetnumber;

        /// <summary>
        ///财产编号
        /// </summary>
        public string AssetNumber
        {
            set { _assetnumber = value; }
            get { return _assetnumber; }
        }

        private string _equipmentname;

        /// <summary>
        ///设备名称
        /// </summary>
        public string EquipmentName
        {
            set { _equipmentname = value; }
            get { return _equipmentname; }
        }

        private DateTime _maintenancedate;

        /// <summary>
        ///保养日期
        /// </summary>
        public DateTime MaintenanceDate
        {
            set { _maintenancedate = value; }
            get { return _maintenancedate; }
        }

        private string _maintenanceworkerid;

        /// <summary>
        ///保养人工号
        /// </summary>
        public string MaintenanceWorkerID
        {
            set { _maintenanceworkerid = value; }
            get { return _maintenanceworkerid; }
        }

        private string _maintenanceuser;

        /// <summary>
        ///保养人姓名
        /// </summary>
        public string MaintenanceUser
        {
            set { _maintenanceuser = value; }
            get { return _maintenanceuser; }
        }

        private string _maintenanceresult;

        /// <summary>
        ///保养结果
        /// </summary>
        public string MaintenanceResult
        {
            set { _maintenanceresult = value; }
            get { return _maintenanceresult; }
        }

        private string _verifyuser;

        /// <summary>
        ///确认人
        /// </summary>
        public string VerifyUser
        {
            set { _verifyuser = value; }
            get { return _verifyuser; }
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
        ///操作标识
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

    /// <summary>
    ///设备报废记录模型
    /// </summary>
    [Serializable]
    public partial class EquipmentDiscardRecordModel
    {
        public EquipmentDiscardRecordModel()
        { }

        #region Model

        private string _assetnumber;

        /// <summary>
        ///财产编号
        /// </summary>
        public string AssetNumber
        {
            set { _assetnumber = value; }
            get { return _assetnumber; }
        }

        private string _equipmentname;

        /// <summary>
        ///设备名称
        /// </summary>
        public string EquipmentName
        {
            set { _equipmentname = value; }
            get { return _equipmentname; }
        }

        private DateTime _discarddate;

        /// <summary>
        ///报废日期
        /// </summary>
        public DateTime DiscardDate
        {
            set { _discarddate = value; }
            get { return _discarddate; }
        }

        private string _discardmonth;

        /// <summary>
        ///报废月份
        /// </summary>
        public string DiscardMonth
        {
            set { _discardmonth = value; }
            get { return _discardmonth; }
        }

        private string _discardtype;

        /// <summary>
        ///报废类别
        /// </summary>
        public string DiscardType
        {
            set { _discardtype = value; }
            get { return _discardtype; }
        }

        private string _discardcause;

        /// <summary>
        ///报废原因
        /// </summary>
        public string DiscardCause
        {
            set { _discardcause = value; }
            get { return _discardcause; }
        }

        private string _documentid;

        /// <summary>
        ///文档编号
        /// </summary>
        public string DocumentId
        {
            set { _documentid = value; }
            get { return _documentid; }
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

    /// <summary>
    ///设备维修记录模型
    /// </summary>
    [Serializable]
    public class EquipmentRepairedRecordModel
    {
        public EquipmentRepairedRecordModel()
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

        private string _assetnumber;

        /// <summary>
        ///财产编号
        /// </summary>
        public string AssetNumber
        {
            set { _assetnumber = value; }
            get { return _assetnumber; }
        }

        private string _equipmentname;

        /// <summary>
        ///设备名称
        /// </summary>
        public string EquipmentName
        {
            set { _equipmentname = value; }
            get { return _equipmentname; }
        }

        private string _manufacturingnumber;

        /// <summary>
        ///制造编号
        /// </summary>
        public string ManufacturingNumber
        {
            set { _manufacturingnumber = value; }
            get { return _manufacturingnumber; }
        }

        private string _safekeepdepartment;

        /// <summary>
        ///保管部门
        /// </summary>
        public string SafekeepDepartment
        {
            set { _safekeepdepartment = value; }
            get { return _safekeepdepartment; }
        }

        private DateTime _filingdate;

        /// <summary>
        ///申请维修日期
        /// </summary>
        public DateTime FilingDate
        {
            set { _filingdate = value; }
            get { return _filingdate; }
        }

        private string _repaireduser;

        /// <summary>
        ///维修人
        /// </summary>
        public string RepairedUser
        {
            set { _repaireduser = value; }
            get { return _repaireduser; }
        }

        private DateTime _finishdate;

        /// <summary>
        ///完成日期
        /// </summary>
        public DateTime FinishDate
        {
            set { _finishdate = value; }
            get { return _finishdate; }
        }

        private string _repairedresult;

        /// <summary>
        ///维修结果
        /// </summary>
        public string RepairedResult
        {
            set { _repairedresult = value; }
            get { return _repairedresult; }
        }

        private string _faultdescription;

        /// <summary>
        ///故障描述
        /// </summary>
        public string FaultDescription
        {
            set { _faultdescription = value; }
            get { return _faultdescription; }
        }

        private string _solution;

        /// <summary>
        ///结局方案
        /// </summary>
        public string Solution
        {
            set { _solution = value; }
            get { return _solution; }
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