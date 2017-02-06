using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(testaccount.Startup))]
namespace testaccount
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
