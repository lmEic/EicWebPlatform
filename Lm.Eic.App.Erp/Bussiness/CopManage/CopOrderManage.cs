﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.Erp.DbAccess.CopManageDb;
using Lm.Eic.App.Erp.DbAccess.MocManageDb.OrderManageDb;
using Lm.Eic.App.Erp.Domain.ProductTypeMonitorModel;
using Lm.Eic.App.Erp.Domain.InvManageModel;
using Lm.Eic.App.Erp.DbAccess.InvManageDb;
using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using System.IO;
using Lm.Eic.App.Erp.Domain.CopManageModel;

namespace Lm.Eic.App.Erp.Bussiness.CopManage
{
    /// <summary>
    /// 订单与工单对比
    /// </summary>
    public class CopOrderWorkorderManage
    {
        /// <summary>
        /// 得到制三部的 订单与工单的对比参数
        /// </summary>
        /// <returns></returns>
        public List<ProductTypeMonitorModel> GetMS589ProductTypeMonitor()
        {
            try
            {
                List<ProductTypeMonitorModel> typeMonitorModelList = new List<ProductTypeMonitorModel>();
                //获取MES的产品型号
                var mesPortype = CopOrderCrudFactory.CopOrderManageDb.MesProductTypeList();

                if (mesPortype == null || mesPortype.Count <= 0) return typeMonitorModelList;
                mesPortype.ForEach(e =>
                 {
                     var m = GetProductTypeMonitorInfoBy(e);
                     if (m != null)
                     { typeMonitorModelList.Add(m); }
                 });
                return typeMonitorModelList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 生成EXCEL表格
        /// </summary>
        /// <returns></returns>
        public DownLoadFileModel BuildProductTypeMonitoList(List<ProductTypeMonitorModel> datas, string fileDownLoadName)
        {
            try
            {
                
                if (datas == null || datas.Count < 0) return new DownLoadFileModel().Default();
                var datasGroupping = datas.GetGroupList<ProductTypeMonitorModel>("订单与工单对比");
                return datasGroupping.ExportToExcelMultiSheets<ProductTypeMonitorModel>(fieldmappping).CreateDownLoadExcelFileModel(fileDownLoadName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        #region 依产品型号得到相应信息

        #region 常量字符
        /// <summary>
        ///
        /// </summary>
        const string
               // 全检工单单别
               AllCheckCategory = "523",
               // 现场成品仓标识
               localeFinishedSign = "D05",
               // 来料工单标识
               IncomingmaterialSign = "C03",
               ///保税标识  
               FreeTradeSign = "B03",
               // 已完工字段
               HaveFinishSign = "已完工",
               // 指定完工字段
               SpecifiedFinishSign = "指定完工";
        List<FileFieldMapping> fieldmappping = new List<FileFieldMapping>(){
                 new FileFieldMapping ("Number","项次") ,
                  new FileFieldMapping ("ProductType","品名") ,
                  new FileFieldMapping ("ProductSpecify","规格") ,
                  new FileFieldMapping ("SumCount","汇总") ,
                  new FileFieldMapping ("OrderCount","工单数量")  ,
                  new FileFieldMapping ("LocaleFinishedCount","现场成品仓") ,
                  new FileFieldMapping ("FreeTradeInHouseCount","库存成品"),
                  new FileFieldMapping ("AllCheckOrderCount","全检工单"),
                  new FileFieldMapping ("PutInMaterialCount","来料成品"),
                  new FileFieldMapping ("DifferenceCount","差异"),
                  new FileFieldMapping ("More","备注")
                };
        #endregion
        /// <summary>
        /// 获得一个产品型号对应的订单与工单的对比参数
        /// </summary>
        /// <param name="containsProductTypeOrProductSpecify">包含产品名字和规格</param>
        /// <returns></returns>
        private ProductTypeMonitorModel GetProductTypeMonitorInfoBy(string containsProductTypeOrProductSpecify)
        {
            #region 业务订单
            double localeFinishedCount, freeTradeInHouseCount, putInMaterialCount, orderCount = 0, allCheckOrderCount = 0, sumCount = 0;
            //查找一个型号的订单 并添加到业务订单中
            var getCoporderModel = CopOrderCrudFactory.CopOrderManageDb.GetCopOrderBy(containsProductTypeOrProductSpecify);
            //如果业务订单中没有，直接返回空
            if (getCoporderModel == null || getCoporderModel.Count <= 0) return null;
            //业务订单中未完工订单
            var allCopOrderList = getCoporderModel.FindAll(f => f.ProductName == containsProductTypeOrProductSpecify);
            if (allCopOrderList != null && allCopOrderList.Count > 0)
            {
                sumCount = allCopOrderList.Sum(m => m.ProductNumber) - allCopOrderList.Sum(m => m.FinishNumber);
            }
            #endregion

            #region  工单处理
            // 依型号汇总工单信息 
            OutunfinishedOrderConct(containsProductTypeOrProductSpecify, out orderCount, out allCheckOrderCount);
            #endregion

            #region 成品仓库
            //输出成品仓库信息
            OutProductInStoreConct(containsProductTypeOrProductSpecify, out localeFinishedCount, out freeTradeInHouseCount, out putInMaterialCount);
            #endregion
            return new ProductTypeMonitorModel
            {
                ProductType = containsProductTypeOrProductSpecify,
                ProductSpecify = getCoporderModel.Find(f => f.ProductName == containsProductTypeOrProductSpecify).ProductSpecify,
                //工单总量-工单入库量  不包括“全检工单523”
                OrderCount = orderCount,
                //订单总量-订单已交量
                SumCount = sumCount,
                LocaleFinishedCount = localeFinishedCount,
                FreeTradeInHouseCount = freeTradeInHouseCount,
                PutInMaterialCount = putInMaterialCount,
                //另外统计 “全检工单523”
                AllCheckOrderCount = allCheckOrderCount,
                DifferenceCount = localeFinishedCount + freeTradeInHouseCount + putInMaterialCount + allCheckOrderCount + orderCount - sumCount
            };
        }

        /// <summary>
        ///  获取所有生产工单, 除掉镭射雕刻,客退品
        /// </summary>
        /// <param name="containsProductName"></param>
        /// <returns></returns>
        private List<OrderModel> GetAllProductOrderList(string containsProductName)
        {
            var modedates = OrderCrudFactory.OrderDetailsDb.GetAllOrderBy(containsProductName);
            return OrderCrudFactory.OrderDetailsDb.GetAllOrderBy(containsProductName).
                FindAll(f => !(f.ProductSpecify.Contains("镭射雕刻") || f.OrderId.Contains("528"))); ;
        }
        /// <summary>
        /// 获取成品仓信息
        /// </summary>
        /// <param name="containsProductName"></param>
        /// <returns></returns>
        private List<FinishedProductStoreModel> GetProductInStoreInfoBy(string containsProductName)
        {
            List<FinishedProductStoreModel> productInStoreInfoList = new List<FinishedProductStoreModel>();
            //从销售订单和生产工单中得到型号所对应的所有料号
            List<string> productIDList = GetAllPorductIdBy(containsProductName);
            //对每个料号得到相应的成品仓信息
            productIDList.ForEach(e =>
                 {
                     var mmm = InvOrderCrudFactory.InvManageDb.GetProductStroeInfoBy(e);
                     productInStoreInfoList.AddRange(mmm);
                 });
            return productInStoreInfoList;

        }
        /// <summary>
        /// 获取此产品型号或规格中的所有品号
        /// </summary>
        /// <param name="containsProductName"></param>
        /// <returns></returns>
        private List<string> GetAllPorductIdBy(string containsProductName)
        {
            List<string> productIDList = new List<string>();
            var copOrderList = CopOrderCrudFactory.CopOrderManageDb.GetCopOrderBy(containsProductName);

            var allOrderList = GetAllProductOrderList(containsProductName);
            //销售订单
            copOrderList.ForEach(e =>
            {
                if (!productIDList.Contains(e.ProductID))
                { productIDList.Add(e.ProductID); }
            });
            //所有工单材号
            allOrderList.ForEach(e =>
            {
                if (!productIDList.Contains(e.ProductID))
                { productIDList.Add(e.ProductID); }
            });
            return productIDList;
        }


        /// <summary>
        /// 输出成品仓信息数量
        /// </summary>
        /// <param name="containsProductTypeOrProductSpecify"></param>
        /// <param name="localeFinishedCount"></param>
        /// <param name="freeTradeInHouseCount"></param>
        /// <param name="putInMaterialCount"></param
        /// 
        private void OutProductInStoreConct(string containsProductTypeOrProductSpecify, out double localeFinishedCount, out double freeTradeInHouseCount, out double putInMaterialCount)
        {
            try
            {
                var ProductInStoreInfoList = GetProductInStoreInfoBy(containsProductTypeOrProductSpecify);
                if (ProductInStoreInfoList != null || ProductInStoreInfoList.Count > 0)
                {
                    localeFinishedCount = ProductInStoreInfoList.FindAll(f => f.StroeId == localeFinishedSign).ToList().Sum(m => m.InStroeNumber);
                    freeTradeInHouseCount = ProductInStoreInfoList.FindAll(f => f.StroeId == FreeTradeSign).ToList().Sum(m => m.InStroeNumber);
                    putInMaterialCount = ProductInStoreInfoList.FindAll(f => f.StroeId == IncomingmaterialSign).ToList().Sum(m => m.InStroeNumber);
                }
                else
                { localeFinishedCount = 0; freeTradeInHouseCount = 0; putInMaterialCount = 0; }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 输出未完工信息数量
        /// </summary>
        /// <param name="containsProductTypeOrProductSpecify"></param>
        /// <param name="orderCount"></param>
        /// <param name="allCheckOrderCount"></param>
        private void OutunfinishedOrderConct(string containsProductTypeOrProductSpecify, out double orderCount, out double allCheckOrderCount)
        {
            allCheckOrderCount = 0; orderCount = 0;
            var unfinishedOrderList = GetAllProductOrderList(containsProductTypeOrProductSpecify).
                                   FindAll(e => !(e.OrderFinishStatus == HaveFinishSign || e.OrderFinishStatus == SpecifiedFinishSign));
            if (unfinishedOrderList == null || unfinishedOrderList.Count <= 0)
                return;
            //不包括全检工单的工单
            var UnallCheckCategoryList = unfinishedOrderList.FindAll(f => !f.OrderId.Contains(AllCheckCategory));
            if (UnallCheckCategoryList != null && UnallCheckCategoryList.Count > 0)
            {
                orderCount = UnallCheckCategoryList.Sum(f => f.Count) - UnallCheckCategoryList.Sum(f => f.InStoreCount);
            }
            //包括全检工单的工单
            var allCheckCategoryList = unfinishedOrderList.FindAll(f => f.OrderId.Contains(AllCheckCategory));
            if (allCheckCategoryList != null && allCheckCategoryList.Count > 0)
            {
                allCheckOrderCount = allCheckCategoryList.Sum(f => f.Count) - allCheckCategoryList.Sum(f => f.InStoreCount);
            }

        }

        #endregion
    }

    /// <summary>
    /// 销售退换单 管理
    /// </summary>
    public class CopReturnOrderManage
    {
        /// <summary>
        /// 得到退料和换货单信息
        /// <returns></returns>
        public List<CopReturnOrderModel> GetCopReturnOrderInfoBy(string returnHandleOrder)
        {
            return CopOrderCrudFactory.CopReturnOrderManageDb.FindReturnOrderByID(returnHandleOrder);
        }
    }
}
