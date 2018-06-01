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

        public async Task Delete(int id)
        {
            FlightDataModel existingFlight =
                await _appDbContext.Flights.Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            _appDbContext.Entry(existingFlight).State = EntityState.Deleted;
            await _appDbContext.SaveChangesAsync();
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

        public async Task<Flight> GetFlight(int id)
        {
            var flight = await _appDbContext.Flights
                            .Where(a => a.Id == id)
                            .Include(fli => fli.DepartureAirport)
                            .Include(fli => fli.DestinationAirport).FirstOrDefaultAsync();

            if (flight == null)
                return null;

            return new Flight(flight.Id,
                              new Airport(flight.DepartureAirport.Id, flight.DepartureAirport.Name, flight.DepartureAirport.Latitude, flight.DepartureAirport.Longitude),
                              new Airport(flight.DestinationAirport.Id, flight.DestinationAirport.Name, flight.DestinationAirport.Latitude, flight.DestinationAirport.Longitude),
                              flight.FlightTime,
                              flight.TakeoffEffort);
        }

        public async Task UpdateFlight(int id, Flight flight)
        {
            FlightDataModel existingFlight =
                await _appDbContext.Flights.Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            existingFlight.DepartureAirportId = flight.DepartureAirport.Id;
            existingFlight.DestinationAirportId = flight.DestinationAirport.Id;
            existingFlight.Distance = flight.Distance;
            existingFlight.FuelConsumption = flight.FuelConsumption;
            existingFlight.FlightTime = flight.FlightTime;
            existingFlight.TakeoffEffort = flight.TakeoffEffort;

            _appDbContext.Entry(existingFlight).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
