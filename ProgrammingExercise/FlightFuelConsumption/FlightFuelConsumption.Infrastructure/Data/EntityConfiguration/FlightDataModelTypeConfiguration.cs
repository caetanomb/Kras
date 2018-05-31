using FlightFuelConsumption.Infrastructure.Data.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightFuelConsumption.Infrastructure.Data.EntityConfiguration
{
    public class FlightDataModelTypeConfiguration
        : IEntityTypeConfiguration<FlightDataModel>
    {
        public void Configure(EntityTypeBuilder<FlightDataModel> builder)
        {
            builder.ToTable("Flight");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Distance)
                .IsRequired();
            builder.Property(p => p.FuelConsumption)
                .IsRequired();
            builder.Property(p => p.FlightTime)
                .IsRequired();
            builder.Property(p => p.TakeoffEffort)
                .IsRequired();

            builder.HasOne(a => a.DepartureAirport)
                .WithMany(b => b.DepartureFlights)
                .HasForeignKey(c => c.DepartureAirportId);               

            builder.HasOne(a => a.DestinationAirport)
                .WithMany(b => b.DestinationFlights)
                .HasForeignKey(c => c.DestinationAirportId);

        }
    }
}
