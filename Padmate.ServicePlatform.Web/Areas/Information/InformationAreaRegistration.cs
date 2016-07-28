using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Areas.Information
{
    public class InformationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Information";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Information_default",
                "Information/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Padmate.ServicePlatform.Web.Controllers.Information" }
            );
        }
    }
}