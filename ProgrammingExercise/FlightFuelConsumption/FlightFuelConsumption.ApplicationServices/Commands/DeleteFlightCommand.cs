using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightFuelConsumption.ApplicationServices.Commands
{
    public class DeleteFlightCommand : IRequest
    {
        public int Id { get; set; }
    }
}
