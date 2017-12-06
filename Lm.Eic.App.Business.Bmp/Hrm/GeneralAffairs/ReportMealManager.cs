using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.GeneralAffairs;
using Lm.Eic.App.DomainModel.Bpm.Hrm.GeneralAffairs;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using CrudFactory = Lm.Eic.App.Business.Bmp.Hrm.GeneralAffairs.GeneralAffairsFactory;
using System.IO;

namespace Lm.Eic.App.Business.Bmp.Hrm.GeneralAffairs
{
    public class ReportMealManager
    {
        #region method
        //public OpResult Store(IList<MealReportManageModel> entities)
        //{

        //}
        #endregion
    }

    internal class ReportMealStore : CrudBase<MealReportManageModel, IMealReportManageRepository>
    {
        public ReportMealStore() : base(new MealReportManageRepository(), "报餐")
        {

        }
        #region store method


        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, this.Add);
            this.AddOpItem(OpMode.Edit, this.Update);
            this.AddOpItem(OpMode.Delete, this.Delete);
        }

        private OpResult Add(MealReportManageModel entity)
        {
            if (!this.irep.IsExist(e => e.Department == entity.Department && e.WorkerId == entity.WorkerId && e.ReportDay == entity.ReportDay))
            {
                return this.irep.Insert(entity).ToOpResult_Add(this.OpContext);
            }
            return this.Update(entity);
        }
        private OpResult Update(MealReportManageModel entity)
        {
            return this.irep.Update(f => f.Id_Key == entity.Id_Key, entity).ToOpResult_Eidt(this.OpContext);
        }

        private OpResult Delete(MealReportManageModel entity)
        {
            return this.irep.Delete(f => f.Id_Key == entity.Id_Key).ToOpResult_Delete(this.OpContext);
        }
        #endregion

        #region query method

        #endregion

    }
}
