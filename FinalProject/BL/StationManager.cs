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
    public class StationManager : IDisposable
    {
        public event EventHandler TookNewPlane;
        public event EventHandler PlaneFinished;

        public StationManager(string queueKey, IQueueService queueService)
        {
            this.queueKey = queueKey;
            On = true;

            new Task(() =>
            {
                while (On)
                {
                    while (queueService.TryDequeue(queueKey, out Plane plane))
                    {
                        this.plane = plane;
                        Do(plane);
                    }
                    Task.Delay(200).Wait();
                }
            }).Start();

        }

        public bool On { get; set; }

        public string queueKey { get; set; }

        public int StationNumber { get; set; }

        public Plane plane { get; set; }

        public bool isAvailable { get { return plane == null; } }



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
