using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PrometheusWeb.MVC.Startup))]
namespace PrometheusWeb.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app); 
        }
    }
}
