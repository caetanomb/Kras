using FlightFuelConsumption.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightFuelConsumption.Core.Entities
{
    public class Airport
    {
        public string Name { get; private set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Airport(string name, double latitude, double longitude)
        {
            Name = (!string.IsNullOrEmpty(name) ? name : throw new DomainException("Name is not valid"));
            Latitude = latitude;
            Longitude = longitude;
        }        
    }
}
