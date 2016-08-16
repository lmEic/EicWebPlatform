
using Lm.Eic.App.DbAccess.Bpm.Repository.PmsRep.BoardManager;
using Lm.Eic.App.DomainModel.Bpm.Pms.BoardManager;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.BoardManagment
{
 
   internal class BorardCrudFactory
   {
       public static   BoardCrud  BoardCrud
       {
           get {return OBulider.BuildInstance <BoardCrud >();}
       }
   }
  public  class BoardCrud : CrudBase<MaterialSpecBoardModel, IMaterialSpecBoardRepository>
    {
        public BoardCrud() : base(new MaterialSpecBoardRepository(), "物料规格看板")
        {
        }

        protected override void AddCrudOpItems()
        {
            AddOpItem(OpMode.Add, AddChangeRecord);
            AddOpItem(OpMode.Edit, EditChangeRecord);
            
        }

        private OpResult AddChangeRecord( MaterialSpecBoardModel model)
        {
            return null;
        }
        private OpResult EditChangeRecord(MaterialSpecBoardModel model)
        {
            return null;
        }

        #region Find

      /// <summary>
      ///  通过物料查找看板信息
      /// </summary>
      /// <param name="materialID">料号</param>
      /// <returns></returns>
        public List<MaterialSpecBoardModel> FindMaterialSpecBoardBy(string materialID)
        {
            try
            {
                return irep.Entities.Where(m => m.MaterialID == materialID).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        #endregion
    }
}
