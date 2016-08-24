
using Lm.Eic.App.DbAccess.Bpm.Repository.PmsRep.BoardManagment;
using Lm.Eic.App.DomainModel.Bpm.Pms.BoardManagment;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lm.Eic.App.Business.Bmp.Pms.BoardManagment
{

    internal class BorardCrudFactory
   {
        /// <summary>
        /// 物料看板ＣＲＵＤ
        /// </summary>
       public static MaterialBoardCrud MaterialBoardCrud
        {
            get { return OBulider.BuildInstance<MaterialBoardCrud>(); }
        }
   }


  /// <summary>
  ///物料看板CRUD
  /// </summary>
  public  class MaterialBoardCrud : CrudBase<MaterialSpecBoardModel, IMaterialSpecBoardRepository>
    {
        public MaterialBoardCrud() : base(new MaterialSpecBoardRepository(), "物料规格看板") { }

        protected override void AddCrudOpItems()
        {
            AddOpItem(OpMode.Add, AddMaterialBoard);
            AddOpItem(OpMode.Edit, UpdateMaterialBoard);
            
        }
          /// <summary>
        /// 修改数据仓库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override OpResult Store(MaterialSpecBoardModel model)
        {
            return this.PersistentDatas(model);
        }

        private OpResult AddMaterialBoard( MaterialSpecBoardModel model)
        {
            ///判断产品品号是否存在
           if (irep.IsExist (e=>e.ProductID==model.ProductID ) )
               return OpResult.SetResult("此产品号已存在！");

            return irep.Insert(model).ToOpResult("添加新看板成功");
        }

        private OpResult UpdateMaterialBoard(MaterialSpecBoardModel model)
        {
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt("修改完成"); 
        }

       

        #region Find

        /// <summary>
        ///  通过产品品号查找看板信息
        /// </summary>
        /// <param name="productId">产品品号</param>
        /// <returns></returns>
        public MaterialSpecBoardModel  FindMaterialSpecBoardBy(string productId)
        {
            try
            {
                return irep.Entities.Where(m => m.ProductID == productId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        #endregion
    }
}
