using System.Web;
using System.Web.Optimization;

namespace PlainElastic.Net.WebAppMVC
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // готово к выпуску, используйте средство сборки по адресу https://modernizr.com, чтобы выбрать только необходимые тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/scrpostlogin").Include(
                        "~/Content/new/js/bootstrap.min.js",
                        "~/Content/new/js/chartist.min.js",
                        "~/Content/new/js/jquery-1.11.2.min.js",
                        "~/Content/new/js/main.js"                        
                        ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      //"~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap1.min.js",
                      "~/Content/dropzone.css",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/sitecss").Include(
                    "~/Content/bootstrap.min.css",
                      "~/Content/dropzone.css",
                    "~/Content/site.css"
                ));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                    "~/Content/bootstrap.min.css",
                      "~/Content/dropzone.css"
                ));
            bundles.Add(new StyleBundle("~/Content/postlogin").Include(
                    "~/Content/bootstrappost.min.css",
                      "~/Content/dropzone.css",
                    "~/Content/postlogin.css"
                ));

            bundles.Add(new StyleBundle("~/Content/newPostlogin").Include(
                    "~/Content/new/css/bootstrap.min.css",
                    "~/Content/new/css/bootstrap-theme.min.css",
                    "~/Content/new/css/bootstrap-theme.css",
                    "~/Content/new/css/bank-web.css",
                    "~/Content/new/css/card1.css",
                      "~/Content/dropzone.css",
                    "~/Content/new/css/card.css",
                    "~/Content/new/css/chartist.min.css",
                    "~/Content/new/css/flags.css",
                    "~/Content/new/css/font-awesome.min.css"
                ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/signin.css",
                      "~/Content/dropzone.css",
                      "~/Content/form-validation.css"));
        }
    }
}
