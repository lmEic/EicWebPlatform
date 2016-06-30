using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EicWorkPlatfrom.Controllers
{
    public class EquipmentController : EicBaseController
    {
        //
        // GET: /Equipment/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AstArchiveInput()
        {
            return View();
        }


        public FileResult ExportToExcel()
        {
            List<DDD> datas = new List<DDD>() { 
               new DDD(){ Name="aaa", Age =20},
               new DDD(){ Name="bbb", Age =25},
            };

            return this.ExportToExcel<DDD>(datas, "aaa", "AAA");
        }
    }

    public class DDD
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
