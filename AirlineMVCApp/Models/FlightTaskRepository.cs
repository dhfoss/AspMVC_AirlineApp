using System.Linq;

namespace AirlineMVCApp.Models
{
    public class FlightTaskRepository : IFlightTaskRepository
    {
        private readonly AppDbContext _context;
        public FlightTaskRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public void AddNewFlightToFlightTask(int flightId)
        {
            Flight flight = _context.Flights.FirstOrDefault(f => f.FlightId == flightId);
            var task = _context.FlightTasks.ToList().First();
            task.FlightsWithThisTask.Add(flight);
            _context.SaveChanges();
        }
    }
}
