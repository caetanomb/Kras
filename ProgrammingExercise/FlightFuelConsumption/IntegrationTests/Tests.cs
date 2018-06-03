using System;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlightFuelConsumption.API;
using System.Net.Http;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using System.Text;

namespace IntegrationTests
{    
    public class Tests : IClassFixture<TestFixture<TestStartup>>
    {
        private readonly HttpClient _client;

        public Tests(TestFixture<TestStartup> fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task Get_Airport_EndpointReturnSuccess()
        {
            // Act
            var response = await _client.GetAsync("/api/Airport");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            //Check if a specific property exists wihtin the response json
            Assert.Contains("\"Latitude\"", responseString);
        }

        [Fact]
        public async Task Get_Flight_EndpointReturnSuccess()
        {
            // Act
            var response = await _client.GetAsync("/api/Flight");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            //Check if a specific property exists wihtin the response json
            Assert.Contains("\"FuelConsumption\"", responseString);
        }

        [Fact]
        public async Task Post_Flight_EndpointReturnSuccess()
        {
            // Act
            var data = new
            {
                DepartureAirportId = "3",
                DestinationAirportId = "3",                
                FlightTime = "10",                
                TakeoffEffort = "5"
            };

            string stringJson = JsonConvert.SerializeObject(data);

            var response = await _client.PostAsync("/api/Flight", new StringContent(stringJson, Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);            
        }

        [Fact]
        public async Task Put_Flight_EndpointReturnSuccess()
        {
            // Act
            var data = new
            {
                DepartureAirportId = "1",
                DestinationAirportId = "4",                
                FlightTime = "10",
                TakeoffEffort = "10"
            };

            string stringJson = JsonConvert.SerializeObject(data);

            var response = await _client.PutAsync("/api/Flight?=1", new StringContent(stringJson, Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Flight_EndpointReturnSuccess()
        {                        
            var response = await _client.DeleteAsync("/api/Flight?=2");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
