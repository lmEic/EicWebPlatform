using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Archives;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using System.Linq;

namespace Lm.Eic.App.Business.Bmp.Hrm.Archives
{
    public class ArPostManager : ArchiveBase
    {
        #region member

        private IArPostChangeLibRepository irep = null;

        #endregion member

        #region constructure

        public ArPostManager()
        {
            this.irep = new ArPostChangeLibRepository();
        }

        #endregion constructure



        #region method

        /// <summary>
        /// 变动岗位信息
        /// </summary>
        /// <param name="changeEntity"></param>
        /// <returns></returns>
        public int ChangeRecord(ArPostChangeLibModel changeEntity, out int changeRecord)
        {
            changeRecord = 0;
            int record = 0;
            if (changeEntity == null) return record;
            var Posts = this.irep.Entities.Where(e => e.WorkerId == changeEntity.WorkerId).OrderByDescending(o => o.Id_Key).ToList();
            if (Posts != null && Posts.Count > 0)
            {
                if (changeEntity.OpSign == "edit")
                {
                    var dep = Posts[0];
                    record = this.irep.Update(e => e.Id_Key == dep.Id_Key, u => new ArPostChangeLibModel
                    {
                        NowPost = changeEntity.NowPost,
                        OpPerson = changeEntity.OpPerson
                    });
                }
            }
            if (changeEntity.OpSign == "change")
            {
                if (Posts != null && Posts.Count > 0)
                {
                    changeRecord = Posts.Count;
                    Posts.ForEach(d =>
                    {
                        record += this.irep.Update(e => e.Id_Key == d.Id_Key, u => new ArPostChangeLibModel
                        {
                            InStatus = "Out"
                        });
                    });
                }
                changeEntity.InStatus = "In";
                record = this.irep.Insert(changeEntity);
                changeRecord = changeRecord + record;
            }
            return record;
        }

        #endregion method
    }
}