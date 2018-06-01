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

        public Flight(int id, Airport departureAirport, Airport destinationAirport,
           double flightTime, double takeoffEffort)
        {
            Id = id;
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

        /// <summary>
        /// Set new values and call CalculateDistance and CalculateFuelConsumption
        /// </summary>
        /// <param name="departureAirport"></param>
        /// <param name="destinationAirport"></param>
        /// <param name="flightTime"></param>
        /// <param name="takeoffEffort"></param>
        /// <returns></returns>
        public Flight ReCalculate(Airport departureAirport, Airport destinationAirport,
           double flightTime, double takeoffEffort)
        {
            this.DepartureAirport = departureAirport ?? throw new DomainException("Departure Airport is not valid");
            this.DestinationAirport = destinationAirport ?? throw new DomainException("Destination Airport is not valid");
            this.FlightTime = flightTime;
            this.TakeoffEffort = takeoffEffort;

            CalculateDistance().CalculateFuelConsumption();

            return this;
        }
    }
}
