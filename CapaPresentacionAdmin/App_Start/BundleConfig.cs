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

            // Font Awesome JS (optional) - include folder if you use the JS approach
            bundles.Add(new ScriptBundle("~/bundles/fontawesome").IncludeDirectory(
                        "~/Scripts/fontawesome", "*.js", searchSubdirectories: true));

            // Modernizr
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // Bootstrap (use bundle that includes Popper: bootstrap.bundle)
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.min.js"));

            // Font Awesome CSS (uses files present under Content)
            bundles.Add(new StyleBundle("~/Content/fontawesome").Include(
                      "~/Content/fontawesome.min.css",
                      "~/Content/all.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
