using FlightFuelConsumption.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightFuelConsumption.Core.Entities
{
    public class Airport : BaseEntity
    {
        public string Name { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public Airport(string name, double latitude, double longitude)
        {
            Name = (!string.IsNullOrEmpty(name) ? name : throw new DomainException("Name is not valid"));
            Latitude = latitude;
            Longitude = longitude;
        }

        public Airport(int id, string name, double latitude, double longitude)
        {
            Id = id;
            Name = (!string.IsNullOrEmpty(name) ? name : throw new DomainException("Name is not valid"));
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
