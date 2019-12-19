using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fintrak.Presentation.CloudClient.Startup))]
namespace Fintrak.Presentation.CloudClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
