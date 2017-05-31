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

        internal static RmaBusinessDescriptionCrud RmaBussesDescription
        {
            get { return OBulider.BuildInstance<RmaBusinessDescriptionCrud>(); }
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
            this.AddOpItem(OpMode.Add, Add);
            this.AddOpItem(OpMode.Edit, Eidt);
        }
        internal OpResult Add(RmaReportInitiateModel model)
        {
            if (!IsExist(model.RmaId))
            {
                SetModelVaule(model, RmaHandleStatus.InitiateStatus);
                return irep.Insert(model).ToOpResult_Add(OpContext);
            }
            return OpResult.SetErrorResult("该RMA单号记录已经存在");
        }
        internal OpResult Eidt(RmaReportInitiateModel model)
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
        /// 
        /// </summary>
        /// <returns></returns>
        internal int CountNowYaerMonthRmaIdNumber(string nowYaer,string nowMonth)
        {
            return irep.Entities.Count(e => e.RmaYear == nowYaer && e.RmaMonth == nowMonth);
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
        internal OpResult UpdateHandleStatus(string rmaId, string handleStatus)
        {
            return irep.Update(e => e.RmaId == rmaId, u => new RmaReportInitiateModel
            {
                RmaIdStatus = handleStatus
            }).ToOpResult_Eidt(OpContext);
        }
        #endregion

    }
    internal class RmaBusinessDescriptionCrud : CrudBase<RmaBusinessDescriptionModel, IRmaBussesDescriptionRepository>
    {
        public RmaBusinessDescriptionCrud() : base(new RmaBussesDescriptionRepository(), "登记表单")
        {
        }
        #region  CRUD
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddModel);
            this.AddOpItem(OpMode.Edit, UpdateModel);
        }

        OpResult AddModel(RmaBusinessDescriptionModel model)
        {
            if (!IsExist(model.RmaId, model.ProductId, model.ReturnHandleOrder))
                return irep.Insert(model).ToOpResult_Add(OpContext);
            return OpResult.SetErrorResult("该记录已经存在！");
        }
        OpResult UpdateModel(RmaBusinessDescriptionModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        #endregion

        #region find
        /// <summary>
        /// 得业务录入 数据
        /// </summary>
        /// <param name="rmaId"></param>
        /// <returns></returns>
        internal List<RmaBusinessDescriptionModel> GetRmaBussesDescriptionDatasBy(string rmaId)
        {
            return irep.Entities.Where(e => e.RmaId == rmaId).ToList();
        }

        internal bool IsExist(string rmaid, string productId, string returnHandleOrder)
        {
            return irep.IsExist(e => e.RmaId == rmaid && e.ProductId == productId && e.ReturnHandleOrder == returnHandleOrder);
        }
        internal OpResult UpdateHandleStatus(string rmaId, string productId, string returnHandleOrder)
        {
            return irep.Update(e => e.RmaId == rmaId && e.ProductId == productId && e.ReturnHandleOrder == returnHandleOrder,
               u => new RmaBusinessDescriptionModel
               {
                   HandleStatus = RmaHandleStatus.BusinessStatus
               }).ToOpResult_Eidt(OpContext);
        }
        #endregion
    }
    internal class RmaInspectionManageCrud : CrudBase<RmaInspectionManageModel, IRmaInspectionManageRepository>
    {
        public RmaInspectionManageCrud() : base(new RmaInspectionManageRepository(), "检验处理")
        {
        }

        #region  CRUD
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, Add);
            this.AddOpItem(OpMode.Edit, Update);
        }
        OpResult Add(RmaInspectionManageModel model)
        {
            if (!IsExist(model.RmaId, model.ParameterKey))
                return irep.Insert(model).ToOpResult_Add(OpContext);
            return OpResult.SetErrorResult("该记录已经存在！");
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

        internal OpResult UpdateHandleStatus(string parameterKey)
        {

            if (string.IsNullOrEmpty(parameterKey)) return OpResult.SetErrorResult("ramId或者productId不能为空!");
            return irep.Update(f => f.ParameterKey == parameterKey, u => new RmaInspectionManageModel
            {
                HandleStatus = RmaHandleStatus.InspecitonStatus
            }).ToOpResult_Eidt(OpContext);
        }
        internal bool IsExist(string rmaId, string productId)
        {
            return irep.IsExist(e => e.RmaId == rmaId && e.ParameterKey == productId);
        }
    }
}
