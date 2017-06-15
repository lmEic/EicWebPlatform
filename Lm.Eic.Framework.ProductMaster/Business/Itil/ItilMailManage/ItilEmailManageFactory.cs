using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;



namespace Lm.Eic.Framework.ProductMaster.Business.Itil.ItilMailManage
{
    ///// <summary>
    ///// 邮箱管理工厂
    ///// </summary>
    public static class ItilEmailManageFactory
    {
        /// <summary>
        /// 邮箱管理Crud
        /// </summary>
        internal static ItilEmailCrud ItilEmailCrud
        {
            get { return OBulider.BuildInstance<ItilEmailCrud>(); }
        }
    }


    ///// <summary>
    ///// 邮箱管理工厂
    ///// </summary>
    ////internal class ItilEmailManageFactory
    ////{
    ////    public static ItilEmailManageCrud ItilEmailManageCrud
    ////    {
    ////        get { return OBulider.BuildInstance<ItilEmailManageCrud>(); }
    ////    }

    ////}
    //public class IitilEmailManageer
    //{

    //}
    ///// <summary>
    ///// 邮箱管理Crud
    ///// </summary>
    //internal class ItilEmailManageFactory : CrudBase<ItilEmailManageModel, IItilEmailManageRepository>
    //{
    //    public ItilEmailManageFactory() : base(new ItilEmailManageRepository(), "邮箱管理")
    //    { }

    //    protected override void AddCrudOpItems()
    //    {
    //        this.AddOpItem(OpMode.Add,AddEmailManageRecord);
    //        this.AddOpItem(OpMode.Edit, EditEmailManageRecord);
    //        this.AddOpItem(OpMode.UpDate, UpDateEmailManageRecord);
    //    }

    //    private OpResult UpDateEmailManageRecord(ItilEmailManageModel model)
    //    {
    //        OpResult result = OpResult.SetErrorResult("未执行任何修改");
    //        if (model == null) return result;
    //        return result;
    //    }

    //    private OpResult EditEmailManageRecord(ItilEmailManageModel model)
    //    {
    //        return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt(model.Email.ToString());
    //    }

    //    private OpResult AddEmailManageRecord(ItilEmailManageModel model)
    //    {
    //        if (irep.IsExist(m => m.Email == model.Email))
    //        {
    //            return OpResult.SetErrorResult("邮箱帐号己存在！");
    //        }
    //        return irep.Insert(model).ToOpResult_Add("邮箱用户",model.Id_Key);
    //    }

    //    public List<ItilEmailManageModel>FindBy(ItilEmailManageDto dto)
    //    {
    //        if (dto == null) return new  List<ItilEmailManageModel>();
    //        try
    //        {
    //            switch (dto.SearchMode)
    //            {
    //                case 1:
    //                    return irep.Entities.Where(m => m.WorkerId == dto.WorkerId).ToList();                  
    //                case 2:
    //                    return irep.Entities.Where(m => m.Email == dto.Email).ToList();
    //                default:
    //                    return new List<ItilEmailManageModel>();
    //            }


    //        }
    //        catch (Exception ex)
    //        {

    //            throw new Exception(ex.Message);
    //        }
    //    }
    //}
}
