using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class AirportInitializer : DropCreateDatabaseAlways<AirportDataModel>
    {
        protected override void Seed(AirportDataModel context)
        {
            context.Stations.Add(new Station() { Number = 1 , stepKey = "S1"});
            context.Stations.Add(new Station() { Number = 2  , stepKey = "S2"});
            context.Stations.Add(new Station() { Number = 3 , stepKey = "S3"});
            context.Stations.Add(new Station() { Number = 4  , stepKey = "S4"});
            context.Stations.Add(new Station() { Number = 5 , stepKey = "S5"});
            context.Stations.Add(new Station() { Number = 6 , stepKey = "S6"});
            context.Stations.Add(new Station() { Number = 7 , stepKey = "S6"});
            context.Stations.Add(new Station() { Number = 8 , stepKey = "S7"});
            context.SaveChanges();
        }
    }
}
