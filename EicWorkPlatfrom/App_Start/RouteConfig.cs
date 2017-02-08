using System.Web.Mvc;
using System.Web.Routing;

namespace EicWorkPlatfrom
{
    public static class AuthenCheckManager
    {
        static AuthenCheckManager()
        {
          // IsCheck = true ;
        }

        private static bool isCheck = false;

        /// <summary>
        /// 是否进行权限检测
        /// 在开发测试中，可将此属性设置为false
        /// 正式发布时，设置为true
        /// </summary>
        public static bool IsCheck
        {
            get { return isCheck; }
            set { isCheck = value; }
        }
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
                //Purchase
                routes.MapRoute(
                             name: "Default",
                             url: "{controller}/{action}/{id}",
                             defaults: new { controller = "Equipment", action = "Index", id = UrlParameter.Optional }
                         );
            } 
        }
    }
}