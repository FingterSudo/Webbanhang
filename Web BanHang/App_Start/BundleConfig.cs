﻿using System.Web;
using System.Web.Optimization;

namespace Web_BanHang
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/PagedList.css"));

            bundles.Add(new StyleBundle("~/Views/TemplateAdmin/css").Include(
                "~/Views/TemplateAdmin/css/sb-admin-2.css",
                "~/Views/TemplateAdmin/css/sb-admin-2.min.css"
                ));
            bundles.Add(new StyleBundle("~/Views/TemplateAdmin/css/vendor/fontawesome-free/css").Include(
                "~/Views/TemplateAdmin/css/vendor/fontawesome-free/css/all.min.css"
                ));
        }
    }
}
