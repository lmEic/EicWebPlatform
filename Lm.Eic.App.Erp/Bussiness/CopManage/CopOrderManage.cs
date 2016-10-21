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
using Lm.Eic.App.Erp.DbAccess.CopManageDb;
using System.IO;

namespace Lm.Eic.App.Erp.Bussiness.CopManage
{
  
  public   class CopOrderManage
  {
 
      public CopOrderManage()
      { }
      #region 常量字符
      /// <summary>
      /// 全检工单单别
      /// </summary>
     private  const string AllCheckCategory = "523";
      /// <summary>
      /// 现场成品仓标识
      /// </summary>
     private const string localeFinishedSign = "D05";
      /// <summary>
      /// 来料工单标识
      /// </summary>
     private const string IncomingmaterialSign = "C03";
      /// <summary>
      ///保税标识
      /// </summary>
     private const string FreeTradeSign = "B03";
      /// <summary>
      /// 已完工字段
      /// </summary>
     private const string HaveFinishSign = "已完工";
      /// <summary>
      /// 指定完工字段
      /// </summary>
     private const string SpecifiedFinishSign = "指定完工";
      #endregion
     /// <summary>
      /// 获得一个产品型号对应的订单与工单的对比参数
      /// </summary>
      /// <param name="containsProductTypeOrProductSpecify">包含产品名字和规格</param>
      /// <returns></returns>
     public  ProductTypeMonitorModel GetProductTypeMonitorInfoBy(string containsProductTypeOrProductSpecify)
     {
         //查找一个型号的订单 并添加到业务订单中
         var getCoporderModel = CopOrderCrudFactory.CopOrderManageDb.GetCopOrderBy(containsProductTypeOrProductSpecify);
         //如果业务订单中没有，直接返回空
         if (getCoporderModel == null || getCoporderModel.Count <= 0) return null;
         // 依型号汇总工单信息 
         var unfinishedOrderList = GetAllProductOrderList(containsProductTypeOrProductSpecify).
                                   FindAll(e => !(e.OrderFinishStatus == HaveFinishSign || e.OrderFinishStatus == SpecifiedFinishSign));
         if (unfinishedOrderList == null || unfinishedOrderList.Count <= 0) return null;
         //依型号成品仓信息  
         double localeFinishedCount ,freeTradeInHouseCount ,putInMaterialCount, orderCount = 0, allCheckOrderCount = 0, sumCount = 0;
         //输出成品仓库信息
         OutProductInStoreConctBy(containsProductTypeOrProductSpecify, out localeFinishedCount, out freeTradeInHouseCount, out putInMaterialCount);
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
         //业务订单中未完工订单
         var allCopOrderList = getCoporderModel.FindAll(f => f.ProductName == containsProductTypeOrProductSpecify);
         if (allCopOrderList != null && allCopOrderList.Count > 0)
         {
             sumCount = allCopOrderList.Sum(m => m.ProductNumber) - allCopOrderList.Sum(m => m.FinishNumber);
         }
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
      /// 输出成品仓信息数量
      /// </summary>
      /// <param name="containsProductTypeOrProductSpecify"></param>
      /// <param name="localeFinishedCount"></param>
      /// <param name="freeTradeInHouseCount"></param>
      /// <param name="putInMaterialCount"></param>
      private void OutProductInStoreConctBy(string containsProductTypeOrProductSpecify, out double localeFinishedCount, out double freeTradeInHouseCount, out double putInMaterialCount)
      {
          var ProductInStoreInfoList = GetProductInStoreInfoBy(containsProductTypeOrProductSpecify);
          if (ProductInStoreInfoList != null || ProductInStoreInfoList.Count > 0)
          {
              localeFinishedCount = ProductInStoreInfoList.FindAll(f => f.StroeId == localeFinishedSign).ToList().Sum(m => m.InStroeNumber);
              freeTradeInHouseCount = ProductInStoreInfoList.FindAll(f => f.StroeId == FreeTradeSign).ToList().Sum(m => m.InStroeNumber);
              putInMaterialCount = ProductInStoreInfoList.FindAll(f => f.StroeId == IncomingmaterialSign).ToList().Sum(m => m.InStroeNumber);
          }
          else
          {
              localeFinishedCount = 0; freeTradeInHouseCount = 0;  putInMaterialCount = 0;
          }
      }
      /// <summary>
      /// 得到制三部的 订单与工单的对比参数
      /// </summary>
      /// <returns></returns>
      public  List <ProductTypeMonitorModel>GetMS589ProductTypeMonitor()
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
      public MemoryStream BuildProductTypeMonitoList()
      {
          try
          {
              var dataGroupping = GetMS589ProductTypeMonitor();
              return dataGroupping.ExportToExcel<ProductTypeMonitorModel>("订单与工单对比");
          }
          catch (Exception ex)
          {
              throw new Exception(ex.InnerException.Message);
          }
      }


      #region 依产品型号得到相应信息
      /// <summary>
      ///  获取所有生产工单, 除掉镭射雕刻,客退品
      /// </summary>
      /// <param name="containsProductName"></param>
      /// <returns></returns>
     private  List<OrderModel>GetAllProductOrderList(string containsProductName )
      {
          return OrderCrudFactory.OrderDetailsDb.GetAllOrderBy(containsProductName).
              FindAll(f => !(f.ProductSpecify.Contains("镭射雕刻") || f.OrderId.Contains("528"))); ;
      }
      /// <summary>
      /// 获取成品仓信息
      /// </summary>
      /// <param name="containsProductName"></param>
      /// <returns></returns>
      private  List<FinishedProductStoreModel> GetProductInStoreInfoBy(string  containsProductName)
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

      #endregion
    }
}
