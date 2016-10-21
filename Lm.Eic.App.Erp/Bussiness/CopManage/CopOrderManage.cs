using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.Erp.DbAccess.CopManageDb;
using Lm.Eic.App.Erp.DbAccess.MocManageDb.OrderManageDb;
using Lm.Eic.App.Erp.Domain.InvManageModel;
using Lm.Eic.App.Erp.DbAccess.InvManageDb;
using Lm.Eic.App.Erp.Domain.ProductTypeMonitorModel;
using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using System.IO;

namespace Lm.Eic.App.Erp.Bussiness.CopManage
{
  
  public   class CopOrderManage
    {

      /// <summary>
      /// 获得一个产品型号对应的订单与工单的对比参数
      /// </summary>
      /// <param name="containsProductTypeOrProductSpecify">包含产品名字和规格</param>
      /// <returns></returns>
     
      public ProductTypeMonitorModel GetProductTypeMonitorInfoBy(string containsProductTypeOrProductSpecify)
      {
              //查找一个型号的订单 并添加到业务订单中
          var getCoporderModel = CopOrderCrudFactory.CopOrderManage.GetCopOrderBy(containsProductTypeOrProductSpecify);
              //如果业务订单中没有，直接返回空
             if (getCoporderModel == null || getCoporderModel.Count <= 0) return null;
            // 依型号汇总工单信息 
             var unfinishedOrderList = GetAllProductOrderList(containsProductTypeOrProductSpecify).
                                       FindAll(e => !(e.OrderFinishStatus == "已完工" || e.OrderFinishStatus == "指定完工"));
             //依型号成品仓信息  
             var ProductInStoreInfoList = GetProductInStoreInfoBy(containsProductTypeOrProductSpecify);

             double orderCount = unfinishedOrderList.FindAll(f => !f.OrderId.Contains("523")).ToList().Sum(f => f.Count) -
                                 unfinishedOrderList.FindAll(f => !f.OrderId.Contains("523")).ToList().Sum(f => f.InStoreCount);
             double sumCount = getCoporderModel.FindAll(f => f.ProductName == containsProductTypeOrProductSpecify).ToList().Sum(m => m.ProductNumber) -
                                 getCoporderModel.FindAll(f => f.ProductName == containsProductTypeOrProductSpecify).ToList().Sum(m => m.FinishNumber);
             double localeFinishedCount = ProductInStoreInfoList.FindAll(f => f.StroeId == "D05").ToList().Sum(m => m.InStroeNumber);
             double freeTradeInHouseCount = ProductInStoreInfoList.FindAll(f => f.StroeId == "B03").ToList().Sum(m => m.InStroeNumber);
             double putInMaterialCount = ProductInStoreInfoList.FindAll(f => f.StroeId == "C03").ToList().Sum(m => m.InStroeNumber);
             double allCheckOrderCount = unfinishedOrderList.FindAll(f => f.OrderId.Contains("523")).ToList().Sum(f => f.Count)-
                                 unfinishedOrderList.FindAll(f =>f.OrderId.Contains("523")).ToList().Sum(f => f.InStoreCount); ;
            
          return  new ProductTypeMonitorModel
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
      /// 得到制三部的 订单与工单的对比参数
      /// </summary>
      /// <returns></returns>
      public  List <ProductTypeMonitorModel>GetMS589ProductTypeMonitor()
      {
          List<ProductTypeMonitorModel> typeMonitorModelList = new List<ProductTypeMonitorModel>();
          //获取MES的产品型号
          List<string> mesPortype = CopOrderCrudFactory.CopOrderManage.MesProductType();
          mesPortype.ForEach(e => {
              var m = GetProductTypeMonitorInfoBy(e);
              if (m != null)
              { typeMonitorModelList.Add(m); }
          });
          return typeMonitorModelList;
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
          var copOrderList = CopOrderCrudFactory.CopOrderManage.GetCopOrderBy(containsProductName);

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
