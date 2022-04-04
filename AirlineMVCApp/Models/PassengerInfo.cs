using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineMVCApp.Models
{
    public class PassengerInfo
    {
        [ForeignKey("ApplicationUser")]
        public string PassengerInfoId { get; set; }
        public bool IsBoarded { get; set; }
        public bool IsRequestingService { get; set; }
        public int BoardedFlight { get; set; } = 0;
        public List<Flight> BookedFlights { get; set; } = new List<Flight>();


        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
