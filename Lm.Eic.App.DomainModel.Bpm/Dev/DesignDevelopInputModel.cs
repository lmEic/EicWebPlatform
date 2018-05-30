using System;


namespace Lm.Eic.App.DomainModel.Bpm.Dev
{
   
    
     /// <summary>
     ///设计开发输入模形
     /// </summary>
     [Serializable]
     public partial class DesignDevelopInputModel
    {
        public DesignDevelopInputModel()
        { }
        #region Model
        private string _rdid;
        /// <summary>
        ///开发接单编号
        /// </summary>
        public string RdId
        {
            set { _rdid = value; }
            get { return _rdid; }
        }
        private string _sdid;
        /// <summary>
        ///发文编号
        /// </summary>
        public string SDId
        {
            set { _sdid = value; }
            get { return _sdid; }
        }
        private string _sdpreparer;
        /// <summary>
        ///发文人
        /// </summary>
        public string SDPreparer
        {
            set { _sdpreparer = value; }
            get { return _sdpreparer; }
        }
        private string _productname;
        /// <summary>
        ///品名
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        private string _productnamedescriptionattachmentpath;
        /// <summary>
        ///品名附件
        /// </summary>
        public string ProductNameDescriptionAttachmentPath
        {
            set { _productnamedescriptionattachmentpath = value; }
            get { return _productnamedescriptionattachmentpath; }
        }
        private string _productspecdescription;
        /// <summary>
        ///产品规格描述
        /// </summary>
        public string ProductSpecDescription
        {
            set { _productspecdescription = value; }
            get { return _productspecdescription; }
        }
        private string _productspecdescriptionattachmentpath;
        /// <summary>
        ///产品规格描述附件
        /// </summary>
        public string ProductSpecDescriptionAttachmentPath
        {
            set { _productspecdescriptionattachmentpath = value; }
            get { return _productspecdescriptionattachmentpath; }
        }
        private string _expectedproductionnumber;
        /// <summary>
        ///计划生产数量
        /// </summary>
        public string ExpectedProductionNumber
        {
            set { _expectedproductionnumber = value; }
            get { return _expectedproductionnumber; }
        }
        private string _sampledemandquantity;
        /// <summary>
        ///样品需求数量
        /// </summary>
        public string SampleDemandQuantity
        {
            set { _sampledemandquantity = value; }
            get { return _sampledemandquantity; }
        }
        private DateTime _demanddate;
        /// <summary>
        ///需求日期
        /// </summary>
        public DateTime DemandDate
        {
            set { _demanddate = value; }
            get { return _demanddate; }
        }
        private string _ishavetheclassproduct;
        /// <summary>
        ///是否有此类产品
        /// </summary>
        public string IsHaveTheClassProduct
        {
            set { _ishavetheclassproduct = value; }
            get { return _ishavetheclassproduct; }
        }
        private string _developmentdifficultylevel;
        /// <summary>
        ///开发难度
        /// </summary>
        public string DevelopmentDifficultyLevel
        {
            set { _developmentdifficultylevel = value; }
            get { return _developmentdifficultylevel; }
        }
        private string _productspecandequipmentisallready;
        /// <summary>
        ///开发规格与相关设备是否准备完成
        /// </summary>
        public string ProductSpecAndEquipmentIsAllReady
        {
            set { _productspecandequipmentisallready = value; }
            get { return _productspecandequipmentisallready; }
        }
        private string _productspecandequipmentallreadydescription;
        /// <summary>
        ///相关设备准备情况说明
        /// </summary>
        public string ProductSpecAndEquipmentAllReadyDescription
        {
            set { _productspecandequipmentallreadydescription = value; }
            get { return _productspecandequipmentallreadydescription; }
        }
        private string _isneedtryproduction;
        /// <summary>
        ///是否需试产
        /// </summary>
        public string IsNeedTryProduction
        {
            set { _isneedtryproduction = value; }
            get { return _isneedtryproduction; }
        }
        private string _needtryproductionnumber;
        /// <summary>
        ///试产数量
        /// </summary>
        public string NeedTryProductionNumber
        {
            set { _needtryproductionnumber = value; }
            get { return _needtryproductionnumber; }
        }
        private string _certifiedscrumproductowner;
        /// <summary>
        ///产品负责人
        /// </summary>
        public string CertifiedScrumProductOwner
        {
            set { _certifiedscrumproductowner = value; }
            get { return _certifiedscrumproductowner; }
        }
        private string _designatedperson;
        /// <summary>
        ///指派人
        /// </summary>
        public string DesignatedPerson
        {
            set { _designatedperson = value; }
            get { return _designatedperson; }
        }
        private string _scheduledcompletiondays;
        /// <summary>
        ///预计完成天数
        /// </summary>
        public string ScheduledCompletionDays
        {
            set { _scheduledcompletiondays = value; }
            get { return _scheduledcompletiondays; }
        }
        private string _thecostestimate;
        /// <summary>
        ///人与材料成本
        /// </summary>
        public string TheCostEstimate
        {
            set { _thecostestimate = value; }
            get { return _thecostestimate; }
        }
        private string _othersupplementdescription;
        /// <summary>
        ///其它数据描述
        /// </summary>
        public string OtherSupplementDescription
        {
            set { _othersupplementdescription = value; }
            get { return _othersupplementdescription; }
        }
        private string _jsonserializemodeldatas;
        /// <summary>
        ///序列化数据
        /// </summary>
        public string JsonSerializeModelDatas
        {
            set { _jsonserializemodeldatas = value; }
            get { return _jsonserializemodeldatas; }
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
        ///操作标识
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

}
