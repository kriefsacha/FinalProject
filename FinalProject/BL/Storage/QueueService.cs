using BL.Interfaces;
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
        Dictionary<string, ConcurrentQueue<Plane>> queuesSteps;

        public QueueService()
        {
            queuesSteps = new Dictionary<string, ConcurrentQueue<Plane>>();
        }

        public bool TryDequeue(string key, out Plane plane)
        {
            return queuesSteps[key].TryDequeue(out plane);
        }

        public void EnQueue(string key, Plane plane)
        {
            queuesSteps[key].Enqueue(plane);
        }

        public void Add(List<Relation> relations)
        {
            foreach (var relation in relations)
            {
                if (relation.StepId != null && !queuesSteps.ContainsKey(relation.StepId))
                    queuesSteps.Add(relation.StepId, new ConcurrentQueue<Plane>());
            }
        }
    }
}
