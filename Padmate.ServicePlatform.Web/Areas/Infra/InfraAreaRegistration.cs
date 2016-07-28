using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Areas.Infra
{
    public class InfraAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Infra";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Infra_default",
                "Infra/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Padmate.ServicePlatform.Web.Controllers.Infra" }
            );
        }
    }
}