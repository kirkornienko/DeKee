using System.Web;
using System.Web.Mvc;

namespace PlainElastic.Net.WebAppMVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
