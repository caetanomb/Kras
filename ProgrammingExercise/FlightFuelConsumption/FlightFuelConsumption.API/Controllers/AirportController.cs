using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightFuelConsumption.API.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FlightFuelConsumption.API.Controllers
{
    [Route("api/[controller]")]    
    public class AirportController : Controller
    {        
        private readonly IAirportQueries _airportQueries;

        public AirportController(IAirportQueries airportQueries)
        {
            _airportQueries = airportQueries ?? throw new ArgumentNullException(nameof(airportQueries));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var flights = await _airportQueries.GetAirportsAsync();

            return Ok(flights);
        }
    }
}