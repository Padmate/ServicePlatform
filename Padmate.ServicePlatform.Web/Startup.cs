using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Padmate.ServicePlatform.Web.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
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
