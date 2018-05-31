using System;
using System.Collections.Generic;
using System.Text;

namespace FlightFuelConsumption.Infrastructure.Data.DataModel
{
    public class FlightDataModel
    {
        public int Id { get; set; }
        public virtual AirportDataModel DepartureAirport { get; set; }
        public virtual AirportDataModel DestinationAirport { get; set; }
        public double Distance { get; set; }
        public double FuelConsumption { get; set; }
        public double FlightTime { get; set; }
        public double TakeoffEffort { get; set; }

        public int? DepartureAirportId { get; set; }
        public int? DestinationAirportId { get; set; }
    }
}
