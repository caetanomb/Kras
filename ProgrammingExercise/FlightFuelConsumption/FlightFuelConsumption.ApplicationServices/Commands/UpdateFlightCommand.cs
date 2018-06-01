﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightFuelConsumption.ApplicationServices.Commands
{
    public class UpdateFlightCommand : IRequest
    {
        public int Id { get; set; }
        public int DepartureAirportId { get; set; }
        public int DestinationAirportId { get; set; }
        public double FlightTime { get; set; }
        public double TakeoffEffort { get; set; }
    }
}
