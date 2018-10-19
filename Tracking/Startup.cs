using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Tracking.Configuration;

[assembly: OwinStartup(typeof(Tracking.Startup))]

namespace Tracking
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = Config.Type,
                LoginPath = new PathString(Config.Path)
            });
        }
    }
}
