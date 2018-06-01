using FlightFuelConsumption.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightFuelConsumption.Core.Interfaces
{
    public interface IFlightRepository
    {
        Task<int> EnterFlight(Flight flight);
        Task UpdateFlight(int id, Flight flight);
        Task<Flight> GetFlight(int id);
        Task Delete(int id);
    }
}
