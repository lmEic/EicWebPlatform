using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.App.Erp.Bussiness.MocManage;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.DailyReport
{

    /// <summary>
    /// 日报配置管理器
    /// </summary>
    public class ConfigManager
    {
        /// <summary>
        /// 产品工艺设置
        /// </summary>
        public ProductFlowManager ProductFlowSetter
        {
            get { return OBulider.BuildInstance<ProductFlowManager>(); }
        }
        /// <summary>
        /// 机台设置
        /// </summary>
        public MachineManager MachineSetter
        {
            get { return OBulider.BuildInstance<MachineManager>(); }
        }

        /// <summary>
        /// 非生产原因设置
        /// </summary>
        public NonProductionReasonManager NonProductionReasonSetter
        {
            get { return OBulider.BuildInstance<NonProductionReasonManager>(); }
        }
       
    }

    /// <summary>
    /// 产品工艺管理器
    /// </summary>
    public class ProductFlowManager
    {
        #region Find

        /// <summary>
        /// 获取产品工艺总览列表 前30行
        /// </summary>
        /// <param name="departemant"> 部门</param>
        /// <returns></returns>
        public List<ProductFlowOverviewModel> GetProductFlowOverviewListBy(string departemant)
        {
            return DailyReportCrudFactory.ProductFlowCrud.GetProductFlowOverviewListBy(departemant);
        }
        public List<ProductFlowOverviewModel> GetProductFlowOverviewListBy(string departemant ,string ProductName)
        {
            return DailyReportCrudFactory.ProductFlowCrud.GetProductFlowOverviewListBy(departemant, ProductName);
        }
        /// <summary>
        /// 获取产品工艺总览
        /// </summary>
        /// <param name="dto">数据传输对象 品名和部门是必须的</param>
        /// <returns></returns>
        public  ProductFlowOverviewModel GetProductFlowOverviewBy(QueryDailyReportDto dto)
        {
            return DailyReportCrudFactory.ProductFlowCrud.GetProductFlowOverviewBy(dto);
        }

        /// <summary>
        /// 查询 1.依据部门查询  2.依据产品品名查询 3.依据录入日期查询 4.依据产品品名 工艺名称查询 
        /// 5.依据工单单号查询
        /// </summary>
        /// <param name="qryDto">数据传输对象 部门是必须的 </param>
        /// <returns></returns>
        public List<ProductFlowModel> GetProductFlowListBy(QueryDailyReportDto dto)
        {
            return DailyReportCrudFactory.ProductFlowCrud.FindBy(dto);
        }

    
         #endregion


        #region FileOption

        /// <summary>
        /// 获取工序Excel模板
        /// </summary>
        /// <param name="documentPath"></param>
        /// <returns></returns>
        public System.IO.MemoryStream GetProductFlowTemplate(string documentPath)
        {
            return FileOperationExtension.GetMemoryStream(documentPath);
        }

        /// <summary>
        /// 导入工序列表
        /// </summary>
        /// <param name="documentPatch">Excel文档路径</param>
        /// <returns></returns>
        public List<ProductFlowModel> ImportProductFlowListBy(string documentPatch)
        {
            StringBuilder errorStr = new StringBuilder();
            var listEntity = ExcelHelper.ExcelToEntityList<ProductFlowModel>(documentPatch,out errorStr);
            string errorStoreFilePath = @"C:\ExcelToEntity\ErrorStr.txt";
            if (errorStr.ToString() != string.Empty)
            {
                errorStoreFilePath.CreateFile(errorStr.ToString());
            }
            return listEntity;
        }

        #endregion


        #region Store

        /// <summary>
        /// 仓储
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(List<ProductFlowModel> modelList)
        {
            //先依据部门和品名进行数据库清除 然后批量添加进数据库
            if (modelList.Count > 0)
            {
                DailyReportCrudFactory.ProductFlowCrud.DeleteProductFlowModelBy(modelList[0].Department, modelList[0].ProductName);
                return DailyReportCrudFactory.ProductFlowCrud.AddProductFlowModelList(modelList);
            }
            else
            {
                return OpResult.SetResult("列表不能为空！");
            }
        }
        #endregion

    }

    /// <summary>
    /// 机台配置管理器
    /// </summary>
    public class MachineManager
    {
        /// <summary>
        /// 获取机台列表
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public List<MachineModel> GetMachineListBy(string department)
        {
            return DailyReportCrudFactory.MachineCrud.GetMachineListBy(department);
        }

        /// <summary>
        /// 添加一条机台记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult AddMachineRecord(MachineModel model)
        {
            return DailyReportCrudFactory.MachineCrud.AddMachineRecord(model);
        }
      
    }

    /// <summary>
    /// 非生产原因管理器
    /// </summary>
    public class NonProductionReasonManager
    {
        /// <summary>
        /// 获取非生产原因列表
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public List<NonProductionReasonModel> GetNonProductionReasonListBy(string department)
        {
            return DailyReportCrudFactory.NonProductionReasonCrud.GetNonProductionListBy(department);
        }

        /// <summary>
        /// 添加一条非生产原因
        /// </summary>
        /// <param name="model">非生产原因模型</param>
        /// <returns></returns>
        public OpResult AddNonProductionRecord(NonProductionReasonModel model)
        {
            return DailyReportCrudFactory.NonProductionReasonCrud.AddNonProductionRecord(model);
        }

    }
    /// <summary>
    ///日报出勤管理
    /// </summary>

    public class ReportAttendenceManager
    {

     

        public OpResult SaveReportAttendenceEntity(ReportsAttendenceModel entity)
        {
         
            if (DailyReportCrudFactory.ReportsAttendenceCrud.IsExist(entity))
            {
                entity.OpSign = "edit";
            }
            else entity.OpSign = "add";
            return DailyReportCrudFactory.ReportsAttendenceCrud.Store(entity);
        }
        /// <summary>
        /// 得到当天的出勤数据
        /// </summary>
        /// <param name="department"></param>
        /// <param name="dailyReportDate"></param>
        /// <returns></returns>
        public List<ReportsAttendenceModel> GetReportsAttendence(string department, DateTime dailyReportDate)
        {
            DateTime dailyDate = dailyReportDate.ToDate();
            return  DailyReportCrudFactory.ReportsAttendenceCrud.GetReportsAttendence(department, dailyDate);
        }
    }

}
