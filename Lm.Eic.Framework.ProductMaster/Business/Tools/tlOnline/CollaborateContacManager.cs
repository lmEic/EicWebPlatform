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
    public class CollaborateContactManager
    {
        CollaborateContatCrud ContatCrud
        {
            get { return OBulider.BuildInstance<CollaborateContatCrud>(); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreData(CollaborateContactLibModel model)
        {
            if (model == null) return OpResult.SetErrorResult("数据不能为空");
            if (model.ContactPerson == null) return OpResult.SetErrorResult("联系人不能为空");
            if (ContatCrud.isExistContactPerson(model.ContactPerson) && model.Id_Key == 0)
                return ContatCrud.UpdateData(model);
            return ContatCrud.Store(model);
        }
        /// <summary>
        ///0:部门查找
        ///1:联系人
        ///2:电话
        /// </summary>
        /// <param name="queryDto">查询</param>
        /// <returns></returns>
        public List<CollaborateContactLibModel> GetContactLibDatasBy(string department, int searchMode, string queryContent, bool isExactQuery)
        {

            var datas = GetContactLibDatasBy(department);
            QueryContactDto queryDto = new QueryContactDto()
            {
                SearchMode = searchMode,
                Department = department,
                QueryContent = queryContent,
                IsExactQuery = isExactQuery
            };

            return FiltrateData(datas, queryDto);
        }
        /// <summary>
        /// 对已有的数据时行筛选
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        private List<CollaborateContactLibModel> FiltrateData(List<CollaborateContactLibModel> datas, QueryContactDto queryDto)
        {
            try
            {
                if (datas == null || datas.Count == 0) return FromCrudFind(queryDto);
                switch (queryDto.SearchMode)
                {
                    case 0:
                        return datas;
                    case 1:
                        return datas.FindAll(e => e.ContactPerson.Contains(queryDto.QueryContent));
                    case 2:
                        return datas.FindAll(e => e.Telephone.Contains(queryDto.QueryContent));
                    default:
                        return datas;
                }
            }
            catch (Exception es)
            {
                throw new Exception(es.Message);
            }
        }
        /// <summary>
        /// 通过部门得到所有的信息
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        private List<CollaborateContactLibModel> GetContactLibDatasBy(string department)
        {
            return ContatCrud.GetCollaborateContactLibDatasBy(department);
        }
        /// <summary>
        /// 从数据直接查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        private List<CollaborateContactLibModel> FromCrudFind(QueryContactDto queryDto)
        {
            try
            {
                if (queryDto == null) return new List<CollaborateContactLibModel>();
                if (queryDto.IsExactQuery)
                    return ContatCrud.ExactFind(queryDto);
                else
                    return ContatCrud.ContainsFind(queryDto);
            }
            catch (Exception es)
            {
                throw new Exception(es.Message);
            }
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
            this.AddOpItem(OpMode.Add, AddData);
            this.AddOpItem(OpMode.Edit, EditData);
        }
        OpResult AddData(CollaborateContactLibModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        OpResult EditData(CollaborateContactLibModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        #endregion

        internal OpResult UpdateData(CollaborateContactLibModel model)
        {
            return irep.Update(e => e.ContactPerson == model.ContactPerson, f => new CollaborateContactLibModel
            {
                ContactAdress = model.ContactAdress,
                ContactCompany = model.ContactCompany,
                ContactMemo = model.ContactMemo,
                CustomerCategory = model.CustomerCategory,
                Mail = model.Mail,
                OfficeTelephone = model.OfficeTelephone,
                QqOrSkype = model.QqOrSkype,
                WorkerPosition = model.WorkerPosition,
                WebsiteAdress = model.WebsiteAdress,
                Telephone = model.Telephone,
                Fax = model.Fax
            }).ToOpResult_Eidt(OpContext);
        }
        internal List<CollaborateContactLibModel> GetCollaborateContactLibDatasBy(string department)
        {
            return irep.Entities.Where(e => e.Department == department).ToList();
        }

        internal bool isExistContactPerson(string contactPerson)
        {
            return irep.IsExist(e => e.ContactPerson == contactPerson);
        }
        /// <summary>
        /// 包含查询
        ///0:部门查找
        ///1:联系人
        ///2:电话
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        internal List<CollaborateContactLibModel> ContainsFind(QueryContactDto queryDto)
        {
            switch (queryDto.SearchMode)
            {
                case 0:
                    return irep.Entities.Where(e => e.Department.Contains(queryDto.Department)).ToList();
                case 1:
                    return irep.Entities.Where(e => e.ContactPerson.Contains(queryDto.QueryContent)).ToList();
                case 2:
                    return irep.Entities.Where(e => e.Telephone.Contains(queryDto.QueryContent)).ToList();
                default:
                    return new List<CollaborateContactLibModel>();
            }
        }
        /// <summary>
        /// 精确查询
        ///0:部门查找
        ///1:联系人
        ///2:电话
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        internal List<CollaborateContactLibModel> ExactFind(QueryContactDto queryDto)
        {
            switch (queryDto.SearchMode)
            {
                case 0:
                    return irep.Entities.Where(e => e.Department == queryDto.Department).ToList();
                case 1:
                    return irep.Entities.Where(e => e.ContactPerson == (queryDto.QueryContent)).ToList();
                case 2:
                    return irep.Entities.Where(e => e.Telephone == (queryDto.QueryContent)).ToList();
                default:
                    return new List<CollaborateContactLibModel>();
            }
        }
    }
}
