using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cafeteria.Startup))]
namespace Cafeteria
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
