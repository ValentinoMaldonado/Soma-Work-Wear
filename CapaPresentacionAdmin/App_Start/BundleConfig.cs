using System.Web;
using System.Web.Optimization;

namespace CapaPresentacionAdmin
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // keep jquery validation only here
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Vendor scripts are already minified. Use Bundle instead of ScriptBundle
            // so the legacy ASP.NET minifier does not try to parse modern JS syntax.
            bundles.Add(new Bundle("~/bundles/fontawesome").Include(
                        "~/Scripts/fontawesome/all.min.js"));

            // Modernizr
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // Bootstrap (use bundle that includes Popper: bootstrap.bundle)
            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.min.js"));

            // DataTables scripts
            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                "~/Scripts/DataTables/jquery.dataTables.min.js",
                "~/Scripts/DataTables/dataTables.bootstrap4.min.js",
                "~/Scripts/DataTables/dataTables.buttons.min.js",
                "~/Scripts/DataTables/buttons.bootstrap4.min.js"
            ));

            // DataTables responsive (separate bundle as view requests it)
            bundles.Add(new ScriptBundle("~/bundles/datatables.responsive").Include(
                "~/Scripts/DataTables/dataTables.responsive.min.js",
                "~/Scripts/DataTables/responsive.bootstrap4.min.js"
            ));

            // Font Awesome CSS (uses files present under Content)
            bundles.Add(new StyleBundle("~/Content/fontawesome").Include(
                      "~/Content/fontawesome.min.css",
                      "~/Content/all.css"));

            // DataTables CSS
            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                "~/Content/DataTables/css/dataTables.bootstrap4.min.css",
                "~/Content/DataTables/css/responsive.bootstrap4.min.css",
                "~/Content/DataTables/css/buttons.bootstrap4.min.css"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
