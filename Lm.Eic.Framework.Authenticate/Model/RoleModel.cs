using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.Framework.Authenticate.Model
{
    /// <summary>
    ///角色对象模型
    /// </summary>
    [Serializable]
    public partial class RoleModel
    {
        public RoleModel()
        { }
        #region Model
        private string _roleid;
        /// <summary>
        ///角色编号
        /// </summary>
        public string RoleId
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        private string _rolename;
        /// <summary>
        ///角色名称
        /// </summary>
        public string RoleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }
        private int _rolelevel;
        /// <summary>
        ///等级
        /// </summary>
        public int RoleLevel
        {
            set { _rolelevel = value; }
            get { return _rolelevel; }
        }
        private string _rolegroupname;
        /// <summary>
        ///角色群组名称
        /// </summary>
        public string RoleGroupName
        {
            set { _rolegroupname = value; }
            get { return _rolegroupname; }
        }
        private string _parentname;
        /// <summary>
        ///父对象
        /// </summary>
        public string ParentName
        {
            set { _parentname = value; }
            get { return _parentname; }
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
   /// 用户角色匹配模型
   /// </summary>
   [Serializable]
   public partial class UserMatchRoleModel
   {
       public UserMatchRoleModel()
       { }
       #region Model
       private string _userid;
       private string _roleid;
       private string _opsign;
       private decimal _id_key;
       /// <summary>
       /// 用户ID
       /// </summary>
       public string UserId
       {
           set { _userid = value; }
           get { return _userid; }
       }
       /// <summary>
       /// 角色ID
       /// </summary>
       public string RoleID
       {
           set { _roleid = value; }
           get { return _roleid; }
       }
       /// <summary>
       /// 操作标志
       /// </summary>
       public string OpSign
       {
           set { _opsign = value; }
           get { return _opsign; }
       }
       /// <summary>
       /// 自增键
       /// </summary>
       public decimal Id_Key
       {
           set { _id_key = value; }
           get { return _id_key; }
       }
       #endregion Model

   }

   /// <summary>
   /// 角色与模块匹配信息模型
   /// </summary>
   [Serializable]
   public partial class RoleMatchModuleModel
   {
       public RoleMatchModuleModel()
       { }
       #region Model
       private string _roleid;
       private string _assemblyname;
       private string _modulename;
       private string _moduletext;
       private string _ctrlname;
       private string _actionname;
       private string _primarykey;
       private string _modulenavprimarykey;
       private string _opsign;
       private decimal _id_key;
       /// <summary>
       /// 
       /// </summary>
       public string RoleId
       {
           set { _roleid = value; }
           get { return _roleid; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string AssemblyName
       {
           set { _assemblyname = value; }
           get { return _assemblyname; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string ModuleName
       {
           set { _modulename = value; }
           get { return _modulename; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string ModuleText
       {
           set { _moduletext = value; }
           get { return _moduletext; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string CtrlName
       {
           set { _ctrlname = value; }
           get { return _ctrlname; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string ActionName
       {
           set { _actionname = value; }
           get { return _actionname; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string PrimaryKey
       {
           set { _primarykey = value; }
           get { return _primarykey; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string ModuleNavPrimaryKey
       {
           set { _modulenavprimarykey = value; }
           get { return _modulenavprimarykey; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string OpSign
       {
           set { _opsign = value; }
           get { return _opsign; }
       }
       /// <summary>
       /// 
       /// </summary>
       public decimal Id_Key
       {
           set { _id_key = value; }
           get { return _id_key; }
       }
       #endregion Model

   }


}
