using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class StationManager
    {
        public delegate void FinishedDelegate(StationManager sender, EventArgs e);

        public event FinishedDelegate finishedEvent;

        public int StationNumber { get; set; }

        public bool isAvailable { get; set; }

        public void Do(Plane plane)
        {
            isAvailable = false;

            Task.Delay(plane.waitingTime).ContinueWith((task) =>
            {
                isAvailable = true;

                finishedEvent.Invoke(this, EventArgs.Empty);
            }).Wait();
        }
    }
}
