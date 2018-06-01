using FlightFuelConsumption.ApplicationServices.Commands;
using FlightFuelConsumption.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightFuelConsumption.ApplicationServices.CommandHandlers
{
    public class DeleteFlightCommandHandler : IRequestHandler<DeleteFlightCommand>
    {
        private readonly IFlightRepository _flightRepository;

        public DeleteFlightCommandHandler(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _flightRepository.Delete(request.Id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
