using System;
using System.Collections.Generic;
using System.Linq;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using System.Data;
using Lm.Eic.App.Erp.DbAccess.PurchaseManageDb;

namespace Lm.Eic.App.Erp.DbAccess.QuantitySampleDb
{
    public class OrderIdInspectionDb
    {
        StockDb StockDb = null;
        MaterialInfoDb PorductInfoS = null;
        public OrderIdInspectionDb()
        {
            PorductInfoS = new MaterialInfoDb();
            StockDb = new StockDb();
        }
        /// <summary>  
        ///  由单子 得到单别 单号   (单别为 591 110 (341 342 343 344)和制令单)
        /// <param name="id">单号XXX-XXXXXXX</param>
        /// <returns></returns>
        public List<MaterialModel> FindMaterialBy(string id)
        {
            var idm = ErpDbAccessHelper.DecomposeID(id);
            return GetMaterialIdBy(idm.Category, idm.Code);
        }
        /// <summary>
        ///找到ERP中所有的进货单的物料信息（数量不超过100）
        /// </summary>
        /// <param name="searchStartDate"></param>
        /// <param name="searchEndDate"></param>
        /// <returns></returns>
        public List<MaterialModel> FindErpAllMasterilBy(DateTime searchStartDate, DateTime searchEndDate)
        {

            List<MaterialModel> masterialAllinfo = new List<MaterialModel>();
            List<string> allOrderId = GetAllMaterialOrderId(searchStartDate, searchEndDate);
            if (allOrderId == null || allOrderId.Count <= 0) return masterialAllinfo;
            allOrderId.ForEach(e =>
            {
                if (masterialAllinfo.Count < 200)
                    masterialAllinfo.AddRange(FindMaterialBy(e));
            });
            return masterialAllinfo;
        }

