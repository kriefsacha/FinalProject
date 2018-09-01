using Common;
using System.Collections.Generic;

namespace BL.Interfaces
{
    public interface IQueueService
    {
        void EnQueue(string key, Plane plane);
        bool TryDequeue(string key, out Plane plane);
        void Add(List<Relation> relations);
    }
}
