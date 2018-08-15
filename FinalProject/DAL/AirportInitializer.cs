using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class AirportInitializer : CreateDatabaseIfNotExists<AirportDataModel>
    {
        protected override void Seed(AirportDataModel context)
        {
            context.Stations.Add(new Station() { Number = 1 });
            context.Stations.Add(new Station() { Number = 2 });
            context.Stations.Add(new Station() { Number = 3 });
            context.Stations.Add(new Station() { Number = 4 });
            context.Stations.Add(new Station() { Number = 5 });
            context.Stations.Add(new Station() { Number = 6 });
            context.Stations.Add(new Station() { Number = 7 });
            context.Stations.Add(new Station() { Number = 8 });
            context.SaveChanges();
        }
    }
}
