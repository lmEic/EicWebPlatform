using System;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using System.Collections.Generic;
using System.Linq;

namespace Lm.Eic.App.Business.Bmp.Quality.RmaManage
{
    internal class RmaCurdFactory
    {
        internal static RmaReportInitiateCrud RmaReportInitiate
        {
            get { return OBulider.BuildInstance<RmaReportInitiateCrud>(); }
        }

        internal static RmaBussesDescriptionCrud RmaBussesDescription
        {
            get { return OBulider.BuildInstance<RmaBussesDescriptionCrud>(); }
        }

        internal static RmaInspectionManageCrud RmaInspectionManage
        {
            get { return OBulider.BuildInstance<RmaInspectionManageCrud>(); }
        }
    }

    internal class RmaReportInitiateCrud : CrudBase<RmaReportInitiateModel, IRmaReportInitiateRepository>
    {
        public RmaReportInitiateCrud() : base(new RmaReportInitiateRepository(), "创建表单")
        { }
        #region  CRUD
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddModel);
            this.AddOpItem(OpMode.Edit, EidtModel);
        }
        internal OpResult AddModel(RmaReportInitiateModel model)
        {

            if (!IsExist(model.RmaId))
            {
                SetModelVaule(model, RmaHandleStatus.InitiateStatus);

                return irep.Insert(model).ToOpResult_Add(OpContext);
            }

            return EidtModel(model);
        }
        internal OpResult EidtModel(RmaReportInitiateModel model)
        {
            SetModelVaule(model, model.RmaIdStatus);
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        private void SetModelVaule(RmaReportInitiateModel model, string InitiateStatus)
        {
            if (model.RmaId != null && model.RmaId.Length == 8)
            {
                model.RmaYear = model.RmaId.Substring(1, 2);
                model.RmaMonth = model.RmaId.Substring(3, 2);
            }
            model.RmaIdStatus = InitiateStatus;
        }
        #endregion

        #region  Find
        /// <summary>
        /// 以R开头 当前 00年份 00月份  后面三位流水号
        /// </summary>
        /// <returns></returns>
        internal string BuildingNewRmaId()
        {
            ///以R开头 年份 月份  再加序序号000
            string nowYaer = DateTime.Now.ToString("yy");
            string nowMonth = DateTime.Now.ToString("MM");
            var count = irep.Entities.Count(e => e.RmaYear == nowYaer && e.RmaMonth == nowMonth) + 1;
            return "R" + nowYaer + nowMonth + count.ToString("000");

        }
        internal List<RmaReportInitiateModel> GetInitiateDatas(string rmaId)
        {
            return irep.Entities.Where(e => e.RmaId == rmaId).ToList();
        }
        internal bool IsExist(string rmaId)
        {
            return irep.IsExist(e => e.RmaId == rmaId);
        }
        internal List<RmaReportInitiateModel> GetRmaReportInitiateDatas(string year, string month)
        {
            return irep.Entities.Where(e => e.RmaYear == year && e.RmaMonth == month).ToList();
        }
        /// <summary>
        /// 改Rma状态
        /// </summary>
        /// <param name="rmaId"></param>
        /// <param name="rmaIdStatus"></param>
        /// <returns></returns>
        internal OpResult UpdateInitiateRmaIdStatus(string rmaId, string rmaIdStatus)
        {
            return irep.Update(e => e.RmaId == rmaId, u => new RmaReportInitiateModel { RmaIdStatus = rmaIdStatus }).ToOpResult_Eidt(OpContext);
        }

        #endregion

    }
    internal class RmaBussesDescriptionCrud : CrudBase<RmaBusinessDescriptionModel, IRmaBussesDescriptionRepository>
    {
        public RmaBussesDescriptionCrud() : base(new RmaBussesDescriptionRepository(), "记录登记表单")
        {
        }
        #region  CRUD
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddModel);
            this.AddOpItem(OpMode.UpDate, Update);
        }

        OpResult AddModel(RmaBusinessDescriptionModel model)
        {
            if (!IsExist(model.RmaId, model.ProductId))
                return irep.Insert(model).ToOpResult_Add(OpContext);
            return Update(model);
        }
        OpResult Update(RmaBusinessDescriptionModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        #endregion

        /// <summary>
        /// 得业务录入 数据
        /// </summary>
        /// <param name="rmaId"></param>
        /// <returns></returns>
        internal List<RmaBusinessDescriptionModel> GetRmaBussesDescriptionDatasBy(string rmaId)
        {
            return irep.Entities.Where(e => e.RmaId == rmaId).ToList();
        }

        internal bool IsExist(string rmaid, string productId)
        {
            return irep.IsExist(e => e.RmaId == rmaid && e.ProductId == productId);
        }
        internal OpResult UpdateBussesDescriptionStatus(string rmaId, string productId, string handleStatus)
        {
            return irep.Update(e => e.RmaId == rmaId && e.ProductId == productId,
                new RmaBusinessDescriptionModel { HandleStatus = handleStatus }).ToOpResult_Eidt(OpContext);
        }
    }

    internal class RmaInspectionManageCrud : CrudBase<RmaInspectionManageModel, IRmaInspectionManageRepository>
    {
        public RmaInspectionManageCrud() : base(new RmaInspectionManageRepository(), "Ram检验处理")
        {
        }
        #region  CRUD
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddModel);
            this.AddOpItem(OpMode.UpDate, Update);
        }

        OpResult AddModel(RmaInspectionManageModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }



        OpResult Update(RmaInspectionManageModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        #endregion

        internal List<RmaInspectionManageModel> GetInspectionManageDatasBy(string rmaId)
        {
            return irep.Entities.Where(e => e.RmaId == rmaId).ToList();
        }
        internal bool IsExist(string rmaId, string productId)
        {
            return irep.IsExist(e => e.RmaId == rmaId && e.ProductId == productId);
        }
    }

}
