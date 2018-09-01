using Common;
using Common.Interfaces;
using Server.Interfaces;
using System;
using System.Collections.Generic;

namespace Server
{
    public class ServerManager : IServerManager
    {
        IControlTour controlTour;
        IHubService hubService;
        IDalService dalService;

        public ServerManager(IControlTour controlTour, IHubService hubService, IDalService dalService)
        {
            this.controlTour = controlTour;
            this.dalService = dalService;
            this.hubService = hubService;

            controlTour.onPlaneMoved += Airport_planeMoved;
            controlTour.onError += Airport_onError;
        }

        /// <summary>
        /// When the control tour is on error
        /// </summary>
        /// <param name="sender">The exception</param>
        /// <param name="e"></param>
        private void Airport_onError(object sender, EventArgs e)
        {
            hubService.OnError(sender as Exception);
        }

        /// <summary>
        /// When a plane moved
        /// </summary>
        /// <param name="sender">The plane that moved</param>
        /// <param name="e"></param>
        private void Airport_planeMoved(object sender, EventArgs e)
        {
            if (sender is Plane plane)
            {
                hubService.Moved(plane);
                dalService.Moved(plane);
            }
        }

        /// <summary>
        /// When a new departure/arrival is planed
        /// </summary>
        /// <param name="plane">The new plane</param>
        public void DepartureOrArrival(Plane plane)
        {
            controlTour.NewDepartureOrArrival(plane);
            hubService.DepartureOrArrival(plane);
            dalService.DepartureOrArrival(plane);
        }

        public List<Plane> GetFutureDeparturesAndArrivals()
        {
            return dalService.GetFutureDeparturesAndArrivals();
        }

        public List<Station> GetCurrentStationsState()
        {
            return dalService.GetCurrentStationsState();
        }
    }
}