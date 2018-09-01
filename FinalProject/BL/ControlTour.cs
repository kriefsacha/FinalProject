using BL.Interfaces;
using Common;
using Common.Enums;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace BL
{
    public class ControlTour : IControlTour, IDisposable
    {
        public List<StationManager> Stations { get; private set; }
        /// <summary>
        /// List of planes that waits to departure or arrive (not queue because they can come in disorder)
        /// </summary>
        public List<Plane> waitingList { get; private set; }
        /// <summary>
        /// Timer that take out the waiting planes
        /// </summary>
        private Timer waitingTimer;

        private IQueueService queueService;
        private IStationRepository stationRepository;
        /// <summary>
        /// Tells the server that a plane moved
        /// </summary>
        public event EventHandler onPlaneMoved;
        /// <summary>
        /// Tells the server that an error happened (mostly on Database functions)
        /// </summary>
        public event EventHandler onError;

        /// <summary>
        /// Gives the next step of arrival planes from number of station to id of next step (like "S3" for "Step 3")
        /// </summary>
        private Dictionary<int, string> arrivalNextStep;
        /// <summary>
        ///  Gives the next step of departure planes from number of station to id of next step (like "S3" for "Step 3")
        /// </summary>
        private Dictionary<int, string> departureNextStep;

        public ControlTour(IQueueService queueService, IStationRepository stationRepository)
        {
            this.queueService = queueService;
            this.stationRepository = stationRepository;

            try
            {
                Initialize();
            }
            catch (Exception exc)
            {
                onError?.Invoke(exc, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Initialize the tour
        /// </summary>
        private void Initialize()
        {
            Stations = new List<StationManager>();
            waitingList = new List<Plane>();
            arrivalNextStep = new Dictionary<int, string>();
            departureNextStep = new Dictionary<int, string>();

            //Get the relations (that gives the next steps) from the database (to not be hardcoded)
            var relations = stationRepository.GetRelations();

            #region Initialize the next steps Dictionarries
            var departureRelations = relations.Where(r => r.State == FlightState.Departure);
            foreach (var relation in departureRelations)
            {
                departureNextStep.Add(relation.StationId, relation.NextStepId);
            }

            var arrivalRelations = relations.Where(r => r.State == FlightState.Arrival);
            foreach (var relation in arrivalRelations)
            {
                arrivalNextStep.Add(relation.StationId, relation.NextStepId);
            }
            #endregion

            //Add the relations to the queue service (that create dinamicly the queues we need)
            queueService.Add(relations);

            //Get the stations from the database to create them
            var dalStations = stationRepository.GetCurrentStationsState();

            foreach (var station in dalStations)
            {
                var stationManager = new StationManager(station.StepKey, station.Number, queueService);

                //When the station fires the event that she took a new plane
                stationManager.TookNewPlane += (sender, e) =>
                {
                    //Will tell the server that a plane moved
                    onPlaneMoved?.Invoke(sender, e);
                };

                //When the station fires the event that a plane finished her job (but he still stays in the station until the other station take him from the queue)
                stationManager.PlaneFinished += (sender, e) =>
                {
                    if (sender is Plane plane)
                    {
                        //Get the next step
                        var nextStep = GetNextMove(plane);

                        if (nextStep != null)
                            //Put the plane in the queue of the next step he needs to go (and the next station will take it when she is available)
                            queueService.EnQueue(nextStep, plane);
                        //if it was the final move of the plane
                        else
                        {
                            //Release the final station from the plane
                            plane.FinishWay();
                            //Tells the server the plane moved
                            onPlaneMoved?.Invoke(plane, e);
                        }
                    }
                };

                Stations.Add(stationManager);
            }

            waitingTimer = new Timer(15000);
            waitingTimer.Elapsed += WaitingTimer_Elapsed;
            waitingTimer.Start();
        }

        /// <summary>
        /// New departure or arrival came
        /// </summary>
        /// <param name="Plane">The new plane</param>
        public void NewDepartureOrArrival(Plane Plane)
        {
            waitingList.Add(Plane);
        }

        public void WaitingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Get the planes that are ready to go
            var planesReady = waitingList.Where(p => p.ActionDate <= DateTime.Now).ToList();

            foreach (var plane in planesReady)
            {
                string nextStep = GetNextMove(plane);

                queueService.EnQueue(nextStep, plane);

                waitingList.Remove(plane);
            }
        }
        
        /// <summary>
        /// Gives the next move of a plane
        /// </summary>
        /// <param name="plane">Plane that we need his next move</param>
        /// <returns></returns>
        public string GetNextMove(Plane plane)
        {
            return plane.flightState == FlightState.Departure ? departureNextStep[plane.StationNumber] : arrivalNextStep[plane.StationNumber];
        }

        /// <summary>
        /// Dispose the control tour (that dispose all his stations)
        /// </summary>
        public void Dispose()
        {
            waitingTimer.Stop();

            foreach (var station in Stations)
            {
                station.Dispose();
            }
        }
    }
}