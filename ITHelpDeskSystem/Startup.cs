using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ITHelpDeskSystem.Startup))]
namespace ITHelpDeskSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
