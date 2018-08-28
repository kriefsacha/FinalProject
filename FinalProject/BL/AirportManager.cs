using BL.Interfaces;
using Common;
using Common.Enums;
using Common.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BL
{
    public class AirportManager : IAirportManager, IDisposable
    {
        public List<StationManager> Stations { get; private set; }
        private List<WaitingModel> waitingList;
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
            waitingList = new List<WaitingModel>();
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
        }

        public void NewDepartureOrArrival(Plane Plane)
        {
            WaitingModel waitingModel = new WaitingModel();
            waitingModel.plane = Plane;

            var interval = Plane.ActionTime.Subtract(DateTime.Now);

            Timer timer = new Timer(interval.TotalMilliseconds);
            timer.Elapsed += Timer_Elapsed;
            waitingModel.timer = timer;
            timer.Start();

            waitingList.Add(waitingModel);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (sender is Timer timer)
            {
                timer.Stop();
                var model = waitingList.Where(m => m.timer == timer).FirstOrDefault();

                if (model != null)
                {
                    string nextStep = GetNextMove(model.plane);

                    queueService.EnQueue(nextStep, model.plane);

                    waitingList.Remove(model);
                }
            }
        }

        public string GetNextMove(Plane plane)
        {
            if (plane.flightState == FlightState.Departure) return departureSteps[plane.StationNumber];
            else return arrivalSteps[plane.StationNumber];
        }

        public void Dispose()
        {
            foreach (var station in Stations)
            {
                station.Dispose();
            }
        }
    }

    public class WaitingModel
    {
        public Timer timer { get; set; }
        public Plane plane { get; set; }
    }
}
