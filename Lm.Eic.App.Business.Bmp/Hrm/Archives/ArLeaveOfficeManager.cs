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
    //public class ArLeaveOfficeCrud 
    //{
       
    //    public OpResult SaveLeaveOfficeInfo(ArWorkerLeaveOfficeModel model)
    //    {
    //        if (model!=null  )
    //        {
    //            ArLeaveOfficeFactory.ArchivesManager.ChangeWorkingStatus(model.WorkerId, "离职");
    //            return ArLeaveOfficeFactory.ArleaveOfficeCrud.SaveWorkerleaveOfficeInfo(model);
    //        }
    //        else return OpResult.SetResult("数据不能为空！");
    //    }

      
    //}


    public class ArleaveOfficeManager: CrudBase<ArLeaveOfficeModel, IArWorkerLeaveOfficeRepository>
    {
        public ArleaveOfficeManager()
            : base(new ArWorkerLeaveOfficeRepository(),"离职人员信息")
        { }
         protected override void AddCrudOpItems() { }
        public OpResult StoreleaveOfficeInfo(ArLeaveOfficeModel model)
        {
              try
            {
             
                SetFixFieldValue(model);
                int record = irep.Insert(model);
                return OpResult.SetResult("保存成功", "保存失败", record);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
    }
}
