using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirlineMVCApp.Models
{
    public class Flight : IEquatable<Flight>
    {
        public int FlightId { get; set; }

        [Required]
        [Display(Name = "Flight Number")]
        [StringLength(6)]
        public string FlightNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Departure City")]
        public string DepartureCity { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Arrival City")]
        public string ArrivalCity { get; set; }

        [Required]
        [Range(1, 100)]
        public int NumberOfSeats { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime TravelDate { get; set; }
        public bool IsInTransit { get; set; }
        public bool IsComplete { get; set; }
        public FlightTask NextFlightTask { get; set; }
        public List<PassengerInfo> BookedPassengers { get; set; } = new List<PassengerInfo>();


        public bool Equals(Flight other)
        {
            if (other is null)
                return false;
            return this.FlightId == other.FlightId;
        }
        public override bool Equals(object obj) => Equals(obj as Flight);
        public override int GetHashCode() => (FlightId).GetHashCode();


    }
}
