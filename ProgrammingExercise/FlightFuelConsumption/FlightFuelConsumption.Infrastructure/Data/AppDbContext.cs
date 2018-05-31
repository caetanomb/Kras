using FlightFuelConsumption.Infrastructure.Data.DataModel;
using FlightFuelConsumption.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightFuelConsumption.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AirportDataModel> Airports { get; set; }
        public DbSet<FlightDataModel> Flights { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {            
        }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AirportDataModelTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FlightDataModelTypeConfiguration());
        }
    }
}
