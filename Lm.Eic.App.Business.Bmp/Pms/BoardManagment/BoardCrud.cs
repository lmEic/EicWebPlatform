
using Lm.Eic.App.DbAccess.Bpm.Repository.PmsRep.BoardManager;
using Lm.Eic.App.DomainModel.Bpm.Pms.BoardManager;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.BoardManagment
{
    class BoardCrud : CrudBase<MaterialSpecBoardModel, IMaterialSpecBoardRepository>
    {
        public BoardCrud() : base(new MaterialSpecBoardRepository(), "物料规格看板")
        {
        }

        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }
    }
}
