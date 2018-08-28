using BL.Storage;
using Common;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class QueueServiceTests
    {
        [Fact]
        public void EnQueueTest()
        {
            QueueService queueService = new QueueService();
            queueService.Add(new List<Relation>()
            {
                new Relation(1,"S1" , FlightState.Departure),
            });

            queueService.EnQueue("S1", new Plane("FR123", DateTime.Now, 3000, FlightState.Departure));

            Assert.False(queueService.queuesSteps["S1"].IsEmpty);
        }

        [Fact]
        public void TryDeQueue_PositiveTest()
        {
            QueueService queueService = new QueueService();
            queueService.Add(new List<Relation>()
            {
                new Relation(1,"S1" , FlightState.Departure),
            });

            queueService.EnQueue("S1", new Plane("FR123", DateTime.Now, 3000, FlightState.Departure));

            queueService.TryDequeue("S1", out Plane plane);

            Assert.True(queueService.queuesSteps["S1"].IsEmpty);
        }

        [Fact]
        public void TryDeQueue_NegativeTest()
        {
            QueueService queueService = new QueueService();
            queueService.Add(new List<Relation>()
            {
                new Relation(1,"S1" , FlightState.Departure),
            });

            queueService.TryDequeue("S1", out Plane plane);

            Assert.True(queueService.queuesSteps["S1"].IsEmpty);
        }

        [Fact]
        public void AddTest()
        {
            QueueService queueService = new QueueService();
            queueService.Add(new List<Relation>()
            {
                new Relation(1,"S1" , FlightState.Departure),
            });

            Assert.True(queueService.queuesSteps.ContainsKey("S1"));
        }
    }
}
