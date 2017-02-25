using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Archives;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using System.Collections.Generic;
using System.Linq;

namespace Lm.Eic.App.Business.Bmp.Hrm.Archives
{
    public class ArStudyManager
    {
        #region member

        private IArStudyRepository irep = null;

        #endregion member

        #region constructure

        public ArStudyManager()
        {
            this.irep = new ArStudyRepository();
        }

        #endregion constructure

        #region method

        /// <summary>
        ///  插入数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal int Insert(ArStudyModel entity)
        {
            int record = 0;
            if (!irep.IsExist(e => e.WorkerId == entity.WorkerId && e.SchoolName == entity.SchoolName))
                record = irep.Insert(entity);
            return record;
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="oldEntity"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal int Edit(ArStudyModel oldEntity, ArStudyModel entity)
        {
            var mdl = this.irep.Entities.FirstOrDefault(e => e.WorkerId == oldEntity.WorkerId && e.SchoolName == oldEntity.SchoolName);
            if (mdl != null) entity.Id_Key = mdl.Id_Key;
            return irep.Update(u => u.WorkerId == oldEntity.WorkerId && u.SchoolName == oldEntity.SchoolName, mdl);
        }

        /// <summary>
        /// 修改学习信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="changeEntity"></param>
        /// <returns></returns>
        internal int ChangeStudy(ArStudyModel entity)
        {
            int record = 0;
            if (entity == null) return record;
            //1.若是添加，则直接添加
            if (entity.OpSign.Trim().ToLower() == "add")
            {
                entity.WorkingStatus = "在职";
                record = Insert(entity);
            }
            else if (entity.OpSign.Trim().ToLower() == "edit") //2.若是修改，则先查询是否存在，
            {
                var mdl = this.irep.Entities.FirstOrDefault(e => e.Id_Key == entity.Id_Key);
                if (mdl != null)
                {
                    //若存在，则直接修改
                    record = irep.Update(u => u.Id_Key == entity.Id_Key, entity);
                }
                else
                {
                    entity.WorkingStatus = "在职";
                    //否则，直接添加
                    record = Insert(entity);
                }
            }
            return record;
        }

        /// <summary>
        /// 根据作业工号查询相应的学习信息数据
        /// </summary>
        /// <param name="workerIdList"></param>
        /// <returns></returns>
        public List<ArStudyModel> FindBy(List<string> workerIdList)
        {
            if (workerIdList == null || workerIdList.Count == 0) return null;
            return this.irep.Entities.Where(e => workerIdList.Contains(e.WorkerId)).ToList();
        }

        #endregion method
    }
}