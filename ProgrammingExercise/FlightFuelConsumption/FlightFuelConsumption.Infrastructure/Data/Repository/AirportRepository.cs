using FlightFuelConsumption.Core.Entities;
using FlightFuelConsumption.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightFuelConsumption.Infrastructure.Data.Repository
{
    public class AirportRepository : IAirportRepository
    {
        private readonly AppDbContext _appDbContext;

        public AirportRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<Airport> GetAsync(int airportId)
        {
            var airPort = _appDbContext.Airports.FirstOrDefault();

            return Task.FromResult(new Airport(airPort.Id, airPort.Name, airPort.Latitude, airPort.Longitude));             
        }
    }
}
