using Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace DAL
{
    public class AirportDataModel : DbContext
    {
        //static readonly string connectionString = CloudConfigurationManager.GetSetting("dbconnectionstring");

        static readonly string connectionString = @"data source=(LocalDb)\MSSQLLocalDB;initial catalog=FinalProjectDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";

        public AirportDataModel()
            : base(connectionString)
        {
            Database.SetInitializer(new AirportInitializer());
        }

        public virtual DbSet<Station> Stations { get; set; }

        public virtual DbSet<Plane> Planes { get; set; }

        public virtual DbSet<History> Histories { get; set; }

        public virtual DbSet<Departure> Departures { get; set; }

        public virtual DbSet<Arrival> Arrivals { get; set; }

        public virtual DbSet<Relation> Relations { get; set; }
    }

    public class Station
    {
        [Key]
        public int Id { get; set; }

        public int Number { get; set; }

        public int? PlaneId { get; set; }

        public virtual Plane Plane { get; set; }

        public bool IsAvailable { get; set; }

        public string stepKey { get; set; }

    }

    public class Plane
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int StationNumber { get; set; }

        public int PreviousStationNumber { get; set; }

        public int waitingTime { get; set; }

        public DateTime ActionDate { get; set; }

        public FlightState flightState { get; set; }
    }

    public class History
    {
        [Key]
        public int Id { get; set; }

        public string PlaneId { get; set; }

        public int StationNumber { get; set; }

        public DateTime? DateIn { get; set; }

        public DateTime? DateOut { get; set; }
    }

    public class Departure
    {
        [Key]
        public int Id { get; set; }

        public string PlaneId { get; set; }

        public DateTime DatePlanned { get; set; }

        public int waitingTime { get; set; }
    }

    public class Arrival
    {
        [Key]
        public int Id { get; set; }

        public string PlaneId { get; set; }

        public DateTime DatePlanned { get; set; }

        public int waitingTime { get; set; }
    }

    public class Relation
    {
        [Key]
        public int Id { get; set; }

        public int StationId { get; set; }

        public string NextStepId { get; set; }

        public FlightState State { get; set; }
    }
}