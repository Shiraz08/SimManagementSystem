using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimManagementSystem.Startup))]
namespace SimManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
