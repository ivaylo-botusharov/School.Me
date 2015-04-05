using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(School.Web.App_Start.Startup))]

namespace School.Web.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
