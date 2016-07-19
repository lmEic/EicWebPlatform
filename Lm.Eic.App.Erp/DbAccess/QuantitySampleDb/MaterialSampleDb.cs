using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using System.Data;
using Lm.Eic.App.Erp.DbAccess;

namespace Lm.Eic.App.Erp.DbAccess.QuantitySampleDb
{
    public class PorductInfoDb
    {

        private string GetPorductSqlFields()
        {
            return "Select MB001,MB002,MB003,MB004,MB015,MB029,MB068,MB028  from  INVMB ";
        }
        /// <summary>
        ///  由数据给Model赋值
        /// </summary>
        /// <param name="dr">DataTable的一行</param>
        /// <param name="model"></param>
        private void MapProductRowAndModel(DataRow dr, ProductModel model)
        {
            model.ProductID = dr["MB001"].ToString();
            model.ProductName = dr["MB002"].ToString();
            model.ProductSpecify = dr["MB003"].ToString();
            model.UnitedName = dr["MB004"].ToString();
            model.UniteCount = dr["MB015"].ToString();
            model.ProductDrawID = dr["MB029"].ToString();
            model.ProductBelongDepartment = dr["MB068"].ToString();
            model.Memo = dr["MB028"].ToString();
        }

       /// <summary>
       /// 由物料料号得到物料所有相关信息
       /// </summary>
       /// <param name="marteial">料号</param>
       /// <returns></returns>
        public List<ProductModel> GetProductInfoBy(string marteial)
        {
            string sqlWhere = string.Format(" where MB001='{0}'", marteial.Trim() );
            return ErpDbAccessHelper.FindDataBy<ProductModel>(GetPorductSqlFields(), sqlWhere, (dr, m) =>
            {
                this.MapProductRowAndModel(dr, m);
            });
        }

