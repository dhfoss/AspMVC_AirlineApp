using Microsoft.EntityFrameworkCore.Migrations;

namespace AirlineMVCApp.Migrations
{
    public partial class SeedFlightTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FlightTasks",
                columns: new[] { "FlightTaskId", "TaskCompleteMessage", "TaskName" },
                values: new object[,]
                {
                    { 1, "No task", "No task" },
                    { 2, "This is your Captain speaking, sit back and enjoy the flight.", "Greet Passengers" },
                    { 3, "Flight has started. All systems go.  Good luck, Captain.", "Start Flight" },
                    { 4, "Flight has ascended.", "Ascend Flight" },
                    { 5, "Another happy landing. Well done, Captain.", "Descend Flight" },
                    { 6, "No more tasks for this flight.", "Tasks Complete" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FlightTasks",
                keyColumn: "FlightTaskId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FlightTasks",
                keyColumn: "FlightTaskId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FlightTasks",
                keyColumn: "FlightTaskId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FlightTasks",
                keyColumn: "FlightTaskId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FlightTasks",
                keyColumn: "FlightTaskId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FlightTasks",
                keyColumn: "FlightTaskId",
                keyValue: 6);
        }
    }
}
