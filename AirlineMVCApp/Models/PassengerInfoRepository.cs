using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AirlineMVCApp.Models
{
    public class PassengerInfoRepository : IPassengerInfoRepository
    {
        private readonly AppDbContext _context;

        public PassengerInfoRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public void DeboardPassengers(int flightId)
        {
            var boardedPassengers = GetBoardedPassengersByFlightId(flightId);
            boardedPassengers.ForEach(p =>
            {
                p.IsBoarded = false;
                p.IsRequestingService = false;
                p.BoardedFlight = 0;
            });
            _context.SaveChanges();
        }

        public List<PassengerInfo> GetBoardedPassengersByFlightId(int flightId)
        {
            var flight = _context.Flights.Include(f => f.BookedPassengers.Where(p => p.IsBoarded == true)).FirstOrDefault(f => f.FlightId == flightId);
            return flight.BookedPassengers;
        }

        public List<BoardedPassenger> AltGetBoardedPassengers(int flightId)
        {
            List<BoardedPassenger> boardedPassengers = new List<BoardedPassenger>();
            boardedPassengers = _context.BoardedPassengers.FromSqlRaw($"[dbo].[GetBoardedPassengersByFlightId] {flightId}").ToList();
            return boardedPassengers;
        }

        public string InsertPassengerReturnId(PassengerInfo passengerInfo)
        {
            _context.PassengerInfo.Add(passengerInfo);
            _context.SaveChanges();
            return passengerInfo.PassengerInfoId;
        }


        public List<PassengerInfo> SelectManifestByFlightId(int flightId)
        {
            return _context.Flights.Include(f => f.BookedPassengers).Where(f => f.FlightId == flightId).FirstOrDefault().BookedPassengers.ToList();
        }

        public PassengerInfo SelectPassengerById(string passengerId)
        {
            return _context.PassengerInfo.Find(passengerId);
        }

        public void SetPassengerToBoarded(string passengerId, int flightId)
        {
            var passengerInfo = _context.PassengerInfo.Find(passengerId);
            passengerInfo.IsBoarded = true;
            passengerInfo.BoardedFlight = flightId;
            _context.SaveChanges();
        }

        public void UpdatePassengerServiceById(string passengerId, bool isRequestingService)
        {
            _context.PassengerInfo.Find(passengerId).IsRequestingService = isRequestingService;
            _context.SaveChanges();
        }

        public void RemovePassengerFromManifestById(string passengerId, int flightId)
        {
            var flight = _context.Flights.Find(flightId);
            var passenger = _context.PassengerInfo.Include(p => p.BookedFlights).FirstOrDefault(p => p.PassengerInfoId == passengerId);
            var beforeDelete = passenger.BookedFlights;
            passenger.BookedFlights.Remove(flight);


            _context.SaveChanges();

            var afterDelete = passenger.BookedFlights;

        }
    }
}
