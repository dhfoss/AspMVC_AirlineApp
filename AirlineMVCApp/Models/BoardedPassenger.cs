using System.ComponentModel.DataAnnotations;

namespace AirlineMVCApp.Models
{
    public class BoardedPassenger : Passenger
    {
        public string PassengerInfoId { get; set; }

        [Display(Name = "Request Service?")]
        public bool IsRequestingService { get; set; }
    }
}
