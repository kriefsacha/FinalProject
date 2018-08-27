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
        public List<StationManager> Stations { get; set; }
        List<WaitingModel> waitingList { get; set; }
        IQueueService queueService { get; set; }
        IStationRepository stationRepository { get; set; }
        public event EventHandler planeMoved;
        Dictionary<int, string> arrivalSteps;
        Dictionary<int, string> departureSteps;

        public AirportManager(IQueueService queueService , IStationRepository stationRepository)
        {
            this.queueService = queueService;
            this.stationRepository = stationRepository;
            Initialize();
        }

        private void Initialize()
        {
            Stations = new List<StationManager>();
            waitingList = new List<WaitingModel>();
            arrivalSteps = new Dictionary<int, string>();
            departureSteps = new Dictionary<int, string>();

            var dalStations = stationRepository.GetCurrentStationsState();

            foreach (var station in dalStations)
            {
                var stationManager = new StationManager(station.stepKey, station.Number, queueService);

                stationManager.TookNewPlane += (sender, e) =>
                {
                    planeMoved.Invoke(sender, e);
                };

                stationManager.PlaneFinished += (sender, e) =>
                {
                    if (sender is Plane plane)
                    {
                        var nextStep = "";

                        if (plane.flightState == FlightState.Arrival)
                            nextStep = ArrivalMovement(plane);
                        else
                            nextStep = DepartureMovement(plane);

                        if (nextStep != null)
                            queueService.EnQueue(nextStep, plane);
                        else
                        {
                            plane.FinishWay();
                            planeMoved.Invoke(plane, e);
                        }
                    }
                };

                Stations.Add(stationManager);
            }


            arrivalSteps.Add(0, "S1");
            arrivalSteps.Add(1, "S2");
            arrivalSteps.Add(2, "S3");
            arrivalSteps.Add(3, "S4");
            arrivalSteps.Add(4, "S5");
            arrivalSteps.Add(5, "S6");
            arrivalSteps.Add(6, null);
            arrivalSteps.Add(7, null);

            departureSteps.Add(0, "S6");
            departureSteps.Add(4, null);
            departureSteps.Add(6, "S7");
            departureSteps.Add(7, "S7");
            departureSteps.Add(8, "S4");
        }

        public void NewDepartureOrArrival(Plane Plane)
        {
            WaitingModel waitingModel = new WaitingModel();
            waitingModel.plane = Plane;

            Timer timer = new Timer();
            timer.Elapsed += Timer_Elapsed;
            var interval = Plane.ActionTime.Subtract(DateTime.Now);
            timer.Interval = interval.TotalMilliseconds;
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
                    string nextStep = "";

                    if (model.plane.flightState == FlightState.Arrival) nextStep = ArrivalMovement(model.plane);

                    else nextStep = DepartureMovement(model.plane);

                    queueService.EnQueue(nextStep, model.plane);

                    waitingList.Remove(model);
                }
            }
        }

        public string DepartureMovement(Plane plane)
        {
            return departureSteps[plane.StationNumber];
        }

        public string ArrivalMovement(Plane plane)
        {
            return arrivalSteps[plane.StationNumber];
        }

        public void AddStation(Station station)
        {
            Stations.Add(new StationManager(station.stepKey, station.Number, queueService));
            queueService.Add(station.stepKey);
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
