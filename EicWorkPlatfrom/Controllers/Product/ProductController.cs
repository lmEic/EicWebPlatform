using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers.Product
{
    public class ProductController : EicBaseController
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View();
        }
       
    }
}
