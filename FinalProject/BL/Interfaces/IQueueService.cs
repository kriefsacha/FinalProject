using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IQueueService
    {
        void EnQueue(string key, Plane plane);
        bool TryDequeue(string key, out Plane plane);
        void Add(List<Relation> relations);
    }
}
