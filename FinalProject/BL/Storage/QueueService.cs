using Common;
using Common.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Storage
{
    public class QueueService : IQueueService
    {
        public Dictionary<string, ConcurrentQueue<Plane>> queuesSteps;

        public QueueService()
        {
            queuesSteps = new Dictionary<string, ConcurrentQueue<Plane>>();
            queuesSteps.Add("S1", new ConcurrentQueue<Plane>());
            queuesSteps.Add("S2", new ConcurrentQueue<Plane>());
            queuesSteps.Add("S3", new ConcurrentQueue<Plane>());
            queuesSteps.Add("S4", new ConcurrentQueue<Plane>());
            queuesSteps.Add("S5", new ConcurrentQueue<Plane>());
            queuesSteps.Add("S6", new ConcurrentQueue<Plane>());
            queuesSteps.Add("S7", new ConcurrentQueue<Plane>());

        }

        public bool TryDequeue(string key, out Plane plane)
        {
           return queuesSteps[key].TryDequeue(out plane);
        }

        public void EnQueue(string key, Plane plane)
        {
            queuesSteps[key].Enqueue(plane);
        }
    }
}
