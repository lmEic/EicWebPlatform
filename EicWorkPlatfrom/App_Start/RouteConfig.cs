using System.Web.Mvc;
using System.Web.Routing;

namespace EicWorkPlatfrom
{
    public static class AuthenCheckManager
    {
        static AuthenCheckManager()
        {
           IsCheck = true;
           //IsCheck = false;
        }

        /// <summary>
        /// 是否进行权限检测
        /// 在开发测试中，可将此属性设置为false
        /// 正式发布时，设置为true
        /// </summary>
        public static bool IsCheck { get; set; }
    }

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            if (AuthenCheckManager.IsCheck)
            {
                routes.MapRoute(
                             name: "Default",
                             url: "{controller}/{action}/{id}",
                             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                          );
            }
            else
            {
                //Product
                //EicSystemManage
                //Equipment
                //HR
                routes.MapRoute(
                             name: "Default",
                             url: "{controller}/{action}/{id}",
                             defaults: new { controller = "EicSystemManage", action = "Index", id = UrlParameter.Optional }
                         );
            }
        }
    }
}