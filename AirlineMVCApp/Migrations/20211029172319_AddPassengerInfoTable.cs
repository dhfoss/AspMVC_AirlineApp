using Microsoft.EntityFrameworkCore.Migrations;

namespace AirlineMVCApp.Migrations
{
    public partial class AddPassengerInfoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserFlight");

            migrationBuilder.DropColumn(
                name: "BoardedFlight",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsBoarded",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsRequestingService",
                table: "AspNetUsers");

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

            migrationBuilder.CreateTable(
                name: "PassengerInfo",
                columns: table => new
                {
                    PassengerInfoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsBoarded = table.Column<bool>(type: "bit", nullable: false),
                    IsRequestingService = table.Column<bool>(type: "bit", nullable: false),
                    BoardedFlight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerInfo", x => x.PassengerInfoId);
                    table.ForeignKey(
                        name: "FK_PassengerInfo_AspNetUsers_PassengerInfoId",
                        column: x => x.PassengerInfoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Flights_FlightId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_PassengerInfo_PassengerInfoId",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "PassengerInfo");

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

            migrationBuilder.AddColumn<int>(
                name: "BoardedFlight",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsBoarded",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRequestingService",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ApplicationUserFlight",
                columns: table => new
                {
                    BookedFlightsFlightId = table.Column<int>(type: "int", nullable: false),
                    BookedPassengersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserFlight", x => new { x.BookedFlightsFlightId, x.BookedPassengersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserFlight_AspNetUsers_BookedPassengersId",
                        column: x => x.BookedPassengersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserFlight_Flights_BookedFlightsFlightId",
                        column: x => x.BookedFlightsFlightId,
                        principalTable: "Flights",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserFlight_BookedPassengersId",
                table: "ApplicationUserFlight",
                column: "BookedPassengersId");
        }
    }
}
