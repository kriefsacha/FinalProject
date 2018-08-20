using BL;
using DAL;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using Server.Services;

[assembly: OwinStartup(typeof(Server.Startup))]

namespace Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.DependencyResolver.Register(
                typeof(AirportHub), () => new AirportHub(new Logic(new Manager())));
            app.MapSignalR();
        }
    }
}