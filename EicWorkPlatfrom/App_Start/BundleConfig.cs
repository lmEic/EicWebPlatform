using System.Web.Optimization;

namespace EicWorkPlatfrom
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/jquery-2.1.4.js",
                "~/Content/bootstrap/dist/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularJs").Include(
                         "~/Content/print/print.min.js",
                         "~/Content/underscore/underscore-min.js",
                         //"~/Content/angular-pageloadingeffect/snap.svg-min.js",
                         "~/Scripts/angular.min.js",
                         "~/Scripts/angular-animate.min.js",
                         "~/Scripts/angular-messages.min.js",
                         "~/Scripts/angular-sanitize.min.js",
                         "~/Scripts/angular-ui-router.min.js"));
            //"~/Content/angular-pageloadingeffect/me-pageloading.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/anstrap").Include(
                "~/Content/angular-busy/dist/angular-busy.min.js",
                "~/Content/angular-strap/dist/angular-strap.min.js",
                "~/Content/angular-strap/dist/angular-strap.tpl.min.js"));

            bundles.Add(new ScriptBundle("~/common/leehelper").Include(
               "~/Scripts/common/angulee.js"));

            bundles.Add(new StyleBundle("~/css/anstrap").Include(
                         "~/Content/print/print.min.css",
                        "~/Content/angular-motion/dist/angular-motion.css",
                        "~/Content/bootstrap-additions/dist/bootstrap-additions.css",
                        "~/Content/angular-busy/dist/angular-busy.css"));
            //"~/Content/angular-pageloadingeffect/me-pageloading.min.css"));
        }
    }
}