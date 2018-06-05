# Flight Fuel Consumption

CRUD web application - A user is able to enter a Flight which has a departure airport and a destination airport.
The Domain Model calculates the distance from departure and destination airport and the fuel needed.

Domain rule: Fuel consumption = (distance / flight time) + takeoff effort

A report page shows a summary of all entered data including the calculate data

# Details - Designs patterns

 - Unit tests - Xunit.net
 - MediatR
 - CQRS - Command Query Responsibility Segregation
 - Repository
 - Domain Model
 - Dependency Injection
 
 # Required frameworks and tools
 - .Net Core 2.0
 - Nodejs
 - Visual Studio, preferably
 
 # How to run
 
 - Download project or clone repo
 - Open Command Prompt
 - Access app root folder .\FlightFuelConsumption
 - Execute tests - for Integration tests => dotnet test IntegrationTests | for unit tests => dotnet test UnitTest.Domain
 - Access directory \FlightFuelConsumption.API
 - Execute command ng build - this command build front-end project and publish it into wwwroot folder
 - Execute command dotnet run
 - Open browser and access url http://localhost:53105/
 
 # Docker for testing purposes
 
  - Install docker client
  - Open Command Prompt
  - Execute docker pull caetanomb/flightfuelconsumptionapi
  - docker run -d -p "8000:80" caetanomb/flightfuelconsumptionapi
  - Open browser and type this url http://localhost:8000/
  
 ## Stop flightfuelconsumptionapi docker container
  - List the latest created container for that execute docker ps -l
  - Copy its ID  
  - Execute docker stop "ID"
