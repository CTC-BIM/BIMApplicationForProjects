using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BIMApplicationForProjects.Startup))]
namespace BIMApplicationForProjects
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
