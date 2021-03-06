﻿using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Archives;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.Framework.ProductMaster.Business.Config;
using Lm.Eic.Framework.ProductMaster.Model;
using System.Collections.Generic;
using System.Linq;

namespace Lm.Eic.App.Business.Bmp.Hrm.Archives
{
    public class ArDepartmentManager : ArchiveBase
    {
        #region member

        private IArDepartmentChangeLibRepository irep = null;

        #endregion member

        #region constructure

        public ArDepartmentManager()
        {
            this.irep = new ArDepartmentChangeLibRepository();
        }

        #endregion constructure

        #region property

        public List<ConfigDataDictionaryModel> Departments
        {
            get { return PmConfigService.DataDicManager.FindConfigDatasBy("Organization"); }
        }

        #endregion property

        #region method
        /// <summary>
        /// 初次修改部门信息(档案编辑入口)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal int Edit(ArDepartmentChangeLibModel entity)
        {
            int record = 0;
            var posts = this.irep.Entities.Where(e => e.WorkerId == entity.WorkerId).ToList();
            if (posts != null && posts.Count == 1)
            {
                record = this.irep.Update(e => e.WorkerId == entity.WorkerId, u => new ArDepartmentChangeLibModel
                {
                    NowDepartment = entity.NowDepartment,
                    OldDepartment = entity.OldDepartment,
                });
            }
            return record;
        }
        /// <summary>
        /// 初始化员工部门数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal int InitDepartment(ArDepartmentChangeLibModel entity)
        {
            int record = 0;
            if (!this.irep.IsExist(e => e.WorkerId == entity.WorkerId))
            {
                record = this.irep.Insert(entity);
            }
            return record;
        }

        /// <summary>
        /// 变动部门信息
        /// </summary>
        /// <param name="changeEntity"></param>
        /// <returns></returns>
        public int ChangeRecord(ArDepartmentChangeLibModel changeEntity, out int changeRecord)
        {
            changeRecord = 0;
            int record = 0;
            if (changeEntity == null) return record;
            var departments = this.irep.Entities.Where(e => e.WorkerId == changeEntity.WorkerId).OrderByDescending(o => o.Id_Key).ToList();
            if (departments != null && departments.Count > 0)
            {
                if (changeEntity.OpSign == "edit")
                {
                    var dep = departments[0];
                    record = this.irep.Update(e => e.Id_Key == dep.Id_Key, u => new ArDepartmentChangeLibModel
                    {
                        NowDepartment = changeEntity.NowDepartment,
                        OpPerson = changeEntity.OpPerson
                    });
                }
                else if (changeEntity.OpSign == "change")
                {
                    changeRecord = departments.Count;
                    departments.ForEach(d =>
                    {
                        record += this.irep.Update(e => e.Id_Key == d.Id_Key, u => new ArDepartmentChangeLibModel
                        {
                            InStatus = "Out"
                        });
                    });
                    changeEntity.InStatus = "In";
                    record = this.irep.Insert(changeEntity);
                    changeRecord = changeRecord + record;
                }
            }
            return record;
        }

        /// <summary>
        /// 根据部门编码获取部门名称
        /// </summary>
        /// <param name="departmentCode"></param>
        /// <returns></returns>
        public string GetDepartmentText(string departmentCode)
        {
            var dep = this.Departments.FirstOrDefault(e => e.DataNodeName == departmentCode);
            string text = dep == null ? "" : dep.DataNodeText;
            return text;
        }

        /// <summary>
        /// 根据部门名称或者部门编码
        /// </summary>
        /// <param name="departmentText"></param>
        /// <returns></returns>
        public string GetDepartmentCode(string departmentText)
        {
            var dep = this.Departments.FirstOrDefault(e => e.DataNodeText == departmentText);
            string code = dep == null ? "" : dep.DataNodeName;
            return code;
        }
        #endregion method
    }
}