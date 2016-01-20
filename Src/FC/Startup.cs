using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FC.Startup))]
namespace FC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
