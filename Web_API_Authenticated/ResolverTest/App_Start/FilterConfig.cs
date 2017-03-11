using System.Web;
using System.Web.Mvc;

namespace ResolverTest
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ActionFilters.LoggingFilterAttribute());
        }
    }
}
