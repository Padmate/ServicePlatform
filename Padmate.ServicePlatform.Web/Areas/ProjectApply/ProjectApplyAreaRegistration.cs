using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Areas.ProjectApply
{
    public class ProjectApplyAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ProjectApply";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ProjectApply_default",
                "ProjectApply/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Padmate.ServicePlatform.Web.Controllers.ProjectApply" }
            );
        }
    }
}