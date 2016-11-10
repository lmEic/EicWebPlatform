using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Archives;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.Uti.Common.YleeDbHandler;
using  Lm.Eic.Uti.Common.YleeOOMapper;
using  Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeExtension.Validation;
namespace Lm.Eic.App.Business.Bmp.Hrm.Archives
{
    public class ArLeaveOfficeManager 
    {
        private ArleaveOfficeCrud crud = null;
        public ArLeaveOfficeManager()
        {
            this.crud = new ArleaveOfficeCrud();
        }

        /// <summary>
        /// 存储离职信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public OpResult StoreLeaveOffInfo(ArLeaveOfficeModel entity)
        {
            return this.crud.SaveWorkerleaveOfficeInfo(entity);
        }
    }

   
    public class ArleaveOfficeCrud: CrudBase<ArLeaveOfficeModel, IArWorkerLeaveOfficeRepository>
    {
        public ArleaveOfficeCrud()
            : base(new ArWorkerLeaveOfficeRepository(),"离职人员信息")
      { }
      
        public OpResult SaveWorkerleaveOfficeInfo(ArLeaveOfficeModel entity)
        {
              try
            {
                return this.PersistentDatas(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        protected override void AddCrudOpItems()
        {
            //this.AddOpItem("add", entity => {  });
        }
    }
}