        /// <summary>
        ///找到ERP中所有的进货单的物料信息（数量不超过100）
        /// </summary>
        /// <param name="searchStartDate"></param>
        /// <param name="searchEndDate"></param>
        /// <returns></returns>
        public List<MaterialModel> FindErpAllMasterilBy(DateTime searchStartDate, DateTime searchEndDate, string department)
        {

            List<MaterialModel> masterialAllinfo = new List<MaterialModel>();
            List<string> allOrderId = GetAllMaterialOrderId(searchStartDate, searchEndDate, department);
            if (allOrderId == null || allOrderId.Count <= 0) return masterialAllinfo;
            allOrderId.ForEach(e =>
            {
                if (masterialAllinfo.Count < 200)
                    masterialAllinfo.AddRange(FindMaterialBy(e));
            });
            return masterialAllinfo;
        }
        /// <summary>
        /// 得到进货物料  341 342 343 344
        /// </summary>
        /// <param name="category">单别</param>
        /// <param name="code">单号</param>
        /// <returns></returns>
        public List<MaterialModel> GetPurchaseMaterialBy(string category, string code)
        {
            MaterialModel Material = null;
            var PurchaseHeader = StockDb.FindStoHeaderByID(category + "-" + code).FirstOrDefault();
            var PurchaseBody = StockDb.FindStoBodyByID(category + "-" + code);
            List<MaterialModel> Materials = new List<MaterialModel>();
            string SupplierID = GetSupplierNameBy(PurchaseHeader.Supplier);
            string InMaterialDate = PurchaseHeader.StockDate;
            foreach (var s in PurchaseBody)
            {
                var PorductInfo = PorductInfoS.GetProductInfoBy(s.ProductID).FirstOrDefault();
                Material = new MaterialModel()
                {
                    ProduceNumber = s.StockCount,
                    ProductDrawID = PorductInfo.MaterialrawID.Trim(),
                    ProductName = PorductInfo.MaterailName.Trim(),
                    ProductStandard = PorductInfo.MaterialSpecify.Trim(),
                    ProductID = PorductInfo.ProductMaterailId.Trim(),
                    ProductSupplier = SupplierID.Trim(),
                    ProduceInDate = DateTime.ParseExact(InMaterialDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                    Category = category,
                    Code = code
                };
                Materials.Add(Material);
            }
            return Materials;
        }


        /// <summary>
        /// 得到所有进货单单号  数量不要超过（100）
        /// </summary>
        /// <param name="searchStartDate"></param>
        /// <param name="searchEndDate"></param>
        /// <returns></returns>
        private List<string> GetAllMaterialOrderId(DateTime searchStartDate, DateTime searchEndDate)
        {
            List<string> eeturnOrderList = new List<string>();
            if (searchEndDate >= searchStartDate)
            {
                eeturnOrderList = GetAllMaterialOrderBy(searchStartDate);
                for (int i = 1; i <= (searchEndDate - searchStartDate).Days; i++)
                {
                    DateTime spanDate = searchStartDate.AddDays(i);
                    if (eeturnOrderList.Count <= 100)
                        eeturnOrderList.AddRange(GetAllMaterialOrderBy(spanDate));
                    else break;

                }
            }
            return eeturnOrderList;

        }

        /// <summary>
        /// 得到所有进货单单号  数量不要超过（100）
        /// </summary>
        /// <param name="searchStartDate"></param>
        /// <param name="searchEndDate"></param>
        /// <returns></returns>
        private List<string> GetAllMaterialOrderId(DateTime searchStartDate, DateTime searchEndDate, string department)
        {
            List<string> returnOrderList = new List<string>();
            if (searchEndDate >= searchStartDate)
            {
                returnOrderList = GetAllMaterialOrderBy(searchStartDate, department);
                for (int i = 1; i <= (searchEndDate - searchStartDate).Days; i++)
                {
                    DateTime spanDate = searchStartDate.AddDays(i);
                    if (returnOrderList.Count <= 100)
                        returnOrderList.AddRange(GetAllMaterialOrderBy(spanDate, department));
                    else break;

                }
            }
            return returnOrderList;

        }
        /// <summary>
        /// 进料检验
        /// </summary>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        private List<string> GetAllMaterialOrderBy(DateTime searchDate)
        {
            List<string> OrderId = new List<string>();
            string Startsql591 = string.Empty;
            string Startsql110 = string.Empty;
            string Startsql34 = string.Empty;
            string startDateStr = searchDate.ToDateTimeShortStr();
            if (startDateStr != string.Empty)
            {
                Startsql591 = "AND (TH029 = '" + startDateStr + "')";
                Startsql110 = "AND (TA014 = '" + startDateStr + "')";
                Startsql34 = "AND (TG014 = '" + startDateStr + "')";
            }
            DataTable dt34 = DbHelper.Erp.LoadTable("SELECT TG001,TG002   FROM PURTG  WHERE (TG001 = '341' OR TG001 = '343')" + Startsql34);
            if (dt34.Rows.Count > 0)
            {
                foreach (DataRow dt in dt34.Rows)
                {
                    OrderId.Add(dt[0].ToString().Trim() + "-" + dt[1].ToString().Trim());
                }


            }
            DataTable dt591 = DbHelper.Erp.LoadTable("SELECT  TH001,TH002  FROM  MOCTH  WHERE (TH001 = '591')" + Startsql591);
            if (dt591.Rows.Count > 0)
            {
                foreach (DataRow dt in dt591.Rows)
                {
                    OrderId.Add(dt[0].ToString().Trim() + "-" + dt[1].ToString().Trim());
                }

            }
            DataTable dt110 = DbHelper.Erp.LoadTable("SELECT TA001,TA002   FROM  INVTA  WHERE  (TA001 = '110')" + Startsql110);
            if (dt110.Rows.Count > 0)
            {
                foreach (DataRow dt in dt110.Rows)
                {
                    OrderId.Add(dt[0].ToString().Trim() + "-" + dt[1].ToString().Trim());
                }
            }
            return OrderId;
        }
        /// <summary>
        /// FQC在制单安部门获得物料
        /// </summary>
        /// <param name="searchDate"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<string> GetAllMaterialOrderBy(DateTime searchDate, string department)
        {

            List<string> OrderId = new List<string>();
            string sql = string.Format("SELECT TA001,TA002,TA006,TA034,TA035,TA017,TA015 FROM  MOCTA   WHERE  (TA021 = '{0}') AND (TA009='{1}') AND (TA035 NOT LIKE '%镭射雕刻%') order by TA001", department, searchDate.ToDateTimeShortStr());
            DataTable dt110 = DbHelper.Erp.LoadTable(sql);
            if (dt110.Rows.Count > 0)
            {
                foreach (DataRow dt in dt110.Rows)
                {
                    OrderId.Add(dt[0].ToString().Trim() + "-" + dt[1].ToString().Trim());
                }
            }
            return OrderId;
        }
        /// <summary>
        /// 工单生产状态查询
        /// </summary>
        /// <param name="department">部门(MS1,MS10,MS2,MS3,MS5,MS6 ,MS7 ,PT1)</param>
        /// <param name="orderStatus">完工、在制</param>
        /// <returns></returns>
        public List<ProductionOrderIdInfo> GetProductionOrderIdInfoBy(string department, string orderStatus)
        {
            string orderStatusSql = OrderStatusStr(orderStatus);
            List<ProductionOrderIdInfo> ProductionOrderIdDatas = new List<ProductionOrderIdInfo>();
            ProductionOrderIdInfo OrderIdData = null;
            string sql = string.Format("SELECT TA001, TA002, TA006, TA034, TA035, TA017, TA015,TA021,TA011,TA003,TA010 FROM MOCTA WHERE(TA021 = '{0}')  {1}   ORDER BY TA002 ", department, orderStatusSql);
            DataTable dt = DbHelper.Erp.LoadTable(sql);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    OrderIdData = new ProductionOrderIdInfo()
                    {
                        ProductionDepartment = dr[7].ToString().Trim(),
                        ProductStatus = RetrunOrderStatus(dr[8].ToString().Trim()),
                        Category = dr[0].ToString().Trim(),
                        Code = dr[1].ToString().Trim(),
                        ProduceNumber = Convert.ToDouble(dr[6].ToString().Trim()),
                        ProductId = dr[2].ToString().Trim(),
                        ProductName = dr[3].ToString().Trim(),
                        ProductSpec = dr[4].ToString().Trim(),
                        PutInStoreNumber = Convert.ToDouble(dr[5].ToString().Trim()),
                        PlanEndProductionDate = dr[10].ToString().Trim().ToDate(),
                        PlanStartProductionDate = dr[9].ToString().Trim().ToDate(),
                    };
                    if (!ProductionOrderIdDatas.Contains(OrderIdData))
                        ProductionOrderIdDatas.Add(OrderIdData);
                }
            }
            return ProductionOrderIdDatas;
        }

