using BL.Interfaces;
using Common;
using Common.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class StationManager : IStationManager , IDisposable
    {
        public event EventHandler TookNewPlane;
        public event EventHandler PlaneFinished;

        private bool On;

        private int StationNumber;

        public Plane plane { get; private set; }

        private bool isAvailable { get { return plane == null; } }

        public StationManager(string stepKey, int number , IQueueService queueService)
        {
            StationNumber = number;

            On = true;

            new Task(() =>
            {
                while (On)
                {
                    while (queueService.TryDequeue(stepKey, out Plane plane))
                    {
                        this.plane = plane;
                        Do(plane);
                    }
                    Task.Delay(200).Wait();
                }
            }).Start();
        }

        public void Do(Plane plane)
        {
            plane.SetStation(StationNumber);
            TookNewPlane.Invoke(plane, EventArgs.Empty);

            plane.Moved += (sender, e) => {
                if (sender != this.StationNumber) this.plane = null;
            };

            Task.Delay(plane.waitingTime).Wait();

            PlaneFinished.Invoke(plane, EventArgs.Empty);

            while (!isAvailable)
            {
                Task.Delay(200).Wait();
            }
        }

        public void Dispose()
        {
            On = false;
        }
    }
}
