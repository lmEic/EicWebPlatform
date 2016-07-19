using Lm.Eic.App.Erp.Domain.PurchaseManage;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using System;
using System.Collections.Generic;
using System.Data;

namespace Lm.Eic.App.Erp.DbAccess.PurchaseManageDb
{
    /// <summary>
    /// 请购数据访问DB
    /// </summary>
    public class RequisitionDb
    {
        #region requisitionHeader

        private string GetReqHeaderSqlFields()
        {
            return "Select TA001,TA002,TA003,TA004,TA006,TA011,TA012,TA013,TA014  from PURTA";
        }

        private void MapReqHeaderRowAndModel(DataRow dr, RequisitionHeaderModel m)
        {
            m.Category = dr["TA001"].ToString();
            m.Code = dr["TA002"].ToString();
            m.BuyFromDepartment = dr["TA004"].ToString();
            m.BuyingDate = dr["TA003"].ToString();
            m.Auditor = dr["TA014"].ToString();
            m.BuyingPerson = dr["TA012"].ToString();
            m.Memo = dr["TA006"].ToString();
            m.RequisitionDate = dr["TA013"].ToString();
            m.TotalCount = dr["TA011"].ToString();
        }

        /// <summary>
        /// 根据查询条件获取请购单单头数据信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private List<RequisitionHeaderModel> FindReqHeaderBy(string sqlWhere)
        {
            return ErpDbAccessHelper.FindDataBy<RequisitionHeaderModel>(GetReqHeaderSqlFields(), sqlWhere, (dr, m) =>
            {
                this.MapReqHeaderRowAndModel(dr, m);
            });
        }

        /// <summary>
        /// 根据采购部门,起止日期获取请购单单头数据信息
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<RequisitionHeaderModel> FindReqHeaderBy(string department, DateTime dateFrom, DateTime dateTo)
        {
            string dFrom = dateFrom.ToString("yyyyMMdd");
            string dTo = dateTo.ToString("yyyyMMdd");
            string sqlWhere = string.Format(" where TA004='{0}' And TA003 Between '{1}' And '{2}'", department, dFrom, dTo);
            return FindReqHeaderBy(sqlWhere);
        }

        /// <summary>
        /// 根据采购部门获取请购单单头数据信息
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<RequisitionHeaderModel> FindReqHeaderByDepartment(string department)
        {
            string sqlWhere = string.Format(" where TA004='{0}'", department);
            return FindReqHeaderBy(sqlWhere);
        }

        #endregion requisitionHeader

        //-----------------RequisitionBody---------------------------------

        #region RequisitionBody

        private string GetReqBodySqlFields()
        {
            return "Select TB001,TB002,TB004,TB005,TB006,TB018,TB014,TB009,TB010,TB008,TB024  from PURTB";
        }

        private void MapReqBodyRowAndModel(DataRow dr, RequisitionBodyModel m)
        {
            m.Code = dr["TB002"].ToString();
            m.Category = dr["TB001"].ToString();
            m.Memo = dr["TB024"].ToString();
            m.ProductID = dr["TB004"].ToString();
            m.ProductName = dr["TB005"].ToString();
            m.ProductSpecify = dr["TB006"].ToString();
            m.PurchaseAmmount = dr["TB018"].ToString();
            m.PurchaseCount = dr["TB014"].ToString().ToDouble();
            m.RequisitionCount = dr["TB009"].ToString().ToDouble();
            m.Supplier = dr["TB010"].ToString();
            m.Warehouse = dr["TB008"].ToString();
        }

        private List<RequisitionBodyModel> FindReqBodyBy(string sqlWhere)
        {
            return ErpDbAccessHelper.FindDataBy<RequisitionBodyModel>(GetReqBodySqlFields(), sqlWhere, (dr, m) =>
             {
                 this.MapReqBodyRowAndModel(dr, m);
             });
        }

        private List<RequisitionBodyModel> FindReqBodyBy(string code, string category)
        {
            string sqlWhere = string.Format(" where TB001='{0}' and TB002='{1}'", category, code);
            return FindReqBodyBy(sqlWhere);
        }

        /// <summary>
        /// 查询该请购单的单身信息
        /// </summary>
        /// <param name="reqID"></param>
        /// <returns></returns>
        public List<RequisitionBodyModel> FindReqBodyByID(string reqID)
        {
            IDModel idm = ErpDbAccessHelper.DecomposeID(reqID);
            return FindReqBodyBy(idm.Code, idm.Category);
        }

