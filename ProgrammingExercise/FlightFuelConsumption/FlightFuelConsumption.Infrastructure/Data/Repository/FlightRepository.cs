using FlightFuelConsumption.Core.Entities;
using FlightFuelConsumption.Core.Interfaces;
using FlightFuelConsumption.Infrastructure.Data.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightFuelConsumption.Infrastructure.Data.Repository
{
    public class FlightRepository : IFlightRepository
    {
        private readonly AppDbContext _appDbContext;

        public FlightRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> EnterFlight(Flight flight)
        {
            FlightDataModel newFlight = new FlightDataModel()
            {
                DepartureAirportId = flight.DepartureAirport.Id,
                DestinationAirportId = flight.DestinationAirport.Id,
                Distance = flight.Distance,
                FuelConsumption = flight.FuelConsumption,
                FlightTime = flight.FlightTime,
                TakeoffEffort = flight.TakeoffEffort
            };

            await _appDbContext.Flights.AddAsync(newFlight);
            await _appDbContext.SaveChangesAsync();

            return newFlight.Id;
        }

        //private async Task<Flight> GetFlightAsync(int id)
        //{
        //    var flight = await _appDbContext.Flights
        //                    .Where(a => a.Id == id)
        //                    .Include(fli => fli.DepartureAirport)
        //                    .Include(fli => fli.DestinationAirport).FirstOrDefaultAsync();

        //    return new Flight(new Airport(flight.DepartureAirport))
        //}
    }
}
