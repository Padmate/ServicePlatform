using Padmate.ServicePlatform.Web.Arrtibutes;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new ApplicationHandleErrorAttribute());
        }
    }
}
