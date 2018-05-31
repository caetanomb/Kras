using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FlightFuelConsumption.Infrastructure.Migrations
{
    public partial class PopulateAirportTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT Airport ON");
            migrationBuilder.Sql("INSERT INTO Airport (Id, Name, Latitude, Longitude) VALUES (1, 'Galeao Airport', -22.805436, -43.256334)");
            migrationBuilder.Sql("INSERT INTO Airport (Id, Name, Latitude, Longitude) VALUES (2, 'Confins Airport', -19.634150, -43.965385)");
            migrationBuilder.Sql("INSERT INTO Airport (Id, Name, Latitude, Longitude) VALUES (3, 'Schiphol Airport', 52.311656, 4.768756)");
            migrationBuilder.Sql("INSERT INTO Airport (Id, Name, Latitude, Longitude) VALUES (4, 'Eindhoven Airport', 51.458270, 5.393663)");
            migrationBuilder.Sql("SET IDENTITY_INSERT Airport OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Airport WHERE Id IN (1, 2, 3, 4)");
        }
    }
}