        /// <summary>
        /// 查找该部门的请购单单身信息
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<RequisitionBodyModel> FindReqBodyByDepartment(string department, DateTime dateFrom, DateTime dateTo)
        {
            List<RequisitionHeaderModel> reqHeaders = FindReqHeaderBy(department, dateFrom, dateTo);
            List<RequisitionBodyModel> reqBodys = new List<RequisitionBodyModel>();
            if (reqHeaders != null && reqHeaders.Count > 0)
            {
                reqHeaders.ForEach(rh =>
                {
                    reqBodys.AddRange(FindReqBodyBy(rh.Code, rh.Category));
                });
            }
            return reqBodys;
        }

        #endregion RequisitionBody
    }

    /// <summary>
    /// 采购数据访问DB
    /// </summary>
    public class PurchaseDb
    {
        private RequisitionDb reqDb = null;

        public PurchaseDb()
        {
            this.reqDb = new RequisitionDb();
        }

        #region PurchaseHeader

        private string GetPurHeaderSqlFields()
        {
            return "Select TC001,TC002,TC003,TC004,TC011,TC019  from PURTC";
        }

        private void MapPurHeaderRowAndModel(DataRow dr, PurchaseHeaderModel m)
        {
            m.Code = dr["TC002"].ToString();
            m.Category = dr["TC001"].ToString();
            m.PurchaseAmount = dr["TC019"].ToString().ToDouble();
            m.PurchaseDate = dr["TC003"].ToString();
            m.PurchasePerson = dr["TC011"].ToString();
            m.SupplierID = dr["TC004"].ToString();
        }

        /// <summary>
        /// 根据查询条件获取采购单单头数据信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private List<PurchaseHeaderModel> FindPurHeaderBy(string sqlWhere)
        {
            return ErpDbAccessHelper.FindDataBy<PurchaseHeaderModel>(GetPurHeaderSqlFields(), sqlWhere, (dr, m) =>
            {
                this.MapPurHeaderRowAndModel(dr, m);
            });
        }

        private List<PurchaseHeaderModel> FindPurHeaderBy(string code, string category)
        {
            string sqlWhere = string.Format(" where TC001='{0}' and TC002='{1}'", category, code);
            return ErpDbAccessHelper.FindDataBy<PurchaseHeaderModel>(GetPurHeaderSqlFields(), sqlWhere, (dr, m) =>
            {
                this.MapPurHeaderRowAndModel(dr, m);
            });
        }

        /// <summary>
        /// 根据采购部门获取采购购单单头数据信息
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<PurchaseHeaderModel> FindPurHeaderByDepartment(string department, DateTime dateFrom, DateTime dateTo)
        {
            List<PurchaseBodyModel> reqBodys = this.FindPurBodyByDepartment(department, dateFrom, dateTo);
            List<PurchaseHeaderModel> purHeaders = new List<PurchaseHeaderModel>();
            if (reqBodys != null && reqBodys.Count > 0)
            {
                List<string> ids = new List<string>();
                reqBodys.ForEach(rb =>
                {
                    string id = ErpDbAccessHelper.ComposeID(rb.Code, rb.Category);
                    if (!ids.Contains(id))
                    {
                        ids.Add(id);
                        purHeaders.AddRange(FindPurHeaderBy(rb.Code, rb.Category));
                    }
                });
            }
            return purHeaders;
        }

        #endregion PurchaseHeader

        //-----------------PurchaseBody---------------------------------

        #region PurchaseBody

        private string GetPurBodySqlFields()
        {
            return "Select TD001,TD002,TD004,TD005,TD006,TD007,TD008,TD010,TD011,TD012,TD015,TD019,TD026,TD027  from PURTD";
        }

