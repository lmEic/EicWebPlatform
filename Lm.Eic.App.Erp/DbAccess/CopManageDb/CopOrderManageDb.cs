using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using Lm.Eic.App.Erp.Domain.CopManageModel;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System.Collections.Generic;
using System;
using System.Data;
using System.Text;


namespace Lm.Eic.App.Erp.DbAccess.CopManageDb
{
    /// <summary>
    /// 销售订单管理Crud工厂
    /// </summary>
    public class CopOrderCrudFactory
    {
        /// <summary>
        /// 销售订单Crud
        /// </summary>
        public static CopOrderManageDb CopOrderManageDb
        {
            get { return OBulider.BuildInstance<CopOrderManageDb>(); }
        }
        /// <summary>
        ///  销退单管理Crud
        /// </summary>
        public static CopReturnOrderManageDb CopReturnOrderManageDb
        {
            get { return OBulider.BuildInstance<CopReturnOrderManageDb>(); }
        }
    }


    public class CopOrderManageDb
    {
        private string SqlFields
        {
            get { return "SELECT TD001 AS 单别, TD002 AS 单号, TD003 AS 序号, TD004 AS 品号, TD005 AS 品名, TD006 AS 规格, TD007 AS 仓位号,   TD008 AS 计划产量, TD009 AS 已交量  FROM  COPTD"; }
        }
        /// <summary>
        /// MES产品型号
        /// </summary>
        /// <returns></returns>
        public List<string> MesProductTypeList()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT  DISTINCT ProductTypeCommon  ")
                  .Append("FROM    Para_ProductType ")
                  .Append("WHERE  (TypeVisible = '1') AND (MaterialId IS NOT NULL) ");
                DataTable dt = DbHelper.Mes.LoadTable(sb.ToString());
                List<string> ProductTypeList = new List<string>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ProductTypeList.Add(dr[0].ToString());
                    }
                }
                return ProductTypeList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }
        /// <summary>
        /// 未完工的业务订单
        /// </summary>
        /// <param name="containsProductType">所包括品名</param>
        /// <returns></returns>
        public List<CopOrderModel> GetCopOrderBy(string containsProductType)
        {
            string sqlWhere = string.Format(" where (TD005 like'%{0}%' or TD006 LIKE '%{0}%')and (TD016 = 'N') ", containsProductType);
            return ErpDbAccessHelper.FindDataBy<CopOrderModel>(SqlFields, sqlWhere, (dr, m) =>
            {

                m.OrderId = string.Format("{0}-{1}", dr["单别"].ToString().Trim(), dr["单号"].ToString().Trim()); ;
                m.OrderDesc = dr["序号"].ToString().Trim();

                m.ProductID = dr["品号"].ToString().Trim();
                m.ProductName = containsProductType;
                m.ProductSpecify = dr["规格"].ToString().Trim();
                m.WarehouseID = (dr["仓位号"].ToString().Trim());

                m.ProductNumber = dr["计划产量"].ToString().Trim().ToDouble();
                m.FinishNumber = dr["已交量"].ToString().Trim().ToDouble();
            });


        }
    }
    /// <summary>
    /// 销售退换单
    /// </summary>

    public class CopReturnOrderManageDb
    {

        private string GetStoBodySqlFields
        {
            get { return "SELECT   TJ003 AS 序号, TJ004 AS 品号, TJ005 AS 品名, TJ006 AS 规格, TJ007 AS 数量, TJ008 AS 单位  FROM  COPTJ"; }
        }
        private List<CopReturnOrderModel> FindBodyReturnOrderBy(string category, string code)
        {
            string sqlBodyWhere = string.Format(" where TJ001='{0}' and TJ002='{1}'", category, code);
            string sqlHeadWhere = string.Format("SELECT  COPTI.TI004 AS 客户编号, COPMA.MA002 AS 客户简单   FROM    COPTI INNER JOIN  COPMA ON COPTI.TI004 = COPMA.MA001  where COPTI.TI001='{0}' and COPTI.TI002='{1}'", category, code);
            string customerId = string.Empty; string CustomerName = string.Empty;
            DataTable dt = DbHelper.Erp.LoadTable(sqlHeadWhere);
            if (dt != null && dt.Rows.Count > 0)
            {
                customerId = dt.Rows[0]["客户编号"].ToString().Trim();
                CustomerName = dt.Rows[0]["客户简单"].ToString().Trim();
            }
            return ErpDbAccessHelper.FindDataBy<CopReturnOrderModel>(GetStoBodySqlFields, sqlBodyWhere, (dr, m) =>
          {
              m.OrderId = category + "-" + code;
              m.OrderDesc = dr["序号"].ToString();
              m.CustomerId = customerId;
              m.CustomerShortName = CustomerName;
              m.ProductID = dr["品号"].ToString().Trim();
              m.ProductName = dr["品名"].ToString().Trim();
              m.ProductSpecify = dr["规格"].ToString().Trim();
              m.ProductNumber = dr["数量"].ToString().Trim().ToDouble();
              m.ProductUnit = dr["单位"].ToString().Trim();

          });
        }

        public List<CopReturnOrderModel> FindReturnOrderByID(string id)
        {
            var idm = ErpDbAccessHelper.DecomposeID(id);
            return FindBodyReturnOrderBy(idm.Category, idm.Code);
        }

    }
    
}
