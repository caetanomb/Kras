# Flight Fuel Consumption

CRUD web application - A user is able to enter a Flight which has a departure airport and a destination airport.
The Domain Model calculates the distance from departure and destination airport and the fuel needed.

Domain rule: Fuel consumption = (distance / flight time) + takeoff effort

A report page shows a summary of all entered data including the calculate data

# Details - Frameworks, Designs patterns

 - ASP.Net Web Api Core 2.0
 - Unit tests - Xunit.net
 - MediatR
 - CQRS - Command Query Responsibility Segregation
 - Repository
 - Domain Model
 - Dependency Injection
 
 # How to run
 
 - Open Command Prompt
 - Access directory \FlightFuelConsumption.API from the root folder
 - Execute command dotnet run
