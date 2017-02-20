using Lm.Eic.Framework.Authenticate.Model;
using Lm.Eic.Framework.Authenticate.Repository;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System.Collections.Generic;
using System.Linq;

namespace Lm.Eic.Framework.Authenticate.Business
{
    /// <summary>
    /// 模块管理器
    /// </summary>
    public class ModuleNavManager
    {
        private IModuleNavigationRepository irep = null;
        private RoleManager rm = null;

        public ModuleNavManager()
        {
            this.irep = new ModuleNavigationRepository();
            this.rm = new RoleManager();
        }

        private void SetPrimaryPropertyValue(ModuleNavigationModel mdl)
        {
            mdl.PrimaryKey = string.Format("{0}&{1}&{2}&{3}", mdl.AssemblyName, mdl.ModuleName, mdl.CtrlName, mdl.ActionName);
        }

        public List<ModuleNavigationModel> NavMneus
        {
            get { return this.irep.Entities.OrderByDescending(o => o.DisplayOrder).ToList(); }
        }

        public OpResult Store(ModuleNavigationModel entity, ModuleNavigationModel oldEntity, string opType)
        {
            OpResult result = OpResult.SetResult("待进行操作", false);
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
                result = Update(oldEntity, entity);
            }
            return result;
        }

        private OpResult Add(ModuleNavigationModel entity)
        {
            int record = 0;
            if (entity == null) return OpResult.SetResult("ModuleNavigationModel entity can't be null", false);
            SetPrimaryPropertyValue(entity);
            if (!irep.IsExist(e => e.PrimaryKey == entity.PrimaryKey))
            {
                record += irep.Insert(entity);
            }
            return OpResult.SetResult("新增模块数据成功", record > 0, entity.Id_Key);
        }

        private OpResult Delete(ModuleNavigationModel entity)
        {
            if (entity == null) return OpResult.SetResult("ModuleNavigationModel entity can't be null", false);
            SetPrimaryPropertyValue(entity);
            int record = irep.Delete(r => r.PrimaryKey == entity.PrimaryKey);
            if (record > 0)
            {
                //删除角色与模块匹配列表中对应的记录
                rm.MatchModuleHandler.Delete(entity.PrimaryKey);
            }
            return OpResult.SetResult("删除模块数据成功", record > 0);
        }

        public OpResult Update(ModuleNavigationModel oldEntity, ModuleNavigationModel mdl)
        {
            if (mdl == null) return OpResult.SetResult("mdl can't be null", false);
            int record = 0;
            SetPrimaryPropertyValue(oldEntity);
            SetPrimaryPropertyValue(mdl);
            //1.更新自身记录
            record = irep.Update(u => u.PrimaryKey == oldEntity.PrimaryKey, mdl);
            if (record > 0)
            {
                record += UpdateChildrenParentModuleTexts(record, oldEntity.ModuleText, mdl.ModuleText);
                //修改角色匹配模块记录中对应的信息
                record += rm.MatchModuleHandler.Update(oldEntity, mdl);
            }
            return OpResult.SetResult("修改成功！", record > 0);
        }

        /// <summary>
        /// 更新该节点的字节点集合的父文本标题
        /// </summary>
        /// <param name="record"></param>
        /// <param name="oldMdl"></param>
        /// <returns></returns>
        private int UpdateChildrenParentModuleTexts(int record, string moduleText, string newModuleText)
        {
            //更新该节点的字节点集合的父文本标题
            var childrenMdls = irep.Entities.Where(e => e.ParentModuleName == moduleText).ToList();
            if (childrenMdls != null && childrenMdls.Count > 0)
            {
                childrenMdls.ForEach(cm =>
                {
                    if (cm.ParentModuleName != newModuleText)
                        record += irep.Update(u => u.Id_Key == cm.Id_Key, u => new ModuleNavigationModel { ParentModuleName = newModuleText });
                });
            }
            return record;
        }

        /// <summary>
        /// 程序集处理器
        /// </summary>
        public AssemblyManager AssemblyHandler
        {
            get { return OBulider.BuildInstance<AssemblyManager>(); }
        }
    }

    /// <summary>
    /// 程序集管理器
    /// </summary>
    public class AssemblyManager
    {
        private IAssemblyRepository irep = null;

        public AssemblyManager()
        {
            this.irep = new AssemblyRepository();
        }

        public OpResult AddAssembly(AssemblyModel mdl)
        {
            if (!irep.IsExist(r => r.AssemblyName == mdl.AssemblyName))
            {
                int record = irep.Insert(mdl);
                return OpResult.SetResult("新增程序集成功！", record > 0);
            }
            else
            {
                return OpResult.SetResult("系统中已经存在此程序集数据", false);
            }
        }

        /// <summary>
        /// 程序集列表
        /// </summary>
        public List<AssemblyModel> AssemblyList
        {
            get { return irep.Entities.ToList(); }
        }

        public List<AssemblyModel> FindBy(string assemblyName)
        {
            return irep.Entities.Where(e => e.AssemblyName == assemblyName).ToList();
        }
    }
}