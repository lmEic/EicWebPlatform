using Lm.Eic.Framework.ProductMaster.DbAccess.Repository;
using Lm.Eic.Framework.ProductMaster.Model.Tools;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Business.Tools.tlOnline
{
    internal class CollaborateCrudFactorty
    {
        internal static CollaborateContatCrud ContatCrud
        {
            get { return OBulider.BuildInstance<CollaborateContatCrud>(); }
        }
    }

    internal class CollaborateContatCrud : CrudBase<CollaborateContactLibModel, ICollaborateContactLibRepository>
    {
        public CollaborateContatCrud() : base(new CollaborateContactLibRepository(), "合作联系人列表")
        {
        }
        #region  CRUD
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, Add);
            this.AddOpItem(OpMode.Edit, Edit);
        }
        OpResult Add(CollaborateContactLibModel model)
        {
            if (irep.IsExist(e => e.ContactPerson == model.ContactPerson))
                return OpResult.SetErrorResult("此联系人已经在无法添加");
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        OpResult Edit(CollaborateContactLibModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        #endregion

        #region  Find
        /// <summary>
        ///部门查找
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        internal List<CollaborateContactLibModel> GetContactLibDatasBy(string department)
        {
            return irep.Entities.Where(e => e.Department == department).ToList();
        }
        /// <summary>
        /// 是否存在联系人
        /// </summary>
        /// <param name="contactPerson"></param>
        /// <returns></returns>
        internal bool IsExistContactPerson(string contactPerson)
        {
            return irep.IsExist(e => e.ContactPerson == contactPerson);
        }
        /// <summary>
        /// 包含查询
        ///0:部门查找
        ///1:联系人姓名
        ///2:联系电话
        ///3.办公电话
        ///4.联系地址
        ///5.公司名称
        ///6.联系人属性
        ///7.往来业务
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        internal List<CollaborateContactLibModel> FindBy(string department, QueryContactDto queryDto)
        {
            switch (queryDto.SearchMode)
            {
                ///部门
                case 0:
                    return irep.Entities.Where(e => e.Department == department).ToList();
                ///联系人姓名
                case 1:
                    return irep.Entities.Where(e => e.Department == department && e.ContactPerson.Contains(queryDto.QueryContent)).ToList();
                ///联系电话
                case 2:
                    return irep.Entities.Where(e => e.Department == department && e.Telephone.Contains(queryDto.QueryContent)).ToList();
                ///办公电话
                case 3:
                    return irep.Entities.Where(e => e.Department == department && e.OfficeTelephone.Contains(queryDto.QueryContent)).ToList();
                ///联系地址
                case 4:
                    return irep.Entities.Where(e => e.Department == department && e.ContactAdress.Contains(queryDto.QueryContent)).ToList();
                ///公司名称
                case 5:
                    return irep.Entities.Where(e => e.Department == department && e.ContactCompany.Contains(queryDto.QueryContent)).ToList();
                ///联系人属性
                case 6:
                    return irep.Entities.Where(e => e.Department == department && e.CustomerCategory.Contains(queryDto.QueryContent)).ToList();
                ///往来业务
                case 7:
                    return irep.Entities.Where(e => e.Department == department && e.ContactMemo.Contains(queryDto.QueryContent)).ToList();
                default:
                    return new List<CollaborateContactLibModel>();
            }
        }
        #endregion
    }
}
