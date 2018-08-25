using BL;
using BL.Storage;
using Common.Interfaces;
using DAL.Repositories;
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
            container.Register<IPlaneRepository, PlaneRepository>(Lifestyle.Scoped);
            container.Register<IStationRepository, StationRepository>(Lifestyle.Scoped);
            container.Register<IFlightRepository, FlightRepository>(Lifestyle.Scoped);
            container.Register<IQueueService, QueueService>(Lifestyle.Scoped);
            container.Register<IAirportManager, AirportManager>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
