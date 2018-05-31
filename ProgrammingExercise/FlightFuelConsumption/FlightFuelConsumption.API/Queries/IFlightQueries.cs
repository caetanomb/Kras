using FlightFuelConsumption.API.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightFuelConsumption.API.Queries
{
    public interface IFlightQueries
    {
        Task<IEnumerable<FlightViewModel>> GetFlightsAsync();
    }
}
