using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(testingemailverification.Startup))]
namespace testingemailverification
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
