using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(School.Web.Startup))]
namespace School.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
