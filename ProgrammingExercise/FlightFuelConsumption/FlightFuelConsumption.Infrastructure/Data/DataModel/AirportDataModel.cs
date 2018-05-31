using System;
using System.Collections.Generic;
using System.Text;

namespace FlightFuelConsumption.Infrastructure.Data.DataModel
{
    public class AirportDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public virtual List<FlightDataModel> DepartureFlights { get; set; }
        public virtual List<FlightDataModel> DestinationFlights { get; set; }
    }
}
