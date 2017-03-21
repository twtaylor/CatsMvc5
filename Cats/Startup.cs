using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cats.Startup))]
namespace Cats
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
