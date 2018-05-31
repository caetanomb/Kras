using FlightFuelConsumption.API.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightFuelConsumption.API.Queries
{
    public interface IAirportQueries
    {
        Task<IEnumerable<AirportViewModel>> GetAirportsAsync();
    }
}
