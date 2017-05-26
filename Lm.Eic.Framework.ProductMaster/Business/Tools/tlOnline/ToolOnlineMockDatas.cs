using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Framework.ProductMaster.Model.Tools;

namespace Lm.Eic.Framework.ProductMaster.Business.Tools.tlOnline
{
    public class ToolOnlineMockDatas
    {
        public static List<CollaborateContactLibModel> CollaborateContactDataSet
        {
            get
            {
                List<CollaborateContactLibModel> mockDataSet = new List<CollaborateContactLibModel>();
                mockDataSet.Add(new CollaborateContactLibModel() { Department = "EIC", ContactPerson = "小明", Sex = "男", CustomerCategory = "试试", ContactCompany = "光圣", WorkerPosition = "经理", ContactMemo = "TT", Telephone = "1111254212", OfficeTelephone = "152432", Fax = "的速度的速度", Mail = "123@123.com", QqOrSkype = "111", ContactAdress = "1111", WebsiteAdress = "wwesd", OpPerson = "yagnl", OpSign = "add" });
                mockDataSet.Add(new CollaborateContactLibModel() { Department = "EIC", ContactPerson = "小明", Sex = "男", CustomerCategory = "试试", ContactCompany = "光圣", WorkerPosition = "经理", ContactMemo = "TT", Telephone = "1111254212", OfficeTelephone = "152432", Fax = "的速度的速度", Mail = "123@123.com", QqOrSkype = "111", ContactAdress = "1111", WebsiteAdress = "wwesd", OpPerson = "yagnl", OpSign = "add" });
                mockDataSet.Add(new CollaborateContactLibModel() { Department = "EIC", ContactPerson = "小明", Sex = "男", CustomerCategory = "试试", ContactCompany = "光圣", WorkerPosition = "经理", ContactMemo = "TT", Telephone = "1111254212", OfficeTelephone = "152432", Fax = "的速度的速度", Mail = "123@123.com", QqOrSkype = "111", ContactAdress = "1111", WebsiteAdress = "wwesd", OpPerson = "yagnl", OpSign = "add" });
                return mockDataSet;
            }
        }
        public static List<WorkTaskManageModel> WorkTaskManageDataSet
        {
            get
            {
                List<WorkTaskManageModel> mockDataSet = new List<WorkTaskManageModel>();
                mockDataSet.Add(new WorkTaskManageModel() {Department="EIC", SystemName = "质量管理", ModuleName = "抽样管理", WorkItem = "IQC检验管理模块", WorkDescription = "IQC检验项目配置", DifficultyCoefficient = 3, WorkPriority = 5, ProgressStatus = "己完成", ProgressDescription = "按时完成", OrderPerson = "张三", CheckPerson = "李四", OpPerson = "XXX", OpSign = "add",StartDate="2017-05-26",EndDate="2017-05-27"});
                mockDataSet.Add(new WorkTaskManageModel() {Department="EIC", SystemName = "质量管理", ModuleName = "抽样管理", WorkItem = "FQC检验管理模块", WorkDescription = "IQC检验项目配置", DifficultyCoefficient = 3, WorkPriority = 5, ProgressStatus = "己完成", ProgressDescription = "按时完成", OrderPerson = "张三", CheckPerson = "李四", OpPerson = "XXX", OpSign = "add" ,StartDate = "2017-05-26", EndDate = "2017-05-27" });
                mockDataSet.Add(new WorkTaskManageModel() {Department="EIC", SystemName = "质量管理", ModuleName = "抽样管理", WorkItem = "IQC检验管理模块", WorkDescription = "IQC检验项目配置", DifficultyCoefficient = 3, WorkPriority = 5, ProgressStatus = "己完成", ProgressDescription = "按时完成", OrderPerson = "张三", CheckPerson = "李四", OpPerson = "XXX", OpSign = "add", StartDate = "2017-05-26", EndDate = "2017-05-27" });
                return mockDataSet;

            }
        }

        
    }
}
