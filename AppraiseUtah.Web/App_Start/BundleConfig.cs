using System.Web.Optimization;

namespace AppraiseUtah.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            const string jQueryCdn = "//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js";

            //JavaScript Includes
            bundles.Add(new ScriptBundle("~/js/jquery", jQueryCdn).Include("~/Scripts/Lib/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/js/modernizr").Include("~/Scripts/modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/js/lib").Include(
                "~/Scripts/Lib/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/js/validation").Include(
                "~/Scripts/Lib/jquery.validate.min.js",
                "~/Scripts/Lib/jquery.validate.unobtrusive.min.js",
                "~/Scripts/Lib/jquery.maskedinput.min.js"));

            bundles.Add(new ScriptBundle("~/js/main").Include(
                "~/Scripts/Common/Appraisers.js"));

            bundles.Add(new ScriptBundle("~/js/order").Include("~/Scripts/Common/OrderAppraisal.js"));

            bundles.Add(new ScriptBundle("~/js/rates").Include("~/Scripts/Common/Rates.js"));

            //CSS Includes
            bundles.Add(new StyleBundle("~/css/main").Include(
                "~/Content/bootstrap/bootstrap.css",
                "~/Content/css/Site.css"));

            bundles.Add(new StyleBundle("~/css/social").Include("~/Content/bootstrap/font-awesome.css"));

            bundles.Add(new StyleBundle("~/css/order").Include("~/Content/css/OrderAppraisal.css"));


            bundles.Add(new StyleBundle("~/css/home").Include("~/Content/css/Index.css"));


        
        }

    }
}