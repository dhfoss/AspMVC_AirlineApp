using Microsoft.EntityFrameworkCore.Migrations;

namespace AirlineMVCApp.Migrations
{
    public partial class BoardedPassengersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoardedPassengers",
                columns: table => new
                {
                    PassengerInfoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRequestingService = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassengerId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardedPassengers");
        }
    }
}
