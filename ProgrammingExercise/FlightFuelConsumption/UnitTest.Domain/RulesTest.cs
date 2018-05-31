using FlightFuelConsumption.Core.Entities;
using FlightFuelConsumption.Core.SharedKernel;
using System;
using Xunit;

namespace UnitTest.Domain
{
    public class RulesTest
    {
        [Fact]
        public void User_Shouldbe_AbleTo_Enter_New_Flight_Departure_Destination()
        {
            var departureAirport = new Airport("CNF", -22.805436, -43.256334);
            var destinationAirport = new Airport("GLA", -19.634150, -43.965385);

            var obj = new Flight(departureAirport, destinationAirport, 2, 10);

            Assert.Equal(departureAirport.Name, obj.DepartureAirport.Name);
            Assert.Equal(departureAirport.Latitude, obj.DepartureAirport.Latitude);
            Assert.Equal(departureAirport.Longitude, obj.DepartureAirport.Longitude);

            Assert.Equal(destinationAirport.Name, obj.DestinationAirport.Name);
            Assert.Equal(destinationAirport.Latitude, obj.DestinationAirport.Latitude);
            Assert.Equal(destinationAirport.Longitude, obj.DestinationAirport.Longitude);
        }

        [Fact]
        public void Calculate_Aiport_Distance_From_Two_Latitude_Longitude()
        {
            var departureAirport = new Airport("CNF", -22.805436, -43.256334);
            var destinationAirport = new Airport("GLA", -19.634150, -43.965385);
            var distance = 360.52;

            var obj = new Flight(departureAirport, destinationAirport, 3, 20)
                .CalculateDistance();            

            Assert.Equal(distance, obj.Distance);
        }

        [Theory]
        [InlineData(2, 10, 190.26)]
        [InlineData(10, 15, 51.05)]
        [InlineData(15, 5, 29.03)]
        public void Calculate_Fuel_Comsuption(double flightTime, double takeoffEffort, double fuelConsumption)
        {
            var departureAirport = new Airport("CNF", -22.805436, -43.256334);
            var destinationAirport = new Airport("GLA", -19.634150, -43.965385);                  

            var obj = new Flight(departureAirport, destinationAirport, flightTime, takeoffEffort)
                .CalculateDistance().CalculateFuelConsumption();

            Assert.Equal(fuelConsumption, obj.FuelConsumption);
        }

        [Fact]
        public void DomainException_Airport_Property_Name_NotValid()
        {            
            var domainException = Assert.ThrowsAny<DomainException>(() =>
            {
                var departureAirport = new Airport(null, -22.805436, -43.256334);
            });

            Assert.Equal("Name is not valid", domainException.Message);
        }

        [Fact]
        public void DomainException_Flight_Property_DepartureAirport_NotValid()
        {
            var domainException = Assert.ThrowsAny<DomainException>(() =>
            {
                var departureAirport = new Flight(null, new Airport("CNF", -22.805436, -43.256334), 3, 20);
            });

            Assert.Equal("Departure Airport is not valid", domainException.Message);
        }

        [Fact]
        public void DomainException_Flight_Property_DestinationAirport_NotValid()
        {
            var domainException = Assert.ThrowsAny<DomainException>(() =>
            {
                var departureAirport = new Flight(new Airport("CNF", -22.805436, -43.256334), null, 3, 20);
            });

            Assert.Equal("Destination Airport is not valid", domainException.Message);
        }
    }
}
