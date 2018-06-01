using FlightFuelConsumption.ApplicationServices.Commands;
using FlightFuelConsumption.Core.Entities;
using FlightFuelConsumption.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightFuelConsumption.ApplicationServices.CommandHandlers
{
    public class EnterFlightCommandHandler : IRequestHandler<EnterFlightCommand, int>
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IAirportRepository _airportRepository;

        public EnterFlightCommandHandler(IFlightRepository flightRepository,
            IAirportRepository airportRepository)
        {
            _flightRepository = flightRepository;
            _airportRepository = airportRepository;
        }

        public async Task<int> Handle(EnterFlightCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Airport departureAirport = await _airportRepository.GetAsync(request.DepartureAirportId);
                if (departureAirport == null)
                    throw new ApplicationException("Departure Airport does not exist");

                Airport destinationAirport = await _airportRepository.GetAsync(request.DestinationAirportId);
                if (destinationAirport == null)
                    throw new ApplicationException("Destination Airport does not exist");

                Flight newFlight =
                    new Flight(departureAirport, destinationAirport, request.FlightTime, request.TakeoffEffort)
                    .CalculateDistance()
                    .CalculateFuelConsumption();

                var identity = await _flightRepository.EnterFlight(newFlight);

                return identity;
            }
            catch (ApplicationException ex)
            {
                throw;
            }
        }
    }
}
