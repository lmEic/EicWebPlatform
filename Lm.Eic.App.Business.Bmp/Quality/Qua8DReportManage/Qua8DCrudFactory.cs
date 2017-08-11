using Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.Qua8DReportManage
{
    internal class Qua8DCrudFactory
    {

        internal static Qua8DReportMasterCrud MasterCrud
        {
            get { return OBulider.BuildInstance<Qua8DReportMasterCrud>(); }
        }

        internal static Qua8DReportDetailsCrud DetailsCrud
        {
            get { return OBulider.BuildInstance<Qua8DReportDetailsCrud>(); }
        }
    }
    internal class Qua8DReportMasterCrud : CrudBase<Qua8DReportMasterModel, IQua8DReportMasterRepository>
    {
        public Qua8DReportMasterCrud() : base(new Qua8DReportMasterRepository(), "8D初始记录表")
        {
        }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, Add);
            this.AddOpItem(OpMode.Edit, Eidt);
        }
        OpResult Add(Qua8DReportMasterModel model)
        {
            if (!IsExist(model.ReportId))
            {
                return irep.Insert(model).ToOpResult_Add(OpContext);
            }
            return irep.Update(e => e.ReportId == model.ReportId, u => new Qua8DReportMasterModel
            {
                AccountabilityDepartment = model.AccountabilityDepartment,
                MaterialCountUnit = model.MaterialCountUnit,
                InspectCount = model.InspectCount,
                InspectCountUint = model.InspectCountUint,
                FailQty = model.FailQty,
                FailQtyUnit = model.FailQtyUnit,
                FailClass = model.FailClass
            }
               ).ToOpResult_Eidt(OpContext);
        }
        OpResult Eidt(Qua8DReportMasterModel model)
        {
            if (!IsExist(model.ReportId) || model.Id_Key == 0)
                return OpResult.SetErrorResult("该单号记录不存在，编辑失败");
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        bool IsExist(string reportId)
        {
            return irep.IsExist(e => e.ReportId == reportId);
        }
    }

    internal class Qua8DReportDetailsCrud : CrudBase<Qua8DReportDetailModel, IQua8DReportDetailsRepository>
    {
        public Qua8DReportDetailsCrud() : base(new Qua8DReportDetailsRepository(), "8D记录总表")
        {
        }

        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public List<Qua8DReportDetailModel> GetQua8DDetailDatasBy(string reportId)
        {
            return irep.Entities.Where(e => e.ReportId == reportId).ToList();
        }
    }
}
