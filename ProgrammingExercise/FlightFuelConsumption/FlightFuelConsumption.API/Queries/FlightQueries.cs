using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightFuelConsumption.API.ApiModels;
using FlightFuelConsumption.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightFuelConsumption.API.Queries
{
    public class FlightQueries : IFlightQueries
    {
        private readonly AppDbContext _appDbContext;

        public FlightQueries(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<FlightViewModel>> GetFlightsAsync()
        {
            var result = await _appDbContext.Flights
                .Include(fli => fli.DepartureAirport)
                .Include(fli2 => fli2.DestinationAirport)
                .ToArrayAsync();

            return result.Select(a => new FlightViewModel()
            {
                DepartureAirportName = a.DepartureAirport.Name,
                DestinationAirportName = a.DestinationAirport.Name,
                Distance = a.Distance,
                FuelConsumption = a.FuelConsumption,
                FlightTime = a.FlightTime,
                TakeoffEffort = a.TakeoffEffort
            });
        }
    }
}
