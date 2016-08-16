using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Pms.BoardManager
{
    /// <summary>
    ///物料规格看板
    /// </summary>
    [Serializable]
    public partial class MaterialSpecBoardModel
    {
        public MaterialSpecBoardModel()
        { }
        #region Model
        private string _productid;
        /// <summary>
        ///产品品号
        /// </summary>
        public string ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        private string _materialid;
        /// <summary>
        ///料号
        /// </summary>
        public string MaterialID
        {
            set { _materialid = value; }
            get { return _materialid; }
        }
        private byte[] _drawing;
        /// <summary>
        ///图纸
        /// </summary>
        public byte[] Drawing
        {
            set { _drawing = value; }
            get { return _drawing; }
        }
        private string _remarks;
        /// <summary>
        ///备注
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
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
    //
    public partial class MaterialBomModel
    {
        public  MaterialBomModel()
        { }
        #region
        //主件品号
        public string MainMaterailId
        { set; get; }
        // 阶  次
        public string MaterailStep
        {
            set;
            get;
        }
        // 元件品号
        public string MaterailNum
        {
            set;
            get;
        }
        //  属    性
        public string Material
        {
            set;
            get;
        }
        // 品     名
        public string MaterailName
        {
            set;
            get;
        }
        // 规     格
        public string MaterialSpec
        {
            set;
            get;
        }
        //  单位
        public string Uint
        {
            set;
            get;
        }
        //组成用量
        public double Number
        {
            set;
            get;
        }
        //底数

        #endregion
    }
}
