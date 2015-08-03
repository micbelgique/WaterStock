using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SummerCamp.WaterStock.Web.Startup))]
namespace SummerCamp.WaterStock.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
