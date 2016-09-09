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
            return DailyReportTemplateManager.GetDailyReportTemplateListBy(department);
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
                    modelList.ForEach(m =>
                    {
                       var returnModel = new DailyReportTemplateModel()
                        {
                            Department = m.Department,
                            OrderId = m.OrderId,
                            ProductName = m.ProductName,
                            ProductSpecification = m.ProductSpecification,
                            ProductFlowSign = m.ProductFlowSign,
                            ProductFlowID = m.ProductFlowID,
                            ProductFlowName = m.ProductFlowName,
                            MachineId = m.MachineId,
                            UserName = m.UserName,
                            UserWorkerId = m.UserWorkerId,
                            MasterName = m.MasterName,
                            MasterWorkerId = m.MasterWorkerId,
                            MouldId = m.MouldId,
                            MouldName = m.MouldName,
                            MouldCavityCount = m.MouldCavityCount,
                            StandardHours = m.StandardHours,
                            StandardHoursType = m.StandardHoursType,
                            ClassType = m.ClassType,
                            DifficultyCoefficient = m.DifficultyCoefficient,
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
                    modelList.ForEach(m =>
                    {
                       var returnModel = new DailyReportModel()
                        {
                            Department = m.Department,
                            OrderId = m.OrderId,
                            ProductName = m.ProductName,
                            ProductSpecification = m.ProductSpecification,
                            ProductFlowSign = m.ProductFlowSign,
                            ProductFlowID = m.ProductFlowID,
                            ProductFlowName = m.ProductFlowName,
                            MachineId = m.MachineId,
                            UserName = m.UserName,
                            UserWorkerId = m.UserWorkerId,
                            MasterName = m.MasterName,
                            MasterWorkerId = m.MasterWorkerId,
                            MouldId = m.MouldId,
                            MouldName = m.MouldName,
                            MouldCavityCount = m.MouldCavityCount,
                            StandardHours = m.StandardHours,
                            StandardHoursType = m.StandardHoursType,
                            ClassType = m.ClassType,
                            DifficultyCoefficient = m.DifficultyCoefficient,
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
            //TODO:从数据集中找到模板列表，将模板列表转换为日报列表 并返回
            var templateList = DailyReportInputCrudFactory.DailyReportTemplateCrud.GetTemplateListBy(department);
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
                //列表是否为空
                if (modelList == null || modelList.Count < 1)
                    return OpResult.SetResult("列表不能为空");

                //清除原始模板列表
                var deleteOpResult = DailyReportInputCrudFactory.DailyReportTemplateCrud.DeleteTemplateListBy(modelList[0].Department);
                if (!deleteOpResult.Result)
                    return OpResult.SetResult("清除原始模板失败！");

                //日报列表转化为模板列表
                var templateList = ConventTemplateList(modelList);
                //转换的列表是否成功
                if (templateList==null ||templateList.Count <1)  return OpResult.SetResult("日报数据转化模板失败！");
                //添加新的模板到数据库
                return  DailyReportInputCrudFactory.DailyReportTemplateCrud.AddTemplateList(templateList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
    }


}
