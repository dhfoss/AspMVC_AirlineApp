namespace AirlineMVCApp.Models
{
    public interface IFlightTaskRepository
    {
        public void AddNewFlightToFlightTask(int flightId);
    }
}
