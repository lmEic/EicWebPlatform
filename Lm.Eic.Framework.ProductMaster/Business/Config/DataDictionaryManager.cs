using Lm.Eic.Framework.ProductMaster.DbAccess.Repository;
using Lm.Eic.Framework.ProductMaster.Model;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lm.Eic.Framework.ProductMaster.Business.Config
{
    public class DataDictionaryManager
    {
        private IConfigDataDictionaryRepository irep = null;

        public DataDictionaryManager()
        {
            this.irep = new ConfigDataDictionaryRepository();
        }

        #region  crud method
        public OpResult Store(ConfigDataDictionaryModel entity, ConfigDataDictionaryModel oldEntity, string opType)
        {
            OpResult result = OpResult.SetSuccessResult("待进行操作", false);
            if (opType == "add")
            {
                result = Add(entity);
            }
            else if (opType == "delete")
            {
                result = Delete(entity);
            }
            else if (opType == "edit")
            {
                result = Update(entity, oldEntity);
            }
            return result;
        }

        private void SetPrimaryPropertyValue(ConfigDataDictionaryModel mdl)
        {
            mdl.PrimaryKey = string.Format("{0}&{1}&{2}&{3}", mdl.TreeModuleKey, mdl.ModuleName, mdl.DataNodeName, mdl.AboutCategory);
        }

        public List<ConfigDataDictionaryModel> LoadConfigDatasBy(string moduleName, string aboutCategory)
        {
            try
            {
                var datas = this.irep.Entities.OrderBy(e => e.DisplayOrder).Where(e => e.ModuleName == moduleName && e.AboutCategory == aboutCategory).ToList();
           
                return datas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public List<ConfigDataDictionaryModel> FindConfigDatasBy(string treeModuleKey)
        {
            try
            {
                var datas = this.irep.Entities.OrderBy(e => e.DisplayOrder).Where(e => e.TreeModuleKey == treeModuleKey).ToList();             
                return datas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public List<ConfigDataDictionaryModel> FindConfigDatasBy(string treeModuleKey, string moduleName)
        {
            try
            {
                var datas = this.irep.Entities.OrderBy(e=> e.DisplayOrder).Where(e => e.TreeModuleKey == treeModuleKey && e.ModuleName == moduleName).ToList();            
                return datas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="treeModuleKey"></param>
        /// <param name="moduleName"></param>
        /// <param name="dataNodeName"></param>
        /// <returns></returns>
        public List<ConfigDataDictionaryModel> GetConfigDataDepartment(string treeModuleKey, string moduleName, string dataNodeName)
        {
            try
            {
                string parentDeparmentText = "";
                var datasList = this.irep.Entities.Where(e => e.TreeModuleKey == treeModuleKey && e.ModuleName == moduleName && e.DataNodeName== dataNodeName).ToList();
                foreach (var item in datasList)
                {
                    parentDeparmentText = item.DataNodeText;
                }                
                var getdatas= this.irep.Entities.Where(e => e.TreeModuleKey == treeModuleKey && e.ModuleName == moduleName && e.ParentDataNodeText== parentDeparmentText).ToList();
                if(getdatas.Count==0)
                {
                    var datas = this.irep.Entities.Where(e => e.TreeModuleKey == treeModuleKey && e.ModuleName == moduleName && e.DataNodeName == dataNodeName).ToList();
                    return datas;
                }
                else
                {
                    var datas = this.irep.Entities.Where(e => e.TreeModuleKey == treeModuleKey && e.ModuleName == moduleName && e.ParentDataNodeText == parentDeparmentText).ToList();
                    return datas;
                }            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private OpResult Add(ConfigDataDictionaryModel entity)
        {
            int record = 0;
            if (entity == null) return OpResult.SetSuccessResult("ConfigDataDictionaryModel entity can't be null", false);
            SetPrimaryPropertyValue(entity);
            if (!irep.IsExist(e => e.PrimaryKey == entity.PrimaryKey))
            {
                record += irep.Insert(entity);
            }
            return OpResult.SetSuccessResult("新增配置数据成功", record > 0, entity.Id_Key);
        }

        private OpResult Delete(ConfigDataDictionaryModel entity)
        {
            if (entity == null) return OpResult.SetSuccessResult("ConfigDataDictionaryModel entity can't be null", false);
            SetPrimaryPropertyValue(entity);
            int record = irep.Delete(r => r.PrimaryKey == entity.PrimaryKey);
            return OpResult.SetSuccessResult("删除配置数据成功", record > 0);
        }

        public OpResult Update(ConfigDataDictionaryModel mdl, ConfigDataDictionaryModel oldMdl)
        {
            if (mdl == null) return OpResult.SetSuccessResult("mdl can't be null", false);
            int record = 0;
            SetPrimaryPropertyValue(mdl);
            SetPrimaryPropertyValue(oldMdl);
            //1.更新自身记录
            record = irep.Update(u => u.Id_Key == oldMdl.Id_Key, mdl);
            return OpResult.SetSuccessResult("修改配置成功！", record > 0);
        }
        #endregion
    }
}