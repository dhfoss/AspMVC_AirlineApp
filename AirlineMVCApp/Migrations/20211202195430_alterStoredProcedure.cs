using Microsoft.EntityFrameworkCore.Migrations;

namespace AirlineMVCApp.Migrations
{
    public partial class alterStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string alteredProcedure = @"USE [AirlinesMVCAppDatabase]
                                        GO
                                        /****** Object:  StoredProcedure [dbo].[GetBoardedPassengersByFlightId]    Script Date: 12/2/2021 2:10:50 PM ******/
                                        SET ANSI_NULLS ON
                                        GO
                                        SET QUOTED_IDENTIFIER ON
                                        GO
                                        -- =============================================
                                                                        -- Author:		<Daniel Hawthorne-Foss>
                                                                        -- Create date: <12/2/21>
                                                                        -- Description:	<Join AspUsers with PassengerInfo>
                                                                        -- =============================================
                                                                        ALTER PROCEDURE [dbo].[GetBoardedPassengersByFlightId]
	                                                                        @FlightId int
                                                                        AS
                                                                        BEGIN
	                                                                        SET NOCOUNT ON;

	                                                                        SELECT 
		                                                                        u.FirstName, u.LastName, p.IsRequestingService, p.PassengerInfoId, u.Id AS PassengerId
	                                                                        FROM PassengerInfo p
	                                                                        JOIN AspNetUsers u ON u.Id = p.PassengerInfoId
	                                                                        JOIN FlightPassengerInfo fp ON fp.BookedPassengersPassengerInfoId = p.PassengerInfoId
	                                                                        WHERE @FlightId = fp.BookedFlightsFlightId AND p.IsBoarded = 'True';

                                                                        END";
            migrationBuilder.Sql(alteredProcedure);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
