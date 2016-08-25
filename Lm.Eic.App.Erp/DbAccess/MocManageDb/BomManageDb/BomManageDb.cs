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
        /// 替代料件
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        private AgentMaterilModel AgentMaterial(string productId)
        {
            string sqlFields = "SELECT  distinct  MB004  FROM  BOMMB  ";
            string sqlwhere = string.Format("WHERE   MB001 = '{0}' AND (MB007 = ''  or  MB007 >= '{1}')", productId, DateTime.Now.Date.ToString("yyyyMMdd"));
            var ListModels = ErpDbAccessHelper.FindDataBy<AgentMaterilModel>(sqlFields, sqlwhere, (dr, m) =>
            {
                m.MatreialID = productId;
                m.AgentMaterialId = dr["MB004"].ToString().Trim();
            });

            if (ListModels != null && ListModels.Count() > 0)
                return ListModels.FirstOrDefault();

            return null;
        }
         /// <summary>
        /// Sql      品号    品名    规格   属性   单位
        /// </summary>
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
            if (gradeNumber == 10000)
            {
                returnGrade = "替代料件";
            }
            else
            {
                for (int i = 0; i < gradeNumber; i++)
                {
                    returnGrade = returnGrade + "*";
                }
                returnGrade = returnGrade + gradeNumber.ToString();
            }
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
            List<BomMaterialModel> mainMaterialModel = new List<BomMaterialModel>();
            List<BomMaterialModel> returnMaterialModel = new List<BomMaterialModel>();
            int grade = 1;
            componentModelList = GetBomFormERP_BOMMD_By(productId, grade);
            returnxuhuan:
            foreach (var materialModel in componentModelList)
            {
               
                var tt2 = GetBomFormERP_BOMMD_By(materialModel.MaterialId, grade);
                if (tt2 != null && tt2.Count>0)
                {
                    tt2.ForEach(t =>
                    {
                        mainMaterialModel.Add(t);
                    });
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
                if (mainMaterialModel != null && mainMaterialModel.Count > 0)
                {
                    componentModelList.Clear();
                    mainMaterialModel.ForEach(m =>
                    {
                        componentModelList.Add(new BomMaterialModel
                        {
                            MainMaterialId = productId,
                            MaterialIdInfo = m.MaterialIdInfo,
                            Grade = m.Grade,
                            BaseNumber = m.BaseNumber,
                            MaterialId = m.MaterialId,
                            NeedNumber = m.NeedNumber
                        });
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


