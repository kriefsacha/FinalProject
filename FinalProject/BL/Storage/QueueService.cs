using BL.Interfaces;
using Common;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace BL.Storage
{
    public class QueueService : IQueueService
    {
        /// <summary>
        /// Each queue is a step to go through to continue , each station listen to a queue an the AirportManager after tells in which to go
        /// </summary>
        public Dictionary<string, ConcurrentQueue<Plane>> queuesSteps { get; private set; }

        public QueueService()
        {
            queuesSteps = new Dictionary<string, ConcurrentQueue<Plane>>();
        }

        /// <summary>
        /// Try to take out a plane from a queue
        /// </summary>
        /// <param name="key">Key to access to the certain queue</param>
        /// <param name="plane">Plane you took out</param>
        /// <returns>True if you took out a plane , false if there is no plane in the queue</returns>
        public bool TryDequeue(string key, out Plane plane)
        {
            return queuesSteps[key].TryDequeue(out plane);
        }

        /// <summary>
        /// Add a plane to a queue
        /// </summary>
        /// <param name="key">The key to access to the queue</param>
        /// <param name="plane">The plane to add to the queue</param>
        public void EnQueue(string key, Plane plane)
        {
            queuesSteps[key].Enqueue(plane);
        }

        /// <summary>
        /// Initialize all the queues from list of relations ( or just add them )
        /// </summary>
        /// <param name="relations">Relations to add (Just the stepId is important here)</param>
        public void Add(List<Relation> relations)
        {
            foreach (var relation in relations)
            {
                if (relation.NextStepId != null && !queuesSteps.ContainsKey(relation.NextStepId))
                    queuesSteps.Add(relation.NextStepId, new ConcurrentQueue<Plane>());
            }
        }
    }
}
