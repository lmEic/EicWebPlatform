using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Lm.Eic.App.Erp.Domain.PMSManageModel;

namespace Lm.Eic.App.Erp.DbAccess.PMSDb
{
 public  class PmsMaterialManageDb
    {
        #region
        private string GetReqHeaderSqlFields()
        {
            return "Select TA001,TA002,TA003,TA004,TA006,TA011,TA012,TA013,TA014  from PURTA";
        }

        private void MapReqHeaderRowAndModel(DataRow dr, MaterialHaaderModel m)
        {
            m.Category = dr["TA001"].ToString();
            m.Code = dr["TA002"].ToString();
            m.ProductID = dr["TA004"].ToString();
            m.ProductName = dr["TA003"].ToString();
            m.ProductSpecify = dr["TA014"].ToString();
            m.ProductCount= dr["TA012"].ToString();
            m.ProductDate= dr["TA006"].ToString();
            m.Unit = dr["TA013"].ToString();
            m. = dr["TA011"].ToString();
        }

        /// <summary>
        /// 根据查询条件获取工单头数据信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private List<MaterialHaaderModel> FindHeaderBy(string sqlWhere)
        {
            return ErpDbAccessHelper.FindDataBy<MaterialHaaderModel>(GetReqHeaderSqlFields(), sqlWhere, (dr, m) =>
            {
                this.MapReqHeaderRowAndModel(dr, m);
            });
        }

        /// <summary>
        /// 根据采购部门,起止日期获取请购单单头数据信息
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<MaterialHaaderModel> FindReqHeaderBy(string orderId)
        {
            IDModel idm = ErpDbAccessHelper.DecomposeID(orderId);
            
            string sqlWhere = string.Format(" where TA004='{0}' And TA003 ='{1}'",idm.Category,idm.Code);
            return FindHeaderBy(sqlWhere);
        }

        /// <summary>
        /// 根据采购部门获取请购单单头数据信息
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<MaterialHaaderModel> FindReqHeaderByDepartment(string department)
        {
            string sqlWhere = string.Format(" where TA004='{0}'", department);
            return FindReqHeaderBy(sqlWhere);
        }
#endregion
    }
}
