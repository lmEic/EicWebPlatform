using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.HwCollaboration.Model
{
    /// <summary>
    ///华为协同数据传输实体模型
    /// </summary>
    [Serializable]
    public partial class HwCollaborationDataTransferModel
    {
        public HwCollaborationDataTransferModel()
        { }
        #region Model
        private string _opmodule;
        /// <summary>
        ///操作模块
        /// </summary>
        public string OpModule
        {
            set { _opmodule = value; }
            get { return _opmodule; }
        }
        private string _opcontent;
        /// <summary>
        ///操作内容
        /// </summary>
        public string OpContent
        {
            set { _opcontent = value; }
            get { return _opcontent; }
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

}
