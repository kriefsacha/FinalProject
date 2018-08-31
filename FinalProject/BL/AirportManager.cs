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
    public class AirportManager : IAirportManager, IDisposable
    {
        public List<StationManager> Stations { get; private set; }
        public List<Plane> waitingList { get; private set; }
        private Timer waitingTimer;
        private IQueueService queueService;
        private IStationRepository stationRepository;
        public event EventHandler onPlaneMoved;
        public event EventHandler onError;
        private Dictionary<int, string> arrivalSteps;
        private Dictionary<int, string> departureSteps;

        public AirportManager(IQueueService queueService, IStationRepository stationRepository)
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

        private void Initialize()
        {
            Stations = new List<StationManager>();
            waitingList = new List<Plane>();
            arrivalSteps = new Dictionary<int, string>();
            departureSteps = new Dictionary<int, string>();

            var relations = stationRepository.GetRelations();

            var departureRelations = relations.Where(r => r.State == FlightState.Departure);
            foreach (var relation in departureRelations)
            {
                departureSteps.Add(relation.StationId, relation.StepId);
            }

            var arrivalRelations = relations.Where(r => r.State == FlightState.Arrival);
            foreach (var relation in arrivalRelations)
            {
                arrivalSteps.Add(relation.StationId, relation.StepId);
            }

            queueService.Add(relations);

            var dalStations = stationRepository.GetCurrentStationsState();

            foreach (var station in dalStations)
            {
                var stationManager = new StationManager(station.stepKey, station.Number, queueService);

                stationManager.TookNewPlane += (sender, e) =>
                {
                    onPlaneMoved?.Invoke(sender, e);
                };

                stationManager.PlaneFinished += (sender, e) =>
                {
                    if (sender is Plane plane)
                    {
                        var nextStep = GetNextMove(plane);

                        if (nextStep != null)
                            queueService.EnQueue(nextStep, plane);
                        else
                        {
                            plane.FinishWay();
                            onPlaneMoved?.Invoke(plane, e);
                        }
                    }
                };

                Stations.Add(stationManager);
            }

            waitingTimer = new Timer(15000);
            waitingTimer.Elapsed += Timer_Elapsed;
            waitingTimer.Start();
        }

        public void NewDepartureOrArrival(Plane Plane)
        {
            waitingList.Add(Plane);
        }

        public void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var planesReady = waitingList.Where(p => p.ActionTime <= DateTime.Now).ToList();
            foreach (var plane in planesReady)
            {
                string nextStep = GetNextMove(plane);

                queueService.EnQueue(nextStep, plane);

                waitingList.Remove(plane);
            }
        }

        public string GetNextMove(Plane plane)
        {
            if (plane.flightState == FlightState.Departure) return departureSteps[plane.StationNumber];
            else return arrivalSteps[plane.StationNumber];
        }

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