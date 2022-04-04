using AirlineMVCApp.Models;
using AirlineMVCApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirlineMVCApp.Controllers
{
    [Authorize(Roles = "BookingAgent, Admin")]
    public class BookingAgentController : Controller
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IPassengerInfoRepository _passengerInfoRepository;
        private UserManager<ApplicationUser> _userManager;

        public BookingAgentController(IFlightRepository flightRepository, IPassengerInfoRepository passengerInfoRepository, UserManager<ApplicationUser> userManager)
        {
            _flightRepository = flightRepository;
            _passengerInfoRepository = passengerInfoRepository;
            _userManager = userManager;
        }

        public IActionResult AddFlight()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFlight(Flight flight)
        {
            if (ModelState.IsValid)
            {
                _flightRepository.InsertFlight(flight);
                return RedirectToAction("ViewAllFlights", "Home");
            }
            return View(flight);
        }

        public IActionResult ViewPassengerManifests()
        {
            var flights = _flightRepository.GetAllFlightsThatHaveNotDeparted();
            var tableWithButtonViewModel = new TableWithButtonViewModel()
            {
                Flights = flights,
                ButtonColumnHeader = "View Manifest",
                Controller = "BookingAgent",
                Action = "ViewManifest"
            };
            return View(tableWithButtonViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ViewManifest(int id)
        {
            var requestData = HttpContext.Request.Headers;
            var passengersInfo = _passengerInfoRepository.SelectManifestByFlightId(id);
            var flight = _flightRepository.SelectFlighByFlightId(id);
            var passengers = new List<Passenger>();
            foreach (var passengerInfo in passengersInfo)
            {
                var passenger = await _userManager.FindByIdAsync(passengerInfo.PassengerInfoId);
                passengers.Add(new Passenger { FirstName = passenger.FirstName, LastName = passenger.LastName, PassengerId = passengerInfo.PassengerInfoId });
            }
            var manifestViewModel = new ManifestViewModel()
            {
                Passengers = passengers,
                Flight = flight
            }; 

            return View(manifestViewModel);
        }

        public IActionResult RemovePassengerFromManifest(string id, int flightId)
        {
            _passengerInfoRepository.RemovePassengerFromManifestById(id, flightId);
            return RedirectToAction("ViewManifest", new { id = flightId });
        }

    }
}
