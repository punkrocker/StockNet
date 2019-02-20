using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StockNet.Startup))]
namespace StockNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
