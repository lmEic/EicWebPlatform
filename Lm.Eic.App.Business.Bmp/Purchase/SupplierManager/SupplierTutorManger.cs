
using System.Collections.Generic;
using System.Linq;
using Lm.Eic.App.DomainModel.Bpm.Purchase;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;

namespace Lm.Eic.App.Business.Bmp.Purchase.SupplierManager
{

    /// <summary>
    /// 供应商辅导\计划管理
    /// </summary>
    public class SupplierTutorManger
    {
        /// <summary>
        /// 得到考核分数低80分的供应商信息
        /// </summary>
        /// <param name="seasonDateNum"></param>
        /// <returns></returns>
        public List<SupplierSeasonTutorModel> GetWaittingTourSupplier(string seasonDateNum, double limitTotalCheckScore, double limitQualityCheck)
        {
            List<SupplierSeasonTutorModel> waittingTourSupplierList = new List<SupplierSeasonTutorModel>();
            //得到低于80分的所以供应商  品质低于90分也可  //QualityCheck <90,  
            var auditModeDatas = SupplierCrudFactory.SuppliersSeasonAuditCrud.GetlimitScoreSupplierAuditInfo(seasonDateNum, limitTotalCheckScore, limitQualityCheck);
            if (auditModeDatas != null && auditModeDatas.Count > 0)
            {
                auditModeDatas.ForEach(m =>
                   {
                       if (SupplierCrudFactory.SuppliersSeasonTutorCrud.IsExist(m.ParameterKey))
                       {
                           var SupplierSeasonTutorInfos = SupplierCrudFactory.SuppliersSeasonTutorCrud.GetSupplierSeasonTutorModelBy(m.ParameterKey);
                           if (SupplierSeasonTutorInfos.Count > 0)
                               SupplierSeasonTutorInfos.ForEach(info => {
                                   if (!waittingTourSupplierList.Contains(info))
                                       waittingTourSupplierList.Add(info);
                               });
                       }
                       else
                       {
                           var SupplierSeasonTutorInfo = ToSupplierSeasonTutorModelBy(m);
                           if (!waittingTourSupplierList.Contains(SupplierSeasonTutorInfo))
                               waittingTourSupplierList.Add(SupplierSeasonTutorInfo);
                       }
                   });
            }
            return waittingTourSupplierList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
       public List<SupplierSeasonTutorModel> GetSupplierSeasonTutorDatasBy(string supplierId)
        {
            return SupplierCrudFactory.SuppliersSeasonTutorCrud.GetSupplierSeasonTutorDatasBy(supplierId);
        }

        /// <summary>
        /// 保存供应商辅导\计划管理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult SaveSupplierTutorModel(SupplierSeasonTutorModel model)
        {
            return SupplierCrudFactory.SuppliersSeasonTutorCrud.Store(model,true);
        }

        public DownLoadFileModel DownLoadTourSupplier(List<SupplierSeasonTutorModel> datas)
        {
            if (datas == null || datas.Count == 0) new DownLoadFileModel().Default(); 
            var dataGroupping = datas.GetGroupList<SupplierSeasonTutorModel>();
           return  dataGroupping.ExportToExcelMultiSheets<SupplierSeasonTutorModel>(CreateFieldSeasonTutorMapping).CreateDownLoadExcelFileModel("供应商辅导管理");
        }
        private List<FileFieldMapping> CreateFieldSeasonTutorMapping
        {
            get
            {
                return new List<FileFieldMapping>(){
                new FileFieldMapping ("Number","项次") ,
                new FileFieldMapping ("SupplierId","供应商Id") ,
                new FileFieldMapping ("SupplierName","供应商全称") ,
                new FileFieldMapping ("AuditPrice","价格考核") ,
                new FileFieldMapping ("DeliveryDate","交期考核") ,
                new FileFieldMapping ("ActionLiven","配合度考核") ,
                new FileFieldMapping ("HSFGrade","HSF考核") ,
                new FileFieldMapping ("TotalCheckScore","考核总分") ,
                new FileFieldMapping ("CheckLevel","考核等级") ,
                new FileFieldMapping ("RewardsWay","处理方式") ,
                new FileFieldMapping ("MaterialGrade","风险等险") ,
                new FileFieldMapping ("ManagerRisk","管理风险") ,
                new FileFieldMapping ("SeasonNum","季度") ,
                new FileFieldMapping ("PlanTutorDate","计划辅导日期") ,
                new FileFieldMapping ("PlanTutorContent","计划辅导内容") ,
                new FileFieldMapping ("ActionTutorDate","实际辅导日期") ,
                new FileFieldMapping ("ActionTutorContent","实际辅导内容") ,
                new FileFieldMapping ("TutorResult","辅导结果") ,
                new FileFieldMapping ("TutorCategory","辅导范畴") ,
                new FileFieldMapping ("Remark","备注") ,
                new FileFieldMapping ("YearMonth","年份") };
            }
        }
        #region  Internet
        SupplierSeasonTutorModel ToSupplierSeasonTutorModelBy(SupplierSeasonAuditModel auditModel)
        {
            try
            {
                SupplierSeasonTutorModel tutorModel = new SupplierSeasonTutorModel();
                if ( auditModel == null) return tutorModel;
                OOMaper.Mapper< SupplierSeasonAuditModel, SupplierSeasonTutorModel>( auditModel, tutorModel);
                tutorModel. TutorCategory = "考核低于80";
                return tutorModel;
            }
            catch (System.Exception ex)
            { throw new System.Exception(ex.Message); }

        }
        #endregion
    }


}
