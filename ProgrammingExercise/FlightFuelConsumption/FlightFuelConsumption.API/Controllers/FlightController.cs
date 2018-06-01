using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightFuelConsumption.API.ApiModels;
using FlightFuelConsumption.API.Queries;
using FlightFuelConsumption.ApplicationServices.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

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
        private readonly IMemoryCache _memoryCache;
        private const string CacheFlightList = "FlightList";

        public FlightController(IMediator mediator, IFlightQueries flightQueries, IMemoryCache memoryCache)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); ;
            _flightQueries = flightQueries ?? throw new ArgumentNullException(nameof(flightQueries));
            _memoryCache = memoryCache;
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

            int commandResultEntityId = await _mediator.Send(enterFlightCommand);

            if (commandResultEntityId <= 0)
                return (IActionResult)BadRequest();

            ClearCache();

            return CreatedAtAction("Get", new { id = commandResultEntityId }, null);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] EnterFlightViewModel viewModel)
        {
            //The command class could be used as API Model too. 
            //Otherwise if any non-business info should come from the front-end as a meaning of validation
            //it is better create a ApiModel

            UpdateFlightCommand updateFlightCommand = new UpdateFlightCommand()
            {
                Id = id,
                DepartureAirportId = viewModel.DepartureAirportId,
                DestinationAirportId = viewModel.DestinationAirportId,
                FlightTime = viewModel.FlightTime,
                TakeoffEffort = viewModel.TakeoffEffort
            };

            await _mediator.Send(updateFlightCommand);

            ClearCache();

            return Ok();
        }

        private void ClearCache()
        {
            _memoryCache.Remove(CacheFlightList);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _memoryCache.GetOrCreateAsync(CacheFlightList, (entry) =>
            {
                var cachedList =
                          _flightQueries.GetFlightsAsync();

                entry.SetSlidingExpiration(TimeSpan.FromMinutes(3));

                return cachedList;
            });

            return Ok(list);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            DeleteFlightCommand deleteFlightCommand = new DeleteFlightCommand()
            {
                Id = id
            };

            await _mediator.Send(deleteFlightCommand);

            ClearCache();
        }
    }
}
