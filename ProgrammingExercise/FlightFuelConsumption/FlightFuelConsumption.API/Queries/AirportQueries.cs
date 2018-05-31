using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightFuelConsumption.API.ApiModels;
using FlightFuelConsumption.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightFuelConsumption.API.Queries
{
    public class AirportQueries : IAirportQueries
    {
        private readonly AppDbContext _appDbContext;

        public AirportQueries(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<AirportViewModel>> GetAirportsAsync()
        {
            var result = await _appDbContext.Airports                
                .ToArrayAsync();

            return result.Select(a => new AirportViewModel()
            {
                Id = a.Id,
                Name = a.Name,
                Latitude = a.Latitude,
                Longitude = a.Longitude
            });
        }
    }
}
