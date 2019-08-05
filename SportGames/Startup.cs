using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SportGames.Startup))]
namespace SportGames
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
