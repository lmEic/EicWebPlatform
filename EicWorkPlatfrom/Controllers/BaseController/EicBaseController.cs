using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Framework.Authenticate.Business;
using Lm.Eic.Framework.Authenticate.Model;

namespace EicWorkPlatfrom.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public class EicBaseController : Controller
    {
        #region json date converter
        /// <summary>
        /// 返回日期经过格式化后的Json数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dateFormat"></param>
        /// <returns></returns>
        protected ContentResult DateJsonResult(object data, string dateFormat = "yyyy-MM-dd")
        {
            var dateConverter = new IsoDateTimeConverter() { DateTimeFormat = dateFormat };
            return Content(JsonConvert.SerializeObject(data, Formatting.Indented, dateConverter));
        }
        #endregion 

        #region my define
        /// <summary>
        /// 登录在线用户
        /// </summary>
        protected LoginUser OnLineUser
        {
            get
            {
                LoginUser user = new LoginUser();
                if (this.LoginIdentity != null)
                {
                    user.UserId = this.LoginIdentity.RegistUser.UserId;
                    user.UserName = this.LoginIdentity.RegistUser.UserName;
                    user.Role = this.LoginIdentity.MatchRoleList.OrderBy(o => o.RoleLevel).ToList()[0];
                }
                else
                {
                    user.UserId = "003095";
                    user.UserName = "开发调试者";
                    user.Role = new RoleModel()
                       {
                           RoleLevel = 0,
                           RoleId = "admin_001",
                           RoleName = "SuperAdmin"
                       };
                }
                return user;
            }
        }
        /// <summary>
        /// 用户的登录身份信息
        /// </summary>
        protected IdentityInfo LoginIdentity
        {
            get { return Session[EicConstKeys.UserAccount] as IdentityInfo; }
        }
        /// <summary>
        /// 获取子模块导航列表
        /// </summary>
        /// <param name="moduleText">当前模块标题</param>
        /// <param name="cacheKey">缓存键值</param>
        /// <param name="atLevel">所在层级</param>
        /// <returns></returns>
        protected List<ModuleNavigationModel> GetChildrenNavModules(string moduleText,string cacheKey,int atLevel)
        {
            List<ModuleNavigationModel> datas = null;
            if (Session[cacheKey] != null)
            {
                datas = Session[cacheKey] as List<ModuleNavigationModel>;
            }
            else
            {
                IdentityInfo info = this.LoginIdentity;
                if (info != null)
                {
                    datas = info.MatchModulePowerList.FindAll(e => e.ParentModuleName == moduleText && e.ModuleType == "module" && e.AtLevel == atLevel);
                    datas = datas.OrderByDescending(o => o.DisplayOrder).ToList();
                    if (Session[cacheKey] == null)
                        Session[cacheKey] = datas;
                }
            }
            return datas;
        }
        /// <summary>
        /// 获取该模块导航菜单
        /// </summary>
        /// <param name="moduleText"></param>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        protected List<NavMenuModel> GetMenuNavModules(string moduleText, string cacheKey)
        {
            List<NavMenuModel> navMenus = new List<NavMenuModel>();
            List<ModuleNavigationModel> mdlNavs = GetChildrenNavModules(moduleText, cacheKey, 2);
            if (mdlNavs != null && mdlNavs.Count > 0)
            {
                NavMenuModel mdl = null;
                NavMenuModel subMdl = null;
                mdlNavs.ForEach(nav => {
                    var navMdls = GetChildrenNavModules(nav.ModuleText, nav.ModuleName, 3);
                    mdl = CreateNavMenuModel(mdl, nav, navMdls);
                    navMenus.Add(mdl);
                    if (navMdls != null && navMdls.Count > 0)
                    {
                        navMdls.ForEach(subNav => {
                            var subNavMdls = GetChildrenNavModules(subNav.ModuleText, subNav.ModuleName, 4);
                            subMdl = CreateNavMenuModel(subMdl, subNav, subNavMdls);
                            navMenus.Add(subMdl);
                        });
                    }
                });
            }
            return navMenus;
        }

        private NavMenuModel CreateNavMenuModel(NavMenuModel mdl, ModuleNavigationModel nav, List<ModuleNavigationModel> navMdls)
        {
            mdl = new NavMenuModel()
            {
                Name = nav.ModuleName,
                Text = nav.ModuleText,
                AtLevel = nav.AtLevel,
                Item = nav,
                Childrens = navMdls
            };
            return mdl;
        }
        #endregion

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            ViewBag.IsLoginMode = AuthenCheckManager.IsCheck;

            AuthenCheck(filterContext);
        }
        /// <summary>
        /// 权限检测
        /// </summary>
        /// <param name="filterContext"></param>
        private void AuthenCheck(ActionExecutingContext filterContext)
        {
            if (!AuthenCheckManager.IsCheck) return;
            object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(NoAuthenCheckAttribute), false);
            if (attrs != null && attrs.Length > 0)
            {

            }
            else
            {
                if (this.LoginIdentity == null)
                {
                    filterContext.Result = new RedirectResult("/Account/Login");
                }
                else
                {

                    IdentityInfo identity = this.LoginIdentity;
                    string actionName = filterContext.ActionDescriptor.ActionName.Trim();
                    string ctrlName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.Trim();
                    if (!identity.IsHasAsccessPower(actionName, ctrlName))
                    {
                        filterContext.Result = new RedirectResult("/Account/Login");
                    }
                }
            }
        }


        #region file operate method
        /// <summary>
        /// 导出数据到Excel文件中
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="dataSource">数据源</param>
        /// <param name="xlsSheetName">Sheet表名称</param>
        /// <param name="xlsFileName">Excel文件名称,包括扩展名</param>
        /// <returns></returns>
        protected FileResult ExportToExcel<T>(List<T> dataSource, string xlsSheetName, string xlsFileName) where T : class
        {
            MemoryStream ms = dataSource.ExportToExcel<T>(xlsSheetName);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", xlsFileName + ".xls");
        }
        #endregion
    }

    public class LoginUser
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        public RoleModel Role { get; set; }

        public string Department { get; set; }
    }
    /// <summary>
    /// 图片行为结果
    /// </summary>
    public class ImageEicResult : ActionResult
    {
        private MemoryStream imgStream;
        private string contentType;

        public ImageEicResult(MemoryStream imgStream, string contentType)
        {
            this.imgStream = imgStream;
            this.contentType = contentType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentException("context");
            if (imgStream == null)
                throw new ArgumentException("imgStream is null");
            if (contentType == null)
                throw new ArgumentException("contentType is null");

            HttpResponseBase response = context.HttpContext.Response;
            response.Clear();
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.ContentType = contentType;
            imgStream.WriteTo(response.OutputStream);
        }
    }


    /// <summary>
    /// 不需要权限验证特性标注
    /// 标注有此标记的将不会对其进行权限验证
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class NoAuthenCheckAttribute : Attribute
    {
        public NoAuthenCheckAttribute()
        { }
    }
}


