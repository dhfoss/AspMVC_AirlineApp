using Microsoft.EntityFrameworkCore.Migrations;

namespace AirlineMVCApp.Migrations
{
    public partial class AddPassengerInfoToFlightsManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Flights_FlightId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_PassengerInfo_PassengerInfoId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_PassengerInfoId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FlightId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PassengerInfoId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "FlightPassengerInfo",
                columns: table => new
                {
                    BookedFlightsFlightId = table.Column<int>(type: "int", nullable: false),
                    BookedPassengersPassengerInfoId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightPassengerInfo", x => new { x.BookedFlightsFlightId, x.BookedPassengersPassengerInfoId });
                    table.ForeignKey(
                        name: "FK_FlightPassengerInfo_Flights_BookedFlightsFlightId",
                        column: x => x.BookedFlightsFlightId,
                        principalTable: "Flights",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightPassengerInfo_PassengerInfo_BookedPassengersPassengerInfoId",
                        column: x => x.BookedPassengersPassengerInfoId,
                        principalTable: "PassengerInfo",
                        principalColumn: "PassengerInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightPassengerInfo_BookedPassengersPassengerInfoId",
                table: "FlightPassengerInfo",
                column: "BookedPassengersPassengerInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightPassengerInfo");

            migrationBuilder.AddColumn<string>(
                name: "PassengerInfoId",
                table: "Flights",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flights_PassengerInfoId",
                table: "Flights",
                column: "PassengerInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FlightId",
                table: "AspNetUsers",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Flights_FlightId",
                table: "AspNetUsers",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_PassengerInfo_PassengerInfoId",
                table: "Flights",
                column: "PassengerInfoId",
                principalTable: "PassengerInfo",
                principalColumn: "PassengerInfoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
