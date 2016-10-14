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
  public   class OrderManage
    {
       

      public List<ProductTypeMonitorModel> GetProductTypeMonitor()
      {
          List<ProductTypeMonitorModel> TypeMonitorModel = new List<ProductTypeMonitorModel>();
          List<CopOrderModel> getCoporderModel = new List<CopOrderModel>();
          List<string> productTypeList = new List<string>();
        
          //获取MES的产品型号
          List<string> mesPortype = CopOrderCrudFactory.CopOrderManage.MesProductType();
        
          //查找每个型号的订单 并添加到业务订单中
          mesPortype.ForEach(e => 
          {
              var m = CopOrderCrudFactory.CopOrderManage.GetCopOrderBy(e);
              getCoporderModel.AddRange(m);
          });
         //除掉重复 得到所需型号
          if (getCoporderModel.Count >0)
          {
              getCoporderModel.ForEach(e => 
              {
                  if(!productTypeList.Contains (e.ProductName))
                  { productTypeList.Add(e.ProductName); }
              } );
          }
          //对每个所需型号汇总
          productTypeList.ForEach(e =>
          {
              //依型号汇总工单信息 
              var unfinishedOrderList = UnfinishedOrderListBy(e);
            
              //依型号成品仓信息  
              var ProductInStoreInfoList = GetProductInStoreInfoBy(e);

              TypeMonitorModel.Add(new ProductTypeMonitorModel
              {
                  ProductType=e,
                  ProductSpecify = getCoporderModel.Find (f=>f.ProductName==e).ProductSpecify,
                  //工单总量-工单入库量  不包括“全检工单523”
                  OrderNumber = unfinishedOrderList.FindAll(f =>! f.OrderId.Contains("523")).ToList().Sum(f => f.Count) -
                                unfinishedOrderList.FindAll(f =>!f.OrderId.Contains("523")).ToList().Sum(f => f.InStoreCount),
                  //订单总量-订单已交量
                  SumNumber = getCoporderModel.FindAll (f => f.ProductName == e).ToList ().Sum(m=>m.ProductNumber)-
                                getCoporderModel.FindAll (f => f.ProductName == e).ToList ().Sum(m=>m.FinishNumber),
                  LocaleFinishedNumber=ProductInStoreInfoList.FindAll (f=>f.StroeId=="D05").ToList ().Sum(m=>m.InStroeNumber),
                  FreeTradeInHouseNumber  = ProductInStoreInfoList.FindAll(f => f.StroeId == "B03").ToList().Sum(m => m.InStroeNumber),
                  PutInMaterialNumber  = ProductInStoreInfoList.FindAll(f => f.StroeId == "C03").ToList().Sum(m => m.InStroeNumber),
                  //另外统计 “全检工单523”
                  AllCheckOrderNumber = unfinishedOrderList.FindAll(f => f.OrderId.Contains("523")).ToList().Sum(f => f.Count)
              });
          });

          return TypeMonitorModel;
      }

      /// <summary>
      /// 生成EXCEL表格
      /// </summary>
      /// <returns></returns>
      public MemoryStream ProductTypeMonitoList()
      {
          try
          {
              var dataGroupping = GetProductTypeMonitor();
              return dataGroupping.ExportToExcel<ProductTypeMonitorModel>("121212");
          }
          catch (Exception ex)
          {
              throw new Exception(ex.InnerException.Message);
          }
          //return NPOIHelper.ExportToExcel(_waitingMaintenanceList, "待保养设备列表");
      }
      #region 依产品型号得到相应信息
      /// <summary>
      /// 获取未完工工单, 除掉镭射雕刻,客退品
      /// </summary>
      /// <param name="containsProductName"></param>
      /// <returns></returns>
      private List<OrderModel> UnfinishedOrderListBy(string containsProductName)
      {
          //依型号汇总工单信息  除掉镭射雕刻,客退品
         return  OrderCrudFactory.OrderDetailsDb.GetUnfinishedOrderBy(containsProductName)
               .FindAll(f => !(f.ProductSpecify.Contains("镭射雕刻") || f.OrderId.Contains("528")));
      }
      /// <summary>
      ///  获取所有生产工单, 除掉镭射雕刻,客退品
      /// </summary>
      /// <param name="containsProductName"></param>
      /// <returns></returns>
     private  List<OrderModel>GetAllProductOrderList(string containsProductName )
      {
          return OrderCrudFactory.OrderDetailsDb.GetAllOrderBy(containsProductName)
                 .FindAll(f => !(f.ProductSpecify.Contains("镭射雕刻") || f.OrderId.Contains("528"))); ;
      }
      /// <summary>
      /// 获取成品仓信息
      /// </summary>
      /// <param name="containsProductName"></param>
      /// <returns></returns>
      private  List<FinishedProductStoreModel> GetProductInStoreInfoBy(string  containsProductName)
      {
          List<FinishedProductStoreModel> ProductInStoreInfoList = new List<FinishedProductStoreModel>();
          //从销售订单中得到型号对应的料号
          List<string> productIDList = GetAllPorductIdBy(containsProductName);
          //对每个料号得到相应的成品仓信息
         productIDList.ForEach(e =>
              {
                  var mmm = InvOrderCrudFactory.InvManageDb.GetProductStroeInfoBy(e);
                  ProductInStoreInfoList.AddRange(mmm);
              });
          return ProductInStoreInfoList;

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
