using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Padmate.ServicePlatform.Web.Startup))]
namespace Padmate.ServicePlatform.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
