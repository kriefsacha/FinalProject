namespace DAL
{
    using Common;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;

    public class AirportDataModel : DbContext
    {
        public AirportDataModel()
            : base("name=AirportDataModel")
        {
            Database.SetInitializer(new AirportInitializer());
        }

        public virtual DbSet<Station> Stations { get; set; }

        public virtual DbSet<History> Histories { get; set; }

        public virtual DbSet<Departure> Departures { get; set; }

        public virtual DbSet<Arrival> Arrivals { get; set; }
    }

    public class Station
    {
        [Key]
        public int Id { get; set; }

        public int Number { get; set; }

        public Plane Plane { get; set; }

        public bool IsAvailable
        {
            get { return Plane == null; }
        }

    }

    public class History
    {
        [Key]
        public int Id { get; set; }

        public string PlaneId { get; set; }

        public int StationNumber { get; set; }

        public DateTime DateIn { get; set; }

        public DateTime DateOut { get; set; }
    }

    public class Departure
    {
        [Key]
        public int Id { get; set; }

        public string PlaneId { get; set; }

        public DateTime DatePlanned { get; set; }
    }

    public class Arrival
    {
        [Key]
        public int Id { get; set; }

        public string PlaneId { get; set; }

        public DateTime DatePlanned { get; set; }
    }
}