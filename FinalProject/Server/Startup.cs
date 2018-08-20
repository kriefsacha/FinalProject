using BL;
using Common.Interfaces;
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
            GlobalHost.DependencyResolver.Register(typeof(IDAL), () => 
            new Manager()); //3

            GlobalHost.DependencyResolver.Register(typeof(ILogic), () => 
            new Logic((IDAL)GlobalHost.DependencyResolver.GetService(typeof(IDAL)))); //2

            GlobalHost.DependencyResolver.Register(
                typeof(AirportHub), () => new AirportHub((ILogic)GlobalHost.DependencyResolver.GetService(typeof(ILogic)))); //1

            app.MapSignalR();
        }
    }
}