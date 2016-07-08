using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Archives;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using System.Collections.Generic;
using System.Linq;

namespace Lm.Eic.App.Business.Bmp.Hrm.Archives
{
    public class ArTelManager
    {
        #region member

        private IArTelRepository irep = null;

        #endregion member

        #region constructure

        public ArTelManager()
        {
            this.irep = new ArTelRepository();
        }

        #endregion constructure



        #region method

        /// <summary>
        ///  插入数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal int Insert(ArTelModel entity)
        {
            int record = 0;
            if (!irep.IsExist(e => e.WorkerId == entity.WorkerId))
                record = irep.Insert(entity);
            return record;
        }

        internal int Edit(ArTelModel oldEntity, ArTelModel entity)
        {
            var mdl = this.irep.Entities.FirstOrDefault(e => e.WorkerId == oldEntity.WorkerId);
            if (mdl != null) entity.Id_Key = mdl.Id_Key;
            return irep.Update(u => u.WorkerId == oldEntity.WorkerId, mdl);
        }

        /// <summary>
        /// 修改联系方式信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="changeEntity"></param>
        /// <returns></returns>
        internal int ChangeTelInfo(ArTelModel entity, out ArTelModel changeEntity)
        {
            int record = 0;
            changeEntity = entity;
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
                    record = irep.Update(e => e.Id_Key == entity.Id_Key, entity);
                }
                else
                {
                    entity.WorkingStatus = "在职";
                    //否则，直接添加
                    record = Insert(entity);
                }
            }
            //输出新操作后实体模型
            changeEntity = entity;
            return record;
        }

        /// <summary>
        /// 根据作业工号查询相应的联系方式数据
        /// </summary>
        /// <param name="workerIdList"></param>
        /// <returns></returns>
        public List<ArTelModel> FindBy(List<string> workerIdList)
        {
            if (workerIdList == null || workerIdList.Count == 0) return null;
            return this.irep.Entities.Where(e => workerIdList.Contains(e.WorkerId)).ToList();
        }

        #endregion method
    }
}