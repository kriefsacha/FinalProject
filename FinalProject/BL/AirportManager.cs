using Common;
using Common.Enums;
using Common.Interfaces;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BL
{
    public class AirportManager : IAirportManager
    {
        public List<StationManager> Stations { get; set; }
        public List<WaitingModel> timersList { get; set; }
        public IQueueService queueService { get; set; }

        IFlightRepository flightRepository;
        IPlaneRepository planeRepository;
        IStationRepository stationRepository;

        public AirportManager(IFlightRepository flightRepository,
            IPlaneRepository planeRepository,
            IStationRepository stationRepository ,IQueueService queueService)
        {
            this.flightRepository = flightRepository;
            this.planeRepository = planeRepository;
            this.stationRepository = stationRepository;
            this.queueService = queueService;

            Stations = new List<StationManager>();

            for (int i = 0; i < 8; i++)
            {
                if (i < 5)
                    Stations.Add(new StationManager("S" + (i + 1), queueService) { StationNumber = i + 1 });
                else if (i < 7)
                    Stations.Add(new StationManager("S6", queueService) { StationNumber = i + 1 });
                else
                    Stations.Add(new StationManager("S" + i , queueService) { StationNumber = i + 1 });

                Stations[i].TookNewPlane += (sender, e) =>
                {
                    //GlobalHost.ConnectionManager.GetHubContext<AirportHub>()
                };

                Stations[i].PlaneFinished += (sender, e) =>
                {
                    if (sender is Plane plane)
                    {
                        if (plane.flightState == FlightState.Arrival)
                        {
                            var nextStep = ArrivalMovement(plane);
                            if (nextStep != null)
                                queueService.EnQueue(nextStep, plane);
                            else
                                plane.FinishWay();
                        }
                        else
                        {
                            var nextStep = DepartureMovement(plane);
                            if(nextStep != null)
                            queueService.EnQueue(nextStep, plane);
                            else
                                plane.FinishWay();
                        }
                    }
                };
            }

            timersList = new List<WaitingModel>();
        }


        public void NewDepartureOrArrival(Plane Plane)
        {
            if (Plane.flightState == FlightState.Arrival) flightRepository.Arrival(Plane);
            else flightRepository.Departure(Plane);
            WaitingModel waitingModel = new WaitingModel();
            waitingModel.plane = Plane;
            Timer timer = new Timer();
            timer.Elapsed += Timer_Elapsed;
            var interval = Plane.ActionTime.Subtract(DateTime.Now);
            timer.Interval = interval.TotalMilliseconds; 
            waitingModel.timer = timer;
            timer.Start();
            timersList.Add(waitingModel);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(sender is Timer timer)
            {
                timer.Stop();
                var model = timersList.Where(m => m.timer == timer).FirstOrDefault();

                if(model != null)
                {
                    if(model.plane.flightState == FlightState.Arrival)
                    {
                        var nextStep = ArrivalMovement(model.plane);
                        queueService.EnQueue(nextStep, model.plane);
                    }
                    else
                    {
                        var nextStep = DepartureMovement(model.plane);
                        queueService.EnQueue(nextStep, model.plane);
                    }

                    timersList.Remove(model);
                }
            }
        }

        public string DepartureMovement(Plane plane)
        {
            switch (plane.StationNumber)
            {
                case 0: return "S6";
                case 4: return null;
                case 6: return "S7";
                case 7: return "S7";
                case 8: return "S4";
                default: return null;
            }
        }

        public string ArrivalMovement(Plane plane)
        {
            switch (plane.StationNumber)
            {
                case 0: return "S1";
                case 1: return "S2";
                case 2: return "S3";
                case 3: return "S4";
                case 4: return "S5";
                case 5: return "S6";
                case 6: return null;
                case 7: return null;
                default: return null;
            }
        }

        public List<Plane> GetFutureArrivals()
        {
            return flightRepository.GetFutureArrivals();
        }

        public List<Plane> GetFutureDepartures()
        {
            return flightRepository.GetFutureDepartures();
        }

        public List<Station> GetCurrentStationsState()
        {
            return stationRepository.GetCurrentStationsState();
        }

    }

    public class WaitingModel
    {
        public Timer timer { get; set; }
        public Plane plane { get; set; }
    }
}
