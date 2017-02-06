using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(identityTemplate.Startup))]
namespace identityTemplate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
