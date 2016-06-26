using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.Framework.Authenticate.Model
{
   /// <summary>
   /// 用户注册信息模型
   /// </summary>
   public class RegistUserModel
    {
       public RegistUserModel()
		{}
		#region Model
		private string _userid;
		private string _username;
		private string _password;
		private string _currentstatus;
		private string _memo;
		private decimal _id_key;
		/// <summary>
		/// 
		/// </summary>
		public string UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CurrentStatus
		{
			set{ _currentstatus=value;}
			get{return _currentstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Id_Key
		{
			set{ _id_key=value;}
			get{return _id_key;}
		}
		#endregion Model
    }
   /// <summary>
   /// 用户组织信息模型
   /// </summary>
   public class UserOrganizeModel
   {
       private string _workerid;
       private string _name;
       private string _department;
       private string _post;
       private string _postnature;
       private byte[] _personalpicture;

       /// <summary>
       /// 
       /// </summary>
       public string WorkerId
       {
           set { _workerid = value; }
           get { return _workerid; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string Name
       {
           set { _name = value; }
           get { return _name; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string Department
       {
           set { _department = value; }
           get { return _department; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string Post
       {
           set { _post = value; }
           get { return _post; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string PostNature
       {
           set { _postnature = value; }
           get { return _postnature; }
       }
       /// <summary>
       /// 
       /// </summary>
       public byte[] PersonalPicture
       {
           set { _personalpicture = value; }
           get { return _personalpicture; }
       }
   }
   /// <summary>
   /// 登陆模型
   /// </summary>
   public class LoginModel
   {
       /// <summary>
       /// 帐号
       /// </summary>
       public string UserId { get; set; }
       /// <summary>
       /// 用户密码
       /// </summary>
       public string Password { get; set; }
   }

   /// <summary>
   /// 登陆者身份数据信息
   /// </summary>
   public class IdentityInfo
   {
       /// <summary>
       /// 注册的用户信息
       /// </summary>
       public RegistUserModel RegistUser { get; set; }
       /// <summary>
       /// 登录状态
       /// </summary>
       public LoginStatus LoginStatus { get; set; }
       /// <summary>
       /// 用户包含的角色信息
       /// </summary>
       public List<RoleModel> MatchRoleList { get; set; }
       /// <summary>
       /// 用户包含的模块使用权限
       /// </summary>
       public List<ModuleNavigationModel> MatchModulePowerList { get; set; }
       /// <summary>
       /// 是否拥有访问权限
       /// </summary>
       /// <param name="actionName"></param>
       /// <param name="ctrlName"></param>
       /// <returns></returns>
       public bool IsHasAsccessPower(string actionName, string ctrlName)
       {
           if (this.MatchModulePowerList == null) return false;
           return this.MatchModulePowerList.FirstOrDefault(e => e.ActionName == actionName && e.CtrlName == ctrlName) != null;
       }
   }
   /// <summary>
   /// 登录状态
   /// </summary>
   public class LoginStatus
   {
       /// <summary>
       /// 状态码
       /// 0:登录成功
       /// 1:用户不存在
       /// 2:密码错误
       /// </summary>
       public int StatusCode { get; set; }
       /// <summary>
       /// 状态信息
       /// </summary>
       public string StatusMessage 
       {
           get
           {
               string message = string.Empty;
               if (StatusCode == 1)
               {
                   message = "对不起，您没有注册系统!";
               }
               else if (StatusCode == 2)
               {
                   message = "对不起，您的密码错误!";
               }
               return message;
           }
       }
   }
}
