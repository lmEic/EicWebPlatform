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
    }
}
