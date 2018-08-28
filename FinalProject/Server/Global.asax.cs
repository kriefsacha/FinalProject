using BL;
using BL.Interfaces;
using BL.Storage;
using Common.Interfaces;
using DAL.Repositories;
using Server.Interfaces;
using Server.Services;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Server
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            container.Register<IPlaneRepository, PlaneRepository>(Lifestyle.Singleton);
            container.Register<IStationRepository, StationRepository>(Lifestyle.Singleton);
            container.Register<IFlightRepository, FlightRepository>(Lifestyle.Singleton);
            container.Register<IQueueService, QueueService>(Lifestyle.Singleton);
            container.Register<IAirportManager, AirportManager>(Lifestyle.Singleton);
            container.Register<IDalService, DalService>(Lifestyle.Singleton);
            container.Register<IHubService, HubService>(Lifestyle.Singleton);
            container.Register<IManager, Manager>(Lifestyle.Singleton);


            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
