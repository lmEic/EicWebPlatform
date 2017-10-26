using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.WorkOverHours;
using Lm.Eic.App.DomainModel.Bpm.Hrm.WorkOverHours;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Lm.Eic.App.Business.Bmp.Hrm.WorkOverHours.WorkOverHoursManager;

namespace Lm.Eic.App.Business.Bmp.Hrm.WorkOverHours
{
    internal class WorkOverHoursFactory
    {
        /// <summary>
        /// 加班管理Crud
        /// </summary>
        internal static WorkOverHoursCrud WorkOverHoursCrud
        {
            get { return OBulider.BuildInstance<WorkOverHoursCrud>(); }

        }

    }
    internal class WorkOverHoursCrud : CrudBase<WorkOverHoursMangeModels, IWorkOverHoursManageModelsRepository>
    {
        public WorkOverHoursCrud()
            : base(new WorkOverHoursManageModelsRepository(), "加班管理")
        { }

        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }

        //查询
        internal List<WorkOverHoursMangeModels> FindBy(WorkOverHoursDto Dto)
        {
            if (Dto == null) return new List<WorkOverHoursMangeModels>();
            try
            {
                switch (Dto.SearchMode)
                {
                    case 1:
                        return irep.Entities.Where(m => m.WorkDate == (Dto.WorkDate)).ToList();
                    default:
                        return new List<WorkOverHoursMangeModels>();
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.InnerException.Message);
            }
        }


    }
}
