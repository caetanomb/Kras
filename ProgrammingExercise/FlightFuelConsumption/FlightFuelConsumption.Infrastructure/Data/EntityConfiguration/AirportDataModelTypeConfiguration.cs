using FlightFuelConsumption.Infrastructure.Data.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightFuelConsumption.Infrastructure.Data.EntityConfiguration
{
    public class AirportDataModelTypeConfiguration
        : IEntityTypeConfiguration<AirportDataModel>
    {
        public void Configure(EntityTypeBuilder<AirportDataModel> builder)
        {
            builder.ToTable("Airport");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
            .IsRequired();
            builder.Property(p => p.Latitude)
                .IsRequired();
            builder.Property(p => p.Longitude)
                .IsRequired();
        }
    }
}
