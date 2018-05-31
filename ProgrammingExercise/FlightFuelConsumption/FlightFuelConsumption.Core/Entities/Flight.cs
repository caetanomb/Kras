using FlightFuelConsumption.Core.SharedKernel;
using GeoCoordinatePortable;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightFuelConsumption.Core.Entities
{
    public class Flight : BaseEntity
    {
        public Airport DepartureAirport { get; private set; }
        public Airport DestinationAirport { get; private set; }
        public double Distance { get; private set; }
        public double FuelConsumption { get; private set; }
        public double FlightTime { get; private set; }
        public double TakeoffEffort { get; private set; }

        public Flight(Airport departureAirport, Airport destinationAirport, 
            double flightTime, double takeoffEffort)
        {
            DepartureAirport = departureAirport ?? throw new DomainException("Departure Airport is not valid");
            DestinationAirport = destinationAirport ?? throw new DomainException("Destination Airport is not valid");
            FlightTime = flightTime;
            TakeoffEffort = takeoffEffort;
        }

        public Flight CalculateDistance()
        {
            GeoCoordinate departureMapPoint = new GeoCoordinate(DepartureAirport.Latitude, DepartureAirport.Longitude);
            GeoCoordinate destinationMapPoint = new GeoCoordinate(DestinationAirport.Latitude, DestinationAirport.Longitude);

            double distanceInMeter = departureMapPoint.GetDistanceTo(destinationMapPoint);            

            //Convert meter to km
            Distance = Math.Round(distanceInMeter / 1000, 2);

            return this;
        }

        public Flight CalculateFuelConsumption()
        {
            FuelConsumption = Math.Round((Distance / FlightTime) + TakeoffEffort, 2);

            return this;
        }
    }
}
