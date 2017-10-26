using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.WorkOverHours;
using Lm.Eic.App.DomainModel.Bpm.Hrm.WorkOverHours;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Hrm.WorkOverHours
{
   public class WorkOverHoursManager
   {
       
        public List<WorkOverHoursMangeModels> FindRecordBy(WorkOverHoursDto Dto)
        {
            return WorkOverHoursFactory.WorkOverHoursCrud.FindBy(Dto);
        }

       

   }
}
