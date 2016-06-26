using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.Framework.Authenticate.Model
{
   /// <summary>
   /// 程序集信息模型
   /// </summary>
   public class AssemblyModel
    {
       public AssemblyModel()
		{}
		#region Model
		private string _assemblyname;
		private string _assemblyText;
		private decimal _id_key;
		/// <summary>
		/// 程序集名称
		/// </summary>
		public string AssemblyName
		{
			set{ _assemblyname=value;}
			get{return _assemblyname;}
		}
		/// <summary>
		/// 系统名称
		/// </summary>
		public string AssemblyText
		{
			set{ _assemblyText=value;}
			get{return _assemblyText;}
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
   /// Authen_ModuleNavigation:实体类(属性说明自动提取数据库字段的描述信息)
   /// </summary>
   [Serializable]
   public partial class ModuleNavigationModel
   {
       public ModuleNavigationModel()
       { }
       #region Model
       private string _assemblyname;
       private string _modulename;
       private string _moduletext;
       private string _parentmodulename;
       private int _atlevel;
       private string _ctrlname;
       private string _actionname;
       private string _uiserf;
       private string _templateid;
       private string _icon;
       private string _clientmode;
       private string _moduletype;
       private int? _displayorder;
       private string _primarykey;
       private decimal _id_key;
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
       public string ParentModuleName
       {
           set { _parentmodulename = value; }
           get { return _parentmodulename; }
       }
       /// <summary>
       /// 
       /// </summary>
       public int AtLevel
       {
           set { _atlevel = value; }
           get { return _atlevel; }
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
       public string UiSerf
       {
           set { _uiserf = value; }
           get { return _uiserf; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string TemplateID
       {
           set { _templateid = value; }
           get { return _templateid; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string Icon
       {
           set { _icon = value; }
           get { return _icon; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string ClientMode
       {
           set { _clientmode = value; }
           get { return _clientmode; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string ModuleType
       {
           set { _moduletype = value; }
           get { return _moduletype; }
       }
       /// <summary>
       /// 
       /// </summary>
       public int? DisplayOrder
       {
           set { _displayorder = value; }
           get { return _displayorder; }
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
       public decimal Id_Key
       {
           set { _id_key = value; }
           get { return _id_key; }
       }
       #endregion Model
   }

   public class NavMenuModel
   {
       /// <summary>
       /// 名称
       /// </summary>
       public string Name { get; set; }
       /// <summary>
       /// 标题
       /// </summary>
       public string Text { get; set; }
       /// <summary>
       /// 菜单项
       /// </summary>
       public ModuleNavigationModel Item { get; set; }
       /// <summary>
       /// 所在层级
       /// </summary>
       public int AtLevel { get; set; }
       /// <summary>
       /// 子菜单
       /// </summary>
       public List<ModuleNavigationModel> Childrens { get; set; }
   }


}