        private void MapPurBodyRowAndModel(DataRow dr, PurchaseBodyModel m)
        {
            m.Code = dr["TD002"].ToString();
            m.Category = dr["TD001"].ToString();
            m.DeliveredCount = dr["TD015"].ToString().ToDouble();
            m.ProductID = dr["TD004"].ToString();
            m.ProductName = dr["TD005"].ToString();
            m.ProductSpecify = dr["TD006"].ToString();
            m.PurchaseAmmount = dr["TD011"].ToString();
            m.PurchaseCount = dr["TD008"].ToString().ToDouble();
            m.PurchaseUnit = dr["TD010"].ToString();
            m.Warehouse = dr["TD007"].ToString();
            m.InventoryCount = dr["TD019"].ToString().ToDouble();
            m.PlanDeliverDate = dr["TD012"].ToString();
            m.BuyingID = ErpDbAccessHelper.ComposeID(dr["TD026"].ToString().Trim(), dr["TD027"].ToString().Trim());
        }

        private List<PurchaseBodyModel> FindPurBodyBy(string sqlWhere)
        {
            return ErpDbAccessHelper.FindDataBy<PurchaseBodyModel>(GetPurBodySqlFields(), sqlWhere, (dr, m) =>
            {
                this.MapPurBodyRowAndModel(dr, m);
            });
        }

        private List<PurchaseBodyModel> FindPurBodyBy(string code, string category)
        {
            string sqlWhere = string.Format(" where TD001='{0}' and TD002='{1}'", category, code);
            return FindPurBodyBy(sqlWhere);
        }

        public List<PurchaseBodyModel> FindPurBodyByID(string id)
        {
            var idm = ErpDbAccessHelper.DecomposeID(id);
            return FindPurBodyBy(idm.Code, idm.Category);
        }

        /// <summary>
        /// 查找该部门的请购单单身信息
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<PurchaseBodyModel> FindPurBodyByDepartment(string department, DateTime dateFrom, DateTime dateTo)
        {
            List<RequisitionHeaderModel> reqHeaders = this.reqDb.FindReqHeaderBy(department, dateFrom, dateTo);
            List<PurchaseBodyModel> purBodys = new List<PurchaseBodyModel>();
            if (reqHeaders != null && reqHeaders.Count > 0)
            {
                List<string> ids = new List<string>();
                reqHeaders.ForEach(rh =>
                {
                    if (!ids.Contains(rh.BuyingID))
                    {
                        ids.Add(rh.BuyingID);
                        IDModel idm = ErpDbAccessHelper.DecomposeID(rh.BuyingID);
                        string sqlWhere = string.Format(" where TD026='{0}' and TD027='{1}'", idm.Category, idm.Code);
                        purBodys.AddRange(FindPurBodyBy(sqlWhere));
                    }
                });
            }
            return purBodys;
        }

        #endregion PurchaseBody
    }

    /// <summary>
    /// 进货数据访问DB
    /// </summary>
    public class StockDb
    {
        private PurchaseDb purDb = null;

        public StockDb()
        {
            this.purDb = new PurchaseDb();
        }

        #region StockHeader

        private string GetStoHeaderSqlFields()
        {
            return "Select TG001,TG002,TG003,TG005,TG014,TG017,TG020,TG021,TG026  from PURTG";
        }

        private void MapStoHeaderRowAndModel(DataRow dr, StockHeaderModel m)
        {
            m.Code = dr["TG002"].ToString();
            m.Category = dr["TG001"].ToString();
            m.BillsDate = dr["TG014"].ToString();
            m.StockAmount = dr["TG017"].ToString().ToDouble();
            m.StockCost = dr["TG020"].ToString().ToDouble();
            m.StockDate = dr["TG003"].ToString();
            m.Supplier = dr["TG005"].ToString();
            m.SupplierFullName = dr["TG021"].ToString();
            m.TotalCount = dr["TG026"].ToString().ToDouble();
        }

        /// <summary>
        /// 根据查询条件获取进货单单头数据信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private List<StockHeaderModel> FindStoHeaderBy(string sqlWhere)
        {
            return ErpDbAccessHelper.FindDataBy<StockHeaderModel>(GetStoHeaderSqlFields(), sqlWhere, (dr, m) =>
            {
                this.MapStoHeaderRowAndModel(dr, m);
            });
        }

        private List<StockHeaderModel> FindStoHeaderBy(string code, string category)
        {
            string sqlWhere = string.Format(" where TG001='{0}' and TG002='{1}'", category, code);
            return ErpDbAccessHelper.FindDataBy<StockHeaderModel>(GetStoHeaderSqlFields(), sqlWhere, (dr, m) =>
            {
                this.MapStoHeaderRowAndModel(dr, m);
            });
        }

