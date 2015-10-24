using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StreamQ.Startup))]
namespace StreamQ
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
