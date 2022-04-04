using AirlineMVCApp.Models;
using AirlineMVCApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;


namespace AirlineMVCApp.Controllers
{
    [Authorize]
    public class PassengerController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IFlightRepository _flightRepository;
        private readonly IPassengerInfoRepository _passengerInfoRepository;


        public PassengerController(UserManager<ApplicationUser> userManager, IFlightRepository flightRepository, IPassengerInfoRepository passengerInfoRepository)
        {
            _userManager = userManager;
            _flightRepository = flightRepository;
            _passengerInfoRepository = passengerInfoRepository;
        }

        public IActionResult BookAFlight()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var flights = _flightRepository.SelectAvailableFlightsForPurchaseByPassengerId(userId);
            var tableWithButtonViewModel = new TableWithButtonViewModel()
            {
                Flights = flights,
                ButtonColumnHeader = "Book Flight",
                Controller = "Passenger",
                Action = "BookSelectedFlight"
            };
            return View(tableWithButtonViewModel);
        }

        public async Task<IActionResult> BookSelectedFlight(int id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            _flightRepository.InsertReservation(id, userId);
            return RedirectToAction("ViewReservations");
        }

        [HttpGet]
        public IActionResult ViewReservations()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var flights = _flightRepository.SelectReservationsByPassengerId(userId);

            bool isBoarded = _passengerInfoRepository.SelectPassengerById(userId).IsBoarded;

            //if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            //    return PartialView("_IndexGrid", flights);

            //return View(flights);



            var tableWithButtonViewModel = new TableWithButtonViewModel()
            {
                Flights = flights,
                ButtonColumnHeader = "Board Flight",
                Controller = "Passenger",
                Action = "BoardAFlight"
            };

            if (isBoarded)
            {
                tableWithButtonViewModel.ButtonDisabled = "disabled boardBtn";
            }

            return View(tableWithButtonViewModel);

        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult FilterReservations(SearchQuery query)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var flights = _flightRepository.SelectReservationsByPassengerId(userId);
            var filter = query.Text;
            var twoFlights = flights.Take(2).ToList();
            return PartialView("_IndexGrid", twoFlights);
        }

        public IActionResult BoardAFlight(int id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            _passengerInfoRepository.SetPassengerToBoarded(userId, id);
            return RedirectToAction("PassengerFlightInProgress");
        }

        public async Task<IActionResult> PassengerFlightInProgress()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var passengerInfo = _passengerInfoRepository.SelectPassengerById(userId);
            var boardedFlight = _flightRepository.SelectFlighByFlightId(passengerInfo.BoardedFlight);

            var passengerInFlightViewModel = new PassengerInFlightViewModel()
            {
                Flight = boardedFlight,
                PassengerInfo = passengerInfo
            };
            return View(passengerInFlightViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePassengerService(PassengerInfo passengerInfo)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            _passengerInfoRepository.UpdatePassengerServiceById(userId, passengerInfo.IsRequestingService);
            return RedirectToAction("Index", "Home");
        }
    }
}
