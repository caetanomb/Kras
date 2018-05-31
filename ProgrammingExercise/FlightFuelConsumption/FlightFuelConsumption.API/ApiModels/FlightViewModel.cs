using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightFuelConsumption.API.ApiModels
{
    public class FlightViewModel
    {
        public string DepartureAirportName { get; set; }
        public string DestinationAirportName { get; set; }
        public double Distance { get; set; }
        public double FuelConsumption { get; set; }
        public double FlightTime { get; set; }
        public double TakeoffEffort { get; set; }
    }
}
