using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirlineMVCApp.Models
{
    public class FlightRepository : IFlightRepository
    {
        private readonly AppDbContext _context;

        public FlightRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public void EndFlight(int flightId)
        {
            var flight = _context.Flights.Find(flightId);
            var task = _context.FlightTasks.ToList().Last();
            flight.IsComplete = true;
            flight.IsInTransit = false;
            task.FlightsWithThisTask.Add(flight);
            _context.SaveChanges();
        }

        public List<Flight> GetAllFlightsThatHaveNotDeparted()
        {
            return _context.Flights.Include(f => f.BookedPassengers).Where(f => !f.IsComplete && !f.IsInTransit).OrderBy(f => f.TravelDate).ToList();
        }

        public void InsertFlight(Flight flight)
        {
            _context.Flights.Add(flight);
            _context.SaveChanges();
            var task = _context.FlightTasks.ToList().First();
            task.FlightsWithThisTask.Add(flight);
            _context.SaveChanges();
        }

        public void InsertReservation(int flightId, string passengerId)
        {
            var passengerInfo = _context.PassengerInfo.Find(passengerId);
            _context.Flights.Find(flightId).BookedPassengers.Add(passengerInfo);
            _context.SaveChanges();
        }

        public void ProgressFlight(int flightId)
        {
            var flight = _context.Flights.Include(f => f.NextFlightTask).FirstOrDefault(f => f.FlightId == flightId);
            flight.IsInTransit = true;
            int flightTaskId = flight.NextFlightTask.FlightTaskId;
            var newFlightTask = _context.FlightTasks.Find(flightTaskId + 1);
            newFlightTask.FlightsWithThisTask.Add(flight);
            _context.SaveChanges();
        }

        public List<Flight> SelectAvailableFlightsForPurchaseByPassengerId(string passengerId)
        {
            var existingFlights = GetAllFlightsThatHaveNotDeparted();
            var existingReservations = SelectReservationsByPassengerId(passengerId);
            return existingFlights.Except(existingReservations).ToList();
        }

        public Flight SelectFlighByFlightId(int flightId)
        {
            return _context.Flights.Find(flightId);
        }

        public Flight SelectFlightInProgress()
        {
            return _context.Flights.FirstOrDefault(f => f.IsInTransit);
        }

        public Flight SelectFlightWithPassengersByFlightId(int flightId)
        {
            return _context.Flights.Include(f => f.BookedPassengers).Where(f => f.FlightId == flightId).FirstOrDefault();
        }

        public Flight SelectFlightWithFlightTaskByFlightId(int flightId)
        {
            return _context.Flights.Include(f => f.NextFlightTask).Where(f => f.FlightId == flightId).FirstOrDefault();
        }

        public List<Flight> SelectReservationsByPassengerId(string passengerId)
        {
            var passengerInfo = _context.PassengerInfo.Include(p => p.BookedFlights).FirstOrDefault(p => p.PassengerInfoId == passengerId);
            return passengerInfo.BookedFlights.Where(f => !f.IsComplete && !f.IsInTransit).ToList();
        }

        public void UseReservations(int flightId)
        {
            // For now, do nothing
        }

        public List<Flight> GetThreeSoonestFlights()
        {
            try
            {
                var flights = _context.Flights.Where(f => (!f.IsComplete && !f.IsInTransit)).OrderBy(f => f.TravelDate).ToList();
                if (flights.Count < 3)
                    return flights;
                return flights.Take(3).ToList();
            }
            catch(Exception ex)
            {
                return new List<Flight>();
            }
        }
    }
}
