using FlightFuelConsumption.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightFuelConsumption.Core.Interfaces
{
    public interface IAirportRepository
    {
        Task<Airport> GetAsync(int airportId);
    }
}
