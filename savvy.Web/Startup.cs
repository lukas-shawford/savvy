using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(savvy.Web.Startup))]
namespace savvy.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
