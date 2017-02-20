using System;

namespace Lm.Eic.App.DomainModel.Mes.Optical.ProductWip
{
    /// <summary>
    /// Wip_ProductedWipData:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ProductedWipDataModel
    {
        public ProductedWipDataModel()
        { }

        #region Model

        /// <summary>
        ///
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime ProductDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ClassType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ProductStatus { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ProductBigStation { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ProductStation { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string WorkerID { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string WorkerName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string InDepartment { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FlowCardID { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int GoodCount { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime InputTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Field1 { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Field2 { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Field3 { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Field4 { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Field5 { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal Id_Key { get; set; }

        #endregion Model
    }

    /// <summary>
    /// Wip_NormalFlowStationSet:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class WipNormalFlowStationSetModel
    {
        public WipNormalFlowStationSetModel()
        { }

        #region Model

        /// <summary>
        ///
        /// </summary>
        public int FlowID { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ProductStation { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ProductStationDetail { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string StationDetailPrevious { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string StationSign { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int IsVisible { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ProductStatus { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal Id_Key { get; set; }

        #endregion Model
    }
}