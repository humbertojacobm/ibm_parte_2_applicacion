using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Parte02Aplicacion.Startup))]
namespace Parte02Aplicacion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
