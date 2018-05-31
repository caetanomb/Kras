using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightFuelConsumption.API.ApiModels;
using FlightFuelConsumption.API.Queries;
using FlightFuelConsumption.ApplicationServices.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightFuelConsumption.API.Controllers
{
    /// <summary>
    /// API enables the user to enter Flights
    /// </summary>
    [Route("api/[controller]")]    
    public class FlightController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IFlightQueries _flightQueries;

        public FlightController(IMediator mediator, IFlightQueries flightQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); ;
            _flightQueries = flightQueries ?? throw new ArgumentNullException(nameof(flightQueries));
        }

        [HttpPost]        
        public async Task<IActionResult> Post([FromBody] EnterFlightViewModel viewModel)
        {
            //The command class could be used as API Model too. 
            //Otherwise if any non-business info should come from the front-end as a meaning of validation
            //it is better create a ApiModel

            EnterFlightCommand enterFlightCommand = new EnterFlightCommand()
            {
                DepartureAirportId = viewModel.DepartureAirportId,
                DestinationAirportId = viewModel.DestinationAirportId,
                FlightTime = viewModel.FlightTime,
                TakeoffEffort = viewModel.TakeoffEffort
            };

            bool commandResult = await _mediator.Send(enterFlightCommand);

            if (!commandResult)
                return (IActionResult)BadRequest();

            return CreatedAtAction("Get", new { id = 1 }, null);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var flights = await _flightQueries.GetFlightsAsync();

            return Ok(flights);
        }
    }
}