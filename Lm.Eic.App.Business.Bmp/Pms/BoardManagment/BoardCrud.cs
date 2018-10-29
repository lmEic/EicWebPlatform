﻿using Lm.Eic.App.Business.Bmp.Quality.InspectionManage;
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
            AddOpItem(OpMode.Edit, EditMaterialBoard);
            
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
               return OpResult.SetErrorResult("此产品号已存在！");

            return irep.Insert(model).ToOpResult("添加新看板成功");
        }

        private OpResult EditMaterialBoard(MaterialSpecBoardModel model)
        {
            var board = FindMaterialSpecBoardBy(model.ProductID);
            model.Id_Key = board.Id_Key;
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt("修改完成");
        }


        /// <summary>
        /// 审核看板
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult AuditMaterialBoard(MaterialSpecBoardModel model)
        {
            DateTime opDate = DateTime.Now;
            model.State = InspectionConstant.InspectionStatus.HaveCheck;
            model.OpSign = OpMode.Edit;
            return Store(model);

            //return irep.Update(u => u.Id_Key == model.Id_Key,m=> new MaterialSpecBoardModel
            //{
            //    State= "已审核",
            //    OpPerson= model.OpPerson,
            //    OpDate = opDate.ToDate(),
            //    OpTime = opDate,
            //    OpSign = "update"
            //}).ToOpResult_Eidt("审核完成");
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

                return irep.Entities.Where(m => m.ProductID == productId).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 获取待审核的看板列表
        /// </summary>
        /// <returns></returns>
        public List<MaterialSpecBoardModel> GetWaittingAuditBoardList()
        {
            try
            {
                var tem = irep.Entities.Where(m => m.State == InspectionConstant.InspectionStatus.WaitCheck).ToList();
                return tem;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        #endregion
    }
}
