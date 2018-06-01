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
    public class UpdateFlightCommandHandler : IRequestHandler<UpdateFlightCommand>
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IAirportRepository _airportRepository;

        public UpdateFlightCommandHandler(IFlightRepository flightRepository,
            IAirportRepository airportRepository)
        {
            _flightRepository = flightRepository;
            _airportRepository = airportRepository;
        }

        public async Task Handle(UpdateFlightCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Airport departureAirport = await _airportRepository.GetAsync(request.DepartureAirportId);
                if (departureAirport == null)
                    throw new ApplicationException("Departure Airport does not exist");

                Airport destinationAirport = await _airportRepository.GetAsync(request.DestinationAirportId);
                if (destinationAirport == null)
                    throw new ApplicationException("Destination Airport does not exist");

                //Get existing Flight
                Flight existingFlight = await _flightRepository.GetFlight(request.Id);
                if (existingFlight == null)
                    throw new ApplicationException("Flight does not exist");

                existingFlight.ReCalculate(departureAirport, destinationAirport,
                    request.FlightTime, request.TakeoffEffort);

                await _flightRepository.UpdateFlight(request.Id, existingFlight);
            }
            catch (ApplicationException ex)
            {
                throw;
            }
        }
    }
}
