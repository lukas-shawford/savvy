using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(savvy.Startup))]
namespace savvy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