        public List<StockHeaderModel> FindStoHeaderByID(string id)
        {
            var idm = ErpDbAccessHelper.DecomposeID(id);
            return FindStoHeaderBy(idm.Code, idm.Category);
        }

        /// <summary>
        /// 根据采购部门获取进货单单头数据信息
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<StockHeaderModel> FindStoHeaderByDepartment(string department, DateTime dateFrom, DateTime dateTo)
        {
            List<StockBodyModel> stoBodys = this.FindStoBodyByDepartment(department, dateFrom, dateTo);
            List<StockHeaderModel> stoHeaders = new List<StockHeaderModel>();
            if (stoBodys != null && stoBodys.Count > 0)
            {
                List<string> ids = new List<string>();
                stoBodys.ForEach(sto =>
                {
                    string id = ErpDbAccessHelper.ComposeID(sto.Code, sto.Category);
                    if (!ids.Contains(id))
                    {
                        ids.Add(id);
                        stoHeaders.AddRange(FindStoHeaderBy(sto.Code, sto.Category));
                    }
                });
            }
            return stoHeaders;
        }

        #endregion StockHeader

        //-----------------StockBody---------------------------------

        #region StockBody

        private string GetStoBodySqlFields()
        {
            return "Select TH001,TH002,TH004,TH005,TH006,TH007,TH009,TH011,TH012,TH014,TH015,TH018,TH019,TH026,TH027,TH033,TH034,TH038  from PURTH";
        }

        private void MapStoBodyRowAndModel(DataRow dr, StockBodyModel m)
        {
            m.Code = dr["TH002"].ToString();
            m.Category = dr["TH001"].ToString();
            m.Auditor = dr["TH038"].ToString();
            m.ProductID = dr["TH004"].ToString();
            m.ProductName = dr["TH005"].ToString();
            m.ProductSpecify = dr["TH006"].ToString();
            m.CheckCount = dr["TH015"].ToString().ToDouble();
            m.CheckDate = dr["TH014"].ToString();
            m.InventoryCount = dr["TH034"].ToString().ToDouble();
            m.Warehouse = dr["TH009"].ToString();
            m.Memo = dr["TH033"].ToString();
            m.PurchaseID = ErpDbAccessHelper.ComposeID(dr["TH011"].ToString().Trim(), dr["TH012"].ToString().Trim());
            m.StockAmount = dr["TH019"].ToString().ToDouble();
            m.StockCount =Convert .ToInt64 ( dr["TH007"].ToString().ToDouble());
            m.StockUnit = dr["TH018"].ToString().ToDouble();
        }

        private List<StockBodyModel> FindStoBodyBy(string sqlWhere)
        {
            return ErpDbAccessHelper.FindDataBy<StockBodyModel>(GetStoBodySqlFields(), sqlWhere, (dr, m) =>
            {
                this.MapStoBodyRowAndModel(dr, m);
            });
        }

        private List<StockBodyModel> FindStoBodyBy(string code, string category)
        {
            string sqlWhere = string.Format(" where TH001='{0}' and TH002='{1}'", category, code);
            return FindStoBodyBy(sqlWhere);
        }

        public List<StockBodyModel> FindStoBodyByID(string id)
        {
            var idm = ErpDbAccessHelper.DecomposeID(id);
            return FindStoBodyBy(idm.Code, idm.Category);
        }

        /// <summary>
        /// 查找该部门的请购单单身信息
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<StockBodyModel> FindStoBodyByDepartment(string department, DateTime dateFrom, DateTime dateTo)
        {
            List<PurchaseHeaderModel> reqHeaders = this.purDb.FindPurHeaderByDepartment(department, dateFrom, dateTo);
            List<StockBodyModel> stoBodys = new List<StockBodyModel>();
            if (reqHeaders != null && reqHeaders.Count > 0)
            {
                List<string> Ids = new List<string>();
                reqHeaders.ForEach(req =>
                {
                    if (!Ids.Contains(req.PurchaseID))
                    {
                        Ids.Add(req.PurchaseID);
                        IDModel idm = ErpDbAccessHelper.DecomposeID(req.PurchaseID);
                        string sqlWhere = string.Format(" where TH011='{0}' and TH012='{1}'", idm.Category, idm.Code);
                        stoBodys.AddRange(FindStoBodyBy(sqlWhere));
                    }
                });
            }
            return stoBodys;
        }

        #endregion StockBody
    }
}