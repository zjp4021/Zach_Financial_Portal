using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Zach_Financial_Portal.Startup))]
namespace Zach_Financial_Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
