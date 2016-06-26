using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Lm.Eic.App.Business.Bmp.Hrm.Archives.Tests
{
    [TestClass()]
    public class ArchivesManagerTests
    {
        [TestMethod()]
        public void StoreTest()
        {
            string dt = "2011年12月27日--2031年12月27日";
            int pos=dt.LastIndexOf("--");
            if (pos> 0)
            {
                string et = dt.Substring(pos + 2, dt.Length - pos - 2);
                DateTime b = DateTime.Parse(et);
            }
            Assert.Fail();
        }
    }
}
