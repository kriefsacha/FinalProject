using BL.Interfaces;
using Common;
using System;
using System.Threading.Tasks;

namespace BL
{
    public class StationManager : IStationManager , IDisposable
    {
        /// <summary>
        /// Tells the control tour that the station took a new plane , for him to tell the UI
        /// </summary>
        public event EventHandler TookNewPlane;
        /// <summary>
        /// Tells the control tour that a plane finished , that she can tell him where he needs to go after
        /// </summary>
        public event EventHandler PlaneFinished;
        /// <summary>
        /// When the station is On , she will listen to her queue , if you want to Off just call Dispose Function
        /// </summary>
        private bool On;

        private int StationNumber;

        public Plane plane { get; private set; }

        private bool isAvailable { get { return plane == null; } }

        public StationManager(string stepKey, int StationNumber , IQueueService queueService)
        {
            this.StationNumber = StationNumber;

            On = true;

            //Begin a task in the background that listen to her queue everytime she is On , to disconnect her just call Dispose
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

        /// <summary>
        /// Send the plane to do what he have to do (refill gaz .. )
        /// </summary>
        /// <param name="plane">The plane that does the job</param>
        public void Do(Plane plane)
        {
            //Set the number of the station in the plane and calls his event that he moved to release the previous station
            plane.SetStation(StationNumber);

            TookNewPlane.Invoke(plane, EventArgs.Empty);

            //When this event will be called ( from the SetStation function ) , the plane moved so he will not be on this station anymore and she can continue
            plane.Moved += (sender, e) => {
                if (sender != this.StationNumber) this.plane = null;
            };

            //Wait the plane finish the job
            Task.Delay(plane.waitingTime).Wait();

            PlaneFinished.Invoke(plane, EventArgs.Empty);

            //Until a station is not available and doesnt take it , the plane is still here and the station is not available
            while (!isAvailable)
            {
                Task.Delay(200).Wait();
            }
        }

        /// <summary>
        /// Dispose the station (by putting the On in Off status)
        /// </summary>
        public void Dispose()
        {
            On = false;
        }
    }
}