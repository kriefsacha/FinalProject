using BL;
using Common.Interfaces;
using DAL.Repositories;
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
            GlobalHost.DependencyResolver.Register(typeof(IFlightRepository), () =>
            new FlightRepository());

            GlobalHost.DependencyResolver.Register(typeof(IPlaneRepository), () =>
            new PlaneRepository());

            GlobalHost.DependencyResolver.Register(typeof(IStationRepository), () =>
            new StationRepository());

            GlobalHost.DependencyResolver.Register(typeof(ILogic), () =>
            new Logic(
                (IFlightRepository)GlobalHost.DependencyResolver.GetService(typeof(IFlightRepository)),
                (IPlaneRepository)GlobalHost.DependencyResolver.GetService(typeof(IPlaneRepository)),
                (IStationRepository)GlobalHost.DependencyResolver.GetService(typeof(IStationRepository))
            ));

            GlobalHost.DependencyResolver.Register(
                typeof(AirportHub), () => new AirportHub((ILogic)GlobalHost.DependencyResolver.GetService(typeof(ILogic))));

            app.MapSignalR();
        }
    }
}