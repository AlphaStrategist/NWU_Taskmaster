using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NWU_Taskmaster.Startup))]
namespace NWU_Taskmaster
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
