using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineAcademy.Startup))]
namespace OnlineAcademy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CTConfigureAuth(app);
            GTConfigureAuth(app);
            PTConfigureAuth(app);
            STConfigureAuth(app);
        }
    }
}
