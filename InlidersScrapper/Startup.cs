using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InlidersScrapper.Startup))]
namespace InlidersScrapper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
