using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SiteVol.Startup))]
namespace SiteVol
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
