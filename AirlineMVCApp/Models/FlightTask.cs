using System.Collections.Generic;

namespace AirlineMVCApp.Models
{
    public class FlightTask
    {
        public int FlightTaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskCompleteMessage { get; set; }
        public List<Flight> FlightsWithThisTask { get; set; } = new List<Flight>();
    }
}
