using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Lm.Eic.App.Erp.DbAccess.MocManageDb.BomManageBb
{
    /// <summary>
    /// Bom管理Crud工厂
    /// </summary>
    internal class BomCrudFactory
    {
        /// <summary>
        /// Bom管理Crud
        /// </summary>
        public static BomManageDb BomManageDb
        {
            get { return OBulider.BuildInstance<BomManageDb>(); }
        }
    }

    /// <summary>
    /// Bom管理Db
    /// </summary>
    public class BomManageDb
    {
        /// <summary>
        /// 获取工单详情      主物料    品号     底数   组成用量
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        private   List<BomMaterialModel> GetBomFormERP_BOMMD_By(string productId ,int grade )
        {
           
            string SqlFields= "Select MD003 as 组料品号,MD007 as 底数,MD006 as 组成用量 from BOMMD";
            string sqlWhere = string.Format(" where MD001='{0}' and  MD012=''", productId);
            var ListModels = ErpDbAccessHelper.FindDataBy<BomMaterialModel>(SqlFields, sqlWhere, (dr, m) =>
            {
                m.MainMaterialId = productId;
                m.MaterialId = dr["组料品号"].ToString().Trim();
                m.MaterialIdInfo = GetBomFormERP_INVMB_By(dr["组料品号"].ToString().Trim());
                m.Grade = ConvertGrade(grade);
                m.BaseNumber = dr["底数"].ToString().Trim().ToDouble();
                m.NeedNumber = dr["组成用量"].ToString().Trim().ToDouble ();
            });
          
            return ListModels;
        }
        /// <summary>
        /// 查找替代料件属性
        /// </summary>
        /// <param name="mainMaterial">主物料</param>
        /// <param name="productId">分物料</param>
        /// <param name="agentproductId">替代料号</param>
        /// <returns></returns>
        private BomMaterialModel GetAgentBomFormERP_BOMMD_By(string mainMaterial,string productId,string agentproductId)
        {

            string SqlFields = "Select MD001 AS 主料件, MD003 as 组料品号,MD007 as 底数,MD006 as 组成用量 from BOMMD";
            string sqlWhere = string.Format(" where MD001='{0}' and MD003='{1}' and  MD012=''", mainMaterial, productId);
            var ListModels = ErpDbAccessHelper.FindDataBy<BomMaterialModel>(SqlFields, sqlWhere, (dr, m) =>
            {
                m.MainMaterialId = mainMaterial;
                m.MaterialId = agentproductId;
                m.MaterialIdInfo = GetBomFormERP_INVMB_By(agentproductId.Trim());
                m.Grade = "替代料件";
                m.BaseNumber = dr["底数"].ToString().Trim().ToDouble();
                m.NeedNumber = dr["组成用量"].ToString().Trim().ToDouble();
            });

            return ListModels.FirstOrDefault();
        }
        /// <summary>
        /// 替代料件
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        private  List<AgentMaterilModel> GetAgentMaterialFormERP_BOMMB_BY(string productId)
        {
            string sqlFields = "SELECT  distinct  MB004  FROM  BOMMB  ";
            string sqlwhere = string.Format("WHERE   MB001 = '{0}' AND (MB007 = ''  or  MB007 >= '{1}')", productId, DateTime.Now.Date.ToString("yyyyMMdd"));
            var ListModels = ErpDbAccessHelper.FindDataBy<AgentMaterilModel>(sqlFields, sqlwhere, (dr, m) =>
            {
                m.MatreialID = productId;
                m.AgentMaterialId = dr["MB004"].ToString().Trim();
            });
            return ListModels;
        }
        /// <summary>
        /// Sql      品号    品名    规格   属性   单位
      /// </summary>
      /// <param name="productId">产品料号</param>
      /// <returns></returns>
        
        private  MarterialBaseInfo GetBomFormERP_INVMB_By(string productId)
        {
            string SqlFields="SELECT MB001 as 品号,MB002 as 品名, MB003 as 规格, MB025 as 属性,MB004 as 单位 FROM  INVMB"; 
            string sqlWhere = string.Format(" where MB001='{0}'", productId);
            var ListModels = ErpDbAccessHelper.FindDataBy<MarterialBaseInfo>(SqlFields, sqlWhere, (dr, m) =>
            {
                m.MaterialId = dr["品号"].ToString().Trim();
                m.MaterialName = dr["品名"].ToString().Trim();
                m.MaterialSpecify = dr["规格"].ToString().Trim();
                m.Property = ConvertProperty(dr["属性"].ToString().Trim());
                m.Unit = dr["单位"].ToString().Trim();
            });
            return ListModels.FirstOrDefault();
        }
         /// <summary>
         /// 属性转换
         /// </summary>
         /// <param name="materialProperty"></param>
         /// <returns></returns>
        private string ConvertProperty(string materialProperty)
         {
            switch (materialProperty.Trim())
            {
                case "P":
                    return "采购件";
                case "M":
                    return "自制件";
                case "Y":
                    return "虚设品号";
                case "S":
                    return "委外加工";
                case "C":
                    return "配件";
                default:
                    return string.Empty;
            }
        }
        /// <summary>
        /// 阶次转换
        /// </summary>
        /// <param name="gradeNumber"></param>
        /// <returns></returns>
        private string ConvertGrade(int gradeNumber)
        {
            string returnGrade = string.Empty;
                for (int i = 0; i < gradeNumber; i++)
                {
                    returnGrade = returnGrade + "*";
                }
                returnGrade = returnGrade + gradeNumber.ToString();
            return returnGrade;
        }

        /// <summary>
        /// 获取Bom物料列表
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<BomMaterialModel> GetBomMaterialListBy(string productId)
        {
            List<BomMaterialModel> componentModelList = new List<BomMaterialModel>();
            List<BomMaterialModel> componentAgentModelList = new List<BomMaterialModel>();
            List<BomMaterialModel> mainMaterialModel = new List<BomMaterialModel>();
            List<BomMaterialModel> returnMaterialModel = new List<BomMaterialModel>();
            int grade = 1;
            componentModelList = GetBomFormERP_BOMMD_By(productId, grade);
            returnxuhuan:
            foreach (var materialModel in componentModelList)
            {
                var agentBomMaterilal = GetAgentMaterialFormERP_BOMMB_BY(materialModel.MaterialId);
                if (agentBomMaterilal != null && agentBomMaterilal.Count()>0)
                {
                    agentBomMaterilal.ForEach(e =>
                    {
                        componentAgentModelList.Add(GetAgentBomFormERP_BOMMD_By(materialModel.MainMaterialId, e.MatreialID, e.AgentMaterialId));
                    }); 
                }
            }
            if (componentAgentModelList.Count > 0)
            { componentModelList = componentModelList.Union(componentAgentModelList).ToList(); componentAgentModelList.Clear(); }
            foreach (var materialModel in componentModelList)
            {
                if (materialModel.Grade != "替代料件")
                {
                    var bomMaterilal = GetBomFormERP_BOMMD_By(materialModel.MaterialId, grade);
                    if (bomMaterilal.Count > 0)
                    {
                        bomMaterilal.ForEach(t =>
                        {
                            mainMaterialModel.Add(t);
                        });
                    }
                }
                returnMaterialModel.Add(new BomMaterialModel {
                    MainMaterialId = productId,
                    MaterialIdInfo =materialModel.MaterialIdInfo,
                    Grade =materialModel.Grade ,
                    BaseNumber=materialModel.BaseNumber ,
                    MaterialId = materialModel.MaterialId,
                    NeedNumber=materialModel.NeedNumber 
                });
            }
                grade++;
                //如果子阶不为空 循环子阶
                if ( mainMaterialModel.Count > 0)
                {
                    componentModelList.Clear();
                    mainMaterialModel.ForEach(m =>
                    {
                        componentModelList.Add(m);
                    }); 
                    mainMaterialModel.Clear();
                    goto returnxuhuan;
                    //返回到Foreach
                }
         
            //主件品号	阶次	元件品号	属性	品名	规格	单位	组成用量	底数

                return returnMaterialModel;
        }

    }


  
}


