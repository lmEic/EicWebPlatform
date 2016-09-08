using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.DailyReport
{
    /// <summary>
    /// 日报输入管理器
    /// </summary>
    public class InputManager
    {
        /// <summary>
        /// 日报输入管理器
        /// </summary>
        public DailyReportInputManager DailyReportInputManager
        {
            get { return OBulider.BuildInstance<DailyReportInputManager>(); }
        }

    }

    /// <summary>
    /// 日报录入管理器
    /// </summary>
    public class DailyReportInputManager
    {
        /// <summary>
        /// 日报输入管理器
        /// </summary>
        private DailyReportTemplateManager DailyReportTemplateManager
        {
            get { return OBulider.BuildInstance<DailyReportTemplateManager>(); }
        }

        /// <summary>
        /// 获取日报模板
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public List<DailyReportModel> GetDailyReportTemplate(string department)
        {
          return  DailyReportTemplateManager.GetDailyReportTemplateListBy(department);
        }


    }


    /// <summary>
    /// 日报模板管理器
    /// </summary>
    internal class DailyReportTemplateManager
    {

        /// <summary>
        /// 转换为日报模板列表
        /// </summary>
        /// <param name="modelList">日报列表</param>
        /// <returns></returns>
        List<DailyReportTemplateModel> ConventTemplateList(List<DailyReportModel> modelList)
        {
            //TODO：实现日报List 转换为模板List
            try
            {
                List<DailyReportTemplateModel> returnList = new List<DailyReportTemplateModel>();
                if (modelList != null && modelList.Count > 0)
                {
                    DailyReportTemplateModel returnModel = null;
                    modelList.ForEach(e =>
                    {
                        returnModel = new DailyReportTemplateModel()
                        {
                            Department = e.Department,
                            OrderId = e.OrderId,
                            ProductName = e.ProductName,
                            ProductSpecification = e.ProductSpecification,
                            ProductFlowSign = e.ProductFlowSign,
                            ProductFlowID = e.ProductFlowID,
                            ProductFlowName = e.ProductFlowName,
                            MachineId = e.MachineId,
                            UserName = e.UserName,
                            UserWorkerId = e.UserWorkerId,
                            MasterName = e.MasterName,
                            MasterWorkerId = e.MasterWorkerId,
                            MouldId = e.MouldId,
                            MouldName = e.MouldName,
                            MouldCavityCount = e.MouldCavityCount,
                            StandardHours = e.StandardHours,
                            StandardHoursType = e.StandardHoursType,
                            ClassType = e.ClassType,
                            DifficultyCoefficient = e.DifficultyCoefficient,
                        };
                        returnList.Add(returnModel);
                    });
                }
                return returnList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        /// <summary>
        /// 转会为日报列表
        /// </summary>
        /// <param name="modelList">模板列表</param>
        /// <returns></returns>
        List<DailyReportModel> ConventDailyReportList(List<DailyReportTemplateModel> modelList)
        {
            //TODO：实现模板list 转换为日报List
            try
            {
                List<DailyReportModel> returnList = new List<DailyReportModel>();
                if (modelList != null && modelList.Count > 0)
                {
                    DailyReportModel returnModel = null;
                    modelList.ForEach(e =>
                    {
                        returnModel = new DailyReportModel()
                        {
                            Department = e.Department,
                            OrderId = e.OrderId,
                            ProductName = e.ProductName,
                            ProductSpecification = e.ProductSpecification,
                            ProductFlowSign = e.ProductFlowSign,
                            ProductFlowID = e.ProductFlowID,
                            ProductFlowName = e.ProductFlowName,
                            MachineId = e.MachineId,
                            UserName = e.UserName,
                            UserWorkerId = e.UserWorkerId,
                            MasterName = e.MasterName,
                            MasterWorkerId = e.MasterWorkerId,
                            MouldId = e.MouldId,
                            MouldName = e.MouldName,
                            MouldCavityCount = e.MouldCavityCount,
                            StandardHours = e.StandardHours,
                            StandardHoursType = e.StandardHoursType,
                            ClassType = e.ClassType,
                            DifficultyCoefficient = e.DifficultyCoefficient,
                        };
                        returnList.Add(returnModel);
                    });
                }
                return returnList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        /// <summary>
        /// 获取日报模板列表
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public List<DailyReportModel> GetDailyReportTemplateListBy(string department)
        {
            //TODO:从数据集中找到模板列表
            //将模板列表转换为日报列表 并返回
           var templateList =  DailyReportInputCrudFactory.DailyReportTemplateCrud.GetTemplateListBy(department);
           return ConventDailyReportList(templateList);
        }

        /// <summary>
        /// 存储模板列表
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult Store(List<DailyReportModel> modelList)
        {
            //TODO:先查找出该部门的模板进行删除 然后进行存储 保证数据库中始终是最新的模板
            try
            {
                OpResult retrueOpResult = OpResult.SetResult("列表为空");
                OpResult refreshTemplateResult = OpResult.SetResult("没有任何操作");
                if (modelList != null && modelList.Count() > 0)
                {
                    string department = modelList.FirstOrDefault().Department;
                    //转化日报转化为日报模板
                    var templateList = ConventTemplateList(modelList);
                    if (templateList != null && templateList.Count() > 0)
                    {
                        //删除此部门的日报模板
                       refreshTemplateResult= DailyReportInputCrudFactory.DailyReportTemplateCrud.DeleteTemplateListBy(department);
                        //添加新模板
                       refreshTemplateResult =DailyReportInputCrudFactory.DailyReportTemplateCrud.AddTemplateList(templateList);
                    }
                    retrueOpResult = DailyReportInputCrudFactory.DailyReportCrud.AddDailyReportList(modelList);
                }
                return retrueOpResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
           
           
        }
    }


}