        /// <summary>
    }
    public class MaterialSampleDb
    {
        PurchaseManageDb.StockDb StockDb = null;
        PorductInfoDb PorductInfoS = null;
        public MaterialSampleDb ()
        {
            PorductInfoS = new PorductInfoDb();
            StockDb = new PurchaseManageDb.StockDb();
        }
       /// <summary>  
        ///  由单子 得到单别 单号
        /// <param name="id">单号XXX-XXXXXXX</param>
       /// <returns></returns>
        public List<MaterialModel> FindMaterialBy(string id)
        {
            var idm = ErpDbAccessHelper.DecomposeID(id);
            return GetMaterialIdBy(idm.Category, idm.Code);
        }
        /// <summary>
        /// 所有进货单
        /// </summary>
        /// <param name="category"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private List<MaterialModel> GetMaterialIdBy(string category, string code)
        {

            string sql = "";
            string dtSqlMaterialId = "";
            string dtSqlSum = "";
            if (category.Contains("5") & category != "591")
            {
                return GetMaterials(category, code);
            }
            else
            {
                switch (category)
                {
                    case "591":
                        sql = string.Format("SELECT  TH029 AS 进货日期,TH005 AS 供应商代码 FROM  MOCTH  WHERE   (TH001= '{0}') AND (TH002 = '{1}')", category, code);

                        dtSqlMaterialId = string.Format("SELECT  DISTINCT TI004 AS 料号 FROM  MOCTI  WHERE   (TI001= '{0}') AND (TI002 = '{1}')", category, code);
                        dtSqlSum = string.Format("SELECT  Sum(TI007) AS 数量  FROM  MOCTI  WHERE   (TI001= '{0}') AND (TI002 = '{1}')AND (TI004='", category, code);
                        break;
                    case "110":
                        sql = string.Format("SELECT  TA014 AS 进货日期,TA004 AS 供应商代码 FROM  INVTA  WHERE   (TA001= '{0}') AND (TA002 = '{1}')", category, code);

                        dtSqlMaterialId = string.Format("SELECT  DISTINCT TB004 AS 料号 FROM INVTB  WHERE   (TB001= '{0}') AND (TB002 = '{1}')", category, code);
                        dtSqlSum = string.Format("SELECT  Sum(TB007) AS 数量  FROM  INVTB  WHERE   (TB001= '{0}') AND (TB002 = '{1}')AND (TB004='", category, code);
                        break;
                    default:
                        sql = string.Format("SELECT  TG014 AS 进货日期,TG005 AS 供应商代码 FROM  PURTG  WHERE   (TG001= '{0}') AND (TG002 = '{1}')", category, code);

                        dtSqlMaterialId = string.Format("SELECT  DISTINCT TH004 AS 料号 FROM  PURTH   WHERE   (TH001= '{0}') AND (TH002 = '{1}')", category, code);
                        dtSqlSum = string.Format("SELECT  Sum(TH007) AS 数量 FROM  PURTH   WHERE   (TH001= '{0}') AND (TH002 = '{1}') AND (TH004='", category, code);
                        break;


                }
                return GetMaterialsBy(category, code, sql, dtSqlMaterialId, dtSqlSum);
            }

        }
       /// <summary>
       /// 得到进货物料  341 342 343 344
       /// </summary>
       /// <param name="category">单别</param>
       /// <param name="code">单号</param>
       /// <returns></returns>
       public  List<MaterialModel> GetPurchaseMaterialBy(string category, string code)
        {
            MaterialModel Material = null;
            var PurchaseHeader = StockDb.FindStoHeaderByID(category + "-" + code).FirstOrDefault();
            var PurchaseBody = StockDb.FindStoBodyByID(category + "-" + code);
            List<MaterialModel> Materials = new List<MaterialModel>(); 
            string SupplierID = GetSupplierNameBy(PurchaseHeader.Supplier); 
            string InMaterialDate = PurchaseHeader.StockDate;
            foreach  (var s in PurchaseBody)
            {
                var PorductInfo = PorductInfoS.GetProductInfoBy(s.ProductID).FirstOrDefault();
                Material = new MaterialModel()
                {
                    ProduceNumber =s.StockCount,
                    ProductDrawID = PorductInfo.ProductDrawID,
                    ProductName = PorductInfo.ProductName,
                    ProductStandard = PorductInfo.ProductSpecify,
                    ProductID = PorductInfo.ProductID,
                    ProductSupplier = SupplierID,
                    ProduceInDate = DateTime.ParseExact(InMaterialDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                    Category = category,
                    Code = code
                };
                Materials.Add(Material);
            }
            return Materials;
        }
         /// <summary>
         ///   得到供应商名称
         /// </summary>
         /// <param name="SupplierID">供应商ID</param>
         /// <returns></returns>
        private  string GetSupplierNameBy(string SupplierID)
        {
            DataTable dt2 = DbHelper.Erp.LoadTable("SELECT  MA002 AS 供应商  FROM PURMA  WHERE (MA001 = '" + SupplierID + "')");
            if (dt2.Rows.Count > 0)
            {
                SupplierID = SupplierID + "/" + dt2.Rows[0]["供应商"].ToString().Trim();
            }
            return SupplierID;
        }

      /// <summary> 
      /// 进货单
      /// </summary>
      /// <param name="category">单别</param>
      /// <param name="code">单号</param>
      /// <param name="sql"></param>
      /// <param name="dtSqlMaterialId"></param>
      /// <param name="dtSQLsum"></param>
      /// <returns></returns>
        private List<MaterialModel> GetMaterialsBy(string category, string code, string sql, string dtSqlMaterialId, string dtSqlSum)
        {
            MaterialModel Material = null;
            List<MaterialModel> Materials = new List<MaterialModel>(); ;
            string SupplierID = string.Empty ;
            string InMaterialDate =string .Empty ;
            DataTable DTds = DbHelper.Erp.LoadTable(sql);
            if (DTds.Rows.Count > 0)
            {
                SupplierID = DTds.Rows[0]["供应商代码"].ToString().Trim();
                InMaterialDate = DTds.Rows[0]["进货日期"].ToString().Trim();
                DataTable dt2 = DbHelper.Erp.LoadTable("SELECT  MA002 AS 供应商  FROM PURMA  WHERE (MA001 = '" + SupplierID + "')");
                if (dt2.Rows.Count > 0)
                {
                    SupplierID = SupplierID + "/" + dt2.Rows[0]["供应商"].ToString().Trim();
                }
                DataTable DT = DbHelper.Erp.LoadTable(dtSqlMaterialId);
                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow dr in DT.Rows)
                    {
                        long ProduceNumber = 0;
                        //  SELECT  Sum(TH007) AS 数量 FROM  PURTH   WHERE   (TH001= '" + category + "') AND (TH002 = '" + code + "') AND (TH004='"; ;
                        DataTable dtSum = DbHelper.Erp.LoadTable(dtSqlSum + dr["料号"].ToString() + "')");
                        if (dtSum.Rows.Count > 0)
                        {
                            ProduceNumber =Convert .ToInt64( dtSum.Rows[0]["数量"].ToString().Trim());
                        }
                       
                       var PorductInfo= PorductInfoS.GetProductInfoBy(dr["料号"].ToString()).FirstOrDefault ();
                       Material = new MaterialModel()
                       {
                         ProduceNumber = ProduceNumber,
                         ProductDrawID = PorductInfo.ProductDrawID ,
                         ProductName = PorductInfo .ProductName,
                         ProductStandard = PorductInfo.ProductSpecify,
                         ProductID = PorductInfo.ProductID,
                         ProductSupplier = SupplierID,
                         ProduceInDate =  DateTime.ParseExact(InMaterialDate , "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                         Category = category,
                         Code = code
                        };
                      
                        Materials.Add(Material);
                    }
                }
            }
            return Materials;
        }
           /// <summary>
           /// 制令单
           /// </summary>
           /// <param name="category">单别</param>
           /// <param name="code">单号</param>
           /// <returns></returns>
        private List<MaterialModel> GetMaterials(string category, string code)
        {
            string TA001 = category;
            string TA002 = code;
            MaterialModel Material = null;
            List<MaterialModel> Materials = new List<MaterialModel>(); ;
            string SQL = "SELECT  TA006 AS 料号,TA015 AS 数量,TA026 AS 单别,TA027 AS 单号,TA003 AS 开单日期  FROM  MOCTA  WHERE   (TA001= '" + TA001 + "') AND (TA002 = '" + TA002 + "')";
            DataTable DTds = DbHelper.Erp.LoadTable(SQL);
            if (DTds.Rows.Count > 0)
            {
                Material = new MaterialModel();
                DataTable dt = DbHelper.Erp.LoadTable("SELECT MB029 AS 图号, MB002 AS 品名,  MB003 AS 规格  FROM INVMB WHERE (MB001='" + DTds.Rows[0]["料号"].ToString() + "')");
                if (dt.Rows.Count > 0)
                {
                    Material.ProductDrawID = dt.Rows[0]["图号"].ToString().Trim();
                    Material.ProductName = dt.Rows[0]["品名"].ToString().Trim();
                    Material.ProductStandard = dt.Rows[0]["规格"].ToString().Trim();

                }
                DataTable dtss = DbHelper.Erp.LoadTable("SELECT TC004 AS 客户编号  FROM COPTC WHERE (TC001='" + DTds.Rows[0]["单别"].ToString() + "')AND (TC002 = '" + DTds.Rows[0]["单号"].ToString() + "')");
                if (dtss.Rows.Count > 0)
                {
                    Material.ProductSupplier = dtss.Rows[0]["客户编号"].ToString().Trim();
                }
                Material.Category = category;
                Material.Code = code;
                Material.ProductID = DTds.Rows[0]["料号"].ToString().Trim();
                Material.ProduceNumber =Convert.ToInt64 ( DTds.Rows[0]["数量"].ToString().Trim().ToDouble ());
                Material.ProduceInDate = DateTime.ParseExact(DTds.Rows[0]["开单日期"].ToString().Trim(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                Materials.Add(Material);
            }
            return Materials;
        }
    }
  

    
}
