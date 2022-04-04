using AirlineMVCApp.Models;
using AirlineMVCApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineMVCApp.Controllers
{
    [Authorize(Roles = "FlightAttendant, Admin")]

    public class FlightAttendantController : Controller
    {
        private readonly IPassengerInfoRepository _passengerInfoRepository;
        private readonly IFlightRepository _flightRepository;
        private UserManager<ApplicationUser> _userManager;

        public FlightAttendantController(IPassengerInfoRepository passengerInfoRepository, IFlightRepository flightRepository, UserManager<ApplicationUser> userManager)
        {
            _passengerInfoRepository = passengerInfoRepository;
            _flightRepository = flightRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> ServicePassengers()
        {
            var flight = _flightRepository.SelectFlightInProgress();
            if(flight == null)
            {
                return RedirectToAction("Index", "FlightNotFound");
            }
            var boardedPassengers = _passengerInfoRepository.AltGetBoardedPassengers(flight.FlightId);
            var servicePassengersViewModel = new ServicePassengersViewModel()
            {
                Flight = flight,
                BoardedPassengers = boardedPassengers.OrderBy(p => p.LastName).ToList()
            };

            return View(servicePassengersViewModel);
        }

        public IActionResult ServicePassenger(string id)
        {
            _passengerInfoRepository.UpdatePassengerServiceById(id, false);
            return RedirectToAction("ServicePassengers");
        }
    }
}