        public List<ProductionOrderIdInfo> GetProductionOrderIdInfoBy(string orderId)
        {
            List<ProductionOrderIdInfo> ProductionOrderIdDatas = new List<ProductionOrderIdInfo>();
            ProductionOrderIdInfo OrderIdData = null;
            string sql = string.Format("SELECT TA001, TA002, TA006, TA034, TA035, TA017, TA015,TA021,TA011 FROM MOCTA WHERE  CAST(RTRIM(TA001) AS varchar(10)) + '-' + CAST(RTRIM(TA002) AS varchar(10)) ='{0}'   ORDER BY TA002 ", orderId);
            DataTable dt = DbHelper.Erp.LoadTable(sql);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    OrderIdData = new ProductionOrderIdInfo()
                    {
                        ProductionDepartment = dr[7].ToString().Trim(),
                        ProductStatus = dr[8].ToString().Trim(),
                        Category = dr[0].ToString().Trim(),
                        Code = dr[1].ToString().Trim(),
                        ProduceNumber = Convert.ToDouble(dr[6].ToString().Trim()),
                        ProductId = dr[2].ToString().Trim(),
                        ProductName = dr[3].ToString().Trim(),
                        ProductSpec = dr[4].ToString().Trim(),
                        PutInStoreNumber = Convert.ToDouble(dr[5].ToString().Trim()),
                    };
                    if (!ProductionOrderIdDatas.Contains(OrderIdData))
                        ProductionOrderIdDatas.Add(OrderIdData);
                }
            }
            return ProductionOrderIdDatas;
        }
        string OrderStatusStr(string orderStatus)
        {
            switch (orderStatus)
            {
                case "完工":
                    return " AND(TA011 In('Y', 'y'))";
                case "在制":
                    return " AND(TA011 IN('1', '2', '3'))";
                default:
                    return string.Empty;
            }
        }
        string RetrunOrderStatus(string orderStatus)
        {
            switch (orderStatus)
            {
                case "1":
                    return "未开工";
                case "2":
                    return "已发料";
                case "3":
                    return "未完工";
                case "y":
                    return "指完工";
                case "Y":
                    return "已完工";
                default:
                    return orderStatus;
            }
        }

        #region     私有方法
        /// <summary>
        /// 所有进货单
        /// </summary>
        /// <param name="category"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private List<MaterialModel> GetMaterialIdBy(string category, string code)
        {

            string sql = string.Empty;
            string dtSqlMaterialId = string.Empty;
            string dtSqlSum = string.Empty;
            if (category == null || code == null) return new List<MaterialModel>();
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
        ///   得到供应商名称
        /// </summary>
        /// <param name="SupplierID">供应商ID</param>
        /// <returns></returns>
        private string GetSupplierNameBy(string SupplierID)
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
            string SupplierID = string.Empty;
            string InMaterialDate = string.Empty;
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
                        double ProduceNumber = 0;
                        string materialId = dr["料号"].ToString();
                        //  SELECT  Sum(TH007) AS 数量 FROM  PURTH   WHERE   (TH001= '" + category + "') AND (TH002 = '" + code + "') AND (TH004='"; ;
                        DataTable dtSum = DbHelper.Erp.LoadTable(dtSqlSum + materialId + "')");
                        if (dtSum.Rows.Count > 0)
                        {
                            ProduceNumber = dtSum.Rows[0]["数量"].ToString().Trim().ToDouble();
                        }

                        var PorductInfo = PorductInfoS.GetProductInfoBy(dr["料号"].ToString()).FirstOrDefault();
                        Material = new MaterialModel()
                        {
                            ProduceNumber = ProduceNumber,
                            ProductDrawID = PorductInfo.MaterialrawID.Trim(),
                            ProductName = PorductInfo.MaterailName.Trim(),
                            ProductStandard = PorductInfo.MaterialSpecify.Trim(),
                            ProductID = PorductInfo.ProductMaterailId.Trim(),
                            ProductSupplier = SupplierID.Trim(),
                            ProduceInDate = DateTime.ParseExact(InMaterialDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
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
                Material.ProduceNumber = DTds.Rows[0]["数量"].ToString().Trim().ToDouble();
                Material.ProduceInDate = DateTime.ParseExact(DTds.Rows[0]["开单日期"].ToString().Trim(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                Materials.Add(Material);
            }
            return Materials;
        }


        #endregion   私有方法
    }


    /// <summary>
    /// ERP中由料号得到物料相关信息
    /// </summary>
    public class MaterialInfoDb
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
        private void MapProductRowAndModel(DataRow dr, ProductMaterailDto model)
        {
            model.ProductMaterailId = dr["MB001"].ToString();
            model.MaterailName = dr["MB002"].ToString();
            model.MaterialSpecify = dr["MB003"].ToString();
            model.UnitedName = dr["MB004"].ToString();
            model.UniteCount = dr["MB015"].ToString();
            model.MaterialrawID = dr["MB029"].ToString();
            model.MaterialBelongDepartment = dr["MB068"].ToString();
            model.Memo = dr["MB028"].ToString();
        }

        /// <summary>
        /// 由物料料号得到物料所有相关信息
        /// </summary>
        /// <param name="marteial">料号</param>
        /// <returns></returns>
        public List<ProductMaterailDto> GetProductInfoBy(string marteial)
        {
            if (marteial == null || marteial == string.Empty) return new List<ProductMaterailDto>();
            string sqlWhere = string.Format(" where MB001='{0}'", marteial.Trim());
            return ErpDbAccessHelper.FindDataBy<ProductMaterailDto>(GetPorductSqlFields(), sqlWhere, (dr, m) =>
            {
                this.MapProductRowAndModel(dr, m);
            });
        }

    }

}
