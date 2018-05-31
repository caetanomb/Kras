using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightFuelConsumption.API.ApiModels
{
    public class EnterFlightViewModel
    {
        public int DepartureAirportId { get; set; }
        public int DestinationAirportId { get; set; }
        public double FlightTime { get; set; }
        public double TakeoffEffort { get; set; }
    }
}
