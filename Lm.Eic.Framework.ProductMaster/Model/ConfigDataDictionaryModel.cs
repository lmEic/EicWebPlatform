using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lm.Eic.Framework.ProductMaster.Model
{
    /// <summary>
    /// 数据字典配置模型
    /// </summary>
    public class ConfigDataDictionaryModel
    {
        public ConfigDataDictionaryModel()
        { }
        #region Model
        private string _treemodulekey;
        private string _modulename;
        private string _datanodename;
        private string _datanodetext;
        private string _parentdatanodetext;
        private int _ishaschildren;
        private int _atlevel;
        private string _aboutcategory;
        private string _icon;
        private int? _displayorder;
        private string _primarykey;
        private string _memo;
        private decimal _id_key;
        /// <summary>
        /// 
        /// </summary>
        public string TreeModuleKey
        {
            set { _treemodulekey = value; }
            get { return _treemodulekey; }
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
        public string DataNodeName
        {
            set { _datanodename = value; }
            get { return _datanodename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DataNodeText
        {
            set { _datanodetext = value; }
            get { return _datanodetext; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ParentDataNodeText
        {
            set { _parentdatanodetext = value; }
            get { return _parentdatanodetext; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsHasChildren
        {
            set { _ishaschildren = value; }
            get { return _ishaschildren; }
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
        public string AboutCategory
        {
            set { _aboutcategory = value; }
            get { return _aboutcategory; }
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
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
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

    /// <summary>
    ///文件路径配置模型
    /// </summary>
    [Serializable]
    public partial class ConfigFilePathModel
    {
        public ConfigFilePathModel()
        { }
        #region Model
        private string _directorypath;
        /// <summary>
        ///文件夹路径
        /// </summary>
        public string DirectoryPath
        {
            set { _directorypath = value; }
            get { return _directorypath; }
        }
        private string _filename;
        /// <summary>
        ///文件名称
        /// </summary>
        public string FileName
        {
            set { _filename = value; }
            get { return _filename; }
        }
        private string _assemblyname;
        /// <summary>
        ///程序集名称
        /// </summary>
        public string AssemblyName
        {
            set { _assemblyname = value; }
            get { return _assemblyname; }
        }
        private string _modulekey;
        /// <summary>
        ///模块键值
        /// </summary>
        public string ModuleKey
        {
            set { _modulekey = value; }
            get { return _modulekey; }
        }
        private string _description;
        /// <summary>
        ///说明
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
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
    /// 文件路径模型的扩展方法
    /// </summary>
    public static class ConfigFilePathModelExtension
    {
        /// <summary>
        /// 根据文件模型生成文件全路径
        /// </summary>
        /// <param name="filePathInfo"></param>
        /// <returns></returns>
        public static string CreateFullFileName(this ConfigFilePathModel filePathInfo)
        {
           return Path.Combine(filePathInfo.DirectoryPath, filePathInfo.FileName);
        }
        /// <summary>
        /// 文件夹
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public static bool DirectoryExist(this string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            return true;
        }
        /// <summary>
        /// 检测文件名称是否存在
        /// </summary>
        /// <param name="fullFileName"></param>
        /// <returns></returns>
        public static bool FileExist(this string fullFileName)
        {
            return File.Exists(fullFileName);
        }
    }

}
