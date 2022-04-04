using Microsoft.EntityFrameworkCore.Migrations;

namespace AirlineMVCApp.Migrations
{
    public partial class GetBoardedPassengersByFlightIdStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"USE [AirlinesMVCAppDatabase]
                                GO
                                SET ANSI_NULLS ON
                                GO
                                SET QUOTED_IDENTIFIER ON
                                GO
                                -- =============================================
                                -- Author:		<Daniel Hawthorne-Foss>
                                -- Create date: <12/2/21>
                                -- Description:	<Join AspUsers with PassengerInfo>
                                -- =============================================
                                CREATE PROCEDURE [dbo].[GetBoardedPassengersByFlightId]
	                                @FlightId int
                                AS
                                BEGIN
	                                SET NOCOUNT ON;

	                                SELECT 
		                                u.FirstName, u.LastName, p.IsRequestingService, p.PassengerInfoId
	                                FROM PassengerInfo p
	                                JOIN AspNetUsers u ON u.Id = p.PassengerInfoId
	                                JOIN FlightPassengerInfo fp ON fp.BookedPassengersPassengerInfoId = p.PassengerInfoId
	                                WHERE @FlightId = fp.BookedFlightsFlightId AND p.IsBoarded = 'True';

                                END";

            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string removeProcedure = @"DROP PROCEDURE [dbo].[GetBoardedPassengersByFlightId]";
            migrationBuilder.Sql(removeProcedure);
        }
    }
}
