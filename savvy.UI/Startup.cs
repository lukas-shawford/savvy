using Microsoft.Owin;
using Owin;
using savvy.UI;

[assembly: OwinStartup(typeof(Startup))]
namespace savvy.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
