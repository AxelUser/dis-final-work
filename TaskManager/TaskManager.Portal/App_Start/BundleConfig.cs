using System.Web;
using System.Web.Optimization;

namespace TaskManager.Portal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/CustomScripts.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular.min.js",
                      "~/Scripts/angular-route.min.js",
                      "~/Scripts/angular-sanitize.min.js",
                      "~/app/node_modules/ng-dialog/js/ngDialog.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/frontapp-init").Include(
                "~/app/app.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/frontapp-service").Include(
                "~/app/services/project-service.js",
                "~/app/services/task-service.js",
                "~/app/services/status-service.js",
                "~/app/services/role-service.js",
                "~/app/services/exception-service.js",
                "~/app/services/user-service.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/frontapp-controller").Include(
                "~/app/directives/scroll.js",
                "~/app/controllers/menu-controller.js",
                "~/app/controllers/main-controller.js",
                "~/app/controllers/tasks-controller.js",
                "~/app/controllers/task-controller.js",
                "~/app/controllers/projects-controller.js",
                "~/app/controllers/project-controller.js",
                "~/app/controllers/executor-controller.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/app/node_modules/ng-dialog/css/ngDialog.min.css",
                      "~/app/node_modules/ng-dialog/css/ngDialog-theme-default.min.css",
                      "~/app/node_modules/ng-dialog/css/ngDialog-theme-plain.min.css",
                      "~/Content/site.css"
                      ));
        }
    }
}
