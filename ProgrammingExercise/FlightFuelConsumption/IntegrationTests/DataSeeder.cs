using FlightFuelConsumption.Core.Entities;
using FlightFuelConsumption.Infrastructure.Data;
using FlightFuelConsumption.Infrastructure.Data.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class DataSeeder
    {
        private AppDbContext _context;

        public DataSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            await SeedAirport();
            await SeedFlights();
        }

        public async Task SeedFlights()
        {
            List<FlightDataModel> flightList = new List<FlightDataModel>()
            {
                new FlightDataModel()
                {
                    DepartureAirportId = 1,
                    DestinationAirportId = 2,
                    Distance = 970,
                    FlightTime = 12,
                    FuelConsumption = 12,
                    TakeoffEffort = 23
                },
                new FlightDataModel()
                {
                    DepartureAirportId = 3,
                    DestinationAirportId = 4,
                    Distance = 500,
                    FlightTime = 10,
                    FuelConsumption = 5,
                    TakeoffEffort = 2
                }
            };
            
            await _context.Flights.AddRangeAsync(flightList);
            await _context.SaveChangesAsync();
        }

        public async Task SeedAirport()
        {
            List<AirportDataModel> airportList = new List<AirportDataModel>()
            {
                new AirportDataModel()
                {
                    Id = 1,
                    Latitude = -19.634150,
                    Longitude = -43.965385,
                    Name = "'Confins Airport"
                },
                new AirportDataModel()
                {
                    Id = 2,
                    Latitude = -22.805436,
                    Longitude = -43.256334,
                    Name = "'Galeao Airport"
                },
                new AirportDataModel()
                {
                    Id = 3,
                    Latitude = 52.311656,
                    Longitude = 4.768756,
                    Name = "'Schiphol Airport"
                },
                new AirportDataModel()
                {
                    Id = 4,
                    Latitude = 51.458270,
                    Longitude =  5.393663,
                    Name = "'Eindhoven Airport"
                }
            };

            await _context.Airports.AddRangeAsync(airportList);
            await _context.SaveChangesAsync();
        }
    }
}
