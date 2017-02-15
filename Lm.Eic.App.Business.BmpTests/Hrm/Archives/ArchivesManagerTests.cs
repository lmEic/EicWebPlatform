using Microsoft.VisualStudio.TestTools.UnitTesting;
 using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using System;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;
namespace Lm.Eic.App.Business.Bmp.Hrm.Archives.Tests
{
    [TestClass()]
    public class ArchivesManagerTests
    {
        [TestMethod()]
        public void StoreTest()
        {
            string dt = "2011年12月27日--2031年12月27日";
            int pos = dt.LastIndexOf("--");
            if (pos > 0)
            {
                string et = dt.Substring(pos + 2, dt.Length - pos - 2);
                DateTime b = DateTime.Parse(et);
            }
            Assert.Fail();
        }
          [TestMethod()]
        public void testLeaveOffManager()
        {
            var model = new ArLeaveOfficeModel
            {
                ID="42092319811109247X",
                WorkerId="001359",
                WorkerName ="万晓桥",
                Department="Eic",
                LeaveDate=DateTime.Now,
                LeaveReason="漫漫流量监测",
                OpPerson ="万晓桥",
                Post ="操作工",
                Memo ="测试",
                OpSign ="add"

            };
         var Result=   ArchiveService.ArchivesManager.LeaveOffManager.StoreLeaveOffInfo(model);
        }

        public void testWorkerIdChange()
        {

            var model = new WorkerChangedModel {
                OldWorkerId = "881359",
                NewWorkerId ="001359",
                WorkerName = "万晓桥",
                OpSign = "add",
                OpPerson ="万晓桥"
                
              
                
            };
            var resulst = ArchiveService.ArchivesManager.WorkerIdChangeManager.StoreWorkerIdChangeInfo(model);
        }


        public void test()
        {
            var mm = ArchiveService.ArCalendarManger.GetDateDictionary(2017, 2);
            if (mm==null || mm.Count < 0)
            {
                Assert.Fail();
            }
          
        }
    }
    }
