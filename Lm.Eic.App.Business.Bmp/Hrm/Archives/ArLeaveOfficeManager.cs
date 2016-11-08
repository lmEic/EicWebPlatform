﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Archives;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.Uti.Common.YleeDbHandler;
using  Lm.Eic.Uti.Common.YleeOOMapper;
using  Lm.Eic.Uti.Common.YleeObjectBuilder;
namespace Lm.Eic.App.Business.Bmp.Hrm.Archives
{
    public class ArLeaveOfficeManager 
    {
       
        public OpResult SaveLeaveOfficeInfo(List <ArWorkerLeaveOfficeModel> modelList)
        {
            return ArLeaveOfficeFactory.ArleaveOfficeCrud.SaveWorkerleaveOfficeInfo(modelList);
        }

      
    }

   internal class  ArLeaveOfficeFactory
   {
        public static  ArleaveOfficeCrud ArleaveOfficeCrud
       {
           get { return OBulider.BuildInstance<ArleaveOfficeCrud>(); }
       }
       public static ArchivesManager ArchivesManager
        {
            get { return OBulider.BuildInstance<ArchivesManager>(); }
        }
   }
    public class ArleaveOfficeCrud: CrudBase<ArWorkerLeaveOfficeModel, IArWorkerLeaveOfficeRepository>
    {
        public ArleaveOfficeCrud()
            : base(new ArWorkerLeaveOfficeRepository(),"离职人员信息")
      { }
         protected override void AddCrudOpItems() { }
        public OpResult SaveWorkerleaveOfficeInfo(List<ArWorkerLeaveOfficeModel> modelList)
        {
              try
            {
                int record = irep.Insert(modelList);
                return OpResult.SetResult("保存成功", "保存失败", record);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
    }
}
