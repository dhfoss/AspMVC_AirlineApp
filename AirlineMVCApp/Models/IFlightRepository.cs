using System.Collections.Generic;

namespace AirlineMVCApp.Models
{
    public interface IFlightRepository
    {
        public List<Flight> GetAllFlightsThatHaveNotDeparted();
        public Flight SelectFlighByFlightId(int flightId);
        public Flight SelectFlightWithPassengersByFlightId(int flightId);
        public Flight SelectFlightWithFlightTaskByFlightId(int flightId);
        public void InsertFlight(Flight flight);
        public void EndFlight(int flightId);
        public void ProgressFlight(int flightId);
        public Flight SelectFlightInProgress();
        public List<Flight> SelectReservationsByPassengerId(string passengerId);
        public List<Flight> SelectAvailableFlightsForPurchaseByPassengerId(string passengerId);
        public void InsertReservation(int flightId, string passengerId);
        public void UseReservations(int flightId);
        public List<Flight> GetThreeSoonestFlights();
    }
}
