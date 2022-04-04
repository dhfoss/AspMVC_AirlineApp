using System.Collections.Generic;

namespace AirlineMVCApp.Models
{
    public interface IPassengerInfoRepository
    {
        public PassengerInfo SelectPassengerById(string passengerId);
        public List<PassengerInfo> GetBoardedPassengersByFlightId(int flightId);
        public void DeboardPassengers(int flightId);
        public string InsertPassengerReturnId(PassengerInfo passengerInfo);
        public void UpdatePassengerServiceById(string passengerId, bool isRequestingService);
        public void SetPassengerToBoarded(string passengerId, int flightId);
        public List<PassengerInfo> SelectManifestByFlightId(int flightId);

        public void RemovePassengerFromManifestById(string passengerId, int flightId);
        public List<BoardedPassenger> AltGetBoardedPassengers(int flightId);

    }
}
