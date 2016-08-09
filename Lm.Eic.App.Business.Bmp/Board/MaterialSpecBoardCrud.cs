using Lm.Eic.App.DbAccess.Bpm.Repository.BoardRep;
using Lm.Eic.App.DomainModel.Bpm.Board;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Board
{
    class MaterialSpecBoardCrud : CrudBase<MaterialSpecBoardModel, IMaterialSpecBoardRepository>
    {
        public MaterialSpecBoardCrud() : base(new MaterialSpecBoardRepository(), "物料规格看板")
        {
        }

        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }
    }
}
