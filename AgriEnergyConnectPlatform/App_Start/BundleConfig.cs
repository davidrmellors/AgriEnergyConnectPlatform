using System.Web;
using System.Web.Optimization;

namespace AgriEnergyConnectPlatform
{
    /// <summary>
    /// Handles the configuration of bundling and minification for the application.
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Registers the JavaScript and CSS bundles for the application.
        /// </summary>
        /// <param name="bundles">The collection of bundles for the application.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Add jQuery library bundle
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Add jQuery validation library bundle
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Add Modernizr library bundle
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // Add Bootstrap library bundle
            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            // Add CSS styles bundle
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css"));
        }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//